using PackingApplication.Helper;
using PackingApplication.Models.ResponseEntities;
using PackingApplication.Services;
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
    public partial class POYPackingList : Form
    {
        private static Logger Log = Logger.GetLogger();
        PackingService _packingService = new PackingService();
        public POYPackingList()
        {
            InitializeComponent();
            this.Shown += POYPackingList_Shown;
            this.AutoScroll = true;

            SetButtonBorderRadius(this.addnew, 8);
        }

        private void POYPackingList_Load(object sender, EventArgs e)
        {
            // Configure ListView
            listView1.View = View.Details;
            listView1.FullRowSelect = true;
            listView1.GridLines = true;

            // Add columns
            listView1.Columns.Add("SR. No", 50);
            listView1.Columns.Add("PackingType", 100);
            listView1.Columns.Add("Department", 100);
            listView1.Columns.Add("Machine", 100);
            listView1.Columns.Add("LotNo", 100);
            listView1.Columns.Add("BoxNo", 100);
            listView1.Columns.Add("ProductionDate", 100);
            listView1.Columns.Add("Quality", 100);
            listView1.Columns.Add("SaleOrder", 100);
            listView1.Columns.Add("PackSize", 100);
            listView1.Columns.Add("WindingType", 100);
            listView1.Columns.Add("ProdType", 100);
            listView1.Columns.Add("NoOfCopies", 100);
            listView1.Columns.Add("Action", 100);
        }

        private async void POYPackingList_Shown(object sender, EventArgs e)
        {
            var poypackingList = await Task.Run(() => getAllPOYPackingList());

            int index = 1;
            foreach (var item in poypackingList)
            {
                // Add items (rows)
                ListViewItem item1 = new ListViewItem(index.ToString());
                item1.SubItems.Add(item.PackingType);
                item1.SubItems.Add(item.DepartmentName);
                item1.SubItems.Add(item.MachineName);
                item1.SubItems.Add(item.LotNo);
                item1.SubItems.Add(item.BoxNo);
                item1.SubItems.Add(item.ProductionDate.ToString());
                item1.SubItems.Add(item.QualityName);
                item1.SubItems.Add(item.SalesOrderNumber);
                item1.SubItems.Add(item.PackSizeName);
                item1.SubItems.Add(item.WindingTypeName);
                item1.SubItems.Add(item.ProductionType);
                item1.SubItems.Add(item.NoOfCopies.ToString());
                item1.SubItems.Add("Edit");

                item1.Tag = item.ProductionId;

                // Add items to ListView
                listView1.Items.Add(item1);

                index++;               
            }

            listView1.MouseClick += listView1_MouseClick;
        }

        private List<ProductionResponse> getAllPOYPackingList()
        {
            var getPacking = _packingService.getAllPackingListByPackingType("poypacking");
            return getPacking;
        }

        private void SetButtonBorderRadius(System.Windows.Forms.Button button, int radius)
        {
            Log.writeMessage("SetButtonBorderRadius start");
            try
            {
                button.FlatStyle = FlatStyle.Flat;
                button.FlatAppearance.BorderSize = 0;
                button.FlatAppearance.BorderColor = Color.FromArgb(0, 92, 232); // Set to the background color of your form or panel
                button.FlatAppearance.MouseOverBackColor = button.BackColor; // To prevent color change on mouseover
                button.BackColor = Color.FromArgb(0, 92, 232);

                // Set the border radius
                System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
                int diameter = radius * 2;
                path.AddArc(0, 0, diameter, diameter, 180, 95); // Top-left corner
                path.AddArc(button.Width - diameter, 0, diameter, diameter, 270, 95); // Top-right corner
                path.AddArc(button.Width - diameter, button.Height - diameter, diameter, diameter, 0, 95); // Bottom-right corner
                path.AddArc(0, button.Height - diameter, diameter, diameter, 90, 95); // Bottom-left corner
                path.CloseFigure();

                button.Region = new Region(path);
            }
            catch (Exception ex)
            {
                //MessageBox.Show($"An error occurred: {ex.Message}");
                Log.writeMessage($"An error occurred: {ex.Message}");
            }
            Log.writeMessage("SetButtonBorderRadius end");
        }

        private void addNew_Click(object sender, EventArgs e)
        {
            var dashboard = this.ParentForm as Dashboard;
            if (dashboard != null)
            {
                dashboard.LoadFormInContent(new POYPackingForm(0)); // open Add form
            }
        }

        private void listView1_MouseClick(object sender, MouseEventArgs e)
        {
            var info = listView1.HitTest(e.X, e.Y);
            if (info.Item != null && info.SubItem != null)
            {
                int colIndex = info.Item.SubItems.IndexOf(info.SubItem);
                if (colIndex == listView1.Columns.Count - 1) // Action column (Edit)
                {
                    // Get the item you clicked
                    int productionId = Convert.ToInt32(info.Item.Tag);

                    var dashboard = this.ParentForm as Dashboard;
                    if (dashboard != null)
                    {
                        dashboard.LoadFormInContent(new POYPackingForm(productionId)); // open Add form
                    }
                }
            }
        }
    }
}
