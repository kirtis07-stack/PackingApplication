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
        private static Logger Log = Logger.GetLogger();

        // Windows menu
        ToolStripMenuItem windows = new ToolStripMenuItem("Windows")
        {
            Font = FontManager.GetFont(9, FontStyle.Bold),
            BackColor = Color.White
        };
        public AdminAccount()
        {
            Log.writeMessage("AdminAccount - Start : " + DateTime.Now);

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

            PictureBox logoPictureBox = new PictureBox
            {
                Image = Properties.Resources.logo,
                SizeMode = PictureBoxSizeMode.Normal,
                Size = new Size(170, 50),
                Location = new System.Drawing.Point(10, 10)
            };
            leftPanel.Controls.Add(logoPictureBox);

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
            ToolStripMenuItem deletepoy = new ToolStripMenuItem("Delete POY Packing", null, DeletePOYPacking_Click)
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
            ToolStripMenuItem deletedty = new ToolStripMenuItem("Delete DTY Packing", null, DeleteDTYPacking_Click)
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
            ToolStripMenuItem deletebcf = new ToolStripMenuItem("Delete BCF Packing", null, DeleteBCFPacking_Click)
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
            ToolStripMenuItem deletechips = new ToolStripMenuItem("Delete Chips Packing", null, DeleteChipsPacking_Click)
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
            windows.DropDownOpened += Windows_DropDownOpened;
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
            if (this.ActiveMdiChild != null)
            {
                this.Text = this.ActiveMdiChild.Text;
            }
            menuStrip.MdiWindowListItem = windows;
            foreach (Control ctl in this.Controls)
            {
                if (ctl is MdiClient)
                {
                    ctl.BackColor = Color.White;
                    break;
                }
            }

            Log.writeMessage("AdminAccount - End : " + DateTime.Now);
        }

        private void MenuStrip_EnterHandler(object sender, EventArgs e)
        {
            Log.writeMessage("AdminAccount MenuStrip_EnterHandler - Start : " + DateTime.Now);

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

            Log.writeMessage("AdminAccount MenuStrip_EnterHandler - End : " + DateTime.Now);
        }

        private void SetHandCursorForMenuItems(ToolStripItemCollection menuItems)
        {
            Log.writeMessage("AdminAccount SetHandCursorForMenuItems - Start : " + DateTime.Now);

            foreach (ToolStripItem item in menuItems)
            {
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

            Log.writeMessage("AdminAccount SetHandCursorForMenuItems - End : " + DateTime.Now);
        }

        private void ToolStripMenuItem_MouseMove(object sender, EventArgs e)
        {
            Log.writeMessage("AdminAccount ToolStripMenuItem_MouseMove - Start : " + DateTime.Now);

            this.Cursor = Cursors.Hand;

            Log.writeMessage("AdminAccount ToolStripMenuItem_MouseMove - End : " + DateTime.Now);
        }

        private void ToolStripMenuItem_MouseLeave(object sender, EventArgs e)
        {
            Log.writeMessage("AdminAccount ToolStripMenuItem_MouseLeave - Start : " + DateTime.Now);

            this.Cursor = Cursors.Default;

            Log.writeMessage("AdminAccount ToolStripMenuItem_MouseLeave - End : " + DateTime.Now);
        }

        private void Logout_Click(object sender, EventArgs e)
        {
            Log.writeMessage("AdminAccount Logout_Click - Start : " + DateTime.Now);

            SessionManager.Clear();

            var loginForm = new Login();
            loginForm.Show();
            this.Close();

            Log.writeMessage("AdminAccount Logout_Click - End : " + DateTime.Now);
        }

        public void LoadFormInContent(Form child, string formKey)
        {
            Log.writeMessage("AdminAccount LoadFormInContent - Start : " + DateTime.Now);

            // If already opened → just show it
            //if (openForms.ContainsKey(formKey))
            //{
            //    if (activeChild != null)
            //        activeChild.Hide();

            //    activeChild = openForms[formKey];
            //    activeChild.Show();
            //    activeChild.WindowState = FormWindowState.Maximized;
            //    activeChild.BringToFront();
            //    return;
            //}

            //// New child form
            //child.MdiParent = this;

            //// Remove border/title bar
            ////child.FormBorderStyle = FormBorderStyle.None;
            //child.ControlBox = false;
            ////child.ShowIcon = false;
            ////child.Text = "";

            //// Fullscreen inside MDI
            //child.StartPosition = FormStartPosition.Manual;
            //child.WindowState = FormWindowState.Maximized;

            //// Match parent’s client area
            //child.Dock = DockStyle.Fill;
            //child.FormClosed += Child_FormClosed;
            //// Save
            //openForms[formKey] = child;

            //// Hide previous
            //if (activeChild != null)
            //    activeChild.Hide();

            //activeChild = child;

            //// Add to Windows Menu
            //AddMinimizedFormToMenu(formKey);

            //child.Show();
            //child.BringToFront();
            // If already opened, activate it
            if (openForms.ContainsKey(formKey))
            {
                Form existing = openForms[formKey];

                if (activeChild != null && activeChild != existing)
                {
                    //activeChild.Hide();
                    activeChild.Close();   // removes from Window menu correctly
                }

                activeChild = existing;
                existing.WindowState = FormWindowState.Normal;
                existing.Show();
                existing.BringToFront();
                return;
            }

            // New MDI child setup
            child.MdiParent = this;

            // IMPORTANT for Option A (MDI should manage menu)
            //child.AllowMerge = true;

            // Remove window chrome so child looks like page
            //child.FormBorderStyle = FormBorderStyle.None;
            child.ControlBox = true;
            //child.MinimizeBox = false;
            //child.MaximizeBox = false;
            //child.ShowIcon = false;
            //child.Text = ""; // Prevent name from showing in menu if unwanted

            // Fill parent
            child.Dock = DockStyle.Fill;
            child.WindowState = FormWindowState.Normal;

            // Track in dictionary
            openForms[formKey] = child;

            // Track active
            //if (activeChild != null)
            //    activeChild.Hide();
            activeChild = child;
            activeChild.WindowState = FormWindowState.Normal;
            // Handle removal on close
            child.FormClosed += Child_FormClosed;

            child.Show();
            child.BringToFront();

            Log.writeMessage("AdminAccount LoadFormInContent - End : " + DateTime.Now);
        }

        private void AddPOYPacking_Click(object sender, EventArgs e)
        {
            Log.writeMessage("AdminAccount AddPOYPacking_Click - Start : " + DateTime.Now);

            var dashboard = this.FindForm() as AdminAccount;
            if (dashboard != null)
            {
                var form = new AddPOYPackingForm();
                var formKey = "AddPOYPackingForm";
                form.Tag = "Packing - Add POYPacking";
                dashboard.LoadFormInContent(form, formKey);
                //this.Text = form.Tag.ToString();
            }

            Log.writeMessage("AdminAccount AddPOYPacking_Click - End : " + DateTime.Now);
        }

        private void ViewPOYPacking_Click(object sender, EventArgs e)
        {
            Log.writeMessage("AdminAccount ViewPOYPacking_Click - Start : " + DateTime.Now);

            var dashboard = this.FindForm() as AdminAccount;
            if (dashboard != null)
            {
                var form = new ViewPOYPackingForm();
                var formKey = "ViewPOYPackingForm";
                form.Tag = "Packing - View POYPacking";
                dashboard.LoadFormInContent(form, formKey);
                //this.Text = form.Tag.ToString();
            }

            Log.writeMessage("AdminAccount ViewPOYPacking_Click - End : " + DateTime.Now);
        }

        private void ModifyPOYPacking_Click(object sender, EventArgs e)
        {
            Log.writeMessage("AdminAccount ModifyPOYPacking_Click - Start : " + DateTime.Now);

            var dashboard = this.FindForm() as AdminAccount;
            if (dashboard != null)
            {
                var form = new ModifyPOYPackingForm();
                var formKey = "ModifyPOYPackingForm";
                form.Tag = "Packing - Modify POYPacking";
                dashboard.LoadFormInContent(form, formKey);
                //this.Text = form.Tag.ToString();
            }

            Log.writeMessage("AdminAccount ModifyPOYPacking_Click - End : " + DateTime.Now);
        }

        private void DeletePOYPacking_Click(object sender, EventArgs e)
        {
            Log.writeMessage("AdminAccount DeletePOYPacking_Click - Start : " + DateTime.Now);

            var dashboard = this.FindForm() as AdminAccount;
            if (dashboard != null)
            {
                var form = new DeletePOYPackingForm();
                var formKey = "DeletePOYPackingForm";
                form.Tag = "Packing - Delete POYPacking";
                dashboard.LoadFormInContent(form, formKey);
                //this.Text = form.Tag.ToString();
            }

            Log.writeMessage("AdminAccount DeletePOYPacking_Click - End : " + DateTime.Now);
        }

        private void DTYPacking_Click(object sender, EventArgs e)
        {
            Log.writeMessage("AdminAccount DTYPacking_Click - Start : " + DateTime.Now);

            var dashboard = this.FindForm() as AdminAccount;
            if (dashboard != null)
            {
                var form = new AddDTYPackingForm();
                var formKey = "AddDTYPackingForm";
                form.Tag = "Packing - Add DTYPacking";
                dashboard.LoadFormInContent(form, formKey);
                //this.Text = form.Tag.ToString();
            }

            Log.writeMessage("AdminAccount DTYPacking_Click - End : " + DateTime.Now);
        }

        private void ViewDTYPacking_Click(object sender, EventArgs e)
        {
            Log.writeMessage("AdminAccount ViewDTYPacking_Click - Start : " + DateTime.Now);

            var dashboard = this.FindForm() as AdminAccount;
            if (dashboard != null)
            {
                var form = new ViewDTYPackingForm();
                var formKey = "ViewDTYPackingForm";
                form.Tag = "Packing - View DTYPacking";
                dashboard.LoadFormInContent(form, formKey);
                //this.Text = form.Tag.ToString();
            }

            Log.writeMessage("AdminAccount ViewDTYPacking_Click - End : " + DateTime.Now);
        }

        private void ModifyDTYPacking_Click(object sender, EventArgs e)
        {
            Log.writeMessage("AdminAccount ModifyDTYPacking_Click - Start : " + DateTime.Now);

            var dashboard = this.FindForm() as AdminAccount;
            if (dashboard != null)
            {
                var form = new ModifyDTYPackingForm();
                var formKey = "ModifyDTYPackingForm";
                form.Tag = "Packing - Modify DTYPacking";
                dashboard.LoadFormInContent(form, formKey);
                //this.Text = form.Tag.ToString();
            }

            Log.writeMessage("AdminAccount ModifyDTYPacking_Click - End : " + DateTime.Now);
        }

        private void DeleteDTYPacking_Click(object sender, EventArgs e)
        {
            Log.writeMessage("AdminAccount DeleteDTYPacking_Click - Start : " + DateTime.Now);

            var dashboard = this.FindForm() as AdminAccount;
            if (dashboard != null)
            {
                var form = new DeleteDTYPackingForm();
                var formKey = "DeleteDTYPackingForm";
                form.Tag = "Packing - Delete DTYPacking";
                dashboard.LoadFormInContent(form, formKey);
                //this.Text = form.Tag.ToString();
            }

            Log.writeMessage("AdminAccount DeleteDTYPacking_Click - End : " + DateTime.Now);
        }

        private void BCFPacking_Click(object sender, EventArgs e)
        {
            Log.writeMessage("AdminAccount BCFPacking_Click - Start : " + DateTime.Now);

            var dashboard = this.FindForm() as AdminAccount;
            if (dashboard != null)
            {
                var form = new AddBCFPackingForm();
                var formKey = "AddBCFPackingForm";
                form.Tag = "Packing - Add BCFPacking";
                dashboard.LoadFormInContent(form, formKey);
                //this.Text = form.Tag.ToString();
            }

            Log.writeMessage("AdminAccount BCFPacking_Click - End : " + DateTime.Now);
        }

        private void ViewBCFPacking_Click(object sender, EventArgs e)
        {
            Log.writeMessage("AdminAccount ViewBCFPacking_Click - Start : " + DateTime.Now);

            var dashboard = this.FindForm() as AdminAccount;
            if (dashboard != null)
            {
                var form = new ViewBCFPackingForm();
                var formKey = "ViewBCFPackingForm";
                form.Tag = "Packing - View BCFPacking";
                dashboard.LoadFormInContent(form, formKey);
                //this.Text = form.Tag.ToString();
            }

            Log.writeMessage("AdminAccount ViewBCFPacking_Click - End : " + DateTime.Now);
        }

        private void ModifyBCFPacking_Click(object sender, EventArgs e)
        {
            Log.writeMessage("AdminAccount ModifyBCFPacking_Click - Start : " + DateTime.Now);

            var dashboard = this.FindForm() as AdminAccount;
            if (dashboard != null)
            {
                var form = new ModifyBCFPackingForm();
                var formKey = "ModifyBCFPackingForm";
                form.Tag = "Packing - Modify BCFPacking";
                dashboard.LoadFormInContent(form, formKey);
                //this.Text = form.Tag.ToString();
            }

            Log.writeMessage("AdminAccount ModifyBCFPacking_Click - End : " + DateTime.Now);
        }

        private void DeleteBCFPacking_Click(object sender, EventArgs e)
        {
            Log.writeMessage("AdminAccount DeleteBCFPacking_Click - Start : " + DateTime.Now);

            var dashboard = this.FindForm() as AdminAccount;
            if (dashboard != null)
            {
                var form = new DeleteBCFPackingForm();
                var formKey = "DeleteBCFPackingForm";
                form.Tag = "Packing - Delete BCFPacking";
                dashboard.LoadFormInContent(form, formKey);
                //this.Text = form.Tag.ToString();
            }

            Log.writeMessage("AdminAccount DeleteBCFPacking_Click - End : " + DateTime.Now);
        }

        private void ChipsPacking_Click(object sender, EventArgs e)
        {
            Log.writeMessage("AdminAccount ChipsPacking_Click - Start : " + DateTime.Now);

            var dashboard = this.FindForm() as AdminAccount;
            if (dashboard != null)
            {
                var form = new AddChipsPackingForm();
                var formKey = "AddChipsPackingForm";
                form.Tag = "Packing - Add ChipsPacking";
                dashboard.LoadFormInContent(form, formKey);
                //this.Text = form.Tag.ToString();
            }

            Log.writeMessage("AdminAccount ChipsPacking_Click - End : " + DateTime.Now);
        }

        private void ViewChipsPacking_Click(object sender, EventArgs e)
        {
            Log.writeMessage("AdminAccount ViewChipsPacking_Click - Start : " + DateTime.Now);

            var dashboard = this.FindForm() as AdminAccount;
            if (dashboard != null)
            {
                var form = new ViewChipsPackingForm();
                var formKey = "ViewChipsPackingForm";
                form.Tag = "Packing - View ChipsPacking";
                dashboard.LoadFormInContent(form, formKey);
                //this.Text = form.Tag.ToString();
            }

            Log.writeMessage("AdminAccount ViewChipsPacking_Click - End : " + DateTime.Now);
        }

        private void ModifyChipsPacking_Click(object sender, EventArgs e)
        {
            Log.writeMessage("AdminAccount ModifyChipsPacking_Click - Start : " + DateTime.Now);

            var dashboard = this.FindForm() as AdminAccount;
            if (dashboard != null)
            {
                var form = new ModifyChipsPackingForm();
                var formKey = "ModifyChipsPackingForm";
                form.Tag = "Packing - Modify ChipsPacking";
                dashboard.LoadFormInContent(form, formKey);
                //this.Text = form.Tag.ToString();
            }

            Log.writeMessage("AdminAccount ModifyChipsPacking_Click - End : " + DateTime.Now);
        }

        private void DeleteChipsPacking_Click(object sender, EventArgs e)
        {
            Log.writeMessage("AdminAccount DeleteChipsPacking_Click - Start : " + DateTime.Now);

            var dashboard = this.FindForm() as AdminAccount;
            if (dashboard != null)
            {
                var form = new DeleteChipsPackingForm();
                var formKey = "DeleteChipsPackingForm";
                form.Tag = "Packing - Delete ChipsPacking";
                dashboard.LoadFormInContent(form, formKey);
                //this.Text = form.Tag.ToString();
            }

            Log.writeMessage("AdminAccount DeleteChipsPacking_Click - End : " + DateTime.Now);
        }

        // highlight selected menu
        private void HighlightMenuItem(object sender)
        {
            Log.writeMessage("AdminAccount HighlightMenuItem - Start : " + DateTime.Now);

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

            Log.writeMessage("AdminAccount HighlightMenuItem - End : " + DateTime.Now);
        }

        private void AddMinimizedFormToMenu(string formKey)
        {
            Log.writeMessage("AdminAccount AddMinimizedFormToMenu - Start : " + DateTime.Now);

            ToolStripMenuItem item = new ToolStripMenuItem(formKey);
            item.Click += (s, e) =>
            {
                Form frm = openForms[formKey];

                if (activeChild != null && activeChild != frm)
                    activeChild.Hide();

                activeChild = frm;
                frm.WindowState = FormWindowState.Maximized;
                this.Text = frm.Tag.ToString();
                frm.Show();
                frm.BringToFront();
            };
            item.Font = FontManager.GetFont(8, FontStyle.Regular);
            windows.DropDownItems.Add(item);

            Log.writeMessage("AdminAccount AddMinimizedFormToMenu - End : " + DateTime.Now);
        }

        private void MinimizedFormMenu_Click(object sender, EventArgs e)
        {
            Log.writeMessage("AdminAccount MinimizedFormMenu_Click - Start : " + DateTime.Now);

            var menuItem = sender as ToolStripMenuItem;
            var form = menuItem?.Tag as Form;
            if (form != null)
            {
                RestoreForm(form);
            }

            Log.writeMessage("AdminAccount MinimizedFormMenu_Click - End : " + DateTime.Now);
        }

        private void RestoreForm(Form form)
        {
            Log.writeMessage("AdminAccount RestoreForm - Start : " + DateTime.Now);

            // Hide currently active form
            if (activeForm != null && activeForm != form)
                activeForm.WindowState = FormWindowState.Minimized;
            form.WindowState = FormWindowState.Normal;
            form.Activate();

            activeForm = form;

            Log.writeMessage("AdminAccount RestoreForm - End : " + DateTime.Now);
        }

        private void OpenForm<T>(string title) where T : Form, new()
        {
            Log.writeMessage("AdminAccount OpenForm - Start : " + DateTime.Now);

            string formKey = typeof(T).Name;

            // Create or restore form
            var form = openForms.ContainsKey(formKey)
                ? openForms[formKey]
                : new T();

            form.Tag = title; // Store title for restore

            LoadFormInContent(form, formKey);
            this.Text = title;

            Log.writeMessage("AdminAccount OpenForm - End : " + DateTime.Now);
        }

        private void FocusFirstField(Form form)
        {
            Log.writeMessage("AdminAccount FocusFirstField - Start : " + DateTime.Now);

            form.BeginInvoke(new Action(() =>
            {
                form.SelectNextControl(null, true, true, true, true);
            }));

            Log.writeMessage("AdminAccount FocusFirstField - End : " + DateTime.Now);
        }

        private void Child_FormClosed(object sender, FormClosedEventArgs e)
        {
            Log.writeMessage("AdminAccount Child_FormClosed - Start : " + DateTime.Now);

            Form closedForm = sender as Form;
            if (closedForm == null) return;

            string keyToRemove = null;

            // Find the matching key
            foreach (var pair in openForms)
            {
                if (pair.Value == closedForm)
                {
                    keyToRemove = pair.Key;
                    break;
                }
            }

            // Remove from dictionary
            if (keyToRemove != null)
                openForms.Remove(keyToRemove);

            // Remove from Windows menu
            foreach (ToolStripMenuItem item in windows.DropDownItems)
            {
                if (item.Text == closedForm.Text)
                {
                    windows.DropDownItems.Remove(item);
                    break;
                }
            }

            // If the active form was closed → clear activeChild
            if (activeChild == closedForm)
                activeChild = null;

            Log.writeMessage("AdminAccount Child_FormClosed - End : " + DateTime.Now);
        }

        private void Windows_DropDownOpened(object sender, EventArgs e)
        {
            Log.writeMessage("AdminAccount Windows_DropDownOpened - Start : " + DateTime.Now);

            foreach (ToolStripItem item in windows.DropDownItems)
            {
                item.Font = FontManager.GetFont(8, FontStyle.Regular);
            }

            Log.writeMessage("AdminAccount Windows_DropDownOpened - End : " + DateTime.Now);
        }

    }
}
