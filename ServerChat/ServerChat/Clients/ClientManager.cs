using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading;

namespace ServerChat.Clients
{
    class ClientManager
    {
        private Thread thread;
        private TcpListener socket;
        private bool listening;

        public ClientManager(TcpListener socket)
        {
            this.socket = socket;
            thread = new Thread(new ThreadStart(listen));
        }

        public void Start()
        {
            listening = true;
            thread.Start();
        }

        public void Stop()
        {
            listening = false;
        }

        private void listen()
        {
            while (listening)
            {
                Console.WriteLine("[ClientManager] Waiting for clients...");
                TcpClient clientSocket = socket.AcceptTcpClient();
                Console.WriteLine("[ClientManager] New client connected!");

                Client client = new Client(clientSocket);

            }
        }
    }
}
