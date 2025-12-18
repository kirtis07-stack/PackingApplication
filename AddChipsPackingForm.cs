using PackingApplication.Helper;
using PackingApplication.Models.CommonEntities;
using PackingApplication.Models.RequestEntities;
using PackingApplication.Models.ResponseEntities;
using PackingApplication.Services;
using PdfiumViewer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PackingApplication
{
    public partial class AddChipsPackingForm : Form
    {
        private static Logger Log = Logger.GetLogger();

        MasterService _masterService = new MasterService();
        ProductionService _productionService = new ProductionService();
        PackingService _packingService = new PackingService();
        SaleService _saleService = new SaleService();
        private long _productionId;
        private int width = 0;
        CommonMethod _cmethod = new CommonMethod();
        bool sidebarExpand = false;
        private bool showSidebarBorder = true;
        List<LotsDetailsResponse> lotsDetailsList = new List<LotsDetailsResponse>();
        LotsResponse lotResponse = new LotsResponse();
        WeighingScaleReader wtReader = new WeighingScaleReader();
        string comPort;
        int selectedSOId = 0;
        decimal totalSOQty = 0;
        decimal totalProdQty = 0;
        int selectLotId = 0;
        decimal balanceQty = 0;
        string selectedSONumber = "";
        private System.Windows.Forms.Label lblLoading;
        ProductionResponse productionResponse = new ProductionResponse();
        private ProductionRequest productionRequest = new ProductionRequest();
        private bool isFormReady = false;
        int itemBoxCategoryId = 2;
        int itemCopsCategoryId = 3;
        int itemPalletCategoryId = 5;
        List<MachineResponse> o_machinesResponse = new List<MachineResponse>();
        List<DepartmentResponse> o_departmentResponses = new List<DepartmentResponse>();
        TransactionTypePrefixRequest prefixRequest = new TransactionTypePrefixRequest();
        decimal startWeight = 0;
        decimal endWeight = 0;
        string reportServer = ConfigurationManager.AppSettings["reportServer"];
        string reportPath = ConfigurationManager.AppSettings["reportPath"];
        string UserName = ConfigurationManager.AppSettings["UserName"];
        string Password = ConfigurationManager.AppSettings["Password"];
        string Domain = ConfigurationManager.AppSettings["Domain"];
        bool suppressEvents = false;
        int selectedDeptId = 0;
        int selectedMachineid = 0;
        int selectedItemTypeid = 0;
        public AddChipsPackingForm()
        {
            Log.writeMessage("Chips AddChipsPackingForm Constructor - Start : " + DateTime.Now);

            InitializeComponent();
            ApplyFonts();
            //this.Shown += AddChipsPackingForm_Shown;
            this.AutoScroll = true;
            lblLoading = CommonMethod.InitializeLoadingLabel(this);

            _cmethod.SetButtonBorderRadius(this.submit, 8);
            _cmethod.SetButtonBorderRadius(this.cancelbtn, 8);
            _cmethod.SetButtonBorderRadius(this.saveprint, 8);

            rowMaterial.AutoGenerateColumns = false;

            Log.writeMessage("Chips AddChipsPackingForm Constructor - End : " + DateTime.Now);
        }

        private void AddChipsPackingForm_Load(object sender, EventArgs e)
        {
            Log.writeMessage("Chips AddChipsPackingForm_Load - Start : " + DateTime.Now);

            LoadDropdowns();

            copyno.Text = "1";
            //palletwtno.Text = "0.000";
            grosswtno.Text = "0.000";
            tarewt.Text = "0.000";
            netwt.Text = "0.000";
            wtpercop.Text = "0.000";
            boxpalletstock.Text = "0";
            //boxpalletitemwt.Text = "0";
            //frdenier.Text = "0";
            //updenier.Text = "0";
            //deniervalue.Text = "0";
            isFormReady = true;
            //this.reportViewer1.RefreshReport();

            RefreshLastBoxDetails();

            prcompany.FlatStyle = FlatStyle.System;
            this.tableLayoutPanel6.SetColumnSpan(this.panel29, 2);
            this.tableLayoutPanel4.SetColumnSpan(this.panel30, 3);
            this.tableLayoutPanel4.SetColumnSpan(this.panel11, 2);
            this.tableLayoutPanel4.SetColumnSpan(this.panel12, 2);
            this.tableLayoutPanel4.SetColumnSpan(this.panel8, 2);
            this.tableLayoutPanel4.SetColumnSpan(this.panel9, 2);
            this.tableLayoutPanel4.SetColumnSpan(this.panel16, 3);
            this.grosswtno.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox1_KeyDown);
            this.palletwtno.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox1_KeyDown);

            Log.writeMessage("Chips AddChipsPackingForm_Load - End : " + DateTime.Now);
        }

        private void LoadDropdowns()
        {
            Log.writeMessage("Chips LoadDropdowns - Start : " + DateTime.Now);

            var machineList = new List<MachineResponse>();
            machineList.Insert(0, new MachineResponse { MachineId = 0, MachineName = "Select Line No." });
            LineNoList.DataSource = machineList;
            LineNoList.DisplayMember = "MachineName";
            LineNoList.ValueMember = "MachineId";
            LineNoList.SelectedIndex = 0;

            var deptList = new List<DepartmentResponse>();
            deptList.Insert(0, new DepartmentResponse { DepartmentId = 0, DepartmentName = "Select Dept" });
            DeptList.DataSource = deptList;
            DeptList.DisplayMember = "DepartmentName";
            DeptList.ValueMember = "DepartmentId";
            DeptList.SelectedIndex = 0;

            var packsizeList = new List<PackSizeResponse>();
            packsizeList.Insert(0, new PackSizeResponse { PackSizeId = 0, PackSizeName = "Select Pack Size" });
            PackSizeList.DataSource = packsizeList;
            PackSizeList.DisplayMember = "PackSizeName";
            PackSizeList.ValueMember = "PackSizeId";
            PackSizeList.SelectedIndex = 0;

            var windingtypeList = new List<WindingTypeResponse>();
            windingtypeList.Insert(0, new WindingTypeResponse { WindingTypeId = 0, WindingTypeName = "Select Winding Type" });
            WindingTypeList.DataSource = windingtypeList;
            WindingTypeList.DisplayMember = "WindingTypeName";
            WindingTypeList.ValueMember = "WindingTypeId";
            WindingTypeList.SelectedIndex = 0;

            var qualityList = new List<QualityResponse>();
            qualityList.Insert(0, new QualityResponse { QualityId = 0, Name = "Select Quality" });
            QualityList.DataSource = qualityList;
            QualityList.DisplayMember = "Name";
            QualityList.ValueMember = "QualityId";
            QualityList.SelectedIndex = 0;

            var prefixList = new List<PrefixResponse>();
            prefixList.Insert(0, new PrefixResponse { PrefixCode = 0, Prefix = "Select Prefix" });
            PrefixList.DataSource = prefixList;
            PrefixList.DisplayMember = "Prefix";
            PrefixList.ValueMember = "PrefixCode";
            PrefixList.SelectedIndex = 0;

            var mergenoList = new List<LotsResponse>();
            mergenoList.Insert(0, new LotsResponse { LotId = 0, LotNoFrmt = "Select MergeNo" });
            MergeNoList.DataSource = mergenoList;
            MergeNoList.DisplayMember = "LotNoFrmt";
            MergeNoList.ValueMember = "LotId";
            MergeNoList.SelectedIndex = 0;

            var boxitemList = new List<ItemResponse>();
            boxitemList.Insert(0, new ItemResponse { ItemId = 0, Name = "Select Box/Pallet" });
            BoxItemList.DataSource = boxitemList;
            BoxItemList.DisplayMember = "Name";
            BoxItemList.ValueMember = "ItemId";
            BoxItemList.SelectedIndex = 0;

            var ownerList = new List<BusinessPartnerResponse>();
            ownerList.Insert(0, new BusinessPartnerResponse { BusinessPartnerId = 0, LegalName = "Select Owner" });
            OwnerList.DataSource = ownerList;
            OwnerList.DisplayMember = "LegalName";
            OwnerList.ValueMember = "BusinessPartnerId";
            OwnerList.SelectedIndex = 0;

            var comportList = new List<string>();
            comportList = getComPortList().Result;
            ComPortList.DataSource = comportList;
            ComPortList.SelectedIndex = 0;
            ComPortList.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            ComPortList.AutoCompleteSource = AutoCompleteSource.ListItems;

            var weightingList = new List<WeighingItem>();
            weightingList = GetWeighingList().Result;
            WeighingList.DataSource = weightingList;
            WeighingList.DisplayMember = "Name";
            WeighingList.ValueMember = "Id";
            WeighingList.SelectedIndex = 0;
            WeighingList.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            WeighingList.AutoCompleteSource = AutoCompleteSource.ListItems;

            Log.writeMessage("Chips LoadDropdowns - End : " + DateTime.Now);
        }

        private void ApplyFonts()
        {
            Log.writeMessage("Chips ApplyFonts - Start : " + DateTime.Now);

            this.lineno.Font = FontManager.GetFont(8F, FontStyle.Bold);
            this.department.Font = FontManager.GetFont(8F, FontStyle.Bold);
            this.mergeno.Font = FontManager.GetFont(8F, FontStyle.Bold);
            this.lastboxno.Font = FontManager.GetFont(8F, FontStyle.Bold);
            this.lastbox.Font = FontManager.GetFont(8F, FontStyle.Regular);
            this.item.Font = FontManager.GetFont(8F, FontStyle.Bold);
            this.shade.Font = FontManager.GetFont(8F, FontStyle.Bold);
            this.shadecode.Font = FontManager.GetFont(8F, FontStyle.Bold);
            this.boxno.Font = FontManager.GetFont(8F, FontStyle.Bold);
            this.packingdate.Font = FontManager.GetFont(8F, FontStyle.Bold);
            this.dateTimePicker1.Font = FontManager.GetFont(8F, FontStyle.Regular);
            this.quality.Font = FontManager.GetFont(8F, FontStyle.Bold);
            this.packsize.Font = FontManager.GetFont(8F, FontStyle.Bold);
            this.frdenier.Font = FontManager.GetFont(8F, FontStyle.Regular);
            this.updenier.Font = FontManager.GetFont(8F, FontStyle.Regular);
            this.windingtype.Font = FontManager.GetFont(8F, FontStyle.Bold);
            this.comport.Font = FontManager.GetFont(8F, FontStyle.Bold);
            this.boxpalletitemwt.Font = FontManager.GetFont(8F, FontStyle.Regular);
            this.boxtype.Font = FontManager.GetFont(8F, FontStyle.Bold);
            this.boxweight.Font = FontManager.GetFont(8F, FontStyle.Bold);
            this.boxstock.Font = FontManager.GetFont(8F, FontStyle.Bold);
            this.boxpalletstock.Font = FontManager.GetFont(8F, FontStyle.Regular);
            this.productiontype.Font = FontManager.GetFont(8F, FontStyle.Bold);
            this.remark.Font = FontManager.GetFont(8F, FontStyle.Bold);
            this.remarks.Font = FontManager.GetFont(8F, FontStyle.Regular);
            this.scalemodel.Font = FontManager.GetFont(8F, FontStyle.Bold);
            this.LineNoList.Font = FontManager.GetFont(8F, FontStyle.Regular);
            this.DeptList.Font = FontManager.GetFont(8F, FontStyle.Regular);
            this.MergeNoList.Font = FontManager.GetFont(8F, FontStyle.Regular);
            this.itemname.Font = FontManager.GetFont(8F, FontStyle.Regular);
            this.shadename.Font = FontManager.GetFont(8F, FontStyle.Regular);
            this.shadecd.Font = FontManager.GetFont(8F, FontStyle.Regular);
            this.QualityList.Font = FontManager.GetFont(8F, FontStyle.Regular);
            this.PackSizeList.Font = FontManager.GetFont(8F, FontStyle.Regular);
            this.WindingTypeList.Font = FontManager.GetFont(8F, FontStyle.Regular);
            this.ComPortList.Font = FontManager.GetFont(8F, FontStyle.Regular);
            this.WeighingList.Font = FontManager.GetFont(8F, FontStyle.Regular);
            this.BoxItemList.Font = FontManager.GetFont(8F, FontStyle.Regular);
            this.prcompany.Font = FontManager.GetFont(8F, FontStyle.Regular);
            this.prowner.Font = FontManager.GetFont(8F, FontStyle.Regular);
            this.prdate.Font = FontManager.GetFont(8F, FontStyle.Regular);
            this.pruser.Font = FontManager.GetFont(8F, FontStyle.Regular);
            this.prhindi.Font = FontManager.GetFont(8F, FontStyle.Regular);
            this.prwtps.Font = FontManager.GetFont(8F, FontStyle.Regular);
            this.prqrcode.Font = FontManager.GetFont(8F, FontStyle.Regular);
            this.prtwist.Font = FontManager.GetFont(8F, FontStyle.Regular);
            this.label1.Font = FontManager.GetFont(8F, FontStyle.Bold);
            this.copyno.Font = FontManager.GetFont(8F, FontStyle.Regular);
            this.wtpercop.Font = FontManager.GetFont(8F, FontStyle.Regular);
            this.label5.Font = FontManager.GetFont(8F, FontStyle.Bold);
            this.netwt.Font = FontManager.GetFont(8F, FontStyle.Regular);
            this.label4.Font = FontManager.GetFont(8F, FontStyle.Bold);
            this.tarewt.Font = FontManager.GetFont(8F, FontStyle.Regular);
            this.label3.Font = FontManager.GetFont(8F, FontStyle.Bold);
            this.grosswtno.Font = FontManager.GetFont(8F, FontStyle.Regular);
            this.label2.Font = FontManager.GetFont(8F, FontStyle.Bold);
            this.palletwtno.Font = FontManager.GetFont(8F, FontStyle.Regular);
            this.palletwt.Font = FontManager.GetFont(8F, FontStyle.Bold);
            this.prodtype.Font = FontManager.GetFont(8F, FontStyle.Regular);
            this.submit.Font = FontManager.GetFont(8F, FontStyle.Bold);
            this.saveprint.Font = FontManager.GetFont(8F, FontStyle.Bold);
            this.Printinglbl.Font = FontManager.GetFont(9F, FontStyle.Bold);
            this.netwttxtbox.Font = FontManager.GetFont(8F, FontStyle.Bold);
            this.netweight.Font = FontManager.GetFont(8F, FontStyle.Bold);
            this.grosswttxtbox.Font = FontManager.GetFont(8F, FontStyle.Bold);
            this.grossweight.Font = FontManager.GetFont(8F, FontStyle.Bold);
            this.copstxtbox.Font = FontManager.GetFont(8F, FontStyle.Bold);
            this.tarewghttxtbox.Font = FontManager.GetFont(8F, FontStyle.Bold);
            this.tareweight.Font = FontManager.GetFont(8F, FontStyle.Bold);
            this.cops.Font = FontManager.GetFont(8F, FontStyle.Bold);
            this.Lastboxlbl.Font = FontManager.GetFont(9F, FontStyle.Bold);
            this.deniervalue.Font = FontManager.GetFont(8F, FontStyle.Regular);
            this.denier.Font = FontManager.GetFont(8F, FontStyle.Bold);
            this.PrefixList.Font = FontManager.GetFont(8F, FontStyle.Regular);
            this.machineboxheader.Font = FontManager.GetFont(8F, FontStyle.Regular);
            this.Machinelbl.Font = FontManager.GetFont(9F, FontStyle.Bold);
            this.grosswterror.Font = FontManager.GetFont(7F, FontStyle.Regular);
            this.palletwterror.Font = FontManager.GetFont(7F, FontStyle.Regular);
            this.spoolwterror.Font = FontManager.GetFont(7F, FontStyle.Regular);
            this.Weighboxlbl.Font = FontManager.GetFont(9F, FontStyle.Bold);
            this.Packagingboxlbl.Font = FontManager.GetFont(9F, FontStyle.Bold);
            this.cancelbtn.Font = FontManager.GetFont(8F, FontStyle.Bold);
            this.boxnoerror.Font = FontManager.GetFont(7F, FontStyle.Regular);
            this.windingerror.Font = FontManager.GetFont(7F, FontStyle.Regular);
            this.packsizeerror.Font = FontManager.GetFont(7F, FontStyle.Regular);
            this.qualityerror.Font = FontManager.GetFont(7F, FontStyle.Regular);
            this.mergenoerror.Font = FontManager.GetFont(7F, FontStyle.Regular);
            this.copynoerror.Font = FontManager.GetFont(7F, FontStyle.Regular);
            this.linenoerror.Font = FontManager.GetFont(7F, FontStyle.Regular);
            this.rowMaterialBox.Font = FontManager.GetFont(8F, FontStyle.Bold);
            this.fromdenier.Font = FontManager.GetFont(8F, FontStyle.Bold);
            this.uptodenier.Font = FontManager.GetFont(8F, FontStyle.Bold);
            this.Font = FontManager.GetFont(9F, FontStyle.Bold);
            this.salelotvalue.Font = FontManager.GetFont(8F, FontStyle.Regular);
            this.salelot.Font = FontManager.GetFont(8F, FontStyle.Bold);
            this.owner.Font = FontManager.GetFont(8F, FontStyle.Bold);
            this.OwnerList.Font = FontManager.GetFont(8F, FontStyle.Regular);
            this.fromwt.Font = FontManager.GetFont(8F, FontStyle.Bold);
            this.frwt.Font = FontManager.GetFont(8F, FontStyle.Regular);
            this.uptowt.Font = FontManager.GetFont(8F, FontStyle.Bold);
            this.upwt.Font = FontManager.GetFont(8F, FontStyle.Regular);

            Log.writeMessage("Chips ApplyFonts - Start : " + DateTime.Now);
        }

        //private async void AddChipsPackingForm_Shown(object sender, EventArgs e)
        //{
        //    Log.writeMessage("Chips AddChipsPackingForm_Shown - Start : " + DateTime.Now);

        //    try
        //    {
        //        var machineTask = _masterService.GetMachineList("ChipsLot", "");
        //        var packsizeTask = _masterService.GetPackSizeList("");
        //        var copsitemTask = _masterService.GetItemList(itemCopsCategoryId, "");
        //        var boxitemTask = _masterService.GetItemList(itemBoxCategoryId, "");
        //        var deptTask = _masterService.GetDepartmentList("");
        //        var ownerTask = _masterService.GetOwnerList("");

        //        // 2. Wait for all to complete
        //        await Task.WhenAll(machineTask, packsizeTask, copsitemTask, boxitemTask, deptTask, ownerTask);

        //        // 3. Get the results
        //        var machineList = machineTask.Result;
        //        var packsizeList = packsizeTask.Result;
        //        var copsitemList = copsitemTask.Result;
        //        var boxitemList = boxitemTask.Result;
        //        var deptList = deptTask.Result;
        //        var ownerList = ownerTask.Result;

        //        //machine
        //        o_machinesResponse = machineList;
        //        machineList.Insert(0, new MachineResponse { MachineId = 0, MachineName = "Select Line No." });
        //        LineNoList.DataSource = machineList;
        //        LineNoList.DisplayMember = "MachineName";
        //        LineNoList.ValueMember = "MachineId";
        //        LineNoList.SelectedIndex = 0;
        //        LineNoList.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
        //        LineNoList.AutoCompleteSource = AutoCompleteSource.ListItems;

        //        //packsize
        //        packsizeList.Insert(0, new PackSizeResponse { PackSizeId = 0, PackSizeName = "Select Pack Size" });
        //        PackSizeList.DataSource = packsizeList;
        //        PackSizeList.DisplayMember = "PackSizeName";
        //        PackSizeList.ValueMember = "PackSizeId";
        //        PackSizeList.SelectedIndex = 0;
        //        PackSizeList.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
        //        PackSizeList.AutoCompleteSource = AutoCompleteSource.ListItems;

        //        var comportList = await Task.Run(() => getComPortList());
        //        //comport
        //        ComPortList.DataSource = comportList;
        //        ComPortList.SelectedIndex = 0;
        //        ComPortList.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
        //        ComPortList.AutoCompleteSource = AutoCompleteSource.ListItems;

        //        var weightingList = await Task.Run(() => getWeighingList());
        //        //weighting
        //        WeighingList.DataSource = weightingList;
        //        WeighingList.DisplayMember = "Name";
        //        WeighingList.ValueMember = "Id";
        //        WeighingList.SelectedIndex = 0;
        //        WeighingList.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
        //        WeighingList.AutoCompleteSource = AutoCompleteSource.ListItems;

        //        //boxitem
        //        boxitemList.Insert(0, new ItemResponse { ItemId = 0, Name = "Select Box/Pallet" });
        //        BoxItemList.DataSource = boxitemList;
        //        BoxItemList.DisplayMember = "Name";
        //        BoxItemList.ValueMember = "ItemId";
        //        BoxItemList.SelectedIndex = 0;
        //        BoxItemList.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
        //        BoxItemList.AutoCompleteSource = AutoCompleteSource.ListItems;

        //        o_departmentResponses = deptList;
        //        deptList.Insert(0, new DepartmentResponse { DepartmentId = 0, DepartmentName = "Select Dept" });
        //        DeptList.DataSource = deptList;
        //        DeptList.DisplayMember = "DepartmentName";
        //        DeptList.ValueMember = "DepartmentId";
        //        DeptList.SelectedIndex = 0;
        //        DeptList.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
        //        DeptList.AutoCompleteSource = AutoCompleteSource.ListItems;

        //        ownerList.Insert(0, new BusinessPartnerResponse { BusinessPartnerId = 0, LegalName = "Select Owner" });
        //        OwnerList.DataSource = ownerList;
        //        OwnerList.DisplayMember = "LegalName";
        //        OwnerList.ValueMember = "BusinessPartnerId";
        //        OwnerList.SelectedIndex = 0;
        //        OwnerList.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
        //        OwnerList.AutoCompleteSource = AutoCompleteSource.ListItems;

        //        RefreshLastBoxDetails();

        //        isFormReady = true;
        //    }
        //    finally
        //    {
        //        lblLoading.Visible = false;
        //    }

        //    Log.writeMessage("Chips AddChipsPackingForm_Shown - Start : " + DateTime.Now);
        //}

        private async Task LoadProductionDetailsAsync(ProductionResponse prodResponse)
        {
            Log.writeMessage("Chips LoadProductionDetailsAsync - Start : " + DateTime.Now);

            if (prodResponse != null)
            {
                productionResponse = prodResponse;

                //LineNoList.SelectedValue = productionResponse.MachineId;
                //DeptList.SelectedValue = productionResponse.DepartmentId;
                //MergeNoList.SelectedValue = productionResponse.LotId;
                //PrefixList.SelectedValue = productionResponse.PrefixCode;
                //QualityList.SelectedValue = productionResponse.QualityId;
                //WindingTypeList.SelectedValue = productionResponse.WindingTypeId;
                //PackSizeList.SelectedValue = productionResponse.PackSizeId;
                //BoxItemList.SelectedValue = productionResponse.BoxItemId;
                //proChipspe.Text = productionResponse.ProductionType;
                //remarks.Text = productionResponse.Remarks;
                //prcompany.Checked = productionResponse.PrintCompany;
                //prowner.Checked = productionResponse.PrintOwner;
                //prdate.Checked = productionResponse.PrintDate;
                //pruser.Checked = productionResponse.PrintUser;
                //prhindi.Checked = productionResponse.PrintHindiWords;
                //prwtps.Checked = productionResponse.PrintWTPS;
                //prqrcode.Checked = productionResponse.PrintQRCode;
                //prtwist.Checked = productionResponse.PrintTwist;
                //OwnerList.SelectedValue = productionResponse.OwnerId;
                //LineNoList_SelectedIndexChanged(LineNoList, EventArgs.Empty);

                LineNoList.DataSource = null;
                LineNoList.Items.Clear();
                LineNoList.Items.Add("Select Line No.");
                LineNoList.Items.Add(productionResponse.MachineName);
                LineNoList.SelectedItem = productionResponse.MachineName;
                productionRequest.MachineId = productionResponse.MachineId;
                selectedMachineid = productionResponse.MachineId;

                DeptList.DataSource = null;
                DeptList.Items.Clear();
                DeptList.Items.Add("Select Dept");
                DeptList.Items.Add(productionResponse.DepartmentName);
                DeptList.SelectedItem = productionResponse.DepartmentName;
                productionRequest.DepartmentId = productionResponse.DepartmentId;
                selectedDeptId = productionResponse.DepartmentId;

                MergeNoList.DataSource = null;
                MergeNoList.Items.Clear();
                MergeNoList.Items.Add("Select MergeNo");
                MergeNoList.Items.Add(productionResponse.LotNo);
                MergeNoList.SelectedItem = productionResponse.LotNo;
                productionRequest.LotId = productionResponse.LotId;
                selectLotId = productionResponse.LotId;

                PrefixList.DataSource = null;
                PrefixList.Items.Clear();
                PrefixList.Items.Add("Select Prefix");
                PrefixList.Items.Add(productionResponse.BoxPrefix);
                PrefixList.SelectedItem = productionResponse.BoxPrefix;
                productionRequest.PrefixCode = productionResponse.PrefixCode;

                QualityList.DataSource = null;
                QualityList.Items.Clear();
                QualityList.Items.Add("Select Quality");
                QualityList.Items.Add(productionResponse.QualityName);
                QualityList.SelectedItem = productionResponse.QualityName;
                productionRequest.QualityId = productionResponse.QualityId;

                WindingTypeList.DataSource = null;
                WindingTypeList.Items.Clear();
                WindingTypeList.Items.Add("Select Winding Type");
                WindingTypeList.Items.Add(productionResponse.WindingTypeName);
                WindingTypeList.SelectedItem = productionResponse.WindingTypeName;
                productionRequest.WindingTypeId = productionResponse.WindingTypeId;

                PackSizeList.DataSource = null;
                PackSizeList.Items.Clear();
                PackSizeList.Items.Add("Select Pack Size");
                PackSizeList.Items.Add(productionResponse.PackSizeName);
                PackSizeList.SelectedItem = productionResponse.PackSizeName;
                productionRequest.PackSizeId = productionResponse.PackSizeId;

                BoxItemList.DataSource = null;
                BoxItemList.Items.Clear();
                BoxItemList.Items.Add("Select Box/Pallet");
                BoxItemList.Items.Add(productionResponse.BoxItemName);
                BoxItemList.SelectedItem = productionResponse.BoxItemName;
                productionRequest.BoxItemId = productionResponse.BoxItemId;

                OwnerList.DataSource = null;
                OwnerList.Items.Clear();
                OwnerList.Items.Add("Select Owner");
                if (!string.IsNullOrEmpty(productionResponse.OwnerName))
                {
                    OwnerList.Items.Add(productionResponse.OwnerName);
                    OwnerList.SelectedItem = productionResponse.OwnerName;
                    productionRequest.OwnerId = productionResponse.OwnerId;
                }

                prodtype.Text = productionResponse.ProductionType;
                productionRequest.ProdTypeId = productionResponse.ProdTypeId;
                remarks.Text = productionResponse.Remarks;
                productionRequest.Remarks = productionResponse.Remarks;
                prcompany.Checked = productionResponse.PrintCompany;
                productionRequest.PrintCompany = productionResponse.PrintCompany;
                prowner.Checked = productionResponse.PrintOwner;
                productionRequest.PrintOwner = productionResponse.PrintOwner;
                prdate.Checked = productionResponse.PrintDate;
                productionRequest.PrintDate = productionResponse.PrintDate;
                pruser.Checked = productionResponse.PrintUser;
                productionRequest.PrintUser = productionResponse.PrintUser;
                prhindi.Checked = productionResponse.PrintHindiWords;
                productionRequest.PrintHindiWords = productionResponse.PrintHindiWords;
                prwtps.Checked = productionResponse.PrintWTPS;
                productionRequest.PrintWTPS = productionResponse.PrintWTPS;
                prqrcode.Checked = productionResponse.PrintQRCode;
                productionRequest.PrintQRCode = productionResponse.PrintQRCode;
                prtwist.Checked = productionResponse.PrintTwist;
                productionRequest.PrintTwist = productionResponse.PrintTwist;

                lotsDetailsList = new List<LotsDetailsResponse>();
                if (productionResponse.LotsDetailsResponse.Count > 0)
                {
                    rowMaterial.Columns.Clear();
                    rowMaterial.Columns.Add(new DataGridViewTextBoxColumn { Name = "PrevLotType", DataPropertyName = "PrevLotType", HeaderText = "Prev.LotType" });
                    rowMaterial.Columns.Add(new DataGridViewTextBoxColumn { Name = "PrevLotNo", DataPropertyName = "PrevLotNo", HeaderText = "Prev.LotNo" });
                    rowMaterial.Columns.Add(new DataGridViewTextBoxColumn { Name = "PrevLotItemName", DataPropertyName = "PrevLotItemName", HeaderText = "Prev.LotItem" });
                    rowMaterial.Columns.Add(new DataGridViewTextBoxColumn { Name = "PrevLotShadeName", DataPropertyName = "PrevLotShadeName", HeaderText = "Prev.LotShade" });
                    rowMaterial.Columns.Add(new DataGridViewTextBoxColumn { Name = "PrevLotQuality", DataPropertyName = "PrevLotQuality", HeaderText = "Quality" });
                    rowMaterial.Columns.Add(new DataGridViewTextBoxColumn { Name = "ProductionPerc", DataPropertyName = "ProductionPerc", HeaderText = "Production %" });
                    rowMaterial.Columns.Add(new DataGridViewTextBoxColumn { Name = "EffectiveFrom", DataPropertyName = "EffectiveFrom", HeaderText = "EffectiveFrom", Width = 150, DefaultCellStyle = { Format = "dd-MM-yyyy hh:mm tt" } });
                    rowMaterial.Columns.Add(new DataGridViewTextBoxColumn { Name = "EffectiveUpto", DataPropertyName = "EffectiveUpto", HeaderText = "EffectiveUpto", Width = 150, DefaultCellStyle = { Format = "dd-MM-yyyy hh:mm tt" } });
                    rowMaterial.DataSource = productionResponse.LotsDetailsResponse;
                    lotsDetailsList = productionResponse.LotsDetailsResponse;
                }
                itemname.Text = productionResponse.ItemName;
                shadename.Text = productionResponse.ShadeName;
                shadecd.Text = productionResponse.ShadeCode;
                deniervalue.Text = productionResponse.Denier.ToString();
                salelotvalue.Text = productionResponse.SaleLot.ToString();
                frdenier.Text = productionResponse.FromDenier.ToString();
                updenier.Text = productionResponse.UpToDenier.ToString();
                startWeight = productionResponse.StartWeight;
                endWeight = productionResponse.EndWeight;
                frwt.Text = productionResponse.StartWeight.ToString();
                upwt.Text = productionResponse.EndWeight.ToString();
                boxpalletitemwt.Text = productionResponse.BoxItemWeight.ToString();
                palletwtno.Text = productionResponse.BoxItemWeight.ToString();
                totalSOQty = productionResponse.SOQuantity;
                RefreshGradewiseGrid();
                AdjustNameByCharCount();
                productionRequest.ItemId = productionResponse.ItemId;
                productionRequest.ShadeId = productionResponse.ShadeId;
                productionRequest.TwistId = productionResponse.TwistId;
                productionRequest.ContainerTypeId = productionResponse.ContainerTypeId;
            }

            Log.writeMessage("Chips LoadProductionDetailsAsync - End : " + DateTime.Now);
        }

        private async void LineNoList_SelectedIndexChanged(object sender, EventArgs e)
        {
            Log.writeMessage("Chips LineNoList_SelectedIndexChanged - Start : " + DateTime.Now);

            if (!isFormReady) return; // skip during load

            if (suppressEvents) return;     //Prevent recursive refresh

            if (LineNoList.Items.Count == 0) return;

            if (LineNoList.SelectedIndex <= 0)
            {
                selectedMachineid = 0;
                return;
            }
            suppressEvents = true;          //Freeze dependent dropdown events
            lblLoading.Visible = true;
            try
            {
                if (LineNoList.SelectedValue != null)
                {
                    MachineResponse selectedMachine = (MachineResponse)LineNoList.SelectedItem;
                    int selectedMachineId = selectedMachine.MachineId;
                    if (selectedMachineId > 0)
                    {
                        productionRequest.MachineId = selectedMachineId;
                        selectedMachineid = selectedMachine.MachineId;
                        if (selectedMachine != null)
                        {
                            var deptTask = _masterService.GetDepartmentList(selectedMachine.DepartmentName).Result;
                            deptTask.Insert(0, new DepartmentResponse { DepartmentId = 0, DepartmentName = "Select Dept" });
                            DeptList.SelectedIndexChanged -= DeptList_SelectedIndexChanged;
                            DeptList.DataSource = deptTask;
                            DeptList.SelectedValue = selectedMachine.DepartmentId;
                            selectedDeptId = selectedMachine.DepartmentId;
                            productionRequest.DepartmentId = selectedDeptId;
                            //var filteredDepts = o_departmentResponses.Where(m => m.DepartmentId == selectedMachine.DepartmentId).ToList();
                            //filteredDepts.Insert(0, new DepartmentResponse { DepartmentId = 0, DepartmentName = "Select Dept" });
                            //DeptList.DataSource = filteredDepts;
                            DeptList.DisplayMember = "DepartmentName";
                            DeptList.ValueMember = "DepartmentId";
                            if (DeptList.Items.Count > 1)
                            {
                                DeptList.SelectedIndex = 1;
                            }
                            DeptList.SelectedIndexChanged += DeptList_SelectedIndexChanged;
                        }
                        if (productionRequest.PrefixCode != 0)
                        {
                            prefixRequest.DepartmentId = selectedDeptId;
                            prefixRequest.TxnFlag = "Chp";
                            prefixRequest.TransactionTypeId = 5;
                            prefixRequest.ProductionTypeId = 1;
                            prefixRequest.Prefix = "";
                            prefixRequest.FinYearId = SessionManager.FinYearId;

                            List<PrefixResponse> prefixList = _masterService.GetPrefixList(prefixRequest).Result.OrderBy(x => x.Prefix).ToList();
                            prefixList.Insert(0, new PrefixResponse { PrefixCode = 0, Prefix = "Select Prefix" });

                            var isExist = prefixList.Where(x => x.PrefixCode == productionRequest.PrefixCode).Any();
                            if (!isExist)
                            {
                                PrefixList.DataSource = null;
                                PrefixList.Items.Clear();
                                PrefixList.Items.Add("Select Prefix");
                                PrefixList.SelectedItem = "Select Prefix";
                            }
                        }

                        MergeNoList.DataSource = null;
                        MergeNoList.Items.Clear();
                        MergeNoList.Items.Add("Select MergeNo");
                        MergeNoList.SelectedItem = "Select MergeNo";

                        ResetDependentDropdownValues();
                    }
                }
            }
            finally
            {
                lblLoading.Visible = false;
                suppressEvents = false;             //Allow events again
            }

            Log.writeMessage("Chips LineNoList_SelectedIndexChanged - End : " + DateTime.Now);
        }

        private void LinoNoList_TextUpdate(object sender, EventArgs e)
        {
            Log.writeMessage("Chips LinoNoList_TextUpdate - Start : " + DateTime.Now);

            System.Windows.Forms.ComboBox cb = (System.Windows.Forms.ComboBox)sender;
            string typedText = cb.Text;

            if (string.IsNullOrWhiteSpace(cb.Text))
            {
                cb.TextUpdate -= LinoNoList_TextUpdate;

                cb.SelectedIndex = 0;   // "Select Line No."
                cb.Text = string.Empty;
                cb.DroppedDown = false;
                selectedMachineid = 0;

                cb.TextUpdate += LinoNoList_TextUpdate;
                return;
            }

            int cursorPosition = cb.SelectionStart;

            if (typedText.Length >= 2)
            {
                //LineNoList.Items.Clear();

                List<MachineResponse> machineList = new List<MachineResponse>();
                if (selectedDeptId == 0)
                {
                    machineList = _masterService.GetMachineList("ChipsLot", typedText).Result.OrderBy(x => x.MachineName).ToList();

                    machineList.Insert(0, new MachineResponse { MachineId = 0, MachineName = "Select Line No." });
                }
                else
                {
                    machineList = _masterService.GetMachineByDepartmentIdAndLotType(selectedDeptId, "ChipsLot").Result.OrderBy(x => x.MachineName).ToList();

                    machineList.Insert(0, new MachineResponse { MachineId = 0, MachineName = "Select Line No." });
                }

                LineNoList.BeginUpdate();
                LineNoList.DataSource = null;
                LineNoList.DisplayMember = "MachineName";
                LineNoList.ValueMember = "MachineId";
                LineNoList.DataSource = machineList;
                LineNoList.EndUpdate();

                LineNoList.TextUpdate -= LinoNoList_TextUpdate;
                LineNoList.Text = typedText;
                LineNoList.DroppedDown = true;
                LineNoList.SelectionStart = cursorPosition;
                LineNoList.SelectionLength = typedText.Length;
                LineNoList.TextUpdate += LinoNoList_TextUpdate;
            }

            Log.writeMessage("Chips LinoNoList_TextUpdate - End : " + DateTime.Now);
        }

        private async void MergeNoList_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Log.writeMessage("Chips MergeNoList_SelectionChangeCommitted - Start : " + DateTime.Now);

            if (!isFormReady) return;

            if (suppressEvents) return;

            if (MergeNoList.Items.Count == 0) return;

            if (MergeNoList.SelectedIndex < 0)
            {
                ResetLotValues();
                return;
            }
            suppressEvents = true;
            lblLoading.Visible = true;
            try
            {
                if (MergeNoList.SelectedValue != null)
                {
                    LotsResponse selectedLot = (LotsResponse)MergeNoList.SelectedItem;
                    int selectedLotId = selectedLot.LotId;
                    if (selectedLotId < 0) { ResetLotValues(); return; }
                    if (selectedLotId > 0)
                    {
                        ResetDependentDropdownValues();
                        productionRequest.LotId = selectedLot.LotId;
                        if (selectedMachineid == 0)
                        {
                            LineNoList.DataSource = null;
                            LineNoList.Items.Clear();
                            LineNoList.Items.Add("Select Line No.");
                            LineNoList.Items.Add(selectedLot.MachineName);
                            LineNoList.SelectedItem = selectedLot.MachineName;
                            productionRequest.MachineId = selectedLot.MachineId;
                            selectedMachineid = selectedLot.MachineId;
                        }
                        if (selectedDeptId == 0)
                        {
                            DeptList.DataSource = null;
                            DeptList.Items.Clear();
                            DeptList.Items.Add("Select Dept");
                            DeptList.Items.Add(selectedLot.DepartmentName);
                            DeptList.SelectedItem = selectedLot.DepartmentName;
                            productionRequest.DepartmentId = selectedLot.DepartmentId;
                            selectedDeptId = selectedLot.DepartmentId;
                        }
                        selectLotId = selectedLotId;
                        lotResponse = _productionService.getLotById(selectedLotId).Result;
                        if (lotResponse != null)
                        {
                            itemname.Text = (!string.IsNullOrEmpty(lotResponse.ItemName)) ? lotResponse.ItemName : "";
                            shadename.Text = (!string.IsNullOrEmpty(lotResponse.ShadeName)) ? lotResponse.ShadeName : "";
                            AdjustNameByCharCount();
                            shadecd.Text = (!string.IsNullOrEmpty(lotResponse.ShadeCode)) ? lotResponse.ShadeCode : "";
                            deniervalue.Text = lotResponse.Denier.ToString();
                            salelotvalue.Text = (!string.IsNullOrEmpty(lotResponse.SaleLot)) ? lotResponse.SaleLot.ToString() : null;
                            productionRequest.SaleLot = (!string.IsNullOrEmpty(lotResponse.SaleLot)) ? lotResponse.SaleLot : null;
                            productionRequest.MachineId = lotResponse.MachineId;
                            productionRequest.ItemId = lotResponse.ItemId;
                            productionRequest.ShadeId = lotResponse.ShadeId;
                            LineNoList.SelectedValue = lotResponse.MachineId;

                            if (lotResponse.ItemId > 0)
                            {
                                var itemResponse = _masterService.GetItemById(lotResponse.ItemId).Result;
                                if (itemResponse != null)
                                {
                                    selectedItemTypeid = itemResponse.ItemTypeId;
                                    var qualityList = _masterService.GetQualityListByItemTypeId(selectedItemTypeid).Result.OrderBy(x => x.Name).ToList();
                                    qualityList.Insert(0, new QualityResponse { QualityId = 0, Name = "Select Quality" });
                                    QualityList.SelectedIndexChanged -= QualityList_SelectedIndexChanged;
                                    QualityList.DataSource = qualityList;
                                    QualityList.DisplayMember = "Name";
                                    QualityList.ValueMember = "QualityId";
                                    //QualityList.SelectedIndex = 0;
                                    //QualityList.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                                    //QualityList.AutoCompleteSource = AutoCompleteSource.ListItems;

                                    if (QualityList.Items.Count > 1)
                                    {
                                        QualityList.SelectedIndex = 1;
                                    }
                                    else if (QualityList.Items.Count > 0) // fallback to first item if only one exists
                                    {
                                        QualityList.SelectedIndex = 0;
                                    }
                                    else
                                    {
                                        QualityList.SelectedIndex = -1; // no selection possible
                                    }
                                    if (QualityList.SelectedIndex >= 0)
                                    {
                                        int firstQualityId = Convert.ToInt32(QualityList.SelectedValue);
                                        productionRequest.QualityId = firstQualityId;
                                    }
                                    QualityList.SelectedIndexChanged += QualityList_SelectedIndexChanged;
                                }
                            }
                        }
                        lotsDetailsList = new List<LotsDetailsResponse>();
                        productionRequest.ProductionDate = dateTimePicker1.Value;
                        lotsDetailsList = _productionService.getLotsDetailsByLotsIdAndProductionDate(selectedLotId, productionRequest.ProductionDate).Result;
                        if (lotsDetailsList.Count > 0)
                        {
                            rowMaterial.Columns.Clear();
                            rowMaterial.Columns.Add(new DataGridViewTextBoxColumn { Name = "PrevLotType", DataPropertyName = "PrevLotType", HeaderText = "Prev.LotType" });
                            rowMaterial.Columns.Add(new DataGridViewTextBoxColumn { Name = "PrevLotNo", DataPropertyName = "PrevLotNo", HeaderText = "Prev.LotNo" });
                            rowMaterial.Columns.Add(new DataGridViewTextBoxColumn { Name = "PrevLotItemName", DataPropertyName = "PrevLotItemName", HeaderText = "Prev.LotItem" });
                            rowMaterial.Columns.Add(new DataGridViewTextBoxColumn { Name = "PrevLotShadeName", DataPropertyName = "PrevLotShadeName", HeaderText = "Prev.LotShade" });
                            rowMaterial.Columns.Add(new DataGridViewTextBoxColumn { Name = "PrevLotQuality", DataPropertyName = "PrevLotQuality", HeaderText = "Quality" });
                            rowMaterial.Columns.Add(new DataGridViewTextBoxColumn { Name = "ProductionPerc", DataPropertyName = "ProductionPerc", HeaderText = "Production %" });
                            rowMaterial.Columns.Add(new DataGridViewTextBoxColumn { Name = "EffectiveFrom", DataPropertyName = "EffectiveFrom", HeaderText = "EffectiveFrom", Width = 150, DefaultCellStyle = { Format = "dd-MM-yyyy hh:mm tt" } });
                            rowMaterial.Columns.Add(new DataGridViewTextBoxColumn { Name = "EffectiveUpto", DataPropertyName = "EffectiveUpto", HeaderText = "EffectiveUpto", Width = 150, DefaultCellStyle = { Format = "dd-MM-yyyy hh:mm tt" } });
                            rowMaterial.DataSource = lotsDetailsList;
                        }
                    }

                }
            }
            finally
            {
                lblLoading.Visible = false;
                suppressEvents = false;
            }

            Log.writeMessage("Chips MergeNoList_SelectionChangeCommitted - End : " + DateTime.Now);
        }

        private void MergeNoList_TextUpdate(object sender, EventArgs e)
        {
            Log.writeMessage("Chips MergeNoList_TextUpdate - Start : " + DateTime.Now);

            if (suppressEvents) return;

            System.Windows.Forms.ComboBox cb = (System.Windows.Forms.ComboBox)sender;
            string typedText = cb.Text;

            if (string.IsNullOrWhiteSpace(cb.Text))
            {
                cb.TextUpdate -= MergeNoList_TextUpdate;

                cb.SelectedIndex = 0;
                cb.Text = string.Empty;
                cb.DroppedDown = false;
                ResetLotValues();

                cb.TextUpdate += MergeNoList_TextUpdate;
                return;
            }

            int cursorPosition = cb.SelectionStart;

            if (typedText.Length >= 2)
            {
                suppressEvents = true;

                //MergeNoList.Items.Clear();

                List<LotsResponse> mergenoList = new List<LotsResponse>();
                if (selectedMachineid > 0)
                {
                    mergenoList = _productionService.getLotList(selectedMachineid, typedText).Result.OrderBy(x => x.LotNoFrmt).ToList();

                    mergenoList.Insert(0, new LotsResponse { LotId = 0, LotNoFrmt = "Select MergeNo" });
                }
                else
                {
                    mergenoList = _productionService.getLotsByLotType("ChipsLot", typedText).Result.OrderBy(x => x.LotNoFrmt).ToList();

                    mergenoList.Insert(0, new LotsResponse { LotId = 0, LotNoFrmt = "Select MergeNo" });
                }

                MergeNoList.BeginUpdate();
                MergeNoList.DataSource = null;
                MergeNoList.DisplayMember = "LotNoFrmt";
                MergeNoList.ValueMember = "LotId";
                MergeNoList.DataSource = mergenoList;
                MergeNoList.EndUpdate();

                MergeNoList.TextUpdate -= MergeNoList_TextUpdate;
                MergeNoList.Text = typedText;
                MergeNoList.DroppedDown = true;
                MergeNoList.SelectionStart = cursorPosition;
                MergeNoList.SelectionLength = typedText.Length;
                MergeNoList.TextUpdate += MergeNoList_TextUpdate;

                suppressEvents = false;
            }

            Log.writeMessage("Chips MergeNoList_TextUpdate - End : " + DateTime.Now);
        }

        private void ResetLotValues()
        {
            Log.writeMessage("Chips ResetLotValues - Start : " + DateTime.Now);

            itemname.Text = "";
            shadename.Text = "";
            shadecd.Text = "";
            deniervalue.Text = "";
            salelotvalue.Text = "";
            lotResponse = new LotsResponse();
            lotsDetailsList = new List<LotsDetailsResponse>();
            ResetDependentDropdownValues();
            rowMaterial.Columns.Clear();
            totalProdQty = 0;
            selectedSOId = 0;
            totalSOQty = 0;
            balanceQty = 0;
            selectLotId = 0;
            selectedSONumber = "";
            selectedItemTypeid = 0;
            //MergeNoList.SelectedIndex = 0;
            Log.writeMessage("Chips ResetLotValues - End : " + DateTime.Now);
        }

        private async void PackSizeList_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Log.writeMessage("Chips PackSizeList_SelectionChangeCommitted - Start : " + DateTime.Now);

            if (!isFormReady) return;

            if (PackSizeList.SelectedIndex <= 0)
            {
                frdenier.Text = "0";
                updenier.Text = "0";
                frwt.Text = "0";
                upwt.Text = "0";
                return;
            }
            lblLoading.Visible = true;
            try
            {
                if (PackSizeList.SelectedValue != null)
                {
                    PackSizeResponse selectedPacksize = (PackSizeResponse)PackSizeList.SelectedItem;
                    int selectedPacksizeId = selectedPacksize.PackSizeId;

                    productionRequest.PackSizeId = selectedPacksizeId;
                    if (selectedPacksizeId > 0)
                    {
                        var packsize = _masterService.GetPackSizeById(selectedPacksizeId).Result;
                        frdenier.Text = packsize.FromDenier.ToString();
                        updenier.Text = packsize.UpToDenier.ToString();
                        startWeight = packsize.StartWeight;
                        endWeight = packsize.EndWeight;
                        frwt.Text = packsize.StartWeight.ToString();
                        upwt.Text = packsize.EndWeight.ToString();
                    }
                }
            }
            finally
            {
                lblLoading.Visible = false;
            }

            Log.writeMessage("Chips PackSizeList_SelectionChangeCommitted - End : " + DateTime.Now);
        }

        private void PackSizeList_TextUpdate(object sender, EventArgs e)
        {
            Log.writeMessage("Chips PackSizeList_TextUpdate - Start : " + DateTime.Now);

            System.Windows.Forms.ComboBox cb = (System.Windows.Forms.ComboBox)sender;
            string typedText = cb.Text;

            if (string.IsNullOrWhiteSpace(cb.Text))
            {
                cb.TextUpdate -= PackSizeList_TextUpdate;

                cb.SelectedIndex = 0;
                cb.Text = string.Empty;
                cb.DroppedDown = false;
                frdenier.Text = "0";
                updenier.Text = "0";
                frwt.Text = "0";
                upwt.Text = "0";

                cb.TextUpdate += PackSizeList_TextUpdate;
                return;
            }

            int cursorPosition = cb.SelectionStart;

            if (typedText.Length >= 2)
            {
                //PackSizeList.Items.Clear();

                var packsizeList = _masterService.GetPackSizeList(typedText).Result.OrderBy(x => x.PackSizeName).ToList();

                packsizeList.Insert(0, new PackSizeResponse { PackSizeId = 0, PackSizeName = "Select Pack Size" });

                PackSizeList.BeginUpdate();
                PackSizeList.DataSource = null;
                PackSizeList.DisplayMember = "PackSizeName";
                PackSizeList.ValueMember = "PackSizeId";
                PackSizeList.DataSource = packsizeList;
                PackSizeList.EndUpdate();

                PackSizeList.TextUpdate -= PackSizeList_TextUpdate;
                PackSizeList.DroppedDown = true;
                PackSizeList.Text = typedText;
                PackSizeList.SelectionStart = cursorPosition;
                PackSizeList.SelectionLength = typedText.Length;
                PackSizeList.TextUpdate += PackSizeList_TextUpdate;

            }

            Log.writeMessage("Chips PackSizeList_TextUpdate - End : " + DateTime.Now);
        }

        private void QualityList_SelectedIndexChanged(object sender, EventArgs e)
        {
            Log.writeMessage("Chips QualityList_SelectedIndexChanged - Start : " + DateTime.Now);

            if (!isFormReady) return;

            if (QualityList.SelectedValue != null)
            {
                QualityResponse selectedQuality = (QualityResponse)QualityList.SelectedItem;
                int selectedQualityId = selectedQuality.QualityId;

                productionRequest.QualityId = selectedQualityId;
            }

            Log.writeMessage("Chips QualityList_SelectedIndexChanged - End : " + DateTime.Now);
        }

        private void QualityList_TextUpdate(object sender, EventArgs e)
        {
            Log.writeMessage("Chips QualityList_TextUpdate - Start : " + DateTime.Now);

            System.Windows.Forms.ComboBox cb = (System.Windows.Forms.ComboBox)sender;
            string typedText = cb.Text;

            if (string.IsNullOrWhiteSpace(cb.Text))
            {
                cb.TextUpdate -= QualityList_TextUpdate;

                cb.SelectedIndex = 0;
                cb.Text = string.Empty;
                cb.DroppedDown = false;

                cb.TextUpdate += QualityList_TextUpdate;
                return;
            }

            int cursorPosition = cb.SelectionStart;

            if (typedText.Length >= 2)
            {
                suppressEvents = true;

                //QualityList.Items.Clear();

                var qualityList = _masterService.GetQualityListByItemTypeId(selectedItemTypeid).Result.OrderBy(x => x.Name).ToList();
                qualityList.Insert(0, new QualityResponse { QualityId = 0, Name = "Select Quality" });

                QualityList.BeginUpdate();
                QualityList.DataSource = null;
                QualityList.DisplayMember = "Name";
                QualityList.ValueMember = "QualityId";
                QualityList.DataSource = qualityList;
                QualityList.EndUpdate();

                QualityList.TextUpdate -= QualityList_TextUpdate;
                QualityList.DroppedDown = true;
                QualityList.Text = typedText;
                QualityList.SelectionStart = cursorPosition;
                QualityList.SelectionLength = typedText.Length;
                QualityList.TextUpdate += QualityList_TextUpdate;

                suppressEvents = false;
            }

            Log.writeMessage("Chips QualityList_TextUpdate - End : " + DateTime.Now);
        }

        private void WindingTypeList_SelectedIndexChanged(object sender, EventArgs e)
        {
            Log.writeMessage("Chips WindingTypeList_SelectedIndexChanged - Start : " + DateTime.Now);

            if (!isFormReady) return;

            lblLoading.Visible = true;

            try
            {
                if (WindingTypeList.SelectedValue != null)
                {
                    WindingTypeResponse selectedWindingType = (WindingTypeResponse)WindingTypeList.SelectedItem;
                    int selectedWindingTypeId = selectedWindingType.WindingTypeId;

                    if (selectedWindingTypeId > 0)
                    {
                        productionRequest.WindingTypeId = selectedWindingTypeId;
                    }
                }
            }
            finally
            {
                lblLoading.Visible = false;
            }

            Log.writeMessage("Chips WindingTypeList_SelectedIndexChanged - End : " + DateTime.Now);
        }

        private void WindingTypeList_TextUpdate(object sender, EventArgs e)
        {
            Log.writeMessage("Chips WindingTypeList_TextUpdate - Start : " + DateTime.Now);

            System.Windows.Forms.ComboBox cb = (System.Windows.Forms.ComboBox)sender;
            string typedText = cb.Text;

            if (string.IsNullOrWhiteSpace(cb.Text))
            {
                cb.TextUpdate -= WindingTypeList_TextUpdate;

                cb.SelectedIndex = 0;
                cb.Text = string.Empty;
                cb.DroppedDown = false;

                cb.TextUpdate += WindingTypeList_TextUpdate;
                return;
            }

            int cursorPosition = cb.SelectionStart;

            if (typedText.Length >= 2)
            {
                suppressEvents = true;

                //WindingTypeList.Items.Clear();

                var getWindingType = new List<WindingTypeResponse>();
                getWindingType = _productionService.getWinderTypeList(selectLotId, typedText).Result.OrderBy(x => x.WindingTypeName).ToList();
                getWindingType.Insert(0, new WindingTypeResponse { WindingTypeId = 0, WindingTypeName = "Select Winding Type" });
                if (getWindingType.Count <= 1)
                {
                    getWindingType = _masterService.GetWindingTypeList(typedText).Result.OrderBy(x => x.WindingTypeName).ToList();
                    getWindingType.Insert(0, new WindingTypeResponse { WindingTypeId = 0, WindingTypeName = "Select Winding Type" });

                }

                WindingTypeList.BeginUpdate();
                WindingTypeList.DataSource = null;
                WindingTypeList.DisplayMember = "WindingTypeName";
                WindingTypeList.ValueMember = "WindingTypeId";
                WindingTypeList.DataSource = getWindingType;
                WindingTypeList.EndUpdate();

                WindingTypeList.TextUpdate -= WindingTypeList_TextUpdate;
                WindingTypeList.DroppedDown = true;
                WindingTypeList.Text = typedText;
                WindingTypeList.SelectionStart = cursorPosition;
                WindingTypeList.SelectionLength = typedText.Length;
                WindingTypeList.TextUpdate += WindingTypeList_TextUpdate;

                suppressEvents = false;
            }

            Log.writeMessage("Chips WindingTypeList_TextUpdate - End : " + DateTime.Now);
        }

        private async void RefreshGradewiseGrid()
        {
            Log.writeMessage("Chips RefreshGradewiseGrid - Start : " + DateTime.Now);

            if (productionRequest.QualityId != 0)
            {
                balanceQty = 0;
                //int selectedQualityId = Convert.ToInt32(QualityList.SelectedValue.ToString());
                var getProductionByQuality = _packingService.getAllByLotIdandSaleOrderItemIdandPackingType(selectLotId, selectedSOId).Result;
                List<QualityGridResponse> gridList = new List<QualityGridResponse>();
                foreach (var quality in getProductionByQuality)
                {
                    var existing = gridList.FirstOrDefault(x => x.QualityId == quality.QualityId && x.SaleOrderItemsId == quality.SaleOrderItemsId);

                    if (existing == null)
                    {
                        QualityGridResponse grid = new QualityGridResponse();
                        grid.QualityId = quality.QualityId;
                        grid.SaleOrderItemsId = quality.SaleOrderItemsId;
                        grid.QualityName = quality.QualityName;
                        grid.SaleOrderQty = totalSOQty;
                        grid.GrossWt = quality.GrossWt;

                        gridList.Add(grid);
                    }
                    else
                    {
                        existing.GrossWt += quality.GrossWt;
                    }

                }                
            }

            Log.writeMessage("Chips RefreshGradewiseGrid - End : " + DateTime.Now);
        }

        private async void RefreshLastBoxDetails()
        {
            Log.writeMessage("Chips RefreshLastBoxDetails - Start : " + DateTime.Now);

            var getLastBox = _packingService.getLastBoxDetails("chipspacking").Result;

            //lastboxdetails
            if (getLastBox.ProductionId > 0)
            {
                _productionId = getLastBox.ProductionId;
                await LoadProductionDetailsAsync(getLastBox);

                this.copstxtbox.Text = getLastBox.Spools.ToString();
                this.tarewghttxtbox.Text = getLastBox.TareWt.ToString();
                this.grosswttxtbox.Text = getLastBox.GrossWt.ToString();
                this.netwttxtbox.Text = getLastBox.NetWt.ToString();
                this.lastbox.Text = getLastBox.BoxNoFmtd.ToString();
            }

            Log.writeMessage("Chips RefreshLastBoxDetails - End : " + DateTime.Now);
        }

        private void ComPortList_SelectedIndexChanged(object sender, EventArgs e)
        {
            Log.writeMessage("Chips ComPortList_SelectedIndexChanged - Start : " + DateTime.Now);

            if (!isFormReady) return;

            if (ComPortList.SelectedValue != null)
            {
                var ComPort = ComPortList.SelectedValue.ToString();
                comPort = ComPortList.SelectedValue.ToString();
            }

            Log.writeMessage("Chips ComPortList_SelectedIndexChanged - End : " + DateTime.Now);
        }

        private void WeighingList_SelectedIndexChanged(object sender, EventArgs e)
        {
            Log.writeMessage("Chips WeighingList_SelectedIndexChanged - Start : " + DateTime.Now);

            if (!isFormReady) return;

            if (WeighingList.SelectedValue != null)
            {
                WeighingItem selectedWeighingScale = (WeighingItem)WeighingList.SelectedItem;
                int selectedScaleId = selectedWeighingScale.Id;

                if (selectedScaleId >= 0 && !string.IsNullOrEmpty(comPort))
                {
                    var readWeight = wtReader.ReadWeight(comPort, selectedScaleId);
                    if (readWeight != null && (!string.IsNullOrEmpty(readWeight)))
                    {
                        grosswtno.Text = readWeight.ToString();
                        grosswtno.ReadOnly = true;
                        grosswtno.BackColor = System.Drawing.SystemColors.ButtonHighlight;
                    }
                }
            }

            Log.writeMessage("Chips WeighingList_SelectedIndexChanged - End : " + DateTime.Now);
        }

        private async void BoxItemList_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Log.writeMessage("Chips BoxItemList_SelectionChangeCommitted - Start : " + DateTime.Now);

            if (!isFormReady) return;

            if (BoxItemList.SelectedIndex <= 0)
            {
                boxpalletitemwt.Text = "0";
                palletwtno.Text = "0";
                return;
            }

            lblLoading.Visible = true;
            try
            {
                if (BoxItemList.SelectedValue != null)
                {
                    ItemResponse selectedBoxItem = (ItemResponse)BoxItemList.SelectedItem;
                    int selectedBoxItemId = selectedBoxItem.ItemId;

                    if (selectedBoxItemId > 0)
                    {
                        productionRequest.BoxItemId = selectedBoxItemId;
                        var itemResponse = _masterService.GetItemById(selectedBoxItemId).Result;
                        if (itemResponse != null)
                        {
                            boxpalletitemwt.Text = itemResponse.Weight.ToString();
                            palletwtno.Text = itemResponse.Weight.ToString();
                        }
                    }
                }
            }
            finally
            {
                lblLoading.Visible = false;
            }

            Log.writeMessage("Chips BoxItemList_SelectionChangeCommitted - End : " + DateTime.Now);
        }

        private void BoxItemList_TextUpdate(object sender, EventArgs e)
        {
            Log.writeMessage("Chips BoxItemList_TextUpdate - Start : " + DateTime.Now);

            System.Windows.Forms.ComboBox cb = (System.Windows.Forms.ComboBox)sender;
            string typedText = cb.Text;

            if (string.IsNullOrWhiteSpace(cb.Text))
            {
                cb.TextUpdate -= BoxItemList_TextUpdate;

                cb.SelectedIndex = 0;
                cb.Text = string.Empty;
                cb.DroppedDown = false;
                boxpalletitemwt.Text = "0";
                palletwtno.Text = "0";

                cb.TextUpdate += BoxItemList_TextUpdate;
                return;
            }

            int cursorPosition = cb.SelectionStart;

            if (typedText.Length >= 2)
            {
                //BoxItemList.Items.Clear();

                var boxitemList = _masterService.GetItemList(itemBoxCategoryId, typedText).Result.OrderBy(x => x.Name).ToList();

                boxitemList.Insert(0, new ItemResponse { ItemId = 0, Name = "Select Box/Pallet" });

                BoxItemList.BeginUpdate();
                BoxItemList.DataSource = null;
                BoxItemList.DisplayMember = "Name";
                BoxItemList.ValueMember = "ItemId";
                BoxItemList.DataSource = boxitemList;
                BoxItemList.EndUpdate();

                BoxItemList.TextUpdate -= BoxItemList_TextUpdate;
                BoxItemList.DroppedDown = true;
                BoxItemList.Text = typedText;
                BoxItemList.SelectionStart = cursorPosition;
                BoxItemList.SelectionLength = typedText.Length;
                BoxItemList.TextUpdate += BoxItemList_TextUpdate;

            }
            Log.writeMessage("Chips BoxItemList_TextUpdate - End : " + DateTime.Now);
        }

        private void PrefixList_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Log.writeMessage("Chips PrefixList_SelectionChangeCommitted - Start : " + DateTime.Now);

            if (!isFormReady) return;

            if (PrefixList.SelectedIndex <= 0)
            {
                prodtype.Text = "";
                return;
            }

            if (PrefixList.SelectedValue != null)
            {
                PrefixResponse selectedPrefix = (PrefixResponse)PrefixList.SelectedItem;
                int selectedPrefixId = selectedPrefix.PrefixCode;

                productionRequest.PrefixCode = selectedPrefixId;

                var deptTask = _masterService.GetDepartmentList(selectedPrefix.Department).Result;
                deptTask.Insert(0, new DepartmentResponse { DepartmentId = 0, DepartmentName = "Select Dept" });
                DeptList.SelectedIndexChanged -= DeptList_SelectedIndexChanged;
                DeptList.DataSource = deptTask;
                DeptList.SelectedValue = selectedPrefix.DepartmentId;
                selectedDeptId = selectedPrefix.DepartmentId;
                productionRequest.DepartmentId = selectedDeptId;
                DeptList.DisplayMember = "DepartmentName";
                DeptList.ValueMember = "DepartmentId";
                if (DeptList.Items.Count > 1)
                {
                    DeptList.SelectedIndex = 1;
                }
                DeptList.SelectedIndexChanged += DeptList_SelectedIndexChanged;
                List<MachineResponse> machineList = new List<MachineResponse>();
                if (selectedDeptId != 0)
                {
                    machineList = _masterService.GetMachineByDepartmentIdAndLotType(selectedDeptId, "ChipsLot").Result;

                    machineList.Insert(0, new MachineResponse { MachineId = 0, MachineName = "Select Line No." });
                    //LineNoList.DataSource = machineList;

                    var isExist = machineList.Where(x => x.MachineId == selectedMachineid).Any();
                    if (!isExist)
                    {
                        LineNoList.BeginUpdate();
                        LineNoList.DataSource = null;
                        LineNoList.DisplayMember = "MachineName";
                        LineNoList.ValueMember = "MachineId";
                        LineNoList.DataSource = machineList;
                        LineNoList.EndUpdate();
                    }
                }

                if (selectedPrefix.ProductionType.ToString() != null)
                {
                    prodtype.Text = selectedPrefix.ProductionType.ToString();
                    productionRequest.ProdTypeId = selectedPrefix.ProductionTypeId;
                }
            }

            Log.writeMessage("Chips PrefixList_SelectionChangeCommitted - End : " + DateTime.Now);
        }

        private void PrefixList_TextUpdate(object sender, EventArgs e)
        {
            Log.writeMessage("Chips PrefixList_TextUpdate - Start : " + DateTime.Now);

            System.Windows.Forms.ComboBox cb = (System.Windows.Forms.ComboBox)sender;
            string typedText = cb.Text;

            if (string.IsNullOrWhiteSpace(cb.Text))
            {
                cb.TextUpdate -= PrefixList_TextUpdate;

                cb.SelectedIndex = 0;
                cb.Text = string.Empty;
                cb.DroppedDown = false;
                prodtype.Text = "";

                cb.TextUpdate += PrefixList_TextUpdate;
                return;
            }

            int cursorPosition = cb.SelectionStart;

            if (typedText.Length >= 2)
            {
                //PrefixList.Items.Clear();

                prefixRequest.DepartmentId = selectedDeptId;
                prefixRequest.TxnFlag = "Chp";
                prefixRequest.TransactionTypeId = 5;
                prefixRequest.ProductionTypeId = 1;
                prefixRequest.Prefix = "";
                prefixRequest.FinYearId = SessionManager.FinYearId;
                prefixRequest.SubString = typedText;

                List<PrefixResponse> prefixList = _masterService.GetPrefixList(prefixRequest).Result.OrderBy(x => x.Prefix).ToList();
                prefixList.Insert(0, new PrefixResponse { PrefixCode = 0, Prefix = "Select Prefix" });

                PrefixList.BeginUpdate();
                PrefixList.DataSource = null;
                PrefixList.DisplayMember = "Prefix";
                PrefixList.ValueMember = "PrefixCode";
                PrefixList.DataSource = prefixList;
                PrefixList.EndUpdate();

                PrefixList.TextUpdate -= PrefixList_TextUpdate;
                PrefixList.DroppedDown = true;
                PrefixList.Text = typedText;
                PrefixList.SelectionStart = cursorPosition;
                PrefixList.SelectionLength = typedText.Length;
                PrefixList.TextUpdate += PrefixList_TextUpdate;

            }
            Log.writeMessage("Chips PrefixList_TextUpdate - End : " + DateTime.Now);
        }

        private async void DeptList_SelectedIndexChanged(object sender, EventArgs e)
        {
            Log.writeMessage("Chips DeptList_SelectedIndexChanged - Start : " + DateTime.Now);

            if (!isFormReady) return;

            if (suppressEvents) return;

            if (DeptList.SelectedIndex <= 0)
            {
                selectedDeptId = 0;
                return;
            }
            suppressEvents = true;
            lblLoading.Visible = true;
            try
            {
                if (DeptList.SelectedValue != null)
                {
                    DepartmentResponse selectedDepartment = (DepartmentResponse)DeptList.SelectedItem;
                    int selectedDepartmentId = selectedDepartment.DepartmentId;

                    //if (selectedDepartment != null && productionRequest.MachineId == 0)
                    //{
                    //    var machineList = _masterService.GetMachineByDepartmentIdAndLotType(selectedDepartmentId, "ChipsLot").Result;

                    //    machineList.Insert(0, new MachineResponse { MachineId = 0, MachineName = "Select Line No." });
                    //    LineNoList.DataSource = machineList;
                    //    LineNoList.SelectedValue = productionResponse.MachineId;
                    //}

                    productionRequest.DepartmentId = selectedDepartmentId;
                    selectedDeptId = selectedDepartmentId;

                    LineNoList.DataSource = null;
                    LineNoList.Items.Clear();
                    LineNoList.Items.Add("Select Line No.");
                    LineNoList.SelectedItem = "Select Line No.";

                    PrefixList.DataSource = null;
                    PrefixList.Items.Clear();
                    PrefixList.Items.Add("Select Prefix");
                    PrefixList.SelectedItem = "Select Prefix";

                    MergeNoList.DataSource = null;
                    MergeNoList.Items.Clear();
                    MergeNoList.Items.Add("Select MergeNo");
                    MergeNoList.SelectedItem = "Select MergeNo";

                    ResetLotValues();
                    prodtype.Text = "";
                    ResetDependentDropdownValues();
                    //prefixRequest.DepartmentId = selectedDepartmentId;
                    //prefixRequest.TxnFlag = "Chp";
                    //prefixRequest.TransactionTypeId = 5;
                    //prefixRequest.ProductionTypeId = 1;
                    //prefixRequest.Prefix = "";
                    //prefixRequest.FinYearId = SessionManager.FinYearId;

                    //List<PrefixResponse> prefixList = await _masterService.GetPrefixList(prefixRequest);
                    //prefixList.Insert(0, new PrefixResponse { PrefixCode = 0, Prefix = "Select Prefix" });
                    //PrefixList.DataSource = prefixList;
                    //PrefixList.DisplayMember = "Prefix";
                    //PrefixList.ValueMember = "PrefixCode";
                    //PrefixList.SelectedIndex = 0;
                    //PrefixList.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    //PrefixList.AutoCompleteSource = AutoCompleteSource.ListItems;
                    //if (PrefixList.Items.Count == 2)
                    //{
                    //    PrefixList.SelectedIndex = 1;   // Select the single record
                    //    PrefixList_SelectedIndexChanged(PrefixList, EventArgs.Empty);
                    //}
                    //else
                    //{
                    //    PrefixList.Enabled = true;      // Allow user selection
                    //    PrefixList.SelectedIndex = 0;  // Optional: no default selection
                    //}
                    //if (_productionId > 0 && productionResponse != null)
                    //{
                    //    if (selectedDepartmentId == productionResponse.DepartmentId && productionRequest.MachineId == productionResponse.MachineId)
                    //    {
                    //        PrefixList.SelectedValue = productionResponse.PrefixCode;
                    //    }
                    //}
                }
            }
            finally
            {
                lblLoading.Visible = false;
                suppressEvents = false;
            }

            Log.writeMessage("Chips DeptList_SelectedIndexChanged - End : " + DateTime.Now);
        }

        private void DeptList_TextUpdate(object sender, EventArgs e)
        {
            Log.writeMessage("Chips DeptList_TextUpdate - Start : " + DateTime.Now);

            System.Windows.Forms.ComboBox cb = (System.Windows.Forms.ComboBox)sender;
            string typedText = cb.Text;

            if (string.IsNullOrWhiteSpace(cb.Text))
            {
                cb.TextUpdate -= DeptList_TextUpdate;

                cb.SelectedIndex = 0;
                cb.Text = string.Empty;
                cb.DroppedDown = false;
                selectedDeptId = 0;

                cb.TextUpdate += DeptList_TextUpdate;
                return;
            }

            int cursorPosition = cb.SelectionStart;

            if (typedText.Length >= 2)
            {
                //DeptList.Items.Clear();

                var deptList = _masterService.GetDepartmentList(typedText).Result.OrderBy(x => x.DepartmentName).ToList();

                deptList.Insert(0, new DepartmentResponse { DepartmentId = 0, DepartmentName = "Select Dept" });

                DeptList.BeginUpdate();
                DeptList.DataSource = null;
                DeptList.DisplayMember = "DepartmentName";
                DeptList.ValueMember = "DepartmentId";
                DeptList.DataSource = deptList;
                DeptList.EndUpdate();

                DeptList.TextUpdate -= DeptList_TextUpdate;
                DeptList.DroppedDown = true;
                DeptList.Text = typedText;
                DeptList.SelectionStart = cursorPosition;
                DeptList.SelectionLength = typedText.Length;
                DeptList.TextUpdate += DeptList_TextUpdate;

            }
            Log.writeMessage("Chips DeptList_TextUpdate - End : " + DateTime.Now);
        }

        private async void OwnerList_SelectedIndexChanged(object sender, EventArgs e)
        {
            Log.writeMessage("Chips OwnerList_SelectedIndexChanged - Start : " + DateTime.Now);

            if (!isFormReady) return;

            if (OwnerList.SelectedIndex <= 0)
            {
                return;
            }
            if (OwnerList.SelectedIndex > 0)
            {
            }
            lblLoading.Visible = true;
            try
            {
                if (OwnerList.SelectedValue != null)
                {

                    BusinessPartnerResponse selectedOwner = (BusinessPartnerResponse)OwnerList.SelectedItem;
                    int selectedOwnerId = selectedOwner.BusinessPartnerId;

                    productionRequest.OwnerId = selectedOwnerId;
                }
            }
            finally
            {
                lblLoading.Visible = false;
            }

            Log.writeMessage("Chips OwnerList_SelectedIndexChanged - End : " + DateTime.Now);
        }

        private void OwnerList_TextUpdate(object sender, EventArgs e)
        {
            Log.writeMessage("Chips OwnerList_TextUpdate - Start : " + DateTime.Now);

            System.Windows.Forms.ComboBox cb = (System.Windows.Forms.ComboBox)sender;
            string typedText = cb.Text;

            if (string.IsNullOrWhiteSpace(cb.Text))
            {
                cb.TextUpdate -= OwnerList_TextUpdate;

                cb.SelectedIndex = 0;
                cb.Text = string.Empty;
                cb.DroppedDown = false;

                cb.TextUpdate += OwnerList_TextUpdate;
                return;
            }

            int cursorPosition = cb.SelectionStart;

            if (typedText.Length >= 2)
            {
                //OwnerList.Items.Clear();

                var ownerList = _masterService.GetOwnerList(typedText).Result.OrderBy(x => x.LegalName).ToList();

                ownerList.Insert(0, new BusinessPartnerResponse { BusinessPartnerId = 0, LegalName = "Select Owner" });

                OwnerList.BeginUpdate();
                OwnerList.DataSource = null;
                OwnerList.DisplayMember = "LegalName";
                OwnerList.ValueMember = "BusinessPartnerId";
                OwnerList.DataSource = ownerList;
                OwnerList.EndUpdate();

                OwnerList.TextUpdate -= OwnerList_TextUpdate;
                OwnerList.DroppedDown = true;
                OwnerList.Text = typedText;
                OwnerList.SelectionStart = cursorPosition;
                OwnerList.SelectionLength = typedText.Length;
                OwnerList.TextUpdate += OwnerList_TextUpdate;

            }
            Log.writeMessage("Chips OwnerList_TextUpdate - End : " + DateTime.Now);
        }

        private async Task<List<string>> getComPortList()
        {
            Log.writeMessage("Chips getComPortList - Start : " + DateTime.Now);

            var getComPortType = new List<string>
            {
                "Select Com Port",
                "COM1",
                "COM2",
                "COM3",
                "COM4"
            };

            Log.writeMessage("Chips getComPortList - End : " + DateTime.Now);

            return getComPortType;
        }

        private async Task<List<WeighingItem>> GetWeighingList()
        {
            Log.writeMessage("Chips GetWeighingList - Start : " + DateTime.Now);

            var getWeighingScale = new List<WeighingItem>
            {
                new WeighingItem { Id = -1, Name = "Select Weigh Scale" },
                new WeighingItem { Id = 0, Name = "Old" },
                new WeighingItem { Id = 1, Name = "Unique" },
                new WeighingItem { Id = 2, Name = "JISL (9600)" },
                new WeighingItem { Id = 3, Name = "JISL (2400)" }
            };

            Log.writeMessage("Chips GetWeighingList - End : " + DateTime.Now);

            return getWeighingScale;
        }

        private void PalletWeight_TextChanged(object sender, EventArgs e)
        {
            Log.writeMessage("Chips PalletWeight_TextChanged - Start : " + DateTime.Now);

            if (string.IsNullOrWhiteSpace(palletwtno.Text))
            {
                CalculateTareWeight();
            }
            else
            {
                CalculateTareWeight();
            }

            Log.writeMessage("Chips PalletWeight_TextChanged - End : " + DateTime.Now);
        }

        private void CalculateTareWeight()
        {
            Log.writeMessage("Chips CalculateTareWeight - Start : " + DateTime.Now);

            decimal num2 = 0;

            decimal.TryParse(palletwtno.Text, out num2);

            tarewt.Text = (num2).ToString("F3");
            if (!string.IsNullOrWhiteSpace(grosswtno.Text) && !string.IsNullOrWhiteSpace(tarewt.Text))
            {
                decimal gross, tare;
                if (decimal.TryParse(grosswtno.Text, out gross) && decimal.TryParse(tarewt.Text, out tare))
                {
                    if (gross >= tare)
                    {
                        CalculateNetWeight();
                        grosswterror.Visible = false;
                    }
                }
            }

            Log.writeMessage("Chips CalculateTareWeight - End : " + DateTime.Now);
        }

        private void GrossWeight_Validating(object sender, CancelEventArgs e)
        {
            Log.writeMessage("Chips GrossWeight_Validating - Start : " + DateTime.Now);

            if (!isFormReady) return;

            if (string.IsNullOrWhiteSpace(grosswtno.Text))
            {
                MessageBox.Show("Please enter gross weight", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true;
                return;
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(tarewt.Text))
                {
                    decimal gross, tare;
                    if (decimal.TryParse(grosswtno.Text, out gross) && decimal.TryParse(tarewt.Text, out tare))
                    {
                        if (gross >= tare)
                        {
                            CalculateNetWeight();
                        }
                    }
                }
            }

            Log.writeMessage("Chips GrossWeight_Validating - End : " + DateTime.Now);
        }

        private void CalculateNetWeight()
        {
            Log.writeMessage("Chips CalculateNetWeight - Start : " + DateTime.Now);

            decimal num1 = 0, num2 = 0;

            decimal.TryParse(grosswtno.Text, out num1);
            decimal.TryParse(tarewt.Text, out num2);
            if (num1 > num2)
            {
                netwt.Text = (num1 - num2).ToString("F3");
                CalculateWeightPerCop();
            }

            Log.writeMessage("Chips CalculateNetWeight - End : " + DateTime.Now);
        }

        private void NetWeight_TextChanged(object sender, EventArgs e)
        {
            Log.writeMessage("Chips NetWeight_TextChanged - Start : " + DateTime.Now);

            CalculateWeightPerCop();

            Log.writeMessage("Chips NetWeight_TextChanged - End : " + DateTime.Now);
        }

        private void CalculateWeightPerCop()
        {
            Log.writeMessage("Chips CalculateWeightPerCop - Start : " + DateTime.Now);

            decimal num1 = 0;

            decimal.TryParse(netwt.Text, out num1);
            if (num1 > 0)
            {
                wtpercop.Text = (num1).ToString("F3");                
            }

            Log.writeMessage("Chips CalculateWeightPerCop - End : " + DateTime.Now);
        }

        private void CopyNos_TextChanged(object sender, EventArgs e)
        {
            Log.writeMessage("Chips CopyNos_TextChanged - Start : " + DateTime.Now);

            if (string.IsNullOrWhiteSpace(copyno.Text))
            {
                copynoerror.Visible = true;
            }
            else
            {
                copynoerror.Text = "";
                copynoerror.Visible = false;
            }

            Log.writeMessage("Chips CopyNos_TextChanged - End : " + DateTime.Now);
        }

        private async void submit_Click(object sender, EventArgs e)
        {
            Log.writeMessage("Chips submit_Click - Start : " + DateTime.Now);

            submitForm(false);

            Log.writeMessage("Chips submit_Click - End : " + DateTime.Now);
        }

        private async void saveprint_Click(object sender, EventArgs e)
        {
            Log.writeMessage("Chips saveprint_Click - Start : " + DateTime.Now);

            submitForm(true);

            Log.writeMessage("Chips saveprint_Click - End : " + DateTime.Now);
        }

        public async void submitForm(bool isPrint)
        {
            Log.writeMessage("Chips submitForm - Start : " + DateTime.Now);

            if (ValidateForm())
            {
                productionRequest.OwnerId = this.OwnerList.SelectedIndex <= 0 ? 0 : productionRequest.OwnerId;
                productionRequest.PackingType = "ChipsPacking";
                productionRequest.Remarks = remarks.Text.Trim();
                productionRequest.EmptyBoxPalletWt = Convert.ToDecimal(palletwtno.Text.Trim());
                productionRequest.GrossWt = Convert.ToDecimal(grosswtno.Text.Trim());
                productionRequest.NoOfCopies = Convert.ToInt32(copyno.Text.Trim());
                productionRequest.TareWt = Convert.ToDecimal(tarewt.Text.Trim());
                productionRequest.NetWt = Convert.ToDecimal(netwt.Text.Trim());
                productionRequest.ProductionDate = dateTimePicker1.Value;
                productionRequest.ContainerTypeId = 0;

                productionRequest.PrintCompany = prcompany.Checked;
                productionRequest.PrintOwner = prowner.Checked;
                productionRequest.PrintDate = prdate.Checked;
                productionRequest.PrintUser = pruser.Checked;
                productionRequest.PrintHindiWords = prhindi.Checked;
                productionRequest.PrintQRCode = prqrcode.Checked;
                productionRequest.PrintWTPS = prwtps.Checked;
                productionRequest.PrintTwist = prtwist.Checked;

                productionRequest.PalletDetailsRequest = new List<ProductionPalletDetailsRequest>();

                productionRequest.ConsumptionDetailsRequest = new List<ProductionConsumptionDetailsRequest>();
                foreach (var lot in lotsDetailsList)
                {
                    ProductionConsumptionDetailsRequest consumptionDetailsRequest = new ProductionConsumptionDetailsRequest();
                    consumptionDetailsRequest.Extruder = lot.Extruder;
                    consumptionDetailsRequest.InputPerc = lot.InputPerc;
                    consumptionDetailsRequest.GainLossPerc = lot.GainLossPerc;
                    consumptionDetailsRequest.ProductionPerc = lot.ProductionPerc;
                    consumptionDetailsRequest.ProductionLotId = lot.LotId;
                    consumptionDetailsRequest.InputLotId = lot.LotId;
                    consumptionDetailsRequest.InputItemId = lotResponse.ItemId;
                    consumptionDetailsRequest.InputQualityId = lot.PrevLotQualityId;
                    consumptionDetailsRequest.PropWeight = consumptionDetailsRequest.ProductionPerc * productionRequest.NetWt;
                    productionRequest.ConsumptionDetailsRequest.Add(consumptionDetailsRequest);
                }

                ProductionResponse result = SubmitPacking(productionRequest, isPrint);
            }

            Log.writeMessage("Chips submitForm - End : " + DateTime.Now);
        }

        public ProductionResponse SubmitPacking(ProductionRequest productionRequest, bool isPrint)
        {
            Log.writeMessage("Chips SubmitPacking - Start : " + DateTime.Now);

            submit.Enabled = false;
            saveprint.Enabled = false;
            ProductionResponse result = new ProductionResponse();
            result = _packingService.AddUpdatePOYPacking(0, productionRequest);
            if (result != null && result.ProductionId > 0)
            {
                submit.Enabled = true;
                saveprint.Enabled = true;
                RefreshGradewiseGrid();
                RefreshLastBoxDetails();

                ShowCustomMessage(result.BoxNoFmtd);
                isFormReady = false;
                this.grosswtno.Text = "0.000";
                this.tarewt.Text = "0.000";
                this.netwt.Text = "0.000";
                this.wtpercop.Text = "0.000";
                palletwtno.Text = boxpalletitemwt.Text;
                isFormReady = true;
                if (isPrint)
                {
                    //call ssrs report to print
                    string reportpathlink = reportPath + "/Chips";
                    string format = "PDF";

                    //set params
                    string productionId = result.ProductionId.ToString();
                    string url = $"{reportServer}?{reportpathlink}&rs:Format={format}" + $"&ProductionId={productionId}&StartDate:null=true&EndDate:null=true";

                    WebClient client = new WebClient();
                    //client.Credentials = CredentialCache.DefaultNetworkCredentials;
                    client.Credentials = new System.Net.NetworkCredential(UserName, Password, Domain);
                    //client.UseDefaultCredentials = false;

                    // Download PDF
                    byte[] bytes = client.DownloadData(url);

                    // Save to temp
                    string tempFile = System.IO.Path.Combine(System.IO.Path.GetTempPath(), "Report.pdf");
                    File.WriteAllBytes(tempFile, bytes);

                    using (var pdfDoc = PdfDocument.Load(tempFile))
                    {
                        using (var printDoc = pdfDoc.CreatePrintDocument())
                        {
                            var printerSettings = new PrinterSettings()
                            {
                                // PrinterName = "YourPrinterName", // optional, default printer if omitted
                                Copies = 1
                            };
                            // Set custom 4x4 label size
                            printDoc.DefaultPageSettings.PaperSize = new PaperSize("Label4x4", 400, 400);
                            printDoc.DefaultPageSettings.Margins = new Margins(0, 0, 0, 0); // no margins

                            printDoc.PrinterSettings = printerSettings;
                            printDoc.Print(); // sends PDF to printer
                        }
                    }

                    // 5️⃣ Clean up temp file
                    File.Delete(tempFile);
                }

            }
            else
            {
                submit.Enabled = true;
                saveprint.Enabled = true;
                MessageBox.Show("Something went wrong.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }

            Log.writeMessage("Chips SubmitPacking - End : " + DateTime.Now);

            return result;
        }

        private bool ValidateForm()
        {
            Log.writeMessage("Chips ValidateForm - Start : " + DateTime.Now);

            bool isValid = true;

            if (LineNoList.SelectedIndex <= 0)
            {
                MessageBox.Show("Please select a line no", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                isValid = false;
            }
            if (string.IsNullOrWhiteSpace(copyno.Text))
            {
                MessageBox.Show("Please enter no of copies", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                isValid = false;
            }

            if (MergeNoList.SelectedIndex <= 0)
            {
                MessageBox.Show("Please select merge no", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                isValid = false;
            }

            if (QualityList.SelectedIndex <= 0)
            {
                MessageBox.Show("Please select quality", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                isValid = false;
            }

            if (PackSizeList.SelectedIndex <= 0)
            {
                MessageBox.Show("Please select pack size", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                isValid = false;
            }

            if (WindingTypeList.SelectedIndex <= 0)
            {
                MessageBox.Show("Please select winding type", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                isValid = false;
            }

            if (PrefixList.SelectedIndex <= 0)
            {
                MessageBox.Show("Please select prefix", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                isValid = false;
            }

            if (BoxItemList.SelectedIndex <= 0)
            {
                MessageBox.Show("Please select box item", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                isValid = false;
            }

            if (string.IsNullOrWhiteSpace(palletwtno.Text) || Convert.ToDecimal(palletwtno.Text) == 0)
            {
                MessageBox.Show("Please enter pallet wt", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                isValid = false;
            }

            if (string.IsNullOrWhiteSpace(grosswtno.Text) || Convert.ToDecimal(grosswtno.Text) == 0)
            {
                MessageBox.Show("Please enter gross wt", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                isValid = false;
            }
            decimal gross, tare;
            decimal.TryParse(grosswtno.Text, out gross);
            decimal.TryParse(tarewt.Text, out tare);
            if (gross < tare)
            {
                MessageBox.Show("Gross Wt > Tare Wt", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                netwt.Text = "0";
                wtpercop.Text = "0";
                isValid = false;
            }
            decimal whtpercop = 0;
            decimal.TryParse(wtpercop.Text, out whtpercop);
            if (whtpercop >= startWeight && whtpercop <= endWeight)
            {
                //isValid = true;
            }
            else
            {
                MessageBox.Show("Weight Per Cops is out of range.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                isValid = false;
            }

            Log.writeMessage("Chips ValidateForm - End : " + DateTime.Now);

            return isValid;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Log.writeMessage("Chips btnCancel_Click - Start : " + DateTime.Now);

            ResetForm(this);

            Log.writeMessage("Chips btnCancel_Click - End : " + DateTime.Now);
        }

        private void qualityqty_Paint(object sender, PaintEventArgs e)
        {
            Log.writeMessage("Chips qualityqty_Paint - Start : " + DateTime.Now);

            _cmethod.DrawRectangleBorder((Control)sender, e, Color.LightGray, 2);

            Log.writeMessage("Chips qualityqty_Paint - End : " + DateTime.Now);
        }

        private void windinggrid_Paint(object sender, PaintEventArgs e)
        {
            Log.writeMessage("Chips windinggrid_Paint - Start : " + DateTime.Now);

            _cmethod.DrawRectangleBorder((Control)sender, e, Color.LightGray, 2);

            Log.writeMessage("Chips windinggrid_Paint - End : " + DateTime.Now);
        }

        private void ordertable_Paint(object sender, PaintEventArgs e)
        {
            Log.writeMessage("Chips ordertable_Paint - Start : " + DateTime.Now);

            _cmethod.DrawRoundedBorder((Control)sender, e, 12, Color.FromArgb(102, 163, 255), 1);

            Log.writeMessage("Chips ordertable_Paint - End : " + DateTime.Now);
        }

        private void packagingtable_Paint(object sender, PaintEventArgs e)
        {
            Log.writeMessage("Chips packagingtable_Paint - Start : " + DateTime.Now);

            _cmethod.DrawRoundedBorder((Control)sender, e, 12, Color.FromArgb(102, 163, 255), 1);

            Log.writeMessage("Chips packagingtable_Paint - End : " + DateTime.Now);
        }

        private void weightable_Paint(object sender, PaintEventArgs e)
        {
            Log.writeMessage("Chips weightable_Paint - Start : " + DateTime.Now);

            _cmethod.DrawRoundedBorder((Control)sender, e, 12, Color.FromArgb(102, 163, 255), 1);

            Log.writeMessage("Chips weightable_Paint - End : " + DateTime.Now);
        }

        private void reviewtable_Paint(object sender, PaintEventArgs e)
        {
            Log.writeMessage("Chips reviewtable_Paint - Start : " + DateTime.Now);

            _cmethod.DrawRoundedBorder((Control)sender, e, 12, Color.FromArgb(102, 163, 255), 1);

            Log.writeMessage("Chips reviewtable_Paint - End : " + DateTime.Now);
        }

        private void machineboxlayout_Paint(object sender, PaintEventArgs e)
        {
            Log.writeMessage("Chips machineboxlayout_Paint - Start : " + DateTime.Now);

            _cmethod.DrawRoundedBorder((Control)sender, e, 8, Color.FromArgb(191, 191, 191), 1);

            Log.writeMessage("Chips machineboxlayout_Paint - End : " + DateTime.Now);
        }

        private void machineboxheader_Paint(object sender, PaintEventArgs e)
        {
            Log.writeMessage("Chips machineboxheader_Paint - Start : " + DateTime.Now);

            _cmethod.DrawBottomBorder((Control)sender, e, Color.FromArgb(191, 191, 191), 1);

            Log.writeMessage("Chips machineboxheader_Paint - End : " + DateTime.Now);
        }

        private void weighboxlayout_Paint(object sender, PaintEventArgs e)
        {
            Log.writeMessage("Chips weighboxlayout_Paint - Start : " + DateTime.Now);

            _cmethod.DrawRoundedBorder((Control)sender, e, 8, Color.FromArgb(191, 191, 191), 1);

            Log.writeMessage("Chips weighboxlayout_Paint - End : " + DateTime.Now);
        }

        private void weighboxheader_Paint(object sender, PaintEventArgs e)
        {
            Log.writeMessage("Chips weighboxheader_Paint - Start : " + DateTime.Now);

            _cmethod.DrawBottomBorder((Control)sender, e, Color.FromArgb(191, 191, 191), 1);

            Log.writeMessage("Chips weighboxheader_Paint - End : " + DateTime.Now);
        }

        private void packagingboxlayout_Paint(object sender, PaintEventArgs e)
        {
            Log.writeMessage("Chips packagingboxlayout_Paint - Start : " + DateTime.Now);

            _cmethod.DrawRoundedBorder((Control)sender, e, 8, Color.FromArgb(191, 191, 191), 1);

            Log.writeMessage("Chips packagingboxlayout_Paint - End : " + DateTime.Now);
        }

        private void packagingboxheader_Paint(object sender, PaintEventArgs e)
        {
            Log.writeMessage("Chips packagingboxheader_Paint - Start : " + DateTime.Now);

            _cmethod.DrawBottomBorder((Control)sender, e, Color.FromArgb(191, 191, 191), 1);

            Log.writeMessage("Chips packagingboxheader_Paint - End : " + DateTime.Now);
        }

        private void lastboxlayout_Paint(object sender, PaintEventArgs e)
        {
            Log.writeMessage("Chips lastboxlayout_Paint - Start : " + DateTime.Now);

            _cmethod.DrawRoundedBorder((Control)sender, e, 8, Color.FromArgb(191, 191, 191), 1);

            Log.writeMessage("Chips lastboxlayout_Paint - End : " + DateTime.Now);
        }

        private void lastboxheader_Paint(object sender, PaintEventArgs e)
        {
            Log.writeMessage("Chips lastboxheader_Paint - Start : " + DateTime.Now);

            _cmethod.DrawBottomBorder((Control)sender, e, Color.FromArgb(191, 191, 191), 1);

            Log.writeMessage("Chips lastboxheader_Paint - End : " + DateTime.Now);
        }

        private void lastbxcopspanel_Paint(object sender, PaintEventArgs e)
        {
            Log.writeMessage("Chips lastbxcopspanel_Paint - Start : " + DateTime.Now);

            _cmethod.DrawRoundedDashedBorder((Control)sender, e, 8, Color.FromArgb(102, 163, 255), 1);

            Log.writeMessage("Chips lastbxcopspanel_Paint - End : " + DateTime.Now);
        }

        private void lastbxtarepanel_Paint(object sender, PaintEventArgs e)
        {
            Log.writeMessage("Chips lastbxtarepanel_Paint - Start : " + DateTime.Now);

            _cmethod.DrawRoundedDashedBorder((Control)sender, e, 8, Color.FromArgb(102, 163, 255), 1);

            Log.writeMessage("Chips lastbxtarepanel_Paint - End : " + DateTime.Now);
        }

        private void lastbxgrosswtpanel_Paint(object sender, PaintEventArgs e)
        {
            Log.writeMessage("Chips lastbxgrosswtpanel_Paint - Start : " + DateTime.Now);

            _cmethod.DrawRoundedDashedBorder((Control)sender, e, 8, Color.FromArgb(102, 163, 255), 1);

            Log.writeMessage("Chips lastbxgrosswtpanel_Paint - End : " + DateTime.Now);
        }

        private void lastbxnetwtpanel_Paint(object sender, PaintEventArgs e)
        {
            Log.writeMessage("Chips lastbxnetwtpanel_Paint - Start : " + DateTime.Now);

            _cmethod.DrawRoundedDashedBorder((Control)sender, e, 8, Color.FromArgb(102, 163, 255), 1);

            Log.writeMessage("Chips lastbxnetwtpanel_Paint - End : " + DateTime.Now);
        }

        private void printingdetailslayout_Paint(object sender, PaintEventArgs e)
        {
            Log.writeMessage("Chips printingdetailslayout_Paint - Start : " + DateTime.Now);

            _cmethod.DrawRoundedBorder((Control)sender, e, 8, Color.FromArgb(191, 191, 191), 1);

            Log.writeMessage("Chips printingdetailslayout_Paint - End : " + DateTime.Now);
        }

        private void printingdetailsheader_Paint(object sender, PaintEventArgs e)
        {
            Log.writeMessage("Chips printingdetailsheader_Paint - Start : " + DateTime.Now);

            _cmethod.DrawBottomBorder((Control)sender, e, Color.FromArgb(191, 191, 191), 1);

            Log.writeMessage("Chips printingdetailsheader_Paint - End : " + DateTime.Now);
        }

        private void palletdetailslayout_Paint(object sender, PaintEventArgs e)
        {
            Log.writeMessage("Chips palletdetailslayout_Paint - Start : " + DateTime.Now);

            _cmethod.DrawRoundedBorder((Control)sender, e, 8, Color.FromArgb(191, 191, 191), 1);

            Log.writeMessage("Chips palletdetailslayout_Paint - End : " + DateTime.Now);
        }

        private void palletdetailsheader_Paint(object sender, PaintEventArgs e)
        {
            Log.writeMessage("Chips palletdetailsheader_Paint - Start : " + DateTime.Now);

            _cmethod.DrawBottomBorder((Control)sender, e, Color.FromArgb(191, 191, 191), 1);

            Log.writeMessage("Chips palletdetailsheader_Paint - End : " + DateTime.Now);
        }

        private void machineboxheader_Resize(object sender, EventArgs e)
        {
            Log.writeMessage("Chips machineboxheader_Resize - Start : " + DateTime.Now);

            _cmethod.SetTopRoundedRegion(machineboxheader, 8);

            Log.writeMessage("Chips machineboxheader_Resize - End : " + DateTime.Now);
        }

        private void weighboxheader_Resize(object sender, EventArgs e)
        {
            Log.writeMessage("Chips weighboxheader_Resize - Start : " + DateTime.Now);

            _cmethod.SetTopRoundedRegion(weighboxheader, 8);

            Log.writeMessage("Chips weighboxheader_Resize - End : " + DateTime.Now);
        }

        private void packagingboxheader_Resize(object sender, EventArgs e)
        {
            Log.writeMessage("Chips packagingboxheader_Resize - Start : " + DateTime.Now);

            _cmethod.SetTopRoundedRegion(packagingboxheader, 8);

            Log.writeMessage("Chips packagingboxheader_Resize - End : " + DateTime.Now);
        }

        private void lastboxheader_Resize(object sender, EventArgs e)
        {
            Log.writeMessage("Chips lastboxheader_Resize - Start : " + DateTime.Now);

            _cmethod.SetTopRoundedRegion(lastboxheader, 8);

            Log.writeMessage("Chips lastboxheader_Resize - End : " + DateTime.Now);
        }

        private void printingdetailsheader_Resize(object sender, EventArgs e)
        {
            Log.writeMessage("Chips printingdetailsheader_Resize - Start : " + DateTime.Now);

            _cmethod.SetTopRoundedRegion(printingdetailsheader, 8);

            Log.writeMessage("Chips printingdetailsheader_Resize - End : " + DateTime.Now);
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            Log.writeMessage("Chips textBox1_KeyPress - Start : " + DateTime.Now);

            if (sender is System.Windows.Forms.TextBox txt)
            {
                if (char.IsControl(e.KeyChar))
                    return;

                // Allow digits
                if (char.IsDigit(e.KeyChar))
                    return;

                // Allow only one decimal point
                if (e.KeyChar == '.' && !txt.Text.Contains('.'))
                    return;

                // Block everything else
                e.Handled = true;
            }

            Log.writeMessage("Chips textBox1_KeyPress - End : " + DateTime.Now);
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            Log.writeMessage("Chips textBox1_KeyDown - Start : " + DateTime.Now);

            // Select all text when the textbox receives focus via keyboard (Enter key)
            if (e.KeyCode == Keys.Enter)
            {
                ((System.Windows.Forms.TextBox)sender).SelectAll();
            }

            if (e.Control && e.KeyCode == Keys.V) // Ctrl+V paste
            {
                ((System.Windows.Forms.TextBox)sender).SelectAll();
                ((System.Windows.Forms.TextBox)sender).Clear(); // clear existing value before paste
            }

            Log.writeMessage("Chips textBox1_KeyDown - End : " + DateTime.Now);
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            Log.writeMessage("Chips textBox1_Enter - Start : " + DateTime.Now);

            System.Windows.Forms.TextBox tb = sender as System.Windows.Forms.TextBox;

            if (!string.IsNullOrEmpty(tb.Text))
                tb.SelectAll();

            Log.writeMessage("Chips textBox1_Enter - End : " + DateTime.Now);
        }

        private void checkBox1_KeyDown(object sender, KeyEventArgs e)
        {
            Log.writeMessage("Chips checkBox1_KeyDown - Start : " + DateTime.Now);

            if (e.KeyCode == Keys.Enter)
            {
                System.Windows.Forms.CheckBox cb = sender as System.Windows.Forms.CheckBox;
                if (cb != null)
                {
                    cb.Checked = !cb.Checked; // toggle the checkbox
                    e.Handled = true;          // prevent beep
                }
            }
            else
            {
                // For Tab (and other keys), don't mark as handled
                e.Handled = false;
            }

            Log.writeMessage("Chips checkBox1_KeyDown - End : " + DateTime.Now);
        }

        private void LineNoList_KeyDown(object sender, KeyEventArgs e)
        {
            Log.writeMessage("Chips LineNoList_KeyDown - Start : " + DateTime.Now);

            if (e.KeyCode == Keys.ShiftKey) // Detect Shift key
            {
                LineNoList.DroppedDown = true; // Open the dropdown list
                e.SuppressKeyPress = true;    // Prevent any side effect
            }
            if (e.KeyCode == Keys.Escape)
            {
                LineNoList.DroppedDown = false;
            }
            if (e.KeyCode == Keys.F2) // Detect F2 key
            {
                LineNoList.DataSource = null;
                var machineList = _masterService.GetMachineList("ChipsLot", "").Result.OrderBy(x => x.MachineName).ToList();
                machineList.Insert(0, new MachineResponse { MachineId = 0, MachineName = "Select Line No." });
                LineNoList.DataSource = machineList;
                LineNoList.DisplayMember = "MachineName";
                LineNoList.ValueMember = "MachineId";
                LineNoList.SelectedIndex = 0;
                LineNoList.DroppedDown = true; // Open the dropdown list
                e.SuppressKeyPress = true;    // Prevent any side effect
            }

            Log.writeMessage("Chips LineNoList_KeyDown - End : " + DateTime.Now);
        }

        private void MergeNoList_KeyDown(object sender, KeyEventArgs e)
        {
            Log.writeMessage("Chips MergeNoList_KeyDown - Start : " + DateTime.Now);

            if (e.KeyCode == Keys.ShiftKey) // Detect Shift key
            {
                MergeNoList.DroppedDown = true; // Open the dropdown list
                e.SuppressKeyPress = true;    // Prevent any side effect
            }
            if (e.KeyCode == Keys.Escape)
            {
                MergeNoList.DroppedDown = false;
            }

            Log.writeMessage("Chips MergeNoList_KeyDown - End : " + DateTime.Now);
        }

        private void PackSizeList_KeyDown(object sender, KeyEventArgs e)
        {
            Log.writeMessage("Chips PackSizeList_KeyDown - Start : " + DateTime.Now);

            if (e.KeyCode == Keys.ShiftKey) // Detect Shift key
            {
                PackSizeList.DroppedDown = true; // Open the dropdown list
                e.SuppressKeyPress = true;    // Prevent any side effect
            }
            if (e.KeyCode == Keys.Escape)
            {
                PackSizeList.DroppedDown = false;
            }
            if (e.KeyCode == Keys.F2) // Detect F2 key
            {
                PackSizeList.DataSource = null;
                var packsizeList = _masterService.GetPackSizeList("").Result.OrderBy(x => x.PackSizeName).ToList();
                packsizeList.Insert(0, new PackSizeResponse { PackSizeId = 0, PackSizeName = "Select Pack Size" });
                PackSizeList.DisplayMember = "PackSizeName";
                PackSizeList.ValueMember = "PackSizeId";
                PackSizeList.DataSource = packsizeList;
                PackSizeList.SelectedIndex = 0;
                PackSizeList.DroppedDown = true; // Open the dropdown list
                e.SuppressKeyPress = true;    // Prevent any side effect
            }

            Log.writeMessage("Chips PackSizeList_KeyDown - End : " + DateTime.Now);
        }

        private void QualityList_KeyDown(object sender, KeyEventArgs e)
        {
            Log.writeMessage("Chips QualityList_KeyDown - Start : " + DateTime.Now);

            if (e.KeyCode == Keys.ShiftKey) // Detect Shift key
            {
                QualityList.DroppedDown = true; // Open the dropdown list
                e.SuppressKeyPress = true;    // Prevent any side effect
            }
            if (e.KeyCode == Keys.Escape)
            {
                QualityList.DroppedDown = false;
            }

            Log.writeMessage("Chips QualityList_KeyDown - End : " + DateTime.Now);
        }

        private void PrefixList_KeyDown(object sender, KeyEventArgs e)
        {
            Log.writeMessage("Chips PrefixList_KeyDown - Start : " + DateTime.Now);

            if (e.KeyCode == Keys.ShiftKey) // Detect Shift key
            {
                PrefixList.DroppedDown = true; // Open the dropdown list
                e.SuppressKeyPress = true;    // Prevent any side effect
            }
            if (e.KeyCode == Keys.Escape)
            {
                PrefixList.DroppedDown = false;
            }
            if (e.KeyCode == Keys.F2) // Detect F2 key
            {
                prefixRequest.DepartmentId = 0;
                prefixRequest.TxnFlag = "Chp";
                prefixRequest.TransactionTypeId = 5;
                prefixRequest.ProductionTypeId = 1;
                prefixRequest.Prefix = "";
                prefixRequest.FinYearId = SessionManager.FinYearId;
                prefixRequest.GetAllFlag = true;

                PrefixList.DataSource = null;
                List<PrefixResponse> prefixList = _masterService.GetPrefixList(prefixRequest).Result.OrderBy(x => x.Prefix).ToList();
                prefixList.Insert(0, new PrefixResponse { PrefixCode = 0, Prefix = "Select Prefix" });
                PrefixList.DisplayMember = "Prefix";
                PrefixList.ValueMember = "PrefixCode";
                PrefixList.DataSource = prefixList;
                PrefixList.SelectedIndex = 0;
                PrefixList.DroppedDown = true; // Open the dropdown list
                e.SuppressKeyPress = true;    // Prevent any side effect
            }

            Log.writeMessage("Chips PrefixList_KeyDown - End : " + DateTime.Now);
        }

        private void WindingTypeList_KeyDown(object sender, KeyEventArgs e)
        {
            Log.writeMessage("Chips WindingTypeList_KeyDown - Start : " + DateTime.Now);

            if (e.KeyCode == Keys.ShiftKey) // Detect Shift key
            {
                WindingTypeList.DroppedDown = true; // Open the dropdown list
                e.SuppressKeyPress = true;    // Prevent any side effect
            }
            if (e.KeyCode == Keys.Escape)
            {
                WindingTypeList.DroppedDown = false;
            }
            if (e.KeyCode == Keys.F2) // Detect F2 key
            {
                WindingTypeList.DataSource = null;
                var getWindingType = new List<WindingTypeResponse>();
                getWindingType = _productionService.getWinderTypeList(selectLotId, "").Result.OrderBy(x => x.WindingTypeName).ToList();
                getWindingType.Insert(0, new WindingTypeResponse { WindingTypeId = 0, WindingTypeName = "Select Winding Type" });
                if (getWindingType.Count <= 1)
                {
                    getWindingType = _masterService.GetWindingTypeList("").Result.OrderBy(x => x.WindingTypeName).ToList();
                    getWindingType.Insert(0, new WindingTypeResponse { WindingTypeId = 0, WindingTypeName = "Select Winding Type" });
                }
                WindingTypeList.DisplayMember = "WindingTypeName";
                WindingTypeList.ValueMember = "WindingTypeId";
                WindingTypeList.DataSource = getWindingType;
                WindingTypeList.SelectedIndex = 0;
                WindingTypeList.DroppedDown = true; // Open the dropdown list
                e.SuppressKeyPress = true;    // Prevent any side effect
            }

            Log.writeMessage("Chips WindingTypeList_KeyDown - End : " + DateTime.Now);
        }

        private void ComPortList_KeyDown(object sender, KeyEventArgs e)
        {
            Log.writeMessage("Chips ComPortList_KeyDown - Start : " + DateTime.Now);

            if (e.KeyCode == Keys.ShiftKey) // Detect Shift key
            {
                ComPortList.DroppedDown = true; // Open the dropdown list
                e.SuppressKeyPress = true;    // Prevent any side effect
            }
            if (e.KeyCode == Keys.Escape)
            {
                ComPortList.DroppedDown = false;
            }

            Log.writeMessage("Chips ComPortList_KeyDown - End : " + DateTime.Now);
        }

        private void WeighingList_KeyDown(object sender, KeyEventArgs e)
        {
            Log.writeMessage("Chips WeighingList_KeyDown - Start : " + DateTime.Now);

            if (e.KeyCode == Keys.ShiftKey) // Detect Shift key
            {
                WeighingList.DroppedDown = true; // Open the dropdown list
                e.SuppressKeyPress = true;    // Prevent any side effect
            }
            if (e.KeyCode == Keys.Escape)
            {
                WeighingList.DroppedDown = false;
            }

            Log.writeMessage("Chips WeighingList_KeyDown - End : " + DateTime.Now);
        }

        private void BoxItemList_KeyDown(object sender, KeyEventArgs e)
        {
            Log.writeMessage("Chips BoxItemList_KeyDown - Start : " + DateTime.Now);

            if (e.KeyCode == Keys.ShiftKey) // Detect Shift key
            {
                BoxItemList.DroppedDown = true; // Open the dropdown list
                e.SuppressKeyPress = true;    // Prevent any side effect
            }
            if (e.KeyCode == Keys.Escape)
            {
                BoxItemList.DroppedDown = false;
            }
            if (e.KeyCode == Keys.F2) // Detect F2 key
            {
                BoxItemList.DataSource = null;
                var boxitemList = _masterService.GetItemList(itemBoxCategoryId, "").Result.OrderBy(x => x.Name).ToList();
                boxitemList.Insert(0, new ItemResponse { ItemId = 0, Name = "Select Box/Pallet" });
                BoxItemList.DisplayMember = "Name";
                BoxItemList.ValueMember = "ItemId";
                BoxItemList.DataSource = boxitemList;
                BoxItemList.SelectedIndex = 0;
                BoxItemList.DroppedDown = true; // Open the dropdown list
                e.SuppressKeyPress = true;    // Prevent any side effect
            }

            Log.writeMessage("Chips BoxItemList_KeyDown - End : " + DateTime.Now);
        }

        private void DeptList_KeyDown(object sender, KeyEventArgs e)
        {
            Log.writeMessage("Chips DeptList_KeyDown - Start : " + DateTime.Now);

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
                var deptList = _masterService.GetDepartmentList("").Result.OrderBy(x => x.DepartmentName).ToList();
                deptList.Insert(0, new DepartmentResponse { DepartmentId = 0, DepartmentName = "Select Dept" });
                DeptList.DisplayMember = "DepartmentName";
                DeptList.ValueMember = "DepartmentId";
                DeptList.DataSource = deptList;
                DeptList.SelectedIndex = 0;
                DeptList.DroppedDown = true; // Open the dropdown list
                e.SuppressKeyPress = true;    // Prevent any side effect
            }

            Log.writeMessage("Chips DeptList_KeyDown - End : " + DateTime.Now);
        }

        private void OwnerList_KeyDown(object sender, KeyEventArgs e)
        {
            Log.writeMessage("Chips OwnerList_KeyDown - Start : " + DateTime.Now);

            if (e.KeyCode == Keys.ShiftKey) // Detect Shift key
            {
                OwnerList.DroppedDown = true; // Open the dropdown list
                e.SuppressKeyPress = true;    // Prevent any side effect
            }
            if (e.KeyCode == Keys.Escape)
            {
                OwnerList.DroppedDown = false;
            }
            if (e.KeyCode == Keys.F2) // Detect F2 key
            {
                OwnerList.DataSource = null;
                var ownerList = _masterService.GetOwnerList("").Result.OrderBy(x => x.LegalName).ToList();
                ownerList.Insert(0, new BusinessPartnerResponse { BusinessPartnerId = 0, LegalName = "Select Owner" });
                OwnerList.DisplayMember = "LegalName";
                OwnerList.ValueMember = "BusinessPartnerId";
                OwnerList.DataSource = ownerList;
                OwnerList.SelectedIndex = 0;
                OwnerList.DroppedDown = true; // Open the dropdown list
                e.SuppressKeyPress = true;    // Prevent any side effect
            }

            Log.writeMessage("Chips OwnerList_KeyDown - End : " + DateTime.Now);
        }

        private void ResetForm(Control parent)
        {
            Log.writeMessage("Chips ResetForm - Start : " + DateTime.Now);

            lblLoading.Visible = true;
            try
            {
                foreach (Control c in parent.Controls)
                {
                    if (c is System.Windows.Forms.TextBox)
                        ((System.Windows.Forms.TextBox)c).Clear();

                    else if (c is System.Windows.Forms.ComboBox)
                        ((System.Windows.Forms.ComboBox)c).SelectedIndex = 0;

                    else if (c is DateTimePicker)
                        ((DateTimePicker)c).Value = DateTime.Now;
                }
                copyno.Text = "1";
                palletwtno.Text = "0";
                grosswtno.Text = "0";
                tarewt.Text = "0";
                netwt.Text = "0";
                wtpercop.Text = "0";
                boxpalletitemwt.Text = "0";
                boxpalletstock.Text = "0";
                boxpalletitemwt.Text = "0";
                frdenier.Text = "0";
                updenier.Text = "0";
                deniervalue.Text = "0";
                itemname.Text = "";
                shadename.Text = "";
                shadecd.Text = "";
                prodtype.Text = "";
                frwt.Text = "0";
                upwt.Text = "0";
                remarks.Text = "";
                lotResponse = new LotsResponse();
                lotsDetailsList = new List<LotsDetailsResponse>();
                LoadDropdowns();
                rowMaterial.Columns.Clear();
                totalProdQty = 0;
                selectedSOId = 0;
                totalSOQty = 0;
                balanceQty = 0;
                selectedMachineid = 0;
                selectedItemTypeid = 0;
                selectedDeptId = 0;
                selectLotId = 0;
                selectedSOId = 0;
                prcompany.Checked = false;
                prowner.Checked = false;
                productionRequest = new ProductionRequest();
                //DeptList.DataSource = null;
                //DeptList.Items.Clear();
                //DeptList.Items.Add("Select Dept");
                //DeptList.SelectedIndex = 0;

                //MergeNoList.DataSource = null;
                //MergeNoList.Items.Clear();
                //MergeNoList.Items.Add("Select MergeNo");
                //MergeNoList.SelectedIndex = 0;

                //LineNoList.SelectedIndex = 0;

                //PackSizeList.SelectedIndex = 0;

                //BoxItemList.SelectedIndex = 0;

                //ComPortList.SelectedIndex = 0;

                //WeighingList.SelectedIndex = 0;

                //OwnerList.SelectedIndex = 0;

                //PrefixList.SelectedIndex = 0;

                isFormReady = false;
            }
            finally
            {
                lblLoading.Visible = false;
                isFormReady = true;
                if (Application.OpenForms["AdminAccount"] is AdminAccount parentForm)
                {
                    if (parentForm.MainMenuStrip != null)
                    {
                        parentForm.MainMenuStrip.Focus();
                        bool highlightedFound = false;

                        foreach (ToolStripMenuItem item in parentForm.MainMenuStrip.Items)
                        {
                            if (item.BackColor == Color.FromArgb(230, 240, 255))
                            {
                                item.Select();
                                parentForm.MainMenuStrip.Focus();

                                // select the current active item
                                parentForm.MainMenuStrip.Items[0].Owner.Focus();
                                highlightedFound = true;
                                break;
                            }
                        }

                        if (!highlightedFound && parentForm.MainMenuStrip.Items.Count > 0)
                            ((ToolStripMenuItem)parentForm.MainMenuStrip.Items[0]).Select();
                    }
                }
            }

            Log.writeMessage("Chips ResetForm - End : " + DateTime.Now);
        }

        private void prcompany_CheckedChanged(object sender, EventArgs e)
        {
            Log.writeMessage("Chips prcompany_CheckedChanged - Start : " + DateTime.Now);

            if (!isFormReady) return;

            if (prcompany.Checked)
            {
                prowner.Checked = false;
                prcompany.Focus();       // keep focus on the current one
            }

            Log.writeMessage("Chips prcompany_CheckedChanged - End : " + DateTime.Now);
        }

        private void prowner_CheckedChanged(object sender, EventArgs e)
        {
            Log.writeMessage("Chips prowner_CheckedChanged - Start : " + DateTime.Now);

            if (!isFormReady) return;

            if (prowner.Checked)
            {
                prcompany.Checked = false;
                prowner.Focus();           // keep focus
            }

            Log.writeMessage("Chips prowner_CheckedChanged - End : " + DateTime.Now);
        }

        private void txtNumeric_KeyPress(object sender, KeyPressEventArgs e)
        {
            Log.writeMessage("Chips txtNumeric_KeyPress - Start : " + DateTime.Now);

            System.Windows.Forms.TextBox txt = sender as System.Windows.Forms.TextBox;

            // Allow control keys (Backspace, Delete, etc.)
            if (char.IsControl(e.KeyChar))
                return;

            // Allow only one decimal point
            if (e.KeyChar == '.' && txt.Text.Contains('.'))
            {
                e.Handled = true;
                return;
            }

            // Allow only digits and one decimal point
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
                return;
            }

            // Check for 3 digits after decimal
            if (txt.Text.Contains('.'))
            {
                int decimalIndex = txt.Text.IndexOf('.');
                string afterDecimal = txt.Text.Substring(decimalIndex + 1);
                if (afterDecimal.Length >= 3 && txt.SelectionStart > decimalIndex)
                {
                    e.Handled = true;
                }
            }

            Log.writeMessage("Chips txtNumeric_KeyPress - End : " + DateTime.Now);
        }

        private void Control_EnterKeyMoveNext(object sender, KeyEventArgs e)
        {
            Log.writeMessage("Chips Control_EnterKeyMoveNext - Start : " + DateTime.Now);

            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // Prevent the "ding" sound

                Control current = (Control)sender;

                // If current is the last field, move focus to the button
                if (current == grosswtno) // replace with your last field
                {
                    saveprint.Focus(); // replace with your button name
                }
                else
                {
                    // Move to next control in tab order
                    this.SelectNextControl(current, true, true, true, true);
                }
            }

            Log.writeMessage("Chips Control_EnterKeyMoveNext - End : " + DateTime.Now);
        }

        private void ShowCustomMessage(string boxNo)
        {
            Log.writeMessage("Chips ShowCustomMessage - Start : " + DateTime.Now);

            using (Form msgForm = new Form())
            {
                msgForm.Width = 420;
                msgForm.Height = 200;
                msgForm.FormBorderStyle = FormBorderStyle.FixedDialog;
                msgForm.Text = "Success";
                msgForm.StartPosition = FormStartPosition.CenterScreen;
                msgForm.MaximizeBox = false;
                msgForm.MinimizeBox = false;
                msgForm.ShowIcon = false;
                msgForm.ShowInTaskbar = false;
                msgForm.BackColor = Color.White;

                System.Windows.Forms.Label lblMessage = new System.Windows.Forms.Label()
                {
                    AutoSize = false,
                    Text = $"Chips Packing added successfully for BoxNo {boxNo}.",
                    Font = FontManager.GetFont(12F, FontStyle.Regular),
                    ForeColor = Color.Black,
                    Location = new System.Drawing.Point(85, 40),
                    Size = new Size(300, 60)
                };

                System.Windows.Forms.Button btnOk = new System.Windows.Forms.Button()
                {
                    Text = "OK",
                    DialogResult = DialogResult.OK,
                    Font = FontManager.GetFont(10F, FontStyle.Bold),
                    BackColor = Color.FromArgb(230, 240, 255),
                    FlatStyle = FlatStyle.Flat,
                    Size = new Size(80, 32),
                    Location = new System.Drawing.Point(msgForm.Width / 2 - 40, 100),
                    Cursor = Cursors.Hand
                };
                btnOk.FlatAppearance.BorderColor = Color.FromArgb(180, 200, 230);

                msgForm.Controls.Add(lblMessage);
                msgForm.Controls.Add(btnOk);

                msgForm.AcceptButton = btnOk;
                msgForm.ShowDialog();
            }

            Log.writeMessage("Chips ShowCustomMessage - End : " + DateTime.Now);
        }

        private void ComboBox_Leave(object sender, EventArgs e)
        {
            Log.writeMessage("Chips ComboBox_Leave - Start : " + DateTime.Now);

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

            Log.writeMessage("Chips ComboBox_Leave - End : " + DateTime.Now);
        }

        private void txtNumeric_Leave(object sender, EventArgs e)
        {
            Log.writeMessage("Chips txtNumeric_Leave - Start : " + DateTime.Now);

            FormatToThreeDecimalPlaces(sender as TextBox);

            Log.writeMessage("Chips txtNumeric_Leave - End : " + DateTime.Now);
        }
        private void FormatToThreeDecimalPlaces(TextBox textBox)
        {
            Log.writeMessage("Chips FormatToThreeDecimalPlaces - Start : " + DateTime.Now);

            if (decimal.TryParse(textBox.Text, out decimal value))
                textBox.Text = value.ToString("0.000");
            else
                textBox.Text = "0.000"; // optional fallback

            Log.writeMessage("Chips FormatToThreeDecimalPlaces - End : " + DateTime.Now);
        }

        private void AdjustNameByCharCount()
        {
            Log.writeMessage("Chips AdjustNameByCharCount - Start : " + DateTime.Now);

            int shadeCharCount = shadename.Text.Length;

            if (shadeCharCount > 20)
            {
                // if shadename is large, fits in two lines
                shadename.Location = new System.Drawing.Point(43, -3);
            }
            else
            {
                // if shadename is large, fits in one line
                shadename.Location = new System.Drawing.Point(43, 5);
            }

            int itemCharCount = itemname.Text.Length;
            if (itemCharCount > 20)
            {
                itemname.Location = new System.Drawing.Point(38, -3);
            }
            else
            {
                itemname.Location = new System.Drawing.Point(38, 5);
            }

            Log.writeMessage("Chips AdjustNameByCharCount - End : " + DateTime.Now);
        }

        private void ResetDependentDropdownValues()
        {
            Log.writeMessage("Chips ResetDependentDropdownValues - Start : " + DateTime.Now);

            QualityList.DataSource = null;
            QualityList.Items.Clear();
            QualityList.Items.Add("Select Quality");
            QualityList.SelectedItem = "Select Quality";

            WindingTypeList.DataSource = null;
            WindingTypeList.Items.Clear();
            WindingTypeList.Items.Add("Select Winding Type");
            WindingTypeList.SelectedItem = "Select Winding Type";

            Log.writeMessage("Chips ResetDependentDropdownValues - End : " + DateTime.Now);
        }
    }
}
