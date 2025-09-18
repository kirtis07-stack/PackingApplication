using PackingApplication.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Runtime.InteropServices;
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
            //contentPanel = new Panel
            //{
            //    Dock = DockStyle.Fill,
            //    BackColor = Color.White
            //};
            //this.Controls.Add(contentPanel);
            //this.Controls.SetChildIndex(contentPanel, 0);

            //LoadFormInContent(new Dashboard());
        }

        private void DashboardForm_Load(object sender, EventArgs e)
        {
            //MenuStrip menuStrip = new MenuStrip();
            //menuStrip.BackColor = Color.White;
            //menuStrip.Padding = new Padding(10, 10, 0, 0);

            //// POY Menu
            //ToolStripMenuItem poy = new ToolStripMenuItem("POYPacking")
            //{
            //    Font = FontManager.GetFont(8, FontStyle.Bold),
            //    BackColor = Color.White
            //};
            //ToolStripMenuItem poysubItem = new ToolStripMenuItem("POYPacking", null, POYPacking_Click)
            //{
            //    Font = FontManager.GetFont(8, FontStyle.Regular)
            //};
            //poy.DropDownItems.Add(poysubItem);

            //// DTY Menu
            //ToolStripMenuItem dty = new ToolStripMenuItem("DTYPacking")
            //{
            //    Font = FontManager.GetFont(8, FontStyle.Bold),
            //    BackColor = Color.White
            //};
            //ToolStripMenuItem dtysubItem = new ToolStripMenuItem("DTYPacking", null, DTYPacking_Click)
            //{
            //    Font = FontManager.GetFont(8, FontStyle.Regular)
            //};
            //dty.DropDownItems.Add(dtysubItem);

            //// BCF Menu
            //ToolStripMenuItem bcf = new ToolStripMenuItem("BCFPacking")
            //{
            //    Font = FontManager.GetFont(8, FontStyle.Bold),
            //    BackColor = Color.White
            //};
            //ToolStripMenuItem bcfsubItem = new ToolStripMenuItem("BCFPacking", null, BCFPacking_Click)
            //{
            //    Font = FontManager.GetFont(8, FontStyle.Regular)
            //};
            //bcf.DropDownItems.Add(bcfsubItem);

            //// Chips Menu
            //ToolStripMenuItem chips = new ToolStripMenuItem("ChipsPacking")
            //{
            //    Font = FontManager.GetFont(8, FontStyle.Bold),
            //    BackColor = Color.White
            //};
            //ToolStripMenuItem chipssubItem = new ToolStripMenuItem("ChipsPacking", null, ChipsPacking_Click)
            //{
            //    Font = FontManager.GetFont(8, FontStyle.Regular)
            //};
            //chips.DropDownItems.Add(chipssubItem);

            //// Add to menuStrip
            //menuStrip.Items.Add(poy);
            //menuStrip.Items.Add(dty);
            //menuStrip.Items.Add(bcf);
            //menuStrip.Items.Add(chips);

            //// Add menuStrip to form
            //this.MainMenuStrip = menuStrip;
            //this.Controls.Add(menuStrip);
        }

        //private void POYPacking_Click(object sender, EventArgs e)
        //{
        //    //var parent = this.ParentForm as Dashboard;
        //    //if (parent != null)
        //    //{
        //    //    parent.LoadFormInContent(new POYPackingForm());
        //    //}
        //    var dashboard = this.FindForm() as Dashboard;
        //    if (dashboard != null)
        //    {
        //        dashboard.LoadFormInContent(new POYPackingList());
        //    }
        //}

        //private void DTYPacking_Click(object sender, EventArgs e)
        //{
        //    //var parent = this.ParentForm as Dashboard;
        //    //if (parent != null)
        //    //{
        //    //    parent.LoadFormInContent(new DTYPackingForm());
        //    //}
        //    var dashboard = this.FindForm() as Dashboard;
        //    if (dashboard != null)
        //    {
        //        dashboard.LoadFormInContent(new DTYPackingList());
        //    }
        //}

        //private void BCFPacking_Click(object sender, EventArgs e)
        //{
        //    //var parent = this.ParentForm as Dashboard;
        //    //if (parent != null)
        //    //{
        //    //    parent.LoadFormInContent(new BCFPackingForm());
        //    //}
        //    var dashboard = this.FindForm() as Dashboard;
        //    if (dashboard != null)
        //    {
        //        dashboard.LoadFormInContent(new BCFPackingList());
        //    }
        //}

        //private void ChipsPacking_Click(object sender, EventArgs e)
        //{
        //    //var parent = this.ParentForm as Dashboard;
        //    //if (parent != null)
        //    //{
        //    //    parent.LoadFormInContent(new ChipsPackingForm());
        //    //}
        //    var dashboard = this.FindForm() as Dashboard;
        //    if (dashboard != null)
        //    {
        //        dashboard.LoadFormInContent(new ChipsPackingList());
        //    }
        //}

        //public void LoadFormInContent(Form form)
        //{
        //    contentPanel.Controls.Clear();
        //    form.TopLevel = false;
        //    form.FormBorderStyle = FormBorderStyle.None;
        //    form.Dock = DockStyle.Fill;

        //    contentPanel.Controls.Add(form);
        //    form.Show();
        //}

        //public class StylishMenuRenderer : ToolStripProfessionalRenderer
        //{
        //    public StylishMenuRenderer() : base(new StylishColorTable()) { }
        //}

        //public class StylishColorTable : ProfessionalColorTable
        //{
        //    public override Color MenuItemSelected => Color.FromArgb(255, 255, 255); // hover background
        //    public override Color MenuItemBorder => Color.FromArgb(255, 255, 255);   // border on hover
        //    public override Color MenuItemPressedGradientBegin => Color.FromArgb(255, 255, 255);
        //    public override Color MenuItemPressedGradientEnd => Color.FromArgb(255, 255, 255);
        //    public override Color ToolStripGradientBegin => Color.White;        // menu background
        //    public override Color ToolStripGradientEnd => Color.White;
        //}

    }
}
