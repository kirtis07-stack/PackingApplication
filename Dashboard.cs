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
        public Dashboard()
        {
            InitializeComponent();

            // Add content inside the content panel
            Label dashboardLabel = new Label
            {
                Text = "Dashboard",
                Font = new Font("Microsoft Tai Le", 8, FontStyle.Regular),
                Location = new Point(20, 20)
            };

            this.Controls.Add(dashboardLabel);
        }

        private void POYPacking_Click(object sender, EventArgs e)
        {
            var parent = this.ParentForm as AdminAccount;
            if (parent != null)
            {
                parent.LoadFormInContent(new POYPackingForm());
            }
        }

        private void DTYPacking_Click(object sender, EventArgs e)
        {
            var parent = this.ParentForm as AdminAccount;
            if (parent != null)
            {
                parent.LoadFormInContent(new DTYPackingForm());
            }
        }

        private void BCFPacking_Click(object sender, EventArgs e)
        {
            var parent = this.ParentForm as AdminAccount;
            if (parent != null)
            {
                parent.LoadFormInContent(new BCFPackingForm());
            }
        }

        private void ChipsPacking_Click(object sender, EventArgs e)
        {
            var parent = this.ParentForm as AdminAccount;
            if (parent != null)
            {
                parent.LoadFormInContent(new ChipsPackingForm());
            }
        }
    }
}
