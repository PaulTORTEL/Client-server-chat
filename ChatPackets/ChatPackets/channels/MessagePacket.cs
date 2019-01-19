using System;
using System.Collections.Generic;
using System.Text;

namespace ChatPackets
{
    [Serializable]
    class MessagePacket : Packet
    {
        public String message;
        public String sender;
        public DateTime date;
        public String channelName;

        public Boolean success;
        public String errorMessage;
    }
}
