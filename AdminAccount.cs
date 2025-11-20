using Microsoft.Reporting.Map.WebForms.BingMaps;
using PackingApplication.Helper;
using PackingApplication.Models.CommonEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace PackingApplication
{
    public partial class AdminAccount: Form
    {
        protected Panel headerPanel;
        protected Panel footerPanel;
        protected Panel contentPanel;
        int currentIndex = 0;
        private Form activeForm = null;
        private List<Form> minimizedForms = new List<Form>();
        private Dictionary<string, Form> openForms = new Dictionary<string, Form>();
        MenuStrip menuStrip = new MenuStrip();
        private Form activeChild = null;

        // Windows menu
        ToolStripMenuItem windows = new ToolStripMenuItem("Windows")
        {
            Font = FontManager.GetFont(9, FontStyle.Bold),
            BackColor = Color.White
        };
        public AdminAccount()
        {
            InitializeComponent();
            this.Text = "Packing";

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
            //string basePath = AppDomain.CurrentDomain.BaseDirectory;
            //string projectRoot = Directory.GetParent(basePath).Parent.Parent.FullName;
            //string imagePath = Path.Combine(projectRoot, "Images", "logo.png");

            //if (File.Exists(imagePath))
            //{
                PictureBox logoPictureBox = new PictureBox
                {
                    Image = Properties.Resources.logo,
                    SizeMode = PictureBoxSizeMode.Normal,
                    Size = new Size(170, 50),
                    Location = new System.Drawing.Point(10, 10)
                };
                leftPanel.Controls.Add(logoPictureBox);
            //}
            //else
            //{
            //    MessageBox.Show("Image not found: " + imagePath);
            //}

            menuStrip.BackColor = Color.White;
            menuStrip.Padding = new Padding(10, 18, 0, 0);
            menuStrip.TabIndex = 0;
            this.MainMenuStrip = menuStrip;

            // POY Menu
            ToolStripMenuItem poy = new ToolStripMenuItem("POYPacking")
            {
                Font = FontManager.GetFont(9, FontStyle.Bold),
                BackColor = Color.White,
            };
            poy.Click += (s, e) => HighlightMenuItem(s);
            poy.DropDownItemClicked += (s, ev) => HighlightMenuItem(ev.ClickedItem);
            ToolStripMenuItem addpoy = new ToolStripMenuItem("Add POY Packing", null, AddPOYPacking_Click)
            {
                Font = FontManager.GetFont(8, FontStyle.Regular)
            };
            ToolStripMenuItem modifypoy = new ToolStripMenuItem("Modify POY Packing", null, ModifyPOYPacking_Click)
            {
                Font = FontManager.GetFont(8, FontStyle.Regular)
            };
            ToolStripMenuItem deletepoy = new ToolStripMenuItem("Delete POY Packing")
            {
                Font = FontManager.GetFont(8, FontStyle.Regular)
            };
            ToolStripMenuItem viewpoy = new ToolStripMenuItem("View POY Packing", null, ViewPOYPacking_Click)
            {
                Font = FontManager.GetFont(8, FontStyle.Regular)
            };
            ToolStripMenuItem printpoy = new ToolStripMenuItem("Print POY Packing Slip")
            {
                Font = FontManager.GetFont(8, FontStyle.Regular)
            };
            poy.DropDownItems.Add(addpoy);
            poy.DropDownItems.Add(modifypoy);
            poy.DropDownItems.Add(deletepoy);
            poy.DropDownItems.Add(viewpoy);
            poy.DropDownItems.Add(printpoy);

            // DTY Menu
            ToolStripMenuItem dty = new ToolStripMenuItem("DTYPacking")
            {
                Font = FontManager.GetFont(9, FontStyle.Bold),
                BackColor = Color.White
            };
            dty.Click += (s, e) => HighlightMenuItem(s);
            dty.DropDownItemClicked += (s, ev) => HighlightMenuItem(ev.ClickedItem);
            ToolStripMenuItem adddty = new ToolStripMenuItem("Add DTY Packing", null, DTYPacking_Click)
            {
                Font = FontManager.GetFont(8, FontStyle.Regular)
            };
            ToolStripMenuItem modifydty = new ToolStripMenuItem("Modify DTY Packing", null, ModifyDTYPacking_Click)
            {
                Font = FontManager.GetFont(8, FontStyle.Regular)
            };
            ToolStripMenuItem deletedty = new ToolStripMenuItem("Delete DTY Packing")
            {
                Font = FontManager.GetFont(8, FontStyle.Regular)
            };
            ToolStripMenuItem viewdty = new ToolStripMenuItem("View DTY Packing", null, ViewDTYPacking_Click)
            {
                Font = FontManager.GetFont(8, FontStyle.Regular)
            };
            ToolStripMenuItem printdty = new ToolStripMenuItem("Print DTY Packing Slip")
            {
                Font = FontManager.GetFont(8, FontStyle.Regular)
            };
            dty.DropDownItems.Add(adddty);
            dty.DropDownItems.Add(modifydty);
            dty.DropDownItems.Add(deletedty);
            dty.DropDownItems.Add(viewdty);
            dty.DropDownItems.Add(printdty);

            // BCF Menu
            ToolStripMenuItem bcf = new ToolStripMenuItem("BCFPacking")
            {
                Font = FontManager.GetFont(9, FontStyle.Bold),
                BackColor = Color.White
            };
            bcf.Click += (s, e) => HighlightMenuItem(s);
            bcf.DropDownItemClicked += (s, ev) => HighlightMenuItem(ev.ClickedItem);
            ToolStripMenuItem addbcf = new ToolStripMenuItem("Add BCF Packing", null, BCFPacking_Click)
            {
                Font = FontManager.GetFont(8, FontStyle.Regular)
            };
            ToolStripMenuItem modifybcf = new ToolStripMenuItem("Modify BCF Packing", null, ModifyBCFPacking_Click)
            {
                Font = FontManager.GetFont(8, FontStyle.Regular)
            };
            ToolStripMenuItem deletebcf = new ToolStripMenuItem("Delete BCF Packing")
            {
                Font = FontManager.GetFont(8, FontStyle.Regular)
            };
            ToolStripMenuItem viewbcf = new ToolStripMenuItem("View BCF Packing", null, ViewBCFPacking_Click)
            {
                Font = FontManager.GetFont(8, FontStyle.Regular)
            };
            ToolStripMenuItem printbcf = new ToolStripMenuItem("Print BCF Packing Slip")
            {
                Font = FontManager.GetFont(8, FontStyle.Regular)
            };
            bcf.DropDownItems.Add(addbcf);
            bcf.DropDownItems.Add(modifybcf);
            bcf.DropDownItems.Add(deletebcf);
            bcf.DropDownItems.Add(viewbcf);
            bcf.DropDownItems.Add(printbcf);

            // Chips Menu
            ToolStripMenuItem chips = new ToolStripMenuItem("ChipsPacking")
            {
                Font = FontManager.GetFont(9, FontStyle.Bold),
                BackColor = Color.White
            };
            chips.Click += (s, e) => HighlightMenuItem(s);
            chips.DropDownItemClicked += (s, ev) => HighlightMenuItem(ev.ClickedItem);
            ToolStripMenuItem addchips = new ToolStripMenuItem("Add Chips Packing", null, ChipsPacking_Click)
            {
                Font = FontManager.GetFont(8, FontStyle.Regular)
            };
            ToolStripMenuItem modifychips = new ToolStripMenuItem("Modify Chips Packing", null, ModifyChipsPacking_Click)
            {
                Font = FontManager.GetFont(8, FontStyle.Regular)
            };
            ToolStripMenuItem deletechips = new ToolStripMenuItem("Delete Chips Packing")
            {
                Font = FontManager.GetFont(8, FontStyle.Regular)
            };
            ToolStripMenuItem viewchips = new ToolStripMenuItem("View Chips Packing", null, ViewChipsPacking_Click)
            {
                Font = FontManager.GetFont(8, FontStyle.Regular)
            };
            ToolStripMenuItem printchips = new ToolStripMenuItem("Print Chips Packing Slip")
            {
                Font = FontManager.GetFont(8, FontStyle.Regular)
            };
            chips.DropDownItems.Add(addchips);
            chips.DropDownItems.Add(modifychips);
            chips.DropDownItems.Add(deletechips);
            chips.DropDownItems.Add(viewchips);
            chips.DropDownItems.Add(printchips);

            
            windows.Click += (s, e) => HighlightMenuItem(s);
            windows.DropDownItemClicked += (s, ev) => HighlightMenuItem(ev.ClickedItem);
            // Add to menuStrip
            menuStrip.Items.Add(poy);
            menuStrip.Items.Add(dty);
            menuStrip.Items.Add(bcf);
            menuStrip.Items.Add(chips);
            menuStrip.Items.Add(windows);

            leftPanel.Controls.Add(menuStrip);
            SetHandCursorForMenuItems(menuStrip.Items);
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
            System.Windows.Forms.Button logoutBtn = new System.Windows.Forms.Button
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
           
            menuStrip.TabStop = true;
            menuStrip.TabIndex = 0;
            logoutBtn.TabIndex = 1;

            menuStrip.Enter += MenuStrip_EnterHandler;
            // Navigate Shift+Tab inside MenuStrip
            menuStrip.PreviewKeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Tab)
                {
                    e.IsInputKey = true;

                    if (e.Shift) // backwards
                    {
                        if (currentIndex == 0)
                        {
                            // Shift+Tab on first item → leave MenuStrip, go to Logout
                            logoutBtn.Focus();
                        }
                        else
                        {
                            currentIndex--;
                            ((ToolStripMenuItem)menuStrip.Items[currentIndex]).Select();
                        }
                    }
                    else // forwards
                    {
                        if (currentIndex == menuStrip.Items.Count - 1)
                        {
                            // Tab on last item → go to Logout
                            logoutBtn.Focus();
                        }
                        else
                        {
                            currentIndex++;
                            ((ToolStripMenuItem)menuStrip.Items[currentIndex]).Select();
                        }
                    }
                }
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
            headerPanel.Dock = DockStyle.Top;
            this.IsMdiContainer = true;
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
            //contentPanel = new Panel
            //{
            //    Dock = DockStyle.Fill,   
            //    BackColor = Color.White,
            //};
            //this.Controls.Add(contentPanel);
            //this.Controls.SetChildIndex(contentPanel, 0);

            //LoadFormInContent(new Dashboard());
        }

        private void MenuStrip_EnterHandler(object sender, EventArgs e)
        {
            if (sender is MenuStrip menuStrip && menuStrip.Items.Count > 0)
            {
                // Find highlighted item (if any)
                int highlightedIndex = -1;
                for (int i = 0; i < menuStrip.Items.Count; i++)
                {
                    if (menuStrip.Items[i] is ToolStripMenuItem item &&
                        item.BackColor == Color.FromArgb(230, 240, 255))
                    {
                        highlightedIndex = i;
                        break;
                    }
                }

                // If highlighted found, select it; else select first
                currentIndex = highlightedIndex >= 0 ? highlightedIndex : 0;
                ((ToolStripMenuItem)menuStrip.Items[currentIndex]).Select();

                // Give keyboard focus to menuStrip
                menuStrip.Focus();
            }
        }

        private void SetHandCursorForMenuItems(ToolStripItemCollection menuItems)
        {
            foreach (ToolStripItem item in menuItems)
            {
                //item.MouseEnter += (s, e) => this.Cursor = Cursors.Hand;
                //item.MouseLeave += (s, e) => this.Cursor = Cursors.Default;
                item.MouseMove += ToolStripMenuItem_MouseMove;
                item.MouseLeave += ToolStripMenuItem_MouseLeave;

                // If this item has sub-items, apply recursively
                if (item is ToolStripMenuItem mi)
                {
                    mi.DropDownOpened += (s, e) =>
                    {
                        foreach (ToolStripItem sub in mi.DropDownItems)
                        {
                            sub.MouseMove += ToolStripMenuItem_MouseMove;
                            sub.MouseLeave += ToolStripMenuItem_MouseLeave;
                        }
                    };
                }
            }
        }

        private void ToolStripMenuItem_MouseMove(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Hand;
        }

        private void ToolStripMenuItem_MouseLeave(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Default;
        }

        private void Logout_Click(object sender, EventArgs e)
        {
            SessionManager.Clear();

            var loginForm = new Login();
            loginForm.Show();
            this.Close();
        }

        public void LoadFormInContent(Form child, string formKey)
        {
            // Temporarily remove Enter event
            //menuStrip.Enter -= MenuStrip_EnterHandler;

            //// Hide currently active form (minimize behavior)
            //// Hide (minimize) the active form
            //if (activeForm != null)
            //{
            //    activeForm.Hide();

            //    if (!minimizedForms.Contains(activeForm))
            //    {
            //        minimizedForms.Add(activeForm);
            //        AddMinimizedFormToMenu(activeForm);
            //    }
            //}
            //// Check if form already exists
            ////if (openForms.ContainsKey(formKey))
            ////{
            ////    RestoreForm(openForms[formKey]);
            ////    return;
            ////}

            //// New form
            //openForms[formKey] = form;

            //contentPanel.Controls.Clear();
            //form.TopLevel = false;
            //form.FormBorderStyle = FormBorderStyle.None;
            //form.Dock = DockStyle.Fill;
            //form.BackColor = Color.White;

            //contentPanel.Controls.Add(form);
            //form.Show();

            //// Set focus explicitly to first control
            //if (form.Controls.Count > 0)
            //{
            //    //Control firstControl = form.Controls[0];
            //    //if (firstControl.CanSelect)
            //    //    firstControl.Focus();
            //    form.Controls[0].Focus();
            //}

            //activeForm = form;

            //// Re-attach Enter event
            //menuStrip.Enter += MenuStrip_EnterHandler;
            //FocusFirstField(form);

            // If already opened → just show it
            if (openForms.ContainsKey(formKey))
            {
                if (activeChild != null)
                    activeChild.Hide();

                activeChild = openForms[formKey];
                activeChild.Show();
                activeChild.WindowState = FormWindowState.Maximized;
                activeChild.BringToFront();
                return;
            }

            // New child form
            child.MdiParent = this;

            // Remove border/title bar
            child.FormBorderStyle = FormBorderStyle.None;
            child.ControlBox = false;
            child.ShowIcon = false;
            child.Text = "";

            // Fullscreen inside MDI
            child.StartPosition = FormStartPosition.Manual;
            child.WindowState = FormWindowState.Maximized;

            // Match parent’s client area
            child.Dock = DockStyle.Fill;

            // Save
            openForms[formKey] = child;

            // Hide previous
            if (activeChild != null)
                activeChild.Hide();

            activeChild = child;

            // Add to Windows Menu
            AddMinimizedFormToMenu(formKey);

            child.Show();
            child.BringToFront();
        }

        private void AddPOYPacking_Click(object sender, EventArgs e)
        {
            //var parent = this.ParentForm as Dashboard;
            //if (parent != null)
            //{
            //    parent.LoadFormInContent(new POYPackingForm());
            //}
            var dashboard = this.FindForm() as AdminAccount;
            if (dashboard != null)
            {
                var form = new AddPOYPackingForm();
                var formKey = "AddPOYPackingForm";
                form.Tag = "Packing - Add POYPacking";
                dashboard.LoadFormInContent(form, formKey);
                this.Text = form.Tag.ToString();
                //dashboard.OpenForm<AddPOYPackingForm>("Packing - Add POY Packing");
            }
        }

        private void ViewPOYPacking_Click(object sender, EventArgs e)
        {
            var dashboard = this.FindForm() as AdminAccount;
            if (dashboard != null)
            {
                var form = new ViewPOYPackingForm();
                var formKey = "ViewPOYPackingForm";
                form.Tag = "Packing - View POYPacking";
                dashboard.LoadFormInContent(form, formKey);
                this.Text = form.Tag.ToString();
                //dashboard.OpenForm<ViewPOYPackingForm>("Packing - View POYPacking");
            }
        }

        private void ModifyPOYPacking_Click(object sender, EventArgs e)
        {
            var dashboard = this.FindForm() as AdminAccount;
            if (dashboard != null)
            {
                var form = new ModifyPOYPackingForm();
                var formKey = "ViewPOYPackingForm";
                form.Tag = "Packing - Modify POYPacking";
                dashboard.LoadFormInContent(form, formKey);
                this.Text = form.Tag.ToString();
                //dashboard.OpenForm<ModifyPOYPackingForm>("Packing - Modify POYPacking");
            }
        }

        private void DTYPacking_Click(object sender, EventArgs e)
        {
            var dashboard = this.FindForm() as AdminAccount;
            if (dashboard != null)
            {
                var form = new AddDTYPackingForm();
                var formKey = "AddDTYPackingForm";
                form.Tag = "Packing - Add DTYPacking";
                dashboard.LoadFormInContent(form, formKey);
                this.Text = form.Tag.ToString();
                //dashboard.OpenForm<AddDTYPackingForm>("Packing - Add DTYPacking");
            }
        }

        private void ViewDTYPacking_Click(object sender, EventArgs e)
        {
            var dashboard = this.FindForm() as AdminAccount;
            if (dashboard != null)
            {
                var form = new ViewDTYPackingForm();
                var formKey = "ViewDTYPackingForm";
                form.Tag = "Packing - View DTYPacking";
                dashboard.LoadFormInContent(form, formKey);
                this.Text = form.Tag.ToString();
                //dashboard.OpenForm<ViewDTYPackingForm>("Packing - View DTYPacking");
            }
        }

        private void ModifyDTYPacking_Click(object sender, EventArgs e)
        {
            var dashboard = this.FindForm() as AdminAccount;
            if (dashboard != null)
            {
                var form = new ModifyDTYPackingForm();
                var formKey = "ModifyDTYPackingForm";
                form.Tag = "Packing - Modify DTYPacking";
                dashboard.LoadFormInContent(form, formKey);
                this.Text = form.Tag.ToString();
                //dashboard.OpenForm<ModifyDTYPackingForm>("Packing - Modify DTYPacking");
            }
        }

        private void BCFPacking_Click(object sender, EventArgs e)
        {
            var dashboard = this.FindForm() as AdminAccount;
            if (dashboard != null)
            {
                var form = new AddBCFPackingForm();
                var formKey = "AddBCFPackingForm";
                form.Tag = "Packing - Add BCFPacking";
                dashboard.LoadFormInContent(form, formKey);
                this.Text = form.Tag.ToString();
                //dashboard.OpenForm<AddBCFPackingForm>("Packing - Add BCFPacking");
            }
        }

        private void ViewBCFPacking_Click(object sender, EventArgs e)
        {
            var dashboard = this.FindForm() as AdminAccount;
            if (dashboard != null)
            {
                var form = new ViewBCFPackingForm();
                var formKey = "ViewBCFPackingForm";
                form.Tag = "Packing - View BCFPacking";
                dashboard.LoadFormInContent(form, formKey);
                this.Text = form.Tag.ToString();
                //dashboard.OpenForm<ViewBCFPackingForm>("Packing - View BCFPacking");
            }
        }

        private void ModifyBCFPacking_Click(object sender, EventArgs e)
        {
            var dashboard = this.FindForm() as AdminAccount;
            if (dashboard != null)
            {
                var form = new ModifyBCFPackingForm();
                var formKey = "ModifyBCFPackingForm";
                form.Tag = "Packing - Modify BCFPacking";
                dashboard.LoadFormInContent(form, formKey);
                this.Text = form.Tag.ToString();
                //dashboard.OpenForm<ModifyBCFPackingForm>("Packing - Modify BCFPacking");
            }
        }

        private void ChipsPacking_Click(object sender, EventArgs e)
        {
            var dashboard = this.FindForm() as AdminAccount;
            if (dashboard != null)
            {
                var form = new AddChipsPackingForm();
                var formKey = "AddChipsPackingForm";
                form.Tag = "Packing - Add ChipsPacking";
                dashboard.LoadFormInContent(form, formKey);
                this.Text = form.Tag.ToString();
                //dashboard.OpenForm<AddChipsPackingForm>("Packing - Add ChipsPacking");
            }
        }

        private void ViewChipsPacking_Click(object sender, EventArgs e)
        {
            var dashboard = this.FindForm() as AdminAccount;
            if (dashboard != null)
            {
                var form = new ViewChipsPackingForm();
                var formKey = "ViewChipsPackingForm";
                form.Tag = "Packing - View ChipsPacking";
                dashboard.LoadFormInContent(form, formKey);
                this.Text = form.Tag.ToString();
                //dashboard.OpenForm<ViewChipsPackingForm>("Packing - View ChipsPacking");
            }
        }

        private void ModifyChipsPacking_Click(object sender, EventArgs e)
        {
            var dashboard = this.FindForm() as AdminAccount;
            if (dashboard != null)
            {
                var form = new ModifyChipsPackingForm();
                var formKey = "ModifyChipsPackingForm";
                form.Tag = "Packing - Modify ChipsPacking";
                dashboard.LoadFormInContent(form, formKey);
                this.Text = form.Tag.ToString();
                //dashboard.OpenForm<ModifyChipsPackingForm>("Packing - Modify ChipsPacking");
            }
        }

        // highlight selected menu
        private void HighlightMenuItem(object sender)
        {
            foreach (ToolStripMenuItem item in menuStrip.Items)
            {
                item.BackColor = Color.White;
            }

            ToolStripMenuItem clickedItem = sender as ToolStripMenuItem;
            if (clickedItem == null)
                return;

            ToolStripMenuItem topMenu;

            if (clickedItem.OwnerItem is ToolStripMenuItem parentMenu)
            {
                topMenu = parentMenu;
            }
            else
            {
                topMenu = clickedItem;
            }
            topMenu.BackColor = Color.FromArgb(230, 240, 255);
        }

        private void AddMinimizedFormToMenu(string formKey)
        {
            //if (form == null) return;
            //if (windows.DropDownItems.Cast<ToolStripMenuItem>().Any(x => x.Tag == form))
            //    return;
            ////form.Hide();
            ////minimizedForms.Add(form);
            //var item = new ToolStripMenuItem(form.Text);
            //item.Tag = form; // store reference to the form
            //item.Click += MinimizedFormMenu_Click;
            //item.Font = FontManager.GetFont(8, FontStyle.Regular);
            //windows.DropDownItems.Add(item);
            ToolStripMenuItem item = new ToolStripMenuItem(formKey);
            item.Click += (s, e) =>
            {
                Form frm = openForms[formKey];

                if (activeChild != null && activeChild != frm)
                    activeChild.Hide();

                activeChild = frm;
                frm.WindowState = FormWindowState.Maximized;
                frm.Show();
                frm.BringToFront();
            };
            item.Font = FontManager.GetFont(8, FontStyle.Regular);
            windows.DropDownItems.Add(item);
        }

        private void MinimizedFormMenu_Click(object sender, EventArgs e)
        {
            var menuItem = sender as ToolStripMenuItem;
            var form = menuItem?.Tag as Form;
            if (form != null)
            {
                RestoreForm(form);
            }
        }

        private void RestoreForm(Form form)
        {
            // Hide currently active form
            if (activeForm != null && activeForm != form)
                //activeForm.Hide();
                activeForm.WindowState = FormWindowState.Minimized;
            form.WindowState = FormWindowState.Normal;
            form.Activate();

            activeForm = form;
            //if (!contentPanel.Controls.Contains(form))
            //{
            //    form.TopLevel = false;
            //    form.FormBorderStyle = FormBorderStyle.None;
            //    form.Dock = DockStyle.Fill;
            //    contentPanel.Controls.Add(form);
            //}

            //// Show the selected one
            //form.Show();
            //form.BringToFront();
            //form.Focus();
            //if (form.Tag != null)
            //    this.Text = form.Tag.ToString();
            //activeForm = form;

            //FocusFirstField(form);
            // Remove from minimized list and menu
            //minimizedForms.Remove(form);
            //var toRemove = windows.DropDownItems
            //    .Cast<ToolStripMenuItem>()
            //    .FirstOrDefault(x => x.Tag == form);
            //if (toRemove != null)
            //    windows.DropDownItems.Remove(toRemove);
        }

        private void OpenForm<T>(string title) where T : Form, new()
        {
            string formKey = typeof(T).Name;

            // Create or restore form
            var form = openForms.ContainsKey(formKey)
                ? openForms[formKey]
                : new T();

            form.Tag = title; // Store title for restore

            LoadFormInContent(form, formKey);
            this.Text = title;
        }

        private void FocusFirstField(Form form)
        {
            form.BeginInvoke(new Action(() =>
            {
                form.SelectNextControl(null, true, true, true, true);
            }));
        }
    }
}
