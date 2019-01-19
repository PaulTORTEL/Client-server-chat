using ChatPackets;
using ServerChat.Clients;
using ServerChat.Listeners;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerChat
{
    class Program
    {
        static void Main(string[] args)
        {
            Server.Instance.Start();
        }
    }
}
