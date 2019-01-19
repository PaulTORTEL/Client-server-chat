using Chat.Channels;
using ChatPackets;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chat
{
    class ChatManager
    {
        public String Token { get; private set; }
        public int Rank { get; private set; }


        public ConcurrentDictionary<String, ChannelItem> Channels { get; private set; }
        public ConcurrentDictionary<String, UserItem> Users { get; private set; }

        private ChatManager()
        {
            
        }


        public void Launch(String token, int rank, string username, Boolean firstLaunch = false)
        {
            Token = token;
            Rank = rank;
            Users = new ConcurrentDictionary<string, UserItem>();
            
            UIManager.Instance.RunOnUIThread(() =>
            {
                if (firstLaunch)
                {
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                }

                Application.Run(UIManager.Instance.createChatForm(Rank,username));
            });

        }

        public void manageNewMessage(MessagePacket p)
        {
            if (checkChannel(p.channelName))
            {
                ChannelMessage newMessage = new ChannelMessage();
                newMessage.Sender = p.sender;
                newMessage.Message = p.message;
                newMessage.Date = p.date;

                Channels[p.channelName].Channel.addMessage(newMessage);

                String selectedChannel = UIManager.Instance.chatForm.getSelectedChannel();

                if (p.channelName.Equals(selectedChannel))
                    UIManager.Instance.handleMessage(newMessage);

                // Put a little * on the channel to indicate that new messages came
                else
                {
                    UIManager.Instance.chatForm.StarOnChannel(Channels[p.channelName], true);
                }
            }
        }

        public void showSelectedChannel(String channelToDisp)
        {
            if (checkChannel(channelToDisp))
            {
                ChannelItem channel = Channels[channelToDisp];
                UIManager.Instance.displayChannel(channel);
                UIManager.Instance.chatForm.StarOnChannel(channel, false);
            }
        }

        public void askRenameChannel(String formerName, String newName)
        {
            if (Rank == 0)
                return;

            if (newName == null || newName.Equals(""))
                return;

            newName = newName.Trim();

            if (Channels.ContainsKey(formerName) && !Channels.ContainsKey(newName))
            {
                RenameChannelPacket p = new RenameChannelPacket();
                p.token = Token;
                p.formerName = formerName;
                p.newName = newName;

                Client.Instance.SendPacket(p);
            }
            else if (Channels.ContainsKey(newName))
            {
                MessageBox.Show("This channel name is already taken!", "Error!");
            }
        }

        public void askCreateChannel(String newChanName)
        {
            if (Rank == 0)
                return;

            if (newChanName == null || newChanName.Equals(""))
                return;

            newChanName = newChanName.Trim();

            if (!Channels.ContainsKey(newChanName))
            {
                CreateChannelPacket p = new CreateChannelPacket();
                p.chanName = newChanName;
                p.token = Token;

                Client.Instance.SendPacket(p);
            }
            else
            {
                MessageBox.Show("This channel name is already taken!", "Error!");
            }
        }

        public void askDeleteChannel(String chanName)
        {
            if (Rank == 0)
                return;

            if (chanName == null || chanName.Equals(""))
                return;

            if (Channels.ContainsKey(chanName))
            {
                DeleteChannelPacket p = new DeleteChannelPacket();
                p.chanName = chanName;
                p.token = Token;

                Client.Instance.SendPacket(p);
            }
        }

        public void deleteChannel(String chanName)
        {
            if (Channels.ContainsKey(chanName))
            {
                ChannelItem chan;
                Channels.TryRemove(chanName, out chan);
                UIManager.Instance.chatForm.DeleteChannel(chan);
            }
        }

        public void createChannel(String chanName)
        {
            if (!Channels.ContainsKey(chanName))
            {
                ChannelItem chan = new ChannelItem(chanName);
                Channels.TryAdd(chanName, chan);
            }
        }

        public void renameChannel(String formerName, String newName)
        {
            if (Channels.ContainsKey(formerName) && !Channels.ContainsKey(newName))
            {
                ChannelItem chan;
                Channels.TryRemove(formerName, out chan);

                Channels.TryAdd(newName, chan);
                UIManager.Instance.chatForm.ModifyChannelName(chan, newName);
            }  
        }

        private Boolean checkChannel(String channelName)
        {
            return Channels.ContainsKey(channelName);
        }

        public void askForChannels()
        {
            GetChannelPacket p = new GetChannelPacket();
            p.token = Token;
            Client.Instance.SendPacket(p);
        }

        public void setupChannels(String[] channels)
        {
            Channels = new ConcurrentDictionary<string, ChannelItem>();

            foreach (String chan in channels)
            {
                Channels.TryAdd(chan, new ChannelItem(chan));
            }

            UIManager.Instance.chatForm.setDefaultIndex();
        }

        public void sendNewMessage(String message, String channel)
        {
            message = message.Trim();

            if (message.Equals(""))
                return;

            NewMessagePacket packet = new NewMessagePacket();
            packet.newMessage = message;
            packet.token = Token;
            packet.channel = channel;

            Client.Instance.SendPacket(packet);
        }

        public void UserConnected(string username)
        {
            if (!this.Users.ContainsKey(username))
            {
                UserItem user = new UserItem(username);
                this.Users.TryAdd(username, user);
                UIManager.Instance.chatForm.addUserItem(user);
            }
        }

        public void UserDisconnected(string username)
        {
            if (this.Users.ContainsKey(username))
            {
                UserItem item;
                this.Users.TryRemove(username, out item);
                UIManager.Instance.chatForm.removeUserItem(item);
            }
        }



        public static ChatManager Instance { get { return Singleton.instance; } }

        private class Singleton
        {

            static Singleton()
            {
            }

            internal static readonly ChatManager instance = new ChatManager();
        }
    }
}
