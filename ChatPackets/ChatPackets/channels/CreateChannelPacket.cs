using System;
using System.Collections.Generic;
using System.Text;

namespace ChatPackets
{
    [Serializable]
    class CreateChannelPacket : Packet
    {
        public String token;
        public String chanName;
    }
}
