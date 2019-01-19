using Chat.Channels;
using ChatPackets;
using Microsoft.VisualBasic;
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
    public partial class ChatForm : Form
    {
        private bool shift;

        public ChatForm(int rank, string username)
        {
            InitializeComponent();
            this.nameLabel.Text = username;
            hideButtons(rank);
            shift = false;
        }

        public void addChannelItem(ChannelItem channelItem)
        {
            this.Channels.Items.Add(channelItem);
        }

        public void setDefaultIndex()
        {
            this.Channels.SelectedIndex = 0;
        }

        delegate void invokeUserCallback(UserItem user);

        public void addUserItem(UserItem user)
        {
        
            if (this.ConnectedMembers.InvokeRequired)
            {
                invokeUserCallback callback = new invokeUserCallback(addUserItem);
                this.Invoke(callback, new object[] { user });
            }
            else
            {
                this.ConnectedMembers.Items.Add(user);
            }
        }


        public void removeUserItem(UserItem user)
        {
            this.ConnectedMembers.Items.Remove(user);
		}
		
        delegate void invokeErrorCallback(String message);

        public void showErrorMessage(String message)
        {

            if (this.InvokeRequired)
            {
                invokeErrorCallback callback = new invokeErrorCallback(showErrorMessage);
                this.Invoke(callback, new object[] { message });
            }
            else
            {
                MessageBox.Show(message, "Error!");
            }
        }

        public void ModifyChannelName(ChannelItem channelItem, String newName)
        {
            if (Channels.Items.Contains(channelItem))
            {
                channelItem.Name = newName;

                //Refresh the listbox
                Channels.DisplayMember = "";
                Channels.DisplayMember = "ChannelItem";
            }
        }

        public void DeleteChannel(ChannelItem channelItem)
        {
            if ((channelItem != null) && Channels.Items.Contains(channelItem))
            {
                Channels.Items.Remove(channelItem);

                if (Channels.SelectedItem == null)
                    Channels.SelectedIndex = 0;                

                //Refresh the listbox
                Channels.DisplayMember = "";
                Channels.DisplayMember = "ChannelItem";
            }
        }

        public void StarOnChannel(ChannelItem channelItem, Boolean needStar)
        {
            if (Channels.Items.Contains(channelItem))
            {
                channelItem.NewMessage = needStar;
                
                //Refresh the listbox
                Channels.DisplayMember = "";
                Channels.DisplayMember = "ChannelItem";
            }
        }

        private void hideButtons(int rank)
        {
            if (rank == 0)
            {
                this.CreateChannelButton.Visible = false;
                this.RenameChannelButton.Visible = false;
                this.DeleteChannelButton.Visible = false;
            }
        }

        public void newMessage(ChannelMessage message)
        {
            this.MessagesBox.AppendText(message.Sender + " (" + message.Date + ") - " + message.Message + "\r\n");
        }


        public String getSelectedChannel()
        {
            ChannelItem chan = (ChannelItem)this.Channels.SelectedItem;
            return chan.Name;
        }

        private void ChatForm_Load(object sender, EventArgs e)
        {
            this.MaximizeBox = false;
        }

        private void SendButton_Click(object sender, EventArgs e)
        {
            sendMessage();
        }
        private void disconnectButton_Click(object sender, EventArgs e)
        {
            Client.Instance.Disconnect();
        }

        private void Channels_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.MessagesBox.Text = "";
            ChannelItem chan = (ChannelItem)Channels.SelectedItem;
            if (chan != null)
                ChatManager.Instance.showSelectedChannel(chan.Name);
        }

        private void RenameChannelButton_Click(object sender, EventArgs e)
        {
            ChannelItem chan = (ChannelItem)Channels.SelectedItem;
            string input = Interaction.InputBox("Rename the channel", "Renaming channel", chan.Name, -1, -1);
            ChatManager.Instance.askRenameChannel(chan.Name, input);
        }


        private void sendMessage()
        {
            ChatManager.Instance.sendNewMessage(newMessageBox.Text, Channels.SelectedItem.ToString());
            newMessageBox.Clear();
            newMessageBox.Select(); 
            newMessageBox.Select(0, 0);

        }

        private void newMessageBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Shift || e.KeyCode == Keys.ShiftKey)
            {
                shift = true;
            }

            if (e.KeyCode == Keys.Enter && !shift)
            {
                sendMessage();
                newMessageBox.Clear();
                newMessageBox.Select();
                newMessageBox.Select(0, 0);
            }
               
        }

        private void newMessageBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Shift || e.KeyCode == Keys.ShiftKey)
            {
                shift = false;
            }

            if (e.KeyCode == Keys.Enter && !shift)
            {
                newMessageBox.Clear();
                newMessageBox.Select();
                newMessageBox.Select(0, 0);
            }

        }
        private void CreateChannelButton_Click(object sender, EventArgs e)
        {
            string input = Interaction.InputBox("Create a channel", "Create a new channel", "", -1, -1);
            ChatManager.Instance.askCreateChannel(input);
        }

        private void DeleteChannelButton_Click(object sender, EventArgs e)
        {
            ChannelItem chan = (ChannelItem)Channels.SelectedItem;
            ChatManager.Instance.askDeleteChannel(chan.Name);
        }

        private void ChatForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Client.Instance.Disconnect(false);
        }
    }
}
