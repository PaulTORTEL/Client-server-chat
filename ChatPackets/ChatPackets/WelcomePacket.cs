﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ChatPackets
{
    [Serializable]
    class WelcomePacket : Packet
    {
        public bool clientConnected;
    }
}
