using ServerChat.Users;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ChatPackets;
using ServerChat.Exceptions;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace ServerChat.Channels
{
    class ChannelManager
    {
        private BlockingConcurrentQueue<Packet> _packetsToSend;

        private ConcurrentBag<String> _channels;

        public static ChannelManager Instance { get { return Singleton.instance; } }
        private Thread _sendingThread;
        private Boolean _sending;

        private ChannelManager()
        {
            _packetsToSend = new BlockingConcurrentQueue<Packet>();

            _channels = new ConcurrentBag<String>();

            _channels.Add("General");
            _sendingThread = new Thread(new ThreadStart(run));
            _sending = true;
            _sendingThread.Start();
        }

        public ConcurrentBag<String> getChannels()
        {
            return _channels;
        }

        public Boolean channelExists(String name)
        {
            foreach(String chan in _channels)
            {
                if (name.Equals(chan)) return true;
            }
            return false;
        }
        public void toSend(Packet m)
        {
            _packetsToSend.Enqueue(m);
        }

        public void loadChannels()
        {
            try
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream fs = new FileStream("channels.save", FileMode.Open);
                ConcurrentBag<String> channels = (ConcurrentBag<String>)bf.Deserialize(fs);
                _channels = channels;
                fs.Close();

                Console.WriteLine("[ChannelManager] Channels saved successfully loaded!");
            }
            catch (Exception e)
            {
                Console.WriteLine("[ERROR] Unable to load channels. File may be missing.");
            }
        }

        private void saveChannels()
        {
            FileStream fs = new FileStream("channels.save", FileMode.Create);
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(fs, _channels);
            fs.Close();
        }

        private void run()
        {
            while (_sending)
            {

                Packet m = _packetsToSend.TryDequeue();

                if (m != null)
                {
                    if (m is MessagePacket)
                    {
                         if (areDataCorrect((MessagePacket)m))
                            sendMessage(m);
                    }
                    else
                    {
                        sendMessage(m);
                    }
                }
                   
            }
        }

        private Boolean areDataCorrect(MessagePacket m)
        {
            if (!channelExist(m.channelName))
                return false;                              

            return true;
        }

        private Boolean userExist(String username)
        {
            return UserManager.Instance.isUsernameExisting(username);
        }

        private Boolean channelExist(String channelName)
        {
            return _channels.Contains(channelName);
        }

        public void manageChannelRenaming(String formerName, String newName)
        {               
            if (channelExist(formerName))
            {
                if (!channelExist(newName))
                {
                    String[] temp = _channels.ToArray();

                    // Empty channels
                    while (!_channels.IsEmpty)
                    {
                        String s;
                        _channels.TryTake(out s);
                    }

                    // Fill again channels with formerName removed
                    foreach (String value in temp)
                    {
                        if (!value.Equals(formerName))
                            _channels.Add(value);
                    }

                    // Add the new name of the channel
                    _channels.Add(newName);
                    saveChannels();
                }

                else
                {
                    throw new ChannelNameAlreadyTaken("Channel name already taken!");
                } 
            }
            else
            {
                throw new UnknownChannelException("Unknown channel!");
            }
        }

        public void manageChannelCreation(String chanName)
        {
            if (!channelExist(chanName))
            {
                _channels.Add(chanName);
                saveChannels();
            }
            else
            {
                throw new ChannelNameAlreadyTaken("Channel name already taken!");
            }
        }

        public void manageChannelDeleting(String chanName)
        {
            if (channelExist(chanName))
            {
                if (_channels.Count == 1)
                    throw new TooFewChannelsException("You cannot delete the last existing channel!");

                ConcurrentBag<String> temp = new ConcurrentBag<string>();
                foreach (String name in _channels)
                {
                    if (!name.Equals(chanName))
                    {
                        temp.Add(name);
                    }
                }
                temp.Reverse();
                _channels = temp;
                saveChannels();
            }
            else
            {
                throw new UnknownChannelException("Unknown channel!");
            }
        }

        private void sendMessage(Packet m)
        {
            UserManager.Instance.broadcast(m);
        }

        private class Singleton
        {

            static Singleton()
            {
            }

            internal static readonly ChannelManager instance = new ChannelManager();
        }
    }
}
