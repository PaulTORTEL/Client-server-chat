namespace Chat
{
    partial class RegisterForm
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
            this.registerLabel = new System.Windows.Forms.Label();
            this.usernameLabel = new System.Windows.Forms.Label();
            this.loginText = new System.Windows.Forms.TextBox();
            this.passwordLabel = new System.Windows.Forms.Label();
            this.passwordText = new System.Windows.Forms.TextBox();
            this.passwordConfirmLabel = new System.Windows.Forms.Label();
            this.confirmationText = new System.Windows.Forms.TextBox();
            this.registerButton = new System.Windows.Forms.Button();
            this.loadingLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // registerLabel
            // 
            this.registerLabel.BackColor = System.Drawing.Color.Transparent;
            this.registerLabel.Font = new System.Drawing.Font("Whitney", 22F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.registerLabel.ForeColor = System.Drawing.Color.White;
            this.registerLabel.Location = new System.Drawing.Point(-3, 63);
            this.registerLabel.Name = "registerLabel";
            this.registerLabel.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.registerLabel.Size = new System.Drawing.Size(529, 42);
            this.registerLabel.TabIndex = 2;
            this.registerLabel.Text = "Register";
            this.registerLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // usernameLabel
            // 
            this.usernameLabel.BackColor = System.Drawing.Color.Transparent;
            this.usernameLabel.Font = new System.Drawing.Font("Whitney", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.usernameLabel.ForeColor = System.Drawing.Color.White;
            this.usernameLabel.Location = new System.Drawing.Point(73, 159);
            this.usernameLabel.Name = "usernameLabel";
            this.usernameLabel.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.usernameLabel.Size = new System.Drawing.Size(381, 29);
            this.usernameLabel.TabIndex = 5;
            this.usernameLabel.Text = "Username";
            this.usernameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // loginText
            // 
            this.loginText.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(75)))), ((int)(((byte)(81)))));
            this.loginText.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.loginText.Font = new System.Drawing.Font("Whitney", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.loginText.ForeColor = System.Drawing.SystemColors.Info;
            this.loginText.Location = new System.Drawing.Point(154, 195);
            this.loginText.Name = "loginText";
            this.loginText.Size = new System.Drawing.Size(219, 27);
            this.loginText.TabIndex = 6;
            this.loginText.KeyDown += new System.Windows.Forms.KeyEventHandler(this.loginText_KeyDown);
            // 
            // passwordLabel
            // 
            this.passwordLabel.BackColor = System.Drawing.Color.Transparent;
            this.passwordLabel.Font = new System.Drawing.Font("Whitney", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.passwordLabel.ForeColor = System.Drawing.Color.White;
            this.passwordLabel.Location = new System.Drawing.Point(73, 234);
            this.passwordLabel.Name = "passwordLabel";
            this.passwordLabel.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.passwordLabel.Size = new System.Drawing.Size(381, 25);
            this.passwordLabel.TabIndex = 7;
            this.passwordLabel.Text = "Password";
            this.passwordLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // passwordText
            // 
            this.passwordText.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(75)))), ((int)(((byte)(81)))));
            this.passwordText.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.passwordText.Font = new System.Drawing.Font("Whitney", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.passwordText.ForeColor = System.Drawing.SystemColors.Info;
            this.passwordText.Location = new System.Drawing.Point(154, 269);
            this.passwordText.Name = "passwordText";
            this.passwordText.PasswordChar = '*';
            this.passwordText.Size = new System.Drawing.Size(219, 27);
            this.passwordText.TabIndex = 8;
            this.passwordText.KeyDown += new System.Windows.Forms.KeyEventHandler(this.passwordText_KeyDown);
            // 
            // passwordConfirmLabel
            // 
            this.passwordConfirmLabel.BackColor = System.Drawing.Color.Transparent;
            this.passwordConfirmLabel.Font = new System.Drawing.Font("Whitney", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.passwordConfirmLabel.ForeColor = System.Drawing.Color.White;
            this.passwordConfirmLabel.Location = new System.Drawing.Point(73, 311);
            this.passwordConfirmLabel.Name = "passwordConfirmLabel";
            this.passwordConfirmLabel.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.passwordConfirmLabel.Size = new System.Drawing.Size(381, 25);
            this.passwordConfirmLabel.TabIndex = 9;
            this.passwordConfirmLabel.Text = "Confirmation";
            this.passwordConfirmLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // confirmationText
            // 
            this.confirmationText.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(75)))), ((int)(((byte)(81)))));
            this.confirmationText.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.confirmationText.Font = new System.Drawing.Font("Whitney", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.confirmationText.ForeColor = System.Drawing.SystemColors.Info;
            this.confirmationText.Location = new System.Drawing.Point(154, 348);
            this.confirmationText.Name = "confirmationText";
            this.confirmationText.PasswordChar = '*';
            this.confirmationText.Size = new System.Drawing.Size(219, 27);
            this.confirmationText.TabIndex = 10;
            this.confirmationText.KeyDown += new System.Windows.Forms.KeyEventHandler(this.confirmationText_KeyDown);
            // 
            // registerButton
            // 
            this.registerButton.Font = new System.Drawing.Font("Whitney", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.registerButton.Location = new System.Drawing.Point(206, 435);
            this.registerButton.Name = "registerButton";
            this.registerButton.Size = new System.Drawing.Size(112, 37);
            this.registerButton.TabIndex = 11;
            this.registerButton.Text = "Submit";
            this.registerButton.UseVisualStyleBackColor = true;
            this.registerButton.Click += new System.EventHandler(this.loginButton_Click);
            // 
            // loadingLabel
            // 
            this.loadingLabel.BackColor = System.Drawing.Color.Transparent;
            this.loadingLabel.Font = new System.Drawing.Font("Whitney", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.loadingLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.loadingLabel.Location = new System.Drawing.Point(73, 390);
            this.loadingLabel.Name = "loadingLabel";
            this.loadingLabel.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.loadingLabel.Size = new System.Drawing.Size(381, 29);
            this.loadingLabel.TabIndex = 12;
            this.loadingLabel.Text = "Loading...";
            this.loadingLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.loadingLabel.Visible = false;
            // 
            // RegisterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(57)))), ((int)(((byte)(62)))));
            this.ClientSize = new System.Drawing.Size(526, 494);
            this.Controls.Add(this.loadingLabel);
            this.Controls.Add(this.registerButton);
            this.Controls.Add(this.confirmationText);
            this.Controls.Add(this.passwordConfirmLabel);
            this.Controls.Add(this.passwordText);
            this.Controls.Add(this.passwordLabel);
            this.Controls.Add(this.loginText);
            this.Controls.Add(this.usernameLabel);
            this.Controls.Add(this.registerLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "RegisterForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Chat - Register";
            this.Load += new System.EventHandler(this.RegisterForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.RegisterForm_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label registerLabel;
        private System.Windows.Forms.Label usernameLabel;
        private System.Windows.Forms.TextBox loginText;
        private System.Windows.Forms.Label passwordLabel;
        private System.Windows.Forms.TextBox passwordText;
        private System.Windows.Forms.Label passwordConfirmLabel;
        private System.Windows.Forms.TextBox confirmationText;
        private System.Windows.Forms.Button registerButton;
        private System.Windows.Forms.Label loadingLabel;
    }
}