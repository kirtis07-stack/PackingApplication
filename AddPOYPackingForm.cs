using Microsoft.Reporting.Map.WebForms.BingMaps;
using Microsoft.Reporting.WinForms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PackingApplication.Constants;
using PackingApplication.Helper;
using PackingApplication.Models.CommonEntities;
using PackingApplication.Models.RequestEntities;
using PackingApplication.Models.ResponseEntities;
using PackingApplication.Properties;
using PackingApplication.Services;
using PdfiumViewer;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection.Emit;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Net.WebRequestMethods;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolBar;
using File = System.IO.File;

namespace PackingApplication
{
    public partial class AddPOYPackingForm: Form
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
        short selectedItemTypeid = 0;
        short selectedMainItemTypeid = 0;
        ProductionPrintSlipRequest slipRequest = new ProductionPrintSlipRequest();
        private Panel _editingPanel = null;
        public AddPOYPackingForm()
        {
            Log.writeMessage("POY AddPOYPackingForm - Start : " + DateTime.Now);

            InitializeComponent();
            ApplyFonts();

            //this.Shown += AddPOYPackingForm_Shown;
            this.AutoScroll = true;
            lblLoading = CommonMethod.InitializeLoadingLabel(this);

            _cmethod.SetButtonBorderRadius(this.addqty, 8);
            _cmethod.SetButtonBorderRadius(this.submit, 8);
            _cmethod.SetButtonBorderRadius(this.cancelbtn, 8);
            _cmethod.SetButtonBorderRadius(this.saveprint, 8);

            width = flowLayoutPanel1.ClientSize.Width;
            rowMaterial.AutoGenerateColumns = false;
            windinggrid.AutoGenerateColumns = false;
            qualityqty.AutoGenerateColumns = false;

            Log.writeMessage("POY AddPOYPackingForm - End : " + DateTime.Now);
        }

        private void AddPOYPackingForm_Load(object sender, EventArgs e)
        {
            Log.writeMessage("POY AddPOYPackingForm_Load - Start : " + DateTime.Now);

            AddHeader();

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
            //partyn.Text = "";
            //partyshade.Text = "";
            dateTimePicker1.Value = DateTimeHelper.GetDateTime();
            isFormReady = true;

            RefreshLastBoxDetails();

            prcompany.FlatStyle = FlatStyle.System;
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

            Log.writeMessage("POY AddPOYPackingForm_Load - Start : " + DateTime.Now);
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

            Log.writeMessage("POY LoadDropdowns - End : " + DateTime.Now);
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
            this.addqty.Font             = FontManager.GetFont(8F, FontStyle.Bold);
            this.flowLayoutPanel1.Font   = FontManager.GetFont(8F, FontStyle.Regular);
            this.submit.Font             = FontManager.GetFont(8F, FontStyle.Bold);
            this.saveprint.Font          = FontManager.GetFont(8F, FontStyle.Bold);
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
            this.PrefixList.Font         = FontManager.GetFont(8F, FontStyle.Regular);
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

            Log.writeMessage("POY ApplyFonts - End : " + DateTime.Now);
        }

        //private async void AddPOYPackingForm_Shown(object sender, EventArgs e)
        //{
        //    Log.writeMessage("POY AddPOYPackingForm_Shown - Start : " + DateTime.Now);

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

        //    Log.writeMessage("POY AddPOYPackingForm_Shown - End : " + DateTime.Now);
        //}

        private async Task LoadProductionDetailsAsync(ProductionResponse prodResponse)
        {
            Log.writeMessage("POY LoadProductionDetailsAsync - Start : " + DateTime.Now);

            if (prodResponse != null)
            {
                productionResponse = prodResponse;

                //LineNoList.SelectedValue = productionResponse.MachineId;
                //DeptList.SelectedValue = productionResponse.DepartmentId;
                //PrefixList.SelectedValue = productionResponse.PrefixCode;
                //MergeNoList.SelectedValue = productionResponse.LotId;
                //SaleOrderList.SelectedValue = productionResponse.SaleOrderItemsId;
                //QualityList.SelectedValue = productionResponse.QualityId;
                //WindingTypeList.SelectedValue = productionResponse.WindingTypeId;
                //PackSizeList.SelectedValue = productionResponse.PackSizeId;
                //CopsItemList.SelectedValue = productionResponse.SpoolItemId;
                //BoxItemList.SelectedValue = productionResponse.BoxItemId;
                //prodtype.Text = productionResponse.ProductionType;
                //remarks.Text = productionResponse.Remarks;
                //prcompany.Checked = productionResponse.PrintCompany;
                //prowner.Checked = productionResponse.PrintOwner;
                //prdate.Checked = productionResponse.PrintDate;
                //pruser.Checked = productionResponse.PrintUser;
                //prhindi.Checked = productionResponse.PrintHindiWords;
                //prwtps.Checked = productionResponse.PrintWTPS;
                //prqrcode.Checked = productionResponse.PrintQRCode;
                //OwnerList.SelectedValue = productionResponse.OwnerId;
                //LineNoList_SelectedIndexChanged(LineNoList, EventArgs.Empty);
                
                
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
                productionRequest.PrintTwist = productionResponse.PrintTwist;
                //OwnerList.SelectedValue = productionResponse.OwnerId;
                //LineNoList_SelectedIndexChanged(LineNoList, EventArgs.Empty);
                productionRequest.PrefixCode = productionResponse.PrefixCode;
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
                copsitemwt.Text = productionResponse.CopsItemWeight.ToString();
                boxpalletitemwt.Text = productionResponse.BoxItemWeight.ToString();
                palletwtno.Text = productionResponse.BoxItemWeight.ToString();
                totalSOQty = productionResponse.SOQuantity;
                grdsoqty.Text = totalSOQty.ToString("F2");
                RefreshGradewiseGrid();
                RefreshWindingGrid();
                AdjustNameByCharCount();
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

                selectedMainItemTypeid = productionResponse.MainItemTypeId;
                selectedItemTypeid = productionResponse.ItemTypeId;
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
                // Edit Button
                System.Windows.Forms.Button btnEdit = new System.Windows.Forms.Button() { Text = "Edit", Size = new Size(35, 23), Location = new System.Drawing.Point(320, 5), Font = FontManager.GetFont(7F, FontStyle.Regular), BackColor = Color.FromArgb(230, 240, 255), ForeColor = Color.FromArgb(51, 133, 255), Tag = new Tuple<ItemResponse, System.Windows.Forms.Label>(selectedItem, lblQty), FlatStyle = FlatStyle.Flat };
                btnEdit.FlatAppearance.BorderColor = Color.FromArgb(51, 133, 255);
                btnEdit.FlatAppearance.BorderSize = 1;
                btnEdit.FlatAppearance.MouseOverBackColor = Color.FromArgb(210, 230, 255);
                btnEdit.FlatAppearance.MouseDownBackColor = Color.FromArgb(180, 210, 255);
                btnEdit.FlatAppearance.BorderSize = 0;
                btnEdit.TabIndex = 4;
                btnEdit.TabStop = true;
                btnEdit.Cursor = Cursors.Hand;
                btnEdit.Paint += (s, f) =>
                {
                    var rect = new Rectangle(0, 0, btnEdit.Width - 1, btnEdit.Height - 1);

                    using (GraphicsPath path = _cmethod.GetRoundedRect(rect, 4)) // radius = 4
                    using (Pen borderPen = new Pen(btnEdit.FlatAppearance.BorderColor, btnEdit.FlatAppearance.BorderSize))
                    using (SolidBrush brush = new SolidBrush(btnEdit.BackColor))
                    {
                        f.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

                        f.Graphics.FillPath(brush, path);

                        f.Graphics.DrawPath(borderPen, path);

                        if (btnEdit.Focused)
                        {
                            ControlPaint.DrawFocusRectangle(f.Graphics, rect);
                        }

                        TextRenderer.DrawText(
                            f.Graphics,
                            btnEdit.Text,
                            btnEdit.Font,
                            rect,
                            btnEdit.ForeColor,
                            TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter
                        );
                    }
                };
                btnEdit.Click += editPallet_Click;

                // Delete Button
                System.Windows.Forms.Button btnDelete = new System.Windows.Forms.Button() { Name = "btnRemove", Text = "Remove", Size = new Size(50, 23), Location = new System.Drawing.Point(360, 5), Font = FontManager.GetFont(7F, FontStyle.Regular), BackColor = Color.FromArgb(255, 230, 230), ForeColor = Color.FromArgb(255, 51, 51), Tag = rowPanel, FlatStyle = FlatStyle.Flat };
                btnDelete.FlatAppearance.BorderColor = Color.FromArgb(255, 51, 51);
                btnDelete.FlatAppearance.BorderSize = 1;
                btnDelete.FlatAppearance.MouseOverBackColor = Color.FromArgb(255, 204, 204);
                btnDelete.FlatAppearance.MouseDownBackColor = Color.FromArgb(255, 230, 230);
                btnDelete.FlatAppearance.BorderSize = 0;
                btnDelete.TabIndex = 5;
                btnDelete.TabStop = true;
                btnDelete.Cursor = Cursors.Hand;
                btnDelete.Paint += (s, f) =>
                {
                    var button = (System.Windows.Forms.Button)s;
                    var rect = new Rectangle(0, 0, button.Width - 1, button.Height - 1);

                    // button color change for enabled/disabled
                    Color backColor = button.Enabled ? button.BackColor : Color.LightGray;
                    Color borderColor = button.Enabled ? button.FlatAppearance.BorderColor : Color.Gray;
                    Color foreColor = button.Enabled ? button.ForeColor : Color.DarkGray;

                    using (GraphicsPath path = _cmethod.GetRoundedRect(rect, 4))
                    using (Pen borderPen = new Pen(borderColor, button.FlatAppearance.BorderSize))
                    using (SolidBrush brush = new SolidBrush(backColor))
                    {
                        f.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                        f.Graphics.FillPath(brush, path);
                        f.Graphics.DrawPath(borderPen, path);

                        if (btnDelete.Focused)
                        {
                            ControlPaint.DrawFocusRectangle(f.Graphics, rect);
                        }

                        TextRenderer.DrawText(
                            f.Graphics,
                            button.Text,
                            button.Font,
                            rect,
                            foreColor,
                            TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter
                        );
                    }
                };
                // Remove Row
                btnDelete.Click += (s, args) =>
                {
                    flowLayoutPanel1.Controls.Remove(rowPanel);
                    ReorderSrNo();
                };

                rowPanel.Controls.Add(lblSrNo);
                rowPanel.Controls.Add(lblItem);
                rowPanel.Controls.Add(lblQty);
                rowPanel.Controls.Add(btnEdit);
                rowPanel.Controls.Add(btnDelete);
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

            Log.writeMessage("POY BindPalletDetails - End : " + DateTime.Now);
        }

        private async void LineNoList_SelectedIndexChanged(object sender, EventArgs e)
        {
            Log.writeMessage("POY LineNoList_SelectedIndexChanged - Start : " + DateTime.Now);

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
                            var deptTask = _masterService.GetDepartmentList("POY", selectedMachine.DepartmentName).Result;
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
                            DeptList_SelectedIndexChanged(DeptList, EventArgs.Empty);
                        }
                        if (productionRequest.PrefixCode != 0)
                        {
                            prefixRequest.DepartmentId = selectedDeptId;
                            prefixRequest.TxnFlag = "POY";
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
                        //    if (selectedMachineId == productionResponse.MachineId)
                        //    {
                        //        MergeNoList.SelectedValue = productionResponse.LotId;
                        //        DeptList.SelectedValue = productionResponse.DepartmentId;
                        //    }
                        //}
                    }
                }
            }
            finally
            {
                lblLoading.Visible = false;
                suppressEvents = false;             //Allow events again
            }

            Log.writeMessage("POY LineNoList_SelectedIndexChanged - End : " + DateTime.Now);
        }

        private void LinoNoList_TextUpdate(object sender, EventArgs e)
        {
            Log.writeMessage("POY LinoNoList_TextUpdate - Start : " + DateTime.Now);

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
                    machineList = _masterService.GetMachineList("SpinningLot", typedText).Result.OrderBy(x => x.MachineName).ToList();

                    machineList.Insert(0, new MachineResponse { MachineId = 0, MachineName = "Select Line No." });
                }
                else
                {
                    machineList = _masterService.GetMachineByDepartmentIdAndLotType(selectedDeptId, "SpinningLot").Result.OrderBy(x => x.MachineName).ToList();

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

            Log.writeMessage("POY LinoNoList_TextUpdate - End : " + DateTime.Now);
        }

        private async void MergeNoList_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Log.writeMessage("POY MergeNoList_SelectionChangeCommitted - Start : " + DateTime.Now);

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
                            salelotvalue.Text = (!string.IsNullOrEmpty(lotResponse.SaleLot)) ? lotResponse.SaleLot.ToString() : null;
                            productionRequest.SaleLot = (!string.IsNullOrEmpty(lotResponse.SaleLot)) ? lotResponse.SaleLot : null;
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
                                    QualityList.DataSource = qualityList;
                                    QualityList.DisplayMember = "Name";
                                    QualityList.ValueMember = "QualityId";
                                    //QualityList.SelectedIndex = 0;
                                    //QualityList.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                                    //QualityList.AutoCompleteSource = AutoCompleteSource.ListItems;
                                    //QualityList.DropDownStyle = ComboBoxStyle.DropDown;
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
                            //    }           
                            //}
                        }                      

                        //var getWindingType = new List<WindingTypeResponse>();
                        //getWindingType = _productionService.getWinderTypeList(selectedLotId,"").Result;
                        //getWindingType.Insert(0, new WindingTypeResponse { WindingTypeId = 0, WindingTypeName = "Select Winding Type" });
                        //if (getWindingType.Count <= 1)
                        //{
                        //    getWindingType = _masterService.GetWindingTypeList("").Result;
                        //    getWindingType.Insert(0, new WindingTypeResponse { WindingTypeId = 0, WindingTypeName = "Select Winding Type" });

                        //}
                        //WindingTypeList.DataSource = getWindingType;
                        //WindingTypeList.DisplayMember = "WindingTypeName";
                        //WindingTypeList.ValueMember = "WindingTypeId";
                        //WindingTypeList.SelectedIndex = 0;
                        //WindingTypeList.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                        //WindingTypeList.AutoCompleteSource = AutoCompleteSource.ListItems;

                        var getSaleOrder = _productionService.getSaleOrderList(selectedLotId, "").Result.OrderBy(x => x.ItemName).ToList();
                        getSaleOrder.Insert(0, new LotSaleOrderDetailsResponse { SaleOrderItemsId = 0, ItemName = "Select Sale Order Item" });
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
                        SaleOrderList_SelectedIndexChanged(SaleOrderList, EventArgs.Empty);
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

            Log.writeMessage("POY MergeNoList_SelectionChangeCommitted - End : " + DateTime.Now);
        }

        private void MergeNoList_TextUpdate(object sender, EventArgs e)
        {
            Log.writeMessage("POY MergeNoList_TextUpdate - Start : " + DateTime.Now);

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
                    mergenoList = _productionService.getLotsByLotType("SpinningLot", typedText).Result.OrderBy(x => x.LotNoFrmt).ToList();

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

            Log.writeMessage("POY MergeNoList_TextUpdate - End : " + DateTime.Now);
        }

        private void ResetLotValues()
        {
            Log.writeMessage("POY ResetLotValues - Start : " + DateTime.Now);

            itemname.Text = "";
            shadename.Text = "";
            shadecd.Text = "";
            deniervalue.Text = "";
            salelotvalue.Text = "";
            partyn.Text = "";
            partyshade.Text = "";
            lotResponse = new LotsResponse();
            lotsDetailsList = new List<LotsDetailsResponse>();
            ResetDependentDropdownValues();
            rowMaterial.Columns.Clear();
            windinggrid.Columns.Clear();
            qualityqty.Columns.Clear();
            totalProdQty = 0;
            prodnbalqty.Text = "";
            selectedSOId = 0;
            totalSOQty = 0;
            grdsoqty.Text = "";
            balanceQty = 0;
            selectLotId = 0;
            selectedSONumber = "";
            selectedItemTypeid = 0;
            flowLayoutPanel1.Controls.Clear();
            rowCount = 0;
            AddHeader();

            Log.writeMessage("POY ResetLotValues - End : " + DateTime.Now);
        }

        private async void PackSizeList_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Log.writeMessage("POY PackSizeList_SelectionChangeCommitted - Start : " + DateTime.Now);

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

            Log.writeMessage("POY PackSizeList_SelectionChangeCommitted - End : " + DateTime.Now);
        }

        private void PackSizeList_TextUpdate(object sender, EventArgs e)
        {
            Log.writeMessage("POY PackSizeList_TextUpdate - Start : " + DateTime.Now);

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

            Log.writeMessage("POY PackSizeList_TextUpdate - End : " + DateTime.Now);
        }

        private void QualityList_SelectedIndexChanged(object sender, EventArgs e)
        {
            Log.writeMessage("POY QualityList_SelectedIndexChanged - Start : " + DateTime.Now);

            if (!isFormReady) return;

            if (QualityList.SelectedValue != null)
            {
                QualityResponse selectedQuality = (QualityResponse)QualityList.SelectedItem;
                int selectedQualityId = selectedQuality.QualityId;

                productionRequest.QualityId = selectedQualityId;
            }

            Log.writeMessage("POY QualityList_SelectedIndexChanged - End : " + DateTime.Now);
        }

        private void QualityList_TextUpdate(object sender, EventArgs e)
        {
            Log.writeMessage("POY QualityList_TextUpdate - Start : " + DateTime.Now);

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

            Log.writeMessage("POY QualityList_TextUpdate - End : " + DateTime.Now);
        }

        private void WindingTypeList_SelectedIndexChanged(object sender, EventArgs e)
        {
            Log.writeMessage("POY WindingTypeList_SelectedIndexChanged - Start : " + DateTime.Now);

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
                        RefreshWindingGrid();
                    }
                }
            }
            finally
            {
                lblLoading.Visible = false;
            }

            Log.writeMessage("POY WindingTypeList_SelectedIndexChanged - End : " + DateTime.Now);
        }

        private void WindingTypeList_TextUpdate(object sender, EventArgs e)
        {
            Log.writeMessage("POY WindingTypeList_TextUpdate - Start : " + DateTime.Now);

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

            Log.writeMessage("POY WindingTypeList_TextUpdate - End : " + DateTime.Now);
        }

        private async void SaleOrderList_SelectedIndexChanged(object sender, EventArgs e)
        {
            Log.writeMessage("POY SaleOrderList_SelectedIndexChanged - Start : " + DateTime.Now);

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
                        grdsoqty.Text = "";
                        var saleOrderItemResponse =_saleService.getSaleOrderItemById(selectedSaleOrderId).Result;
                        if (saleOrderItemResponse != null)
                        {
                            productionRequest.ContainerTypeId = saleOrderItemResponse.ContainerTypeId;
                            partyn.Text = saleOrderItemResponse.ItemDescription;
                            partyshade.Text = saleOrderItemResponse.ShadeNameDescription + "-" + saleOrderItemResponse.ShadeCodeDescription;
                        }

                        totalSOQty = selectedSaleOrder.Quantity;
                        grdsoqty.Text = totalSOQty.ToString("F2");

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

            Log.writeMessage("POY SaleOrderList_SelectedIndexChanged - End : " + DateTime.Now);
        }

        private void SaleOrderList_TextUpdate(object sender, EventArgs e)
        {
            Log.writeMessage("POY SaleOrderList_TextUpdate - Start : " + DateTime.Now);

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

            Log.writeMessage("POY SaleOrderList_TextUpdate - End : " + DateTime.Now);
        }

        private async void RefreshWindingGrid()
        {
            Log.writeMessage("POY RefreshWindingGrid - Start : " + DateTime.Now);

            if (productionRequest.WindingTypeId != 0)
            {
                //int selectedWindingTypeId = productionRequest.WindingTypeId;
                if (productionRequest.WindingTypeId > 0)
                {
                    var getProductionByWindingType = _packingService.getAllByLotIdandSaleOrderItemIdandPackingType(selectLotId, selectedSOId).Result;
                    List<WindingTypeGridResponse> windinggridList = new List<WindingTypeGridResponse>();

                    foreach (var winding in getProductionByWindingType)
                    {
                        var existing = windinggridList.FirstOrDefault(x => x.WindingTypeId == winding.WindingTypeId && x.SaleOrderItemsId == winding.SaleOrderItemsId);

                        if (existing == null)
                        {
                            WindingTypeGridResponse grid = new WindingTypeGridResponse();
                            grid.WindingTypeId = winding.WindingTypeId;
                            grid.SaleOrderItemsId = winding.SaleOrderItemsId;
                            grid.WindingTypeName = winding.WindingTypeName;
                            grid.SaleOrderQty = totalSOQty;
                            grid.GrossWt = winding.GrossWt;

                            windinggridList.Add(grid);
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
                    windinggrid.DataSource = windinggridList;
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
                this.lastbox.Text = getLastBox.BoxNoFmtd.ToString();
            }

            Log.writeMessage("POY RefreshLastBoxDetails - End : " + DateTime.Now);
        }

        private void ComPortList_SelectedIndexChanged(object sender, EventArgs e)
        {
            Log.writeMessage("POY ComPortList_SelectedIndexChanged - Start : " + DateTime.Now);

            if (!isFormReady) return;

            if (ComPortList.SelectedValue != null)
            {
                var ComPort = ComPortList.SelectedValue.ToString();
                comPort = ComPortList.SelectedValue.ToString();
            }

            Log.writeMessage("POY ComPortList_SelectedIndexChanged - End : " + DateTime.Now);
        }

        private void WeighingList_SelectedIndexChanged(object sender, EventArgs e)
        {
            Log.writeMessage("POY WeighingList_SelectedIndexChanged - Start : " + DateTime.Now);

            if (!isFormReady) return;

            if (WeighingList.SelectedValue != null)
            {
                WeighingItem selectedWeighingScale = (WeighingItem)WeighingList.SelectedItem;
                int selectedScaleId = selectedWeighingScale.Id;

                if (selectedScaleId >= 0 && !string.IsNullOrEmpty(comPort))
                {
                    var readWeight = wtReader.ReadWeight(comPort, selectedScaleId);
                    if (readWeight != null && (!string.IsNullOrEmpty(readWeight))) {
                        grosswtno.Text = readWeight.ToString();
                        grosswtno.ReadOnly = true;
                        grosswtno.BackColor = System.Drawing.SystemColors.ButtonHighlight;
                    }
                }
            }

            Log.writeMessage("POY WeighingList_SelectedIndexChanged - End : " + DateTime.Now);
        }

        private async void CopsItemList_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Log.writeMessage("POY CopsItemList_SelectionChangeCommitted - Start : " + DateTime.Now);

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

            Log.writeMessage("POY CopsItemList_SelectionChangeCommitted - End : " + DateTime.Now);
        }

        private void CopsItemList_TextUpdate(object sender, EventArgs e)
        {
            Log.writeMessage("POY CopsItemList_TextUpdate - Start : " + DateTime.Now);

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
            Log.writeMessage("POY CopsItemList_TextUpdate - End : " + DateTime.Now);
        }

        private async void BoxItemList_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Log.writeMessage("POY BoxItemList_SelectionChangeCommitted - Start : " + DateTime.Now);

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

            Log.writeMessage("POY BoxItemList_SelectionChangeCommitted - End : " + DateTime.Now);
        }

        private void BoxItemList_TextUpdate(object sender, EventArgs e)
        {
            Log.writeMessage("POY BoxItemList_TextUpdate - Start : " + DateTime.Now);

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
            Log.writeMessage("POY BoxItemList_TextUpdate - End : " + DateTime.Now);
        }

        private void PrefixList_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Log.writeMessage("POY PrefixList_SelectionChangeCommitted - Start : " + DateTime.Now);

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

                var deptTask = _masterService.GetDepartmentList("POY", selectedPrefix.Department).Result;
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
                    machineList = _masterService.GetMachineByDepartmentIdAndLotType(selectedDeptId, "SpinningLot").Result;

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

            Log.writeMessage("POY PrefixList_SelectionChangeCommitted - End : " + DateTime.Now);
        }

        private void PrefixList_TextUpdate(object sender, EventArgs e)
        {
            Log.writeMessage("POY PrefixList_TextUpdate - Start : " + DateTime.Now);

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
                prefixRequest.TxnFlag = "POY";
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
                PrefixList.SelectionLength = typedText.Length;
                PrefixList.SelectedIndex = -1;
                PrefixList.Text = typedText;
                PrefixList.SelectionStart = cursorPosition;
                PrefixList.TextUpdate += PrefixList_TextUpdate;

            }
            Log.writeMessage("POY PrefixList_TextUpdate - End : " + DateTime.Now);
        }

        private async void DeptList_SelectedIndexChanged(object sender, EventArgs e)
        {
            Log.writeMessage("POY DeptList_SelectedIndexChanged - Start : " + DateTime.Now);

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
                    //    var machineList = _masterService.GetMachineByDepartmentIdAndLotType(selectedDepartmentId, "SpinningLot").Result;   
                    //    machineList.Insert(0, new MachineResponse { MachineId = 0, MachineName = "Select Line No." });
                    //    LineNoList.DataSource = machineList;
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
                    //prefixRequest.TxnFlag = "POY";
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

            Log.writeMessage("POY DeptList_SelectedIndexChanged - Start : " + DateTime.Now);
        }

        private void DeptList_TextUpdate(object sender, EventArgs e)
        {
            Log.writeMessage("POY DeptList_TextUpdate - Start : " + DateTime.Now);

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

                var deptList = _masterService.GetDepartmentList("POY", typedText).Result.OrderBy(x => x.DepartmentName).ToList();

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
            Log.writeMessage("POY DeptList_TextUpdate - End : " + DateTime.Now);
        }

        private async void OwnerList_SelectedIndexChanged(object sender, EventArgs e)
        {
            Log.writeMessage("POY OwnerList_SelectedIndexChanged - Start : " + DateTime.Now);

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

            Log.writeMessage("POY OwnerList_SelectedIndexChanged - End : " + DateTime.Now);
        }

        private void OwnerList_TextUpdate(object sender, EventArgs e)
        {
            Log.writeMessage("POY OwnerList_TextUpdate - Start : " + DateTime.Now);

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
            Log.writeMessage("POY OwnerList_TextUpdate - End : " + DateTime.Now);
        }

        private void PalletTypeList_TextUpdate(object sender, EventArgs e)
        {
            Log.writeMessage("POY PalletTypeList_TextUpdate - Start : " + DateTime.Now);

            System.Windows.Forms.ComboBox cb = (System.Windows.Forms.ComboBox)sender;
            string typedText = cb.Text;

            if (string.IsNullOrWhiteSpace(cb.Text))
            {
                cb.TextUpdate -= PalletTypeList_TextUpdate;

                cb.SelectedIndex = 0;
                cb.Text = string.Empty;
                cb.DroppedDown = false;

                cb.TextUpdate += PalletTypeList_TextUpdate;
                return;
            }

            int cursorPosition = cb.SelectionStart;

            if (typedText.Length >= 2)
            {
                var palletitemList = _masterService.GetItemList(itemPalletCategoryId, typedText).Result.OrderBy(x => x.Name).ToList();

                palletitemList.Insert(0, new ItemResponse { ItemId = 0, Name = "Select Box/Pallet" });

                PalletTypeList.BeginUpdate();
                PalletTypeList.DataSource = null;
                PalletTypeList.DisplayMember = "Name";
                PalletTypeList.ValueMember = "ItemId";
                PalletTypeList.DataSource = palletitemList;
                PalletTypeList.EndUpdate();

                PalletTypeList.TextUpdate -= PalletTypeList_TextUpdate;
                PalletTypeList.DroppedDown = true;
                PalletTypeList.SelectionLength = typedText.Length;
                PalletTypeList.SelectedIndex = -1;
                PalletTypeList.Text = typedText;
                PalletTypeList.SelectionStart = cursorPosition;
                PalletTypeList.TextUpdate += PalletTypeList_TextUpdate;
            }
            Log.writeMessage("POY PalletTypeList_TextUpdate - End : " + DateTime.Now);
        }

        private async Task<List<string>> getComPortList()
        {
            Log.writeMessage("POY getComPortList - Start : " + DateTime.Now);

            var getComPortType = new List<string>
            {
                "Select Com Port",
                "COM1",
                "COM2",
                "COM3",
                "COM4"
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
                new WeighingItem { Id = 0, Name = "Old" },
                new WeighingItem { Id = 1, Name = "Unique" },
                new WeighingItem { Id = 2, Name = "JISL (9600)" },
                new WeighingItem { Id = 3, Name = "JISL (2400)" }
            };

            Log.writeMessage("POY getWeighingList - End : " + DateTime.Now);

            return getWeighingScale;
        }

        private int rowCount = 0; // Keeps track of SrNo
        private bool headerAdded = false; // To ensure header is added only once
        private int currentY = 35; // Start below header height
        private void addqty_Click(object sender, EventArgs e)
        {
            Log.writeMessage("POY addqty_Click - Start : " + DateTime.Now);

            // Validate item
            var selectedItem = PalletTypeList.SelectedItem as ItemResponse;
            if (selectedItem == null || selectedItem.ItemId == 0)
            {
                MessageBox.Show("Please select an item.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Validate quantity
            if (string.IsNullOrEmpty(qnty.Text))
            {
                MessageBox.Show("Please enter quantity.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); 
                return;
            }
            if (!int.TryParse(qnty.Text, out int quty) || quty < 0)
            {
                MessageBox.Show("Please enter a valid number for quantity.", "Invalid Quantity", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                qnty.Focus();
                return;
            }

            // Check range
            if (quty < 0)
            {
                MessageBox.Show("Quantity cannot be negative.", "Invalid Quantity", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                qnty.Focus();
                return;
            }
            else if (quty > int.MaxValue)
            {
                MessageBox.Show("Quantity cannot exceed 2,147,483,647.", "Limit Exceeded", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                qnty.Text = int.MaxValue.ToString();
                qnty.Focus();
                return;
            }
            int qty = Convert.ToInt32(qnty.Text);

            // Update record
            if (addqty.Text == "Update" && _editingPanel != null)
            {
                // Prevent duplicate item (except current row)
                bool duplicate = flowLayoutPanel1.Controls
                    .OfType<Panel>()
                    .Skip(1)
                    .Any(p =>
                        p != _editingPanel &&
                        p.Tag is Tuple<ItemResponse, System.Windows.Forms.Label> t &&
                        t.Item1.ItemId == selectedItem.ItemId);

                if (duplicate)
                {
                    MessageBox.Show("Item already added.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Get existing qty label
                var oldTag = (Tuple<ItemResponse, System.Windows.Forms.Label>)_editingPanel.Tag;
                System.Windows.Forms.Label qtyLabel = oldTag.Item2;

                // REPLACE tuple (Tuple is immutable)
                _editingPanel.Tag = new Tuple<ItemResponse, System.Windows.Forms.Label>(selectedItem, qtyLabel);

                // Update UI
                qtyLabel.Text = qty.ToString();

                // Update item name label
                var lblItem = _editingPanel.Controls
                                .OfType<System.Windows.Forms.Label>()
                                .FirstOrDefault(l => l.Name == "lblItemName");

                if (lblItem != null)
                {
                    lblItem.Text = selectedItem.Name;
                }

                var btnRemove = _editingPanel.Controls
                                .OfType<System.Windows.Forms.Button>()
                                .FirstOrDefault(b => b.Name == "btnRemove");

                if (btnRemove != null)
                    btnRemove.Enabled = true;

                // Reset UI
                addqty.Text = "Add";
                qnty.Clear();
                PalletTypeList.SelectedIndex = 0;
                PalletTypeList.Enabled = true;
                _editingPanel = null;

                Log.writeMessage("POY addqty_Click - End (Update)");
                return;
            }

            if (selectedItem.ItemId > 0)
            {
                // Check duplicate value
                //bool alreadyExists = flowLayoutPanel1.Controls
                //    .OfType<Panel>().Skip(1) // Skip header
                //    .Any(ctrl => {
                //        if (ctrl.Tag is Tuple<ItemResponse, int> tagData)
                //        {
                //            return tagData.Item1.ItemId == selectedItem.ItemId && tagData.Item2 == qty;
                //        }
                //        return false;
                //    });

                bool alreadyExists = flowLayoutPanel1.Controls
                                    .OfType<Panel>().Skip(1)
                                    .Any(p =>
                                            p.Tag is Tuple<ItemResponse, System.Windows.Forms.Label> t &&
                                            t.Item1.ItemId == selectedItem.ItemId);

                if (alreadyExists)
                {
                    MessageBox.Show("Item already added.",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                //var existingPanel = flowLayoutPanel1.Controls
                //    .OfType<Panel>()
                //    .Skip(1) // Skip header
                //    .FirstOrDefault(ctrl =>
                //        ctrl.Tag is Tuple<ItemResponse, int> tag &&
                //        tag.Item1.ItemId == selectedItem.ItemId);

                //if (existingPanel != null)
                //{
                //    var tag = (Tuple<ItemResponse, System.Windows.Forms.Label>)existingPanel.Tag;
                //    tag.Item2.Text = qty.ToString();
                //    //MessageBox.Show("Item quantity updated.");
                //    foreach (var control in existingPanel.Controls.OfType<System.Windows.Forms.Button>())
                //    {
                //        if (control.Text == "Remove")
                //        {
                //            control.Enabled = true;
                //        }
                //    }

                //    addqty.Text = "Add"; // reset button text back to Add
                //    qnty.Text = "";
                //    PalletTypeList.SelectedIndex = 0;
                //    PalletTypeList.Enabled = true;
                //    return;
                //}

                if (!alreadyExists)
                {
                    rowCount++;

                    Panel rowPanel = new Panel();
                    rowPanel.Size = new Size(width, 35);
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
                    System.Windows.Forms.Label lblQty = new System.Windows.Forms.Label() { Text = qty.ToString(), Width = 60, Location = new System.Drawing.Point(260, 10), Font = FontManager.GetFont(8F, FontStyle.Regular) };

                    // Edit Button
                    System.Windows.Forms.Button btnEdit = new System.Windows.Forms.Button() { Text = "Edit", Size = new Size(35, 23), Location = new System.Drawing.Point(320, 5), Font = FontManager.GetFont(7F, FontStyle.Regular), BackColor = Color.FromArgb(230, 240, 255), ForeColor = Color.FromArgb(51, 133, 255), Tag = new Tuple<ItemResponse, System.Windows.Forms.Label>(selectedItem, lblQty), FlatStyle = FlatStyle.Flat };
                    btnEdit.FlatAppearance.BorderColor = Color.FromArgb(51, 133, 255);
                    btnEdit.FlatAppearance.BorderSize = 1;  
                    btnEdit.FlatAppearance.MouseOverBackColor = Color.FromArgb(210, 230, 255); 
                    btnEdit.FlatAppearance.MouseDownBackColor = Color.FromArgb(180, 210, 255);
                    btnEdit.FlatAppearance.BorderSize = 0;
                    btnEdit.TabIndex = 4;
                    btnEdit.TabStop = true;
                    btnEdit.Cursor = Cursors.Hand;
                    btnEdit.Paint += (s, f) =>
                    {
                        var rect = new Rectangle(0, 0, btnEdit.Width - 1, btnEdit.Height - 1);

                        using (GraphicsPath path = _cmethod.GetRoundedRect(rect, 4)) // radius = 4
                        using (Pen borderPen = new Pen(btnEdit.FlatAppearance.BorderColor, btnEdit.FlatAppearance.BorderSize))
                        using (SolidBrush brush = new SolidBrush(btnEdit.BackColor))
                        {
                            f.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

                            f.Graphics.FillPath(brush, path);

                            f.Graphics.DrawPath(borderPen, path);

                            if (btnEdit.Focused)
                            {
                                ControlPaint.DrawFocusRectangle(f.Graphics, rect);
                            }

                            TextRenderer.DrawText(
                                f.Graphics,
                                btnEdit.Text,
                                btnEdit.Font,
                                rect,
                                btnEdit.ForeColor,
                                TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter
                            );
                        }
                    };
                    btnEdit.Click += editPallet_Click;

                    // Delete Button
                    System.Windows.Forms.Button btnDelete = new System.Windows.Forms.Button() { Name = "btnRemove", Text = "Remove", Size = new Size(50, 23), Location = new System.Drawing.Point(360, 5), Font = FontManager.GetFont(7F, FontStyle.Regular), BackColor = Color.FromArgb(255, 230, 230), ForeColor = Color.FromArgb(255, 51, 51), Tag = rowPanel, FlatStyle = FlatStyle.Flat };
                    btnDelete.FlatAppearance.BorderColor = Color.FromArgb(255, 51, 51);
                    btnDelete.FlatAppearance.BorderSize = 1;   
                    btnDelete.FlatAppearance.MouseOverBackColor = Color.FromArgb(255, 204, 204);
                    btnDelete.FlatAppearance.MouseDownBackColor = Color.FromArgb(255, 230, 230); 
                    btnDelete.FlatAppearance.BorderSize = 0;
                    btnDelete.TabIndex = 5;
                    btnDelete.TabStop = true;
                    btnDelete.Cursor = Cursors.Hand;
                    btnDelete.Paint += (s, f) =>
                    {
                        var button = (System.Windows.Forms.Button)s;
                        var rect = new Rectangle(0, 0, button.Width - 1, button.Height - 1);

                        // button color change for enabled/disabled
                        Color backColor = button.Enabled ? button.BackColor : Color.LightGray;
                        Color borderColor = button.Enabled ? button.FlatAppearance.BorderColor : Color.Gray;
                        Color foreColor = button.Enabled ? button.ForeColor : Color.DarkGray;

                        using (GraphicsPath path = _cmethod.GetRoundedRect(rect, 4))
                        using (Pen borderPen = new Pen(borderColor, button.FlatAppearance.BorderSize))
                        using (SolidBrush brush = new SolidBrush(backColor))
                        {
                            f.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                            f.Graphics.FillPath(brush, path);
                            f.Graphics.DrawPath(borderPen, path);

                            if (btnDelete.Focused)
                            {
                                ControlPaint.DrawFocusRectangle(f.Graphics, rect);
                            }

                            TextRenderer.DrawText(
                                f.Graphics,
                                button.Text,
                                button.Font,
                                rect,
                                foreColor,
                                TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter
                            );
                        }
                    };
                    // Remove Row
                    btnDelete.Click += (s, args) =>
                    {
                        flowLayoutPanel1.Controls.Remove(rowPanel);
                        ReorderSrNo();
                    };

                    rowPanel.Controls.Add(lblSrNo);
                    rowPanel.Controls.Add(lblItem);
                    rowPanel.Controls.Add(lblQty);
                    rowPanel.Controls.Add(btnEdit);
                    rowPanel.Controls.Add(btnDelete);
                    rowPanel.Tag = new Tuple<ItemResponse, System.Windows.Forms.Label>(selectedItem, lblQty);
                    //if itemname is larger then increase the rowPanel height and change its location point
                    int rowHeight = Math.Max(lblItem.Height + 10, 35);
                    rowPanel.Size = new Size(width, rowHeight);
                    int newY = (rowPanel.Height - lblItem.Height) / 2;
                    lblItem.Location = new System.Drawing.Point(lblItem.Location.X, newY);

                    flowLayoutPanel1.Controls.Add(rowPanel);
                    flowLayoutPanel1.AutoScroll = true;
                    flowLayoutPanel1.WrapContents = false;
                    flowLayoutPanel1.FlowDirection = FlowDirection.TopDown;
                    addqty.Text = "Add";

                    qnty.Text = "";
                    PalletTypeList.SelectedIndex = 0;
                    PalletTypeList.Enabled = true;
                    PalletTypeList.Focus();
                }
                else
                {
                    MessageBox.Show("Item already added.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please select an item.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }

            Log.writeMessage("POY addqty_Click - End : " + DateTime.Now);
        }

        private void ReorderSrNo()
        {
            Log.writeMessage("POY ReorderSrNo - Start : " + DateTime.Now);

            int srNo = 1;
            int y = 35;

            foreach (Panel row in flowLayoutPanel1.Controls.OfType<Panel>().Skip(1))
            {
                row.Location = new System.Drawing.Point(0, y);
                var lbl = row.Controls.OfType<System.Windows.Forms.Label>().FirstOrDefault();
                if (lbl != null)
                    lbl.Text = srNo.ToString();

                y += row.Height;
                srNo++;
            }

            currentY = y; // Reset currentY for next added row
            rowCount = srNo - 1;
            PalletTypeList.Focus();

            Log.writeMessage("POY ReorderSrNo - End : " + DateTime.Now);
        }

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
            headerPanel.Controls.Add(new System.Windows.Forms.Label() { Text = "Action", Width = 120, Location = new System.Drawing.Point(350, 10), Font = FontManager.GetFont(7F, FontStyle.Bold) });

            flowLayoutPanel1.Controls.Add(headerPanel);
            headerAdded = true;

            Log.writeMessage("POY AddHeader - End : " + DateTime.Now);
        }

        //private void editPallet_Click(object sender, EventArgs e)
        //{
        //    Log.writeMessage("POY editPallet_Click - Start : " + DateTime.Now);

        //    var btn = sender as System.Windows.Forms.Button;
        //    var data = btn.Tag as Tuple<ItemResponse, System.Windows.Forms.Label>;

        //    if (data != null)
        //    {
        //        ItemResponse item = data.Item1;
        //        int quantity = Convert.ToInt32(data.Item2.Text);

        //        PalletTypeList.DataSource = null;
        //        PalletTypeList.Items.Clear();
        //        PalletTypeList.Items.Add(new ItemResponse { ItemId = 0, Name = "Select Box/Pallet" });
        //        PalletTypeList.Items.Add(item);
        //        PalletTypeList.DisplayMember = "Name";
        //        PalletTypeList.ValueMember = "ItemId";
        //        PalletTypeList.SelectedIndex = 1;
        //        //PalletTypeList.Enabled = false;
        //        //foreach (ItemResponse entry in PalletTypeList.Items)
        //        //{
        //        //    if (entry.ItemId == item.ItemId)
        //        //    {
        //        //        PalletTypeList.SelectedItem = entry;
        //        //        break;
        //        //    }
        //        //}

        //        qnty.Text = quantity.ToString();
        //        addqty.Text = "Update";

        //        //disable remove button when edit row
        //        //var rowPanel = btn.Parent as Panel;
        //        //if (rowPanel != null)
        //        //{
        //        //    foreach (var control in rowPanel.Controls.OfType<System.Windows.Forms.Button>())
        //        //    {
        //        //        if (control.Text == "Remove")
        //        //        {
        //        //            control.Enabled = false;
        //        //            control.Paint += (s, f) =>
        //        //            {
        //        //                var button = (System.Windows.Forms.Button)s;
        //        //                var rect = new Rectangle(0, 0, button.Width - 1, button.Height - 1);

        //        //                // button color change for enabled/disabled
        //        //                Color backColor = button.Enabled ? button.BackColor : Color.LightGray;
        //        //                Color borderColor = button.Enabled ? button.FlatAppearance.BorderColor : Color.Gray;
        //        //                Color foreColor = button.Enabled ? button.ForeColor : Color.Gray;

        //        //                using (GraphicsPath path = _cmethod.GetRoundedRect(rect, 4))
        //        //                using (Pen borderPen = new Pen(borderColor, button.FlatAppearance.BorderSize))
        //        //                using (SolidBrush brush = new SolidBrush(backColor))
        //        //                {
        //        //                    f.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
        //        //                    f.Graphics.FillPath(brush, path);
        //        //                    f.Graphics.DrawPath(borderPen, path);

        //        //                    if (control.Focused)
        //        //                    {
        //        //                        ControlPaint.DrawFocusRectangle(f.Graphics, rect);
        //        //                    }

        //        //                    TextRenderer.DrawText(
        //        //                        f.Graphics,
        //        //                        button.Text,
        //        //                        button.Font,
        //        //                        rect,
        //        //                        foreColor,
        //        //                        TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter
        //        //                    );
        //        //                }
        //        //            };
        //        //        }
        //        //    }
        //        //}
        //        qnty.Focus();
        //    }

        //    Log.writeMessage("POY editPallet_Click - End : " + DateTime.Now);
        //}

        private void editPallet_Click(object sender, EventArgs e)
        {
            Log.writeMessage("POY editPallet_Click - Start : " + DateTime.Now);

            var btn = sender as System.Windows.Forms.Button;
            if (btn == null) return;

            Panel rowPanel = btn.Parent as Panel;
            if (rowPanel == null) return;

            _editingPanel = rowPanel;

            // Disable remove button ONLY for this row
            var btnRemove = rowPanel.Controls
                .OfType<System.Windows.Forms.Button>()
                .FirstOrDefault(b => b.Name == "btnRemove");

            if (btnRemove != null)
                btnRemove.Enabled = false;

            var tag = rowPanel.Tag as Tuple<ItemResponse, System.Windows.Forms.Label>;
            var selectedItem = tag.Item1;

            PalletTypeList.DataSource = null; 
            PalletTypeList.Items.Clear(); 
            PalletTypeList.Items.Add(new ItemResponse { ItemId = 0, Name = "Select Box/Pallet" }); 
            PalletTypeList.Items.Add(selectedItem); 
            PalletTypeList.DisplayMember = "Name"; 
            PalletTypeList.ValueMember = "ItemId";
            PalletTypeList.SelectedItem = PalletTypeList.Items.Cast<ItemResponse>().FirstOrDefault(i => i.ItemId == selectedItem.ItemId);
            qnty.Text = tag.Item2.Text;

            addqty.Text = "Update";
            PalletTypeList.Enabled = true;
            PalletTypeList.Focus();
            Log.writeMessage("POY editPallet_Click - End : " + DateTime.Now);
        }

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

        private void GrossWeight_Validating(object sender, CancelEventArgs e)
        {
            Log.writeMessage("POY GrossWeight_Validating - Start : " + DateTime.Now);

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
            else {
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

        private async void submit_Click(object sender, EventArgs e)
        {
            Log.writeMessage("POY submit_Click - Start : " + DateTime.Now);

            submitForm(false);

            Log.writeMessage("POY submit_Click - End : " + DateTime.Now);
        }

        private async void saveprint_Click(object sender, EventArgs e)
        {
            Log.writeMessage("POY saveprint_Click - Start : " + DateTime.Now);

            submitForm(true);

            Log.writeMessage("POY saveprint_Click - End : " + DateTime.Now);
        }

        public async void submitForm(bool isPrint)
        {
            Log.writeMessage("POY submitForm - Start : " + DateTime.Now);

            if (ValidateForm())
            {
                productionRequest.OwnerId = this.OwnerList.SelectedIndex <= 0 ? 0 : productionRequest.OwnerId;
                productionRequest.PackingType = "POYPacking";
                productionRequest.Remarks = remarks.Text.Trim();
                productionRequest.Spools = Convert.ToInt32(spoolno.Text.Trim());
                productionRequest.SpoolsWt = Convert.ToDecimal(spoolwt.Text.Trim());
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

            Log.writeMessage("POY submitForm - End : " + DateTime.Now);
        }

        public ProductionResponse SubmitPacking(ProductionRequest productionRequest, bool isPrint)
        {
            Log.writeMessage("POY SubmitPacking - Start : " + DateTime.Now);

            submit.Enabled = false;
            saveprint.Enabled = false;
            ProductionResponse result = new ProductionResponse();
            result = _packingService.AddUpdatePOYPacking(0, productionRequest);
            if (result != null && result.ProductionId > 0)
            {
                slipRequest.ProductionId = result.ProductionId;
                submit.Enabled = true;
                saveprint.Enabled = true;
                ShowCustomMessage(result.BoxNoFmtd);
                RefreshWindingGrid();
                RefreshGradewiseGrid();
                RefreshLastBoxDetails();

                isFormReady = false;
                this.spoolno.Text = "0";
                this.spoolwt.Text = "0";
                this.grosswtno.Text = "0.000";
                this.tarewt.Text = "0.000";
                this.netwt.Text = "0.000";
                this.wtpercop.Text = "0.000";
                palletwtno.Text = boxpalletitemwt.Text;
                isFormReady = true;
                this.spoolno.Focus();
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

            Log.writeMessage("POY SubmitPacking - End : " + DateTime.Now);

            return result;
        }

        private bool ValidateForm()
        {
            Log.writeMessage("POY ValidateForm - Start : " + DateTime.Now);

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

            if (flowLayoutPanel1.Controls.Count == 1) 
            {
                MessageBox.Show("Please add atleast one record in Pallet details", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

            Log.writeMessage("POY ValidateForm - End : " + DateTime.Now);

            return isValid;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Log.writeMessage("POY btnCancel_Click - Start : " + DateTime.Now);

            ResetForm(this);

            Log.writeMessage("POY btnCancel_Click - End : " + DateTime.Now);
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

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            Log.writeMessage("POY textBox1_KeyPress - Start : " + DateTime.Now);

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

            Log.writeMessage("POY textBox1_KeyPress - End : " + DateTime.Now);
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            Log.writeMessage("POY textBox1_KeyDown - Start : " + DateTime.Now);

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

            Log.writeMessage("POY textBox1_KeyDown - End : " + DateTime.Now);
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            Log.writeMessage("POY textBox1_Enter - Start : " + DateTime.Now);

            System.Windows.Forms.TextBox tb = sender as System.Windows.Forms.TextBox;

            if (!string.IsNullOrEmpty(tb.Text))
                tb.SelectAll();

            Log.writeMessage("POY textBox1_Enter - End : " + DateTime.Now);
        }

        private void palletQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            Log.writeMessage("POY palletQty_KeyPress - Start : " + DateTime.Now);

            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Reject the input
            }

            Log.writeMessage("POY palletQty_KeyPress - End : " + DateTime.Now);
        }

        private void checkBox1_KeyDown(object sender, KeyEventArgs e)
        {
            Log.writeMessage("POY checkBox1_KeyDown - Start : " + DateTime.Now);

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

            Log.writeMessage("POY checkBox1_KeyDown - End : " + DateTime.Now);
        }

        private void LineNoList_KeyDown(object sender, KeyEventArgs e)
        {
            Log.writeMessage("POY LineNoList_KeyDown - Start : " + DateTime.Now);

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
                var machineList = _masterService.GetMachineList("SpinningLot", "").Result.OrderBy(x => x.MachineName).ToList();
                machineList.Insert(0, new MachineResponse { MachineId = 0, MachineName = "Select Line No." });
                LineNoList.DataSource = machineList;
                LineNoList.DisplayMember = "MachineName";
                LineNoList.ValueMember = "MachineId";
                LineNoList.SelectedIndex = 0;
                LineNoList.DroppedDown = true; // Open the dropdown list
                e.SuppressKeyPress = true;    // Prevent any side effect
            }

            Log.writeMessage("POY LineNoList_KeyDown - End : " + DateTime.Now);
        }

        private void MergeNoList_KeyDown(object sender, KeyEventArgs e)
        {
            Log.writeMessage("POY MergeNoList_KeyDown - Start : " + DateTime.Now);

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
                var mergenoList = _productionService.getLotsByLotType("SpinningLot", "").Result.OrderBy(x => x.LotNoFrmt).ToList();
                mergenoList.Insert(0, new LotsResponse { LotId = 0, LotNoFrmt = "Select MergeNo" });
                MergeNoList.DisplayMember = "LotNoFrmt";
                MergeNoList.ValueMember = "LotId";
                MergeNoList.DataSource = mergenoList;
                MergeNoList.SelectedIndex = 0;
                MergeNoList.DroppedDown = true; // Open the dropdown list
                e.SuppressKeyPress = true;    // Prevent any side effect
            }

            Log.writeMessage("POY MergeNoList_KeyDown - End : " + DateTime.Now);
        }

        private void PackSizeList_KeyDown(object sender, KeyEventArgs e)
        {
            Log.writeMessage("POY PackSizeList_KeyDown - Start : " + DateTime.Now);

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

            Log.writeMessage("POY PackSizeList_KeyDown - End : " + DateTime.Now);
        }

        private void QualityList_KeyDown(object sender, KeyEventArgs e)
        {
            Log.writeMessage("POY QualityList_KeyDown - Start : " + DateTime.Now);

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

            Log.writeMessage("POY QualityList_KeyDown - End : " + DateTime.Now);
        }

        private void SaleOrderList_KeyDown(object sender, KeyEventArgs e)
        {
            Log.writeMessage("POY SaleOrderList_KeyDown - Start : " + DateTime.Now);

            if (e.KeyCode == Keys.ShiftKey) // Detect Shift key
            {
                SaleOrderList.DroppedDown = true; // Open the dropdown list
                e.SuppressKeyPress = true;    // Prevent any side effect
            }
            if (e.KeyCode == Keys.Escape)
            {
                SaleOrderList.DroppedDown = false;
            }

            Log.writeMessage("POY SaleOrderList_KeyDown - End : " + DateTime.Now);
        }

        private void PrefixList_KeyDown(object sender, KeyEventArgs e)
        {
            Log.writeMessage("POY PrefixList_KeyDown - Start : " + DateTime.Now);

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
                prefixRequest.TxnFlag = "poy";
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

            Log.writeMessage("POY PrefixList_KeyDown - End : " + DateTime.Now);
        }

        private void WindingTypeList_KeyDown(object sender, KeyEventArgs e)
        {
            Log.writeMessage("POY WindingTypeList_KeyDown - Start : " + DateTime.Now);

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

            Log.writeMessage("POY WindingTypeList_KeyDown - End : " + DateTime.Now);
        }

        private void ComPortList_KeyDown(object sender, KeyEventArgs e)
        {
            Log.writeMessage("POY ComPortList_KeyDown - Start : " + DateTime.Now);

            if (e.KeyCode == Keys.ShiftKey) // Detect Shift key
            {
                ComPortList.DroppedDown = true; // Open the dropdown list
                e.SuppressKeyPress = true;    // Prevent any side effect
            }
            if (e.KeyCode == Keys.Escape)
            {
                ComPortList.DroppedDown = false;
            }

            Log.writeMessage("POY ComPortList_KeyDown - End : " + DateTime.Now);
        }

        private void WeighingList_KeyDown(object sender, KeyEventArgs e)
        {
            Log.writeMessage("POY WeighingList_KeyDown - Start : " + DateTime.Now);

            if (e.KeyCode == Keys.ShiftKey) // Detect Shift key
            {
                WeighingList.DroppedDown = true; // Open the dropdown list
                e.SuppressKeyPress = true;    // Prevent any side effect
            }
            if (e.KeyCode == Keys.Escape)
            {
                WeighingList.DroppedDown = false;
            }

            Log.writeMessage("POY WeighingList_KeyDown - End : " + DateTime.Now);
        }

        private void CopsItemList_KeyDown(object sender, KeyEventArgs e)
        {
            Log.writeMessage("POY CopsItemList_KeyDown - Start : " + DateTime.Now);

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

            Log.writeMessage("POY CopsItemList_KeyDown - End : " + DateTime.Now);
        }

        private void BoxItemList_KeyDown(object sender, KeyEventArgs e)
        {
            Log.writeMessage("POY BoxItemList_KeyDown - Start : " + DateTime.Now);

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

            Log.writeMessage("POY BoxItemList_KeyDown - End : " + DateTime.Now);
        }

        private void PalletTypeList_KeyDown(object sender, KeyEventArgs e)
        {
            Log.writeMessage("POY PalletTypeList_KeyDown - Start : " + DateTime.Now);

            if (e.KeyCode == Keys.ShiftKey) // Detect Shift key
            {
                PalletTypeList.DroppedDown = true; // Open the dropdown list
                e.SuppressKeyPress = true;    // Prevent any side effect
            }
            if (e.KeyCode == Keys.Escape)
            {
                PalletTypeList.DroppedDown = false;
            }
            if (e.KeyCode == Keys.F2) // Detect F2 key
            {
                PalletTypeList.DataSource = null;
                var palletitemList = _masterService.GetItemList(itemPalletCategoryId, "").Result.OrderBy(x => x.Name).ToList();
                palletitemList.Insert(0, new ItemResponse { ItemId = 0, Name = "Select Box/Pallet" });
                PalletTypeList.DisplayMember = "Name";
                PalletTypeList.ValueMember = "ItemId";
                PalletTypeList.DataSource = palletitemList;
                PalletTypeList.SelectedIndex = 0;
                PalletTypeList.DroppedDown = true; // Open the dropdown list
                e.SuppressKeyPress = true;    // Prevent any side effect
            }

            Log.writeMessage("POY PalletTypeList_KeyDown - End : " + DateTime.Now);
        }

        private void DeptList_KeyDown(object sender, KeyEventArgs e)
        {
            Log.writeMessage("POY DeptList_KeyDown - Start : " + DateTime.Now);

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
                var deptList = _masterService.GetDepartmentList("POY", "").Result.OrderBy(x => x.DepartmentName).ToList();
                deptList.Insert(0, new DepartmentResponse { DepartmentId = 0, DepartmentName = "Select Dept" });
                DeptList.DisplayMember = "DepartmentName";
                DeptList.ValueMember = "DepartmentId";
                DeptList.DataSource = deptList;
                DeptList.SelectedIndex = 0;
                DeptList.DroppedDown = true; // Open the dropdown list
                e.SuppressKeyPress = true;    // Prevent any side effect
            }

            Log.writeMessage("POY DeptList_KeyDown - End : " + DateTime.Now);
        }

        private void OwnerList_KeyDown(object sender, KeyEventArgs e)
        {
            Log.writeMessage("POY OwnerList_KeyDown - Start : " + DateTime.Now);

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

            Log.writeMessage("POY OwnerList_KeyDown - End : " + DateTime.Now);
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

                //CopsItemList.SelectedIndex = 0;

                //BoxItemList.SelectedIndex = 0;

                //ComPortList.SelectedIndex = 0;

                //WeighingList.SelectedIndex = 0;

                //OwnerList.SelectedIndex = 0;

                //PrefixList.SelectedIndex = 0;

                //isFormReady = false;
                spoolno.Text = "0";
                salelotvalue.Text = "";
                lastbox.Text = "";
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

        private void prcompany_CheckedChanged(object sender, EventArgs e)
        {
            Log.writeMessage("POY prcompany_CheckedChanged - Start : " + DateTime.Now);

            if (!isFormReady) return;

            if (prcompany.Checked)
            {
                prowner.Checked = false;
                prcompany.Focus();       // keep focus on the current one
            }

            Log.writeMessage("POY prcompany_CheckedChanged - End : " + DateTime.Now);
        }

        private void prowner_CheckedChanged(object sender, EventArgs e)
        {
            Log.writeMessage("POY prowner_CheckedChanged - Start : " + DateTime.Now);

            if (!isFormReady) return;

            if (prowner.Checked)
            {
                prcompany.Checked = false;
                prowner.Focus();           // keep focus
            }

            Log.writeMessage("POY prowner_CheckedChanged - End : " + DateTime.Now);
        }

        private void txtNumeric_KeyPress(object sender, KeyPressEventArgs e)
        {
            Log.writeMessage("POY txtNumeric_KeyPress - Start : " + DateTime.Now);

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

            Log.writeMessage("POY txtNumeric_KeyPress - End : " + DateTime.Now);
        }

        private void Control_EnterKeyMoveNext(object sender, KeyEventArgs e)
        {
            Log.writeMessage("POY Control_EnterKeyMoveNext - Start : " + DateTime.Now);

            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; 

                Control current = (Control)sender;

                if (current == grosswtno) 
                {
                    saveprint.Focus();
                    saveprint.Paint += _cmethod.Button_Paint;
                }
                else
                {
                    this.SelectNextControl(current, true, true, true, true);
                }
            }

            Log.writeMessage("POY Control_EnterKeyMoveNext - End : " + DateTime.Now);
        }

        private void spoolNo_Enter(object sender, EventArgs e)
        {
            Log.writeMessage("POY spoolNo_Enter - Start : " + DateTime.Now);

            // When control gets focus
            if (spoolno.Text == "0")
            {
                spoolno.Clear(); // remove the default value
            }
            else
            {
                ((System.Windows.Forms.TextBox)sender).SelectAll();
            }

            Log.writeMessage("POY spoolNo_Enter - End : " + DateTime.Now);
        }

        private void spoolNo_Leave(object sender, EventArgs e)
        {
            Log.writeMessage("POY spoolNo_Leave - Start : " + DateTime.Now);

            // When control loses focus
            if (string.IsNullOrWhiteSpace(spoolno.Text))
            {
                spoolno.Text = "0"; // restore default
            }

            Log.writeMessage("POY spoolNo_Leave - End : " + DateTime.Now);
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
                    Text = $"POY Packing added successfully for BoxNo {boxNo}.",
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

        private void ComboBox_Leave(object sender, EventArgs e)
        {
            Log.writeMessage("POY ComboBox_Leave - Start : " + DateTime.Now);

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

            Log.writeMessage("POY ComboBox_Leave - End : " + DateTime.Now);
        }

        private void txtNumeric_Leave(object sender, EventArgs e)
        {
            Log.writeMessage("POY txtNumeric_Leave - Start : " + DateTime.Now);

            FormatToThreeDecimalPlaces(sender as System.Windows.Forms.TextBox);

            Log.writeMessage("POY txtNumeric_Leave - End : " + DateTime.Now);
        }

        private void FormatToThreeDecimalPlaces(System.Windows.Forms.TextBox textBox)
        {
            Log.writeMessage("POY FormatToThreeDecimalPlaces - Start : " + DateTime.Now);

            if (decimal.TryParse(textBox.Text, out decimal value))
                textBox.Text = value.ToString("0.000");
            else
                textBox.Text = "0.000"; // optional fallback

            Log.writeMessage("POY FormatToThreeDecimalPlaces - End : " + DateTime.Now);
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

        private void ResetDependentDropdownValues()
        {
            Log.writeMessage("POY ResetDependentDropdownValues - Start : " + DateTime.Now);

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

            Log.writeMessage("POY ResetDependentDropdownValues - End : " + DateTime.Now);
        }
    }
}
