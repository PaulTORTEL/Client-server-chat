using ChatPackets;
using ChatPackets.login;
using ServerChat.Clients;
using ServerChat.Exceptions;
using ServerChat.Users;
using System;

namespace ServerChat.Listeners
{
    class AuthentificationListener : PacketListener
    {
        public void handle(Packet packet, Client client)
        {
            if (packet is RegisterPacket)
            {
                handleRegister((RegisterPacket)packet, client);
            }
            else if (packet is LoginPacket)
            {
                handleLogin((LoginPacket)packet, client);
            }
            else if (packet is LoginTokenPacket)
            {
                handleLogin((LoginTokenPacket)packet, client);
            }
        }

        private void handleLogin(LoginTokenPacket p, Client client)
        {
            // Log in with token
            LoginTokenResponsePacket packet = new LoginTokenResponsePacket();
            
            try
            {
                Console.WriteLine("login avec token");
                UserManager.Instance.tryConnectUser(p.token, client);
                packet.token = p.token;
                
                try
                {
                    packet.rank = UserManager.Instance.getUserRank(p.token);
                    packet.message = "";
                    packet.username = UserManager.Instance.getUsernameFromToken(p.token);
                    packet.success = true;
                }
                catch (Exception e)
                {
                    packet.success = false;
                    packet.message = e.Message;
                    packet.rank = -1;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("[Auth] Failed to connect client with token : " + p.token);
                Console.WriteLine("[ERROR] " + e.Message);
                packet.success = false;
                packet.message = e.Message;
                packet.token = "";
            }
            client.SendPacket(packet);
        }

        private void handleLogin(LoginPacket p, Client client)
        {

            LoginResponsePacket packet = new LoginResponsePacket();

            try
            {
                // Log in with username password
                packet.token = UserManager.Instance.tryConnectUser(p.username, p.password, client);

                try
                {
                    packet.rank = UserManager.Instance.getUserRank(packet.token);
                    packet.message = "";
                    packet.username = p.username;
                    packet.success = true;
                }
                catch (Exception e)
                {
                    packet.success = false;
                    packet.message = e.Message;
                    packet.rank = -1;
                }

            } catch (Exception e)
            {
                if (e is WrongCredentialsException)
                    e = (WrongCredentialsException)e;

                else if (e is TokenExistingException)
                    e = (TokenExistingException)e;

                Console.WriteLine("[Auth] Failed to connect client : " + p.username);
                Console.WriteLine("[ERROR] " + e.Message);
                packet.success = false;
                packet.message = e.Message;
                packet.token = "";
            }
            client.SendPacket(packet);
        }

        private void handleRegister(RegisterPacket p, Client client)
        {
            User user = User.createUser(p.username,p.password,0);
            
            string token;
            RegisterResponsePacket packet = new RegisterResponsePacket();

            try
            {
                token = UserManager.Instance.addNewUser(user, client);
                packet.token = token;
                Console.WriteLine("[Auth] Client registered : " + p.username);

                try
                {
                    packet.rank = UserManager.Instance.getUserRank(packet.token);
                    packet.message = "";
                    packet.username = UserManager.Instance.getUsernameFromToken(token);
                    packet.success = true;
                }
                catch (Exception e)
                {
                    packet.success = false;
                    packet.message = e.Message;
                    packet.rank = -1;
                }
              

            } catch (Exception e)
            {
                Console.WriteLine("[Auth] Failed to register client : " + p.username);
                Console.WriteLine("[ERROR] " + e.Message);
                packet.success = false;
                packet.message = e.Message;
            }
            
            client.SendPacket(packet);
        }
    }
}
