using ChatPackets;
using ServerChat.Channels;
using ServerChat.Clients;
using ServerChat.Exceptions;
using ServerChat.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerChat.Listeners
{
    class ChannelListener : PacketListener
    {
        public void handle(Packet packet, Client client)
        {
            if (packet is GetChannelPacket)
            {
                getChannels((GetChannelPacket)packet, client);
            }
            else if (packet is NewMessagePacket)
            {
                handleNewMessage((NewMessagePacket)packet, client);
            }
            else if (packet is RenameChannelPacket)
            {
                renameChannel((RenameChannelPacket)packet, client);
            }
            else if (packet is CreateChannelPacket)
            {
                createChannel((CreateChannelPacket)packet, client);
            }
            else if (packet is DeleteChannelPacket)
            {
                deleteChannelPacket((DeleteChannelPacket)packet, client);
            }
            
        }

        private void deleteChannelPacket(DeleteChannelPacket p, Client client)
        {
            DeleteChannelResponsePacket packet = new DeleteChannelResponsePacket();

            if (UserManager.Instance.isUserConnected(p.token))
            {
                if (UserManager.Instance.isUserAdmin(p.token))
                {
                    try
                    {
                        ChannelManager.Instance.manageChannelDeleting(p.chanName);
                        packet.chanName = p.chanName;
                        packet.message = "";
                        packet.success = true;

                        ChannelManager.Instance.toSend(packet);

                    }
                    catch (Exception e)
                    {
                        packet.success = false;
                        packet.message = e.Message;
                        client.SendPacket(packet);
                    }
                }
            }
        }

        private void createChannel(CreateChannelPacket p, Client client)
        {
            CreateChannelResponsePacket packet = new CreateChannelResponsePacket();

            if (UserManager.Instance.isUserConnected(p.token))
            {
                if (UserManager.Instance.isUserAdmin(p.token))
                {

                    try
                    {
                        p.chanName = p.chanName.Replace(" ", "_");

                        ChannelManager.Instance.manageChannelCreation(p.chanName);
                        packet.chanName = p.chanName;
                        packet.message = "";
                        packet.success = true;

                        ChannelManager.Instance.toSend(packet);

                    }
                    catch (Exception e)
                    {
                        packet.success = false;
                        packet.message = e.Message;
                        client.SendPacket(packet);
                    }
                }
            }
        }


        private void renameChannel(RenameChannelPacket p, Client client)
        {
            RenameChannelResponsePacket packet = new RenameChannelResponsePacket();

            if (UserManager.Instance.isUserConnected(p.token))
            {
                if (UserManager.Instance.isUserAdmin(p.token))
                {
                    try
                    {
                        p.newName = p.newName.Replace(" ", "_");

                        ChannelManager.Instance.manageChannelRenaming(p.formerName, p.newName);
                        packet.formerName = p.formerName;
                        packet.newName = p.newName;
                        packet.message = "";
                        packet.success = true;
                        ChannelManager.Instance.toSend(packet);

                    }
                    catch (Exception e)
                    {
                        packet.success = false;
                        packet.message = e.Message;
                        client.SendPacket(packet);
                    }
                }
            }
        }

        private void handleNewMessage(NewMessagePacket p, Client client)
        {
            MessagePacket packet = new MessagePacket();

            if (UserManager.Instance.isUserConnected(p.token))
            {
                try
                {
                    packet.sender = UserManager.Instance.getUsernameFromToken(p.token);
                    packet.message = p.newMessage;
                    packet.date = DateTime.Now;
                    packet.channelName = p.channel;
                    
                    packet.errorMessage = "";
                    packet.success = true;

                    ChannelManager.Instance.toSend(packet);
                }
                catch (Exception e)
                {
                    packet.success = false;
                    packet.errorMessage = e.Message;
                    client.SendPacket(packet);
                }
            }
            else
            {
                packet.success = false;
                packet.errorMessage = "You are not connected!";
                packet.message = "";

                client.SendPacket(packet);
            }
        }

        private void getChannels(GetChannelPacket p, Client client)
        {
            GetChannelResponsePacket packet = new GetChannelResponsePacket();

            if (UserManager.Instance.isUserConnected(p.token))
            {
                String username = "";
                try
                {
                     username = UserManager.Instance.getUserFromToken(p.token).Username;
                }
                catch (Exception e) {
                    Console.WriteLine(e);
                    return;
                }
                

                String[] channels = new String[ChannelManager.Instance.getChannels().Count];
                int size = channels.Length;
                int i = 0;

                foreach (String chan in ChannelManager.Instance.getChannels())
                {
                    channels[size - 1 - i] = chan;
                    i++;
                }

                packet.channels = channels;
                packet.success = true;
                packet.message = "";

                UserManager.Instance.getUserFromToken(p.token).listeningChannels = true;

                PresencePacket myPresence = new PresencePacket();
                myPresence.name = username;
                myPresence.connected = true;

                foreach (KeyValuePair<String, Client> entry in UserManager.Instance.OnlineUsers)
                {
                    try
                    {
                        User user = UserManager.Instance.getUserFromToken(entry.Key);
                        if (user.listeningChannels)
                        {
                            PresencePacket presence = new PresencePacket();
                            presence.name = user.Username;
                            presence.connected = true;
                            client.SendPacket(presence);
                        }
                        if (!user.Username.Equals(username))
                        {
                            entry.Value.SendPacket(myPresence);
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                    

                }
            }
            else
            {
                packet.success = false;
                packet.message = "You are not connected!";
            }

            client.SendPacket(packet);
        }
    }
}
