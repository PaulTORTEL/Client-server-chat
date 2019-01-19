using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat
{
    public class Channel
    {
        ConcurrentBag<ChannelMessage> messages;

        public Channel()
        {
            messages = new ConcurrentBag<ChannelMessage>();
        }

        public ConcurrentBag<ChannelMessage> getMessages()
        {
            return messages;
        }

        public void addMessage(ChannelMessage message)
        {
            messages.Add(message);
        }

        public void clearMessages()
        {
            ChannelMessage message;
            while (!messages.IsEmpty)
            {
                messages.TryTake(out message);
            }
        }
    }
}
