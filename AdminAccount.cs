using PackingApplication.Models.CommonEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
        public AdminAccount()
        {
            InitializeComponent();

            headerPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 60,
                BackColor = Color.WhiteSmoke
            };

            // ==== LEFT: LOGO ====
            string basePath = AppDomain.CurrentDomain.BaseDirectory;
            string projectRoot = Directory.GetParent(basePath).Parent.Parent.FullName;
            string imagePath = Path.Combine(projectRoot, "Images", "logo.png");

            if (File.Exists(imagePath))
            {
                PictureBox logoPictureBox = new PictureBox
                {
                    Image = Image.FromFile(imagePath),
                    SizeMode = PictureBoxSizeMode.StretchImage,
                    Size = new Size(120, 40),
                    Location = new Point(10, 10)
                };
                headerPanel.Controls.Add(logoPictureBox);
            }
            else
            {
                MessageBox.Show("Image not found: " + imagePath);
            }

            // ==== RIGHT SIDE CONTAINER ====
            FlowLayoutPanel rightPanel = new FlowLayoutPanel
            {
                AutoSize = true,
                FlowDirection = FlowDirection.LeftToRight, // horizontal row
                Dock = DockStyle.Right,
                Padding = new Padding(0, 10, 15, 0),
                WrapContents = false
            };

            // User Info (stacked vertically)
            FlowLayoutPanel userInfoPanel = new FlowLayoutPanel
            {
                AutoSize = true,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                Font = new Font("Microsoft Tai Le", 8, FontStyle.Bold),
                Padding = new Padding(5, 5, 5, 5)
            };

            Label userNameInfoLabel = new Label
            {
                Text = SessionManager.UserName,
                AutoSize = true
            };
            Label userRoleInfoLabel = new Label
            {
                Text = SessionManager.Role,
                AutoSize = true
            };

            userInfoPanel.Controls.Add(userNameInfoLabel);
            userInfoPanel.Controls.Add(userRoleInfoLabel);


            // Logout Button
            Button logoutBtn = new Button
            {
                Text = "Logout",
                BackColor = Color.DodgerBlue,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Width = 80,
                Height = 30,
                Font = new Font("Microsoft Tai Le", 8.25F, FontStyle.Bold),
                Margin = new Padding(10, 5, 0, 0) // spacing from user info
            };
            logoutBtn.Click += Logout_Click;

            // Add to right panel
            rightPanel.Controls.Add(userInfoPanel);
            rightPanel.Controls.Add(logoutBtn);

            // Add right panel to header
            headerPanel.Controls.Add(rightPanel);

            // Add header to form
            this.Controls.Add(headerPanel);

            // footer
            footerPanel = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 40,
                BackColor = Color.LightGray
            };

            Label footerLabel = new Label
            {
                Text = "YEAR: 2025 " + SessionManager.UserName,
                Font = new Font("Microsoft Tai Le", 8, FontStyle.Bold),
                AutoSize = true,
                Anchor = AnchorStyles.Bottom | AnchorStyles.Right  
            };

            footerLabel.Location = new Point(
                footerPanel.Width - footerLabel.Width - 10,
                footerPanel.Height - footerLabel.Height - 10
            );

            footerPanel.Resize += (s, e) =>
            {
                footerLabel.Location = new Point(
                    footerPanel.Width - footerLabel.Width - 10,
                    footerPanel.Height - footerLabel.Height - 10
                );
            };

            footerPanel.Controls.Add(footerLabel);
            this.Controls.Add(footerPanel);

            // CONTENT PANEL (sticky between header & footer)
            contentPanel = new Panel
            {
                Dock = DockStyle.Fill,   
                BackColor = Color.White
            };
            this.Controls.Add(contentPanel);
            this.Controls.SetChildIndex(contentPanel, 0);

            LoadFormInContent(new Dashboard());
        }

        private void Logout_Click(object sender, EventArgs e)
        {
            SessionManager.Clear();

            var loginForm = new Login();
            loginForm.Show();
            this.Close();
        }

        //private void POYPacking_Click(object sender, EventArgs e)
        //{
        //    this.Close();
        //    var frm = new POYPackingForm();
        //    frm.Show();
        //}

        //private void DTYPacking_Click(object sender, EventArgs e)
        //{
        //    var frm = new DTYPackingForm();
        //    frm.Show();
        //}

        public void LoadFormInContent(Form form)
        {
            contentPanel.Controls.Clear();
            form.TopLevel = false;
            form.FormBorderStyle = FormBorderStyle.None;
            form.Dock = DockStyle.Fill;

            contentPanel.Controls.Add(form);
            form.Show();
        }
    }
}
