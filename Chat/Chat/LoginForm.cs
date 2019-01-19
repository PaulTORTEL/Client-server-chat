using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Chat.Listeners;

namespace Chat
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
            UIManager.Instance.loginForm = this;
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            this.MaximizeBox = false;
        }

        private void registerLabel_MouseHover(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.Hand;
        }

        private void loginButton_Click(object sender, EventArgs e)
        {
            Login();
        }

        private void Login()
        {
            if (this.loginText.Text.Length < 4)
            {
                MessageBox.Show("Username too short! (min 4)", "Error!");
            }
            else if (this.passwordText.Text.Length < 4)
            {
                MessageBox.Show("Password too short! (min 4)", "Error!");
            }
            else
            {
                this.loadingLabel.Visible = true;
                this.loginText.Enabled = false;
                this.passwordText.Enabled = false;
                this.loginButton.Enabled = false;
                Client.Instance.Login(loginText.Text, passwordText.Text);
            }
        }
        public void ErrorLogin(string message)
        {
            this.loadingLabel.Visible = false;
            this.loginText.Enabled = true;
            this.loginText.Text = "";
            this.passwordText.Enabled = true;
            this.passwordText.Text = "";
            this.loginButton.Enabled = true;
            MessageBox.Show(message, "Error!");
        }

        private void registerLabel_Click(object sender, EventArgs e)
        {
            this.Hide();
            RegisterForm form = new RegisterForm();

            form.ShowDialog();

            this.Close();
        }

        private void InputEnter(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                Login();
        }
        private void LoginForm_KeyDown(object sender, KeyEventArgs e)
        {
            InputEnter(e);
        }

        private void loginText_KeyDown(object sender, KeyEventArgs e)
        {
            InputEnter(e);
        }

        private void passwordText_KeyDown(object sender, KeyEventArgs e)
        {
            InputEnter(e);
        }
    }
}
