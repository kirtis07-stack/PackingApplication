using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PackingApplication
{
    public partial class Sidebar : Form
    {
        bool sidebarExpand;
        public Sidebar()
        {
            InitializeComponent();
            this.sidebarContainer.BringToFront();
        }

        public void sidebarTimer_Tick(object sender, EventArgs e)
        {
            if (sidebarExpand) {

                this.sidebarContainer.Width -= 10;
                //this.mainpage.Width += 10;
                if (sidebarContainer.Width == sidebarContainer.MinimumSize.Width) { 
                    sidebarExpand = false;
                    //mainpage.Width = mainpage.MaximumSize.Width;
                    //mainpage.Location = new Point(sidebarContainer.MinimumSize.Width, 0);
                    sidebarTimer.Stop();
                }
            } else
            {
                this.sidebarContainer.Width += 10;
                //this.mainpage.Width -= 10;
                if (sidebarContainer.Width == sidebarContainer.MaximumSize.Width)
                {
                    //mainpage.Width = mainpage.MinimumSize.Width;
                    //mainpage.Location = new Point(sidebarContainer.MaximumSize.Width, 0);
                    sidebarExpand = true;
                    sidebarTimer.Stop();
                }
            }
        }

        private void menuBtn_Click(object sender, EventArgs e)
        {
            sidebarTimer.Start();
        }

        private void btnAddPOY_Click(object sender, EventArgs e)
        {
            LoadFormInMainPage(new POYPackingForm(0));
        }

        private void LoadFormInMainPage(Form form)
        {
            // Clear existing controls
            panel7.Controls.Clear();

            // Make the child form behave like a control
            form.TopLevel = false;
            form.FormBorderStyle = FormBorderStyle.None;
            form.Dock = DockStyle.Fill;

            // Add to mainpage panel
            panel7.Controls.Add(form);
            form.Show();
        }
    }
}
