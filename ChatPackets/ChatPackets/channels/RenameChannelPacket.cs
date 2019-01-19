using System;
using System.Collections.Generic;
using System.Text;

namespace ChatPackets
{
    [Serializable]
    class RenameChannelPacket : Packet
    {
        public String token;
        public String formerName;
        public String newName;
    }
}
