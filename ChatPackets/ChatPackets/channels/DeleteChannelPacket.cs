using System;
using System.Collections.Generic;
using System.Text;

namespace ChatPackets
{
    [Serializable]
    class DeleteChannelPacket : Packet
    {
        public String token;
        public String chanName;
    }
}
