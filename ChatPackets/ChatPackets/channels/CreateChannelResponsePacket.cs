using System;
using System.Collections.Generic;
using System.Text;

namespace ChatPackets
{
    [Serializable]
    class CreateChannelResponsePacket : Packet
    {
        public String message;
        public Boolean success;
        public String chanName;
    }
}
