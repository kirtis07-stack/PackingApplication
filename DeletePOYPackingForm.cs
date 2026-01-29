using PackingApplication.Helper;
using PackingApplication.Models.CommonEntities;
using PackingApplication.Models.RequestEntities;
using PackingApplication.Models.ResponseEntities;
using PackingApplication.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PackingApplication
{
    public partial class DeletePOYPackingForm : Form
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
        int selectedDeptId = 0;
        int selectedMachineid = 0;
        int selectedItemTypeid = 0;
        List<ProductionResponse> packingList = new List<ProductionResponse>();
        int selectedSrDeptId = 0;
        int selectedSrMachineId = 0;
        string selectedSrBoxNo = null;
        string selectedSrProductionDate = null;
        private int currentPage = 1;
        private int totalPages = 0;
        private int pageSize = 10;
        public DeletePOYPackingForm()
        {
            Log.writeMessage("POY DeletePOYPackingForm - Start : " + DateTime.Now);

            InitializeComponent();
            ApplyFonts();
            //this.Shown += DeletePOYPackingForm_Shown;
            this.AutoScroll = true;
            lblLoading = CommonMethod.InitializeLoadingLabel(this);

            _cmethod.SetButtonBorderRadius(this.cancelbtn, 8);
            _cmethod.SetButtonBorderRadius(this.delete, 8);
            _cmethod.SetButtonBorderRadius(this.findbtn, 8);
            _cmethod.SetButtonBorderRadius(this.closepopupbtn, 8);
            _cmethod.SetButtonBorderRadius(this.searchbtn, 8);
            _cmethod.SetButtonBorderRadius(this.closelistbtn, 8);

            width = flowLayoutPanel1.ClientSize.Width;
            rowMaterial.AutoGenerateColumns = false;
            windinggrid.AutoGenerateColumns = false;
            qualityqty.AutoGenerateColumns = false;

            Log.writeMessage("POY DeletePOYPackingForm - End : " + DateTime.Now);
        }

        private void DeletePOYPackingForm_Load(object sender, EventArgs e)
        {
            Log.writeMessage("POY DeletePOYPackingForm_Load - Start : " + DateTime.Now);

            AddHeader();

            LoadDropdowns();

            copyno.Text = "1";
            spoolno.Text = "0";
            //spoolwt.Text = "0";
            //palletwtno.Text = "0";
            grosswtno.Text = "0";
            tarewt.Text = "0";
            netwt.Text = "0";
            wtpercop.Text = "0";
            copsstock.Text = "0";
            boxpalletstock.Text = "0";
            //copsitemwt.Text = "0";
            //boxpalletitemwt.Text = "0";
            //frdenier.Text = "0";
            //updenier.Text = "0";
            //deniervalue.Text = "0";
            //partyn.Text = "";
            //partyshade.Text = "";
            isFormReady = true;
            dateTimePicker2.Value = DateTime.Now;
            selectedSrProductionDate = dateTimePicker2.Value.ToString("dd-MM-yyyy");
            delete.Enabled = false;
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

            Log.writeMessage("POY DeletePOYPackingForm_Load - Start : " + DateTime.Now);
        }

        private void LoadDropdowns()
        {
            Log.writeMessage("POY LoadDropdowns - Start : " + DateTime.Now);

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
            weightingList = getWeighingList().Result;
            WeighingList.DataSource = weightingList;
            WeighingList.DisplayMember = "Name";
            WeighingList.ValueMember = "Id";
            WeighingList.SelectedIndex = 0;
            WeighingList.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            WeighingList.AutoCompleteSource = AutoCompleteSource.ListItems;

            var palletitemList = new List<ItemResponse>();
            palletitemList.Insert(0, new ItemResponse { ItemId = 0, Name = "Select Box/Pallet" });
            PalletTypeList.DataSource = palletitemList;
            PalletTypeList.DisplayMember = "Name";
            PalletTypeList.ValueMember = "ItemId";
            PalletTypeList.SelectedIndex = 0;

            LoadSearchDropdowns();

            Log.writeMessage("POY LoadDropdowns - End : " + DateTime.Now);
        }

        private void LoadSearchDropdowns()
        {
            Log.writeMessage("POY LoadSearchDropdowns - Start : " + DateTime.Now);

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

            Log.writeMessage("POY LoadSearchDropdowns - End : " + DateTime.Now);
        }

        private void ApplyFonts()
        {
            Log.writeMessage("POY ApplyFonts - Start : " + DateTime.Now);

            this.lineno.Font             = FontManager.GetFont(8F, FontStyle.Bold);
            this.department.Font         = FontManager.GetFont(8F, FontStyle.Bold);
            this.mergeno.Font            = FontManager.GetFont(8F, FontStyle.Bold);
            this.lastboxno.Font          = FontManager.GetFont(8F, FontStyle.Bold);
            this.lastbox.Font            = FontManager.GetFont(8F, FontStyle.Regular);
            this.item.Font               = FontManager.GetFont(8F, FontStyle.Bold);
            this.shade.Font              = FontManager.GetFont(8F, FontStyle.Bold);
            this.shadecode.Font          = FontManager.GetFont(8F, FontStyle.Bold);
            this.boxno.Font              = FontManager.GetFont(8F, FontStyle.Bold);
            this.packingdate.Font        = FontManager.GetFont(8F, FontStyle.Bold);
            this.dateTimePicker1.Font    = FontManager.GetFont(8F, FontStyle.Regular);
            this.quality.Font            = FontManager.GetFont(8F, FontStyle.Bold);
            this.saleorderno.Font        = FontManager.GetFont(8F, FontStyle.Bold);
            this.packsize.Font           = FontManager.GetFont(8F, FontStyle.Bold);
            this.frdenier.Font           = FontManager.GetFont(8F, FontStyle.Regular);
            this.updenier.Font           = FontManager.GetFont(8F, FontStyle.Regular);
            this.windingtype.Font        = FontManager.GetFont(8F, FontStyle.Bold);
            this.comport.Font            = FontManager.GetFont(8F, FontStyle.Bold);
            this.copssize.Font           = FontManager.GetFont(8F, FontStyle.Bold);
            this.copweight.Font          = FontManager.GetFont(8F, FontStyle.Bold);
            this.copstock.Font           = FontManager.GetFont(8F, FontStyle.Bold);
            this.copsitemwt.Font         = FontManager.GetFont(8F, FontStyle.Regular);
            this.boxpalletitemwt.Font    = FontManager.GetFont(8F, FontStyle.Regular);
            this.boxtype.Font            = FontManager.GetFont(8F, FontStyle.Bold);
            this.boxweight.Font          = FontManager.GetFont(8F, FontStyle.Bold);
            this.copsstock.Font          = FontManager.GetFont(8F, FontStyle.Regular);         
            this.boxstock.Font           = FontManager.GetFont(8F, FontStyle.Bold);
            this.boxpalletstock.Font     = FontManager.GetFont(8F, FontStyle.Regular);           
            this.productiontype.Font     = FontManager.GetFont(8F, FontStyle.Bold);
            this.remark.Font             = FontManager.GetFont(8F, FontStyle.Bold);
            this.remarks.Font            = FontManager.GetFont(8F, FontStyle.Regular);
            this.scalemodel.Font         = FontManager.GetFont(8F, FontStyle.Bold);
            this.LineNoList.Font         = FontManager.GetFont(8F, FontStyle.Regular);
            this.DeptList.Font           = FontManager.GetFont(8F, FontStyle.Regular);
            this.MergeNoList.Font        = FontManager.GetFont(8F, FontStyle.Regular);
            this.itemname.Font           = FontManager.GetFont(8F, FontStyle.Regular);
            this.shadename.Font          = FontManager.GetFont(8F, FontStyle.Regular);
            this.shadecd.Font            = FontManager.GetFont(8F, FontStyle.Regular);
            this.QualityList.Font        = FontManager.GetFont(8F, FontStyle.Regular);
            this.PackSizeList.Font       = FontManager.GetFont(8F, FontStyle.Regular);
            this.WindingTypeList.Font    = FontManager.GetFont(8F, FontStyle.Regular);
            this.ComPortList.Font        = FontManager.GetFont(8F, FontStyle.Regular);
            this.WeighingList.Font       = FontManager.GetFont(8F, FontStyle.Regular);
            this.CopsItemList.Font       = FontManager.GetFont(8F, FontStyle.Regular);
            this.BoxItemList.Font        = FontManager.GetFont(8F, FontStyle.Regular);
            this.SaleOrderList.Font      = FontManager.GetFont(8F, FontStyle.Regular);
            this.prcompany.Font          = FontManager.GetFont(8F, FontStyle.Regular);
            this.prowner.Font            = FontManager.GetFont(8F, FontStyle.Regular);
            this.prdate.Font             = FontManager.GetFont(8F, FontStyle.Regular);
            this.pruser.Font             = FontManager.GetFont(8F, FontStyle.Regular);
            this.prhindi.Font            = FontManager.GetFont(8F, FontStyle.Regular);
            this.prwtps.Font             = FontManager.GetFont(8F, FontStyle.Regular);
            this.prqrcode.Font           = FontManager.GetFont(8F, FontStyle.Regular);
            this.label1.Font             = FontManager.GetFont(8F, FontStyle.Bold);
            this.copyno.Font             = FontManager.GetFont(8F, FontStyle.Regular);
            this.wtpercop.Font           = FontManager.GetFont(8F, FontStyle.Regular);
            this.label5.Font             = FontManager.GetFont(8F, FontStyle.Bold);
            this.netwt.Font              = FontManager.GetFont(8F, FontStyle.Regular);
            this.label4.Font             = FontManager.GetFont(8F, FontStyle.Bold);
            this.tarewt.Font             = FontManager.GetFont(8F, FontStyle.Regular);
            this.label3.Font             = FontManager.GetFont(8F, FontStyle.Bold);
            this.grosswtno.Font          = FontManager.GetFont(8F, FontStyle.Regular);
            this.label2.Font             = FontManager.GetFont(8F, FontStyle.Bold);
            this.palletwtno.Font         = FontManager.GetFont(8F, FontStyle.Regular);
            this.palletwt.Font           = FontManager.GetFont(8F, FontStyle.Bold);
            this.spoolwt.Font            = FontManager.GetFont(8F, FontStyle.Regular);
            this.spoolno.Font            = FontManager.GetFont(8F, FontStyle.Regular);
            this.spool.Font              = FontManager.GetFont(8F, FontStyle.Bold);
            this.prodtype.Font           = FontManager.GetFont(8F, FontStyle.Regular);
            this.palletdetails.Font      = FontManager.GetFont(9F, FontStyle.Bold);
            this.label6.Font             = FontManager.GetFont(8F, FontStyle.Bold);
            this.PalletTypeList.Font     = FontManager.GetFont(8F, FontStyle.Regular);
            this.pquantity.Font          = FontManager.GetFont(8F, FontStyle.Bold);
            this.qnty.Font               = FontManager.GetFont(8F, FontStyle.Regular);
            this.flowLayoutPanel1.Font   = FontManager.GetFont(8F, FontStyle.Regular);
            this.cancelbtn.Font          = FontManager.GetFont(8F, FontStyle.Bold);
            this.Printinglbl.Font        = FontManager.GetFont(9F, FontStyle.Bold);
            this.wgroupbox.Font          = FontManager.GetFont(8F, FontStyle.Bold);
            this.netwttxtbox.Font        = FontManager.GetFont(8F, FontStyle.Bold);
            this.netweight.Font          = FontManager.GetFont(8F, FontStyle.Bold);
            this.grosswttxtbox.Font      = FontManager.GetFont(8F, FontStyle.Bold);
            this.grossweight.Font        = FontManager.GetFont(8F, FontStyle.Bold);
            this.copstxtbox.Font         = FontManager.GetFont(8F, FontStyle.Bold);
            this.tarewghttxtbox.Font     = FontManager.GetFont(8F, FontStyle.Bold);
            this.tareweight.Font         = FontManager.GetFont(8F, FontStyle.Bold);
            this.cops.Font               = FontManager.GetFont(8F, FontStyle.Bold);
            this.gradewiseprodn.Font     = FontManager.GetFont(8F, FontStyle.Bold);
            this.totalprodbalqty.Font    = FontManager.GetFont(8F, FontStyle.Regular);
            this.saleordrqty.Font        = FontManager.GetFont(8F, FontStyle.Regular);
            this.Lastboxlbl.Font         = FontManager.GetFont(9F, FontStyle.Bold);
            this.deniervalue.Font        = FontManager.GetFont(8F, FontStyle.Regular);
            this.denier.Font             = FontManager.GetFont(8F, FontStyle.Bold);
            this.machineboxheader.Font   = FontManager.GetFont(8F, FontStyle.Regular);
            this.Machinelbl.Font         = FontManager.GetFont(9F, FontStyle.Bold);
            this.grosswterror.Font       = FontManager.GetFont(7F, FontStyle.Regular);
            this.palletwterror.Font      = FontManager.GetFont(7F, FontStyle.Regular);
            this.spoolwterror.Font       = FontManager.GetFont(7F, FontStyle.Regular);
            this.spoolnoerror.Font       = FontManager.GetFont(7F, FontStyle.Regular);
            this.Weighboxlbl.Font        = FontManager.GetFont(9F, FontStyle.Bold);
            this.Packagingboxlbl.Font    = FontManager.GetFont(9F, FontStyle.Bold);
            this.cancelbtn.Font          = FontManager.GetFont(8F, FontStyle.Bold);
            this.boxnoerror.Font         = FontManager.GetFont(7F, FontStyle.Regular);
            this.windingerror.Font       = FontManager.GetFont(7F, FontStyle.Regular);
            this.packsizeerror.Font      = FontManager.GetFont(7F, FontStyle.Regular);
            this.soerror.Font            = FontManager.GetFont(7F, FontStyle.Regular);
            this.qualityerror.Font       = FontManager.GetFont(7F, FontStyle.Regular);
            this.mergenoerror.Font       = FontManager.GetFont(7F, FontStyle.Regular);
            this.copynoerror.Font        = FontManager.GetFont(7F, FontStyle.Regular);
            this.linenoerror.Font        = FontManager.GetFont(7F, FontStyle.Regular);
            this.grdsoqty.Font           = FontManager.GetFont(8F, FontStyle.Regular);
            this.prodnbalqty.Font        = FontManager.GetFont(8F, FontStyle.Regular);
            this.rowMaterialBox.Font     = FontManager.GetFont(8F, FontStyle.Bold);
            this.fromdenier.Font         = FontManager.GetFont(8F, FontStyle.Bold);
            this.uptodenier.Font         = FontManager.GetFont(8F, FontStyle.Bold);
            this.Font                    = FontManager.GetFont(9F, FontStyle.Bold);
            this.bppartyname.Font        = FontManager.GetFont(8F, FontStyle.Bold);
            this.partyshade.Font         = FontManager.GetFont(8F, FontStyle.Regular);
            this.partyshd.Font           = FontManager.GetFont(8F, FontStyle.Bold);
            this.partyn.Font             = FontManager.GetFont(8F, FontStyle.Regular);
            this.salelotvalue.Font       = FontManager.GetFont(8F, FontStyle.Regular);
            this.salelot.Font            = FontManager.GetFont(8F, FontStyle.Bold);
            this.owner.Font              = FontManager.GetFont(8F, FontStyle.Bold);
            this.OwnerList.Font          = FontManager.GetFont(8F, FontStyle.Regular);
            this.fromwt.Font             = FontManager.GetFont(8F, FontStyle.Bold);
            this.frwt.Font               = FontManager.GetFont(8F, FontStyle.Regular);
            this.uptowt.Font             = FontManager.GetFont(8F, FontStyle.Bold);
            this.upwt.Font               = FontManager.GetFont(8F, FontStyle.Regular);
            this.boxnofrmt.Font          = FontManager.GetFont(8F, FontStyle.Regular);
            this.boxno.Font              = FontManager.GetFont(8F, FontStyle.Bold);
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
            this.cancelbtn.Font = FontManager.GetFont(8F, FontStyle.Bold);
            this.delete.Font = FontManager.GetFont(8F, FontStyle.Bold);
            this.prevbtn.Font = FontManager.GetFont(8F, FontStyle.Bold);
            this.nextbtn.Font = FontManager.GetFont(8F, FontStyle.Bold);
            this.lblPageInfo.Font = FontManager.GetFont(8F, FontStyle.Regular);

            Log.writeMessage("POY ApplyFonts - End : " + DateTime.Now);
        }

        //private async void DeletePOYPackingForm_Shown(object sender, EventArgs e)
        //{
        //    try
        //    {

        //        var machineTask = _masterService.GetMachineList("SpinningLot", "");
        //        var packsizeTask = _masterService.GetPackSizeList("");
        //        var copsitemTask = _masterService.GetItemList(itemCopsCategoryId, "");
        //        var boxitemTask = _masterService.GetItemList(itemBoxCategoryId, "");
        //        var palletitemTask = _masterService.GetItemList(itemPalletCategoryId, "");
        //        var deptTask = _masterService.GetDepartmentList("");
        //        var ownerTask = _masterService.GetOwnerList("");

        //        // 2. Wait for all to complete
        //        await Task.WhenAll(machineTask, packsizeTask, copsitemTask, boxitemTask, palletitemTask, deptTask, ownerTask);

        //        // 3. Get the results
        //        var machineList = machineTask.Result;
        //        var packsizeList = packsizeTask.Result;
        //        var copsitemList = copsitemTask.Result;
        //        var boxitemList = boxitemTask.Result;
        //        var palletitemList = palletitemTask.Result;
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

        //        //palletitem
        //        palletitemList.Insert(0, new ItemResponse { ItemId = 0, Name = "Select Box/Pallet" });
        //        PalletTypeList.DataSource = palletitemList;
        //        PalletTypeList.DisplayMember = "Name";
        //        PalletTypeList.ValueMember = "ItemId";
        //        PalletTypeList.SelectedIndex = 0;
        //        PalletTypeList.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
        //        PalletTypeList.AutoCompleteSource = AutoCompleteSource.ListItems;

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
        //}

        private async Task LoadProductionDetailsAsync(ProductionResponse prodResponse)
        {
            if (prodResponse != null)
            {
                productionResponse = prodResponse;

                productionRequest.PackingType = productionResponse.PackingType;
                productionRequest.ProductionDate = productionResponse.ProductionDate;
                delete.Enabled = productionResponse.IsDisabled ? false : true;
                findbtn.Enabled = false;
                cancelbtn.Enabled = true;

                MergeNoList.DataSource = null;
                MergeNoList.Items.Clear();
                //MergeNoList.Items.Add("Select MergeNo");
                MergeNoList.Items.Add(productionResponse.LotNo);
                MergeNoList.SelectedItem = productionResponse.LotNo;
                productionRequest.LotId = productionResponse.LotId;
                selectLotId = productionResponse.LotId;
                _cmethod.SetReadOnlyBlue(MergeNoList, true, true);

                SaleOrderList.DataSource = null;
                SaleOrderList.Items.Clear();
                //SaleOrderList.Items.Add("Select Sale Order Item");
                var salesOrderNumber = "";
                salesOrderNumber = productionResponse.SalesOrderNumber + "--" + productionResponse.SOItemName + "--" + productionResponse.ShadeName + "--" + productionResponse.SOQuantity;
                SaleOrderList.Items.Add(salesOrderNumber);
                SaleOrderList.SelectedItem = salesOrderNumber;
                productionRequest.SaleOrderItemsId = productionResponse.SaleOrderItemsId;
                selectedSOId = productionResponse.SaleOrderItemsId;
                _cmethod.SetReadOnlyBlue(SaleOrderList, true, true);

                QualityList.DataSource = null;
                QualityList.Items.Clear();
                //QualityList.Items.Add("Select Quality");
                QualityList.Items.Add(productionResponse.QualityName);
                QualityList.SelectedItem = productionResponse.QualityName;
                productionRequest.QualityId = productionResponse.QualityId;
                _cmethod.SetReadOnlyBlue(QualityList, true, true);

                WindingTypeList.DataSource = null;
                WindingTypeList.Items.Clear();
                //WindingTypeList.Items.Add("Select Winding Type");
                WindingTypeList.Items.Add(productionResponse.WindingTypeName);
                WindingTypeList.SelectedItem = productionResponse.WindingTypeName;
                productionRequest.WindingTypeId = productionResponse.WindingTypeId;
                _cmethod.SetReadOnlyBlue(WindingTypeList, true, true);

                PackSizeList.DataSource = null;
                PackSizeList.Items.Clear();
                //PackSizeList.Items.Add("Select Pack Size");
                PackSizeList.Items.Add(productionResponse.PackSizeName);
                PackSizeList.SelectedItem = productionResponse.PackSizeName;
                productionRequest.PackSizeId = productionResponse.PackSizeId;
                _cmethod.SetReadOnlyBlue(PackSizeList, true, true);

                CopsItemList.DataSource = null;
                CopsItemList.Items.Clear();
                //CopsItemList.Items.Add("Select Cops Item");
                CopsItemList.Items.Add(productionResponse.SpoolItemName);
                CopsItemList.SelectedItem = productionResponse.SpoolItemName;
                productionRequest.SpoolItemId = productionResponse.SpoolItemId;
                _cmethod.SetReadOnlyBlue(CopsItemList, true, true);

                BoxItemList.DataSource = null;
                BoxItemList.Items.Clear();
                //BoxItemList.Items.Add("Select Box/Pallet");
                BoxItemList.Items.Add(productionResponse.BoxItemName);
                BoxItemList.SelectedItem = productionResponse.BoxItemName;
                productionRequest.BoxItemId = productionResponse.BoxItemId;
                _cmethod.SetReadOnlyBlue(BoxItemList, true, true);

                OwnerList.DataSource = null;
                OwnerList.Items.Clear();
                //OwnerList.Items.Add("Select Owner");
                if (!string.IsNullOrEmpty(productionResponse.OwnerName))
                {
                    OwnerList.Items.Add(productionResponse.OwnerName);
                    OwnerList.SelectedItem = productionResponse.OwnerName;
                    productionRequest.OwnerId = productionResponse.OwnerId;
                }
                _cmethod.SetReadOnlyBlue(OwnerList, true, true);

                prodtype.Text = productionResponse.ProductionType;
                productionRequest.ProdTypeId = productionResponse.ProdTypeId;
                remarks.Text = productionResponse.Remarks;
                productionRequest.Remarks = productionResponse.Remarks;
                _cmethod.SetReadOnlyBlue(remarks, true, true);
                prcompany.Checked = productionResponse.PrintCompany;
                productionRequest.PrintCompany = productionResponse.PrintCompany;
                _cmethod.SetReadOnlyBlue(prcompany, true, true);
                prowner.Checked = productionResponse.PrintOwner;
                productionRequest.PrintOwner = productionResponse.PrintOwner;
                _cmethod.SetReadOnlyBlue(prowner, true, true);
                prdate.Checked = productionResponse.PrintDate;
                productionRequest.PrintDate = productionResponse.PrintDate;
                _cmethod.SetReadOnlyBlue(prdate, true, true);
                pruser.Checked = productionResponse.PrintUser;
                productionRequest.PrintUser = productionResponse.PrintUser;
                _cmethod.SetReadOnlyBlue(pruser, true, true);
                prhindi.Checked = productionResponse.PrintHindiWords;
                productionRequest.PrintHindiWords = productionResponse.PrintHindiWords;
                _cmethod.SetReadOnlyBlue(prhindi, true, true);
                prwtps.Checked = productionResponse.PrintWTPS;
                productionRequest.PrintWTPS = productionResponse.PrintWTPS;
                _cmethod.SetReadOnlyBlue(prwtps, true, true);
                prqrcode.Checked = productionResponse.PrintQRCode;
                productionRequest.PrintQRCode = productionResponse.PrintQRCode;
                _cmethod.SetReadOnlyBlue(prqrcode, true, true);
                productionRequest.PrintTwist = productionResponse.PrintTwist;
                boxnofrmt.Text = (!string.IsNullOrEmpty(productionResponse.BoxNoFmtd)) ? productionResponse.BoxNoFmtd : "";
                dateTimePicker1.Text = productionResponse.ProductionDate.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                //dateTimePicker1.Value = productionResponse.ProductionDate;
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
                frwt.Text = productionResponse.StartWeight.ToString();
                upwt.Text = productionResponse.EndWeight.ToString();
                copsitemwt.Text = productionResponse.CopsItemWeight.ToString();
                boxpalletitemwt.Text = productionResponse.BoxItemWeight.ToString();
                palletwtno.Text = productionResponse.BoxItemWeight.ToString();
                totalSOQty = productionResponse.SOQuantity;
                grdsoqty.Text = totalSOQty.ToString("F2");
                RefreshGradewiseGrid();
                RefreshWindingGrid();
                productionRequest.ItemId = productionResponse.ItemId;
                productionRequest.ShadeId = productionResponse.ShadeId;
                productionRequest.TwistId = productionResponse.TwistId;
                productionRequest.ContainerTypeId = productionResponse.ContainerTypeId;
                if (productionResponse.PalletDetailsResponse.Count > 0)
                {
                    if (productionResponse?.PalletDetailsResponse != null && productionResponse.PalletDetailsResponse.Any())
                    {
                        this.BeginInvoke((Action)(() =>
                        BindPalletDetails(productionResponse.PalletDetailsResponse)
                        ));
                    }
                }

                LineNoList.DataSource = null;
                LineNoList.Items.Clear();
                //LineNoList.Items.Add("Select Line No.");
                LineNoList.Items.Add(productionResponse.MachineName);
                LineNoList.SelectedItem = productionResponse.MachineName;
                productionRequest.MachineId = productionResponse.MachineId;
                _cmethod.SetReadOnlyBlue(LineNoList, true, true);

                DeptList.DataSource = null;
                DeptList.Items.Clear();
                //DeptList.Items.Add("Select Dept");
                DeptList.Items.Add(productionResponse.DepartmentName);
                DeptList.SelectedItem = productionResponse.DepartmentName;
                productionRequest.DepartmentId = productionResponse.DepartmentId;
                _cmethod.SetReadOnlyBlue(DeptList, true, true);

                spoolno.Text = productionResponse.Spools.ToString();
                productionRequest.Spools = productionResponse.Spools;
                _cmethod.SetReadOnlyBlue(spoolno, true, true);
                spoolwt.Text = productionResponse.SpoolsWt.ToString();
                productionRequest.SpoolsWt = productionResponse.SpoolsWt;
                palletwtno.Text = productionResponse.EmptyBoxPalletWt.ToString();
                productionRequest.EmptyBoxPalletWt = productionResponse.EmptyBoxPalletWt;
                _cmethod.SetReadOnlyBlue(palletwtno, true, true);
                grosswtno.Text = productionResponse.GrossWt.ToString();
                productionRequest.GrossWt = productionResponse.GrossWt;
                _cmethod.SetReadOnlyBlue(grosswtno, true, true);
                tarewt.Text = productionResponse.TareWt.ToString();
                productionRequest.TareWt = productionResponse.TareWt;
                netwt.Text = productionResponse.NetWt.ToString();
                productionRequest.NetWt = productionResponse.NetWt;
                _cmethod.SetReadOnlyBlue(netwt, true, true);
                _cmethod.SetReadOnlyBlue(copyno, true, true);
                AdjustNameByCharCount();
                _cmethod.SetReadOnlyBlue(ComPortList, true, true);
                _cmethod.SetReadOnlyBlue(WeighingList, true, true);

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
            }

            Log.writeMessage("POY LoadProductionDetailsAsync - End : " + DateTime.Now);
        }

        private void BindPalletDetails(List<ProductionPalletDetailsResponse> palletDetailsResponse)
        {
            Log.writeMessage("POY BindPalletDetails - Start : " + DateTime.Now);

            flowLayoutPanel1.Controls.Clear();
            rowCount = 0;
            headerAdded = false;

            // add header first
            AddHeader();

            foreach (var palletDetail in palletDetailsResponse)
            {
                var palletItemList = _masterService.GetItemList(itemPalletCategoryId, "").Result;
                _cmethod.SetReadOnlyBlue(PalletTypeList, true, true);
                _cmethod.SetReadOnlyBlue(qnty, true, true);
                var selectedItem = palletItemList.FirstOrDefault(x => x.ItemId == palletDetail.PalletId);

                if (selectedItem == null)
                {
                    selectedItem = new ItemResponse { ItemId = palletDetail.PalletId, Name = $"Pallet {palletDetail.PalletId}" };
                }

                rowCount++;

                Panel rowPanel = new Panel();
                //rowPanel.Size = new Size(width, 35);
                rowPanel.BorderStyle = BorderStyle.None;

                rowPanel.Paint += (s, pe) =>
                {
                    using (Pen pen = new Pen(Color.FromArgb(230, 230, 230), 1)) // thickness = 1
                    {
                        // dashed border example: pen.DashStyle = DashStyle.Dash;
                        pe.Graphics.DrawLine(
                            pen,
                            0, rowPanel.Height - 1,
                            rowPanel.Width, rowPanel.Height - 1
                        );
                    }
                };

                // SrNo
                System.Windows.Forms.Label lblSrNo = new System.Windows.Forms.Label() { Text = rowCount.ToString(), Width = 30, Location = new System.Drawing.Point(2, 10), Font = FontManager.GetFont(8F, FontStyle.Regular) };

                // Item Name
                System.Windows.Forms.Label lblItem = new System.Windows.Forms.Label() { Name = "lblItemName", Text = selectedItem.Name, AutoSize = false, Width = 160, MaximumSize = new Size(200, 160), Location = new System.Drawing.Point(50, 10), Font = FontManager.GetFont(8F, FontStyle.Regular), Tag = selectedItem.ItemId, TextAlign = ContentAlignment.TopLeft };
                lblItem.Height = TextRenderer.MeasureText(lblItem.Text, lblItem.Font, new Size(lblItem.Width, int.MaxValue), TextFormatFlags.WordBreak).Height;
                // Qty
                System.Windows.Forms.Label lblQty = new System.Windows.Forms.Label() { Text = palletDetail.Quantity.ToString(), Width = 50, Location = new System.Drawing.Point(260, 10), Font = FontManager.GetFont(8F, FontStyle.Regular) };

                rowPanel.Controls.Add(lblSrNo);
                rowPanel.Controls.Add(lblItem);
                rowPanel.Controls.Add(lblQty);
                rowPanel.Tag = new Tuple<ItemResponse, System.Windows.Forms.Label>(selectedItem, lblQty);
                //if itemname is larger then increase the rowPanel height and change its location point
                int rowHeight = Math.Max(lblItem.Height + 10, 35);
                rowPanel.Size = new Size(width, rowHeight);
                int newY = (rowPanel.Height - lblItem.Height) / 2;
                lblItem.Location = new System.Drawing.Point(lblItem.Location.X, newY);
                flowLayoutPanel1.Controls.Add(rowPanel);
            }

            flowLayoutPanel1.AutoScroll = true;
            flowLayoutPanel1.WrapContents = false;
            flowLayoutPanel1.FlowDirection = FlowDirection.TopDown;

            productionRequest.PalletDetailsRequest = new List<ProductionPalletDetailsRequest>();
            foreach (Control ctrl in flowLayoutPanel1.Controls)
            {
                ProductionPalletDetailsRequest pallet = new ProductionPalletDetailsRequest();
                if (ctrl is Panel panel && panel.Tag is Tuple<ItemResponse, System.Windows.Forms.Label> tagData)
                {
                    pallet.PalletId = tagData.Item1.ItemId;
                    pallet.Quantity = Convert.ToInt32(tagData.Item2.Text);
                    productionRequest.PalletDetailsRequest.Add(pallet);
                }

            }

            Log.writeMessage("POY BindPalletDetails - End : " + DateTime.Now);
        }

        //private async void LineNoList_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (!isFormReady) return; // skip during load

        //    if (LineNoList.SelectedIndex <= 0)
        //    {
        //        return;
        //    }

        //    lblLoading.Visible = true;
        //    try
        //    {
        //        if (LineNoList.SelectedValue != null)
        //        {
        //            MachineResponse selectedMachine = (MachineResponse)LineNoList.SelectedItem;
        //            int selectedMachineId = selectedMachine.MachineId;
        //            if (selectedMachineId > 0)
        //            {
        //                productionRequest.MachineId = selectedMachineId;

        //                if (selectedMachine != null)
        //                {
        //                    DeptList.SelectedValue = selectedMachine.DepartmentId;
        //                    var filteredDepts = o_departmentResponses.Where(m => m.DepartmentId == selectedMachine.DepartmentId).ToList();
        //                    filteredDepts.Insert(0, new DepartmentResponse { DepartmentId = 0, DepartmentName = "Select Dept" });
        //                    DeptList.DataSource = filteredDepts;
        //                    DeptList.DisplayMember = "DepartmentName";
        //                    DeptList.ValueMember = "DepartmentId";
        //                    DeptList.SelectedIndex = 1;
        //                    DeptList.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
        //                    DeptList.AutoCompleteSource = AutoCompleteSource.ListItems;
        //                }
        //                var getLots = _productionService.getLotList(selectedMachineId, "").Result;
        //                getLots.Insert(0, new LotsResponse { LotId = 0, LotNoFrmt = "Select MergeNo" });
        //                MergeNoList.DataSource = getLots;
        //                MergeNoList.DisplayMember = "LotNoFrmt";
        //                MergeNoList.ValueMember = "LotId";
        //                MergeNoList.SelectedIndex = 0;
        //                MergeNoList.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
        //                MergeNoList.AutoCompleteSource = AutoCompleteSource.ListItems;

        //                if (_productionId > 0 && productionResponse != null)
        //                {
        //                    MergeNoList.SelectedValue = productionResponse.LotId;
        //                    DeptList.SelectedValue = productionResponse.DepartmentId;
        //                    MergeNoList_SelectedIndexChanged(MergeNoList, EventArgs.Empty);
        //                }
        //            }
        //        }
        //    }
        //    finally
        //    {
        //        lblLoading.Visible = false;
        //    }
        //}

        //private async void MergeNoList_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (!isFormReady) return;

        //    if (MergeNoList.SelectedIndex <= 0)
        //    {
        //        itemname.Text = "";
        //        shadename.Text = "";
        //        shadecd.Text = "";
        //        deniervalue.Text = "";
        //        salelotvalue.Text = "";
        //        partyn.Text = "";
        //        partyshade.Text = "";
        //        lotResponse = new LotsResponse();
        //        lotsDetailsList = new List<LotsDetailsResponse>();
        //        LoadDropdowns();
        //        rowMaterial.Columns.Clear();
        //        windinggrid.Columns.Clear();
        //        qualityqty.Columns.Clear();
        //        totalProdQty = 0;
        //        prodnbalqty.Text = "";
        //        selectedSOId = 0;
        //        totalSOQty = 0;
        //        grdsoqty.Text = "";
        //        balanceQty = 0;
        //        flowLayoutPanel1.Controls.Clear();
        //        rowCount = 0;
        //        AddHeader();
        //        return;
        //    }
        //    lblLoading.Visible = true;
        //    try
        //    {
        //        if (MergeNoList.SelectedValue != null)
        //        {
        //            LotsResponse selectedLot = (LotsResponse)MergeNoList.SelectedItem;
        //            int selectedLotId = selectedLot.LotId;

        //            productionRequest.LotId = selectedLot.LotId;
        //            if (selectedLotId > 0)
        //            {
        //                selectLotId = selectedLotId;

        //                lotResponse = _productionService.getLotById(selectedLotId).Result;
        //                if (lotResponse != null)
        //                {
        //                    itemname.Text = (!string.IsNullOrEmpty(lotResponse.ItemName)) ? lotResponse.ItemName : "";
        //                    shadename.Text = (!string.IsNullOrEmpty(lotResponse.ShadeName)) ? lotResponse.ShadeName : "";
        //                    AdjustNameByCharCount();
        //                    shadecd.Text = (!string.IsNullOrEmpty(lotResponse.ShadeCode)) ? lotResponse.ShadeCode : "";
        //                    deniervalue.Text = lotResponse.Denier.ToString();
        //                    salelotvalue.Text = (!string.IsNullOrEmpty(lotResponse.SaleLot)) ? lotResponse.SaleLot.ToString() : null;
        //                    productionRequest.SaleLot = (!string.IsNullOrEmpty(lotResponse.SaleLot)) ? lotResponse.SaleLot : null;
        //                    productionRequest.MachineId = lotResponse.MachineId;
        //                    productionRequest.ItemId = lotResponse.ItemId;
        //                    productionRequest.ShadeId = lotResponse.ShadeId;
        //                    LineNoList.SelectedValue = lotResponse.MachineId;

        //                    if (lotResponse.ItemId > 0)
        //                    {
        //                        var itemResponse = _masterService.GetItemById(lotResponse.ItemId).Result;
        //                        if (itemResponse != null)
        //                        {
        //                            var qualityList = _masterService.GetQualityListByItemTypeId(itemResponse.ItemTypeId).Result;
        //                            qualityList.Insert(0, new QualityResponse { QualityId = 0, Name = "Select Quality" });
        //                            QualityList.DataSource = qualityList;
        //                            QualityList.DisplayMember = "Name";
        //                            QualityList.ValueMember = "QualityId";
        //                            QualityList.SelectedIndex = 0;
        //                            QualityList.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
        //                            QualityList.AutoCompleteSource = AutoCompleteSource.ListItems;

        //                            if (QualityList.Items.Count > 1)
        //                            {
        //                                QualityList.SelectedIndex = 1;
        //                            }
        //                            else if (QualityList.Items.Count > 0) // fallback to first item if only one exists
        //                            {
        //                                QualityList.SelectedIndex = 0;
        //                            }
        //                            else
        //                            {
        //                                QualityList.SelectedIndex = -1; // no selection possible
        //                            }
        //                        }
        //                    }
        //                }

        //                var getWindingType = new List<WindingTypeResponse>();
        //                getWindingType = _productionService.getWinderTypeList(selectedLotId, "").Result;
        //                getWindingType.Insert(0, new WindingTypeResponse { WindingTypeId = 0, WindingTypeName = "Select Winding Type" });
        //                if (getWindingType.Count <= 1)
        //                {
        //                    getWindingType = _masterService.GetWindingTypeList("").Result;
        //                    getWindingType.Insert(0, new WindingTypeResponse { WindingTypeId = 0, WindingTypeName = "Select Winding Type" });

        //                }
        //                WindingTypeList.DataSource = getWindingType;
        //                WindingTypeList.DisplayMember = "WindingTypeName";
        //                WindingTypeList.ValueMember = "WindingTypeId";
        //                WindingTypeList.SelectedIndex = 0;
        //                WindingTypeList.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
        //                WindingTypeList.AutoCompleteSource = AutoCompleteSource.ListItems;

        //                var getSaleOrder = _productionService.getSaleOrderList(selectedLotId, "").Result;
        //                getSaleOrder.Insert(0, new LotSaleOrderDetailsResponse { SaleOrderItemsId = 0, ItemName = "Select Sale Order Item" });
        //                SaleOrderList.DataSource = getSaleOrder;
        //                SaleOrderList.DisplayMember = "ItemName";
        //                SaleOrderList.ValueMember = "SaleOrderItemsId";
        //                SaleOrderList.SelectedIndex = 0;
        //                SaleOrderList.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
        //                SaleOrderList.AutoCompleteSource = AutoCompleteSource.ListItems;
        //                if (SaleOrderList.Items.Count == 2)
        //                {
        //                    SaleOrderList.SelectedIndex = 1;   // Select the single record
        //                    SaleOrderList.Enabled = false;     // Disable user selection
        //                    SaleOrderList_SelectedIndexChanged(SaleOrderList, EventArgs.Empty);
        //                }
        //                else
        //                {
        //                    SaleOrderList.Enabled = true;      // Allow user selection
        //                    SaleOrderList.SelectedIndex = 0;  // Optional: no default selection
        //                }

        //                lotsDetailsList = new List<LotsDetailsResponse>();
        //                productionRequest.ProductionDate = dateTimePicker1.Value;
        //                lotsDetailsList = _productionService.getLotsDetailsByLotsIdAndProductionDate(selectedLotId, productionRequest.ProductionDate).Result;
        //                if (lotsDetailsList.Count > 0)
        //                {
        //                    rowMaterial.Columns.Clear();
        //                    rowMaterial.Columns.Add(new DataGridViewTextBoxColumn { Name = "PrevLotType", DataPropertyName = "PrevLotType", HeaderText = "Prev.LotType" });
        //                    rowMaterial.Columns.Add(new DataGridViewTextBoxColumn { Name = "PrevLotNo", DataPropertyName = "PrevLotNo", HeaderText = "Prev.LotNo" });
        //                    rowMaterial.Columns.Add(new DataGridViewTextBoxColumn { Name = "PrevLotItemName", DataPropertyName = "PrevLotItemName", HeaderText = "Prev.LotItem" });
        //                    rowMaterial.Columns.Add(new DataGridViewTextBoxColumn { Name = "PrevLotShadeName", DataPropertyName = "PrevLotShadeName", HeaderText = "Prev.LotShade" });
        //                    rowMaterial.Columns.Add(new DataGridViewTextBoxColumn { Name = "PrevLotQuality", DataPropertyName = "PrevLotQuality", HeaderText = "Quality" });
        //                    rowMaterial.Columns.Add(new DataGridViewTextBoxColumn { Name = "ProductionPerc", DataPropertyName = "ProductionPerc", HeaderText = "Production %" });
        //                    rowMaterial.Columns.Add(new DataGridViewTextBoxColumn { Name = "EffectiveFrom", DataPropertyName = "EffectiveFrom", HeaderText = "EffectiveFrom", Width = 150, DefaultCellStyle = { Format = "dd-MM-yyyy hh:mm tt" } });
        //                    rowMaterial.Columns.Add(new DataGridViewTextBoxColumn { Name = "EffectiveUpto", DataPropertyName = "EffectiveUpto", HeaderText = "EffectiveUpto", Width = 150, DefaultCellStyle = { Format = "dd-MM-yyyy hh:mm tt" } });
        //                    rowMaterial.DataSource = lotsDetailsList;
        //                }

        //                if (_productionId > 0 && productionResponse != null)
        //                {
        //                    SaleOrderList.SelectedValue = productionResponse.SaleOrderItemsId;
        //                    SaleOrderList_SelectedIndexChanged(SaleOrderList, EventArgs.Empty);
        //                }
        //            }
        //        }
        //    }
        //    finally
        //    {
        //        lblLoading.Visible = false;
        //    }
        //}

        //private async void PackSizeList_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (!isFormReady) return;

        //    if (PackSizeList.SelectedIndex <= 0)
        //    {
        //        frdenier.Text = "0";
        //        updenier.Text = "0";
        //        return;
        //    }

        //    lblLoading.Visible = true;
        //    try
        //    {
        //        if (PackSizeList.SelectedValue != null)
        //        {
        //            PackSizeResponse selectedPacksize = (PackSizeResponse)PackSizeList.SelectedItem;
        //            int selectedPacksizeId = selectedPacksize.PackSizeId;

        //            productionRequest.PackSizeId = selectedPacksizeId;
        //            if (selectedPacksizeId > 0)
        //            {
        //                var packsize = _masterService.GetPackSizeById(selectedPacksizeId).Result;
        //                frdenier.Text = packsize.FromDenier.ToString();
        //                updenier.Text = packsize.UpToDenier.ToString();
        //                frwt.Text = packsize.StartWeight.ToString();
        //                upwt.Text = packsize.EndWeight.ToString();
        //            }

        //        }
        //    }
        //    finally
        //    {
        //        lblLoading.Visible = false;
        //    }
        //}

        //private void QualityList_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (!isFormReady) return;

        //    if (QualityList.SelectedValue != null)
        //    {
        //        QualityResponse selectedQuality = (QualityResponse)QualityList.SelectedItem;
        //        int selectedQualityId = selectedQuality.QualityId;

        //        productionRequest.QualityId = selectedQualityId;
        //    }
        //}

        //private void WindingTypeList_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (!isFormReady) return;

        //    lblLoading.Visible = true;
        //    try
        //    {
        //        if (WindingTypeList.SelectedValue != null)
        //        {
        //            WindingTypeResponse selectedWindingType = (WindingTypeResponse)WindingTypeList.SelectedItem;
        //            int selectedWindingTypeId = selectedWindingType.WindingTypeId;

        //            if (selectedWindingTypeId > 0)
        //            {
        //                productionRequest.WindingTypeId = selectedWindingTypeId;
        //                RefreshWindingGrid();
        //            }

        //            if (_productionId > 0 && productionResponse != null)
        //            {
        //                WindingTypeList.SelectedValue = productionResponse.WindingTypeId;
        //            }
        //        }
        //    }
        //    finally
        //    {
        //        lblLoading.Visible = false;
        //    }
        //}

        //private async void SaleOrderList_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (!isFormReady) return;

        //    lblLoading.Visible = true;
        //    try
        //    {
        //        if (SaleOrderList.SelectedValue != null)
        //        {
        //            totalSOQty = 0;
        //            grdsoqty.Text = "0";
        //            LotSaleOrderDetailsResponse selectedSaleOrder = (LotSaleOrderDetailsResponse)SaleOrderList.SelectedItem;
        //            int selectedSaleOrderId = selectedSaleOrder.SaleOrderItemsId;
        //            string soNumber = selectedSaleOrder.SaleOrderNumber;
        //            productionRequest.SaleOrderItemsId = selectedSaleOrderId;
        //            if (selectedSaleOrderId > 0)
        //            {
        //                selectedSOId = selectedSaleOrderId;
        //                selectedSONumber = selectedSaleOrder.SaleOrderNumber;
        //                totalSOQty = selectedSaleOrder.Quantity;
        //                grdsoqty.Text = totalSOQty.ToString("F2");
        //                var saleOrderItemResponse = _saleService.getSaleOrderItemById(selectedSaleOrderId).Result;
        //                if (saleOrderItemResponse != null)
        //                {
        //                    productionRequest.ContainerTypeId = saleOrderItemResponse.ContainerTypeId;
        //                    partyn.Text = saleOrderItemResponse.ItemDescription;
        //                    partyshade.Text = saleOrderItemResponse.ShadeNameDescription + "-" + saleOrderItemResponse.ShadeCodeDescription;
        //                }

        //                RefreshGradewiseGrid();
        //                if (_productionId > 0 && productionResponse != null)
        //                {
        //                    WindingTypeList.SelectedValue = productionResponse.WindingTypeId;
        //                    WindingTypeList_SelectedIndexChanged(WindingTypeList, EventArgs.Empty);
        //                }
        //            }
        //        }
        //    }
        //    finally
        //    {
        //        lblLoading.Visible = false;
        //    }
        //}
        private async void RefreshWindingGrid()
        {
            Log.writeMessage("POY RefreshWindingGrid - Start : " + DateTime.Now);

            if (productionRequest.WindingTypeId != 0)
            {
                //int selectedWindingTypeId = Convert.ToInt32(WindingTypeList.SelectedValue.ToString());
                if (productionRequest.WindingTypeId > 0)
                {
                    var getProductionByWindingType = _packingService.getAllByLotIdandSaleOrderItemIdandPackingType(selectLotId, selectedSOId).Result;
                    List<WindingTypeGridResponse> gridList = new List<WindingTypeGridResponse>();
                    foreach (var winding in getProductionByWindingType)
                    {
                        var existing = gridList.FirstOrDefault(x => x.WindingTypeId == winding.WindingTypeId && x.SaleOrderItemsId == winding.SaleOrderItemsId);

                        if (existing == null)
                        {
                            WindingTypeGridResponse grid = new WindingTypeGridResponse();
                            grid.WindingTypeId = winding.WindingTypeId;
                            grid.SaleOrderItemsId = winding.SaleOrderItemsId;
                            grid.WindingTypeName = winding.WindingTypeName;
                            grid.SaleOrderQty = totalSOQty;
                            grid.GrossWt = winding.GrossWt;

                            gridList.Add(grid);
                        }
                        else
                        {
                            existing.GrossWt += winding.GrossWt;
                        }

                    }
                    windinggrid.Columns.Clear();
                    windinggrid.Columns.Add(new DataGridViewTextBoxColumn { Name = "WindingTypeName", DataPropertyName = "WindingTypeName", HeaderText = "Winding Type" });
                    windinggrid.Columns.Add(new DataGridViewTextBoxColumn { Name = "TotalSOQty", DataPropertyName = "SaleOrderQty", HeaderText = "SaleOrder Qty" });
                    windinggrid.Columns.Add(new DataGridViewTextBoxColumn { Name = "ProductionQty", DataPropertyName = "GrossWt", HeaderText = "Production Qty" });
                    windinggrid.Columns.Add(new DataGridViewTextBoxColumn { Name = "BalanceQty", DataPropertyName = "BalanceQty", HeaderText = "Balance Qty" });
                    windinggrid.DataSource = gridList;
                }
            }

            Log.writeMessage("POY RefreshWindingGrid - End : " + DateTime.Now);
        }

        private async void RefreshGradewiseGrid()
        {
            Log.writeMessage("POY RefreshGradewiseGrid - Start : " + DateTime.Now);

            if (productionRequest.QualityId != 0)
            {
                prodnbalqty.Text = "";
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
                qualityqty.Columns.Clear();
                qualityqty.Columns.Add(new DataGridViewTextBoxColumn { Name = "Quality", DataPropertyName = "QualityName", HeaderText = "Quality" });
                qualityqty.Columns.Add(new DataGridViewTextBoxColumn { Name = "ProductionQty", DataPropertyName = "GrossWt", HeaderText = "Production Qty" });
                qualityqty.DataSource = gridList;

                totalProdQty = 0;
                foreach (var proditem in gridList)
                {
                    totalProdQty += proditem.GrossWt;
                }
                balanceQty = (totalSOQty - totalProdQty);
                if (balanceQty <= 0)
                {
                    prodnbalqty.Text = "0";
                }
                else
                {
                    prodnbalqty.Text = balanceQty.ToString("F2");
                }
            }

            Log.writeMessage("POY RefreshGradewiseGrid - End : " + DateTime.Now);
        }

        private async void RefreshLastBoxDetails()
        {
            Log.writeMessage("POY RefreshLastBoxDetails - Start : " + DateTime.Now);

            var getLastBox = _packingService.getLastBoxDetails("poypacking", 0).Result;

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

            Log.writeMessage("POY RefreshLastBoxDetails - End : " + DateTime.Now);
        }

        //private void ComPortList_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (!isFormReady) return;

        //    if (ComPortList.SelectedValue != null)
        //    {
        //        var ComPort = ComPortList.SelectedValue.ToString();
        //        comPort = ComPortList.SelectedValue.ToString();
        //    }
        //}

        //private void WeighingList_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (!isFormReady) return;

        //    if (WeighingList.SelectedValue != null)
        //    {
        //        WeighingItem selectedWeighingScale = (WeighingItem)WeighingList.SelectedItem;
        //        int selectedScaleId = selectedWeighingScale.Id;
        //    }
        //}

        //private async void CopsItemList_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (!isFormReady) return;

        //    if (CopsItemList.SelectedIndex <= 0)
        //    {
        //        copsitemwt.Text = "0";
        //        spoolwt.Text = "0";
        //        return;
        //    }

        //    lblLoading.Visible = true;
        //    try
        //    {
        //        if (CopsItemList.SelectedValue != null)
        //        {
        //            ItemResponse selectedCopsItem = (ItemResponse)CopsItemList.SelectedItem;
        //            int selectedItemId = selectedCopsItem.ItemId;

        //            if (selectedItemId > 0)
        //            {
        //                productionRequest.SpoolItemId = selectedItemId;

        //                var itemResponse = _masterService.GetItemById(selectedItemId).Result;
        //                if (itemResponse != null)
        //                {
        //                    copsitemwt.Text = itemResponse.Weight.ToString();
        //                    SpoolNo_TextChanged(sender, e);
        //                }
        //            }
        //        }
        //    }
        //    finally
        //    {
        //        lblLoading.Visible = false;
        //    }
        //}

        //private async void BoxItemList_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (!isFormReady) return;

        //    if (BoxItemList.SelectedIndex <= 0)
        //    {
        //        boxpalletitemwt.Text = "0";
        //        palletwtno.Text = "0";
        //        return;
        //    }

        //    lblLoading.Visible = true;
        //    try
        //    {
        //        if (BoxItemList.SelectedValue != null)
        //        {
        //            ItemResponse selectedBoxItem = (ItemResponse)BoxItemList.SelectedItem;
        //            int selectedBoxItemId = selectedBoxItem.ItemId;

        //            if (selectedBoxItemId > 0)
        //            {
        //                productionRequest.BoxItemId = selectedBoxItemId;
        //                var itemResponse = _masterService.GetItemById(selectedBoxItemId).Result;
        //                if (itemResponse != null)
        //                {
        //                    boxpalletitemwt.Text = itemResponse.Weight.ToString();
        //                    palletwtno.Text = itemResponse.Weight.ToString();
        //                }
        //            }
        //        }
        //    }
        //    finally
        //    {
        //        lblLoading.Visible = false;
        //    }
        //}

        //private async void DeptList_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (!isFormReady) return;

        //    if (DeptList.SelectedIndex <= 0)
        //    {
        //        return;
        //    }
        //    lblLoading.Visible = true;
        //    try
        //    {
        //        if (DeptList.SelectedValue != null)
        //        {
        //            DepartmentResponse selectedDepartment = (DepartmentResponse)DeptList.SelectedItem;
        //            int selectedDepartmentId = selectedDepartment.DepartmentId;

        //            if (selectedDepartment != null && productionRequest.MachineId == 0)
        //            {
        //                var machineList = _masterService.GetMachineByDepartmentIdAndLotType(selectedDepartmentId, "SpinningLot").Result;

        //                machineList.Insert(0, new MachineResponse { MachineId = 0, MachineName = "Select Line No." });
        //                LineNoList.DataSource = machineList;
        //                LineNoList.SelectedValue = productionResponse.MachineId;
        //            }

        //            productionRequest.DepartmentId = selectedDepartmentId;
        //        }
        //    }
        //    finally
        //    {
        //        lblLoading.Visible = false;
        //    }
        //}

        //private async void OwnerList_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (!isFormReady) return;

        //    if (OwnerList.SelectedIndex <= 0)
        //    {
        //        return;
        //    }
        //    if (OwnerList.SelectedIndex > 0)
        //    {
        //    }
        //    lblLoading.Visible = true;
        //    try
        //    {
        //        if (OwnerList.SelectedValue != null)
        //        {

        //            BusinessPartnerResponse selectedOwner = (BusinessPartnerResponse)OwnerList.SelectedItem;
        //            int selectedOwnerId = selectedOwner.BusinessPartnerId;

        //            productionRequest.OwnerId = selectedOwnerId;
        //        }
        //    }
        //    finally
        //    {
        //        lblLoading.Visible = false;
        //    }
        //}

        private async Task<List<string>> getComPortList()
        {
            Log.writeMessage("POY getComPortList - Start : " + DateTime.Now);

            var getComPortType = new List<string>
            {
                "Select Com Port",
            };

            Log.writeMessage("POY getComPortList - End : " + DateTime.Now);

            return getComPortType;
        }

        private async Task<List<WeighingItem>> getWeighingList()
        {
            Log.writeMessage("POY getWeighingList - Start : " + DateTime.Now);

            var getWeighingScale = new List<WeighingItem>
            {
                new WeighingItem { Id = -1, Name = "Select Weigh Scale" },
            };

            Log.writeMessage("POY getWeighingList - End : " + DateTime.Now);

            return getWeighingScale;
        }

        private int rowCount = 0; // Keeps track of SrNo
        private bool headerAdded = false; // To ensure header is added only once
        private int currentY = 35; // Start below header height
        //private void addqty_Click(object sender, EventArgs e)
        //{
        //    var selectedItem = (ItemResponse)PalletTypeList.SelectedItem;
        //    if (selectedItem != null)
        //    {
        //        if (selectedItem.ItemId == 0)
        //        {
        //            MessageBox.Show("Please select an item.",
        //            "Error",
        //            MessageBoxButtons.OK,
        //            MessageBoxIcon.Error);
        //            return;
        //        }
        //    }
        //    if (string.IsNullOrEmpty(qnty.Text))
        //    {
        //        MessageBox.Show("Please enter quantity.",
        //            "Error",
        //            MessageBoxButtons.OK,
        //            MessageBoxIcon.Error); return;
        //    }
        //    int qty = Convert.ToInt32(qnty.Text);

        //    if (selectedItem.ItemId > 0)
        //    {
        //        // Check duplicate value
        //        bool alreadyExists = flowLayoutPanel1.Controls
        //            .OfType<Panel>().Skip(1) // Skip header
        //            .Any(ctrl => {
        //                if (ctrl.Tag is Tuple<ItemResponse, int> tagData)
        //                {
        //                    return tagData.Item1.ItemId == selectedItem.ItemId && tagData.Item2 == qty;
        //                }
        //                return false;
        //            });

        //        var existingPanel = flowLayoutPanel1.Controls
        //            .OfType<Panel>()
        //            .Skip(1) // Skip header
        //            .FirstOrDefault(ctrl =>
        //                ctrl.Tag is Tuple<ItemResponse, System.Windows.Forms.Label> tag &&
        //                tag.Item1.ItemId == selectedItem.ItemId);

        //        if (existingPanel != null)
        //        {
        //            var tag = (Tuple<ItemResponse, System.Windows.Forms.Label>)existingPanel.Tag;
        //            tag.Item2.Text = qty.ToString();
        //            foreach (var control in existingPanel.Controls.OfType<System.Windows.Forms.Button>())
        //            {
        //                if (control.Text == "Remove")
        //                {
        //                    control.Enabled = true;
        //                }
        //            }

        //            qnty.Text = "";
        //            PalletTypeList.SelectedIndex = 0;
        //            return;
        //        }

        //        if (!alreadyExists)
        //        {
        //            rowCount++;

        //            Panel rowPanel = new Panel();
        //            rowPanel.Size = new Size(width, 35);
        //            rowPanel.BorderStyle = BorderStyle.None;

        //            rowPanel.Paint += (s, pe) =>
        //            {
        //                using (Pen pen = new Pen(Color.FromArgb(230, 230, 230), 1)) // thickness = 1
        //                {
        //                    // dashed border example: pen.DashStyle = DashStyle.Dash;
        //                    pe.Graphics.DrawLine(
        //                        pen,
        //                        0, rowPanel.Height - 1,
        //                        rowPanel.Width, rowPanel.Height - 1
        //                    );
        //                }
        //            };

        //            // SrNo
        //            System.Windows.Forms.Label lblSrNo = new System.Windows.Forms.Label() { Text = rowCount.ToString(), Width = 30, Location = new System.Drawing.Point(2, 10), Font = FontManager.GetFont(8F, FontStyle.Regular) };

        //            // Item Name
        //            System.Windows.Forms.Label lblItem = new System.Windows.Forms.Label() { Text = selectedItem.Name, Width = 140, Location = new System.Drawing.Point(50, 10), Font = FontManager.GetFont(8F, FontStyle.Regular), Tag = selectedItem.ItemId };

        //            // Qty
        //            System.Windows.Forms.Label lblQty = new System.Windows.Forms.Label() { Text = qty.ToString(), Width = 50, Location = new System.Drawing.Point(260, 10), Font = FontManager.GetFont(8F, FontStyle.Regular) };

        //            // Edit Button
        //            System.Windows.Forms.Button btnEdit = new System.Windows.Forms.Button() { Text = "Edit", Size = new Size(35, 23), Location = new System.Drawing.Point(320, 5), Font = FontManager.GetFont(7F, FontStyle.Regular), BackColor = Color.FromArgb(230, 240, 255), ForeColor = Color.FromArgb(51, 133, 255), Tag = new Tuple<ItemResponse, System.Windows.Forms.Label>(selectedItem, lblQty), FlatStyle = FlatStyle.Flat };
        //            btnEdit.FlatAppearance.BorderColor = Color.FromArgb(51, 133, 255);
        //            btnEdit.FlatAppearance.BorderSize = 1;
        //            btnEdit.FlatAppearance.MouseOverBackColor = Color.FromArgb(210, 230, 255);
        //            btnEdit.FlatAppearance.MouseDownBackColor = Color.FromArgb(180, 210, 255);
        //            btnEdit.FlatAppearance.BorderSize = 0;
        //            btnEdit.TabIndex = 4;
        //            btnEdit.TabStop = true;
        //            btnEdit.Cursor = Cursors.Hand;
        //            btnEdit.Paint += (s, f) =>
        //            {
        //                var rect = new Rectangle(0, 0, btnEdit.Width - 1, btnEdit.Height - 1);

        //                using (GraphicsPath path = _cmethod.GetRoundedRect(rect, 4)) // radius = 4
        //                using (Pen borderPen = new Pen(btnEdit.FlatAppearance.BorderColor, btnEdit.FlatAppearance.BorderSize))
        //                using (SolidBrush brush = new SolidBrush(btnEdit.BackColor))
        //                {
        //                    f.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

        //                    f.Graphics.FillPath(brush, path);

        //                    f.Graphics.DrawPath(borderPen, path);

        //                    if (btnEdit.Focused)
        //                    {
        //                        ControlPaint.DrawFocusRectangle(f.Graphics, rect);
        //                    }

        //                    TextRenderer.DrawText(
        //                        f.Graphics,
        //                        btnEdit.Text,
        //                        btnEdit.Font,
        //                        rect,
        //                        btnEdit.ForeColor,
        //                        TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter
        //                    );
        //                }
        //            };
        //            btnEdit.Click += editPallet_Click;

        //            // Delete Button
        //            System.Windows.Forms.Button btnDelete = new System.Windows.Forms.Button() { Text = "Remove", Size = new Size(50, 23), Location = new System.Drawing.Point(360, 5), Font = FontManager.GetFont(7F, FontStyle.Regular), BackColor = Color.FromArgb(255, 230, 230), ForeColor = Color.FromArgb(255, 51, 51), Tag = rowPanel, FlatStyle = FlatStyle.Flat };
        //            btnDelete.FlatAppearance.BorderColor = Color.FromArgb(255, 51, 51);
        //            btnDelete.FlatAppearance.BorderSize = 1;
        //            btnDelete.FlatAppearance.MouseOverBackColor = Color.FromArgb(255, 204, 204);
        //            btnDelete.FlatAppearance.MouseDownBackColor = Color.FromArgb(255, 230, 230);
        //            btnDelete.FlatAppearance.BorderSize = 0;
        //            btnDelete.TabIndex = 5;
        //            btnDelete.TabStop = true;
        //            btnDelete.Cursor = Cursors.Hand;
        //            btnDelete.Paint += (s, f) =>
        //            {
        //                var button = (System.Windows.Forms.Button)s;
        //                var rect = new Rectangle(0, 0, button.Width - 1, button.Height - 1);

        //                // button color change for enabled/disabled
        //                Color backColor = button.Enabled ? button.BackColor : Color.LightGray;
        //                Color borderColor = button.Enabled ? button.FlatAppearance.BorderColor : Color.Gray;
        //                Color foreColor = button.Enabled ? button.ForeColor : Color.DarkGray;

        //                using (GraphicsPath path = _cmethod.GetRoundedRect(rect, 4))
        //                using (Pen borderPen = new Pen(borderColor, button.FlatAppearance.BorderSize))
        //                using (SolidBrush brush = new SolidBrush(backColor))
        //                {
        //                    f.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
        //                    f.Graphics.FillPath(brush, path);
        //                    f.Graphics.DrawPath(borderPen, path);

        //                    if (btnDelete.Focused)
        //                    {
        //                        ControlPaint.DrawFocusRectangle(f.Graphics, rect);
        //                    }

        //                    TextRenderer.DrawText(
        //                        f.Graphics,
        //                        button.Text,
        //                        button.Font,
        //                        rect,
        //                        foreColor,
        //                        TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter
        //                    );
        //                }
        //            };
        //            // Remove Row
        //            btnDelete.Click += (s, args) =>
        //            {
        //                flowLayoutPanel1.Controls.Remove(rowPanel);
        //                ReorderSrNo();
        //            };

        //            rowPanel.Controls.Add(lblSrNo);
        //            rowPanel.Controls.Add(lblItem);
        //            rowPanel.Controls.Add(lblQty);
        //            rowPanel.Controls.Add(btnEdit);
        //            rowPanel.Controls.Add(btnDelete);
        //            rowPanel.Tag = new Tuple<ItemResponse, System.Windows.Forms.Label>(selectedItem, lblQty);

        //            flowLayoutPanel1.Controls.Add(rowPanel);
        //            flowLayoutPanel1.AutoScroll = true;
        //            flowLayoutPanel1.WrapContents = false;
        //            flowLayoutPanel1.FlowDirection = FlowDirection.TopDown;

        //            qnty.Text = "";
        //            PalletTypeList.SelectedIndex = 0;
        //            PalletTypeList.Focus();
        //        }
        //        else
        //        {
        //            MessageBox.Show("Item already added.",
        //            "Error",
        //            MessageBoxButtons.OK,
        //            MessageBoxIcon.Error);
        //        }
        //    }
        //    else
        //    {
        //        MessageBox.Show("Please select an item.",
        //            "Error",
        //            MessageBoxButtons.OK,
        //            MessageBoxIcon.Error);
        //    }
        //}

        //private void ReorderSrNo()
        //{
        //    int srNo = 1;
        //    int y = 35;

        //    foreach (Panel row in flowLayoutPanel1.Controls.OfType<Panel>().Skip(1))
        //    {
        //        row.Location = new System.Drawing.Point(0, y);
        //        var lbl = row.Controls.OfType<System.Windows.Forms.Label>().FirstOrDefault();
        //        if (lbl != null)
        //            lbl.Text = srNo.ToString();

        //        y += row.Height;
        //        srNo++;
        //    }

        //    currentY = y; // Reset currentY for next added row
        //    rowCount = srNo - 1;
        //    PalletTypeList.Focus();
        //}

        private void AddHeader()
        {
            Log.writeMessage("POY AddHeader - Start : " + DateTime.Now);

            Panel headerPanel = new Panel();
            headerPanel.Size = new Size(flowLayoutPanel1.ClientSize.Width, 35);
            headerPanel.BackColor = Color.White;
            headerPanel.Paint += (s, pe) =>
            {
                using (Pen pen = new Pen(Color.FromArgb(230, 230, 230), 1))
                {
                    pe.Graphics.DrawLine(
                        pen,
                        0, headerPanel.Height - 1,
                        headerPanel.Width, headerPanel.Height - 1
                    );
                }
            };

            headerPanel.Controls.Add(new System.Windows.Forms.Label() { Text = "SrNo", Width = 30, Location = new System.Drawing.Point(2, 10), Font = FontManager.GetFont(7F, FontStyle.Bold) });
            headerPanel.Controls.Add(new System.Windows.Forms.Label() { Text = "Item Name", Width = 160, Location = new System.Drawing.Point(50, 10), Font = FontManager.GetFont(7F, FontStyle.Bold) });
            headerPanel.Controls.Add(new System.Windows.Forms.Label() { Text = "Qty", Width = 70, Location = new System.Drawing.Point(260, 10), Font = FontManager.GetFont(7F, FontStyle.Bold) });

            flowLayoutPanel1.Controls.Add(headerPanel);
            headerAdded = true;

            Log.writeMessage("POY AddHeader - End : " + DateTime.Now);
        }

        //private void editPallet_Click(object sender, EventArgs e)
        //{
        //    var btn = sender as System.Windows.Forms.Button;
        //    var data = btn.Tag as Tuple<ItemResponse, System.Windows.Forms.Label>;

        //    if (data != null)
        //    {
        //        ItemResponse item = data.Item1;
        //        int quantity = Convert.ToInt32(data.Item2.Text);

        //        foreach (ItemResponse entry in PalletTypeList.Items)
        //        {
        //            if (entry.ItemId == item.ItemId)
        //            {
        //                PalletTypeList.SelectedItem = entry;
        //                break;
        //            }
        //        }

        //        qnty.Text = quantity.ToString();

        //        //disable remove button when edit row
        //        var rowPanel = btn.Parent as Panel;
        //        if (rowPanel != null)
        //        {
        //            foreach (var control in rowPanel.Controls.OfType<System.Windows.Forms.Button>())
        //            {
        //                if (control.Text == "Remove")
        //                {
        //                    control.Enabled = false;
        //                }
        //            }
        //        }
        //        PalletTypeList.Focus();
        //    }
        //}

        private void SpoolWeight_TextChanged(object sender, EventArgs e)
        {
            Log.writeMessage("POY SpoolWeight_TextChanged - Start : " + DateTime.Now);

            if (string.IsNullOrWhiteSpace(spoolwt.Text))
            {
                CalculateTareWeight();
            }
            else
            {
                CalculateTareWeight();
            }

            Log.writeMessage("POY SpoolWeight_TextChanged - End : " + DateTime.Now);
        }

        private void PalletWeight_TextChanged(object sender, EventArgs e)
        {
            Log.writeMessage("POY PalletWeight_TextChanged - Start : " + DateTime.Now);

            if (string.IsNullOrWhiteSpace(palletwtno.Text))
            {
                CalculateTareWeight();
            }
            else
            {
                CalculateTareWeight();
            }

            Log.writeMessage("POY PalletWeight_TextChanged - End : " + DateTime.Now);
        }

        private void CalculateTareWeight()
        {
            Log.writeMessage("POY CalculateTareWeight - Start : " + DateTime.Now);

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

            Log.writeMessage("POY CalculateTareWeight - End : " + DateTime.Now);
        }

        private void GrossWeight_TextChanged(object sender, EventArgs e)
        {
            Log.writeMessage("POY GrossWeight_Validating - Start : " + DateTime.Now);

            if (!isFormReady) return;

            if (string.IsNullOrWhiteSpace(grosswtno.Text))
            {
                MessageBox.Show("Please enter gross weight", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

            Log.writeMessage("POY GrossWeight_Validating - End : " + DateTime.Now);
        }

        private void CalculateNetWeight()
        {
            Log.writeMessage("POY CalculateNetWeight - Start : " + DateTime.Now);

            decimal num1 = 0, num2 = 0;

            decimal.TryParse(grosswtno.Text, out num1);
            decimal.TryParse(tarewt.Text, out num2);
            if (num1 > num2)
            {
                netwt.Text = (num1 - num2).ToString("F3");
                CalculateWeightPerCop();
            }

            Log.writeMessage("POY CalculateNetWeight - End : " + DateTime.Now);
        }

        private void NetWeight_TextChanged(object sender, EventArgs e)
        {
            Log.writeMessage("POY NetWeight_TextChanged - Start : " + DateTime.Now);

            CalculateWeightPerCop();

            Log.writeMessage("POY NetWeight_TextChanged - End : " + DateTime.Now);
        }

        private void SpoolNo_TextChanged(object sender, EventArgs e)
        {
            Log.writeMessage("POY SpoolNo_TextChanged - Start : " + DateTime.Now);

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
                    GrossWeight_TextChanged(sender, e);
                    spoolnoerror.Text = "";
                    spoolnoerror.Visible = false;
                }
            }

            Log.writeMessage("POY SpoolNo_TextChanged - End : " + DateTime.Now);
        }

        private void CalculateWeightPerCop()
        {
            Log.writeMessage("POY CalculateWeightPerCop - Start : " + DateTime.Now);

            decimal num1 = 0, num2 = 0;

            decimal.TryParse(netwt.Text, out num1);
            decimal.TryParse(spoolno.Text, out num2);
            if (num1 > 0 && num2 > 0)
            {
                wtpercop.Text = (num1 / num2).ToString("F3");
            }

            Log.writeMessage("POY CalculateWeightPerCop - End : " + DateTime.Now);
        }

        private void CopyNos_TextChanged(object sender, EventArgs e)
        {
            Log.writeMessage("POY CopyNos_TextChanged - Start : " + DateTime.Now);

            if (string.IsNullOrWhiteSpace(copyno.Text))
            {
                copynoerror.Visible = true;
            }
            else
            {
                copynoerror.Text = "";
                copynoerror.Visible = false;
            }

            Log.writeMessage("POY CopyNos_TextChanged - End : " + DateTime.Now);
        }

        private void qualityqty_Paint(object sender, PaintEventArgs e)
        {
            Log.writeMessage("POY qualityqty_Paint - Start : " + DateTime.Now);

            _cmethod.DrawRectangleBorder((Control)sender, e, Color.LightGray, 2);

            Log.writeMessage("POY qualityqty_Paint - End : " + DateTime.Now);
        }

        private void windinggrid_Paint(object sender, PaintEventArgs e)
        {
            Log.writeMessage("POY windinggrid_Paint - Start : " + DateTime.Now);

            _cmethod.DrawRectangleBorder((Control)sender, e, Color.LightGray, 2);

            Log.writeMessage("POY windinggrid_Paint - End : " + DateTime.Now);
        }

        private void ordertable_Paint(object sender, PaintEventArgs e)
        {
            Log.writeMessage("POY ordertable_Paint - Start : " + DateTime.Now);

            _cmethod.DrawRoundedBorder((Control)sender, e, 12, Color.FromArgb(102, 163, 255), 1);

            Log.writeMessage("POY ordertable_Paint - End : " + DateTime.Now);
        }

        private void packagingtable_Paint(object sender, PaintEventArgs e)
        {
            Log.writeMessage("POY packagingtable_Paint - Start : " + DateTime.Now);

            _cmethod.DrawRoundedBorder((Control)sender, e, 12, Color.FromArgb(102, 163, 255), 1);

            Log.writeMessage("POY packagingtable_Paint - End : " + DateTime.Now);
        }

        private void weightable_Paint(object sender, PaintEventArgs e)
        {
            Log.writeMessage("POY weightable_Paint - Start : " + DateTime.Now);

            _cmethod.DrawRoundedBorder((Control)sender, e, 12, Color.FromArgb(102, 163, 255), 1);

            Log.writeMessage("POY weightable_Paint - End : " + DateTime.Now);
        }

        private void reviewtable_Paint(object sender, PaintEventArgs e)
        {
            Log.writeMessage("POY reviewtable_Paint - Start : " + DateTime.Now);

            _cmethod.DrawRoundedBorder((Control)sender, e, 12, Color.FromArgb(102, 163, 255), 1);

            Log.writeMessage("POY reviewtable_Paint - End : " + DateTime.Now);
        }

        private void machineboxlayout_Paint(object sender, PaintEventArgs e)
        {
            Log.writeMessage("POY machineboxlayout_Paint - Start : " + DateTime.Now);

            _cmethod.DrawRoundedBorder((Control)sender, e, 8, Color.FromArgb(191, 191, 191), 1);

            Log.writeMessage("POY machineboxlayout_Paint - End : " + DateTime.Now);
        }

        private void machineboxheader_Paint(object sender, PaintEventArgs e)
        {
            Log.writeMessage("POY machineboxheader_Paint - Start : " + DateTime.Now);

            _cmethod.DrawBottomBorder((Control)sender, e, Color.FromArgb(191, 191, 191), 1);

            Log.writeMessage("POY machineboxheader_Paint - End : " + DateTime.Now);
        }

        private void weighboxlayout_Paint(object sender, PaintEventArgs e)
        {
            Log.writeMessage("POY weighboxlayout_Paint - Start : " + DateTime.Now);

            _cmethod.DrawRoundedBorder((Control)sender, e, 8, Color.FromArgb(191, 191, 191), 1);

            Log.writeMessage("POY weighboxlayout_Paint - End : " + DateTime.Now);
        }

        private void weighboxheader_Paint(object sender, PaintEventArgs e)
        {
            Log.writeMessage("POY weighboxheader_Paint - Start : " + DateTime.Now);

            _cmethod.DrawBottomBorder((Control)sender, e, Color.FromArgb(191, 191, 191), 1);

            Log.writeMessage("POY weighboxheader_Paint - End : " + DateTime.Now);
        }

        private void packagingboxlayout_Paint(object sender, PaintEventArgs e)
        {
            Log.writeMessage("POY packagingboxlayout_Paint - Start : " + DateTime.Now);

            _cmethod.DrawRoundedBorder((Control)sender, e, 8, Color.FromArgb(191, 191, 191), 1);

            Log.writeMessage("POY packagingboxlayout_Paint - End : " + DateTime.Now);
        }

        private void packagingboxheader_Paint(object sender, PaintEventArgs e)
        {
            Log.writeMessage("POY packagingboxheader_Paint - Start : " + DateTime.Now);

            _cmethod.DrawBottomBorder((Control)sender, e, Color.FromArgb(191, 191, 191), 1);

            Log.writeMessage("POY packagingboxheader_Paint - End : " + DateTime.Now);
        }

        private void lastboxlayout_Paint(object sender, PaintEventArgs e)
        {
            Log.writeMessage("POY lastboxlayout_Paint - Start : " + DateTime.Now);

            _cmethod.DrawRoundedBorder((Control)sender, e, 8, Color.FromArgb(191, 191, 191), 1);

            Log.writeMessage("POY lastboxlayout_Paint - End : " + DateTime.Now);
        }

        private void lastboxheader_Paint(object sender, PaintEventArgs e)
        {
            Log.writeMessage("POY lastboxheader_Paint - Start : " + DateTime.Now);

            _cmethod.DrawBottomBorder((Control)sender, e, Color.FromArgb(191, 191, 191), 1);

            Log.writeMessage("POY lastboxheader_Paint - End : " + DateTime.Now);
        }

        private void lastbxcopspanel_Paint(object sender, PaintEventArgs e)
        {
            Log.writeMessage("POY lastbxcopspanel_Paint - Start : " + DateTime.Now);

            _cmethod.DrawRoundedDashedBorder((Control)sender, e, 8, Color.FromArgb(102, 163, 255), 1);

            Log.writeMessage("POY lastbxcopspanel_Paint - End : " + DateTime.Now);
        }

        private void lastbxtarepanel_Paint(object sender, PaintEventArgs e)
        {
            Log.writeMessage("POY lastbxtarepanel_Paint - Start : " + DateTime.Now);

            _cmethod.DrawRoundedDashedBorder((Control)sender, e, 8, Color.FromArgb(102, 163, 255), 1);

            Log.writeMessage("POY lastbxtarepanel_Paint - End : " + DateTime.Now);
        }

        private void lastbxgrosswtpanel_Paint(object sender, PaintEventArgs e)
        {
            Log.writeMessage("POY lastbxgrosswtpanel_Paint - Start : " + DateTime.Now);

            _cmethod.DrawRoundedDashedBorder((Control)sender, e, 8, Color.FromArgb(102, 163, 255), 1);

            Log.writeMessage("POY lastbxgrosswtpanel_Paint - End : " + DateTime.Now);
        }

        private void lastbxnetwtpanel_Paint(object sender, PaintEventArgs e)
        {
            Log.writeMessage("POY lastbxnetwtpanel_Paint - Start : " + DateTime.Now);

            _cmethod.DrawRoundedDashedBorder((Control)sender, e, 8, Color.FromArgb(102, 163, 255), 1);

            Log.writeMessage("POY lastbxnetwtpanel_Paint - End : " + DateTime.Now);
        }

        private void printingdetailslayout_Paint(object sender, PaintEventArgs e)
        {
            Log.writeMessage("POY printingdetailslayout_Paint - Start : " + DateTime.Now);

            _cmethod.DrawRoundedBorder((Control)sender, e, 8, Color.FromArgb(191, 191, 191), 1);

            Log.writeMessage("POY printingdetailslayout_Paint - End : " + DateTime.Now);
        }

        private void printingdetailsheader_Paint(object sender, PaintEventArgs e)
        {
            Log.writeMessage("POY printingdetailsheader_Paint - Start : " + DateTime.Now);

            _cmethod.DrawBottomBorder((Control)sender, e, Color.FromArgb(191, 191, 191), 1);

            Log.writeMessage("POY printingdetailsheader_Paint - End : " + DateTime.Now);
        }

        private void palletdetailslayout_Paint(object sender, PaintEventArgs e)
        {
            Log.writeMessage("POY palletdetailslayout_Paint - Start : " + DateTime.Now);

            _cmethod.DrawRoundedBorder((Control)sender, e, 8, Color.FromArgb(191, 191, 191), 1);

            Log.writeMessage("POY palletdetailslayout_Paint - End : " + DateTime.Now);
        }

        private void palletdetailsheader_Paint(object sender, PaintEventArgs e)
        {
            Log.writeMessage("POY palletdetailsheader_Paint - Start : " + DateTime.Now);

            _cmethod.DrawBottomBorder((Control)sender, e, Color.FromArgb(191, 191, 191), 1);

            Log.writeMessage("POY palletdetailsheader_Paint - End : " + DateTime.Now);
        }

        private void machineboxheader_Resize(object sender, EventArgs e)
        {
            Log.writeMessage("POY machineboxheader_Resize - Start : " + DateTime.Now);

            _cmethod.SetTopRoundedRegion(machineboxheader, 8);

            Log.writeMessage("POY machineboxheader_Resize - End : " + DateTime.Now);
        }

        private void weighboxheader_Resize(object sender, EventArgs e)
        {
            Log.writeMessage("POY weighboxheader_Resize - Start : " + DateTime.Now);

            _cmethod.SetTopRoundedRegion(weighboxheader, 8);

            Log.writeMessage("POY weighboxheader_Resize - End : " + DateTime.Now);
        }

        private void packagingboxheader_Resize(object sender, EventArgs e)
        {
            Log.writeMessage("POY packagingboxheader_Resize - Start : " + DateTime.Now);

            _cmethod.SetTopRoundedRegion(packagingboxheader, 8);

            Log.writeMessage("POY packagingboxheader_Resize - End : " + DateTime.Now);
        }

        private void lastboxheader_Resize(object sender, EventArgs e)
        {
            Log.writeMessage("POY lastboxheader_Resize - Start : " + DateTime.Now);

            _cmethod.SetTopRoundedRegion(lastboxheader, 8);

            Log.writeMessage("POY lastboxheader_Resize - End : " + DateTime.Now);
        }

        private void printingdetailsheader_Resize(object sender, EventArgs e)
        {
            Log.writeMessage("POY printingdetailsheader_Resize - Start : " + DateTime.Now);

            _cmethod.SetTopRoundedRegion(printingdetailsheader, 8);

            Log.writeMessage("POY printingdetailsheader_Resize - End : " + DateTime.Now);
        }

        private void palletdetailsheader_Resize(object sender, EventArgs e)
        {
            Log.writeMessage("POY palletdetailsheader_Resize - Start : " + DateTime.Now);

            _cmethod.SetTopRoundedRegion(palletdetailsheader, 8);

            Log.writeMessage("POY palletdetailsheader_Resize - End : " + DateTime.Now);
        }

        private void machinetablelayout_Paint(object sender, PaintEventArgs e)
        {
            Log.writeMessage("POY machinetablelayout_Paint - Start : " + DateTime.Now);

            _cmethod.DrawBottomBorder((Control)sender, e, Color.FromArgb(191, 191, 191), 1);

            Log.writeMessage("POY machinetablelayout_Paint - End : " + DateTime.Now);
        }

        private void popuppanel_Paint(object sender, PaintEventArgs e)
        {
            Log.writeMessage("POY popuppanel_Paint - Start : " + DateTime.Now);

            _cmethod.DrawPanelRoundedBorder((Control)sender, e, 8, Color.FromArgb(191, 191, 191), 1);

            Log.writeMessage("POY popuppanel_Paint - End : " + DateTime.Now);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Log.writeMessage("POY btnCancel_Click - Start : " + DateTime.Now);

            ResetForm(this);

            Log.writeMessage("POY btnCancel_Click - End : " + DateTime.Now);
        }

        private void ResetForm(Control parent)
        {
            Log.writeMessage("POY ResetForm - Start : " + DateTime.Now);

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
                frwt.Text = "0";
                upwt.Text = "0";
                remarks.Text = "";
                lotResponse = new LotsResponse();
                lotsDetailsList = new List<LotsDetailsResponse>();
                LoadDropdowns();
                rowMaterial.Columns.Clear();
                windinggrid.Columns.Clear();
                qualityqty.Columns.Clear();
                totalProdQty = 0;
                prodnbalqty.Text = "";
                selectedSOId = 0;
                totalSOQty = 0;
                grdsoqty.Text = "";
                balanceQty = 0;
                selectedMachineid = 0;
                selectedItemTypeid = 0;
                selectedDeptId = 0;
                selectLotId = 0;
                selectedSOId = 0;
                selectedSONumber = "";
                flowLayoutPanel1.Controls.Clear();
                rowCount = 0;
                prcompany.Checked = false;
                prowner.Checked = false;
                spoolno.Text = "0";
                salelotvalue.Text = "";
                lastbox.Text = "";
                boxnofrmt.Text = "";
                productionRequest = new ProductionRequest();
                salelotvalue.Text = "";
                lastbox.Text = "";
                boxnofrmt.Text = "";
                copstxtbox.Text = "";
                tarewghttxtbox.Text = "";
                grosswttxtbox.Text = "";
                netwttxtbox.Text = "";
                findbtn.Enabled = true;
                delete.Enabled = false;
                _productionId = 0;
                dateTimePicker1.Text = "";
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

            Log.writeMessage("POY ResetForm - End : " + DateTime.Now);
        }

        private void AdjustNameByCharCount()
        {
            Log.writeMessage("AdjustNameByCharCount - Start : " + DateTime.Now);

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

            Log.writeMessage("AdjustNameByCharCount - End : " + DateTime.Now);
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            Log.writeMessage("POY btnFind_Click - Start : " + DateTime.Now);

            if (datalistpopuppanel.Visible) datalistpopuppanel.Visible = false;
            popuppanel.Visible = true;
            popuppanel.BringToFront();

            // Center popup in form
            popuppanel.Left = (this.ClientSize.Width - popuppanel.Width) / 2;
            popuppanel.Top = (this.ClientSize.Height - popuppanel.Height) / 2;

            panel58.Focus();

            Log.writeMessage("POY btnFind_Click - End : " + DateTime.Now);
        }

        private void btnClosePopup_Click(object sender, EventArgs e)
        {
            Log.writeMessage("POY btnClosePopup_Click - Start : " + DateTime.Now);

            popuppanel.Visible = false;
            //srlinenoradiobtn.Checked = srdeptradiobtn.Checked = srproddateradiobtn.Checked = srboxnoradiobtn.Checked = false;
            //SrLineNoList.Enabled = SrDeptList.Enabled = SrBoxNoList.Enabled = dateTimePicker2.Enabled = false;
            selectedSrMachineId = 0; selectedSrDeptId = 0; selectedSrBoxNo = null; selectedSrProductionDate = dateTimePicker2.Value.ToString("dd-MM-yyyy");
            dateTimePicker2.Format = DateTimePickerFormat.Custom;
            dateTimePicker2.CustomFormat = "dd/MM/yyyy";
            LoadSearchDropdowns();
            findbtn.Focus();

            Log.writeMessage("POY btnClosePopup_Click - End : " + DateTime.Now);
        }

        private void SrLineNoList_TextUpdate(object sender, EventArgs e)
        {
            Log.writeMessage("POY SrLineNoList_TextUpdate - Start : " + DateTime.Now);

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

                var machineList = _masterService.GetMachineList("SpinningLot", typedText).Result.OrderBy(x => x.MachineName).ToList();
                machineList.Insert(0, new MachineResponse { MachineId = 0, MachineName = "Select Line No." });

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

            Log.writeMessage("POY SrLineNoList_TextUpdate - End : " + DateTime.Now);
        }

        private void SrDeptList_TextUpdate(object sender, EventArgs e)
        {
            Log.writeMessage("POY SrDeptList_TextUpdate - Start : " + DateTime.Now);

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

                var deptList = _masterService.GetDepartmentList("POY", typedText).Result.OrderBy(x => x.DepartmentName).ToList();

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
            Log.writeMessage("POY SrDeptList_TextUpdate - End : " + DateTime.Now);
        }

        private void SrBoxNoList_TextUpdate(object sender, EventArgs e)
        {
            Log.writeMessage("POY SrBoxNoList_TextUpdate - Start : " + DateTime.Now);

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
                getListRequest.PackingType = "POYPacking";
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
            Log.writeMessage("POY SrBoxNoList_TextUpdate - End : " + DateTime.Now);
        }

        //private void rbLineNo_CheckedChanged(object sender, EventArgs e)
        //{
        //    Log.writeMessage("POY rbLineNo_CheckedChanged - Start : " + DateTime.Now);

        //    if (!srlinenoradiobtn.Checked)
        //        return;

        //    SrLineNoList.Enabled = srlinenoradiobtn.Checked;
        //    SrDeptList.Enabled = false;
        //    SrBoxNoList.Enabled = false;
        //    dateTimePicker2.Enabled = false;

        //    Log.writeMessage("POY rbLineNo_CheckedChanged - End : " + DateTime.Now);
        //}

        //private void rbDepartment_CheckedChanged(object sender, EventArgs e)
        //{
        //    Log.writeMessage("POY rbDepartment_CheckedChanged - Start : " + DateTime.Now);

        //    if (!srdeptradiobtn.Checked)
        //        return;

        //    SrDeptList.Enabled = srdeptradiobtn.Checked;
        //    SrLineNoList.Enabled = false;
        //    SrBoxNoList.Enabled = false;
        //    dateTimePicker2.Enabled = false;

        //    Log.writeMessage("POY rbDepartment_CheckedChanged - End : " + DateTime.Now);
        //}

        //private void rbBoxNo_CheckedChanged(object sender, EventArgs e)
        //{
        //    Log.writeMessage("POY rbBoxNo_CheckedChanged - Start : " + DateTime.Now);

        //    if (!srboxnoradiobtn.Checked)
        //        return;

        //    SrBoxNoList.Enabled = srboxnoradiobtn.Checked;
        //    SrLineNoList.Enabled = false;
        //    SrDeptList.Enabled = false;
        //    dateTimePicker2.Enabled = false;

        //    Log.writeMessage("POY rbBoxNo_CheckedChanged - End : " + DateTime.Now);
        //}

        //private void rbDate_CheckedChanged(object sender, EventArgs e)
        //{
        //    Log.writeMessage("POY rbDate_CheckedChanged - Start : " + DateTime.Now);

        //    if (!srproddateradiobtn.Checked)
        //        return;

        //    dateTimePicker2.Enabled = srproddateradiobtn.Checked;
        //    SrLineNoList.Enabled = false;
        //    SrDeptList.Enabled = false;
        //    SrBoxNoList.Enabled = false;

        //    Log.writeMessage("POY rbDate_CheckedChanged - End : " + DateTime.Now);
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
            Log.writeMessage("POY btnSearch_Click - Start : " + DateTime.Now);

            if (selectedSrMachineId == 0 && selectedSrDeptId == 0 && (string.IsNullOrEmpty(selectedSrBoxNo)) && (string.IsNullOrEmpty(selectedSrProductionDate)))
            {
                MessageBox.Show("Please select at least any one option.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            getProductionList(1);

            Log.writeMessage("POY btnSearch_Click - End : " + DateTime.Now);
        }

        private void getProductionList(int currentPage)
        {
            Log.writeMessage("POY getProductionList - Start : " + DateTime.Now);

            //int machineid = 0, deptid = 0;
            //string boxnoid = null;
            //string proddt = null;
            //if (srlinenoradiobtn.Checked) { machineid = selectedSrMachineId; }
            //if (srdeptradiobtn.Checked) { deptid = selectedSrDeptId; }
            //if (srboxnoradiobtn.Checked) { boxnoid = selectedSrBoxNo; }
            //if (srproddateradiobtn.Checked) { proddt = selectedSrProductionDate; }

            GetProductionList getListRequest = new GetProductionList();
            getListRequest.PackingType = "POYPacking";
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

            Log.writeMessage("POY getProductionList - End : " + DateTime.Now);
        }

        private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            int srNo = (currentPage - 1) * pageSize + e.RowIndex + 1;
            dataGridView1.Rows[e.RowIndex].Cells["SrNo"].Value = srNo;
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            Log.writeMessage("POY btnNext_Click - Start : " + DateTime.Now);

            if (currentPage < totalPages)
            {
                currentPage++;
                getProductionList(currentPage);
            }

            Log.writeMessage("POY btnNext_Click - End : " + DateTime.Now);
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            Log.writeMessage("POY btnPrevious_Click - Start : " + DateTime.Now);

            if (currentPage > 1)
            {
                currentPage--;
                getProductionList(currentPage);
            }

            Log.writeMessage("POY btnPrevious_Click - End : " + DateTime.Now);
        }

        private void btnFirstPg_Click(object sender, EventArgs e)
        {
            Log.writeMessage("POY btnFirstPg_Click - Start : " + DateTime.Now);

            if (currentPage <= totalPages)
            {
                currentPage = 1;
                getProductionList(currentPage);
            }

            Log.writeMessage("POY btnFirstPg_Click - End : " + DateTime.Now);
        }

        private void btnLastPg_Click(object sender, EventArgs e)
        {
            Log.writeMessage("POY btnLastPg_Click - Start : " + DateTime.Now);

            if (currentPage < totalPages)
            {
                currentPage = totalPages;
                getProductionList(currentPage);
            }

            Log.writeMessage("POY btnLastPg_Click - End : " + DateTime.Now);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            Log.writeMessage("POY dataGridView1_CellContentClick - Start : " + DateTime.Now);

            if (e.RowIndex >= 0 && e.ColumnIndex == dataGridView1.Columns["Action"].Index)
            {
                EditRow(e.RowIndex);
            }

            Log.writeMessage("POY dataGridView1_CellContentClick - End : " + DateTime.Now);
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            Log.writeMessage("POY dataGridView1_KeyDown - Start : " + DateTime.Now);

            if ((e.KeyCode == Keys.Enter || e.KeyCode == Keys.Space) &&
                dataGridView1.CurrentCell?.OwningColumn?.Name == "Action")
            {
                EditRow(dataGridView1.CurrentCell.RowIndex);
                e.Handled = true;
            }

            Log.writeMessage("POY dataGridView1_KeyDown - End : " + DateTime.Now);
        }

        private async void EditRow(int rowIndex)
        {
            Log.writeMessage("POY EditRow - Start : " + DateTime.Now);

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

            var getSelectedProductionDetails = _packingService.getLastBoxDetails("poypacking", productionId).Result;

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

            Log.writeMessage("POY EditRow - End : " + DateTime.Now);
        }

        private async void SrLineNoList_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Log.writeMessage("POY SrLineNoList_SelectionChangeCommitted - Start : " + DateTime.Now);

            if (!isFormReady) return; // skip during load

            //if (suppressEvents) return;     //Prevent recursive refresh

            if (SrLineNoList.Items.Count == 0) return;

            if (SrLineNoList.SelectedIndex <= 0)
            {
                return;
            }
            //suppressEvents = true;          //Freeze dependent dropdown events
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
                            var deptTask = _masterService.GetDepartmentList("POY", selectedMachine.DepartmentName).Result;
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
                //suppressEvents = false;             //Allow events again
            }

            Log.writeMessage("POY SrLineNoList_SelectionChangeCommitted - End : " + DateTime.Now);
        }

        private async void SrDeptList_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Log.writeMessage("POY SrDeptList_SelectionChangeCommitted - Start : " + DateTime.Now);

            if (!isFormReady) return; // skip during load

            //if (suppressEvents) return;     //Prevent recursive refresh

            if (SrDeptList.Items.Count == 0) return;

            if (SrDeptList.SelectedIndex <= 0)
            {
                return;
            }
            //suppressEvents = true;          //Freeze dependent dropdown events
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
                //suppressEvents = false;             //Allow events again
            }

            Log.writeMessage("POY SrDeptList_SelectionChangeCommitted - End : " + DateTime.Now);
        }

        private async void SrBoxNoList_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Log.writeMessage("POY SrBoxNoList_SelectionChangeCommitted - Start : " + DateTime.Now);

            if (!isFormReady) return; // skip during load

            //if (suppressEvents) return;     //Prevent recursive refresh

            if (SrBoxNoList.Items.Count == 0) return;

            if (SrBoxNoList.SelectedIndex <= 0)
            {
                return;
            }
            //suppressEvents = true;          //Freeze dependent dropdown events
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
                //suppressEvents = false;             //Allow events again
            }

            Log.writeMessage("POY SrBoxNoList_SelectionChangeCommitted - End : " + DateTime.Now);
        }

        private void SrProdDate_DropDownClosed(object sender, EventArgs e)
        {
            Log.writeMessage("POY SrProdDate_DropDownClosed - Start : " + DateTime.Now);

            dateTimePicker2.Format = DateTimePickerFormat.Custom;
            dateTimePicker2.CustomFormat = "dd/MM/yyyy";
            DateTime selectedDate = dateTimePicker2.Value.Date;
            selectedSrProductionDate = selectedDate.ToString("dd-MM-yyyy");

            Log.writeMessage("POY SrProdDate_DropDownClosed - End : " + DateTime.Now);
        }

        private void SrProdDate_KeyDown(object sender, KeyEventArgs e)
        {
            Log.writeMessage("POY SrProdDate_KeyDown - Start : " + DateTime.Now);

            if (e.KeyCode == Keys.Delete || e.KeyCode == Keys.Back)
            {
                dateTimePicker2.Format = DateTimePickerFormat.Custom;
                dateTimePicker2.CustomFormat = " ";
                selectedSrProductionDate = null;
            }

            Log.writeMessage("POY SrProdDate_KeyDown - End : " + DateTime.Now);
        }

        private void btnDatalistClosePopup_Click(object sender, EventArgs e)
        {
            Log.writeMessage("POY btnDatalistClosePopup_Click - Start : " + DateTime.Now);

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
            panel58.Focus();

            Log.writeMessage("POY btnDatalistClosePopup_Click - End : " + DateTime.Now);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            Log.writeMessage("POY btnDelete_Click - Start : " + DateTime.Now);

            if (_productionId > 0) 
            {
                DialogResult result = MessageBox.Show("Are you sure you want to delete?",
                "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    productionRequest.IsDisabled = true;

                    ProductionResponse response = new ProductionResponse();
                    response = _packingService.AddUpdatePOYPacking(_productionId, productionRequest);
                    if (response.IsDisabled)
                    {
                        ShowCustomMessage(boxnofrmt.Text);
                        //delete.Enabled = false;
                        ResetForm(this);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            delete.Enabled = false;
            findbtn.Enabled = true;
            LoadSearchDropdowns();

            Log.writeMessage("POY btnDelete_Click - End : " + DateTime.Now);
        }

        private void ShowCustomMessage(string boxNo)
        {
            Log.writeMessage("POY ShowCustomMessage - Start : " + DateTime.Now);

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
                    Text = $"POY Packing deleted successfully for BoxNo {boxNo}.",
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

            Log.writeMessage("POY ShowCustomMessage - End : " + DateTime.Now);
        }

        private void SrLineNoList_KeyDown(object sender, KeyEventArgs e)
        {
            Log.writeMessage("POY SrLineNoList_KeyDown - Start : " + DateTime.Now);

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
                var machineList = _masterService.GetMachineList("SpinningLot", "").Result.OrderBy(x => x.MachineName).ToList();
                machineList.Insert(0, new MachineResponse { MachineId = 0, MachineName = "Select Line No." });
                SrLineNoList.DataSource = machineList;
                SrLineNoList.DisplayMember = "MachineName";
                SrLineNoList.ValueMember = "MachineId";
                SrLineNoList.SelectedIndex = 0;
                SrLineNoList.DroppedDown = true; // Open the dropdown list
                e.SuppressKeyPress = true;    // Prevent any side effect
            }

            Log.writeMessage("POY SrLineNoList_KeyDown - End : " + DateTime.Now);
        }

        private void SrDeptList_KeyDown(object sender, KeyEventArgs e)
        {
            Log.writeMessage("POY SrDeptList_KeyDown - Start : " + DateTime.Now);

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
                var deptList = _masterService.GetDepartmentList("POY", "").Result.OrderBy(x => x.DepartmentName).ToList();
                deptList.Insert(0, new DepartmentResponse { DepartmentId = 0, DepartmentName = "Select Dept" });
                SrDeptList.DisplayMember = "DepartmentName";
                SrDeptList.ValueMember = "DepartmentId";
                SrDeptList.DataSource = deptList;
                SrDeptList.SelectedIndex = 0;
                SrDeptList.DroppedDown = true; // Open the dropdown list
                e.SuppressKeyPress = true;    // Prevent any side effect
            }

            Log.writeMessage("POY SrDeptList_KeyDown - End : " + DateTime.Now);
        }

        private void SrBoxNoList_KeyDown(object sender, KeyEventArgs e)
        {
            Log.writeMessage("POY SrBoxNoList_KeyDown - Start : " + DateTime.Now);

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
                getListRequest.PackingType = "POYPacking";
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

            Log.writeMessage("POY SrBoxNoList_KeyDown - End : " + DateTime.Now);
        }

        //private void SrLineNoRadiobtn_KeyDown(object sender, KeyEventArgs e)
        //{
        //    Log.writeMessage("POY SrLineNoRadiobtn_KeyDown - End : " + DateTime.Now);

        //    if (e.KeyCode == Keys.Enter)
        //    {
        //        RadioButton rb = sender as RadioButton;
        //        rb.Checked = !rb.Checked;   // toggle select / deselect
        //        if (rb.Checked) srdeptradiobtn.Checked = srboxnoradiobtn.Checked = srproddateradiobtn.Checked = false;
        //        e.Handled = true;
        //    }

        //    Log.writeMessage("POY SrLineNoRadiobtn_KeyDown - End : " + DateTime.Now);
        //}

        //private void SrDeptRadiobtn_KeyDown(object sender, KeyEventArgs e)
        //{
        //    Log.writeMessage("POY SrDeptRadiobtn_KeyDown - End : " + DateTime.Now);

        //    if (e.KeyCode == Keys.Enter)
        //    {
        //        RadioButton rb = sender as RadioButton;
        //        rb.Checked = !rb.Checked;   // toggle select / deselect
        //        if (rb.Checked) srlinenoradiobtn.Checked = srboxnoradiobtn.Checked = srproddateradiobtn.Checked = false;
        //        e.Handled = true;
        //    }

        //    Log.writeMessage("POY SrDeptRadiobtn_KeyDown - End : " + DateTime.Now);
        //}

        //private void SrBoxNoRadiobtn_KeyDown(object sender, KeyEventArgs e)
        //{
        //    Log.writeMessage("POY SrBoxNoRadiobtn_KeyDown - End : " + DateTime.Now);

        //    if (e.KeyCode == Keys.Enter)
        //    {
        //        RadioButton rb = sender as RadioButton;
        //        rb.Checked = !rb.Checked;   // toggle select / deselect
        //        if (rb.Checked) srdeptradiobtn.Checked = srlinenoradiobtn.Checked = srproddateradiobtn.Checked = false;
        //        e.Handled = true;
        //    }

        //    Log.writeMessage("POY SrBoxNoRadiobtn_KeyDown - End : " + DateTime.Now);
        //}

        //private void SrProdDateRadiobtn_KeyDown(object sender, KeyEventArgs e)
        //{
        //    Log.writeMessage("POY SrProdDateRadiobtn_KeyDown - End : " + DateTime.Now);

        //    if (e.KeyCode == Keys.Enter)
        //    {
        //        RadioButton rb = sender as RadioButton;
        //        rb.Checked = !rb.Checked;   // toggle select / deselect
        //        if (rb.Checked) srdeptradiobtn.Checked = srlinenoradiobtn.Checked = srboxnoradiobtn.Checked = false;
        //        e.Handled = true;
        //    }

        //    Log.writeMessage("POY SrProdDateRadiobtn_KeyDown - End : " + DateTime.Now);
        //}

        //private void RadioButton_MouseDown(object sender, MouseEventArgs e)
        //{
        //    Log.writeMessage("POY RadioButton_MouseDown - End : " + DateTime.Now);

        //    RadioButton rb = sender as RadioButton;
        //    if (rb == null) return;

        //    SelectRadio(rb);

        //    Log.writeMessage("POY RadioButton_MouseDown - End : " + DateTime.Now);
        //}

        //private void SelectRadio(RadioButton selected)
        //{
        //    Log.writeMessage("POY SelectRadio - End : " + DateTime.Now);

        //    foreach (Control ctrl in selected.Parent.Controls)
        //    {
        //        if (ctrl is RadioButton rb)
        //        {
        //            rb.Checked = (rb == selected);
        //        }
        //    }

        //    Log.writeMessage("POY SelectRadio - End : " + DateTime.Now);
        //}
    }
}
