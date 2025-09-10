using PackingApplication.Helper;
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.yearerror = new System.Windows.Forms.Label();
            this.passworderror = new System.Windows.Forms.Label();
            this.emailerror = new System.Windows.Forms.Label();
            this.eyeicon = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.eyeicon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // emailid
            // 
            this.emailid.AutoSize = true;
            this.emailid.Font = FontManager.GetFont(8, FontStyle.Regular);
            this.emailid.Location = new System.Drawing.Point(78, 163);
            this.emailid.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.emailid.Name = "emailid";
            this.emailid.Size = new System.Drawing.Size(73, 22);
            this.emailid.TabIndex = 0;
            this.emailid.Text = "Email ID";
            // 
            // email
            // 
            this.email.Location = new System.Drawing.Point(82, 194);
            this.email.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.email.Name = "email";
            this.email.Size = new System.Drawing.Size(384, 26);
            this.email.Font = FontManager.GetFont(8, FontStyle.Regular);
            this.email.TabIndex = 1;
            this.email.Tag = "";
            this.email.TextChanged += new System.EventHandler(this.Email_TextChanged);
            // 
            // password
            // 
            this.password.AutoSize = true;
            this.password.Font = FontManager.GetFont(8, FontStyle.Regular);
            this.password.Location = new System.Drawing.Point(81, 262);
            this.password.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.password.Name = "password";
            this.password.Size = new System.Drawing.Size(81, 22);
            this.password.TabIndex = 2;
            this.password.Text = "Password";
            // 
            // passwrd
            // 
            this.passwrd.Location = new System.Drawing.Point(82, 288);
            this.passwrd.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.passwrd.Name = "passwrd";
            this.passwrd.Size = new System.Drawing.Size(384, 26);
            this.passwrd.TabIndex = 3;
            this.passwrd.UseSystemPasswordChar = true;
            this.passwrd.WordWrap = false;
            this.passwrd.Font = FontManager.GetFont(8, FontStyle.Regular);
            this.passwrd.TextChanged += new System.EventHandler(this.Passwrd_TextChanged);
            // 
            // year
            // 
            this.year.AutoSize = true;
            this.year.Font = FontManager.GetFont(8, FontStyle.Regular);
            this.year.Location = new System.Drawing.Point(81, 366);
            this.year.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.year.Name = "year";
            this.year.Size = new System.Drawing.Size(43, 22);
            this.year.TabIndex = 4;
            this.year.Text = "Year";
            // 
            // YearList
            // 
            this.YearList.FormattingEnabled = true;
            this.YearList.Location = new System.Drawing.Point(82, 392);
            this.YearList.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.YearList.Name = "YearList";
            this.YearList.Size = new System.Drawing.Size(384, 28);
            this.YearList.TabIndex = 5;
            this.YearList.Font = FontManager.GetFont(8, FontStyle.Regular);
            this.YearList.SelectedIndexChanged += new System.EventHandler(this.YearList_SelectedIndexChanged);
            // 
            // rememberme
            // 
            this.rememberme.AutoSize = true;
            this.rememberme.Checked = true;
            this.rememberme.CheckState = System.Windows.Forms.CheckState.Checked;
            this.rememberme.Font = FontManager.GetFont(8, FontStyle.Regular);
            this.rememberme.Location = new System.Drawing.Point(82, 456);
            this.rememberme.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.rememberme.Name = "rememberme";
            this.rememberme.Size = new System.Drawing.Size(148, 26);
            this.rememberme.TabIndex = 7;
            this.rememberme.Text = "Remember me";
            this.rememberme.UseVisualStyleBackColor = true;
            // 
            // signin
            // 
            this.signin.BackColor = System.Drawing.SystemColors.Highlight;
            this.signin.Cursor = System.Windows.Forms.Cursors.Hand;
            this.signin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.signin.Font = FontManager.GetFont(9, FontStyle.Bold);
            this.signin.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.signin.Location = new System.Drawing.Point(82, 504);
            this.signin.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.signin.Name = "signin";
            this.signin.Size = new System.Drawing.Size(386, 49);
            this.signin.TabIndex = 8;
            this.signin.Text = "SIGN IN";
            this.signin.UseVisualStyleBackColor = false;
            this.signin.Click += new System.EventHandler(this.signin_Click);
            // 
            // welcome
            // 
            this.welcome.AutoSize = true;
            this.welcome.Font = FontManager.GetFont(14, FontStyle.Bold);
            this.welcome.Location = new System.Drawing.Point(192, 36);
            this.welcome.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.welcome.Name = "welcome";
            this.welcome.Size = new System.Drawing.Size(207, 35);
            this.welcome.TabIndex = 9;
            this.welcome.Text = "Welcome Back";
            this.welcome.UseWaitCursor = true;
            // 
            // subtitle
            // 
            this.subtitle.AutoEllipsis = true;
            this.subtitle.AutoSize = true;
            this.subtitle.Font = FontManager.GetFont(9, FontStyle.Regular);
            this.subtitle.Location = new System.Drawing.Point(81, 80);
            this.subtitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.subtitle.Name = "subtitle";
            this.subtitle.Size = new System.Drawing.Size(440, 23);
            this.subtitle.TabIndex = 10;
            this.subtitle.Text = "Enter your email and password to access your account";
            // 
            // req1
            // 
            this.req1.AutoSize = true;
            this.req1.BackColor = System.Drawing.Color.Transparent;
            this.req1.Font = FontManager.GetFont(9, FontStyle.Regular);
            this.req1.ForeColor = System.Drawing.Color.Red;
            this.req1.Location = new System.Drawing.Point(144, 163);
            this.req1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.req1.Name = "req1";
            this.req1.Size = new System.Drawing.Size(20, 25);
            this.req1.TabIndex = 11;
            this.req1.Text = "*";
            // 
            // req2
            // 
            this.req2.AutoSize = true;
            this.req2.BackColor = System.Drawing.Color.Transparent;
            this.req2.Font = FontManager.GetFont(9, FontStyle.Regular);
            this.req2.ForeColor = System.Drawing.Color.Red;
            this.req2.Location = new System.Drawing.Point(160, 260);
            this.req2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.req2.Name = "req2";
            this.req2.Size = new System.Drawing.Size(20, 25);
            this.req2.TabIndex = 12;
            this.req2.Text = "*";
            // 
            // req3
            // 
            this.req3.AutoSize = true;
            this.req3.BackColor = System.Drawing.Color.Transparent;
            this.req3.Font = FontManager.GetFont(9, FontStyle.Regular);
            this.req3.ForeColor = System.Drawing.Color.Red;
            this.req3.Location = new System.Drawing.Point(118, 366);
            this.req3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.req3.Name = "req3";
            this.req3.Size = new System.Drawing.Size(20, 25);
            this.req3.TabIndex = 13;
            this.req3.Text = "*";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Location = new System.Drawing.Point(0, -3);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1200, 694);
            this.panel1.TabIndex = 14;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.panel2);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel3.Location = new System.Drawing.Point(621, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(579, 694);
            this.panel3.TabIndex = 16;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.eyeicon);
            this.panel2.Controls.Add(this.yearerror);
            this.panel2.Controls.Add(this.passworderror);
            this.panel2.Controls.Add(this.emailerror);
            this.panel2.Controls.Add(this.welcome);
            this.panel2.Controls.Add(this.signin);
            this.panel2.Controls.Add(this.subtitle);
            this.panel2.Controls.Add(this.rememberme);
            this.panel2.Controls.Add(this.req3);
            this.panel2.Controls.Add(this.YearList);
            this.panel2.Controls.Add(this.emailid);
            this.panel2.Controls.Add(this.req1);
            this.panel2.Controls.Add(this.email);
            this.panel2.Controls.Add(this.year);
            this.panel2.Controls.Add(this.req2);
            this.panel2.Controls.Add(this.passwrd);
            this.panel2.Controls.Add(this.password);
            this.panel2.Location = new System.Drawing.Point(9, 42);
            this.panel2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(557, 652);
            this.panel2.TabIndex = 14;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = FontManager.GetFont(8, FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(145, 597);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(268, 21);
            this.label2.TabIndex = 19;
            this.label2.Text = "BEEKAYLON SYNTHETICS PVT LTD";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = FontManager.GetFont(8, FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.label1.Location = new System.Drawing.Point(156, 572);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(229, 21);
            this.label1.TabIndex = 18;
            this.label1.Text = "ALL RIGHT RESERVED © 2025";
            // 
            // yearerror
            // 
            this.yearerror.AutoSize = true;
            this.yearerror.Font = FontManager.GetFont(8, FontStyle.Regular);
            this.yearerror.ForeColor = System.Drawing.Color.Red;
            this.yearerror.Location = new System.Drawing.Point(86, 429);
            this.yearerror.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.yearerror.Name = "yearerror";
            this.yearerror.Size = new System.Drawing.Size(0, 22);
            this.yearerror.TabIndex = 16;
            this.yearerror.Visible = false;
            // 
            // passworderror
            // 
            this.passworderror.AutoSize = true;
            this.passworderror.Font = FontManager.GetFont(8, FontStyle.Regular);
            this.passworderror.ForeColor = System.Drawing.Color.Red;
            this.passworderror.Location = new System.Drawing.Point(81, 323);
            this.passworderror.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.passworderror.Name = "passworderror";
            this.passworderror.Size = new System.Drawing.Size(0, 22);
            this.passworderror.TabIndex = 15;
            this.passworderror.Visible = false;
            // 
            // emailerror
            // 
            this.emailerror.AutoSize = true;
            this.emailerror.Font = FontManager.GetFont(8, FontStyle.Regular);
            this.emailerror.ForeColor = System.Drawing.Color.Red;
            this.emailerror.Location = new System.Drawing.Point(81, 229);
            this.emailerror.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.emailerror.Name = "emailerror";
            this.emailerror.Size = new System.Drawing.Size(0, 22);
            this.emailerror.TabIndex = 14;
            this.emailerror.Visible = false;
            // 
            // eyeicon
            // 
            this.eyeicon.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.eyeicon.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.eyeicon.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.eyeicon.Image = global::PackingApplication.Properties.Resources.icons8_hide_24;
            this.eyeicon.Location = new System.Drawing.Point(440, 290);
            this.eyeicon.Name = "eyeicon";
            this.eyeicon.Size = new System.Drawing.Size(24, 24);
            this.eyeicon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.eyeicon.TabIndex = 17;
            this.eyeicon.TabStop = false;
            this.eyeicon.Click += eyeIcon_Click;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Image = global::PackingApplication.Properties.Resources.login;
            this.pictureBox1.Location = new System.Drawing.Point(0, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(620, 702);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 15;
            this.pictureBox1.TabStop = false;
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(1200, 692);
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Login";
            this.Text = "Login";
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.eyeicon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

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
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label emailerror;
        private System.Windows.Forms.Label yearerror;
        private System.Windows.Forms.Label passworderror;
        private System.Windows.Forms.PictureBox eyeicon;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}