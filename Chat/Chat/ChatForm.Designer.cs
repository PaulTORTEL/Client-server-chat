namespace Chat
{
    partial class ChatForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.CreateChannelButton = new System.Windows.Forms.Button();
            this.newMessageBox = new System.Windows.Forms.TextBox();
            this.SendButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.disconnectButton = new System.Windows.Forms.Button();
            this.Channels = new System.Windows.Forms.ListBox();
            this.ConnectedMembers = new System.Windows.Forms.ListBox();
            this.DeleteChannelButton = new System.Windows.Forms.Button();
            this.RenameChannelButton = new System.Windows.Forms.Button();
            this.nameLabel = new System.Windows.Forms.Label();
            this.MessagesBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // CreateChannelButton
            // 
            this.CreateChannelButton.Location = new System.Drawing.Point(55, 670);
            this.CreateChannelButton.Margin = new System.Windows.Forms.Padding(4);
            this.CreateChannelButton.Name = "CreateChannelButton";
            this.CreateChannelButton.Size = new System.Drawing.Size(103, 47);
            this.CreateChannelButton.TabIndex = 1;
            this.CreateChannelButton.Text = "Create new channel";
            this.CreateChannelButton.UseVisualStyleBackColor = true;
            this.CreateChannelButton.Click += new System.EventHandler(this.CreateChannelButton_Click);
            // 
            // newMessageBox
            // 
            this.newMessageBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(75)))), ((int)(((byte)(81)))));
            this.newMessageBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.newMessageBox.ForeColor = System.Drawing.Color.White;
            this.newMessageBox.Location = new System.Drawing.Point(531, 654);
            this.newMessageBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.newMessageBox.Multiline = true;
            this.newMessageBox.Name = "newMessageBox";
            this.newMessageBox.Size = new System.Drawing.Size(631, 62);
            this.newMessageBox.TabIndex = 4;
            this.newMessageBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.newMessageBox_KeyDown);
            this.newMessageBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.newMessageBox_KeyUp);
            // 
            // SendButton
            // 
            this.SendButton.Location = new System.Drawing.Point(1188, 666);
            this.SendButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.SendButton.Name = "SendButton";
            this.SendButton.Size = new System.Drawing.Size(63, 46);
            this.SendButton.TabIndex = 5;
            this.SendButton.Text = "Send";
            this.SendButton.UseVisualStyleBackColor = true;
            this.SendButton.Click += new System.EventHandler(this.SendButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label1.Location = new System.Drawing.Point(1425, 71);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(115, 17);
            this.label1.TabIndex = 6;
            this.label1.Text = "Connected users";
            // 
            // disconnectButton
            // 
            this.disconnectButton.Location = new System.Drawing.Point(1569, 15);
            this.disconnectButton.Margin = new System.Windows.Forms.Padding(4);
            this.disconnectButton.Name = "disconnectButton";
            this.disconnectButton.Size = new System.Drawing.Size(100, 28);
            this.disconnectButton.TabIndex = 7;
            this.disconnectButton.Text = "Disconnect";
            this.disconnectButton.UseVisualStyleBackColor = true;
            this.disconnectButton.Click += new System.EventHandler(this.disconnectButton_Click);
            // 
            // Channels
            // 
            this.Channels.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(57)))), ((int)(((byte)(62)))));
            this.Channels.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Channels.ForeColor = System.Drawing.Color.White;
            this.Channels.FormattingEnabled = true;
            this.Channels.ItemHeight = 29;
            this.Channels.Location = new System.Drawing.Point(55, 81);
            this.Channels.Margin = new System.Windows.Forms.Padding(4);
            this.Channels.Name = "Channels";
            this.Channels.Size = new System.Drawing.Size(372, 468);
            this.Channels.TabIndex = 8;
            this.Channels.SelectedIndexChanged += new System.EventHandler(this.Channels_SelectedIndexChanged);
            // 
            // ConnectedMembers
            // 
            this.ConnectedMembers.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(57)))), ((int)(((byte)(62)))));
            this.ConnectedMembers.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.25F);
            this.ConnectedMembers.ForeColor = System.Drawing.Color.YellowGreen;
            this.ConnectedMembers.FormattingEnabled = true;
            this.ConnectedMembers.ItemHeight = 29;
            this.ConnectedMembers.Location = new System.Drawing.Point(1344, 91);
            this.ConnectedMembers.Margin = new System.Windows.Forms.Padding(4);
            this.ConnectedMembers.Name = "ConnectedMembers";
            this.ConnectedMembers.Size = new System.Drawing.Size(289, 439);
            this.ConnectedMembers.TabIndex = 10;
            // 
            // DeleteChannelButton
            // 
            this.DeleteChannelButton.Location = new System.Drawing.Point(325, 670);
            this.DeleteChannelButton.Margin = new System.Windows.Forms.Padding(4);
            this.DeleteChannelButton.Name = "DeleteChannelButton";
            this.DeleteChannelButton.Size = new System.Drawing.Size(103, 47);
            this.DeleteChannelButton.TabIndex = 11;
            this.DeleteChannelButton.Text = "Delete channel";
            this.DeleteChannelButton.UseVisualStyleBackColor = true;
            this.DeleteChannelButton.Click += new System.EventHandler(this.DeleteChannelButton_Click);
            // 
            // RenameChannelButton
            // 
            this.RenameChannelButton.Location = new System.Drawing.Point(189, 670);
            this.RenameChannelButton.Margin = new System.Windows.Forms.Padding(4);
            this.RenameChannelButton.Name = "RenameChannelButton";
            this.RenameChannelButton.Size = new System.Drawing.Size(103, 47);
            this.RenameChannelButton.TabIndex = 12;
            this.RenameChannelButton.Text = "Rename channel";
            this.RenameChannelButton.UseVisualStyleBackColor = true;
            this.RenameChannelButton.Click += new System.EventHandler(this.RenameChannelButton_Click);
            // 
            // nameLabel
            // 
            this.nameLabel.AutoSize = true;
            this.nameLabel.BackColor = System.Drawing.Color.Transparent;
            this.nameLabel.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.nameLabel.Location = new System.Drawing.Point(1452, 21);
            this.nameLabel.Name = "nameLabel";
            this.nameLabel.Size = new System.Drawing.Size(46, 17);
            this.nameLabel.TabIndex = 13;
            this.nameLabel.Text = "label2";
            // 
            // MessagesBox
            // 
            this.MessagesBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(57)))), ((int)(((byte)(62)))));
            this.MessagesBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.MessagesBox.ForeColor = System.Drawing.Color.White;
            this.MessagesBox.Location = new System.Drawing.Point(531, 81);
            this.MessagesBox.Margin = new System.Windows.Forms.Padding(4);
            this.MessagesBox.Multiline = true;
            this.MessagesBox.Name = "MessagesBox";
            this.MessagesBox.ReadOnly = true;
            this.MessagesBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.MessagesBox.Size = new System.Drawing.Size(719, 477);
            this.MessagesBox.TabIndex = 14;
            // 
            // ChatForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(57)))), ((int)(((byte)(62)))));
            this.ClientSize = new System.Drawing.Size(1685, 838);
            this.Controls.Add(this.MessagesBox);
            this.Controls.Add(this.nameLabel);
            this.Controls.Add(this.RenameChannelButton);
            this.Controls.Add(this.DeleteChannelButton);
            this.Controls.Add(this.ConnectedMembers);
            this.Controls.Add(this.Channels);
            this.Controls.Add(this.disconnectButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.SendButton);
            this.Controls.Add(this.newMessageBox);
            this.Controls.Add(this.CreateChannelButton);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "ChatForm";
            this.Text = "ChatForm";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ChatForm_FormClosed);
            this.Load += new System.EventHandler(this.ChatForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button CreateChannelButton;
        private System.Windows.Forms.TextBox newMessageBox;
        private System.Windows.Forms.Button SendButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button disconnectButton;
        private System.Windows.Forms.ListBox Channels;
        private System.Windows.Forms.ListBox ConnectedMembers;
        private System.Windows.Forms.Button DeleteChannelButton;
        private System.Windows.Forms.Button RenameChannelButton;
        private System.Windows.Forms.Label nameLabel;
        private System.Windows.Forms.TextBox MessagesBox;
    }
}