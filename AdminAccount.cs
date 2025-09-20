using PackingApplication.Helper;
using PackingApplication.Models.CommonEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PackingApplication
{
    public partial class AdminAccount: Form
    {
        protected Panel headerPanel;
        protected Panel footerPanel;
        protected Panel contentPanel;

        MenuStrip menuStrip = new MenuStrip();
        public AdminAccount()
        {
            InitializeComponent();

            headerPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 50,
                BackColor = Color.White
            };

            Panel bottomBorder = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 1,
                BackColor = Color.LightGray   
            };

            FlowLayoutPanel leftPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Left,
                AutoSize = true,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = false,
                BackColor = Color.White
            };

            //leftpanel for logo and menustrip
            string basePath = AppDomain.CurrentDomain.BaseDirectory;
            string projectRoot = Directory.GetParent(basePath).Parent.Parent.FullName;
            string imagePath = Path.Combine(projectRoot, "Images", "logo.png");

            if (File.Exists(imagePath))
            {
                PictureBox logoPictureBox = new PictureBox
                {
                    Image = Image.FromFile(imagePath),
                    SizeMode = PictureBoxSizeMode.Normal,
                    Size = new Size(170, 50),
                    Location = new Point(10, 10)
                };
                leftPanel.Controls.Add(logoPictureBox);
            }
            else
            {
                MessageBox.Show("Image not found: " + imagePath);
            }

            menuStrip.BackColor = Color.White;
            menuStrip.Padding = new Padding(10, 18, 0, 0);

            // POY Menu
            ToolStripMenuItem poy = new ToolStripMenuItem("POYPacking", null, POYPacking_Click)
            {
                Font = FontManager.GetFont(9, FontStyle.Bold),
                BackColor = Color.White,
            };
            poy.Click += (s, e) => HighlightMenuItem(s);
            //ToolStripMenuItem poysubItem = new ToolStripMenuItem("POYPacking", null, POYPacking_Click)
            //{
            //    Font = FontManager.GetFont(8, FontStyle.Regular)
            //};
            //poy.DropDownItems.Add(poysubItem);

            // DTY Menu
            ToolStripMenuItem dty = new ToolStripMenuItem("DTYPacking", null, DTYPacking_Click)
            {
                Font = FontManager.GetFont(9, FontStyle.Bold),
                BackColor = Color.White
            };
            dty.Click += (s, e) => HighlightMenuItem(s);
            // BCF Menu
            ToolStripMenuItem bcf = new ToolStripMenuItem("BCFPacking", null, BCFPacking_Click)
            {
                Font = FontManager.GetFont(9, FontStyle.Bold),
                BackColor = Color.White
            };
            bcf.Click += (s, e) => HighlightMenuItem(s);
            // Chips Menu
            ToolStripMenuItem chips = new ToolStripMenuItem("ChipsPacking", null, ChipsPacking_Click)
            {
                Font = FontManager.GetFont(9, FontStyle.Bold),
                BackColor = Color.White
            };
            chips.Click += (s, e) => HighlightMenuItem(s);
            // Add to menuStrip
            menuStrip.Items.Add(poy);
            menuStrip.Items.Add(dty);
            menuStrip.Items.Add(bcf);
            menuStrip.Items.Add(chips);     

            leftPanel.Controls.Add(menuStrip);
            SetHandCursorForMenuItems(poy, dty, bcf, chips);
            // right panel for profile and logout
            FlowLayoutPanel rightPanel = new FlowLayoutPanel
            {
                AutoSize = true,
                FlowDirection = FlowDirection.LeftToRight, // horizontal row
                Dock = DockStyle.Right,
                Padding = new Padding(0, 10, 15, 0),
                WrapContents = false
            };

            PictureBox profilePictureBox = new PictureBox
            {
                Size = new Size(24, 24),                
                SizeMode = PictureBoxSizeMode.StretchImage,     
                Margin = new Padding(10, 5, 5, 5),       
                Image = Properties.Resources.default_profile
            };

            FlowLayoutPanel userInfoPanel = new FlowLayoutPanel
            {
                AutoSize = true,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                Font = FontManager.GetFont(8, FontStyle.Bold),
                Padding = new Padding(5, 0, 5, 5)
            };

            Label userNameInfoLabel = new Label
            {
                Text = SessionManager.UserName,
                AutoSize = true,
                ForeColor = Color.FromArgb(0, 0, 0),
                Font = FontManager.GetFont(9, FontStyle.Bold),
            };
            Label userRoleInfoLabel = new Label
            {
                Text = SessionManager.Role,
                AutoSize = true,
                ForeColor = Color.FromArgb(115, 115, 115),
                Font = FontManager.GetFont(8, FontStyle.Regular),
            };

            userInfoPanel.Controls.Add(userNameInfoLabel);
            userInfoPanel.Controls.Add(userRoleInfoLabel);

            FlowLayoutPanel profileWithInfo = new FlowLayoutPanel
            {
                AutoSize = true,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = false,
                Margin = new Padding(0),
                Padding = new Padding(0)
            };

            profileWithInfo.Controls.Add(profilePictureBox);
            profileWithInfo.Controls.Add(userInfoPanel);
            // Logout Button
            Button logoutBtn = new Button
            {
                Text = "Logout",
                BackColor = Color.FromArgb(242, 242, 242),
                ForeColor = Color.FromArgb(77, 77, 77),
                FlatStyle = FlatStyle.Flat,
                Width = 75,
                Height = 25,
                Font = FontManager.GetFont(9, FontStyle.Regular),
                Margin = new Padding(15, 5, 0, 0),
                Cursor = Cursors.Hand
            };
            logoutBtn.FlatAppearance.BorderSize = 0;
            logoutBtn.FlatAppearance.MouseOverBackColor = logoutBtn.BackColor;
            logoutBtn.FlatAppearance.MouseDownBackColor = logoutBtn.BackColor;
            logoutBtn.Click += Logout_Click;

            // Add to right panel
            rightPanel.Controls.Add(profileWithInfo);
            rightPanel.Controls.Add(logoutBtn);

            // Add right panel to header
            headerPanel.Controls.Add(leftPanel);
            headerPanel.Controls.Add(rightPanel);
            headerPanel.Controls.Add(bottomBorder);
            // Add header to form
            this.Controls.Add(headerPanel);

            // footer
            //footerPanel = new Panel
            //{
            //    Dock = DockStyle.Bottom,
            //    Height = 40,
            //    BackColor = Color.White
            //};

            //Panel topBorder = new Panel
            //{
            //    Dock = DockStyle.Top,
            //    Height = 1,
            //    BackColor = Color.LightGray   // border color
            //};

            //Label footerLabel = new Label
            //{
            //    Text = "YEAR: 2025 " + SessionManager.UserName,
            //    Font = FontManager.GetFont(8, FontStyle.Bold),
            //    AutoSize = true,
            //    Anchor = AnchorStyles.Bottom | AnchorStyles.Right  
            //};

            //footerLabel.Location = new Point(
            //    footerPanel.Width - footerLabel.Width - 10,
            //    footerPanel.Height - footerLabel.Height - 10
            //);

            //footerPanel.Resize += (s, e) =>
            //{
            //    footerLabel.Location = new Point(
            //        footerPanel.Width - footerLabel.Width - 10,
            //        footerPanel.Height - footerLabel.Height - 10
            //    );
            //};

            //footerPanel.Controls.Add(footerLabel);
            //footerPanel.Controls.Add(topBorder);
            //this.Controls.Add(footerPanel);

            // content panel (sticky between header & footer)
            contentPanel = new Panel
            {
                Dock = DockStyle.Fill,   
                BackColor = Color.White,
            };
            this.Controls.Add(contentPanel);
            this.Controls.SetChildIndex(contentPanel, 0);

            //LoadFormInContent(new Dashboard());
        }

        private void SetHandCursorForMenuItems(params ToolStripMenuItem[] menuItems)
        {
            foreach (var item in menuItems)
            {
                item.MouseEnter += (s, e) => this.Cursor = Cursors.Hand;
                item.MouseLeave += (s, e) => this.Cursor = Cursors.Default;
            }
        }

        private void Logout_Click(object sender, EventArgs e)
        {
            SessionManager.Clear();

            var loginForm = new Login();
            loginForm.Show();
            this.Close();
        }

        public void LoadFormInContent(Form form)
        {
            contentPanel.Controls.Clear();
            form.TopLevel = false;
            form.FormBorderStyle = FormBorderStyle.None;
            form.Dock = DockStyle.Fill;
            form.BackColor = Color.White;

            contentPanel.Controls.Add(form);
            form.Show();
        }

        private void POYPacking_Click(object sender, EventArgs e)
        {
            //var parent = this.ParentForm as Dashboard;
            //if (parent != null)
            //{
            //    parent.LoadFormInContent(new POYPackingForm());
            //}
            var dashboard = this.FindForm() as AdminAccount;
            if (dashboard != null)
            {
                dashboard.LoadFormInContent(new POYPackingList());
            }
        }

        private void DTYPacking_Click(object sender, EventArgs e)
        {
            var dashboard = this.FindForm() as AdminAccount;
            if (dashboard != null)
            {
                dashboard.LoadFormInContent(new DTYPackingList());
            }
        }

        private void BCFPacking_Click(object sender, EventArgs e)
        {
            var dashboard = this.FindForm() as AdminAccount;
            if (dashboard != null)
            {
                dashboard.LoadFormInContent(new BCFPackingList());
            }
        }

        private void ChipsPacking_Click(object sender, EventArgs e)
        {
            var dashboard = this.FindForm() as AdminAccount;
            if (dashboard != null)
            {
                dashboard.LoadFormInContent(new ChipsPackingList());
            }
        }

        // highlight selected menu
        private void HighlightMenuItem(object sender)
        {
            foreach (ToolStripMenuItem item in menuStrip.Items)
            {
                item.BackColor = Color.White; // reset all
            }

            ToolStripMenuItem clickedItem = sender as ToolStripMenuItem;
            if (clickedItem != null)
            {
                clickedItem.BackColor = Color.FromArgb(230,240,255); // highlight selected
            }
        }
    }
}
