using System;
using System.Collections.Generic;
using System.Text;

namespace ChatPackets
{
    [Serializable]
    class LoginTokenResponsePacket : Packet
    {
        public String token;
        public Boolean success;
        public String message;
        public int rank;
        public string username;
    }
}
