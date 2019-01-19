using ChatPackets;
using ServerChat.Channels;
using ServerChat.Clients;
using ServerChat.Users;
using System;
using System.Net;
using System.Net.Sockets;

namespace ServerChat
{
    class Server
    {
        private TcpListener server;
        private ClientManager clientManager;
        private bool running;

        private Server()
        {
            IPAddress ip = IPAddress.Parse("127.0.0.1");
            Int32 port = 12345;
            try
            {
                Console.WriteLine("\t\t\t\t\tSERVER SIDE\n");
                server = new TcpListener(ip, port);
                clientManager = new ClientManager(server);
                UserManager.Instance.loadFile();
                ChannelManager.Instance.loadChannels();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            
        }

        public void Start()
        {
            server.Start();
            Console.WriteLine("[Server] Server started!");
            clientManager.Start();
            running = true;
            HandleConsole();
        }


        private void HandleConsole()
        {
            while (running)
            {
                string commandLine = Console.ReadLine().Trim();
                string command = commandLine;

                if (command.Contains(" "))
                {
                    command = command.Substring(0, command.IndexOf(" "));
                }

                string argsStr = commandLine.Substring(command.Length).Trim();
              
                if (argsStr.Length > 0)
                {
                    string[] args = argsStr.Split(' ');
                    HandleCommand(command, args);
                }
                else
                {
                    HandleSimpleCommand(command);
                }
  
            }
        }

        private void HandleCommand(String command, String[] args)
        {
            if (command.Equals("broadcast") && args.Length > 1)
            {
                String message = "";
                String channel = args[0];

                if (!ChannelManager.Instance.channelExists(channel))
                {
                    Console.WriteLine("[Error] This channel does not exists!");
                    return;
                }

                bool init = false;
                foreach (String str in args)
                {
                    if (init)
                        message += (str + " ");
                    else
                        init = true;
                }
                message = message.Trim();

                MessagePacket p = new MessagePacket();
                p.date = DateTime.Now;
                p.channelName = channel;
                p.success = true;
                p.message = message;
                p.sender = "[SERVER]";
                ChannelManager.Instance.toSend(p);

                Console.WriteLine("[Server] Broadcast done!");


                return;
            }
            else if (command.Equals("privilege") && args.Length == 2)
            {
                String username = args[0];
                int privilege = -1;
                if (!int.TryParse(args[1], out privilege))
                {
                    Console.WriteLine("[Error] Privilege must be a number");
                    return;
                }

                if (privilege < 0 || privilege > 1)
                {
                    Console.WriteLine("[Error] Privilege must be 0 or 1");
                    return;
                }

                User user;
                if (!UserManager.Instance.RegisteredMembers.TryGetValue(username, out user))
                {
                    Console.WriteLine("[Error] This user does not exist!");
                    return;
                }

                user.Rank = privilege;

                Console.WriteLine("[Server] " + username + " has now privilege : " + privilege);

                UserManager.Instance.saveUser(user.Username);

                return;
            }

            Console.WriteLine("[Error] Command unknown");
        }

        private void HandleSimpleCommand(String command)
        {
            Console.WriteLine("[Error] Command unknown");
        }










        public static Server Instance { get { return Singleton.instance; } }

        private class Singleton
        {
   
            static Singleton()
            {
            }

            internal static readonly Server instance = new Server();
        }
    }
}
