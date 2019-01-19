using System;
using System.Collections.Generic;
using System.Text;

namespace ChatPackets
{
    [Serializable]
    class LoginPacket : Packet
    {
        public string username;
        public string password;
        public string token;
    }
}
