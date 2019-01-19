using System;
using System.Collections.Generic;
using System.Text;

namespace ChatPackets.login
{
    [Serializable]
    class LoginResponsePacket : Packet
    {
        public bool success;
        public string token;
        public string message;
        public int rank;
        public string username;
    }
}
