using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChatPackets;
using System.Windows.Forms;
using ChatPackets.login;

namespace Chat.Listeners
{
    class AuthentificationListener : PacketListener
    {
        public void handle(Packet packet)
        {
            if (packet is WelcomePacket)
            {
                handleWelcoming((WelcomePacket)packet);
            }
            else if (packet is RegisterResponsePacket)
            {
                handleRegister((RegisterResponsePacket)packet);
            }
            else if (packet is LoginResponsePacket)
            {
                handleLoginResponse((LoginResponsePacket)packet);
            }
            else if (packet is LoginTokenResponsePacket)
            {
                handleLoginResponse((LoginTokenResponsePacket)packet);
            }
        }

        private void handleLoginResponse(LoginTokenResponsePacket packet)
        {
            if (packet.success)
            {
                ChatManager.Instance.Launch(packet.token, packet.rank, packet.username, true);
            }
            else
            {
                UIManager.Instance.RunOnUIThread(() =>
                {
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    Application.Run(new LoginForm());
                });
            }
        }

        private void handleLoginResponse (LoginResponsePacket packet)
        {
            if (packet.success)
            {
                Console.WriteLine("You are now logged in!");
                // User succeed to login
                UIManager.Instance.loginForm.Close();
                ChatManager.Instance.Launch(packet.token, packet.rank, packet.username);
                Client.Instance.registerToken(packet.token);
            }
            else
                UIManager.Instance.loginForm.ErrorLogin(packet.message);
        }

        private void handleWelcoming(WelcomePacket welcome)
        {
            String token = Client.Instance.readClientSave();
            Console.WriteLine("token : " + token);

            if (token.Equals("no token") || token.Equals("error"))
            {
                UIManager.Instance.RunOnUIThread(() =>
                {
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    Application.Run(new LoginForm());
                });
            }

            else
            {
                LoginTokenPacket p = new LoginTokenPacket();
                p.token = token;
                Client.Instance.SendPacket(p);
            }    

        }

        
        private void handleRegister(RegisterResponsePacket p)
        {
            if (p.success)
            {
                UIManager.Instance.registerForm.Close();
 
                ChatManager.Instance.Launch(p.token, p.rank, p.username);
                Client.Instance.registerToken(p.token);
            }
            else
            {
                UIManager.Instance.registerForm.ErrorRegister(p.message);
            }
        }

    }
}
