using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerChat.Users
{
    [Serializable]
    class User
    {
        public int Rank { get; set; }
        public String Username { get; set; }
        public String Password { get; set; }

        [NonSerialized]
        private bool ListeningChannels;

        public bool listeningChannels
        {
            get
            {
                return ListeningChannels;
            }

            set
            {
                ListeningChannels = value;
            }
        }

        public User()
        {
            
        }

        public static User createUser(String username, String password, int rank)
        {
            User user = new User();
            user.Rank = rank;
            user.Password = password;
            user.Username = username;
            user.listeningChannels = false;

            return user;
        }
    }
}
