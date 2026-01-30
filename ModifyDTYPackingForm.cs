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
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PackingApplication
{
    public partial class ModifyDTYPackingForm : Form
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
        bool suppressEvents = false;
        int selectedDeptId = 0;
        int selectedMachineid = 0;
        short selectedItemTypeid = 0;
        short selectedMainItemTypeid = 0;
        List<ProductionResponse> packingList = new List<ProductionResponse>();
        int selectedSrDeptId = 0;
        int selectedSrMachineId = 0;
        string selectedSrBoxNo = null;
        string selectedSrProductionDate = null;
        ProductionPrintSlipRequest slipRequest = new ProductionPrintSlipRequest();
        string reportServer = ConfigurationManager.AppSettings["reportServer"];
        string reportPath = ConfigurationManager.AppSettings["reportPath"];
        string UserName = ConfigurationManager.AppSettings["UserName"];
        string Password = ConfigurationManager.AppSettings["Password"];
        string Domain = ConfigurationManager.AppSettings["Domain"];
        private int currentPage = 1;
        private int totalPages = 0;
        private int pageSize = 10;
        public ModifyDTYPackingForm()
        {
            Log.writeMessage("ModifyDTYPackingForm - Start : "+ DateTime.Now);

            InitializeComponent();
            ApplyFonts();
            //this.Shown += ModifyDTYPackingForm_Shown;
            this.AutoScroll = true;
            lblLoading = CommonMethod.InitializeLoadingLabel(this);

            _cmethod.SetButtonBorderRadius(this.submit, 8);
            _cmethod.SetButtonBorderRadius(this.cancelbtn, 8);
            _cmethod.SetButtonBorderRadius(this.saveprint, 8);
            _cmethod.SetButtonBorderRadius(this.findbtn, 8);
            _cmethod.SetButtonBorderRadius(this.closepopupbtn, 8);
            _cmethod.SetButtonBorderRadius(this.searchbtn, 8);
            _cmethod.SetButtonBorderRadius(this.closelistbtn, 8);

            rowMaterial.AutoGenerateColumns = false;

            Log.writeMessage("ModifyDTYPackingForm - End");
        }

        private void ModifyDTYPackingForm_Load(object sender, EventArgs e)
        {
            Log.writeMessage("ModifyDTYPackingForm_Load - Start : " + DateTime.Now);

            LoadDropdowns();

            copyno.Text = "1";
            spoolno.Text = "0";
            //spoolwt.Text = "0";
            //palletwtno.Text = "0.000";
            grosswtno.Text = "0.000";
            tarewt.Text = "0.000";
            netwt.Text = "0.000";
            wtpercop.Text = "0.000";
            copsstock.Text = "0";
            boxpalletstock.Text = "0";
            //copsitemwt.Text = "0";
            //boxpalletitemwt.Text = "0";
            //frdenier.Text = "0";
            //updenier.Text = "0";
            //deniervalue.Text = "0";
            //twistvalue.Text = "0";
            //partyn.Text = "";
            //partyshade.Text = "";
            isFormReady = true;
            dateTimePicker2.Value = DateTime.Now;
            selectedSrProductionDate = dateTimePicker2.Value.ToString("dd-MM-yyyy");
            saveprint.Enabled = false;
            submit.Enabled = false;
            //RefreshLastBoxDetails();

            prcompany.FlatStyle = FlatStyle.System;
            //srlinenoradiobtn.FlatStyle = FlatStyle.System;
            //srdeptradiobtn.FlatStyle = FlatStyle.System;
            //srboxnoradiobtn.FlatStyle = FlatStyle.System;
            //srproddateradiobtn.FlatStyle = FlatStyle.System;
            //closepopupbtn.FlatStyle = FlatStyle.System;
            //SrLineNoList.Enabled = SrDeptList.Enabled = SrBoxNoList.Enabled = dateTimePicker2.Enabled = false;
            this.tableLayoutPanel4.SetColumnSpan(this.panel11, 2);
            this.tableLayoutPanel4.SetColumnSpan(this.panel12, 2);
            this.tableLayoutPanel4.SetColumnSpan(this.panel17, 3);
            this.tableLayoutPanel4.SetColumnSpan(this.panel30, 3);
            this.tableLayoutPanel6.SetColumnSpan(this.panel29, 2);
            this.tableLayoutPanel4.SetColumnSpan(this.panel8, 2);
            this.tableLayoutPanel4.SetColumnSpan(this.panel9, 2);
            this.tableLayoutPanel4.SetColumnSpan(this.panel16, 3);
            this.grosswtno.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox1_KeyDown);
            this.palletwtno.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox1_KeyDown);
            this.spoolno.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox1_KeyDown);

            Log.writeMessage("ModifyDTYPackingForm_Load - End : " + DateTime.Now);
        }

        private void LoadDropdowns()
        {
            Log.writeMessage("LoadDropdowns - Start : " + DateTime.Now);

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

            var getSaleOrder = new List<LotSaleOrderDetailsResponse>();
            getSaleOrder.Insert(0, new LotSaleOrderDetailsResponse { SaleOrderItemsId = 0, ItemName = "Select Sale Order Item" });
            SaleOrderList.DataSource = getSaleOrder;
            SaleOrderList.DisplayMember = "ItemName";
            SaleOrderList.ValueMember = "SaleOrderItemsId";
            SaleOrderList.SelectedIndex = 0;

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

            var mergenoList = new List<LotsResponse>();
            mergenoList.Insert(0, new LotsResponse { LotId = 0, LotNoFrmt = "Select MergeNo" });
            MergeNoList.DataSource = mergenoList;
            MergeNoList.DisplayMember = "LotNoFrmt";
            MergeNoList.ValueMember = "LotId";
            MergeNoList.SelectedIndex = 0;

            var copsitemList = new List<ItemResponse>();
            copsitemList.Insert(0, new ItemResponse { ItemId = 0, Name = "Select Cops Item" });
            CopsItemList.DataSource = copsitemList;
            CopsItemList.DisplayMember = "Name";
            CopsItemList.ValueMember = "ItemId";
            CopsItemList.SelectedIndex = 0;

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

            _cmethod.SetReadOnlyBlue(LineNoList, true, false);
            _cmethod.SetReadOnlyBlue(DeptList, true, false);
            _cmethod.SetReadOnlyBlue(PackSizeList, true, false);
            _cmethod.SetReadOnlyBlue(SaleOrderList, true, false);
            _cmethod.SetReadOnlyBlue(WindingTypeList, true, false);
            _cmethod.SetReadOnlyBlue(QualityList, true, false);
            _cmethod.SetReadOnlyBlue(MergeNoList, true, false);
            _cmethod.SetReadOnlyBlue(CopsItemList, true, false);
            _cmethod.SetReadOnlyBlue(BoxItemList, true, false);
            _cmethod.SetReadOnlyBlue(OwnerList, true, false);
            _cmethod.SetReadOnlyBlue(ComPortList, true, false);
            _cmethod.SetReadOnlyBlue(WeighingList, true, false);
            _cmethod.SetReadOnlyBlue(remarks, true, false);
            _cmethod.SetReadOnlyBlue(remarks, true, false);
            _cmethod.SetReadOnlyBlue(prcompany, true, false);
            _cmethod.SetReadOnlyBlue(prowner, true, false);
            _cmethod.SetReadOnlyBlue(prdate, true, false);
            _cmethod.SetReadOnlyBlue(pruser, true, false);
            _cmethod.SetReadOnlyBlue(prhindi, true, false);
            _cmethod.SetReadOnlyBlue(prwtps, true, false);
            _cmethod.SetReadOnlyBlue(prqrcode, true, false);
            _cmethod.SetReadOnlyBlue(prtwist, true, false);
            _cmethod.SetReadOnlyBlue(spoolno, true, false);
            _cmethod.SetReadOnlyBlue(palletwtno, true, false);
            _cmethod.SetReadOnlyBlue(grosswtno, true, false);
            _cmethod.SetReadOnlyBlue(netwt, true, false);
            _cmethod.SetReadOnlyBlue(copyno, true, false);

            LoadSearchDropdowns();

            Log.writeMessage("LoadDropdowns - End : " + DateTime.Now);
        }

        private void LoadSearchDropdowns()
        {
            Log.writeMessage("DTY LoadSearchDropdowns - Start : " + DateTime.Now);

            var srmachineList = new List<MachineResponse>();
            srmachineList.Insert(0, new MachineResponse { MachineId = 0, MachineName = "Select Line No." });
            SrLineNoList.DataSource = srmachineList;
            SrLineNoList.DisplayMember = "MachineName";
            SrLineNoList.ValueMember = "MachineId";
            SrLineNoList.SelectedIndex = 0;

            var srdeptList = new List<DepartmentResponse>();
            srdeptList.Insert(0, new DepartmentResponse { DepartmentId = 0, DepartmentName = "Select Dept" });
            SrDeptList.DataSource = srdeptList;
            SrDeptList.DisplayMember = "DepartmentName";
            SrDeptList.ValueMember = "DepartmentId";
            SrDeptList.SelectedIndex = 0;

            var srboxnoList = new List<ProductionResponse>();
            srboxnoList.Insert(0, new ProductionResponse { ProductionId = 0, BoxNo = "Select BoxNo" });
            SrBoxNoList.DataSource = srboxnoList;
            SrBoxNoList.DisplayMember = "BoxNo";
            SrBoxNoList.ValueMember = "ProductionId";
            SrBoxNoList.SelectedIndex = 0;

            Log.writeMessage("DTY LoadSearchDropdowns - End : " + DateTime.Now);
        }

        private void ApplyFonts()
        {
            Log.writeMessage("ApplyFonts - Start : " + DateTime.Now);

            this.lineno.Font = FontManager.GetFont(8F, FontStyle.Bold);
            this.department.Font = FontManager.GetFont(8F, FontStyle.Bold);
            this.mergeno.Font = FontManager.GetFont(8F, FontStyle.Bold);
            this.lastboxno.Font = FontManager.GetFont(8F, FontStyle.Bold);
            this.lastbox.Font = FontManager.GetFont(8F, FontStyle.Regular);
            this.item.Font = FontManager.GetFont(8F, FontStyle.Bold);
            this.shade.Font = FontManager.GetFont(8F, FontStyle.Bold);
            this.shadecode.Font = FontManager.GetFont(8F, FontStyle.Bold);
            this.packingdate.Font = FontManager.GetFont(8F, FontStyle.Bold);
            this.dateTimePicker1.Font = FontManager.GetFont(8F, FontStyle.Regular);
            this.quality.Font = FontManager.GetFont(8F, FontStyle.Bold);
            this.saleorderno.Font = FontManager.GetFont(8F, FontStyle.Bold);
            this.packsize.Font = FontManager.GetFont(8F, FontStyle.Bold);
            this.frdenier.Font = FontManager.GetFont(8F, FontStyle.Regular);
            this.updenier.Font = FontManager.GetFont(8F, FontStyle.Regular);
            this.windingtype.Font = FontManager.GetFont(8F, FontStyle.Bold);
            this.comport.Font = FontManager.GetFont(8F, FontStyle.Bold);
            this.copssize.Font = FontManager.GetFont(8F, FontStyle.Bold);
            this.copweight.Font = FontManager.GetFont(8F, FontStyle.Bold);
            this.copstock.Font = FontManager.GetFont(8F, FontStyle.Bold);
            this.copsitemwt.Font = FontManager.GetFont(8F, FontStyle.Regular);
            this.boxpalletitemwt.Font = FontManager.GetFont(8F, FontStyle.Regular);
            this.boxtype.Font = FontManager.GetFont(8F, FontStyle.Bold);
            this.boxweight.Font = FontManager.GetFont(8F, FontStyle.Bold);
            this.copsstock.Font = FontManager.GetFont(8F, FontStyle.Regular);
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
            this.CopsItemList.Font = FontManager.GetFont(8F, FontStyle.Regular);
            this.BoxItemList.Font = FontManager.GetFont(8F, FontStyle.Regular);
            this.SaleOrderList.Font = FontManager.GetFont(8F, FontStyle.Regular);
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
            this.spoolwt.Font = FontManager.GetFont(8F, FontStyle.Regular);
            this.spoolno.Font = FontManager.GetFont(8F, FontStyle.Regular);
            this.spool.Font = FontManager.GetFont(8F, FontStyle.Bold);
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
            this.machineboxheader.Font = FontManager.GetFont(8F, FontStyle.Regular);
            this.Machinelbl.Font = FontManager.GetFont(9F, FontStyle.Bold);
            this.grosswterror.Font = FontManager.GetFont(7F, FontStyle.Regular);
            this.palletwterror.Font = FontManager.GetFont(7F, FontStyle.Regular);
            this.spoolwterror.Font = FontManager.GetFont(7F, FontStyle.Regular);
            this.spoolnoerror.Font = FontManager.GetFont(7F, FontStyle.Regular);
            this.Weighboxlbl.Font = FontManager.GetFont(9F, FontStyle.Bold);
            this.Packagingboxlbl.Font = FontManager.GetFont(9F, FontStyle.Bold);
            this.cancelbtn.Font = FontManager.GetFont(8F, FontStyle.Bold);
            this.windingerror.Font = FontManager.GetFont(7F, FontStyle.Regular);
            this.packsizeerror.Font = FontManager.GetFont(7F, FontStyle.Regular);
            this.soerror.Font = FontManager.GetFont(7F, FontStyle.Regular);
            this.qualityerror.Font = FontManager.GetFont(7F, FontStyle.Regular);
            this.mergenoerror.Font = FontManager.GetFont(7F, FontStyle.Regular);
            this.copynoerror.Font = FontManager.GetFont(7F, FontStyle.Regular);
            this.linenoerror.Font = FontManager.GetFont(7F, FontStyle.Regular);
            this.rowMaterialBox.Font = FontManager.GetFont(8F, FontStyle.Bold);
            this.fromdenier.Font = FontManager.GetFont(8F, FontStyle.Bold);
            this.uptodenier.Font = FontManager.GetFont(8F, FontStyle.Bold);
            this.Font = FontManager.GetFont(9F, FontStyle.Bold);
            this.bppartyname.Font = FontManager.GetFont(8F, FontStyle.Bold);
            this.partyshade.Font = FontManager.GetFont(8F, FontStyle.Regular);
            this.partyshd.Font = FontManager.GetFont(8F, FontStyle.Bold);
            this.partyn.Font = FontManager.GetFont(8F, FontStyle.Regular);
            this.twistvalue.Font = FontManager.GetFont(8F, FontStyle.Regular);
            this.twist.Font = FontManager.GetFont(8F, FontStyle.Bold);
            this.salelotvalue.Font = FontManager.GetFont(8F, FontStyle.Regular);
            this.salelot.Font = FontManager.GetFont(8F, FontStyle.Bold);
            this.owner.Font = FontManager.GetFont(8F, FontStyle.Bold);
            this.OwnerList.Font = FontManager.GetFont(8F, FontStyle.Regular);
            this.fromwt.Font = FontManager.GetFont(8F, FontStyle.Bold);
            this.frwt.Font = FontManager.GetFont(8F, FontStyle.Regular);
            this.uptowt.Font = FontManager.GetFont(8F, FontStyle.Bold);
            this.upwt.Font = FontManager.GetFont(8F, FontStyle.Regular);
            this.boxnofrmt.Font = FontManager.GetFont(8F, FontStyle.Regular);
            this.boxno.Font = FontManager.GetFont(8F, FontStyle.Bold);
            this.findbtn.Font = FontManager.GetFont(8F, FontStyle.Bold);
            this.closepopupbtn.Font = FontManager.GetFont(8F, FontStyle.Bold);
            this.searchbtn.Font = FontManager.GetFont(8F, FontStyle.Bold);
            this.srlineno.Font = FontManager.GetFont(8F, FontStyle.Bold);
            this.SrLineNoList.Font = FontManager.GetFont(8F, FontStyle.Regular);
            this.srdept.Font = FontManager.GetFont(8F, FontStyle.Bold);
            this.SrDeptList.Font = FontManager.GetFont(8F, FontStyle.Regular);
            this.srboxno.Font = FontManager.GetFont(8F, FontStyle.Bold);
            this.SrBoxNoList.Font = FontManager.GetFont(8F, FontStyle.Regular);
            this.srproddate.Font = FontManager.GetFont(8F, FontStyle.Bold);
            this.dateTimePicker2.Font = FontManager.GetFont(8F, FontStyle.Regular);
            this.closelistbtn.Font = FontManager.GetFont(8F, FontStyle.Bold);
            this.prevbtn.Font = FontManager.GetFont(8F, FontStyle.Bold);
            this.nextbtn.Font = FontManager.GetFont(8F, FontStyle.Bold);
            this.lblPageInfo.Font = FontManager.GetFont(8F, FontStyle.Regular);

            Log.writeMessage("ApplyFonts - End : " + DateTime.Now);
        }

        //private async void ModifyDTYPackingForm_Shown(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        Log.writeMessage("ModifyDTYPackingForm_Shown - Start : " + DateTime.Now);

        //        var machineTask = _masterService.GetMachineList("TexturisingLot", "");
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

        //        //copsitem
        //        copsitemList.Insert(0, new ItemResponse { ItemId = 0, Name = "Select Cops Item" });
        //        CopsItemList.DataSource = copsitemList;
        //        CopsItemList.DisplayMember = "Name";
        //        CopsItemList.ValueMember = "ItemId";
        //        CopsItemList.SelectedIndex = 0;
        //        CopsItemList.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
        //        CopsItemList.AutoCompleteSource = AutoCompleteSource.ListItems;

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

        //        Log.writeMessage("ModifyDTYPackingForm_Shown - End : " + DateTime.Now);
        //    }
        //    finally
        //    {
        //        lblLoading.Visible = false;
        //    }
        //}

        private async Task LoadProductionDetailsAsync(ProductionResponse prodResponse)
        {
            Log.writeMessage("DTY LoadProductionDetailsAsync - Start : " + DateTime.Now);

            if (prodResponse != null)
            {
                productionResponse = prodResponse;

                submit.Text = "Update";
                saveprint.Text = "Update && Print";
                submit.Enabled = productionResponse.IsDisabled ? false : true;
                saveprint.Enabled = productionResponse.IsDisabled ? false : true;
                findbtn.Enabled = false;
                cancelbtn.Enabled = true;

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

                SaleOrderList.DataSource = null;
                SaleOrderList.Items.Clear();
                SaleOrderList.Items.Add("Select Sale Order Item");
                var salesOrderNumber = "";
                salesOrderNumber = productionResponse.SalesOrderNumber + "--" + productionResponse.SOItemName + "--" + productionResponse.ShadeName + "--" + productionResponse.SOQuantity;
                SaleOrderList.Items.Add(salesOrderNumber);
                SaleOrderList.SelectedItem = salesOrderNumber;
                productionRequest.SaleOrderItemsId = productionResponse.SaleOrderItemsId;
                selectedSOId = productionResponse.SaleOrderItemsId;

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

                CopsItemList.DataSource = null;
                CopsItemList.Items.Clear();
                CopsItemList.Items.Add("Select Cops Item");
                CopsItemList.Items.Add(productionResponse.SpoolItemName);
                CopsItemList.SelectedItem = productionResponse.SpoolItemName;
                productionRequest.SpoolItemId = productionResponse.SpoolItemId;

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
                itemname.Text = (!string.IsNullOrEmpty(productionResponse.ItemName)) ? productionResponse.ItemName : "";
                shadename.Text = (!string.IsNullOrEmpty(productionResponse.ShadeName)) ? productionResponse.ShadeName : "";
                shadecd.Text = (!string.IsNullOrEmpty(productionResponse.ShadeCode)) ? productionResponse.ShadeCode : "";
                deniervalue.Text = productionResponse.Denier.ToString();
                twistvalue.Text = (!string.IsNullOrEmpty(productionResponse.TwistName)) ? productionResponse.TwistName.ToString() : "";
                salelotvalue.Text = (!string.IsNullOrEmpty(productionResponse.SaleLot)) ? productionResponse.SaleLot.ToString() : "";
                frdenier.Text = productionResponse.FromDenier.ToString();
                updenier.Text = productionResponse.UpToDenier.ToString();
                startWeight = productionResponse.StartWeight;
                endWeight = productionResponse.EndWeight;
                frwt.Text = productionResponse.StartWeight.ToString();
                upwt.Text = productionResponse.EndWeight.ToString();
                copsitemwt.Text = productionResponse.CopsItemWeight.ToString();
                boxpalletitemwt.Text = productionResponse.BoxItemWeight.ToString();
                palletwtno.Text = productionResponse.BoxItemWeight.ToString();
                totalSOQty = productionResponse.SOQuantity;
                RefreshGradewiseGrid();
                productionRequest.ItemId = productionResponse.ItemId;
                productionRequest.ShadeId = productionResponse.ShadeId;
                productionRequest.TwistId = productionResponse.TwistId;
                productionRequest.ContainerTypeId = productionResponse.ContainerTypeId;
                boxnofrmt.Text = (!string.IsNullOrEmpty(productionResponse.BoxNoFmtd)) ? productionResponse.BoxNoFmtd : "";
                dateTimePicker1.Text = productionResponse.ProductionDate.ToString();
                dateTimePicker1.Value = productionResponse.ProductionDate;
                spoolno.Text = productionResponse.Spools.ToString();
                productionRequest.Spools = productionResponse.Spools;
                spoolwt.Text = productionResponse.SpoolsWt.ToString();
                productionRequest.SpoolsWt = productionResponse.SpoolsWt;
                palletwtno.Text = productionResponse.EmptyBoxPalletWt.ToString();
                productionRequest.EmptyBoxPalletWt = productionResponse.EmptyBoxPalletWt;
                grosswtno.Text = productionResponse.GrossWt.ToString();
                productionRequest.GrossWt = productionResponse.GrossWt;
                tarewt.Text = productionResponse.TareWt.ToString();
                productionRequest.TareWt = productionResponse.TareWt;
                netwt.Text = productionResponse.NetWt.ToString();
                productionRequest.NetWt = productionResponse.NetWt;
                AdjustNameByCharCount();
                selectedMainItemTypeid = productionResponse.MainItemTypeId;
                selectedItemTypeid = productionResponse.ItemTypeId;

                _cmethod.SetReadOnlyBlue(LineNoList, false, false);
                _cmethod.SetReadOnlyBlue(DeptList, false, false);
                _cmethod.SetReadOnlyBlue(PackSizeList, false, false);
                _cmethod.SetReadOnlyBlue(SaleOrderList, false, false);
                _cmethod.SetReadOnlyBlue(WindingTypeList, false, false);
                _cmethod.SetReadOnlyBlue(QualityList, false, false);
                _cmethod.SetReadOnlyBlue(MergeNoList, false, false);
                _cmethod.SetReadOnlyBlue(CopsItemList, false, false);
                _cmethod.SetReadOnlyBlue(BoxItemList, false, false);
                _cmethod.SetReadOnlyBlue(OwnerList, false, false);
                _cmethod.SetReadOnlyBlue(ComPortList, false, false);
                _cmethod.SetReadOnlyBlue(WeighingList, false, false);
                _cmethod.SetReadOnlyBlue(remarks, false, false);
                _cmethod.SetReadOnlyBlue(remarks, false, false);
                _cmethod.SetReadOnlyBlue(prcompany, false, false);
                _cmethod.SetReadOnlyBlue(prowner, false, false);
                _cmethod.SetReadOnlyBlue(prdate, false, false);
                _cmethod.SetReadOnlyBlue(pruser, false, false);
                _cmethod.SetReadOnlyBlue(prhindi, false, false);
                _cmethod.SetReadOnlyBlue(prwtps, false, false);
                _cmethod.SetReadOnlyBlue(prqrcode, false, false);
                _cmethod.SetReadOnlyBlue(prtwist, false, false);
                _cmethod.SetReadOnlyBlue(spoolno, false, false);
                _cmethod.SetReadOnlyBlue(palletwtno, false, false);
                _cmethod.SetReadOnlyBlue(grosswtno, false, false);
                _cmethod.SetReadOnlyBlue(netwt, false, false);
                _cmethod.SetReadOnlyBlue(copyno, false, false);
            }

            Log.writeMessage("DTY LoadProductionDetailsAsync - End : " + DateTime.Now);
        }

        private async void LineNoList_SelectedIndexChanged(object sender, EventArgs e)
        {
            Log.writeMessage("DTY LineNoList_SelectedIndexChanged - Start : " + DateTime.Now);

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
                            var deptTask = _masterService.GetDepartmentList("DTY", selectedMachine.DepartmentName).Result;
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

                        MergeNoList.DataSource = null;
                        MergeNoList.Items.Clear();
                        MergeNoList.Items.Add("Select MergeNo");
                        MergeNoList.SelectedItem = "Select MergeNo";

                        ResetDependentDropdownValues();

                        //var getLots = _productionService.getLotList(selectedMachineId, "").Result;
                        //getLots.Insert(0, new LotsResponse { LotId = 0, LotNoFrmt = "Select MergeNo" });
                        //MergeNoList.DataSource = getLots;
                        //MergeNoList.DisplayMember = "LotNoFrmt";
                        //MergeNoList.ValueMember = "LotId";
                        //MergeNoList.SelectedIndex = 0;
                        //MergeNoList.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                        //MergeNoList.AutoCompleteSource = AutoCompleteSource.ListItems;

                        //if (_productionId > 0 && productionResponse != null)
                        //{
                        //    MergeNoList.SelectedValue = productionResponse.LotId;
                        //    DeptList.SelectedValue = productionResponse.DepartmentId;
                        //}
                    }
                }
            }
            finally
            {
                lblLoading.Visible = false;
                suppressEvents = false;             //Allow events again
            }

            Log.writeMessage("DTY LineNoList_SelectedIndexChanged - End : " + DateTime.Now);
        }

        private void LinoNoList_TextUpdate(object sender, EventArgs e)
        {
            Log.writeMessage("DTY LinoNoList_TextUpdate - Start : " + DateTime.Now);

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
                    machineList = _masterService.GetMachineList("TexturisingLot", typedText).Result.OrderBy(x => x.MachineName).ToList();

                    machineList.Insert(0, new MachineResponse { MachineId = 0, MachineName = "Select Line No." });
                }
                else
                {
                    machineList = _masterService.GetMachineByDepartmentIdAndLotType(selectedDeptId, "TexturisingLot").Result.OrderBy(x => x.MachineName).ToList();

                    machineList.Insert(0, new MachineResponse { MachineId = 0, MachineName = "Select Line No." });
                }

                LineNoList.BeginUpdate();
                LineNoList.DataSource = null;
                LineNoList.DisplayMember = "MachineName";
                LineNoList.ValueMember = "MachineId";
                LineNoList.DataSource = machineList;
                LineNoList.EndUpdate();

                LineNoList.TextUpdate -= LinoNoList_TextUpdate;
                LineNoList.DroppedDown = true;
                LineNoList.SelectionLength = typedText.Length;
                LineNoList.SelectedIndex = -1;
                LineNoList.Text = typedText;
                LineNoList.SelectionStart = cursorPosition;
                LineNoList.TextUpdate += LinoNoList_TextUpdate;
            }

            Log.writeMessage("DTY LinoNoList_TextUpdate - End : " + DateTime.Now);
        }

        private async void MergeNoList_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Log.writeMessage("DTY MergeNoList_SelectionChangeCommitted - Start : " + DateTime.Now);

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
                            MergeNoList.DataSource = null;
                            MergeNoList.Items.Clear();
                            MergeNoList.Items.Add("Select MergeNo");
                            MergeNoList.Items.Add(selectedLot.LotNoFrmt);
                            MergeNoList.SelectedItem = selectedLot.LotNoFrmt;
                            productionRequest.LotId = selectedLot.LotId;
                            selectLotId = selectedLot.LotId;

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
                            twistvalue.Text = (!string.IsNullOrEmpty(lotResponse.TwistName)) ? lotResponse.TwistName.ToString() : "";
                            salelotvalue.Text = (!string.IsNullOrEmpty(lotResponse.SaleLot)) ? lotResponse.SaleLot.ToString() : null;
                            productionRequest.SaleLot = (!string.IsNullOrEmpty(lotResponse.SaleLot)) ? lotResponse.SaleLot : null;
                            productionRequest.TwistId = lotResponse.TwistId;
                            productionRequest.MachineId = lotResponse.MachineId;
                            productionRequest.ItemId = lotResponse.ItemId;
                            productionRequest.ShadeId = lotResponse.ShadeId;
                            LineNoList.SelectedValue = lotResponse.MachineId;
                            selectedItemTypeid = lotResponse.ItemTypeId;
                            selectedMainItemTypeid = lotResponse.MainItemTypeId;

                            //if (lotResponse.ItemId > 0)
                            //{
                            //    var itemResponse = _masterService.GetItemById(lotResponse.ItemId).Result;
                            //    if (itemResponse != null)
                            //    {
                            //        selectedItemTypeid = itemResponse.ItemTypeId;
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
                            //    }
                            //}
                        }

                        //var getWindingType = new List<WindingTypeResponse>();
                        //getWindingType = _productionService.getWinderTypeList(selectedLotId).Result;
                        //getWindingType.Insert(0, new WindingTypeResponse { WindingTypeId = 0, WindingTypeName = "Select Winding Type" });
                        //if (getWindingType.Count <= 1)
                        //{
                        //    getWindingType = _masterService.GetWindingTypeList().Result;
                        //    getWindingType.Insert(0, new WindingTypeResponse { WindingTypeId = 0, WindingTypeName = "Select Winding Type" });

                        //}
                        //WindingTypeList.SelectedIndexChanged -= WindingTypeList_SelectedIndexChanged;
                        //WindingTypeList.DataSource = getWindingType;
                        //WindingTypeList.DisplayMember = "WindingTypeName";
                        //WindingTypeList.ValueMember = "WindingTypeId";
                        //WindingTypeList.SelectedIndex = 0;
                        ////WindingTypeList.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                        ////WindingTypeList.AutoCompleteSource = AutoCompleteSource.ListItems;
                        //WindingTypeList.SelectedIndexChanged += WindingTypeList_SelectedIndexChanged;

                        var getSaleOrder = _productionService.getSaleOrderList(selectedLotId, "").Result.OrderBy(x => x.ItemName).ToList();
                        getSaleOrder.Insert(0, new LotSaleOrderDetailsResponse { SaleOrderItemsId = 0, ItemName = "Select Sale Order Item" });
                        SaleOrderList.SelectedIndexChanged -= SaleOrderList_SelectedIndexChanged;
                        SaleOrderList.DataSource = getSaleOrder;
                        SaleOrderList.DisplayMember = "ItemName";
                        SaleOrderList.ValueMember = "SaleOrderItemsId";
                        //SaleOrderList.SelectedIndex = 0;
                        //SaleOrderList.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                        //SaleOrderList.AutoCompleteSource = AutoCompleteSource.ListItems;

                        if (SaleOrderList.Items.Count == 2)
                        {
                            SaleOrderList.SelectedIndex = 1;   // Select the single record
                            SaleOrderList.Enabled = false;     // Disable user selection
                            SaleOrderList_SelectedIndexChanged(SaleOrderList, EventArgs.Empty);
                        }
                        else
                        {
                            SaleOrderList.Enabled = true;      // Allow user selection
                            SaleOrderList.SelectedIndex = 0;  // Optional: no default selection
                        }
                        SaleOrderList.SelectedIndexChanged += SaleOrderList_SelectedIndexChanged;
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

                        //if (_productionId > 0 && productionResponse != null)
                        //{
                        //    if (selectLotId == productionResponse.LotId)
                        //    {
                        //        SaleOrderList.SelectedValue = productionResponse.SaleOrderItemsId;
                        //    }
                        //}
                    }
                }
            }
            finally
            {
                lblLoading.Visible = false;
                suppressEvents = false;
            }

            Log.writeMessage("DTY MergeNoList_SelectionChangeCommitted - End : " + DateTime.Now);
        }

        private void MergeNoList_TextUpdate(object sender, EventArgs e)
        {
            Log.writeMessage("DTY MergeNoList_TextUpdate - Start : " + DateTime.Now);

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
                    mergenoList = _productionService.getLotsByLotType("TexturisingLot", typedText).Result.OrderBy(x => x.LotNoFrmt).ToList();

                    mergenoList.Insert(0, new LotsResponse { LotId = 0, LotNoFrmt = "Select MergeNo" });
                }

                MergeNoList.BeginUpdate();
                MergeNoList.DataSource = null;
                MergeNoList.DisplayMember = "LotNoFrmt";
                MergeNoList.ValueMember = "LotId";
                MergeNoList.DataSource = mergenoList;
                MergeNoList.EndUpdate();

                MergeNoList.TextUpdate -= MergeNoList_TextUpdate;
                MergeNoList.DroppedDown = true;
                MergeNoList.SelectionLength = typedText.Length;
                MergeNoList.SelectedIndex = -1;
                MergeNoList.Text = typedText;
                MergeNoList.SelectionStart = cursorPosition;
                MergeNoList.TextUpdate += MergeNoList_TextUpdate;

                suppressEvents = false;
            }

            Log.writeMessage("DTY MergeNoList_TextUpdate - End : " + DateTime.Now);
        }

        private void ResetLotValues()
        {
            Log.writeMessage("DTY ResetLotValues - Start : " + DateTime.Now);

            itemname.Text = "";
            shadename.Text = "";
            shadecd.Text = "";
            deniervalue.Text = "";
            twistvalue.Text = "";
            salelotvalue.Text = "";
            partyn.Text = "";
            partyshade.Text = "";
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
            Log.writeMessage("DTY ResetLotValues - End : " + DateTime.Now);
        }

        private async void PackSizeList_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Log.writeMessage("DTY PackSizeList_SelectionChangeCommitted - Start : " + DateTime.Now);

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

            Log.writeMessage("DTY PackSizeList_SelectionChangeCommitted - End : " + DateTime.Now);
        }

        private void PackSizeList_TextUpdate(object sender, EventArgs e)
        {
            Log.writeMessage("DTY PackSizeList_TextUpdate - Start : " + DateTime.Now);

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

                var packsizeList = _masterService.GetPackSizeList(selectedMainItemTypeid, typedText).Result.OrderBy(x => x.PackSizeName).ToList();

                packsizeList.Insert(0, new PackSizeResponse { PackSizeId = 0, PackSizeName = "Select Pack Size" });

                PackSizeList.BeginUpdate();
                PackSizeList.DataSource = null;
                PackSizeList.DisplayMember = "PackSizeName";
                PackSizeList.ValueMember = "PackSizeId";
                PackSizeList.DataSource = packsizeList;
                PackSizeList.EndUpdate();

                PackSizeList.TextUpdate -= PackSizeList_TextUpdate;
                PackSizeList.DroppedDown = true;
                PackSizeList.SelectionLength = typedText.Length;
                PackSizeList.SelectedIndex = -1;
                PackSizeList.Text = typedText;
                PackSizeList.SelectionStart = cursorPosition;
                PackSizeList.TextUpdate += PackSizeList_TextUpdate;
            }

            Log.writeMessage("DTY PackSizeList_TextUpdate - End : " + DateTime.Now);
        }

        private void QualityList_SelectedIndexChanged(object sender, EventArgs e)
        {
            Log.writeMessage("DTY QualityList_SelectedIndexChanged - Start : " + DateTime.Now);

            if (!isFormReady) return;

            if (QualityList.SelectedValue != null)
            {
                QualityResponse selectedQuality = (QualityResponse)QualityList.SelectedItem;
                int selectedQualityId = selectedQuality.QualityId;

                productionRequest.QualityId = selectedQualityId;
            }

            Log.writeMessage("DTY QualityList_SelectedIndexChanged - End : " + DateTime.Now);
        }

        private void QualityList_TextUpdate(object sender, EventArgs e)
        {
            Log.writeMessage("DTY QualityList_TextUpdate - Start : " + DateTime.Now);

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
                QualityList.SelectionLength = typedText.Length;
                QualityList.SelectedIndex = -1;
                QualityList.Text = typedText;
                QualityList.SelectionStart = cursorPosition;
                QualityList.TextUpdate += QualityList_TextUpdate;

                suppressEvents = false;
            }

            Log.writeMessage("DTY QualityList_TextUpdate - End : " + DateTime.Now);
        }

        private void WindingTypeList_SelectedIndexChanged(object sender, EventArgs e)
        {
            Log.writeMessage("DTY WindingTypeList_SelectedIndexChanged - Start : " + DateTime.Now);

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

            Log.writeMessage("DTY WindingTypeList_SelectedIndexChanged - End : " + DateTime.Now);
        }

        private void WindingTypeList_TextUpdate(object sender, EventArgs e)
        {
            Log.writeMessage("DTY WindingTypeList_TextUpdate - Start : " + DateTime.Now);

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
                WindingTypeList.SelectionLength = typedText.Length;
                WindingTypeList.SelectedIndex = -1;
                WindingTypeList.Text = typedText;
                WindingTypeList.SelectionStart = cursorPosition;
                WindingTypeList.TextUpdate += WindingTypeList_TextUpdate;

                suppressEvents = false;
            }

            Log.writeMessage("DTY WindingTypeList_TextUpdate - End : " + DateTime.Now);
        }

        private async void SaleOrderList_SelectedIndexChanged(object sender, EventArgs e)
        {
            Log.writeMessage("DTY SaleOrderList_SelectedIndexChanged - Start : " + DateTime.Now);

            if (!isFormReady) return;

            lblLoading.Visible = true;
            try
            {
                if (SaleOrderList.SelectedValue != null)
                {
                    LotSaleOrderDetailsResponse selectedSaleOrder = (LotSaleOrderDetailsResponse)SaleOrderList.SelectedItem;
                    int selectedSaleOrderId = selectedSaleOrder.SaleOrderItemsId;
                    string soNumber = selectedSaleOrder.SaleOrderNumber;
                    productionRequest.SaleOrderItemsId = selectedSaleOrderId;
                    if (selectedSaleOrderId > 0)
                    {
                        selectedSOId = selectedSaleOrderId;
                        selectedSONumber = selectedSaleOrder.SaleOrderNumber;
                        totalSOQty = 0;
                        var saleOrderItemResponse = _saleService.getSaleOrderItemById(selectedSaleOrderId).Result;
                        if (saleOrderItemResponse != null)
                        {
                            productionRequest.ContainerTypeId = saleOrderItemResponse.ContainerTypeId;
                            partyn.Text = saleOrderItemResponse.ItemDescription;
                            partyshade.Text = saleOrderItemResponse.ShadeNameDescription + "-" + saleOrderItemResponse.ShadeCodeDescription;
                        }

                        totalSOQty = selectedSaleOrder.Quantity;

                        RefreshGradewiseGrid();

                        //if (_productionId > 0 && productionResponse != null)
                        //{
                        //    if (selectedSaleOrderId == productionResponse.SaleOrderItemsId && productionRequest.LotId == productionResponse.LotId)
                        //    {
                        //        WindingTypeList.SelectedValue = productionResponse.WindingTypeId;
                        //    }
                        //}
                    }
                }
            }
            finally
            {
                lblLoading.Visible = false;
            }

            Log.writeMessage("DTY SaleOrderList_SelectedIndexChanged - End : " + DateTime.Now);
        }

        private void SaleOrderList_TextUpdate(object sender, EventArgs e)
        {
            Log.writeMessage("DTY SaleOrderList_TextUpdate - Start : " + DateTime.Now);

            System.Windows.Forms.ComboBox cb = (System.Windows.Forms.ComboBox)sender;
            string typedText = cb.Text;

            if (string.IsNullOrWhiteSpace(cb.Text))
            {
                cb.TextUpdate -= SaleOrderList_TextUpdate;

                cb.SelectedIndex = 0;
                cb.Text = string.Empty;
                cb.DroppedDown = false;

                cb.TextUpdate += SaleOrderList_TextUpdate;
                return;
            }

            int cursorPosition = cb.SelectionStart;

            if (typedText.Length >= 2)
            {
                suppressEvents = true;
                //SaleOrderList.Items.Clear();

                var getSaleOrder = _productionService.getSaleOrderList(selectLotId, typedText).Result.OrderBy(x => x.ItemName).ToList();
                getSaleOrder.Insert(0, new LotSaleOrderDetailsResponse { SaleOrderItemsId = 0, ItemName = "Select Sale Order Item" });

                SaleOrderList.BeginUpdate();
                SaleOrderList.DataSource = null;
                SaleOrderList.DisplayMember = "ItemName";
                SaleOrderList.ValueMember = "SaleOrderItemsId";
                SaleOrderList.DataSource = getSaleOrder;
                SaleOrderList.EndUpdate();

                SaleOrderList.TextUpdate -= SaleOrderList_TextUpdate;
                SaleOrderList.DroppedDown = true;
                SaleOrderList.SelectionLength = typedText.Length;
                SaleOrderList.SelectedIndex = -1;
                SaleOrderList.Text = typedText;
                SaleOrderList.SelectionStart = cursorPosition;
                SaleOrderList.TextUpdate += SaleOrderList_TextUpdate;

                suppressEvents = false;
            }

            Log.writeMessage("DTY SaleOrderList_TextUpdate - End : " + DateTime.Now);
        }

        private async void RefreshGradewiseGrid()
        {
            Log.writeMessage("DTY RefreshGradewiseGrid - Start : " + DateTime.Now);

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

                totalProdQty = 0;
                foreach (var proditem in gridList)
                {
                    totalProdQty += proditem.GrossWt;
                }
            }

            Log.writeMessage("DTY RefreshGradewiseGrid - End : " + DateTime.Now);
        }

        private async void RefreshLastBoxDetails()
        {
            Log.writeMessage("DTY RefreshLastBoxDetails - Start : " + DateTime.Now);

            var getLastBox = _packingService.getLastBoxDetails("dtypacking", 0).Result;

            //lastboxdetails
            if (getLastBox.ProductionId > 0)
            {
                _productionId = getLastBox.ProductionId;
                await LoadProductionDetailsAsync(getLastBox);

                this.copstxtbox.Text = getLastBox.Spools.ToString();
                this.tarewghttxtbox.Text = getLastBox.TareWt.ToString();
                this.grosswttxtbox.Text = getLastBox.GrossWt.ToString();
                this.netwttxtbox.Text = getLastBox.NetWt.ToString();
                this.lastbox.Text = getLastBox.LastBox.ToString();
            }

            Log.writeMessage("DTY RefreshLastBoxDetails - End : " + DateTime.Now);
        }

        private void ComPortList_SelectedIndexChanged(object sender, EventArgs e)
        {
            Log.writeMessage("DTY ComPortList_SelectedIndexChanged - Start : " + DateTime.Now);

            if (!isFormReady) return;

            if (ComPortList.SelectedValue != null)
            {
                var ComPort = ComPortList.SelectedValue.ToString();
                comPort = ComPortList.SelectedValue.ToString();
            }

            Log.writeMessage("DTY ComPortList_SelectedIndexChanged - End : " + DateTime.Now);
        }

        private void WeighingList_SelectedIndexChanged(object sender, EventArgs e)
        {
            Log.writeMessage("DTY WeighingList_SelectedIndexChanged - Start : " + DateTime.Now);

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

            Log.writeMessage("DTY WeighingList_SelectedIndexChanged - End : " + DateTime.Now);
        }

        private async void CopsItemList_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Log.writeMessage("DTY CopsItemList_SelectionChangeCommitted - Start : " + DateTime.Now);

            if (!isFormReady) return;

            if (CopsItemList.SelectedIndex <= 0)
            {
                copsitemwt.Text = "0";
                spoolwt.Text = "0";
                return;
            }

            lblLoading.Visible = true;
            try
            {
                if (CopsItemList.SelectedValue != null)
                {
                    ItemResponse selectedCopsItem = (ItemResponse)CopsItemList.SelectedItem;
                    int selectedItemId = selectedCopsItem.ItemId;

                    if (selectedItemId > 0)
                    {
                        productionRequest.SpoolItemId = selectedItemId;

                        var itemResponse = _masterService.GetItemById(selectedItemId).Result;
                        if (itemResponse != null)
                        {
                            copsitemwt.Text = itemResponse.Weight.ToString();
                            SpoolNo_TextChanged(sender, e);
                        }
                    }
                }
            }
            finally
            {
                lblLoading.Visible = false;
            }

            Log.writeMessage("DTY CopsItemList_SelectionChangeCommitted - End : " + DateTime.Now);
        }

        private void CopsItemList_TextUpdate(object sender, EventArgs e)
        {
            Log.writeMessage("DTY CopsItemList_TextUpdate - Start : " + DateTime.Now);

            System.Windows.Forms.ComboBox cb = (System.Windows.Forms.ComboBox)sender;
            string typedText = cb.Text;

            if (string.IsNullOrWhiteSpace(cb.Text))
            {
                cb.TextUpdate -= CopsItemList_TextUpdate;

                cb.SelectedIndex = 0;
                cb.Text = string.Empty;
                cb.DroppedDown = false;
                copsitemwt.Text = "0";
                spoolwt.Text = "0";

                cb.TextUpdate += CopsItemList_TextUpdate;
                return;
            }

            int cursorPosition = cb.SelectionStart;

            if (typedText.Length >= 2)
            {
                //CopsItemList.Items.Clear();

                var copsitemList = _masterService.GetItemList(itemCopsCategoryId, typedText).Result.OrderBy(x => x.Name).ToList();

                copsitemList.Insert(0, new ItemResponse { ItemId = 0, Name = "Select Cops Item" });

                CopsItemList.BeginUpdate();
                CopsItemList.DataSource = null;
                CopsItemList.DisplayMember = "Name";
                CopsItemList.ValueMember = "ItemId";
                CopsItemList.DataSource = copsitemList;
                CopsItemList.EndUpdate();

                CopsItemList.TextUpdate -= CopsItemList_TextUpdate;
                CopsItemList.DroppedDown = true;
                CopsItemList.SelectionLength = typedText.Length;
                CopsItemList.SelectedIndex = -1;
                CopsItemList.Text = typedText;
                CopsItemList.SelectionStart = cursorPosition;
                CopsItemList.TextUpdate += CopsItemList_TextUpdate;
            }
            Log.writeMessage("DTY CopsItemList_TextUpdate - End : " + DateTime.Now);
        }

        private async void BoxItemList_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Log.writeMessage("DTY BoxItemList_SelectionChangeCommitted - Start : " + DateTime.Now);

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
                            //GrossWeight_Validating(sender, new CancelEventArgs());
                        }
                    }
                }
            }
            finally
            {
                lblLoading.Visible = false;
            }

            Log.writeMessage("DTY BoxItemList_SelectionChangeCommitted - End : " + DateTime.Now);
        }

        private void BoxItemList_TextUpdate(object sender, EventArgs e)
        {
            Log.writeMessage("DTY BoxItemList_TextUpdate - Start : " + DateTime.Now);

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
                BoxItemList.SelectionLength = typedText.Length;
                BoxItemList.SelectedIndex = -1;
                BoxItemList.Text = typedText;
                BoxItemList.SelectionStart = cursorPosition;
                BoxItemList.TextUpdate += BoxItemList_TextUpdate;
            }
            Log.writeMessage("DTY BoxItemList_TextUpdate - End : " + DateTime.Now);
        }

        private async void DeptList_SelectedIndexChanged(object sender, EventArgs e)
        {
            Log.writeMessage("DTY DeptList_SelectedIndexChanged - Start : " + DateTime.Now);

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
                    //    var machineList = _masterService.GetMachineByDepartmentIdAndLotType(selectedDepartmentId, "TexturisingLot").Result;

                    //    machineList.Insert(0, new MachineResponse { MachineId = 0, MachineName = "Select Line No." });
                    //    LineNoList.DataSource = machineList;
                    //}

                    productionRequest.DepartmentId = selectedDepartmentId;
                    selectedDeptId = selectedDepartmentId;

                    LineNoList.DataSource = null;
                    LineNoList.Items.Clear();
                    LineNoList.Items.Add("Select Line No.");
                    LineNoList.SelectedItem = "Select Line No.";

                    MergeNoList.DataSource = null;
                    MergeNoList.Items.Clear();
                    MergeNoList.Items.Add("Select MergeNo");
                    MergeNoList.SelectedItem = "Select MergeNo";

                    ResetLotValues();
                    ResetDependentDropdownValues();
                    //prefixRequest.DepartmentId = selectedDepartmentId;
                    //prefixRequest.TxnFlag = "DTY";
                    //prefixRequest.TransactionTypeId = 5;
                    //prefixRequest.ProductionTypeId = 1;
                    //prefixRequest.Prefix = "";
                    //prefixRequest.FinYearId = SessionManager.FinYearId;

                    //List<PrefixResponse> prefixList = await _masterService.GetPrefixList(prefixRequest);
                    //prefixList.Insert(0, new PrefixResponse { PrefixCode = 0, Prefix = "Select Prefix" });
                    //PrefixList.SelectedIndexChanged -= PrefixList_SelectedIndexChanged;
                    //PrefixList.DataSource = prefixList;
                    //PrefixList.DisplayMember = "Prefix";
                    //PrefixList.ValueMember = "PrefixCode";
                    //PrefixList.SelectedIndex = 0;
                    //PrefixList.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    //PrefixList.AutoCompleteSource = AutoCompleteSource.ListItems;
                    //if (PrefixList.Items.Count == 2)
                    //{
                    //    PrefixList.SelectedIndex = 1;   // Select the single record
                    //    //PrefixList_SelectedIndexChanged(PrefixList, EventArgs.Empty);
                    //}
                    //else
                    //{
                    //    PrefixList.SelectedIndex = 0;  // Optional: no default selection
                    //}
                    //PrefixList.SelectedIndexChanged += PrefixList_SelectedIndexChanged;
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

            Log.writeMessage("DTY DeptList_SelectedIndexChanged - End : " + DateTime.Now);
        }

        private void DeptList_TextUpdate(object sender, EventArgs e)
        {
            Log.writeMessage("DTY DeptList_TextUpdate - Start : " + DateTime.Now);

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

                var deptList = _masterService.GetDepartmentList("DTY", typedText).Result.OrderBy(x => x.DepartmentName).ToList();

                deptList.Insert(0, new DepartmentResponse { DepartmentId = 0, DepartmentName = "Select Dept" });

                DeptList.BeginUpdate();
                DeptList.DataSource = null;
                DeptList.DisplayMember = "DepartmentName";
                DeptList.ValueMember = "DepartmentId";
                DeptList.DataSource = deptList;
                DeptList.EndUpdate();

                DeptList.TextUpdate -= DeptList_TextUpdate;
                DeptList.DroppedDown = true;
                DeptList.SelectionLength = typedText.Length;
                DeptList.SelectedIndex = -1;
                DeptList.Text = typedText;
                DeptList.SelectionStart = cursorPosition;
                DeptList.TextUpdate += DeptList_TextUpdate;

            }
            Log.writeMessage("DTY DeptList_TextUpdate - End : " + DateTime.Now);
        }

        private async void OwnerList_SelectedIndexChanged(object sender, EventArgs e)
        {
            Log.writeMessage("DTY OwnerList_SelectedIndexChanged - Start : " + DateTime.Now);

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

            Log.writeMessage("DTY OwnerList_SelectedIndexChanged - End : " + DateTime.Now);
        }

        private void OwnerList_TextUpdate(object sender, EventArgs e)
        {
            Log.writeMessage("DTY OwnerList_TextUpdate - Start : " + DateTime.Now);

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
                OwnerList.SelectionLength = typedText.Length;
                OwnerList.SelectedIndex = -1;
                OwnerList.Text = typedText;
                OwnerList.SelectionStart = cursorPosition;
                OwnerList.TextUpdate += OwnerList_TextUpdate;
            }
            Log.writeMessage("DTY OwnerList_TextUpdate - End : " + DateTime.Now);
        }

        private async Task<List<string>> getComPortList()
        {
            Log.writeMessage("DTY getComPortList - Start : " + DateTime.Now);

            var getComPortType = new List<string>
            {
                "Select Com Port",
                "COM1",
                "COM2",
                "COM3",
                "COM4"
            };

            Log.writeMessage("DTY getComPortList - End : " + DateTime.Now);

            return getComPortType;
        }

        private async Task<List<WeighingItem>> GetWeighingList()
        {
            Log.writeMessage("DTY GetWeighingList - Start : " + DateTime.Now);

            var getWeighingScale = new List<WeighingItem>
            {
                new WeighingItem { Id = -1, Name = "Select Weigh Scale" },
                new WeighingItem { Id = 0, Name = "Old" },
                new WeighingItem { Id = 1, Name = "Unique" },
                new WeighingItem { Id = 2, Name = "JISL (9600)" },
                new WeighingItem { Id = 3, Name = "JISL (2400)" }
            };

            Log.writeMessage("DTY GetWeighingList - End : " + DateTime.Now);

            return getWeighingScale;
        }

        private void SpoolWeight_TextChanged(object sender, EventArgs e)
        {
            Log.writeMessage("DTY SpoolWeight_TextChanged - Start : " + DateTime.Now);

            if (string.IsNullOrWhiteSpace(spoolwt.Text))
            {
                CalculateTareWeight();
            }
            else
            {
                CalculateTareWeight();
            }

            Log.writeMessage("DTY SpoolWeight_TextChanged - End : " + DateTime.Now);
        }

        private void PalletWeight_TextChanged(object sender, EventArgs e)
        {
            Log.writeMessage("DTY PalletWeight_TextChanged - Start : " + DateTime.Now);

            if (string.IsNullOrWhiteSpace(palletwtno.Text))
            {
                CalculateTareWeight();
            }
            else
            {
                CalculateTareWeight();
            }

            Log.writeMessage("DTY PalletWeight_TextChanged - End : " + DateTime.Now);
        }

        private void CalculateTareWeight()
        {
            Log.writeMessage("DTY CalculateTareWeight - Start : " + DateTime.Now);

            decimal num1 = 0, num2 = 0;

            decimal.TryParse(spoolwt.Text, out num1);
            decimal.TryParse(palletwtno.Text, out num2);

            tarewt.Text = (num1 + num2).ToString("F3");
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
            Log.writeMessage("DTY CalculateTareWeight - End : " + DateTime.Now);
        }

        private void GrossWeight_Validating(object sender, CancelEventArgs e)
        {
            Log.writeMessage("DTY GrossWeight_Validating - Start : " + DateTime.Now);

            if (!isFormReady) return;

            if (string.IsNullOrWhiteSpace(grosswtno.Text))
            {
                MessageBox.Show("Please enter gross weight", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true;
                return;
            }
            else
            {
                soerror.Visible = false;
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

            Log.writeMessage("DTY GrossWeight_Validating - End : " + DateTime.Now);
        }

        private void CalculateNetWeight()
        {
            Log.writeMessage("DTY CalculateNetWeight - Start : " + DateTime.Now);

            decimal num1 = 0, num2 = 0;

            decimal.TryParse(grosswtno.Text, out num1);
            decimal.TryParse(tarewt.Text, out num2);
            if (num1 > num2)
            {
                netwt.Text = (num1 - num2).ToString("F3");
                CalculateWeightPerCop();
            }

            Log.writeMessage("DTY CalculateNetWeight - End : " + DateTime.Now);
        }

        private void NetWeight_TextChanged(object sender, EventArgs e)
        {
            Log.writeMessage("DTY NetWeight_TextChanged - Start : " + DateTime.Now);

            CalculateWeightPerCop();

            Log.writeMessage("DTY NetWeight_TextChanged - End : " + DateTime.Now);
        }

        private void SpoolNo_TextChanged(object sender, EventArgs e)
        {
            Log.writeMessage("DTY SpoolNo_TextChanged - Start : " + DateTime.Now);

            if (!isFormReady) return;

            if (string.IsNullOrWhiteSpace(copsitemwt.Text))
            {
                spoolwt.Text = "0";
                return;
            }
            else
            {
                decimal spoolnum = 0, copswt = 0;
                decimal.TryParse(spoolno.Text, out spoolnum);
                decimal.TryParse(copsitemwt.Text, out copswt);
                if (spoolnum > 0)
                {
                    spoolwt.Text = (spoolnum * copswt).ToString();
                    CalculateWeightPerCop();
                    CalculateTareWeight();
                    spoolnoerror.Text = "";
                    spoolnoerror.Visible = false;
                }
            }

            Log.writeMessage("DTY SpoolNo_TextChanged - End : " + DateTime.Now);
        }

        private void CalculateWeightPerCop()
        {
            Log.writeMessage("DTY CalculateWeightPerCop - Start : " + DateTime.Now);

            decimal num1 = 0, num2 = 0;

            decimal.TryParse(netwt.Text, out num1);
            decimal.TryParse(spoolno.Text, out num2);
            if (num1 > 0 && num2 > 0)
            {
                wtpercop.Text = (num1 / num2).ToString("F3");
            }

            Log.writeMessage("DTY CalculateWeightPerCop - End : " + DateTime.Now);
        }

        private void CopyNos_TextChanged(object sender, EventArgs e)
        {
            Log.writeMessage("DTY CopyNos_TextChanged - Start : " + DateTime.Now);

            if (string.IsNullOrWhiteSpace(copyno.Text))
            {
                copynoerror.Visible = true;
            }
            else
            {
                copynoerror.Text = "";
                copynoerror.Visible = false;
            }

            Log.writeMessage("DTY CopyNos_TextChanged - End : " + DateTime.Now);
        }

        private async void submit_Click(object sender, EventArgs e)
        {
            Log.writeMessage("DTY submit_Click - Start : " + DateTime.Now);

            submitForm(false);

            Log.writeMessage("DTY submit_Click - End : " + DateTime.Now);
        }

        private async void saveprint_Click(object sender, EventArgs e)
        {
            Log.writeMessage("DTY saveprint_Click - Start : " + DateTime.Now);

            submitForm(true);

            Log.writeMessage("DTY saveprint_Click - End : " + DateTime.Now);
        }

        public async void submitForm(bool isPrint)
        {
            Log.writeMessage("DTY submitForm - Start : " + DateTime.Now);

            if (ValidateForm())
            {
                productionRequest.OwnerId = this.OwnerList.SelectedIndex <= 0 ? 0 : productionRequest.OwnerId;
                productionRequest.PackingType = "DTYPacking";
                productionRequest.Remarks = remarks.Text.Trim();
                productionRequest.Spools = Convert.ToInt32(spoolno.Text.Trim());
                productionRequest.SpoolsWt = Convert.ToDecimal(spoolwt.Text.Trim());
                productionRequest.EmptyBoxPalletWt = Convert.ToDecimal(palletwtno.Text.Trim());
                productionRequest.GrossWt = Convert.ToDecimal(grosswtno.Text.Trim());
                productionRequest.NoOfCopies = Convert.ToInt32(copyno.Text.Trim());
                productionRequest.TareWt = Convert.ToDecimal(tarewt.Text.Trim());
                productionRequest.NetWt = Convert.ToDecimal(netwt.Text.Trim());
                productionRequest.ProductionDate = dateTimePicker1.Value;
                productionRequest.ProdTypeId = productionResponse.ProdTypeId;

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
                    consumptionDetailsRequest.InputItemId = lot.PrevLotItemId;
                    consumptionDetailsRequest.InputQualityId = lot.PrevLotQualityId;
                    consumptionDetailsRequest.PropWeight = consumptionDetailsRequest.ProductionPerc * productionRequest.NetWt;
                    productionRequest.ConsumptionDetailsRequest.Add(consumptionDetailsRequest);
                }

                ProductionResponse result = SubmitPacking(productionRequest, isPrint);
            }

            Log.writeMessage("DTY submitForm - End : " + DateTime.Now);
        }

        public ProductionResponse SubmitPacking(ProductionRequest productionRequest, bool isPrint)
        {
            Log.writeMessage("DTY SubmitPacking - Start : " + DateTime.Now);

            ProductionResponse result = new ProductionResponse();
            result = _packingService.AddUpdatePOYPacking(_productionId, productionRequest);
            if (result != null && result.ProductionId > 0)
            {
                slipRequest.ProductionId = result.ProductionId;
                //submit.Enabled = true;
                //saveprint.Enabled = true;
                RefreshGradewiseGrid();
                //RefreshLastBoxDetails();
                ShowCustomMessage(result.BoxNoFmtd);
                findbtn.Enabled = true;
                submit.Enabled = false;
                saveprint.Enabled = false;
                if (isPrint)
                {
                    //call ssrs report to print
                    string reportpathlink = reportPath + "/Texture";
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
                            //printDoc.Print(); // sends PDF to printer
                            try
                            {
                                printDoc.Print();
                                int slipId = _packingService.AddPrintSlip(slipRequest);
                            }
                            catch (InvalidPrinterException ex)
                            {
                                MessageBox.Show("Printer is not available.\n" + ex.Message);
                            }
                            catch (Win32Exception ex)
                            {
                                MessageBox.Show("Printing failed.\n" + ex.Message);
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Unexpected printing error.\n" + ex.Message);
                            }
                        }
                    }

                    // Clean up temp file
                    File.Delete(tempFile);
                }
                ResetForm(this);
            }
            else
            {
                submit.Enabled = true;
                saveprint.Enabled = true;
                //MessageBox.Show("Something went wrong.",
                //    "Error",
                //    MessageBoxButtons.OK,
                //    MessageBoxIcon.Error);
            }

            Log.writeMessage("DTY SubmitPacking - End : " + DateTime.Now);

            return result;
        }

        private bool ValidateForm()
        {
            Log.writeMessage("DTY ValidateForm - Start : " + DateTime.Now);

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

            if (SaleOrderList.SelectedIndex <= 0)
            {
                MessageBox.Show("Please select sale order", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

            if (BoxItemList.SelectedIndex <= 0)
            {
                MessageBox.Show("Please select box item", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                isValid = false;
            }

            if (CopsItemList.SelectedIndex <= 0)
            {
                MessageBox.Show("Please select cops item", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                isValid = false;
            }

            if (string.IsNullOrWhiteSpace(spoolno.Text) || Convert.ToInt32(spoolno.Text) == 0)
            {
                MessageBox.Show("Please enter spool no", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                isValid = false;
            }

            if (string.IsNullOrWhiteSpace(spoolwt.Text) || Convert.ToDecimal(spoolwt.Text) == 0)
            {
                MessageBox.Show("Please enter spool wt", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            decimal spoolnum = 0;
            decimal.TryParse(spoolno.Text, out spoolnum);
            if (spoolnum == 0)
            {
                MessageBox.Show("Spool no > 0", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            balanceQty = (totalSOQty - totalProdQty);
            if (balanceQty <= 0)
            {
                MessageBox.Show("Quantity not remaining for " + selectedSONumber, "Warning", MessageBoxButtons.OK);
                isValid = false;
            }
            decimal newBalanceQty = balanceQty - gross;
            if (newBalanceQty < 0)
            {
                MessageBox.Show("No Prod Bal Qty remaining", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                isValid = false;
            }

            Log.writeMessage("DTY ValidateForm - End : " + DateTime.Now);
            return isValid;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Log.writeMessage("DTY btnCancel_Click - Start : " + DateTime.Now);

            ResetForm(this);
            submit.Enabled = false;
            saveprint.Enabled = false;
            findbtn.Enabled = true;

            Log.writeMessage("DTY btnCancel_Click - End : " + DateTime.Now);
        }

        private void qualityqty_Paint(object sender, PaintEventArgs e)
        {
            Log.writeMessage("DTY qualityqty_Paint - Start : " + DateTime.Now);

            _cmethod.DrawRectangleBorder((Control)sender, e, Color.LightGray, 2);

            Log.writeMessage("DTY qualityqty_Paint - End : " + DateTime.Now);
        }

        private void windinggrid_Paint(object sender, PaintEventArgs e)
        {
            Log.writeMessage("DTY windinggrid_Paint - Start : " + DateTime.Now);

            _cmethod.DrawRectangleBorder((Control)sender, e, Color.LightGray, 2);

            Log.writeMessage("DTY windinggrid_Paint - End : " + DateTime.Now);
        }

        private void ordertable_Paint(object sender, PaintEventArgs e)
        {
            Log.writeMessage("DTY ordertable_Paint - Start : " + DateTime.Now);

            _cmethod.DrawRoundedBorder((Control)sender, e, 12, Color.FromArgb(102, 163, 255), 1);

            Log.writeMessage("DTY ordertable_Paint - End : " + DateTime.Now);
        }

        private void packagingtable_Paint(object sender, PaintEventArgs e)
        {
            Log.writeMessage("DTY packagingtable_Paint - Start : " + DateTime.Now);

            _cmethod.DrawRoundedBorder((Control)sender, e, 12, Color.FromArgb(102, 163, 255), 1);

            Log.writeMessage("DTY packagingtable_Paint - End : " + DateTime.Now);
        }

        private void weightable_Paint(object sender, PaintEventArgs e)
        {
            Log.writeMessage("DTY weightable_Paint - Start : " + DateTime.Now);

            _cmethod.DrawRoundedBorder((Control)sender, e, 12, Color.FromArgb(102, 163, 255), 1);

            Log.writeMessage("DTY weightable_Paint - End : " + DateTime.Now);
        }

        private void reviewtable_Paint(object sender, PaintEventArgs e)
        {
            Log.writeMessage("DTY reviewtable_Paint - Start : " + DateTime.Now);

            _cmethod.DrawRoundedBorder((Control)sender, e, 12, Color.FromArgb(102, 163, 255), 1);

            Log.writeMessage("DTY reviewtable_Paint - End : " + DateTime.Now);
        }

        private void machineboxlayout_Paint(object sender, PaintEventArgs e)
        {
            Log.writeMessage("DTY machineboxlayout_Paint - Start : " + DateTime.Now);

            _cmethod.DrawRoundedBorder((Control)sender, e, 8, Color.FromArgb(191, 191, 191), 1);

            Log.writeMessage("DTY machineboxlayout_Paint - End : " + DateTime.Now);
        }

        private void machineboxheader_Paint(object sender, PaintEventArgs e)
        {
            Log.writeMessage("DTY machineboxheader_Paint - Start : " + DateTime.Now);

            _cmethod.DrawBottomBorder((Control)sender, e, Color.FromArgb(191, 191, 191), 1);

            Log.writeMessage("DTY machineboxheader_Paint - End : " + DateTime.Now);
        }

        private void weighboxlayout_Paint(object sender, PaintEventArgs e)
        {
            Log.writeMessage("DTY weighboxlayout_Paint - Start : " + DateTime.Now);

            _cmethod.DrawRoundedBorder((Control)sender, e, 8, Color.FromArgb(191, 191, 191), 1);

            Log.writeMessage("DTY weighboxlayout_Paint - End : " + DateTime.Now);
        }

        private void weighboxheader_Paint(object sender, PaintEventArgs e)
        {
            Log.writeMessage("DTY weighboxheader_Paint - Start : " + DateTime.Now);

            _cmethod.DrawBottomBorder((Control)sender, e, Color.FromArgb(191, 191, 191), 1);

            Log.writeMessage("DTY weighboxheader_Paint - End : " + DateTime.Now);
        }

        private void packagingboxlayout_Paint(object sender, PaintEventArgs e)
        {
            Log.writeMessage("DTY packagingboxlayout_Paint - Start : " + DateTime.Now);

            _cmethod.DrawRoundedBorder((Control)sender, e, 8, Color.FromArgb(191, 191, 191), 1);

            Log.writeMessage("DTY packagingboxlayout_Paint - End : " + DateTime.Now);
        }

        private void packagingboxheader_Paint(object sender, PaintEventArgs e)
        {
            Log.writeMessage("DTY packagingboxheader_Paint - Start : " + DateTime.Now);

            _cmethod.DrawBottomBorder((Control)sender, e, Color.FromArgb(191, 191, 191), 1);

            Log.writeMessage("DTY packagingboxheader_Paint - End : " + DateTime.Now);
        }

        private void lastboxlayout_Paint(object sender, PaintEventArgs e)
        {
            Log.writeMessage("DTY lastboxlayout_Paint - Start : " + DateTime.Now);

            _cmethod.DrawRoundedBorder((Control)sender, e, 8, Color.FromArgb(191, 191, 191), 1);

            Log.writeMessage("DTY lastboxlayout_Paint - End : " + DateTime.Now);
        }

        private void lastboxheader_Paint(object sender, PaintEventArgs e)
        {
            Log.writeMessage("DTY lastboxheader_Paint - Start : " + DateTime.Now);

            _cmethod.DrawBottomBorder((Control)sender, e, Color.FromArgb(191, 191, 191), 1);

            Log.writeMessage("DTY lastboxheader_Paint - End : " + DateTime.Now);
        }

        private void lastbxcopspanel_Paint(object sender, PaintEventArgs e)
        {
            Log.writeMessage("DTY lastbxcopspanel_Paint - Start : " + DateTime.Now);

            _cmethod.DrawRoundedDashedBorder((Control)sender, e, 8, Color.FromArgb(102, 163, 255), 1);

            Log.writeMessage("DTY lastbxcopspanel_Paint - End : " + DateTime.Now);
        }

        private void lastbxtarepanel_Paint(object sender, PaintEventArgs e)
        {
            Log.writeMessage("DTY lastbxtarepanel_Paint - Start : " + DateTime.Now);

            _cmethod.DrawRoundedDashedBorder((Control)sender, e, 8, Color.FromArgb(102, 163, 255), 1);

            Log.writeMessage("DTY lastbxtarepanel_Paint - End : " + DateTime.Now);
        }

        private void lastbxgrosswtpanel_Paint(object sender, PaintEventArgs e)
        {
            Log.writeMessage("DTY lastbxgrosswtpanel_Paint - Start : " + DateTime.Now);

            _cmethod.DrawRoundedDashedBorder((Control)sender, e, 8, Color.FromArgb(102, 163, 255), 1);

            Log.writeMessage("DTY lastbxgrosswtpanel_Paint - End : " + DateTime.Now);
        }

        private void lastbxnetwtpanel_Paint(object sender, PaintEventArgs e)
        {
            Log.writeMessage("DTY lastbxnetwtpanel_Paint - Start : " + DateTime.Now);

            _cmethod.DrawRoundedDashedBorder((Control)sender, e, 8, Color.FromArgb(102, 163, 255), 1);

            Log.writeMessage("DTY lastbxnetwtpanel_Paint - End : " + DateTime.Now);
        }

        private void printingdetailslayout_Paint(object sender, PaintEventArgs e)
        {
            Log.writeMessage("DTY printingdetailslayout_Paint - Start : " + DateTime.Now);

            _cmethod.DrawRoundedBorder((Control)sender, e, 8, Color.FromArgb(191, 191, 191), 1);

            Log.writeMessage("DTY printingdetailslayout_Paint - End : " + DateTime.Now);
        }

        private void printingdetailsheader_Paint(object sender, PaintEventArgs e)
        {
            Log.writeMessage("DTY printingdetailsheader_Paint - Start : " + DateTime.Now);

            _cmethod.DrawBottomBorder((Control)sender, e, Color.FromArgb(191, 191, 191), 1);

            Log.writeMessage("DTY printingdetailsheader_Paint - End : " + DateTime.Now);
        }

        private void palletdetailslayout_Paint(object sender, PaintEventArgs e)
        {
            Log.writeMessage("DTY palletdetailslayout_Paint - Start : " + DateTime.Now);

            _cmethod.DrawRoundedBorder((Control)sender, e, 8, Color.FromArgb(191, 191, 191), 1);

            Log.writeMessage("DTY palletdetailslayout_Paint - End : " + DateTime.Now);
        }

        private void palletdetailsheader_Paint(object sender, PaintEventArgs e)
        {
            Log.writeMessage("DTY palletdetailsheader_Paint - Start : " + DateTime.Now);

            _cmethod.DrawBottomBorder((Control)sender, e, Color.FromArgb(191, 191, 191), 1);

            Log.writeMessage("DTY palletdetailsheader_Paint - End : " + DateTime.Now);
        }

        private void machineboxheader_Resize(object sender, EventArgs e)
        {
            Log.writeMessage("DTY machineboxheader_Resize - Start : " + DateTime.Now);

            _cmethod.SetTopRoundedRegion(machineboxheader, 8);

            Log.writeMessage("DTY machineboxheader_Resize - End : " + DateTime.Now);
        }

        private void weighboxheader_Resize(object sender, EventArgs e)
        {
            Log.writeMessage("DTY weighboxheader_Resize - Start : " + DateTime.Now);

            _cmethod.SetTopRoundedRegion(weighboxheader, 8);

            Log.writeMessage("DTY weighboxheader_Resize - End : " + DateTime.Now);
        }

        private void packagingboxheader_Resize(object sender, EventArgs e)
        {
            Log.writeMessage("DTY packagingboxheader_Resize - Start : " + DateTime.Now);

            _cmethod.SetTopRoundedRegion(packagingboxheader, 8);

            Log.writeMessage("DTY packagingboxheader_Resize - End : " + DateTime.Now);
        }

        private void lastboxheader_Resize(object sender, EventArgs e)
        {
            Log.writeMessage("DTY lastboxheader_Resize - Start : " + DateTime.Now);

            _cmethod.SetTopRoundedRegion(lastboxheader, 8);

            Log.writeMessage("DTY lastboxheader_Resize - End : " + DateTime.Now);
        }

        private void printingdetailsheader_Resize(object sender, EventArgs e)
        {
            Log.writeMessage("DTY printingdetailsheader_Resize - Start : " + DateTime.Now);

            _cmethod.SetTopRoundedRegion(printingdetailsheader, 8);

            Log.writeMessage("DTY printingdetailsheader_Resize - End : " + DateTime.Now);
        }

        private void machinetablelayout_Paint(object sender, PaintEventArgs e)
        {
            Log.writeMessage("DTY machinetablelayout_Paint - Start : " + DateTime.Now);

            _cmethod.DrawBottomBorder((Control)sender, e, Color.FromArgb(191, 191, 191), 1);

            Log.writeMessage("DTY machinetablelayout_Paint - End : " + DateTime.Now);
        }

        private void popuppanel_Paint(object sender, PaintEventArgs e)
        {
            Log.writeMessage("DTY popuppanel_Paint - Start : " + DateTime.Now);

            _cmethod.DrawPanelRoundedBorder((Control)sender, e, 8, Color.FromArgb(191, 191, 191), 1);

            Log.writeMessage("DTY popuppanel_Paint - End : " + DateTime.Now);
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            Log.writeMessage("DTY textBox1_KeyPress - Start : " + DateTime.Now);

            if (sender is System.Windows.Forms.TextBox txt)
            {
                // Allow control keys (backspace, delete, etc.)
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

            Log.writeMessage("DTY textBox1_KeyPress - End : " + DateTime.Now);
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            Log.writeMessage("DTY textBox1_KeyDown - Start : " + DateTime.Now);

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

            Log.writeMessage("DTY textBox1_KeyDown - End : " + DateTime.Now);
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            Log.writeMessage("DTY textBox1_Enter - Start : " + DateTime.Now);

            System.Windows.Forms.TextBox tb = sender as System.Windows.Forms.TextBox;

            if (!string.IsNullOrEmpty(tb.Text))
                tb.SelectAll();

            Log.writeMessage("DTY textBox1_Enter - End : " + DateTime.Now);
        }

        private void checkBox1_KeyDown(object sender, KeyEventArgs e)
        {
            Log.writeMessage("DTY checkBox1_KeyDown - Start : " + DateTime.Now);

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

            Log.writeMessage("DTY checkBox1_KeyDown - End : " + DateTime.Now);
        }

        private void LineNoList_KeyDown(object sender, KeyEventArgs e)
        {
            Log.writeMessage("DTY LineNoList_KeyDown - Start : " + DateTime.Now);

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
                var machineList = _masterService.GetMachineList("TexturisingLot", "").Result.OrderBy(x => x.MachineName).ToList();
                machineList.Insert(0, new MachineResponse { MachineId = 0, MachineName = "Select Line No." });
                LineNoList.DataSource = machineList;
                LineNoList.DisplayMember = "MachineName";
                LineNoList.ValueMember = "MachineId";
                LineNoList.SelectedIndex = 0;
                LineNoList.DroppedDown = true; // Open the dropdown list
                e.SuppressKeyPress = true;    // Prevent any side effect
            }

            Log.writeMessage("DTY LineNoList_KeyDown - End : " + DateTime.Now);
        }

        private void MergeNoList_KeyDown(object sender, KeyEventArgs e)
        {
            Log.writeMessage("DTY MergeNoList_KeyDown - Start : " + DateTime.Now);

            if (e.KeyCode == Keys.ShiftKey) // Detect Shift key
            {
                MergeNoList.DroppedDown = true; // Open the dropdown list
                e.SuppressKeyPress = true;    // Prevent any side effect
            }
            if (e.KeyCode == Keys.Escape)
            {
                MergeNoList.DroppedDown = false;
            }
            if (e.KeyCode == Keys.F2) // Detect F2 key
            {
                selectedMachineid = 0;      // Make selectedMachineid, selectedDeptId so that all mergeno will get in list
                selectedDeptId = 0;
                MergeNoList.DataSource = null;
                var mergenoList = _productionService.getLotsByLotType("TexturisingLot", "").Result.OrderBy(x => x.LotNoFrmt).ToList();
                mergenoList.Insert(0, new LotsResponse { LotId = 0, LotNoFrmt = "Select MergeNo" });
                MergeNoList.DisplayMember = "LotNoFrmt";
                MergeNoList.ValueMember = "LotId";
                MergeNoList.DataSource = mergenoList;
                MergeNoList.SelectedIndex = 0;
                MergeNoList.DroppedDown = true; // Open the dropdown list
                e.SuppressKeyPress = true;    // Prevent any side effect
            }

            Log.writeMessage("DTY MergeNoList_KeyDown - End : " + DateTime.Now);
        }

        private void PackSizeList_KeyDown(object sender, KeyEventArgs e)
        {
            Log.writeMessage("DTY PackSizeList_KeyDown - Start : " + DateTime.Now);

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
                var packsizeList = _masterService.GetPackSizeList(selectedMainItemTypeid, "").Result.OrderBy(x => x.PackSizeName).ToList();
                packsizeList.Insert(0, new PackSizeResponse { PackSizeId = 0, PackSizeName = "Select Pack Size" });
                PackSizeList.DisplayMember = "PackSizeName";
                PackSizeList.ValueMember = "PackSizeId";
                PackSizeList.DataSource = packsizeList;
                PackSizeList.SelectedIndex = 0;
                PackSizeList.DroppedDown = true; // Open the dropdown list
                e.SuppressKeyPress = true;    // Prevent any side effect
            }

            Log.writeMessage("DTY PackSizeList_KeyDown - End : " + DateTime.Now);
        }

        private void QualityList_KeyDown(object sender, KeyEventArgs e)
        {
            Log.writeMessage("DTY QualityList_KeyDown - Start : " + DateTime.Now);

            if (e.KeyCode == Keys.ShiftKey) // Detect Shift key
            {
                QualityList.DroppedDown = true; // Open the dropdown list
                e.SuppressKeyPress = true;    // Prevent any side effect
            }
            if (e.KeyCode == Keys.Escape)
            {
                QualityList.DroppedDown = false;
            }
            if (e.KeyCode == Keys.F2) // Detect F2 key
            {
                QualityList.DataSource = null;
                var qualityList = _masterService.GetQualityListByItemTypeId(selectedItemTypeid).Result.OrderBy(x => x.Name).ToList();
                qualityList.Insert(0, new QualityResponse { QualityId = 0, Name = "Select Quality" });
                QualityList.DisplayMember = "Name";
                QualityList.ValueMember = "QualityId";
                QualityList.DataSource = qualityList;
                QualityList.SelectedIndex = 0;
                QualityList.DroppedDown = true; // Open the dropdown list
                e.SuppressKeyPress = true;    // Prevent any side effect
            }

            Log.writeMessage("DTY QualityList_KeyDown - End : " + DateTime.Now);
        }

        private void SaleOrderList_KeyDown(object sender, KeyEventArgs e)
        {
            Log.writeMessage("DTY SaleOrderList_KeyDown - Start : " + DateTime.Now);

            if (e.KeyCode == Keys.ShiftKey) // Detect Shift key
            {
                SaleOrderList.DroppedDown = true; // Open the dropdown list
                e.SuppressKeyPress = true;    // Prevent any side effect
            }
            if (e.KeyCode == Keys.Escape)
            {
                SaleOrderList.DroppedDown = false;
            }

            Log.writeMessage("DTY SaleOrderList_KeyDown - End : " + DateTime.Now);
        }

        private void WindingTypeList_KeyDown(object sender, KeyEventArgs e)
        {
            Log.writeMessage("DTY WindingTypeList_KeyDown - Start : " + DateTime.Now);

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

            Log.writeMessage("DTY WindingTypeList_KeyDown - End : " + DateTime.Now);
        }

        private void ComPortList_KeyDown(object sender, KeyEventArgs e)
        {
            Log.writeMessage("DTY ComPortList_KeyDown - Start : " + DateTime.Now);

            if (e.KeyCode == Keys.ShiftKey) // Detect Shift key
            {
                ComPortList.DroppedDown = true; // Open the dropdown list
                e.SuppressKeyPress = true;    // Prevent any side effect
            }
            if (e.KeyCode == Keys.Escape)
            {
                ComPortList.DroppedDown = false;
            }

            Log.writeMessage("DTY ComPortList_KeyDown - End : " + DateTime.Now);
        }

        private void WeighingList_KeyDown(object sender, KeyEventArgs e)
        {
            Log.writeMessage("DTY WeighingList_KeyDown - Start : " + DateTime.Now);

            if (e.KeyCode == Keys.ShiftKey) // Detect Shift key
            {
                WeighingList.DroppedDown = true; // Open the dropdown list
                e.SuppressKeyPress = true;    // Prevent any side effect
            }
            if (e.KeyCode == Keys.Escape)
            {
                WeighingList.DroppedDown = false;
            }

            Log.writeMessage("DTY WeighingList_KeyDown - End : " + DateTime.Now);
        }

        private void CopsItemList_KeyDown(object sender, KeyEventArgs e)
        {
            Log.writeMessage("DTY CopsItemList_KeyDown - Start : " + DateTime.Now);

            if (e.KeyCode == Keys.ShiftKey) // Detect Shift key
            {
                CopsItemList.DroppedDown = true; // Open the dropdown list
                e.SuppressKeyPress = true;    // Prevent any side effect
            }
            if (e.KeyCode == Keys.Escape)
            {
                CopsItemList.DroppedDown = false;
            }
            if (e.KeyCode == Keys.F2) // Detect F2 key
            {
                CopsItemList.DataSource = null;
                var copsitemList = _masterService.GetItemList(itemCopsCategoryId, "").Result.OrderBy(x => x.Name).ToList();
                copsitemList.Insert(0, new ItemResponse { ItemId = 0, Name = "Select Cops Item" });
                CopsItemList.DisplayMember = "Name";
                CopsItemList.ValueMember = "ItemId";
                CopsItemList.DataSource = copsitemList;
                CopsItemList.SelectedIndex = 0;
                CopsItemList.DroppedDown = true; // Open the dropdown list
                e.SuppressKeyPress = true;    // Prevent any side effect
            }

            Log.writeMessage("DTY CopsItemList_KeyDown - End : " + DateTime.Now);
        }

        private void BoxItemList_KeyDown(object sender, KeyEventArgs e)
        {
            Log.writeMessage("DTY BoxItemList_KeyDown - Start : " + DateTime.Now);

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

            Log.writeMessage("DTY BoxItemList_KeyDown - End : " + DateTime.Now);
        }

        private void DeptList_KeyDown(object sender, KeyEventArgs e)
        {
            Log.writeMessage("DTY DeptList_KeyDown - Start : " + DateTime.Now);

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
                var deptList = _masterService.GetDepartmentList("DTY", "").Result.OrderBy(x => x.DepartmentName).ToList();
                deptList.Insert(0, new DepartmentResponse { DepartmentId = 0, DepartmentName = "Select Dept" });
                DeptList.DisplayMember = "DepartmentName";
                DeptList.ValueMember = "DepartmentId";
                DeptList.DataSource = deptList;
                DeptList.SelectedIndex = 0;
                DeptList.DroppedDown = true; // Open the dropdown list
                e.SuppressKeyPress = true;    // Prevent any side effect
            }

            Log.writeMessage("DTY DeptList_KeyDown - End : " + DateTime.Now);
        }

        private void OwnerList_KeyDown(object sender, KeyEventArgs e)
        {
            Log.writeMessage("DTY OwnerList_KeyDown - Start : " + DateTime.Now);

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

            Log.writeMessage("DTY OwnerList_KeyDown - End : " + DateTime.Now);
        }

        private void ResetForm(Control parent)
        {
            Log.writeMessage("DTY ResetForm - Start : " + DateTime.Now);

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
                spoolwt.Text = "0";
                palletwtno.Text = "0";
                grosswtno.Text = "0";
                tarewt.Text = "0";
                netwt.Text = "0";
                wtpercop.Text = "0";
                boxpalletitemwt.Text = "0";
                boxpalletstock.Text = "0";
                copsitemwt.Text = "0";
                boxpalletitemwt.Text = "0";
                frdenier.Text = "0";
                updenier.Text = "0";
                deniervalue.Text = "0";
                partyn.Text = "";
                partyshade.Text = "";
                itemname.Text = "";
                shadename.Text = "";
                shadecd.Text = "";
                prodtype.Text = "";
                twistvalue.Text = "";
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
                selectedSONumber = "";
                prcompany.Checked = false;
                prowner.Checked = false;
                spoolno.Text = "0";
                productionRequest = new ProductionRequest();
                salelotvalue.Text = "";
                lastbox.Text = "";
                boxnofrmt.Text = "";
                copstxtbox.Text = "";
                tarewghttxtbox.Text = "";
                grosswttxtbox.Text = "";
                netwttxtbox.Text = "";
                _productionId = 0;
                dateTimePicker2.Value = DateTime.Now;
                selectedSrProductionDate = dateTimePicker2.Value.ToString("dd-MM-yyyy");
                dateTimePicker2.Format = DateTimePickerFormat.Custom;
                dateTimePicker2.CustomFormat = "dd/MM/yyyy";
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

            Log.writeMessage("DTY ResetForm - End : " + DateTime.Now);
        }

        private void prcompany_CheckedChanged(object sender, EventArgs e)
        {
            Log.writeMessage("DTY prcompany_CheckedChanged - Start : " + DateTime.Now);

            if (!isFormReady) return;

            if (prcompany.Checked)
            {
                prowner.Checked = false;
                prcompany.Focus();       // keep focus on the current one
            }

            Log.writeMessage("DTY prcompany_CheckedChanged - End : " + DateTime.Now);
        }

        private void prowner_CheckedChanged(object sender, EventArgs e)
        {
            Log.writeMessage("DTY prowner_CheckedChanged - Start : " + DateTime.Now);

            if (!isFormReady) return;

            if (prowner.Checked)
            {
                prcompany.Checked = false;
                prowner.Focus();           // keep focus
            }

            Log.writeMessage("DTY prowner_CheckedChanged - End : " + DateTime.Now);
        }

        private void txtNumeric_KeyPress(object sender, KeyPressEventArgs e)
        {
            Log.writeMessage("DTY txtNumeric_KeyPress - Start : " + DateTime.Now);

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

            string newText =
                    txt.Text.Remove(txt.SelectionStart, txt.SelectionLength)
                .Insert(txt.SelectionStart, e.KeyChar.ToString());

            // Check for 3 digits after decimal
            if (newText.Contains('.'))
            {
                int decimalIndex = newText.IndexOf('.');
                int digitsAfterDecimal = newText.Length - decimalIndex - 1;
                if (digitsAfterDecimal > 3)
                {
                    e.Handled = true;
                    return;
                }
            }

            Log.writeMessage("DTY txtNumeric_KeyPress - End : " + DateTime.Now);
        }

        private void Control_EnterKeyMoveNext(object sender, KeyEventArgs e)
        {
            Log.writeMessage("DTY Control_EnterKeyMoveNext - Start : " + DateTime.Now);

            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // Prevent the "ding" sound

                Control current = (Control)sender;

                // If current is the last field, move focus to the button
                if (current == grosswtno) // replace with your last field
                {
                    saveprint.Focus(); // replace with your button name
                    saveprint.Paint += _cmethod.Button_Paint;
                }
                else
                {
                    // Move to next control in tab order
                    this.SelectNextControl(current, true, true, true, true);
                }
            }

            Log.writeMessage("DTY Control_EnterKeyMoveNext - End : " + DateTime.Now);
        }

        private void spoolNo_Enter(object sender, EventArgs e)
        {
            Log.writeMessage("DTY spoolNo_Enter - Start : " + DateTime.Now);

            // When control gets focus
            if (spoolno.Text == "0")
            {
                spoolno.Clear(); // remove the default value
            }
            else
            {
                ((System.Windows.Forms.TextBox)sender).SelectAll();
            }

            Log.writeMessage("DTY spoolNo_Enter - End : " + DateTime.Now);
        }

        private void spoolNo_Leave(object sender, EventArgs e)
        {
            Log.writeMessage("DTY spoolNo_Leave - Start : " + DateTime.Now);

            // When control loses focus
            if (string.IsNullOrWhiteSpace(spoolno.Text))
            {
                spoolno.Text = "0"; // restore default
            }

            Log.writeMessage("DTY spoolNo_Leave - End : " + DateTime.Now);
        }

        private void ShowCustomMessage(string boxNo)
        {
            Log.writeMessage("DTY ShowCustomMessage - Start : " + DateTime.Now);

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
                    Text = $"DTY Packing updated successfully for BoxNo {boxNo}.",
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
                msgForm.ShowDialog(this);
            }

            Log.writeMessage("DTY ShowCustomMessage - End : " + DateTime.Now);
        }

        private void ComboBox_Leave(object sender, EventArgs e)
        {
            Log.writeMessage("DTY ComboBox_Leave - Start : " + DateTime.Now);

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

            Log.writeMessage("DTY ComboBox_Leave - End : " + DateTime.Now);
        }

        private void txtNumeric_Leave(object sender, EventArgs e)
        {
            Log.writeMessage("DTY txtNumeric_Leave - Start : " + DateTime.Now);

            FormatToThreeDecimalPlaces(sender as System.Windows.Forms.TextBox);

            Log.writeMessage("DTY txtNumeric_Leave - End : " + DateTime.Now);
        }

        private void FormatToThreeDecimalPlaces(System.Windows.Forms.TextBox textBox)
        {
            Log.writeMessage("DTY FormatToThreeDecimalPlaces - Start : " + DateTime.Now);

            if (decimal.TryParse(textBox.Text, out decimal value))
                textBox.Text = value.ToString("0.000");
            else
                textBox.Text = "0.000"; // optional fallback

            Log.writeMessage("DTY FormatToThreeDecimalPlaces - End : " + DateTime.Now);
        }

        private void AdjustNameByCharCount()
        {
            Log.writeMessage("DTY AdjustNameByCharCount - Start : " + DateTime.Now);

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
            if (itemCharCount > 30)
            {
                itemname.Location = new System.Drawing.Point(38, -3);
            }
            else
            {
                itemname.Location = new System.Drawing.Point(38, 5);
            }

            int boxnoCharCount = boxnofrmt.Text.Length;
            if (boxnoCharCount > 8)
            {
                boxnofrmt.Location = new System.Drawing.Point(34, -3);
            }
            else
            {
                boxnofrmt.Location = new System.Drawing.Point(34, 5);
            }

            Log.writeMessage("DTY AdjustNameByCharCount - End : " + DateTime.Now);
        }

        private void ResetDependentDropdownValues()
        {
            Log.writeMessage("DTY ResetDependentDropdownValues - Start : " + DateTime.Now);

            SaleOrderList.DataSource = null;
            SaleOrderList.Items.Clear();
            SaleOrderList.Items.Add("Select Sale Order Item");
            SaleOrderList.SelectedItem = "Select Sale Order Item";

            QualityList.DataSource = null;
            QualityList.Items.Clear();
            QualityList.Items.Add("Select Quality");
            QualityList.SelectedItem = "Select Quality";

            WindingTypeList.DataSource = null;
            WindingTypeList.Items.Clear();
            WindingTypeList.Items.Add("Select Winding Type");
            WindingTypeList.SelectedItem = "Select Winding Type";

            Log.writeMessage("DTY ResetDependentDropdownValues - End : " + DateTime.Now);
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            Log.writeMessage("DTY btnFind_Click - Start : " + DateTime.Now);

            if (datalistpopuppanel.Visible) datalistpopuppanel.Visible = false;
            popuppanel.Visible = true;
            popuppanel.BringToFront();

            // Center popup in form
            popuppanel.Left = (this.ClientSize.Width - popuppanel.Width) / 2;
            popuppanel.Top = (this.ClientSize.Height - popuppanel.Height) / 2;

            SrLineNoList.Focus();

            Log.writeMessage("DTY btnFind_Click - End : " + DateTime.Now);
        }

        private void btnClosePopup_Click(object sender, EventArgs e)
        {
            Log.writeMessage("DTY btnClosePopup_Click - Start : " + DateTime.Now);

            popuppanel.Visible = false;
            dateTimePicker2.Value = DateTime.Now;
            //srlinenoradiobtn.Checked = srdeptradiobtn.Checked = srproddateradiobtn.Checked = srboxnoradiobtn.Checked = false;
            //SrLineNoList.Enabled = SrDeptList.Enabled = SrBoxNoList.Enabled = dateTimePicker2.Enabled = false;
            selectedSrMachineId = 0; selectedSrDeptId = 0; selectedSrBoxNo = null; selectedSrProductionDate = dateTimePicker2.Value.ToString("dd-MM-yyyy");
            dateTimePicker2.Format = DateTimePickerFormat.Custom;
            dateTimePicker2.CustomFormat = "dd/MM/yyyy";
            LoadSearchDropdowns();
            findbtn.Focus();

            Log.writeMessage("DTY btnClosePopup_Click - End : " + DateTime.Now);
        }

        private void SrLineNoList_TextUpdate(object sender, EventArgs e)
        {
            Log.writeMessage("DTY SrLineNoList_TextUpdate - Start : " + DateTime.Now);

            System.Windows.Forms.ComboBox cb = (System.Windows.Forms.ComboBox)sender;
            string typedText = cb.Text;

            if (string.IsNullOrWhiteSpace(cb.Text))
            {
                cb.TextUpdate -= SrLineNoList_TextUpdate;

                cb.SelectedIndex = 0;   // "Select Line No."
                cb.Text = string.Empty;
                cb.DroppedDown = false;
                selectedSrMachineId = 0;

                cb.TextUpdate += SrLineNoList_TextUpdate;
                return;
            }

            int cursorPosition = cb.SelectionStart;

            if (typedText.Length >= 2)
            {
                List<MachineResponse> machineList = new List<MachineResponse>();
                if (selectedSrDeptId == 0)
                {
                    machineList = _masterService.GetMachineList("TexturisingLot", typedText).Result.OrderBy(x => x.MachineName).ToList();
                    machineList.Insert(0, new MachineResponse { MachineId = 0, MachineName = "Select Line No." });
                }
                else
                {
                    machineList = _masterService.GetMachineByDepartmentIdAndLotType(selectedSrDeptId, "SpinningLot").Result.OrderBy(x => x.MachineName).ToList();
                    machineList.Insert(0, new MachineResponse { MachineId = 0, MachineName = "Select Line No." });
                }

                SrLineNoList.BeginUpdate();
                SrLineNoList.DataSource = null;
                SrLineNoList.DisplayMember = "MachineName";
                SrLineNoList.ValueMember = "MachineId";
                SrLineNoList.DataSource = machineList;
                SrLineNoList.EndUpdate();

                SrLineNoList.TextUpdate -= SrLineNoList_TextUpdate;
                SrLineNoList.DroppedDown = true;
                SrLineNoList.SelectionLength = typedText.Length;
                SrLineNoList.SelectedIndex = -1;
                SrLineNoList.Text = typedText;
                SrLineNoList.SelectionStart = cursorPosition;
                SrLineNoList.TextUpdate += SrLineNoList_TextUpdate;
            }

            Log.writeMessage("DTY SrLineNoList_TextUpdate - End : " + DateTime.Now);
        }

        private void SrDeptList_TextUpdate(object sender, EventArgs e)
        {
            Log.writeMessage("DTY SrDeptList_TextUpdate - Start : " + DateTime.Now);

            System.Windows.Forms.ComboBox cb = (System.Windows.Forms.ComboBox)sender;
            string typedText = cb.Text;

            if (string.IsNullOrWhiteSpace(cb.Text))
            {
                cb.TextUpdate -= SrDeptList_TextUpdate;

                cb.SelectedIndex = 0;
                cb.Text = string.Empty;
                cb.DroppedDown = false;
                selectedSrDeptId = 0;

                cb.TextUpdate += SrDeptList_TextUpdate;
                return;
            }

            int cursorPosition = cb.SelectionStart;

            if (typedText.Length >= 2)
            {
                //DeptList.Items.Clear();

                var deptList = _masterService.GetDepartmentList("DTY", typedText).Result.OrderBy(x => x.DepartmentName).ToList();

                deptList.Insert(0, new DepartmentResponse { DepartmentId = 0, DepartmentName = "Select Dept" });

                SrDeptList.BeginUpdate();
                SrDeptList.DataSource = null;
                SrDeptList.DisplayMember = "DepartmentName";
                SrDeptList.ValueMember = "DepartmentId";
                SrDeptList.DataSource = deptList;
                SrDeptList.EndUpdate();

                SrDeptList.TextUpdate -= SrDeptList_TextUpdate;
                SrDeptList.DroppedDown = true;
                SrDeptList.SelectionLength = typedText.Length;
                SrDeptList.SelectedIndex = -1;
                SrDeptList.Text = typedText;
                SrDeptList.SelectionStart = cursorPosition;
                SrDeptList.TextUpdate += SrDeptList_TextUpdate;

            }
            Log.writeMessage("DTY SrDeptList_TextUpdate - End : " + DateTime.Now);
        }

        private void SrBoxNoList_TextUpdate(object sender, EventArgs e)
        {
            Log.writeMessage("DTY SrBoxNoList_TextUpdate - Start : " + DateTime.Now);

            System.Windows.Forms.ComboBox cb = (System.Windows.Forms.ComboBox)sender;
            string typedText = cb.Text;

            if (string.IsNullOrWhiteSpace(cb.Text))
            {
                cb.TextUpdate -= SrBoxNoList_TextUpdate;

                cb.SelectedIndex = 0;
                cb.Text = string.Empty;
                cb.DroppedDown = false;
                selectedSrBoxNo = null;

                cb.TextUpdate += SrBoxNoList_TextUpdate;
                return;
            }

            int cursorPosition = cb.SelectionStart;

            if (typedText.Length >= 2)
            {
                //DeptList.Items.Clear();
                GetProductionList getListRequest = new GetProductionList();
                getListRequest.PackingType = "DTYPacking";
                getListRequest.MachineId = selectedSrMachineId;
                getListRequest.DeptId = selectedSrDeptId;
                getListRequest.SubString = typedText;

                var srboxnoList = _packingService.getAllBoxNoByPackingType(getListRequest).Result;

                srboxnoList.Insert(0, new ProductionResponse { ProductionId = 0, BoxNo = "Select BoxNo" });

                SrBoxNoList.BeginUpdate();
                SrBoxNoList.DataSource = null;
                SrBoxNoList.DisplayMember = "BoxNo";
                SrBoxNoList.ValueMember = "ProductionId";
                SrBoxNoList.DataSource = srboxnoList;
                SrBoxNoList.EndUpdate();

                SrBoxNoList.TextUpdate -= SrBoxNoList_TextUpdate;
                SrBoxNoList.DroppedDown = true;
                SrBoxNoList.SelectionLength = typedText.Length;
                SrBoxNoList.SelectedIndex = -1;
                SrBoxNoList.Text = typedText;
                SrBoxNoList.SelectionStart = cursorPosition;
                SrBoxNoList.TextUpdate += SrBoxNoList_TextUpdate;

            }
            Log.writeMessage("DTY SrBoxNoList_TextUpdate - End : " + DateTime.Now);
        }

        //private void rbLineNo_CheckedChanged(object sender, EventArgs e)
        //{
        //    Log.writeMessage("DTY rbLineNo_CheckedChanged - Start : " + DateTime.Now);

        //    if (!srlinenoradiobtn.Checked)
        //        return;

        //    SrLineNoList.Enabled = srlinenoradiobtn.Checked;
        //    SrDeptList.Enabled = false;
        //    SrBoxNoList.Enabled = false;
        //    dateTimePicker2.Enabled = false;

        //    Log.writeMessage("DTY rbLineNo_CheckedChanged - End : " + DateTime.Now);
        //}

        //private void rbDepartment_CheckedChanged(object sender, EventArgs e)
        //{
        //    Log.writeMessage("DTY rbDepartment_CheckedChanged - Start : " + DateTime.Now);

        //    if (!srdeptradiobtn.Checked)
        //        return;

        //    SrDeptList.Enabled = srdeptradiobtn.Checked;
        //    SrLineNoList.Enabled = false;
        //    SrBoxNoList.Enabled = false;
        //    dateTimePicker2.Enabled = false;

        //    Log.writeMessage("DTY rbDepartment_CheckedChanged - End : " + DateTime.Now);
        //}

        //private void rbBoxNo_CheckedChanged(object sender, EventArgs e)
        //{
        //    Log.writeMessage("DTY rbBoxNo_CheckedChanged - Start : " + DateTime.Now);

        //    if (!srboxnoradiobtn.Checked)
        //        return;

        //    SrBoxNoList.Enabled = srboxnoradiobtn.Checked;
        //    SrLineNoList.Enabled = false;
        //    SrDeptList.Enabled = false;
        //    dateTimePicker2.Enabled = false;

        //    Log.writeMessage("DTY rbBoxNo_CheckedChanged - End : " + DateTime.Now);
        //}

        //private void rbDate_CheckedChanged(object sender, EventArgs e)
        //{
        //    Log.writeMessage("DTY rbDate_CheckedChanged - Start : " + DateTime.Now);

        //    if (!srproddateradiobtn.Checked)
        //        return;

        //    dateTimePicker2.Enabled = srproddateradiobtn.Checked;
        //    SrLineNoList.Enabled = false;
        //    SrDeptList.Enabled = false;
        //    SrBoxNoList.Enabled = false;

        //    Log.writeMessage("DTY rbDate_CheckedChanged - End : " + DateTime.Now);
        //}

        //public List<ProductionResponse> GetPackingList(int machineId, int deptId, string boxNo, string productionDate)
        //{
        //    Log.writeMessage("DTY GetPackingList - Start : " + DateTime.Now);
           
        //    packingList = _packingService.getProductionDetailsBySelectedParameter("DTYPacking", machineId, deptId, boxNo, productionDate).Result;
            
        //    Log.writeMessage("DTY GetPackingList - End : " + DateTime.Now);

        //    return packingList;
        //}

        private void btnSearch_Click(object sender, EventArgs e)
        {
            Log.writeMessage("DTY btnSearch_Click - Start : " + DateTime.Now);

            if (selectedSrMachineId == 0 && selectedSrDeptId == 0 && (string.IsNullOrEmpty(selectedSrBoxNo)) && (string.IsNullOrEmpty(selectedSrProductionDate)))
            {
                MessageBox.Show("Please select at least any one option.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            getProductionList(1);

            Log.writeMessage("DTY btnSearch_Click - End : " + DateTime.Now);
        }

        private void getProductionList(int currentPage)
        {
            Log.writeMessage("DTY getProductionList - Start : " + DateTime.Now);

            //int machineid = 0, deptid = 0;
            //string boxnoid = null;
            //string proddt = null;
            //if (srlinenoradiobtn.Checked) { machineid = selectedSrMachineId; }
            //if (srdeptradiobtn.Checked) { deptid = selectedSrDeptId; }
            //if (srboxnoradiobtn.Checked) { boxnoid = selectedSrBoxNo; }
            //if (srproddateradiobtn.Checked) { proddt = selectedSrProductionDate; }

            GetProductionList getListRequest = new GetProductionList();
            getListRequest.PackingType = "DTYPacking";
            getListRequest.MachineId = selectedSrMachineId;
            getListRequest.DeptId = selectedSrDeptId;
            getListRequest.BoxNo = selectedSrBoxNo;
            getListRequest.ProductionDate = selectedSrProductionDate;
            getListRequest.PageNumber = currentPage;
            getListRequest.PageSize = pageSize;

            packingList = _packingService.getProductionDetailsBySelectedParameter(getListRequest).Result;

            if (packingList.Count > 0)
            {
                datalistpopuppanel.Visible = true;
                datalistpopuppanel.BringToFront();

                findbtn.Enabled = false;
                cancelbtn.Enabled = false;

                totalPages = (int)Math.Ceiling((double)packingList[0].TotalCount / pageSize);
                lblPageInfo.Text = $"Page {currentPage} of {totalPages}";
                // Center popup in form
                datalistpopuppanel.Left = (this.ClientSize.Width - datalistpopuppanel.Width) / 2;
                datalistpopuppanel.Top = (this.ClientSize.Height - datalistpopuppanel.Height) / 2;

                dataGridView1.Focus();
                dataGridView1.AutoGenerateColumns = false;
                dataGridView1.Columns.Clear();

                // Define columns
                dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { Name = "SrNo", HeaderText = "SR. No", SortMode = DataGridViewColumnSortMode.Automatic });
                //dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { Name = "PackingType", DataPropertyName = "PackingType", HeaderText = "Packing Type" });
                dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { Name = "DepartmentName", DataPropertyName = "DepartmentName", HeaderText = "Department", SortMode = DataGridViewColumnSortMode.Automatic });
                dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { Name = "MachineName", DataPropertyName = "MachineName", HeaderText = "Machine", SortMode = DataGridViewColumnSortMode.Automatic });
                dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { Name = "LotNo", DataPropertyName = "LotNo", HeaderText = "Lot No", SortMode = DataGridViewColumnSortMode.Automatic });
                dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { Name = "BoxNo", DataPropertyName = "BoxNoFmtd", HeaderText = "Box No", SortMode = DataGridViewColumnSortMode.Automatic });
                dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "ProductionDate",
                    DataPropertyName = "ProductionDate",
                    HeaderText = "Production Date",
                    SortMode = DataGridViewColumnSortMode.Automatic,
                    ValueType = typeof(DateTime),
                    DefaultCellStyle = { Format = "dd/MM/yyyy", Font = FontManager.GetFont(8F, FontStyle.Regular), Alignment = DataGridViewContentAlignment.MiddleLeft }
                });
                //dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { Name = "QualityName", DataPropertyName = "QualityName", HeaderText = "Quality" });
                dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { Name = "SalesOrderNumber", DataPropertyName = "SalesOrderNumber", HeaderText = "Sales Order", SortMode = DataGridViewColumnSortMode.Automatic });
                //dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { Name = "PackSizeName", DataPropertyName = "PackSizeName", HeaderText = "Pack Size" });
                //dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { Name = "WindingTypeName", DataPropertyName = "WindingTypeName", HeaderText = "Winding Type" });
                //dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { Name = "ProductionType", DataPropertyName = "ProductionType", HeaderText = "Production Type" });
                //dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { Name = "NoOfCopies", DataPropertyName = "NoOfCopies", HeaderText = "Copies" });

                dataGridView1.Columns["SrNo"].DefaultCellStyle.Font = FontManager.GetFont(8F, FontStyle.Regular);
                dataGridView1.Columns["DepartmentName"].DefaultCellStyle.Font = FontManager.GetFont(8F, FontStyle.Regular);
                dataGridView1.Columns["MachineName"].DefaultCellStyle.Font = FontManager.GetFont(8F, FontStyle.Regular);
                dataGridView1.Columns["LotNo"].DefaultCellStyle.Font = FontManager.GetFont(8F, FontStyle.Regular);
                dataGridView1.Columns["BoxNo"].DefaultCellStyle.Font = FontManager.GetFont(8F, FontStyle.Regular);
                dataGridView1.Columns["ProductionDate"].DefaultCellStyle.Font = FontManager.GetFont(8F, FontStyle.Regular);
                dataGridView1.Columns["SalesOrderNumber"].DefaultCellStyle.Font = FontManager.GetFont(8F, FontStyle.Regular);

                dataGridView1.Columns["SrNo"].Width = 50;
                dataGridView1.Columns["DepartmentName"].Width = 110;
                dataGridView1.Columns["BoxNo"].Width = 120;

                // Add Edit button column
                DataGridViewImageColumn btn = new DataGridViewImageColumn();
                btn.HeaderText = "Action";
                btn.Name = "Action";
                btn.Image = _cmethod.ResizeImage(Properties.Resources.icons8_edit_48, 20, 20);
                btn.ImageLayout = DataGridViewImageCellLayout.Normal;
                btn.Width = 45;  // column width
                btn.SortMode = DataGridViewColumnSortMode.NotSortable;
                dataGridView1.RowTemplate.Height = 40; // row height
                dataGridView1.Columns.Add(btn);

                //dataGridView1.DataSource = packingList;
                ListtoDataTableConverter converter = new ListtoDataTableConverter();
                DataTable dt = converter.ToDataTable(packingList);
                dataGridView1.DataSource = dt;

                dataGridView1.CellContentClick += dataGridView1_CellContentClick;
                dataGridView1.RowPostPaint += dataGridView1_RowPostPaint;

                dataGridView1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGridView1_KeyDown);

                dataGridView1.CellMouseEnter += (s, te) =>
                {
                    if (te.RowIndex >= 0 && te.ColumnIndex >= 0 &&
                        dataGridView1.Columns[te.ColumnIndex].Name == "Action")
                    {
                        dataGridView1.Cursor = Cursors.Hand;
                    }
                    else
                    {
                        dataGridView1.Cursor = Cursors.Default;
                    }
                };

                dataGridView1.CellMouseLeave += (s, te) =>
                {
                    dataGridView1.Cursor = Cursors.Default; // Reset back to default
                };

                LoadSearchDropdowns();
            }
            else
            {
                dataGridView1.DataSource = null;
                MessageBox.Show("Data not found", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            Log.writeMessage("DTY getProductionList - End : " + DateTime.Now);
        }

        private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            int srNo = (currentPage - 1) * pageSize + e.RowIndex + 1;
            dataGridView1.Rows[e.RowIndex].Cells["SrNo"].Value = srNo;
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            Log.writeMessage("DTY btnNext_Click - Start : " + DateTime.Now);

            if (currentPage < totalPages)
            {
                currentPage++;
                getProductionList(currentPage);
            }

            Log.writeMessage("DTY btnNext_Click - End : " + DateTime.Now);
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            Log.writeMessage("DTY btnPrevious_Click - Start : " + DateTime.Now);

            if (currentPage > 1)
            {
                currentPage--;
                getProductionList(currentPage);
            }

            Log.writeMessage("DTY btnPrevious_Click - End : " + DateTime.Now);
        }

        private void btnFirstPg_Click(object sender, EventArgs e)
        {
            Log.writeMessage("DTY btnFirstPg_Click - Start : " + DateTime.Now);

            if (currentPage <= totalPages)
            {
                currentPage = 1;
                getProductionList(currentPage);
            }

            Log.writeMessage("DTY btnFirstPg_Click - End : " + DateTime.Now);
        }

        private void btnLastPg_Click(object sender, EventArgs e)
        {
            Log.writeMessage("DTY btnLastPg_Click - Start : " + DateTime.Now);

            if (currentPage < totalPages)
            {
                currentPage = totalPages;
                getProductionList(currentPage);
            }

            Log.writeMessage("DTY btnLastPg_Click - End : " + DateTime.Now);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            Log.writeMessage("DTY dataGridView1_CellContentClick - Start : " + DateTime.Now);

            if (e.RowIndex >= 0 && e.ColumnIndex == dataGridView1.Columns["Action"].Index)
            {
                EditRow(e.RowIndex);
            }

            Log.writeMessage("DTY dataGridView1_CellContentClick - End : " + DateTime.Now);
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            Log.writeMessage("DTY dataGridView1_KeyDown - Start : " + DateTime.Now);

            if ((e.KeyCode == Keys.Enter || e.KeyCode == Keys.Space) &&
                dataGridView1.CurrentCell?.OwningColumn?.Name == "Action")
            {
                EditRow(dataGridView1.CurrentCell.RowIndex);
                e.Handled = true;
            }

            Log.writeMessage("DTY dataGridView1_KeyDown - End : " + DateTime.Now);
        }

        private async void EditRow(int rowIndex)
        {
            Log.writeMessage("DTY EditRow - Start : " + DateTime.Now);

            //if (rowIndex < 0 || rowIndex >= dataGridView1.Rows.Count)
            //    return;

            //var rowObj = dataGridView1.Rows[rowIndex].DataBoundItem as ProductionResponse;
            //if (!rowObj.CanModifyDelete)
            //    return;

            DataRowView drv = dataGridView1.Rows[rowIndex].DataBoundItem as DataRowView;

            if (drv == null) return;

            bool canModifyDelete =
                Convert.ToBoolean(drv["CanModifyDelete"] == DBNull.Value
                    ? false
                    : drv["CanModifyDelete"]);

            if (!canModifyDelete)
                return;

            popuppanel.Visible = false;
            datalistpopuppanel.Visible = false;

            //long productionId = Convert.ToInt32(
            //    ((ProductionResponse)dataGridView1.Rows[rowIndex].DataBoundItem).ProductionId
            //);

            long productionId = Convert.ToInt32(drv["ProductionId"]);

            var getSelectedProductionDetails = _packingService.getLastBoxDetails("dtypacking", productionId).Result;

            //SelectedProductionDetails
            if (getSelectedProductionDetails.ProductionId > 0)
            {
                _productionId = getSelectedProductionDetails.ProductionId;
                await LoadProductionDetailsAsync(getSelectedProductionDetails);

                this.copstxtbox.Text = getSelectedProductionDetails.LastBoxSpools.ToString();
                this.tarewghttxtbox.Text = getSelectedProductionDetails.LastBoxTareWt.ToString();
                this.grosswttxtbox.Text = getSelectedProductionDetails.LastBoxGrossWt.ToString();
                this.netwttxtbox.Text = getSelectedProductionDetails.LastBoxNetWt.ToString();
                this.lastbox.Text = (!string.IsNullOrEmpty(getSelectedProductionDetails.LastBox) ? getSelectedProductionDetails.LastBox : "");
            }

            Log.writeMessage("DTY EditRow - End : " + DateTime.Now);
        }

        private async void SrLineNoList_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Log.writeMessage("DTY SrLineNoList_SelectionChangeCommitted - Start : " + DateTime.Now);

            if (!isFormReady) return; // skip during load

            if (suppressEvents) return;     //Prevent recursive refresh

            if (SrLineNoList.Items.Count == 0) return;

            if (SrLineNoList.SelectedIndex <= 0)
            {
                return;
            }
            suppressEvents = true;          //Freeze dependent dropdown events
            lblLoading.Visible = true;
            try
            {
                if (SrLineNoList.SelectedValue != null)
                {
                    MachineResponse selectedMachine = (MachineResponse)SrLineNoList.SelectedItem;
                    int selectedMachineId = selectedMachine.MachineId;
                    if (selectedMachineId > 0)
                    {
                        selectedSrMachineId = selectedMachine.MachineId;

                        if (selectedMachine != null)
                        {
                            var deptTask = _masterService.GetDepartmentList("DTY", selectedMachine.DepartmentName).Result;
                            deptTask.Insert(0, new DepartmentResponse { DepartmentId = 0, DepartmentName = "Select Dept" });
                            SrDeptList.DataSource = deptTask;
                            SrDeptList.SelectedValue = selectedMachine.DepartmentId;
                            selectedSrDeptId = selectedMachine.DepartmentId;
                            SrDeptList.DisplayMember = "DepartmentName";
                            SrDeptList.ValueMember = "DepartmentId";
                            if (SrDeptList.Items.Count > 1)
                            {
                                SrDeptList.SelectedIndex = 1;
                            }
                        }
                    }
                }
            }
            finally
            {
                lblLoading.Visible = false;
                suppressEvents = false;             //Allow events again
            }

            Log.writeMessage("DTY SrLineNoList_SelectionChangeCommitted - End : " + DateTime.Now);
        }

        private async void SrDeptList_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Log.writeMessage("DTY SrDeptList_SelectionChangeCommitted - Start : " + DateTime.Now);

            if (!isFormReady) return; // skip during load

            if (suppressEvents) return;     //Prevent recursive refresh

            if (SrDeptList.Items.Count == 0) return;

            if (SrDeptList.SelectedIndex <= 0)
            {
                return;
            }
            suppressEvents = true;          //Freeze dependent dropdown events
            lblLoading.Visible = true;
            try
            {
                if (SrDeptList.SelectedValue != null)
                {
                    DepartmentResponse selectedDepartment = (DepartmentResponse)SrDeptList.SelectedItem;
                    int selectedDepartmentId = selectedDepartment.DepartmentId;
                    if (selectedDepartmentId > 0)
                    {
                        selectedSrDeptId = selectedDepartment.DepartmentId;
                    }
                }
            }
            finally
            {
                lblLoading.Visible = false;
                suppressEvents = false;             //Allow events again
            }

            Log.writeMessage("DTY SrDeptList_SelectionChangeCommitted - End : " + DateTime.Now);
        }

        private async void SrBoxNoList_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Log.writeMessage("DTY SrBoxNoList_SelectionChangeCommitted - Start : " + DateTime.Now);

            if (!isFormReady) return; // skip during load

            if (suppressEvents) return;     //Prevent recursive refresh

            if (SrBoxNoList.Items.Count == 0) return;

            if (SrBoxNoList.SelectedIndex <= 0)
            {
                return;
            }
            suppressEvents = true;          //Freeze dependent dropdown events
            lblLoading.Visible = true;
            try
            {
                if (SrBoxNoList.SelectedValue != null)
                {
                    ProductionResponse selectedBoxNo = (ProductionResponse)SrBoxNoList.SelectedItem;
                    long selectedProductionId = selectedBoxNo.ProductionId;
                    if (selectedProductionId > 0)
                    {
                        selectedSrBoxNo = selectedBoxNo.BoxNo;
                    }
                }
            }
            finally
            {
                lblLoading.Visible = false;
                suppressEvents = false;             //Allow events again
            }

            Log.writeMessage("DTY SrBoxNoList_SelectionChangeCommitted - End : " + DateTime.Now);
        }

        private void SrProdDate_DropDownClosed(object sender, EventArgs e)
        {
            Log.writeMessage("DTY SrProdDate_DropDownClosed - Start : " + DateTime.Now);

            dateTimePicker2.Format = DateTimePickerFormat.Custom;
            dateTimePicker2.CustomFormat = "dd/MM/yyyy";
            DateTime selectedDate = dateTimePicker2.Value.Date;
            selectedSrProductionDate = selectedDate.ToString("dd-MM-yyyy");

            Log.writeMessage("DTY SrProdDate_DropDownClosed - End : " + DateTime.Now);
        }

        private void SrProdDate_KeyDown(object sender, KeyEventArgs e)
        {
            Log.writeMessage("DTY SrProdDate_KeyDown - Start : " + DateTime.Now);

            if (e.KeyCode == Keys.Delete || e.KeyCode == Keys.Back)
            {
                dateTimePicker2.Format = DateTimePickerFormat.Custom;
                dateTimePicker2.CustomFormat = " ";
                selectedSrProductionDate = null;
            }

            Log.writeMessage("DTY SrProdDate_KeyDown - End : " + DateTime.Now);
        }

        private void btnDatalistClosePopup_Click(object sender, EventArgs e)
        {
            Log.writeMessage("DTY btnDatalistClosePopup_Click - Start : " + DateTime.Now);

            datalistpopuppanel.Visible = false;
            findbtn.Enabled = true;
            cancelbtn.Enabled = true;
            dataGridView1.DataSource = null;
            dateTimePicker2.Value = DateTime.Now;
            //srlinenoradiobtn.Checked = srdeptradiobtn.Checked = srproddateradiobtn.Checked = srboxnoradiobtn.Checked = false;
            //SrLineNoList.Enabled = SrDeptList.Enabled = SrBoxNoList.Enabled = dateTimePicker2.Enabled = false;
            selectedSrMachineId = 0; selectedSrDeptId = 0; selectedSrBoxNo = null; selectedSrProductionDate = dateTimePicker2.Value.ToString("dd-MM-yyyy");
            dateTimePicker2.Format = DateTimePickerFormat.Custom;
            dateTimePicker2.CustomFormat = "dd/MM/yyyy";
            SrLineNoList.Focus();

            Log.writeMessage("DTY btnDatalistClosePopup_Click - End : " + DateTime.Now);
        }

        private void SrLineNoList_KeyDown(object sender, KeyEventArgs e)
        {
            Log.writeMessage("DTY SrLineNoList_KeyDown - Start : " + DateTime.Now);

            if (e.KeyCode == Keys.ShiftKey) // Detect Shift key
            {
                SrLineNoList.DroppedDown = true; // Open the dropdown list
                e.SuppressKeyPress = true;    // Prevent any side effect
            }
            if (e.KeyCode == Keys.Escape)
            {
                SrLineNoList.DroppedDown = false;
            }
            if (e.KeyCode == Keys.F2) // Detect F2 key
            {
                SrLineNoList.DataSource = null;
                var machineList = _masterService.GetMachineList("TexturisingLot", "").Result.OrderBy(x => x.MachineName).ToList();
                machineList.Insert(0, new MachineResponse { MachineId = 0, MachineName = "Select Line No." });
                SrLineNoList.DataSource = machineList;
                SrLineNoList.DisplayMember = "MachineName";
                SrLineNoList.ValueMember = "MachineId";
                SrLineNoList.SelectedIndex = 0;
                SrLineNoList.DroppedDown = true; // Open the dropdown list
                e.SuppressKeyPress = true;    // Prevent any side effect
            }

            Log.writeMessage("DTY SrLineNoList_KeyDown - End : " + DateTime.Now);
        }

        private void SrDeptList_KeyDown(object sender, KeyEventArgs e)
        {
            Log.writeMessage("DTY SrDeptList_KeyDown - Start : " + DateTime.Now);

            if (e.KeyCode == Keys.ShiftKey) // Detect Shift key
            {
                SrDeptList.DroppedDown = true; // Open the dropdown list
                e.SuppressKeyPress = true;    // Prevent any side effect
            }
            if (e.KeyCode == Keys.Escape)
            {
                SrDeptList.DroppedDown = false;
            }
            if (e.KeyCode == Keys.F2) // Detect F2 key
            {
                SrDeptList.DataSource = null;
                var deptList = _masterService.GetDepartmentList("DTY", "").Result.OrderBy(x => x.DepartmentName).ToList();
                deptList.Insert(0, new DepartmentResponse { DepartmentId = 0, DepartmentName = "Select Dept" });
                SrDeptList.DisplayMember = "DepartmentName";
                SrDeptList.ValueMember = "DepartmentId";
                SrDeptList.DataSource = deptList;
                SrDeptList.SelectedIndex = 0;
                SrDeptList.DroppedDown = true; // Open the dropdown list
                e.SuppressKeyPress = true;    // Prevent any side effect
            }

            Log.writeMessage("DTY SrDeptList_KeyDown - End : " + DateTime.Now);
        }

        private void SrBoxNoList_KeyDown(object sender, KeyEventArgs e)
        {
            Log.writeMessage("DTY SrBoxNoList_KeyDown - Start : " + DateTime.Now);

            if (e.KeyCode == Keys.ShiftKey) // Detect Shift key
            {
                SrBoxNoList.DroppedDown = true; // Open the dropdown list
                e.SuppressKeyPress = true;    // Prevent any side effect
            }
            if (e.KeyCode == Keys.Escape)
            {
                SrBoxNoList.DroppedDown = false;
            }
            if (e.KeyCode == Keys.F2) // Detect F2 key
            {
                GetProductionList getListRequest = new GetProductionList();
                getListRequest.PackingType = "DTYPacking";
                getListRequest.MachineId = selectedSrMachineId;
                getListRequest.DeptId = selectedSrDeptId;
                getListRequest.SubString = null;

                SrBoxNoList.DataSource = null;
                var srboxnoList = _packingService.getAllBoxNoByPackingType(getListRequest).Result;
                srboxnoList.Insert(0, new ProductionResponse { ProductionId = 0, BoxNo = "Select BoxNo" });
                SrBoxNoList.DataSource = srboxnoList;
                SrBoxNoList.DisplayMember = "BoxNo";
                SrBoxNoList.ValueMember = "ProductionId";
                SrBoxNoList.SelectedIndex = 0;
                SrBoxNoList.DroppedDown = true; // Open the dropdown list
                e.SuppressKeyPress = true;    // Prevent any side effect
            }

            Log.writeMessage("DTY SrBoxNoList_KeyDown - End : " + DateTime.Now);
        }

        //private void SrLineNoRadiobtn_KeyDown(object sender, KeyEventArgs e)
        //{
        //    Log.writeMessage("DTY SrLineNoRadiobtn_KeyDown - End : " + DateTime.Now);

        //    if (e.KeyCode == Keys.Enter)
        //    {
        //        RadioButton rb = sender as RadioButton;
        //        rb.Checked = !rb.Checked;   // toggle select / deselect
        //        if (rb.Checked) srdeptradiobtn.Checked = srboxnoradiobtn.Checked = srproddateradiobtn.Checked = false;
        //        e.Handled = true;
        //    }

        //    Log.writeMessage("DTY SrLineNoRadiobtn_KeyDown - End : " + DateTime.Now);
        //}

        //private void SrDeptRadiobtn_KeyDown(object sender, KeyEventArgs e)
        //{
        //    Log.writeMessage("DTY SrDeptRadiobtn_KeyDown - End : " + DateTime.Now);

        //    if (e.KeyCode == Keys.Enter)
        //    {
        //        RadioButton rb = sender as RadioButton;
        //        rb.Checked = !rb.Checked;   // toggle select / deselect
        //        if (rb.Checked) srlinenoradiobtn.Checked = srboxnoradiobtn.Checked = srproddateradiobtn.Checked = false;
        //        e.Handled = true;
        //    }

        //    Log.writeMessage("DTY SrDeptRadiobtn_KeyDown - End : " + DateTime.Now);
        //}

        //private void SrBoxNoRadiobtn_KeyDown(object sender, KeyEventArgs e)
        //{
        //    Log.writeMessage("DTY SrBoxNoRadiobtn_KeyDown - End : " + DateTime.Now);

        //    if (e.KeyCode == Keys.Enter)
        //    {
        //        RadioButton rb = sender as RadioButton;
        //        rb.Checked = !rb.Checked;   // toggle select / deselect
        //        if (rb.Checked) srdeptradiobtn.Checked = srlinenoradiobtn.Checked = srproddateradiobtn.Checked = false;
        //        e.Handled = true;
        //    }

        //    Log.writeMessage("DTY SrBoxNoRadiobtn_KeyDown - End : " + DateTime.Now);
        //}

        //private void SrProdDateRadiobtn_KeyDown(object sender, KeyEventArgs e)
        //{
        //    Log.writeMessage("DTY SrProdDateRadiobtn_KeyDown - End : " + DateTime.Now);

        //    if (e.KeyCode == Keys.Enter)
        //    {
        //        RadioButton rb = sender as RadioButton;
        //        rb.Checked = !rb.Checked;   // toggle select / deselect
        //        if (rb.Checked) srdeptradiobtn.Checked = srlinenoradiobtn.Checked = srboxnoradiobtn.Checked = false;
        //        e.Handled = true;
        //    }

        //    Log.writeMessage("DTY SrProdDateRadiobtn_KeyDown - End : " + DateTime.Now);
        //}

        //private void RadioButton_MouseDown(object sender, MouseEventArgs e)
        //{
        //    Log.writeMessage("DTY RadioButton_MouseDown - End : " + DateTime.Now);

        //    RadioButton rb = sender as RadioButton;
        //    if (rb == null) return;

        //    SelectRadio(rb);

        //    Log.writeMessage("DTY RadioButton_MouseDown - End : " + DateTime.Now);
        //}

        //private void SelectRadio(RadioButton selected)
        //{
        //    Log.writeMessage("DTY SelectRadio - End : " + DateTime.Now);

        //    foreach (Control ctrl in selected.Parent.Controls)
        //    {
        //        if (ctrl is RadioButton rb)
        //        {
        //            rb.Checked = (rb == selected);
        //        }
        //    }

        //    Log.writeMessage("DTY SelectRadio - End : " + DateTime.Now);
        //}
    }
}
