using ChatPackets;
using ChatPackets.login;
using System;
using System.Collections.Concurrent;

namespace Chat.Listeners
{
    class ListenerManager
    {
        public ConcurrentDictionary<Type, PacketListener> Listeners { get; set; }
        public static ListenerManager Instance { get { return Singleton.instance; } }

        private AuthentificationListener authListener;
        private ChannelListener channelListener;

        private ListenerManager()
        {
            Listeners = new ConcurrentDictionary<Type, PacketListener>();

            if (!setupListeners())
            {
                Console.WriteLine("[ERROR] Can't setup listeners");
            }

        }
        public AuthentificationListener getAuthListener()
        {
            return authListener;
        }

        private Boolean setupListeners()
        {
            authListener = new AuthentificationListener();
            channelListener = new ChannelListener();

            if (!Listeners.TryAdd(typeof(LoginResponsePacket), authListener))
                return false;
            if (!Listeners.TryAdd(typeof(WelcomePacket), authListener))
                return false;
            if (!Listeners.TryAdd(typeof(RegisterResponsePacket), authListener))
                return false;
            if (!Listeners.TryAdd(typeof(LoginTokenResponsePacket), authListener))
                return false;
            if (!Listeners.TryAdd(typeof(GetChannelResponsePacket), channelListener))
                return false;
            if (!Listeners.TryAdd(typeof(MessagePacket), channelListener))
                return false;
            if (!Listeners.TryAdd(typeof(PresencePacket), channelListener))
                return false;
            if (!Listeners.TryAdd(typeof(RenameChannelResponsePacket), channelListener))
                return false;
            if (!Listeners.TryAdd(typeof(CreateChannelResponsePacket), channelListener))
                return false;
            if (!Listeners.TryAdd(typeof(DeleteChannelResponsePacket), channelListener))
                return false;

            return true;
        }


        public void onPacketReceived(Packet packet)
        {
            if (Listeners.ContainsKey(packet.GetType()))
            {
                PacketListener listener = Listeners[packet.GetType()];
                listener.handle(packet);
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
