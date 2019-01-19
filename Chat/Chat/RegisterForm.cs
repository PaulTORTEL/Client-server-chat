using Chat.Listeners;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chat
{
    public partial class RegisterForm : Form
    {
        public RegisterForm()
        {
            InitializeComponent();
            UIManager.Instance.registerForm = this;
        }

        private void loginButton_Click(object sender, EventArgs e)
        {
            Register();
        }

        private void RegisterForm_Load(object sender, EventArgs e)
        {
            this.MaximizeBox = false;
        }

        private void Register()
        {
            if (this.loginText.Text.Length < 4)
            {
                MessageBox.Show("Username too short! (min 4)", "Error!");
            }
            else if (this.passwordText.Text.Length < 4)
            {
                MessageBox.Show("Password too short! (min 4)", "Error!");
            }
            else if (!(this.passwordText.Text.Equals(this.confirmationText.Text)))
            {
                MessageBox.Show("Passwords must match!", "Error!");
            }
            else
            {
                this.loadingLabel.Visible = true;
                this.loginText.Enabled = false;
                this.passwordText.Enabled = false;
                this.confirmationText.Enabled = false;
                this.registerButton.Enabled = false;
                Client.Instance.Register(loginText.Text, passwordText.Text);
            }
        }

        public void ErrorRegister(string message)
        {
            this.loadingLabel.Visible = false;
            this.loginText.Enabled = true;
            this.loginText.Text = "";
            this.passwordText.Enabled = true;
            this.passwordText.Text = "";
            this.confirmationText.Enabled = true;
            this.confirmationText.Text = "";
            this.registerButton.Enabled = true;
            MessageBox.Show(message, "Error!");
        }

        private void InputEnter(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Register();
            }
        }

        private void RegisterForm_KeyDown(object sender, KeyEventArgs e)
        {
            InputEnter(e);
        }

        private void confirmationText_KeyDown(object sender, KeyEventArgs e)
        {
            InputEnter(e);
        }

        private void passwordText_KeyDown(object sender, KeyEventArgs e)
        {
            InputEnter(e);
        }

        private void loginText_KeyDown(object sender, KeyEventArgs e)
        {
            InputEnter(e);
        }
    }
}
