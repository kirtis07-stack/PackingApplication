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
        PackingService _packingService = new PackingService();
        public POYPackingList()
        {
            InitializeComponent();
            this.Shown += POYPackingList_Shown;
            this.AutoScroll = true;
        }

        private void POYPackingList_Load(object sender, EventArgs e)
        {
        }

        private async void POYPackingList_Shown(object sender, EventArgs e)
        {
            var poypackingList = await Task.Run(() => getAllPOYPackingList());

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

            foreach (var item in poypackingList)
            {
                // Add items (rows)
                ListViewItem item1 = new ListViewItem("1");
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

                // Add items to ListView
                listView1.Items.AddRange(new ListViewItem[] { item1 });
            }
        }

        private List<ProductionResponse> getAllPOYPackingList()
        {
            var getPacking = _packingService.getAllPOYPackingList();
            return getPacking;
        }
    }
}
