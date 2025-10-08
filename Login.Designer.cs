using PackingApplication.Helper;
using System.Drawing;
using System.Windows.Forms;
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
            this.subtitle1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.eyeicon = new System.Windows.Forms.PictureBox();
            this.yearerror = new System.Windows.Forms.Label();
            this.passworderror = new System.Windows.Forms.Label();
            this.emailerror = new System.Windows.Forms.Label();
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
            this.emailid.Location = new System.Drawing.Point(52, 108);
            this.emailid.Name = "emailid";
            this.emailid.Size = new System.Drawing.Size(46, 13);
            this.emailid.TabIndex = 0;
            this.emailid.Text = "Email ID";
            // 
            // email
            // 
            this.email.Location = new System.Drawing.Point(55, 124);
            this.email.Name = "email";
            this.email.Size = new System.Drawing.Size(257, 20);
            this.email.TabIndex = 1;
            this.email.Tag = "";
            this.email.TextChanged += new System.EventHandler(this.Email_TextChanged);
            // 
            // password
            // 
            this.password.AutoSize = true;
            this.password.Location = new System.Drawing.Point(54, 172);
            this.password.Name = "password";
            this.password.Size = new System.Drawing.Size(53, 13);
            this.password.TabIndex = 2;
            this.password.Text = "Password";
            // 
            // passwrd
            // 
            this.passwrd.Location = new System.Drawing.Point(55, 189);
            this.passwrd.Margin = new System.Windows.Forms.Padding(5);
            this.passwrd.Name = "passwrd";
            this.passwrd.Size = new System.Drawing.Size(257, 20);
            this.passwrd.TabIndex = 3;
            this.passwrd.UseSystemPasswordChar = true;
            this.passwrd.WordWrap = false;
            this.passwrd.TextChanged += new System.EventHandler(this.Passwrd_TextChanged);
            // 
            // year
            // 
            this.year.AutoSize = true;
            this.year.Location = new System.Drawing.Point(54, 240);
            this.year.Name = "year";
            this.year.Size = new System.Drawing.Size(29, 13);
            this.year.TabIndex = 4;
            this.year.Text = "Year";
            // 
            // YearList
            // 
            this.YearList.FormattingEnabled = true;
            this.YearList.Location = new System.Drawing.Point(55, 257);
            this.YearList.Name = "YearList";
            this.YearList.Size = new System.Drawing.Size(257, 21);
            this.YearList.TabIndex = 5;
            this.YearList.SelectedIndexChanged += new System.EventHandler(this.YearList_SelectedIndexChanged);
            // 
            // rememberme
            // 
            this.rememberme.AutoSize = true;
            this.rememberme.Checked = true;
            this.rememberme.CheckState = System.Windows.Forms.CheckState.Checked;
            this.rememberme.Location = new System.Drawing.Point(55, 298);
            this.rememberme.Name = "rememberme";
            this.rememberme.Size = new System.Drawing.Size(94, 17);
            this.rememberme.TabIndex = 7;
            this.rememberme.Text = "Remember me";
            this.rememberme.UseVisualStyleBackColor = true;
            // 
            // signin
            // 
            this.signin.BackColor = System.Drawing.SystemColors.Highlight;
            this.signin.Cursor = System.Windows.Forms.Cursors.Hand;
            this.signin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.signin.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.signin.Location = new System.Drawing.Point(55, 330);
            this.signin.Name = "signin";
            this.signin.Size = new System.Drawing.Size(257, 32);
            this.signin.TabIndex = 8;
            this.signin.Text = "SIGN IN";
            this.signin.UseVisualStyleBackColor = false;
            this.signin.Click += new System.EventHandler(this.signin_Click);
            // 
            // welcome
            // 
            this.welcome.AutoSize = true;
            this.welcome.Location = new System.Drawing.Point(128, 23);
            this.welcome.Name = "welcome";
            this.welcome.Size = new System.Drawing.Size(80, 13);
            this.welcome.TabIndex = 9;
            this.welcome.Text = "Welcome Back";
            this.welcome.UseWaitCursor = true;
            // 
            // subtitle
            // 
            this.subtitle.AutoEllipsis = true;
            this.subtitle.AutoSize = true;
            this.subtitle.Location = new System.Drawing.Point(65, 52);
            this.subtitle.Name = "subtitle";
            this.subtitle.Size = new System.Drawing.Size(200, 13);
            this.subtitle.TabIndex = 10;
            this.subtitle.Text = "Enter your email and password to access";
            // 
            // req1
            // 
            this.req1.AutoSize = true;
            this.req1.BackColor = System.Drawing.Color.Transparent;
            this.req1.ForeColor = System.Drawing.Color.Red;
            this.req1.Location = new System.Drawing.Point(102, 108);
            this.req1.Name = "req1";
            this.req1.Size = new System.Drawing.Size(11, 13);
            this.req1.TabIndex = 11;
            this.req1.Text = "*";
            // 
            // req2
            // 
            this.req2.AutoSize = true;
            this.req2.BackColor = System.Drawing.Color.Transparent;
            this.req2.ForeColor = System.Drawing.Color.Red;
            this.req2.Location = new System.Drawing.Point(113, 172);
            this.req2.Name = "req2";
            this.req2.Size = new System.Drawing.Size(11, 13);
            this.req2.TabIndex = 12;
            this.req2.Text = "*";
            // 
            // req3
            // 
            this.req3.AutoSize = true;
            this.req3.BackColor = System.Drawing.Color.Transparent;
            this.req3.ForeColor = System.Drawing.Color.Red;
            this.req3.Location = new System.Drawing.Point(85, 240);
            this.req3.Name = "req3";
            this.req3.Size = new System.Drawing.Size(11, 13);
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
            this.panel1.Location = new System.Drawing.Point(0, -2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(800, 451);
            this.panel1.TabIndex = 14;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.panel2);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel3.Location = new System.Drawing.Point(414, 0);
            this.panel3.Margin = new System.Windows.Forms.Padding(2);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(386, 451);
            this.panel3.TabIndex = 16;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.panel2.Controls.Add(this.subtitle1);
            this.panel2.Controls.Add(this.email);
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
            this.panel2.Controls.Add(this.year);
            this.panel2.Controls.Add(this.req2);
            this.panel2.Controls.Add(this.passwrd);
            this.panel2.Controls.Add(this.password);
            this.panel2.Location = new System.Drawing.Point(6, 27);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(371, 424);
            this.panel2.TabIndex = 14;
            // 
            // subtitle1
            // 
            this.subtitle1.AutoEllipsis = true;
            this.subtitle1.AutoSize = true;
            this.subtitle1.Location = new System.Drawing.Point(139, 69);
            this.subtitle1.Name = "subtitle1";
            this.subtitle1.Size = new System.Drawing.Size(69, 13);
            this.subtitle1.TabIndex = 20;
            this.subtitle1.Text = "your account";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(75, 400);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(190, 13);
            this.label2.TabIndex = 19;
            this.label2.Text = "BEEKAYLON SYNTHETICS PVT LTD";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.label1.Location = new System.Drawing.Point(85, 379);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(164, 13);
            this.label1.TabIndex = 18;
            this.label1.Text = "ALL RIGHT RESERVED © 2025";
            // 
            // eyeicon
            // 
            this.eyeicon.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.eyeicon.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.eyeicon.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.eyeicon.Cursor = System.Windows.Forms.Cursors.Hand;
            this.eyeicon.Image = global::PackingApplication.Properties.Resources.icons8_hide_24;
            this.eyeicon.Location = new System.Drawing.Point(292, 187);
            this.eyeicon.Margin = new System.Windows.Forms.Padding(2);
            this.eyeicon.Name = "eyeicon";
            this.eyeicon.Size = new System.Drawing.Size(20, 21);
            this.eyeicon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.eyeicon.TabIndex = 17;
            this.eyeicon.TabStop = false;
            this.eyeicon.Click += new System.EventHandler(this.eyeIcon_Click);
            // 
            // yearerror
            // 
            this.yearerror.AutoSize = true;
            this.yearerror.ForeColor = System.Drawing.Color.Red;
            this.yearerror.Location = new System.Drawing.Point(57, 281);
            this.yearerror.Name = "yearerror";
            this.yearerror.Size = new System.Drawing.Size(0, 13);
            this.yearerror.TabIndex = 16;
            this.yearerror.Visible = false;
            // 
            // passworderror
            // 
            this.passworderror.AutoSize = true;
            this.passworderror.ForeColor = System.Drawing.Color.Red;
            this.passworderror.Location = new System.Drawing.Point(54, 212);
            this.passworderror.Name = "passworderror";
            this.passworderror.Size = new System.Drawing.Size(0, 13);
            this.passworderror.TabIndex = 15;
            this.passworderror.Visible = false;
            // 
            // emailerror
            // 
            this.emailerror.AutoSize = true;
            this.emailerror.ForeColor = System.Drawing.Color.Red;
            this.emailerror.Location = new System.Drawing.Point(54, 151);
            this.emailerror.Name = "emailerror";
            this.emailerror.Size = new System.Drawing.Size(0, 13);
            this.emailerror.TabIndex = 14;
            this.emailerror.Visible = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Image = global::PackingApplication.Properties.Resources.login;
            this.pictureBox1.Location = new System.Drawing.Point(0, 2);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(413, 456);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 15;
            this.pictureBox1.TabStop = false;
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.panel1);
            this.Name = "Login";
            this.Text = "Login";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
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
        private Label subtitle1;
    }
}