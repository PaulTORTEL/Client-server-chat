using System;
using System.Collections.Generic;
using System.Text;

namespace ChatPackets
{
    [Serializable]
    class PresencePacket : Packet
    {
        public string name;
        public bool connected;
    }
}
