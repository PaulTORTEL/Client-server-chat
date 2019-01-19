using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Channels
{
    public class UserItem
    {
        public String Name;
    
        public UserItem(String name)
        {
            Name = name;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
