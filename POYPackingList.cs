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

            SetButtonBorderRadius(this.addnew, 5);
        }

        private void POYPackingList_Load(object sender, EventArgs e)
        {
            //dataGridView1.Columns.Clear();
            //dataGridView1.Columns.Add("SrNo", "SR. No");
            //dataGridView1.Columns.Add("PackingType", "Packing Type");
            //dataGridView1.Columns.Add("Department", "Department");
            //dataGridView1.Columns.Add("Machine", "Machine");
            //dataGridView1.Columns.Add("LotNo", "Lot No");
            //dataGridView1.Columns.Add("BoxNo", "Box No");
            //dataGridView1.Columns.Add("ProductionDate", "Production Date");
            //dataGridView1.Columns.Add("Quality", "Quality");
            //dataGridView1.Columns.Add("SaleOrder", "Sale Order");
            //dataGridView1.Columns.Add("PackSize", "Pack Size");
            //dataGridView1.Columns.Add("WindingType", "Winding Type");
            //dataGridView1.Columns.Add("ProdType", "Prod Type");
            //dataGridView1.Columns.Add("NoOfCopies", "No Of Copies");

            //// Add Edit button column
            //DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
            //btn.HeaderText = "Action";
            //btn.Text = "Edit";
            //btn.UseColumnTextForButtonValue = true;
            //dataGridView1.Columns.Add(btn);
        }

        private async void POYPackingList_Shown(object sender, EventArgs e)
        {
            var poypackingList = await Task.Run(() => getAllPOYPackingList());

            dataGridView1.Columns.Clear();
            // Define columns
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { Name = "SrNo", HeaderText = "SR. No" });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { Name = "PackingType", DataPropertyName = "PackingType", HeaderText = "Packing Type" });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { Name = "DepartmentName", DataPropertyName = "DepartmentName", HeaderText = "Department" });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { Name = "MachineName", DataPropertyName = "MachineName", HeaderText = "Machine" });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { Name = "LotNo", DataPropertyName = "LotNo", HeaderText = "Lot No" });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { Name = "BoxNo", DataPropertyName = "BoxNo", HeaderText = "Box No" });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { Name = "ProductionDate", DataPropertyName = "ProductionDate", HeaderText = "Production Date" });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { Name = "QualityName", DataPropertyName = "QualityName", HeaderText = "Quality" });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { Name = "SalesOrderNumber", DataPropertyName = "SalesOrderNumber", HeaderText = "Sales Order" });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { Name = "PackSizeName", DataPropertyName = "PackSizeName", HeaderText = "Pack Size" });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { Name = "WindingTypeName", DataPropertyName = "WindingTypeName", HeaderText = "Winding Type" });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { Name = "ProductionType", DataPropertyName = "ProductionType", HeaderText = "Production Type" });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { Name = "NoOfCopies", DataPropertyName = "NoOfCopies", HeaderText = "Copies" });

            //dataGridView1.Columns["SrNo"].Width = 50;
            //dataGridView1.Columns["PackingType"].Width = 100;
            //dataGridView1.Columns["ProductionDate"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            // Add Edit button column
            DataGridViewImageColumn btn = new DataGridViewImageColumn();
            btn.HeaderText = "Action";
            btn.Name = "Action";
            btn.Image = Properties.Resources.icons8_edit_48;
            btn.ImageLayout = DataGridViewImageCellLayout.Zoom;
            btn.Width = 40;  // column width
            dataGridView1.RowTemplate.Height = 40; // row height
            dataGridView1.Columns.Add(btn);

            // Bind your list
            dataGridView1.DataSource = poypackingList;

            dataGridView1.CellContentClick += dataGridView1_CellContentClick;
            dataGridView1.RowPostPaint += dataGridView1_RowPostPaint;
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

        private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            dataGridView1.Rows[e.RowIndex].Cells["SrNo"].Value = (e.RowIndex + 1).ToString();
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == dataGridView1.Columns["Action"].Index)
            {

                long productionId = Convert.ToInt32(
                    ((ProductionResponse)dataGridView1.Rows[e.RowIndex].DataBoundItem).ProductionId
                );

                var dashboard = this.ParentForm as Dashboard;
                    if (dashboard != null)
                    {
                        dashboard.LoadFormInContent(new POYPackingForm(productionId)); // open edit form
                    }
            }
        }
    }
}
