using ChatPackets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chat.Listeners
{
    class ChannelListener : PacketListener
    {
        public void handle(Packet packet)
        {
            if (packet is GetChannelResponsePacket)
            {
                getChannels((GetChannelResponsePacket)packet);
            }
            else if (packet is MessagePacket)
            {
                handleMessage((MessagePacket)packet);
            }
            else if (packet is PresencePacket)
            {
                updatePresence((PresencePacket)packet);
            }
            else if (packet is RenameChannelResponsePacket)
            {
                handleChannelRenaming((RenameChannelResponsePacket)packet);
            }
            else if (packet is CreateChannelResponsePacket)
            {
                handleChannelCreation((CreateChannelResponsePacket)packet);
            }
            else if (packet is DeleteChannelResponsePacket)
            {
                handleChannelDeleting((DeleteChannelResponsePacket)packet);
            }
        }

        private void handleChannelDeleting(DeleteChannelResponsePacket packet)
        {
            if (packet.success)
            {
                ChatManager.Instance.deleteChannel(packet.chanName);
            }
            else
            {
                UIManager.Instance.chatForm.showErrorMessage(packet.message);
            }
        }

        private void handleChannelCreation(CreateChannelResponsePacket packet)
        {
            if (packet.success)
            {
                ChatManager.Instance.createChannel(packet.chanName);
            }
            else
            {
                Console.WriteLine(packet.message);
            }
        }

        private void handleChannelRenaming(RenameChannelResponsePacket packet)
        {
            if (packet.success)
            {
                ChatManager.Instance.renameChannel(packet.formerName, packet.newName);
            }
            else
            {
                Console.WriteLine(packet.message);
            }
        }

        private void handleMessage(MessagePacket packet)
        {
            Console.WriteLine("[Client] New message packet");
            if (packet.success)
            {
                ChatManager.Instance.manageNewMessage(packet);
            }
            else
            {
                Console.WriteLine(packet.errorMessage);
            }    
        }

        private void getChannels(GetChannelResponsePacket p)
        {
            if (p.success)
            {
                ChatManager.Instance.setupChannels(p.channels);
            }
            else
            {
                Console.WriteLine(p.message);
            }
        }

        private void updatePresence(PresencePacket p)
        {
            if (p.connected)
                ChatManager.Instance.UserConnected(p.name);
            else
            {
                ChatManager.Instance.UserDisconnected(p.name);
            }            
        }
    }
}
