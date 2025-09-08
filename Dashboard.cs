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
    public partial class Dashboard: Form
    {
        protected Panel contentPanel;
        public Dashboard()
        {
            InitializeComponent();

            // Add content inside the content panel
            //Label dashboardLabel = new Label
            //{
            //    Text = "Dashboard",
            //    Font = new Font("Microsoft Tai Le", 8, FontStyle.Regular),
            //    Location = new Point(20, 20)
            //};

            //this.Controls.Add(dashboardLabel);
            // CONTENT PANEL (sticky between header & footer)
            contentPanel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White
            };
            this.Controls.Add(contentPanel);
            this.Controls.SetChildIndex(contentPanel, 0);

            //LoadFormInContent(new Dashboard());
        }

        private void DashboardForm_Load(object sender, EventArgs e)
        {
            MenuStrip menuStrip = new MenuStrip();

            // POY Menu
            ToolStripMenuItem poy = new ToolStripMenuItem("POYPacking");
            poy.DropDownItems.Add("POYPacking", null, POYPacking_Click);

            // DTY Menu
            ToolStripMenuItem dty = new ToolStripMenuItem("DTYPacking");
            dty.DropDownItems.Add("DTYPacking", null, DTYPacking_Click);

            // BCF Menu
            ToolStripMenuItem bcf = new ToolStripMenuItem("BCFPacking");
            bcf.DropDownItems.Add("BCFPacking", null, BCFPacking_Click);

            // Chips Menu
            ToolStripMenuItem chips = new ToolStripMenuItem("ChipsPacking");
            chips.DropDownItems.Add("ChipsPacking", null, ChipsPacking_Click);

            // Add to menuStrip
            menuStrip.Items.Add(poy);
            menuStrip.Items.Add(dty);
            menuStrip.Items.Add(bcf);
            menuStrip.Items.Add(chips);

            // Add menuStrip to form
            this.MainMenuStrip = menuStrip;
            this.Controls.Add(menuStrip);
        }

        private void POYPacking_Click(object sender, EventArgs e)
        {
            //var parent = this.ParentForm as Dashboard;
            //if (parent != null)
            //{
            //    parent.LoadFormInContent(new POYPackingForm());
            //}
            var dashboard = this.FindForm() as Dashboard;
            if (dashboard != null)
            {
                dashboard.LoadFormInContent(new POYPackingList());
            }
        }

        private void DTYPacking_Click(object sender, EventArgs e)
        {
            //var parent = this.ParentForm as Dashboard;
            //if (parent != null)
            //{
            //    parent.LoadFormInContent(new DTYPackingForm());
            //}
            var dashboard = this.FindForm() as Dashboard;
            if (dashboard != null)
            {
                dashboard.LoadFormInContent(new DTYPackingList());
            }
        }

        private void BCFPacking_Click(object sender, EventArgs e)
        {
            //var parent = this.ParentForm as Dashboard;
            //if (parent != null)
            //{
            //    parent.LoadFormInContent(new BCFPackingForm());
            //}
            var dashboard = this.FindForm() as Dashboard;
            if (dashboard != null)
            {
                dashboard.LoadFormInContent(new BCFPackingList());
            }
        }

        private void ChipsPacking_Click(object sender, EventArgs e)
        {
            //var parent = this.ParentForm as Dashboard;
            //if (parent != null)
            //{
            //    parent.LoadFormInContent(new ChipsPackingForm());
            //}
            var dashboard = this.FindForm() as Dashboard;
            if (dashboard != null)
            {
                dashboard.LoadFormInContent(new ChipsPackingList());
            }
        }

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
