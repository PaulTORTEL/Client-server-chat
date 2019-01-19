using Chat.Listeners;
using ChatPackets;
using System;
using System.Collections.Concurrent;
using System.IO;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Chat
{
    class Client
    {

        private TcpClient socket;
        private string ipAdress;
        private Int32 port;

        private Thread threadIn;
        private Thread threadOut;
        private bool listening;

        private NetworkStream clientStream;
        private IFormatter serializer;

        private BlockingConcurrentQueue<Packet> packets;

        private Client()
        {
            ipAdress = "localhost";
            port = 12345;
            socket = new TcpClient();
            serializer = new BinaryFormatter();
            serializer.Binder = new CustomBinder();
            threadIn = new Thread(new ThreadStart(listen));
            threadOut = new Thread(new ThreadStart(send));
            packets = new BlockingConcurrentQueue<Packet>();
        }

        public bool Start()
        {
            Console.WriteLine("\t\t\t\t\tCLIENT SIDE\n");
            try
            {
                socket.Connect(ipAdress, port);
                
                Console.WriteLine("[Client] Connected to server!");
            } catch (Exception e)
            {
                Console.WriteLine("[Client] Can't reach server");
                return false;
            }

            clientStream = socket.GetStream();
            listening = true;
            threadIn.Start();
            threadOut.Start();

            return true;
        }


        public void Register(string username, string password)
        {
            SHA256Managed sha = new SHA256Managed();
            StringBuilder hashedPassword = new System.Text.StringBuilder();
            byte[] crypto = sha.ComputeHash(Encoding.UTF8.GetBytes(password), 0, Encoding.UTF8.GetByteCount(password));
            foreach (byte theByte in crypto)
            {
                hashedPassword.Append(theByte.ToString("x2"));
            }
            RegisterPacket packet = new RegisterPacket();
            packet.username = username;
            packet.password = hashedPassword.ToString();
            SendPacket(packet);
        }

        public void Login(string username, string password)
        {
            SHA256Managed sha = new SHA256Managed();
            StringBuilder hashedPassword = new System.Text.StringBuilder();
            byte[] crypto = sha.ComputeHash(Encoding.UTF8.GetBytes(password), 0, Encoding.UTF8.GetByteCount(password));
            foreach (byte theByte in crypto)
            {
                hashedPassword.Append(theByte.ToString("x2"));
            }
            LoginPacket packet = new LoginPacket();
            packet.username = username;
            packet.password = hashedPassword.ToString();
            packet.token = "";
            SendPacket(packet);
         }

        public void Disconnect(bool delete = true)
        {
            if (delete)
            try
            {
                if (File.Exists("save.user"))
                {
                    File.Delete("save.user");
                    Console.WriteLine("[Client] Token destroyed");
                }
                else
                {
                    Console.WriteLine("[Client] No token to destroy");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
          
            listening = false;
            Application.Exit();
            Environment.Exit(0);
        }

        public void SendPacket(Packet packet)
        {
            packets.Enqueue(packet);
        }

        private void listen()
        {
            Console.WriteLine("[Client] Now listening for server packets ...");

            while (listening)
            {
                try
                {
                    Packet p = (Packet)serializer.Deserialize(clientStream);
                    ListenerManager.Instance.onPacketReceived(p);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Client can't reach the server ({0}).", e.GetBaseException());
                    socket.Close();
                    listening = false;
                }
            }
        }

        private void send()
        {
            while (listening)
            {
                Packet packet = packets.TryDequeue();

                if (packet != null)
                {
                    try
                    {
                        serializer.Serialize(clientStream, packet);
                        Console.WriteLine("[Client] Packet sent! (" + packet.GetType().ToString() + ")");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                }
            }
        }

        public void registerToken(String token)
        {
            ClientSave saveBean = new ClientSave();
            saveBean.Token = token;

            FileStream fs = new FileStream("save.user", FileMode.Create);
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(fs, saveBean);
            fs.Close();
            
        }

        public String readClientSave()
        {
            try
            {
                FileStream fs = new FileStream("save.user", FileMode.Open);
                BinaryFormatter bf = new BinaryFormatter();
                ClientSave saveBean = (ClientSave)bf.Deserialize(fs);
                fs.Close();
                if (saveBean.Token == null || saveBean.Token.Length < 5)
                    return "no token";

                return saveBean.Token;

            } catch (Exception e)
            {
                return "error";
            }
        }


        public static Client Instance { get { return Singleton.instance; } }

        private class Singleton
        {

            static Singleton()
            {
            }

            internal static readonly Client instance = new Client();
        }
    }
}
