using ChatPackets;
using ServerChat.Channels;
using ServerChat.Listeners;
using ServerChat.Users;
using System;
using System.Collections.Concurrent;
using System.IO;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;


namespace ServerChat.Clients
{
    class Client
    {
        private TcpClient socket;
        private Thread threadIn;
        private Thread threadOut;
        private bool listening;

        private BlockingConcurrentQueue<Packet> _packetsToSend;

        private NetworkStream stream;
        private IFormatter serializer;

        public Client(TcpClient socket)
        {
            this.socket = socket;
            stream = socket.GetStream();
            serializer = new BinaryFormatter();
            serializer.Binder = new CustomBinder();

            threadIn = new Thread(new ThreadStart(listen));
            threadOut = new Thread(new ThreadStart(send));

            _packetsToSend = new BlockingConcurrentQueue<Packet>();

            listening = true;
            threadIn.Start();
            threadOut.Start();
            
            WelcomePacket packet = new WelcomePacket();
            packet.clientConnected = true;
			
            this.SendPacket(packet);


        }

        private void listen()
        {
            Console.WriteLine("[Client] Listening from new client");
            while (listening)
            {
                try
                {
                    Packet p = (Packet)serializer.Deserialize(stream);
                    ListenerManager.Instance.onPacketReceived(p, this);
                }
                catch (IOException e)
                {
                    socket.Close();
                    listening = false;
                    UserManager.Instance.disconnectClient(this);
                    
                } catch(SerializationException e)
                {

                }
            }
        }

        private void send()
        {
            while (listening)
            {
                Packet packetToSend = _packetsToSend.TryDequeue();

                if (packetToSend != null)
                {
                    try
                    {
                        serializer.Serialize(stream, packetToSend);
                        Console.WriteLine("[Client] Packet sent! (" + packetToSend.GetType().ToString() + ")");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                }
            }
        }

        public void SendPacket(Packet p)
        {
            _packetsToSend.Enqueue(p);
        }

    }
}
