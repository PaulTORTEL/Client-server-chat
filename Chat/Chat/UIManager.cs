using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chat
{
    class UIManager
    {

        private Thread uiThread;

        private BlockingConcurrentQueue<Action> uiCalls;

        private bool running;

        public LoginForm loginForm { get; set; }
        public RegisterForm registerForm { get; set; }
        public ChatForm chatForm { get; set; }

        private UIManager()
        {
            uiCalls = new BlockingConcurrentQueue<Action>();
            uiThread = new Thread(new ThreadStart(run));
            running = true;
            uiThread.Start();
        }

        public ChatForm createChatForm(int rank, string username)
        {
            chatForm = new ChatForm(rank,username);
            setupViews();
            return UIManager.Instance.chatForm;
        }

        public void setupViews()
        {
            if (chatForm != null)
            {
                ChatManager.Instance.askForChannels();
            }  
        }

        public void handleMessage(ChannelMessage message)
        {
            chatForm.newMessage(message);
        }

        public void displayChannel(ChannelItem channelItem)
        {
            foreach (ChannelMessage m in channelItem.Channel.getMessages().Reverse())
            {
                handleMessage(m);
            }
        }

        public void RunOnUIThread(Action a)
        {
            uiCalls.Enqueue(a);
        }

        private void run()
        {
            while (running)
            {
                Action action = uiCalls.TryDequeue();

                if (action != null)
                {
                    Console.WriteLine("[UIManager] Action invoked!");
                    action.Invoke();
                    Console.WriteLine("[UIManager] Action finished!");
                }
            }
        }



        public static UIManager Instance { get { return Singleton.instance; } }

        private class Singleton
        {

            static Singleton()
            {
            }

            internal static readonly UIManager instance = new UIManager();
        }
    }
}
