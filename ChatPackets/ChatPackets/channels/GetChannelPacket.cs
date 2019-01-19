using System;
using System.Collections.Generic;
using System.Text;

namespace ChatPackets
{
    [Serializable]
    class GetChannelPacket : Packet
    {
        public String token;
    }
}
