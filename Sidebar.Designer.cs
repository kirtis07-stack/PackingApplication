namespace PackingApplication
{
    partial class Sidebar
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
            this.components = new System.ComponentModel.Container();
            this.sidebarContainer = new System.Windows.Forms.FlowLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.menu = new System.Windows.Forms.Label();
            this.menuBtn = new System.Windows.Forms.PictureBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.homebtn = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.aboutbtn = new System.Windows.Forms.Button();
            this.sidebarTimer = new System.Windows.Forms.Timer(this.components);
            this.panel6 = new System.Windows.Forms.Panel();
            this.panel7 = new System.Windows.Forms.Panel();
            this.sidebarContainer.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.menuBtn)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel6.SuspendLayout();
            this.SuspendLayout();
            // 
            // sidebarContainer
            // 
            this.sidebarContainer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.sidebarContainer.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.sidebarContainer.Controls.Add(this.panel1);
            this.sidebarContainer.Controls.Add(this.panel2);
            this.sidebarContainer.Controls.Add(this.panel3);
            this.sidebarContainer.Location = new System.Drawing.Point(3, 0);
            this.sidebarContainer.MaximumSize = new System.Drawing.Size(200, 450);
            this.sidebarContainer.MinimumSize = new System.Drawing.Size(74, 450);
            this.sidebarContainer.Name = "sidebarContainer";
            this.sidebarContainer.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.sidebarContainer.Size = new System.Drawing.Size(170, 450);
            this.sidebarContainer.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel5);
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(197, 76);
            this.panel1.TabIndex = 1;
            // 
            // panel5
            // 
            this.panel5.Location = new System.Drawing.Point(195, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(606, 447);
            this.panel5.TabIndex = 1;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.menu);
            this.panel4.Controls.Add(this.menuBtn);
            this.panel4.Location = new System.Drawing.Point(21, 9);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(139, 55);
            this.panel4.TabIndex = 1;
            // 
            // menu
            // 
            this.menu.AutoSize = true;
            this.menu.ForeColor = System.Drawing.Color.Black;
            this.menu.Location = new System.Drawing.Point(48, 20);
            this.menu.Name = "menu";
            this.menu.Size = new System.Drawing.Size(34, 13);
            this.menu.TabIndex = 1;
            this.menu.Text = "Menu";
            // 
            // menuBtn
            // 
            this.menuBtn.Image = global::PackingApplication.Properties.Resources.icons8_menu_50;
            this.menuBtn.Location = new System.Drawing.Point(3, 15);
            this.menuBtn.Name = "menuBtn";
            this.menuBtn.Size = new System.Drawing.Size(29, 24);
            this.menuBtn.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.menuBtn.TabIndex = 1;
            this.menuBtn.TabStop = false;
            this.menuBtn.Click += new System.EventHandler(this.menuBtn_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.homebtn);
            this.panel2.Location = new System.Drawing.Point(3, 85);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(197, 54);
            this.panel2.TabIndex = 1;
            // 
            // homebtn
            // 
            this.homebtn.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.homebtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.homebtn.Font = new System.Drawing.Font("Microsoft Tai Le", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.homebtn.ForeColor = System.Drawing.Color.Black;
            this.homebtn.Image = global::PackingApplication.Properties.Resources.icons8_one_48;
            this.homebtn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.homebtn.Location = new System.Drawing.Point(-16, -8);
            this.homebtn.Name = "homebtn";
            this.homebtn.Padding = new System.Windows.Forms.Padding(30, 0, 0, 0);
            this.homebtn.Size = new System.Drawing.Size(229, 63);
            this.homebtn.TabIndex = 1;
            this.homebtn.Text = "              Home";
            this.homebtn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.homebtn.UseVisualStyleBackColor = false;
            this.homebtn.Click += new System.EventHandler(this.btnAddPOY_Click);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.aboutbtn);
            this.panel3.Location = new System.Drawing.Point(3, 145);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(197, 54);
            this.panel3.TabIndex = 2;
            // 
            // aboutbtn
            // 
            this.aboutbtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.aboutbtn.Font = new System.Drawing.Font("Microsoft Tai Le", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.aboutbtn.ForeColor = System.Drawing.Color.Black;
            this.aboutbtn.Image = global::PackingApplication.Properties.Resources.icons8_circled_2_48;
            this.aboutbtn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.aboutbtn.Location = new System.Drawing.Point(-16, -8);
            this.aboutbtn.Name = "aboutbtn";
            this.aboutbtn.Padding = new System.Windows.Forms.Padding(30, 0, 0, 0);
            this.aboutbtn.Size = new System.Drawing.Size(229, 63);
            this.aboutbtn.TabIndex = 2;
            this.aboutbtn.Text = "              About";
            this.aboutbtn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.aboutbtn.UseVisualStyleBackColor = true;
            // 
            // sidebarTimer
            // 
            this.sidebarTimer.Interval = 10;
            this.sidebarTimer.Tick += new System.EventHandler(this.sidebarTimer_Tick);
            // 
            // panel6
            // 
            this.panel6.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel6.Controls.Add(this.panel7);
            this.panel6.Controls.Add(this.sidebarContainer);
            this.panel6.Location = new System.Drawing.Point(-2, 0);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(800, 453);
            this.panel6.TabIndex = 1;
            // 
            // panel7
            // 
            this.panel7.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel7.Location = new System.Drawing.Point(78, 3);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(723, 447);
            this.panel7.TabIndex = 1;
            // 
            // Sidebar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.panel6);
            this.Name = "Sidebar";
            this.Text = "Sidebar";
            this.sidebarContainer.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.menuBtn)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button homebtn;
        private System.Windows.Forms.Button aboutbtn;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.PictureBox menuBtn;
        private System.Windows.Forms.Label menu;
        private System.Windows.Forms.Timer sidebarTimer;
        public System.Windows.Forms.FlowLayoutPanel sidebarContainer;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Panel panel7;
    }
}