using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace ChatPackets
{
    [Serializable]
    class GetChannelResponsePacket : Packet
    {
        public String message;
        public Boolean success;
        public String[] channels;
    }
}
