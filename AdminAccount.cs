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

            //Label headerLabel = new Label
            //{
            //    Text = "My Application Header",
            //    Font = new Font("Microsoft Tai Le", 10, FontStyle.Bold),
            //    AutoSize = true,
            //    Location = new Point(10, 15)
            //};

            string basePath = AppDomain.CurrentDomain.BaseDirectory;
            string projectRoot = Directory.GetParent(basePath).Parent.Parent.FullName;
            string imagePath = Path.Combine(projectRoot, "Images", "logo.png");

            if (!File.Exists(imagePath))
            {
                MessageBox.Show("Image not found: " + imagePath);
            }
            else
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
            Panel userInfoPanel = new Panel
            {
                AutoSize = true,
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                Font = new Font("Microsoft Tai Le", 8, FontStyle.Bold),
                Location = new Point(headerPanel.Width - 150, 10) 
            };

            // label for username
            Label userNameInfoLabel = new Label
            {
                Text = SessionManager.UserName,
                AutoSize = true
            };
            userInfoPanel.Controls.Add(userNameInfoLabel);

            // label for role
            Label userRoleInfoLabel = new Label
            {
                Text = SessionManager.Role,
                AutoSize = true,
                Top = userNameInfoLabel.Bottom + 2  
            };

            userInfoPanel.Controls.Add(userRoleInfoLabel);

            headerPanel.Controls.Add(userInfoPanel);

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
