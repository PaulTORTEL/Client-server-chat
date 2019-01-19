using ChatPackets;
using ServerChat.Channels;
using ServerChat.Clients;
using ServerChat.Exceptions;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace ServerChat.Users
{
    class UserManager
    {
        // Username - User
        public ConcurrentDictionary<String, User> RegisteredMembers { get; set; }

        // Token - Username
        private ConcurrentDictionary<String, String> TokenUser { get; set; }

        // Token - Client
        public ConcurrentDictionary<String, Client> OnlineUsers { get; private set; }

        public static UserManager Instance { get { return Singleton.instance; } }

        private UserManager ()
        {
            RegisteredMembers = new ConcurrentDictionary<String, User>();
            TokenUser = new ConcurrentDictionary<String, String>();
            OnlineUsers = new ConcurrentDictionary<String, Client>();
        }

        public void loadFile()
        {
            BinaryFormatter bf = new BinaryFormatter();
            Boolean success = true;
            foreach (string file in Directory.EnumerateFiles("saves", "*.user"))
            {
                try
                {
                    FileStream fs = new FileStream(file, FileMode.Open);
                    User user = (User)bf.Deserialize(fs);
                    RegisteredMembers.TryAdd(user.Username, user);
                    fs.Close();
                } catch (Exception e)
                {
                    success = false;
                    Console.WriteLine(e);
                }
            }

            if (success)
                Console.WriteLine("[UserManager] Users saved successfully loaded!");
        }

        public void saveUser(String username)
        {
            if (!isUsernameExisting(username))
                return;

            User u = RegisteredMembers[username];
            System.IO.Directory.CreateDirectory("saves");
            FileStream fs = new FileStream("saves/" + username + ".user", FileMode.Create);
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(fs, u);
            fs.Close();

        }

        public Boolean isUserConnected(String token)
        {
            if (OnlineUsers.ContainsKey(token))
                return true;

            return false;
        }

        public Boolean isTokenExisting(String token)
        {
            if (TokenUser.ContainsKey(token))
                return true;

            return false;
        }

        public Boolean isUserAdmin(String token)
        {
            if (isTokenExisting(token))
            {
                User user = getUserFromToken(token);

                if (user.Rank == 1)
                    return true;
            }
            return false;
        }

        public Boolean isUsernameExisting(String username)
        {
            if (RegisteredMembers.ContainsKey(username))
                return true;

            return false;
        }

        public Client getClientFromToken(String token)
        {
            if (!isUserConnected(token))
                throw new UserNotCoException("This user is not connected!");

            return OnlineUsers[token];
        }

        public int getUserRank(String token)
        {
            User u = getUserFromToken(token);
            return u.Rank;
        }

        public User getUserFromToken(String token)
        {

            if (!isUserConnected(token))
                throw new UserNotCoException("This user is not connected!");

            if (!isTokenExisting(token))
                throw new UnknownTokenException("This token doesn't exist!");

            if (!isUsernameExisting(TokenUser[token]))
                throw new UnknownUsernameException("This username does not exist!");

            return RegisteredMembers[TokenUser[token]];
        }

        public String getUsernameFromToken(String token)
        {
            if (!isTokenExisting(token))
                throw new UnknownTokenException("This token doesn't exist!");

            if (!isUsernameExisting(TokenUser[token]))
                throw new UnknownUsernameException("This username does not exist!");

            return TokenUser[token];
        }

        // Generate an unique token
        private String generateToken()
        {
            Guid g = Guid.NewGuid();
            String GuidString = Convert.ToBase64String(g.ToByteArray());
            GuidString = GuidString.Replace("=", "");
            GuidString = GuidString.Replace("+", "");

            return GuidString;
        }

        // Add a token-user line in TokenUser (remove and put the new token if a token is already recorded)
        private void addTokenUser(String token, String username)
        {
            removePossibleToken(username);
            if (!isTokenExisting(token))
                TokenUser.TryAdd(token, username);
            else
                throw new TokenExistingException("This token already exist!");
        }

        // Try to find if there is already a token for this username in order to remove it (used to create a new token for the user)
        private void removePossibleToken(String username)
        {
            foreach (KeyValuePair<String, String> entry in TokenUser)
            {
                if (entry.Value.Equals(username))
                {
                    String usernameRemoved;
                    TokenUser.TryRemove(entry.Key, out usernameRemoved);
                    break;
                }
            }
        }
        
        private void addOnlineUser(String token, Client client)
        {
            if (!isUserConnected(token))
                OnlineUsers.TryAdd(token, client);

            else
                throw new UserNotCoException("This user is already connected!");
        }

        // Check if connection data sent by the client are correct
        private Boolean checkLoginPassword(String username, String password)
        {
            if (RegisteredMembers.ContainsKey(username))
            {
                if (RegisteredMembers[username].Password.Equals(password))
                    return true;

                return false;
            }
            else
                return false;
        }

        // Connection with token
        public void tryConnectUser(String token, Client client)
        {   
            if (!isTokenExisting(token))
                throw new UnknownTokenException();

            addOnlineUser(token, client);
        }

        // Connection with login password (used if there is no token in the loginPacket)
        public String tryConnectUser(String username, String password, Client client)
        {
            if (!checkLoginPassword(username, password))
                throw new WrongCredentialsException("Wrong credentials!");

            String token = generateToken();
            addTokenUser(token, username);
            addOnlineUser(token, client);
            return token;
        }

        //Return the new client token in order to return it to the client
        public String addNewUser(User user, Client client)
        {
            if (isUsernameExisting(user.Username))
                throw new UsernameTakenException("This user is already taken!");

            String token = generateToken();
            RegisteredMembers.TryAdd(user.Username, user);
            TokenUser.TryAdd(token, user.Username);
            OnlineUsers.TryAdd(token, client);

            saveUser(user.Username);

            return token;
        }

        // Send packets to everyone connected
        public void broadcast(Packet m)
        {
            foreach (KeyValuePair<String, Client> entry in OnlineUsers)
            {
                entry.Value.SendPacket(m);
            }
        }

        public void disconnectClient(String token)
        {
            if (OnlineUsers.ContainsKey(token))
            {
                Client clientRemoved;
                OnlineUsers.TryRemove(token, out clientRemoved);
                Console.WriteLine("[UserManager] Client disconnected!");

                try
                {
                    PresencePacket p = new PresencePacket();
                    p.connected = false;
                    p.name = getUsernameFromToken(token);
                    foreach (KeyValuePair<String, Client> entry in OnlineUsers)
                    {
                        entry.Value.SendPacket(p);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
              

            }
        }

        public void disconnectClient(Client client)
        {
            foreach (KeyValuePair<String, Client> entry in OnlineUsers)
            {
                if (entry.Value == client)
                {
                    disconnectClient(entry.Key);
                    Console.WriteLine("[UserManager] Client disconnected!");
                    break;
                }
            }
        }



        private class Singleton
        {

            static Singleton()
            {
            }

            internal static readonly UserManager instance = new UserManager();
        }
    }
}
