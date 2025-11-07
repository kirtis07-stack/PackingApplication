using PackingApplication.Helper;
using PackingApplication.Models.ResponseEntities;
using PackingApplication.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PackingApplication
{
    public partial class ChipsPackingList : Form
    {
        private static Logger Log = Logger.GetLogger();
        PackingService _packingService = new PackingService();
        CommonMethod _cmethod = new CommonMethod();

        private int pageSize = 10;
        private int currentPage = 1;
        private int totalRecords = 0;
        private int totalPages = 0;
        private List<ProductionResponse> chipspackingList;
        public ChipsPackingList()
        {
            InitializeComponent();
            ApplyFonts();
            this.Shown += ChipsPackingList_Shown;
            this.AutoScroll = true;

            _cmethod.SetButtonBorderRadius(this.addnew, 5);
        }

        private void ApplyFonts()
        {
            this.addnew.Font = FontManager.GetFont(8F, FontStyle.Bold);
            this.dataGridView1.Font = FontManager.GetFont(8F, FontStyle.Regular);
            this.label1.Font = FontManager.GetFont(10F, FontStyle.Bold);
            this.dataGridView1.ColumnHeadersDefaultCellStyle.Font = FontManager.GetFont(9F, FontStyle.Bold);
            this.dataGridView1.DefaultCellStyle.Font = FontManager.GetFont(8F, FontStyle.Regular);
        }

        private void ChipsPackingList_Load(object sender, EventArgs e)
        {
        }

        private async void ChipsPackingList_Shown(object sender, EventArgs e)
        {
            chipspackingList = await Task.Run(() => getAllChipsPackingList());
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.Columns.Clear();
            // Define columns
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { Name = "SrNo", HeaderText = "SR. No" });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { Name = "PackingType", DataPropertyName = "PackingType", HeaderText = "Packing Type" });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { Name = "DepartmentName", DataPropertyName = "DepartmentName", HeaderText = "Department" });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { Name = "MachineName", DataPropertyName = "MachineName", HeaderText = "Machine" });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { Name = "LotNo", DataPropertyName = "LotNo", HeaderText = "Lot No" });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { Name = "BoxNo", DataPropertyName = "BoxNoFmtd", HeaderText = "Box No" });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { Name = "ProductionDate", DataPropertyName = "ProductionDate", HeaderText = "Production Date" });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { Name = "QualityName", DataPropertyName = "QualityName", HeaderText = "Quality" });
            //dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { Name = "SalesOrderNumber", DataPropertyName = "SalesOrderNumber", HeaderText = "Sales Order" });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { Name = "PackSizeName", DataPropertyName = "PackSizeName", HeaderText = "Pack Size" });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { Name = "WindingTypeName", DataPropertyName = "WindingTypeName", HeaderText = "Winding Type" });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { Name = "ProductionType", DataPropertyName = "ProductionType", HeaderText = "Production Type" });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { Name = "NoOfCopies", DataPropertyName = "NoOfCopies", HeaderText = "Copies" });

            dataGridView1.Columns["SrNo"].Width = 50;
            dataGridView1.Columns["PackingType"].Width = 100;
            dataGridView1.Columns["NoOfCopies"].Width = 50;
            //dataGridView1.Columns["ProductionDate"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            // Add Edit button column
            DataGridViewImageColumn btn = new DataGridViewImageColumn();
            btn.HeaderText = "Action";
            btn.Name = "Action";
            btn.Image = _cmethod.ResizeImage(Properties.Resources.icons8_edit_48, 20, 20);
            btn.ImageLayout = DataGridViewImageCellLayout.Normal;
            btn.Width = 45;  // column width
            dataGridView1.RowTemplate.Height = 40; // row height
            dataGridView1.Columns.Add(btn);

            // Bind your list
            dataGridView1.DataSource = chipspackingList;

            dataGridView1.CellContentClick += dataGridView1_CellContentClick;
            dataGridView1.RowPostPaint += dataGridView1_RowPostPaint;

            dataGridView1.CellMouseEnter += (s, te) =>
            {
                if (te.ColumnIndex == dataGridView1.Columns["Action"].Index && te.RowIndex >= 0)
                {
                    dataGridView1.Cursor = Cursors.Hand; // Hand cursor when over the image cell
                }
            };

            dataGridView1.CellMouseLeave += (s, te) =>
            {
                dataGridView1.Cursor = Cursors.Default; // Reset back to default
            };

            SetupPagination();
        }

        private List<ProductionResponse> getAllChipsPackingList()
        {
            var getPacking = _packingService.getAllPackingListByPackingType("chipspacking");
            return getPacking;
        }

        private void addNew_Click(object sender, EventArgs e)
        {
            var dashboard = this.ParentForm as AdminAccount;
            if (dashboard != null)
            {
                //dashboard.LoadFormInContent(new ChipsPackingForm()); // open Add form
            }
        }

        private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            int srNo = (currentPage - 1) * pageSize + e.RowIndex + 1;
            dataGridView1.Rows[e.RowIndex].Cells["SrNo"].Value = srNo;
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == dataGridView1.Columns["Action"].Index)
            {

                long productionId = Convert.ToInt32(
                    ((ProductionResponse)dataGridView1.Rows[e.RowIndex].DataBoundItem).ProductionId
                );

                var dashboard = this.ParentForm as AdminAccount;
                if (dashboard != null)
                {
                    //dashboard.LoadFormInContent(new ChipsPackingForm()); // open edit form
                }
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            int thickness = 1;   // border thickness
            int radius = 8;      // corner radius

            // shrink rectangle so border fits inside
            Rectangle rect = new Rectangle(
                thickness / 2,
                thickness / 2,
                panel1.Width - thickness - 1,
                panel1.Height - thickness - 1
            );

            using (GraphicsPath path = _cmethod.GetRoundedRect(rect, radius))
            {
                // Fill background with rounded shape
                using (SolidBrush brush = new SolidBrush(panel1.BackColor))
                {
                    e.Graphics.FillPath(brush, path);
                }

                // Draw border on same rounded shape
                using (Pen pen = new Pen(Color.FromArgb(191, 191, 191), thickness))
                {
                    e.Graphics.DrawPath(pen, path);
                }
            }
        }

        private void SetupPagination()
        {
            int totalRecords = chipspackingList.Count;
            totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);
            BindGrid();
        }

        private void BindGrid()
        {
            var data = chipspackingList.Skip((currentPage - 1) * pageSize)
                          .Take(pageSize)
                          .Select((item, index) =>
                          {
                              item.SrNo = (currentPage - 1) * pageSize + index + 1;
                              return item;
                          })
                          .ToList();

            dataGridView1.DataSource = data;
            lblPageInfo.Text = $"Page {currentPage} of {totalPages}";
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (currentPage < totalPages)
            {
                currentPage++;
                BindGrid();
            }
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            if (currentPage > 1)
            {
                currentPage--;
                BindGrid();
            }
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            currentPage = 1;
            BindGrid();
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            currentPage = totalPages;
            BindGrid();
        }
    }
}
