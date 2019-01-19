using System;
using System.Collections.Generic;
using System.Text;

namespace ChatPackets
{
    [Serializable]
    class LoginTokenPacket : Packet
    {
        public String token;
    }
}
