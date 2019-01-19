using ChatPackets;
using ServerChat.Clients;

namespace ServerChat.Listeners
{
    interface PacketListener
    {
        void handle(Packet packet, Client client);
    }
}
