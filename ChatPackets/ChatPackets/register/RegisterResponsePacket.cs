using System;
using System.Collections.Generic;
using System.Text;

namespace ChatPackets
{
    [Serializable]
    class RegisterResponsePacket : Packet
    {
        public bool success;
        public string token;
        public string message;
        public int rank;
        public string username;
    }
}
