using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chat
{
    public class ChannelItem
    {
        public Channel Channel { get; private set; }

        public String Name { get; set; }

        // Boolean which indicates if there are non read messages
        public Boolean NewMessage { get; set; }

        public ChannelItem(String name)
        {
            Name = name;
            Channel = new Channel();
            UIManager.Instance.chatForm.addChannelItem(this);
        }

        public override string ToString()
        {
            if (NewMessage)
                return Name + " *";

            return Name;
        }

    }
}
