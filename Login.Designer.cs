using System.Drawing;
using System.Xml.Linq;

namespace PackingApplication
{
    partial class Login
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
            this.emailid = new System.Windows.Forms.Label();
            this.email = new System.Windows.Forms.TextBox();
            this.password = new System.Windows.Forms.Label();
            this.passwrd = new System.Windows.Forms.TextBox();
            this.year = new System.Windows.Forms.Label();
            this.YearList = new System.Windows.Forms.ComboBox();
            this.rememberme = new System.Windows.Forms.CheckBox();
            this.signin = new System.Windows.Forms.Button();
            this.welcome = new System.Windows.Forms.Label();
            this.subtitle = new System.Windows.Forms.Label();
            this.req1 = new System.Windows.Forms.Label();
            this.req2 = new System.Windows.Forms.Label();
            this.req3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // emailid
            // 
            this.emailid.AutoSize = true;
            this.emailid.Font = new System.Drawing.Font("Microsoft Tai Le", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.emailid.Location = new System.Drawing.Point(488, 162);
            this.emailid.Name = "emailid";
            this.emailid.Size = new System.Drawing.Size(48, 14);
            this.emailid.TabIndex = 0;
            this.emailid.Text = "Email ID";
            // 
            // email
            // 
            this.email.Location = new System.Drawing.Point(488, 179);
            this.email.Name = "email";
            this.email.Size = new System.Drawing.Size(227, 20);
            this.email.TabIndex = 1;
            this.email.Tag = "";
            // 
            // password
            // 
            this.password.AutoSize = true;
            this.password.Font = new System.Drawing.Font("Microsoft Tai Le", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.password.Location = new System.Drawing.Point(488, 217);
            this.password.Name = "password";
            this.password.Size = new System.Drawing.Size(56, 14);
            this.password.TabIndex = 2;
            this.password.Text = "Password";
            // 
            // passwrd
            // 
            this.passwrd.Location = new System.Drawing.Point(488, 233);
            this.passwrd.Name = "passwrd";
            this.passwrd.Size = new System.Drawing.Size(227, 20);
            this.passwrd.TabIndex = 3;
            this.passwrd.UseSystemPasswordChar = true;
            this.passwrd.WordWrap = false;
            // 
            // year
            // 
            this.year.AutoSize = true;
            this.year.Font = new System.Drawing.Font("Microsoft Tai Le", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.year.Location = new System.Drawing.Point(488, 269);
            this.year.Name = "year";
            this.year.Size = new System.Drawing.Size(28, 14);
            this.year.TabIndex = 4;
            this.year.Text = "Year";
            // 
            // YearList
            // 
            this.YearList.FormattingEnabled = true;
            this.YearList.Location = new System.Drawing.Point(488, 285);
            this.YearList.Name = "YearList";
            this.YearList.Size = new System.Drawing.Size(227, 21);
            this.YearList.TabIndex = 5;
            // 
            // rememberme
            // 
            this.rememberme.AutoSize = true;
            this.rememberme.Checked = true;
            this.rememberme.CheckState = System.Windows.Forms.CheckState.Checked;
            this.rememberme.Font = new System.Drawing.Font("Microsoft Tai Le", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rememberme.Location = new System.Drawing.Point(488, 324);
            this.rememberme.Name = "rememberme";
            this.rememberme.Size = new System.Drawing.Size(98, 18);
            this.rememberme.TabIndex = 7;
            this.rememberme.Text = "Remember me";
            this.rememberme.UseVisualStyleBackColor = true;
            // 
            // signin
            // 
            this.signin.BackColor = System.Drawing.SystemColors.Highlight;
            this.signin.Cursor = System.Windows.Forms.Cursors.Hand;
            this.signin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.signin.Font = new System.Drawing.Font("Microsoft Tai Le", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.signin.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.signin.Location = new System.Drawing.Point(488, 356);
            this.signin.Name = "signin";
            this.signin.Size = new System.Drawing.Size(227, 32);
            this.signin.TabIndex = 8;
            this.signin.Text = "SIGN IN";
            this.signin.UseVisualStyleBackColor = false;
            this.signin.Click += new System.EventHandler(this.signin_Click);
            // 
            // welcome
            // 
            this.welcome.AutoSize = true;
            this.welcome.Font = new System.Drawing.Font("Microsoft Tai Le", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.welcome.Location = new System.Drawing.Point(554, 74);
            this.welcome.Name = "welcome";
            this.welcome.Size = new System.Drawing.Size(122, 21);
            this.welcome.TabIndex = 9;
            this.welcome.Text = "Welcome Back";
            this.welcome.UseWaitCursor = true;
            // 
            // subtitle
            // 
            this.subtitle.AutoEllipsis = true;
            this.subtitle.AutoSize = true;
            this.subtitle.Font = new System.Drawing.Font("Microsoft Tai Le", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.subtitle.Location = new System.Drawing.Point(488, 109);
            this.subtitle.Name = "subtitle";
            this.subtitle.Size = new System.Drawing.Size(285, 14);
            this.subtitle.TabIndex = 10;
            this.subtitle.Text = "Enter your email and password to access your account";
            // 
            // req1
            // 
            this.req1.AutoSize = true;
            this.req1.BackColor = System.Drawing.Color.Transparent;
            this.req1.Font = new System.Drawing.Font("Microsoft Tai Le", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.req1.ForeColor = System.Drawing.Color.Red;
            this.req1.Location = new System.Drawing.Point(532, 162);
            this.req1.Name = "req1";
            this.req1.Size = new System.Drawing.Size(12, 16);
            this.req1.TabIndex = 11;
            this.req1.Text = "*";
            // 
            // req2
            // 
            this.req2.AutoSize = true;
            this.req2.BackColor = System.Drawing.Color.Transparent;
            this.req2.Font = new System.Drawing.Font("Microsoft Tai Le", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.req2.ForeColor = System.Drawing.Color.Red;
            this.req2.Location = new System.Drawing.Point(540, 217);
            this.req2.Name = "req2";
            this.req2.Size = new System.Drawing.Size(12, 16);
            this.req2.TabIndex = 12;
            this.req2.Text = "*";
            // 
            // req3
            // 
            this.req3.AutoSize = true;
            this.req3.BackColor = System.Drawing.Color.Transparent;
            this.req3.Font = new System.Drawing.Font("Microsoft Tai Le", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.req3.ForeColor = System.Drawing.Color.Red;
            this.req3.Location = new System.Drawing.Point(513, 269);
            this.req3.Name = "req3";
            this.req3.Size = new System.Drawing.Size(12, 16);
            this.req3.TabIndex = 13;
            this.req3.Text = "*";
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.req3);
            this.Controls.Add(this.req2);
            this.Controls.Add(this.req1);
            this.Controls.Add(this.subtitle);
            this.Controls.Add(this.welcome);
            this.Controls.Add(this.signin);
            this.Controls.Add(this.rememberme);
            this.Controls.Add(this.YearList);
            this.Controls.Add(this.year);
            this.Controls.Add(this.passwrd);
            this.Controls.Add(this.password);
            this.Controls.Add(this.email);
            this.Controls.Add(this.emailid);
            this.Name = "Login";
            this.Text = "Login";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label emailid;
        private System.Windows.Forms.TextBox email;
        private System.Windows.Forms.Label password;
        private System.Windows.Forms.TextBox passwrd;
        private System.Windows.Forms.Label year;
        private System.Windows.Forms.ComboBox YearList;
        private System.Windows.Forms.CheckBox rememberme;
        private System.Windows.Forms.Button signin;
        private System.Windows.Forms.Label welcome;
        private System.Windows.Forms.Label subtitle;
        private System.Windows.Forms.Label req1;
        private System.Windows.Forms.Label req2;
        private System.Windows.Forms.Label req3;
    }
}