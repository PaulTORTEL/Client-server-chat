using System;
using System.Collections.Generic;
using System.Text;

namespace ChatPackets
{
    [Serializable]
    class NewMessagePacket : Packet
    {
        public String newMessage;
        public String token;
        public String channel;
    }
}
