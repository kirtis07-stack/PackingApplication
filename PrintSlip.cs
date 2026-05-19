using PackingApplication.Constants;
using PackingApplication.Helper;
using PackingApplication.Models.RequestEntities;
using PackingApplication.Models.ResponseEntities;
using PackingApplication.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PackingApplication
{
    public partial class PrintSlip : Form
    {
        private static Logger Log = Logger.GetLogger();
        private bool isFormReady = false;
        string pakingType;
        MasterService _masterService = new MasterService();
        bool suppressEvents = false;
        private System.Windows.Forms.Label lblLoading;
        PackingService _packingService = new PackingService();
        CommonMethod _cmethod = new CommonMethod();
        GetProductionListForPrint getBoxListRequest = new GetProductionListForPrint();
        List<ProductionResponse> packingList = new List<ProductionResponse>();
        public PrintSlip()
        {
            Log.writeMessage("PrintSlip - Start : " + DateTime.Now);

            InitializeComponent();
            ApplyFonts();
            lblLoading = CommonMethod.InitializeLoadingLabel(this);

            _cmethod.SetButtonBorderRadius(this.findbtn, 8);
            _cmethod.SetButtonBorderRadius(this.cancelbtn, 8);
            _cmethod.SetButtonBorderRadius(this.selectbtn, 8);
            _cmethod.SetButtonBorderRadius(this.unselectbtn, 8);
            _cmethod.SetButtonBorderRadius(this.printbtn, 8);

            Log.writeMessage("PrintSlip - End : " + DateTime.Now);
        }

        private void PrintSlipForm_Load(object sender, EventArgs e)
        {
            Log.writeMessage("PrintSlip PrintSlipForm_Load - Start : " + DateTime.Now);

            LoadDropdowns();

            dateTimePicker1.Value = DateTimeHelper.GetDateTime();
            dateTimePicker2.Value = DateTimeHelper.GetDateTime();
            getBoxListRequest.StartDate = dateTimePicker1.Value.ToShortDateString();
            getBoxListRequest.EndDate = dateTimePicker2.Value.ToShortDateString();

            isFormReady = true;

            Log.writeMessage("PrintSlip PrintSlipForm_Load - Start : " + DateTime.Now);
        }

        private void LoadDropdowns()
        {
            Log.writeMessage("PrintSlip LoadDropdowns - Start : " + DateTime.Now);

            var deptList = new List<DepartmentResponse>();
            deptList.Insert(0, new DepartmentResponse { DepartmentId = 0, DepartmentName = "Select Dept" });
            DeptList.DataSource = deptList;
            DeptList.DisplayMember = "DepartmentName";
            DeptList.ValueMember = "DepartmentId";
            DeptList.SelectedIndex = 0;

            var packingTypeList = new List<string>();
            packingTypeList = getPackingTypeList().Result;
            PackingTypeList.DataSource = packingTypeList;
            PackingTypeList.SelectedIndex = 0;
            PackingTypeList.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            PackingTypeList.AutoCompleteSource = AutoCompleteSource.ListItems;

            var sboxList = new List<ProductionResponse>();
            sboxList.Insert(0, new ProductionResponse { ProductionId = 0, BoxNoFmtd = "Select Start Box" });
            StartBoxList.DataSource = sboxList;
            StartBoxList.DisplayMember = "BoxNoFmtd";
            StartBoxList.ValueMember = "ProductionId";
            StartBoxList.SelectedIndex = 0;

            var eboxList = new List<ProductionResponse>();
            eboxList.Insert(0, new ProductionResponse { ProductionId = 0, BoxNoFmtd = "Select End Box" });
            EndBoxList.DataSource = eboxList;
            EndBoxList.DisplayMember = "BoxNoFmtd";
            EndBoxList.ValueMember = "ProductionId";
            EndBoxList.SelectedIndex = 0;

            Log.writeMessage("PrintSlip LoadDropdowns - End : " + DateTime.Now);
        }

        private void ApplyFonts()
        {
            Log.writeMessage("PrintSlip ApplyFonts - Start : " + DateTime.Now);

            this.department.Font = FontManager.GetFont(8F, FontStyle.Bold);
            this.packingtype.Font = FontManager.GetFont(8F, FontStyle.Bold);
            this.startbox.Font = FontManager.GetFont(8F, FontStyle.Bold);
            this.endbox.Font = FontManager.GetFont(8F, FontStyle.Bold);
            this.startdate.Font = FontManager.GetFont(8F, FontStyle.Bold);
            this.enddate.Font = FontManager.GetFont(8F, FontStyle.Bold);
            this.dateTimePicker1.Font = FontManager.GetFont(8F, FontStyle.Regular);
            this.dateTimePicker2.Font = FontManager.GetFont(8F, FontStyle.Regular);
            this.DeptList.Font = FontManager.GetFont(8F, FontStyle.Regular);
            this.PackingTypeList.Font = FontManager.GetFont(8F, FontStyle.Regular);
            this.StartBoxList.Font = FontManager.GetFont(8F, FontStyle.Regular);
            this.EndBoxList.Font = FontManager.GetFont(8F, FontStyle.Regular);
            this.prcompany.Font = FontManager.GetFont(8F, FontStyle.Regular);
            this.prowner.Font = FontManager.GetFont(8F, FontStyle.Regular);
            this.prdate.Font = FontManager.GetFont(8F, FontStyle.Regular);
            this.pruser.Font = FontManager.GetFont(8F, FontStyle.Regular);
            this.prhindi.Font = FontManager.GetFont(8F, FontStyle.Regular);
            this.prwtps.Font = FontManager.GetFont(8F, FontStyle.Regular);
            this.prqrcode.Font = FontManager.GetFont(8F, FontStyle.Regular);
            this.prtwist.Font = FontManager.GetFont(8F, FontStyle.Regular);
            this.findbtn.Font = FontManager.GetFont(8F, FontStyle.Bold);
            this.cancelbtn.Font = FontManager.GetFont(8F, FontStyle.Bold);
            this.unselectbtn.Font = FontManager.GetFont(8F, FontStyle.Bold);
            this.selectbtn.Font = FontManager.GetFont(8F, FontStyle.Bold);
            this.printbtn.Font = FontManager.GetFont(8F, FontStyle.Bold);
            this.Font = FontManager.GetFont(9F, FontStyle.Bold);

            Log.writeMessage("POY ApplyFonts - End : " + DateTime.Now);
        }

        private async Task<List<string>> getPackingTypeList()
        {
            Log.writeMessage("PrintSlip getPackingTypeList - Start : " + DateTime.Now);

            var getPackingType = new List<string>
            {
                "Select Packing Type",
                "POY",
                "DTY",
                "BCF",
                "Chips"
            };

            Log.writeMessage("PrintSlip getPackingTypeList - End : " + DateTime.Now);

            return getPackingType;
        }

        private void PackingTypeList_KeyDown(object sender, KeyEventArgs e)
        {
            Log.writeMessage("PrintSlip PackingTypeList_KeyDown - Start : " + DateTime.Now);

            if (e.KeyCode == Keys.ShiftKey) // Detect Shift key
            {
                PackingTypeList.DroppedDown = true; // Open the dropdown list
                e.SuppressKeyPress = true;    // Prevent any side effect
            }
            if (e.KeyCode == Keys.Escape)
            {
                PackingTypeList.DroppedDown = false;
            }

            Log.writeMessage("PrintSlip PackingTypeList_KeyDown - End : " + DateTime.Now);
        }

        private void ComboBox_Leave(object sender, EventArgs e)
        {
            Log.writeMessage("PrintSlip ComboBox_Leave - Start : " + DateTime.Now);

            System.Windows.Forms.ComboBox cmb = sender as System.Windows.Forms.ComboBox;
            string typedText = cmb.Text.Trim();

            if (string.IsNullOrEmpty(typedText))
            {
                cmb.SelectedIndex = -1;
                return;
            }
            int index = cmb.FindStringExact(typedText);

            if (index >= 0)
            {
                cmb.SelectedIndex = index;
            }
            else
            {
                // Optionally clear or handle custom entry
                cmb.SelectedIndex = 0;
            }

            Log.writeMessage("PrintSlip ComboBox_Leave - End : " + DateTime.Now);
        }

        private void PackingTypeList_SelectedIndexChanged(object sender, EventArgs e)
        {
            Log.writeMessage("PrintSlip PackingTypeList_SelectedIndexChanged - Start : " + DateTime.Now);

            if (!isFormReady) return;

            if (PackingTypeList.SelectedValue != null)
            {
                var PackingType = PackingTypeList.SelectedValue.ToString();
                pakingType = PackingTypeList.SelectedValue.ToString();
                getBoxListRequest.PackingType = pakingType == "POY" ? "POYPACKING" : pakingType == "DTY" ? "DTYPACKING" : pakingType == "BCF" ? "BCFPACKING" : "CHPPACKING";
            }

            Log.writeMessage("PrintSlip PackingTypeList_SelectedIndexChanged - End : " + DateTime.Now);
        }

        private async void DeptList_SelectedIndexChanged(object sender, EventArgs e)
        {
            Log.writeMessage("PrintSlip DeptList_SelectedIndexChanged - Start : " + DateTime.Now);

            if (!isFormReady) return;

            if (suppressEvents) return;

            if (DeptList.SelectedIndex <= 0)
            {
                getBoxListRequest.DeptId = 0;
                return;
            }
            suppressEvents = true;
            lblLoading.Visible = true;
            try
            {
                if (DeptList.SelectedValue != null)
                {
                    DepartmentResponse selectedDepartment = (DepartmentResponse)DeptList.SelectedItem;
                    getBoxListRequest.DeptId = selectedDepartment.DepartmentId;
                }
            }
            finally
            {
                lblLoading.Visible = false;
                suppressEvents = false;
            }

            Log.writeMessage("PrintSlip DeptList_SelectedIndexChanged - Start : " + DateTime.Now);
        }

        private void DeptList_TextUpdate(object sender, EventArgs e)
        {
            Log.writeMessage("PrintSlip DeptList_TextUpdate - Start : " + DateTime.Now);

            System.Windows.Forms.ComboBox cb = (System.Windows.Forms.ComboBox)sender;
            string typedText = cb.Text;

            if (string.IsNullOrWhiteSpace(cb.Text))
            {
                cb.TextUpdate -= DeptList_TextUpdate;

                cb.SelectedIndex = 0;
                cb.Text = string.Empty;
                cb.DroppedDown = false;
                getBoxListRequest.DeptId = 0;

                cb.TextUpdate += DeptList_TextUpdate;
                return;
            }

            int cursorPosition = cb.SelectionStart;

            if (typedText.Length >= 2)
            {
                //DeptList.Items.Clear();

                var deptList = _masterService.GetDepartmentList(pakingType, typedText).Result.OrderBy(x => x.DepartmentName).ToList();

                deptList.Insert(0, new DepartmentResponse { DepartmentId = 0, DepartmentName = "Select Dept" });

                DeptList.BeginUpdate();
                DeptList.DataSource = null;
                DeptList.DisplayMember = "DepartmentName";
                DeptList.ValueMember = "DepartmentId";
                DeptList.DataSource = deptList;
                DeptList.EndUpdate();

                DeptList.TextUpdate -= DeptList_TextUpdate;
                DeptList.DroppedDown = true;
                Cursor.Current = Cursors.Default;
                DeptList.SelectionLength = typedText.Length;
                DeptList.SelectedIndex = -1;
                DeptList.Text = typedText;
                DeptList.SelectionStart = cursorPosition;
                DeptList.TextUpdate += DeptList_TextUpdate;

            }
            Log.writeMessage("PrintSlip DeptList_TextUpdate - End : " + DateTime.Now);
        }

        private void DeptList_KeyDown(object sender, KeyEventArgs e)
        {
            Log.writeMessage("PrintSlip DeptList_KeyDown - Start : " + DateTime.Now);

            if (e.KeyCode == Keys.ShiftKey) // Detect Shift key
            {
                DeptList.DroppedDown = true; // Open the dropdown list
                e.SuppressKeyPress = true;    // Prevent any side effect
            }
            if (e.KeyCode == Keys.Escape)
            {
                DeptList.DroppedDown = false;
            }
            if (e.KeyCode == Keys.F2) // Detect F2 key
            {
                DeptList.DataSource = null;
                //selectedPackingType = pakingType == "POY" ? "SpinningLot" : pakingType == "DTY" ? "TexturisingLot" : pakingType == "BCF" ? "BCFLot" : "ChipsLot";
                var deptList = _masterService.GetDepartmentList(pakingType, "").Result.OrderBy(x => x.DepartmentName).ToList();
                deptList.Insert(0, new DepartmentResponse { DepartmentId = 0, DepartmentName = "Select Department" });
                DeptList.DataSource = deptList;
                DeptList.DisplayMember = "DepartmentName";
                DeptList.ValueMember = "DepartmentId";
                DeptList.SelectedIndex = 0;
                DeptList.DroppedDown = true; // Open the dropdown list
                Cursor.Current = Cursors.Default;
                e.SuppressKeyPress = true;    // Prevent any side effect
            }

            Log.writeMessage("PrintSlip DeptList_KeyDown - End : " + DateTime.Now);
        }

        private void StartBoxList_KeyDown(object sender, KeyEventArgs e)
        {
            Log.writeMessage("PrintSlip StartBoxList_KeyDown - Start : " + DateTime.Now);

            if (e.KeyCode == Keys.ShiftKey) // Detect Shift key
            {
                StartBoxList.DroppedDown = true; // Open the dropdown list
                e.SuppressKeyPress = true;    // Prevent any side effect
            }
            if (e.KeyCode == Keys.Escape)
            {
                StartBoxList.DroppedDown = false;
            }
            if (e.KeyCode == Keys.F2) // Detect F2 key
            {
                GetProductionList getListRequest = new GetProductionList();
                getListRequest.PackingType = getBoxListRequest.PackingType;
                getListRequest.MachineId = 0;
                getListRequest.DeptId = getBoxListRequest.DeptId;
                getListRequest.SubString = null;

                StartBoxList.DataSource = null;
                var srboxnoList = _packingService.getAllBoxNoByPackingType(getListRequest).Result;
                srboxnoList.Insert(0, new ProductionResponse { ProductionId = 0, BoxNo = "Select BoxNo" });
                StartBoxList.DataSource = srboxnoList;
                StartBoxList.DisplayMember = "BoxNo";
                StartBoxList.ValueMember = "ProductionId";
                StartBoxList.SelectedIndex = 0;
                StartBoxList.DroppedDown = true; // Open the dropdown list
                Cursor.Current = Cursors.Default;
                e.SuppressKeyPress = true;    // Prevent any side effect
            }

            Log.writeMessage("PrintSlip StartBoxList_KeyDown - End : " + DateTime.Now);
        }

        private async void StartBoxList_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Log.writeMessage("PrintSlip StartBoxList_SelectionChangeCommitted - Start : " + DateTime.Now);

            if (!isFormReady) return; // skip during load

            //if (suppressEvents) return;     //Prevent recursive refresh

            if (StartBoxList.Items.Count == 0) return;

            if (StartBoxList.SelectedIndex <= 0)
            {
                return;
            }
            //suppressEvents = true;          //Freeze dependent dropdown events
            lblLoading.Visible = true;
            try
            {
                if (StartBoxList.SelectedValue != null)
                {
                    ProductionResponse selectedBoxNo = (ProductionResponse)StartBoxList.SelectedItem;
                    long selectedProductionId = selectedBoxNo.ProductionId;
                    if (selectedProductionId > 0)
                    {
                        getBoxListRequest.StartBoxNoId = selectedProductionId;
                    }
                }
            }
            finally
            {
                lblLoading.Visible = false;
                //suppressEvents = false;             //Allow events again
            }

            Log.writeMessage("PrintSlip StartBoxList_SelectionChangeCommitted - End : " + DateTime.Now);
        }

        private void StartBoxList_TextUpdate(object sender, EventArgs e)
        {
            Log.writeMessage("PrintSlip StartBoxList_TextUpdate - Start : " + DateTime.Now);

            System.Windows.Forms.ComboBox cb = (System.Windows.Forms.ComboBox)sender;
            string typedText = cb.Text;

            if (string.IsNullOrWhiteSpace(cb.Text))
            {
                cb.TextUpdate -= StartBoxList_TextUpdate;

                cb.SelectedIndex = 0;
                cb.Text = string.Empty;
                cb.DroppedDown = false;
                getBoxListRequest.StartBoxNoId = 0;

                cb.TextUpdate += StartBoxList_TextUpdate;
                return;
            }

            int cursorPosition = cb.SelectionStart;

            if (typedText.Length >= 2)
            {
                //DeptList.Items.Clear();
                GetProductionList getListRequest = new GetProductionList();
                getListRequest.PackingType = getBoxListRequest.PackingType;
                getListRequest.MachineId = 0;
                getListRequest.DeptId = getBoxListRequest.DeptId;
                getListRequest.SubString = typedText;

                var srboxnoList = _packingService.getAllBoxNoByPackingType(getListRequest).Result;

                srboxnoList.Insert(0, new ProductionResponse { ProductionId = 0, BoxNo = "Select BoxNo" });

                StartBoxList.BeginUpdate();
                StartBoxList.DataSource = null;
                StartBoxList.DisplayMember = "BoxNo";
                StartBoxList.ValueMember = "ProductionId";
                StartBoxList.DataSource = srboxnoList;
                StartBoxList.EndUpdate();

                StartBoxList.TextUpdate -= StartBoxList_TextUpdate;
                StartBoxList.DroppedDown = true;
                Cursor.Current = Cursors.Default;
                StartBoxList.SelectionLength = typedText.Length;
                StartBoxList.SelectedIndex = -1;
                StartBoxList.Text = typedText;
                StartBoxList.SelectionStart = cursorPosition;
                StartBoxList.TextUpdate += StartBoxList_TextUpdate;

            }
            Log.writeMessage("PrintSlip StartBoxList_TextUpdate - End : " + DateTime.Now);
        }

        private void EndBoxList_KeyDown(object sender, KeyEventArgs e)
        {
            Log.writeMessage("PrintSlip EndBoxList_KeyDown - Start : " + DateTime.Now);

            if (e.KeyCode == Keys.ShiftKey) // Detect Shift key
            {
                EndBoxList.DroppedDown = true; // Open the dropdown list
                e.SuppressKeyPress = true;    // Prevent any side effect
            }
            if (e.KeyCode == Keys.Escape)
            {
                EndBoxList.DroppedDown = false;
            }
            if (e.KeyCode == Keys.F2) // Detect F2 key
            {
                GetProductionList getListRequest = new GetProductionList();
                getListRequest.PackingType = getBoxListRequest.PackingType;
                getListRequest.MachineId = 0;
                getListRequest.DeptId = getBoxListRequest.DeptId;
                getListRequest.SubString = null;

                EndBoxList.DataSource = null;
                var srboxnoList = _packingService.getAllBoxNoByPackingType(getListRequest).Result;
                srboxnoList.Insert(0, new ProductionResponse { ProductionId = 0, BoxNo = "Select BoxNo" });
                EndBoxList.DataSource = srboxnoList;
                EndBoxList.DisplayMember = "BoxNo";
                EndBoxList.ValueMember = "ProductionId";
                EndBoxList.SelectedIndex = 0;
                EndBoxList.DroppedDown = true; // Open the dropdown list
                Cursor.Current = Cursors.Default;
                e.SuppressKeyPress = true;    // Prevent any side effect
            }

            Log.writeMessage("PrintSlip EndBoxList_KeyDown - End : " + DateTime.Now);
        }

        private async void EndBoxList_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Log.writeMessage("PrintSlip EndBoxList_SelectionChangeCommitted - Start : " + DateTime.Now);

            if (!isFormReady) return; // skip during load

            //if (suppressEvents) return;     //Prevent recursive refresh

            if (EndBoxList.Items.Count == 0) return;

            if (EndBoxList.SelectedIndex <= 0)
            {
                return;
            }
            //suppressEvents = true;          //Freeze dependent dropdown events
            lblLoading.Visible = true;
            try
            {
                if (EndBoxList.SelectedValue != null)
                {
                    ProductionResponse selectedBoxNo = (ProductionResponse)EndBoxList.SelectedItem;
                    long selectedProductionId = selectedBoxNo.ProductionId;
                    if (selectedProductionId > 0)
                    {
                        getBoxListRequest.EndBoxNoId = selectedProductionId;
                    }
                }
            }
            finally
            {
                lblLoading.Visible = false;
                //suppressEvents = false;             //Allow events again
            }

            Log.writeMessage("PrintSlip EndBoxList_SelectionChangeCommitted - End : " + DateTime.Now);
        }

        private void EndBoxList_TextUpdate(object sender, EventArgs e)
        {
            Log.writeMessage("PrintSlip EndBoxList_TextUpdate - Start : " + DateTime.Now);

            System.Windows.Forms.ComboBox cb = (System.Windows.Forms.ComboBox)sender;
            string typedText = cb.Text;

            if (string.IsNullOrWhiteSpace(cb.Text))
            {
                cb.TextUpdate -= EndBoxList_TextUpdate;

                cb.SelectedIndex = 0;
                cb.Text = string.Empty;
                cb.DroppedDown = false;
                getBoxListRequest.EndBoxNoId = 0;

                cb.TextUpdate += EndBoxList_TextUpdate;
                return;
            }

            int cursorPosition = cb.SelectionStart;

            if (typedText.Length >= 2)
            {
                //DeptList.Items.Clear();
                GetProductionList getListRequest = new GetProductionList();
                getListRequest.PackingType = getBoxListRequest.PackingType;
                getListRequest.MachineId = 0;
                getListRequest.DeptId = getBoxListRequest.DeptId;
                getListRequest.SubString = typedText;

                var srboxnoList = _packingService.getAllBoxNoByPackingType(getListRequest).Result;

                srboxnoList.Insert(0, new ProductionResponse { ProductionId = 0, BoxNo = "Select BoxNo" });

                EndBoxList.BeginUpdate();
                EndBoxList.DataSource = null;
                EndBoxList.DisplayMember = "BoxNo";
                EndBoxList.ValueMember = "ProductionId";
                EndBoxList.DataSource = srboxnoList;
                EndBoxList.EndUpdate();

                EndBoxList.TextUpdate -= EndBoxList_TextUpdate;
                EndBoxList.DroppedDown = true;
                Cursor.Current = Cursors.Default;
                EndBoxList.SelectionLength = typedText.Length;
                EndBoxList.SelectedIndex = -1;
                EndBoxList.Text = typedText;
                EndBoxList.SelectionStart = cursorPosition;
                EndBoxList.TextUpdate += EndBoxList_TextUpdate;

            }
            Log.writeMessage("PrintSlip EndBoxList_TextUpdate - End : " + DateTime.Now);
        }

        private void StartDate_DropDownClosed(object sender, EventArgs e)
        {
            Log.writeMessage("PrintSlip StartDate_DropDownClosed - Start : " + DateTime.Now);

            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "dd/MM/yyyy";
            DateTime selectedDate = dateTimePicker1.Value.Date;
            getBoxListRequest.StartDate = selectedDate.ToString("dd-MM-yyyy");

            Log.writeMessage("PrintSlip StartDate_DropDownClosed - End : " + DateTime.Now);
        }

        private void StartDate_KeyDown(object sender, KeyEventArgs e)
        {
            Log.writeMessage("PrintSlip StartDate_KeyDown - Start : " + DateTime.Now);

            if (e.KeyCode == Keys.Delete || e.KeyCode == Keys.Back)
            {
                dateTimePicker1.Format = DateTimePickerFormat.Custom;
                dateTimePicker1.CustomFormat = " ";
                getBoxListRequest.StartDate = null;
            }

            Log.writeMessage("PrintSlip StartDate_KeyDown - End : " + DateTime.Now);
        }

        private void EndDate_DropDownClosed(object sender, EventArgs e)
        {
            Log.writeMessage("PrintSlip EndDate_DropDownClosed - Start : " + DateTime.Now);

            dateTimePicker2.Format = DateTimePickerFormat.Custom;
            dateTimePicker2.CustomFormat = "dd/MM/yyyy";
            DateTime selectedDate = dateTimePicker1.Value.Date;
            getBoxListRequest.EndDate = selectedDate.ToString("dd-MM-yyyy");

            Log.writeMessage("PrintSlip EndDate_DropDownClosed - End : " + DateTime.Now);
        }

        private void EndDate_KeyDown(object sender, KeyEventArgs e)
        {
            Log.writeMessage("PrintSlip EndDate_KeyDown - Start : " + DateTime.Now);

            if (e.KeyCode == Keys.Delete || e.KeyCode == Keys.Back)
            {
                dateTimePicker2.Format = DateTimePickerFormat.Custom;
                dateTimePicker2.CustomFormat = " ";
                getBoxListRequest.EndDate = null;
            }

            Log.writeMessage("PrintSlip EndDate_KeyDown - End : " + DateTime.Now);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            Log.writeMessage("PrintSlip btnSearch_Click - Start : " + DateTime.Now);

            if (getBoxListRequest.DeptId == 0 && getBoxListRequest.PackingType == null)
            {
                MessageBox.Show("Please select at least any one option.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            getProductionList();

            Log.writeMessage("PrintSlip btnSearch_Click - End : " + DateTime.Now);
        }

        private void getProductionList()
        {
            Log.writeMessage("PrintSlip getProductionList - Start : " + DateTime.Now);

            packingList = _packingService.getAllBoxesForPrint(getBoxListRequest).Result;

            if (packingList.Count > 0)
            {

                dataGridView1.Focus();
                dataGridView1.AutoGenerateColumns = false;
                dataGridView1.Columns.Clear();

                // Define columns
                dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { Name = "BoxNoFmtd", DataPropertyName = "BoxNoFmtd", HeaderText = "Box No", SortMode = DataGridViewColumnSortMode.Automatic });
                dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "ProductionDate",
                    DataPropertyName = "ProductionDate",
                    HeaderText = "Packing Date",
                    SortMode = DataGridViewColumnSortMode.Automatic,
                    ValueType = typeof(DateTime),
                    DefaultCellStyle = { Format = "dd/MM/yyyy", Font = FontManager.GetFont(8F, FontStyle.Regular), Alignment = DataGridViewContentAlignment.MiddleLeft }
                });
                dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { Name = "NetWt", DataPropertyName = "NetWt", HeaderText = "Quantity", SortMode = DataGridViewColumnSortMode.Automatic });
                dataGridView1.Columns.Add(new DataGridViewCheckBoxColumn
                {
                    Name = "Print",
                    HeaderText = "Print",
                    DataPropertyName = "Print",
                    Width = 80
                });

                dataGridView1.Columns["BoxNoFmtd"].DefaultCellStyle.Font = FontManager.GetFont(8F, FontStyle.Regular);
                dataGridView1.Columns["ProductionDate"].DefaultCellStyle.Font = FontManager.GetFont(8F, FontStyle.Regular);
                dataGridView1.Columns["NetWt"].DefaultCellStyle.Font = FontManager.GetFont(8F, FontStyle.Regular);
                dataGridView1.Columns["Print"].DefaultCellStyle.Font = FontManager.GetFont(8F, FontStyle.Regular);

                dataGridView1.Columns["BoxNoFmtd"].Width = 150;
                dataGridView1.Columns["ProductionDate"].Width = 110;
                dataGridView1.Columns["NetWt"].Width = 80;
                dataGridView1.Columns["Print"].Width = 80;

                ListtoDataTableConverter converter = new ListtoDataTableConverter();
                DataTable dt = converter.ToDataTable(packingList);
                dataGridView1.DataSource = dt;

                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    row.Cells["Print"].Value = false;
                }

                dataGridView1.CurrentCellDirtyStateChanged += dataGridView1_CurrentCellDirtyStateChanged;
                dataGridView1.CellContentClick += dataGridView1_CellContentClick;
            }
            else
            {
                dataGridView1.DataSource = null;
                MessageBox.Show("Data not found", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            Log.writeMessage("PrintSlip getProductionList - End : " + DateTime.Now);
        }

        private void dataGridView1_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dataGridView1.IsCurrentCellDirty)
            {
                dataGridView1.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            Log.writeMessage("PrintSlip dataGridView1_CellContentClick - Start : " + DateTime.Now);

            if (e.RowIndex >= 0 && dataGridView1.Columns[e.ColumnIndex].Name == "Print")
            {
                dataGridView1.CommitEdit(DataGridViewDataErrorContexts.Commit);

                bool isChecked = Convert.ToBoolean(
                    dataGridView1.Rows[e.RowIndex].Cells["Print"].Value
                );

                string boxNo = dataGridView1.Rows[e.RowIndex]
                                            .Cells["BoxNoFmtd"]
                                            .Value
                                            .ToString();

                MessageBox.Show("Box No : " + boxNo +
                                "\nChecked : " + isChecked);
            }

            Log.writeMessage("PrintSlip dataGridView1_CellContentClick - End : " + DateTime.Now);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Log.writeMessage("PrintSlip btnCancel_Click - Start : " + DateTime.Now);

            dateTimePicker1.Value = DateTime.Now;
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "dd/MM/yyyy";

            dateTimePicker2.Value = DateTime.Now;
            dateTimePicker2.Format = DateTimePickerFormat.Custom;
            dateTimePicker2.CustomFormat = "dd/MM/yyyy";

            DeptList.DataSource = null;
            DeptList.Items.Clear();
            DeptList.Items.Add("Select Dept");
            DeptList.SelectedItem = "Select Dept";
            DeptList.SelectedIndex = 0;

            StartBoxList.DataSource = null;
            StartBoxList.Items.Clear();
            StartBoxList.Items.Add("Select Box");
            StartBoxList.SelectedItem = "Select Box";
            StartBoxList.SelectedIndex = 0;

            EndBoxList.DataSource = null;
            EndBoxList.Items.Clear();
            EndBoxList.Items.Add("Select Box");
            EndBoxList.SelectedItem = "Select Box";
            EndBoxList.SelectedIndex = 0;

            PackingTypeList.SelectedIndex = 0;
            packingtype = null;

            getBoxListRequest = new GetProductionListForPrint();
            dataGridView1.DataSource = null;

            PackingTypeList.Focus();

            Log.writeMessage("PrintSlip btnCancel_Click - End : " + DateTime.Now);
        }
    }
}
