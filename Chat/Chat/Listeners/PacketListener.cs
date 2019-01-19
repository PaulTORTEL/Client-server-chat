using ChatPackets;

namespace Chat.Listeners
{
    interface PacketListener
    {
        void handle(Packet packet);
    }
}
