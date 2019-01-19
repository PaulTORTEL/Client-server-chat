using System;
using System.Collections.Generic;
using System.Text;

namespace ChatPackets
{
    [Serializable]
    class RenameChannelResponsePacket : Packet
    {
        public String message;
        public Boolean success;
        public String formerName;
        public String newName;
    }
}
