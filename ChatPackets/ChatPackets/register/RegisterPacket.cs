using System;
using System.Collections.Generic;
using System.Text;

namespace ChatPackets
{
    [Serializable]
    class RegisterPacket : Packet
    {
        public string username;
        public string password;
    }
}
