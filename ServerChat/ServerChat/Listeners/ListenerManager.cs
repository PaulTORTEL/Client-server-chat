using System;
using System.Collections.Concurrent;
using ChatPackets;
using ServerChat.Clients;

namespace ServerChat.Listeners
{
    class ListenerManager
    {
        public ConcurrentDictionary<Type, PacketListener> Listeners { get; set; }
        public static ListenerManager Instance { get { return Singleton.instance; } }

        private ListenerManager()
        {
            Listeners = new ConcurrentDictionary<Type, PacketListener>();

            if (!setupListeners())
            {
                Console.WriteLine("[ERROR] Can't setup listeners");
            }
            
        }

        private Boolean setupListeners()
        {

            AuthentificationListener auth = new AuthentificationListener();
            ChannelListener channelsListener = new ChannelListener();

            if (!Listeners.TryAdd(typeof(LoginPacket), auth))
                return false;
            if (!Listeners.TryAdd(typeof(WelcomePacket), auth))
                return false;
            if (!Listeners.TryAdd(typeof(RegisterPacket), auth))
                return false;
            if (!Listeners.TryAdd(typeof(LoginTokenPacket), auth))
                return false;
            if (!Listeners.TryAdd(typeof(GetChannelPacket), channelsListener))
                return false;
            if (!Listeners.TryAdd(typeof(NewMessagePacket), channelsListener))
                return false;
            if (!Listeners.TryAdd(typeof(RenameChannelPacket), channelsListener))
                return false;
            if (!Listeners.TryAdd(typeof(CreateChannelPacket), channelsListener))
                return false;
            if (!Listeners.TryAdd(typeof(DeleteChannelPacket), channelsListener))
                return false;

            return true;
        }

        
        public void onPacketReceived(Packet packet, Client client)
        {
            if (Listeners.ContainsKey(packet.GetType()))
            {
                PacketListener listener = Listeners[packet.GetType()];
                listener.handle(packet, client);
            }

            else
            {
                Console.WriteLine("[ERROR] Unknown packet");
            }
        }




        private class Singleton
        {

            static Singleton()
            {
            }

            internal static readonly ListenerManager instance = new ListenerManager();
        }

    }
}
