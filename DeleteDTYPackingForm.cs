using Microsoft.ReportingServices.ReportProcessing.ReportObjectModel;
using PackingApplication.Helper;
using PackingApplication.Models.CommonEntities;
using PackingApplication.Models.RequestEntities;
using PackingApplication.Models.ResponseEntities;
using PackingApplication.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace PackingApplication
{
    public partial class DeleteDTYPackingForm : Form
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
        List<MachineResponse> o_machinesResponse = new List<MachineResponse>();
        List<DepartmentResponse> o_departmentResponses = new List<DepartmentResponse>();
        TransactionTypePrefixRequest prefixRequest = new TransactionTypePrefixRequest();
        bool suppressEvents = false;
        List<ProductionResponse> packingList = new List<ProductionResponse>();
        int selectedSrDeptId = 0;
        int selectedSrMachineId = 0;
        string selectedSrBoxNo = null;
        string selectedSrProductionDate = null;
        public DeleteDTYPackingForm()
        {
            InitializeComponent();
            ApplyFonts();
            //this.Shown += DeleteDTYPackingForm_Shown;
            this.AutoScroll = true;
            lblLoading = CommonMethod.InitializeLoadingLabel(this);

            _cmethod.SetButtonBorderRadius(this.cancelbtn, 8);
            _cmethod.SetButtonBorderRadius(this.delete, 8);
            _cmethod.SetButtonBorderRadius(this.findbtn, 8);
            _cmethod.SetButtonBorderRadius(this.closepopupbtn, 8);
            _cmethod.SetButtonBorderRadius(this.searchbtn, 8);
            _cmethod.SetButtonBorderRadius(this.closelistbtn, 8);

            rowMaterial.AutoGenerateColumns = false;
        }

        private void DeleteDTYPackingForm_Load(object sender, EventArgs e)
        {
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
            //twistvalue.Text = "0";
            //partyn.Text = "";
            //partyshade.Text = "";
            isFormReady = true;
            //this.reportViewer1.RefreshReport();
            selectedSrProductionDate = dateTimePicker2.Value.ToString("dd-MM-yyyy");

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
        }

        private void LoadDropdowns()
        {
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
        }

        private void ApplyFonts()
        {
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
            this.boxnoerror.Font = FontManager.GetFont(7F, FontStyle.Regular);
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
            this.srlinenoradiobtn.Font = FontManager.GetFont(8F, FontStyle.Bold);
            this.SrLineNoList.Font = FontManager.GetFont(8F, FontStyle.Regular);
            this.srdeptradiobtn.Font = FontManager.GetFont(8F, FontStyle.Bold);
            this.SrDeptList.Font = FontManager.GetFont(8F, FontStyle.Regular);
            this.srboxnoradiobtn.Font = FontManager.GetFont(8F, FontStyle.Bold);
            this.SrBoxNoList.Font = FontManager.GetFont(8F, FontStyle.Regular);
            this.srproddateradiobtn.Font = FontManager.GetFont(8F, FontStyle.Bold);
            this.dateTimePicker2.Font = FontManager.GetFont(8F, FontStyle.Regular);
            this.closelistbtn.Font = FontManager.GetFont(8F, FontStyle.Bold);
        }

        //private async void DeleteDTYPackingForm_Shown(object sender, EventArgs e)
        //{
        //    try
        //    {
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

                LineNoList.DataSource = null;
                LineNoList.Items.Clear();
                LineNoList.Items.Add("Select Line No.");
                LineNoList.Items.Add(productionResponse.MachineName);
                LineNoList.SelectedItem = productionResponse.MachineName;

                DeptList.DataSource = null;
                DeptList.Items.Clear();
                DeptList.Items.Add("Select Dept");
                DeptList.Items.Add(productionResponse.DepartmentName);
                DeptList.SelectedItem = productionResponse.DepartmentName;

                MergeNoList.DataSource = null;
                MergeNoList.Items.Clear();
                MergeNoList.Items.Add("Select MergeNo");
                MergeNoList.Items.Add(productionResponse.LotNo);
                MergeNoList.SelectedItem = productionResponse.LotNo;

                SaleOrderList.DataSource = null;
                SaleOrderList.Items.Clear();
                SaleOrderList.Items.Add("Select Sale Order Item");
                var salesOrderNumber = "";
                salesOrderNumber = productionResponse.SalesOrderNumber + "--" + productionResponse.SOItemName + "--" + productionResponse.ShadeName + "--" + productionResponse.SOQuantity;
                SaleOrderList.Items.Add(salesOrderNumber);
                SaleOrderList.SelectedItem = salesOrderNumber;

                QualityList.DataSource = null;
                QualityList.Items.Clear();
                QualityList.Items.Add("Select Quality");
                QualityList.Items.Add(productionResponse.QualityName);
                QualityList.SelectedItem = productionResponse.QualityName;

                WindingTypeList.DataSource = null;
                WindingTypeList.Items.Clear();
                WindingTypeList.Items.Add("Select Winding Type");
                WindingTypeList.Items.Add(productionResponse.WindingTypeName);
                WindingTypeList.SelectedItem = productionResponse.WindingTypeName;

                PackSizeList.DataSource = null;
                PackSizeList.Items.Clear();
                PackSizeList.Items.Add("Select Pack Size");
                PackSizeList.Items.Add(productionResponse.PackSizeName);
                PackSizeList.SelectedItem = productionResponse.PackSizeName;

                CopsItemList.DataSource = null;
                CopsItemList.Items.Clear();
                CopsItemList.Items.Add("Select Cops Item");
                CopsItemList.Items.Add(productionResponse.SpoolItemName);
                CopsItemList.SelectedItem = productionResponse.SpoolItemName;

                BoxItemList.DataSource = null;
                BoxItemList.Items.Clear();
                BoxItemList.Items.Add("Select Box/Pallet");
                BoxItemList.Items.Add(productionResponse.BoxItemName);
                BoxItemList.SelectedItem = productionResponse.BoxItemName;

                OwnerList.DataSource = null;
                OwnerList.Items.Clear();
                OwnerList.Items.Add("Select Owner");
                if (!string.IsNullOrEmpty(productionResponse.OwnerName))
                {
                    OwnerList.Items.Add(productionResponse.OwnerName);
                    OwnerList.SelectedItem = productionResponse.OwnerName;
                }

                prodtype.Text = productionResponse.ProductionType;
                remarks.Text = productionResponse.Remarks;
                prcompany.Checked = productionResponse.PrintCompany;
                prowner.Checked = productionResponse.PrintOwner;
                prdate.Checked = productionResponse.PrintDate;
                pruser.Checked = productionResponse.PrintUser;
                prhindi.Checked = productionResponse.PrintHindiWords;
                prwtps.Checked = productionResponse.PrintWTPS;
                prqrcode.Checked = productionResponse.PrintQRCode;
                prtwist.Checked = productionResponse.PrintTwist;
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
                frwt.Text = productionResponse.StartWeight.ToString();
                upwt.Text = productionResponse.EndWeight.ToString();
                copsitemwt.Text = productionResponse.CopsItemWeight.ToString();
                boxpalletitemwt.Text = productionResponse.BoxItemWeight.ToString();
                palletwtno.Text = productionResponse.BoxItemWeight.ToString();
                AdjustNameByCharCount();
                boxnofrmt.Text = (!string.IsNullOrEmpty(productionResponse.BoxNoFmtd)) ? productionResponse.BoxNoFmtd : "";
                dateTimePicker1.Text = productionResponse.ProductionDate.ToString();
                dateTimePicker1.Value = productionResponse.ProductionDate;
                spoolno.Text = productionResponse.Spools.ToString();
                spoolwt.Text = productionResponse.SpoolsWt.ToString();
                palletwtno.Text = productionResponse.EmptyBoxPalletWt.ToString();
                grosswtno.Text = productionResponse.GrossWt.ToString();
                tarewt.Text = productionResponse.TareWt.ToString();
                netwt.Text = productionResponse.NetWt.ToString();
            }
        }

        private async void RefreshLastBoxDetails()
        {
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
                this.lastbox.Text = getLastBox.BoxNoFmtd.ToString();
            }
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
        //                    if (DeptList.Items.Count > 1)
        //                    {
        //                        DeptList.SelectedIndex = 1;
        //                    }
        //                    DeptList.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
        //                    DeptList.AutoCompleteSource = AutoCompleteSource.ListItems;
        //                    DeptList_SelectedIndexChanged(DeptList, EventArgs.Empty);
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
        //        twistvalue.Text = "";
        //        salelotvalue.Text = "";
        //        partyn.Text = "";
        //        partyshade.Text = "";
        //        lotResponse = new LotsResponse();
        //        lotsDetailsList = new List<LotsDetailsResponse>();
        //        LoadDropdowns();
        //        rowMaterial.Columns.Clear();
        //        totalProdQty = 0;
        //        selectedSOId = 0;
        //        totalSOQty = 0;
        //        balanceQty = 0;
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
        //                    twistvalue.Text = (!string.IsNullOrEmpty(lotResponse.TwistName)) ? lotResponse.TwistName.ToString() : "";
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
        //            }

        //            if (_productionId > 0 && productionResponse != null)
        //            {
        //                WindingTypeList.SelectedValue = productionResponse.WindingTypeId;
        //                //WindingTypeList_SelectedIndexChanged(WindingTypeList, EventArgs.Empty);
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
        //            LotSaleOrderDetailsResponse selectedSaleOrder = (LotSaleOrderDetailsResponse)SaleOrderList.SelectedItem;
        //            int selectedSaleOrderId = selectedSaleOrder.SaleOrderItemsId;
        //            string soNumber = selectedSaleOrder.SaleOrderNumber;
        //            productionRequest.SaleOrderItemsId = selectedSaleOrderId;
        //            if (selectedSaleOrderId > 0)
        //            {
        //                selectedSOId = selectedSaleOrderId;
        //                selectedSONumber = selectedSaleOrder.SaleOrderNumber;
        //                totalSOQty = selectedSaleOrder.Quantity;
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

        //                var itemResponse = await Task.Run(() => _masterService.GetItemById(selectedItemId));
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
        //                var itemResponse = await Task.Run(() => _masterService.GetItemById(selectedBoxItemId));
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
        //                var machineList = _masterService.GetMachineByDepartmentIdAndLotType(selectedDepartmentId, "TexturisingLot").Result;

        //                machineList.Insert(0, new MachineResponse { MachineId = 0, MachineName = "Select Line No." });
        //                LineNoList.DataSource = machineList;
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

        //private async void RefreshGradewiseGrid()
        //{
        //    if (QualityList.SelectedValue != null)
        //    {
        //        balanceQty = 0;
        //        int selectedQualityId = Convert.ToInt32(QualityList.SelectedValue.ToString());
        //        var getProductionByQuality = _packingService.getAllByLotIdandSaleOrderItemIdandPackingType(selectLotId, selectedSOId).Result;
        //        List<QualityGridResponse> gridList = new List<QualityGridResponse>();
        //        foreach (var quality in getProductionByQuality)
        //        {
        //            var existing = gridList.FirstOrDefault(x => x.QualityId == quality.QualityId && x.SaleOrderItemsId == quality.SaleOrderItemsId);

        //            if (existing == null)
        //            {
        //                QualityGridResponse grid = new QualityGridResponse();
        //                grid.QualityId = quality.QualityId;
        //                grid.SaleOrderItemsId = quality.SaleOrderItemsId;
        //                grid.QualityName = quality.QualityName;
        //                grid.SaleOrderQty = totalSOQty;
        //                grid.GrossWt = quality.GrossWt;

        //                gridList.Add(grid);
        //            }
        //            else
        //            {
        //                existing.GrossWt += quality.GrossWt;
        //            }
        //        }
        //    }
        //}

        private async Task<List<string>> getComPortList()
        {
            var getComPortType = new List<string>
            {
                "Select Com Port",
                "COM1",
                "COM2",
                "COM3",
                "COM4"
            };

            return getComPortType;
        }
        private async Task<List<WeighingItem>> GetWeighingList()
        {
            var getWeighingScale = new List<WeighingItem>
            {
                new WeighingItem { Id = -1, Name = "Select Weigh Scale" },
                new WeighingItem { Id = 0, Name = "Old" },
                new WeighingItem { Id = 1, Name = "Unique" },
                new WeighingItem { Id = 2, Name = "JISL (9600)" },
                new WeighingItem { Id = 3, Name = "JISL (2400)" }
            };

            return getWeighingScale;
        }

        private void SpoolWeight_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(spoolwt.Text))
            {
                CalculateTareWeight();
            }
            else
            {
                CalculateTareWeight();
            }
        }

        private void PalletWeight_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(palletwtno.Text))
            {
                CalculateTareWeight();
            }
            else
            {
                CalculateTareWeight();
            }
        }

        private void CalculateTareWeight()
        {
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

        }

        private void GrossWeight_TextChanged(object sender, EventArgs e)
        {
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
        }

        private void CalculateNetWeight()
        {
            decimal num1 = 0, num2 = 0;

            decimal.TryParse(grosswtno.Text, out num1);
            decimal.TryParse(tarewt.Text, out num2);
            if (num1 > num2)
            {
                netwt.Text = (num1 - num2).ToString("F3");
                CalculateWeightPerCop();
            }
        }

        private void NetWeight_TextChanged(object sender, EventArgs e)
        {
            CalculateWeightPerCop();
        }

        private void SpoolNo_TextChanged(object sender, EventArgs e)
        {
            if (!isFormReady) return;
            if (string.IsNullOrWhiteSpace(spoolno.Text))
            {
                MessageBox.Show("Please enter spool no", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tarewt.Text = "0";
                spoolwt.Text = "0";
                return;
            }
            else if (string.IsNullOrWhiteSpace(copsitemwt.Text))
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
        }

        private void CalculateWeightPerCop()
        {
            decimal num1 = 0, num2 = 0;

            decimal.TryParse(netwt.Text, out num1);
            decimal.TryParse(spoolno.Text, out num2);
            if (num1 > 0 && num2 > 0)
            {
                wtpercop.Text = (num1 / num2).ToString("F3");
            }
        }

        private void CopyNos_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(copyno.Text))
            {
                copynoerror.Visible = true;
            }
            else
            {
                copynoerror.Text = "";
                copynoerror.Visible = false;
            }
        }

        private void qualityqty_Paint(object sender, PaintEventArgs e)
        {
            _cmethod.DrawRectangleBorder((Control)sender, e, Color.LightGray, 2);
        }

        private void windinggrid_Paint(object sender, PaintEventArgs e)
        {
            _cmethod.DrawRectangleBorder((Control)sender, e, Color.LightGray, 2);
        }

        private void ordertable_Paint(object sender, PaintEventArgs e)
        {
            _cmethod.DrawRoundedBorder((Control)sender, e, 12, Color.FromArgb(102, 163, 255), 1);
        }

        private void packagingtable_Paint(object sender, PaintEventArgs e)
        {
            _cmethod.DrawRoundedBorder((Control)sender, e, 12, Color.FromArgb(102, 163, 255), 1);
        }

        private void weightable_Paint(object sender, PaintEventArgs e)
        {
            _cmethod.DrawRoundedBorder((Control)sender, e, 12, Color.FromArgb(102, 163, 255), 1);
        }

        private void reviewtable_Paint(object sender, PaintEventArgs e)
        {
            _cmethod.DrawRoundedBorder((Control)sender, e, 12, Color.FromArgb(102, 163, 255), 1);
        }

        private void machineboxlayout_Paint(object sender, PaintEventArgs e)
        {
            _cmethod.DrawRoundedBorder((Control)sender, e, 8, Color.FromArgb(191, 191, 191), 1);
        }

        private void machineboxheader_Paint(object sender, PaintEventArgs e)
        {
            _cmethod.DrawBottomBorder((Control)sender, e, Color.FromArgb(191, 191, 191), 1);
        }

        private void weighboxlayout_Paint(object sender, PaintEventArgs e)
        {
            _cmethod.DrawRoundedBorder((Control)sender, e, 8, Color.FromArgb(191, 191, 191), 1);
        }

        private void weighboxheader_Paint(object sender, PaintEventArgs e)
        {
            _cmethod.DrawBottomBorder((Control)sender, e, Color.FromArgb(191, 191, 191), 1);
        }

        private void packagingboxlayout_Paint(object sender, PaintEventArgs e)
        {
            _cmethod.DrawRoundedBorder((Control)sender, e, 8, Color.FromArgb(191, 191, 191), 1);
        }

        private void packagingboxheader_Paint(object sender, PaintEventArgs e)
        {
            _cmethod.DrawBottomBorder((Control)sender, e, Color.FromArgb(191, 191, 191), 1);
        }

        private void lastboxlayout_Paint(object sender, PaintEventArgs e)
        {
            _cmethod.DrawRoundedBorder((Control)sender, e, 8, Color.FromArgb(191, 191, 191), 1);
        }

        private void lastboxheader_Paint(object sender, PaintEventArgs e)
        {
            _cmethod.DrawBottomBorder((Control)sender, e, Color.FromArgb(191, 191, 191), 1);
        }

        private void lastbxcopspanel_Paint(object sender, PaintEventArgs e)
        {
            _cmethod.DrawRoundedDashedBorder((Control)sender, e, 8, Color.FromArgb(102, 163, 255), 1);
        }

        private void lastbxtarepanel_Paint(object sender, PaintEventArgs e)
        {
            _cmethod.DrawRoundedDashedBorder((Control)sender, e, 8, Color.FromArgb(102, 163, 255), 1);
        }

        private void lastbxgrosswtpanel_Paint(object sender, PaintEventArgs e)
        {
            _cmethod.DrawRoundedDashedBorder((Control)sender, e, 8, Color.FromArgb(102, 163, 255), 1);
        }

        private void lastbxnetwtpanel_Paint(object sender, PaintEventArgs e)
        {
            _cmethod.DrawRoundedDashedBorder((Control)sender, e, 8, Color.FromArgb(102, 163, 255), 1);
        }

        private void printingdetailslayout_Paint(object sender, PaintEventArgs e)
        {
            _cmethod.DrawRoundedBorder((Control)sender, e, 8, Color.FromArgb(191, 191, 191), 1);
        }

        private void printingdetailsheader_Paint(object sender, PaintEventArgs e)
        {
            _cmethod.DrawBottomBorder((Control)sender, e, Color.FromArgb(191, 191, 191), 1);
        }

        private void palletdetailslayout_Paint(object sender, PaintEventArgs e)
        {
            _cmethod.DrawRoundedBorder((Control)sender, e, 8, Color.FromArgb(191, 191, 191), 1);
        }

        private void palletdetailsheader_Paint(object sender, PaintEventArgs e)
        {
            _cmethod.DrawBottomBorder((Control)sender, e, Color.FromArgb(191, 191, 191), 1);
        }

        private void machineboxheader_Resize(object sender, EventArgs e)
        {
            _cmethod.SetTopRoundedRegion(machineboxheader, 8);
        }

        private void weighboxheader_Resize(object sender, EventArgs e)
        {
            _cmethod.SetTopRoundedRegion(weighboxheader, 8);
        }

        private void packagingboxheader_Resize(object sender, EventArgs e)
        {
            _cmethod.SetTopRoundedRegion(packagingboxheader, 8);
        }

        private void lastboxheader_Resize(object sender, EventArgs e)
        {
            _cmethod.SetTopRoundedRegion(lastboxheader, 8);
        }

        private void printingdetailsheader_Resize(object sender, EventArgs e)
        {
            _cmethod.SetTopRoundedRegion(printingdetailsheader, 8);
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

        private void btnCancel_Click(object sender, EventArgs e)
        {
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
            if (itemCharCount > 20)
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
            Log.writeMessage("DTY btnFind_Click - Start : " + DateTime.Now);

            popuppanel.Visible = true;
            popuppanel.BringToFront();

            // Center popup in form
            popuppanel.Left = (this.ClientSize.Width - popuppanel.Width) / 2;
            popuppanel.Top = (this.ClientSize.Height - popuppanel.Height) / 2;

            Log.writeMessage("DTY btnFind_Click - End : " + DateTime.Now);
        }

        private void btnClosePopup_Click(object sender, EventArgs e)
        {
            Log.writeMessage("DTY btnClosePopup_Click - Start : " + DateTime.Now);

            popuppanel.Visible = false;

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

                var machineList = _masterService.GetMachineList("TexturisingLot", typedText).Result.OrderBy(x => x.MachineName).ToList();
                machineList.Insert(0, new MachineResponse { MachineId = 0, MachineName = "Select Line No." });

                SrLineNoList.BeginUpdate();
                SrLineNoList.DataSource = null;
                SrLineNoList.DisplayMember = "MachineName";
                SrLineNoList.ValueMember = "MachineId";
                SrLineNoList.DataSource = machineList;
                SrLineNoList.EndUpdate();

                SrLineNoList.TextUpdate -= SrLineNoList_TextUpdate;
                SrLineNoList.Text = typedText;
                SrLineNoList.DroppedDown = true;
                SrLineNoList.SelectionStart = cursorPosition;
                SrLineNoList.SelectionLength = typedText.Length;
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

                var deptList = _masterService.GetDepartmentList(typedText).Result.OrderBy(x => x.DepartmentName).ToList();

                deptList.Insert(0, new DepartmentResponse { DepartmentId = 0, DepartmentName = "Select Dept" });

                SrDeptList.BeginUpdate();
                SrDeptList.DataSource = null;
                SrDeptList.DisplayMember = "DepartmentName";
                SrDeptList.ValueMember = "DepartmentId";
                SrDeptList.DataSource = deptList;
                SrDeptList.EndUpdate();

                SrDeptList.TextUpdate -= SrDeptList_TextUpdate;
                SrDeptList.DroppedDown = true;
                SrDeptList.Text = typedText;
                SrDeptList.SelectionStart = cursorPosition;
                SrDeptList.SelectionLength = typedText.Length;
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

                cb.TextUpdate += SrBoxNoList_TextUpdate;
                return;
            }

            int cursorPosition = cb.SelectionStart;

            if (typedText.Length >= 2)
            {
                //DeptList.Items.Clear();

                var srboxnoList = _packingService.getAllBoxNoByPackingType("DTYPacking", typedText).Result;

                srboxnoList.Insert(0, new ProductionResponse { ProductionId = 0, BoxNo = "Select BoxNo" });

                SrBoxNoList.BeginUpdate();
                SrBoxNoList.DataSource = null;
                SrBoxNoList.DisplayMember = "BoxNo";
                SrBoxNoList.ValueMember = "ProductionId";
                SrBoxNoList.DataSource = srboxnoList;
                SrBoxNoList.EndUpdate();

                SrBoxNoList.TextUpdate -= SrBoxNoList_TextUpdate;
                SrBoxNoList.DroppedDown = true;
                SrBoxNoList.Text = typedText;
                SrBoxNoList.SelectionStart = cursorPosition;
                SrBoxNoList.SelectionLength = typedText.Length;
                SrBoxNoList.TextUpdate += SrBoxNoList_TextUpdate;

            }
            Log.writeMessage("DTY SrBoxNoList_TextUpdate - End : " + DateTime.Now);
        }

        private void rbLineNo_CheckedChanged(object sender, EventArgs e)
        {
            Log.writeMessage("DTY rbLineNo_CheckedChanged - Start : " + DateTime.Now);

            SrLineNoList.Enabled = srlinenoradiobtn.Checked;
            SrDeptList.Enabled = false;
            SrBoxNoList.Enabled = false;
            dateTimePicker2.Enabled = false;

            Log.writeMessage("DTY rbLineNo_CheckedChanged - End : " + DateTime.Now);
        }

        private void rbDepartment_CheckedChanged(object sender, EventArgs e)
        {
            Log.writeMessage("DTY rbDepartment_CheckedChanged - Start : " + DateTime.Now);

            SrDeptList.Enabled = srdeptradiobtn.Checked;
            SrLineNoList.Enabled = false;
            SrBoxNoList.Enabled = false;
            dateTimePicker2.Enabled = false;

            Log.writeMessage("DTY rbDepartment_CheckedChanged - End : " + DateTime.Now);
        }

        private void rbBoxNo_CheckedChanged(object sender, EventArgs e)
        {
            Log.writeMessage("DTY rbBoxNo_CheckedChanged - Start : " + DateTime.Now);

            SrBoxNoList.Enabled = srboxnoradiobtn.Checked;
            SrLineNoList.Enabled = false;
            SrDeptList.Enabled = false;
            dateTimePicker2.Enabled = false;

            Log.writeMessage("DTY rbBoxNo_CheckedChanged - End : " + DateTime.Now);
        }

        private void rbDate_CheckedChanged(object sender, EventArgs e)
        {
            Log.writeMessage("DTY rbDate_CheckedChanged - Start : " + DateTime.Now);

            dateTimePicker2.Enabled = srproddateradiobtn.Checked;
            SrLineNoList.Enabled = false;
            SrDeptList.Enabled = false;
            SrBoxNoList.Enabled = false;

            Log.writeMessage("DTY rbDate_CheckedChanged - End : " + DateTime.Now);
        }

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

            if (!srlinenoradiobtn.Checked && !srdeptradiobtn.Checked && !srboxnoradiobtn.Checked && !srproddateradiobtn.Checked)
            {
                MessageBox.Show("Please select at least any one option.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int machineid = 0, deptid = 0;
            string boxnoid = null;
            string proddt = null;
            if (srlinenoradiobtn.Checked) { machineid = selectedSrMachineId; }
            if (srdeptradiobtn.Checked) { deptid = selectedSrDeptId; }
            if (srboxnoradiobtn.Checked) { boxnoid = selectedSrBoxNo; }
            if (srproddateradiobtn.Checked) { proddt = selectedSrProductionDate; }
            packingList = _packingService.getProductionDetailsBySelectedParameter("DTYPacking", machineid, deptid, boxnoid, proddt).Result;

            if (packingList.Count > 0)
            {
                datalistpopuppanel.Visible = true;
                datalistpopuppanel.BringToFront();

                // Center popup in form
                datalistpopuppanel.Left = (this.ClientSize.Width - datalistpopuppanel.Width) / 2;
                datalistpopuppanel.Top = (this.ClientSize.Height - datalistpopuppanel.Height) / 2;

                dataGridView1.AutoGenerateColumns = false;
                dataGridView1.Columns.Clear();

                // Define columns
                dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { Name = "SrNo", DataPropertyName = "SerialNo", HeaderText = "SR. No" });
                //dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { Name = "PackingType", DataPropertyName = "PackingType", HeaderText = "Packing Type" });
                dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { Name = "DepartmentName", DataPropertyName = "DepartmentName", HeaderText = "Department" });
                dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { Name = "MachineName", DataPropertyName = "MachineName", HeaderText = "Machine" });
                dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { Name = "LotNo", DataPropertyName = "LotNo", HeaderText = "Lot No" });
                dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { Name = "BoxNo", DataPropertyName = "BoxNoFmtd", HeaderText = "Box No" });
                dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { Name = "ProductionDate", DataPropertyName = "ProductionDate", HeaderText = "Production Date" });
                //dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { Name = "QualityName", DataPropertyName = "QualityName", HeaderText = "Quality" });
                dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { Name = "SalesOrderNumber", DataPropertyName = "SalesOrderNumber", HeaderText = "Sales Order" });
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

                // Add Edit button column
                DataGridViewImageColumn btn = new DataGridViewImageColumn();
                btn.HeaderText = "Action";
                btn.Name = "Action";
                btn.Image = _cmethod.ResizeImage(Properties.Resources.icons8_edit_48, 20, 20);
                btn.ImageLayout = DataGridViewImageCellLayout.Normal;
                btn.Width = 45;  // column width
                dataGridView1.RowTemplate.Height = 40; // row height
                dataGridView1.Columns.Add(btn);

                dataGridView1.DataSource = packingList;

                dataGridView1.CellContentClick += dataGridView1_CellContentClick;

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
            }
            else
            {
                dataGridView1.DataSource = null;
                MessageBox.Show("Data not found", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            Log.writeMessage("DTY btnSearch_Click - End : " + DateTime.Now);
        }

        //private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        //{
        //    Log.writeMessage("DTY dataGridView1_RowPostPaint - Start : " + DateTime.Now);

        //    dataGridView1.Rows[e.RowIndex].Cells["SrNo"].Value = e.RowIndex + 1;

        //    Log.writeMessage("DTY dataGridView1_RowPostPaint - End : " + DateTime.Now);
        //}

        private async void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            Log.writeMessage("DTY dataGridView1_CellContentClick - Start : " + DateTime.Now);

            if (e.RowIndex >= 0 && e.ColumnIndex == dataGridView1.Columns["Action"].Index)
            {
                var rowObj = dataGridView1.Rows[e.RowIndex].DataBoundItem as ProductionResponse;
                if (!rowObj.CanModifyDelete)
                    return;

                popuppanel.Visible = false;
                datalistpopuppanel.Visible = false;

                long productionId = Convert.ToInt32(
                    ((ProductionResponse)dataGridView1.Rows[e.RowIndex].DataBoundItem).ProductionId
                );

                var getSelectedProductionDetails = _packingService.getLastBoxDetails("dtypacking", productionId).Result;

                //SelectedProductionDetails
                if (getSelectedProductionDetails.ProductionId > 0)
                {
                    await LoadProductionDetailsAsync(getSelectedProductionDetails);

                    this.copstxtbox.Text = getSelectedProductionDetails.Spools.ToString();
                    this.tarewghttxtbox.Text = getSelectedProductionDetails.TareWt.ToString();
                    this.grosswttxtbox.Text = getSelectedProductionDetails.GrossWt.ToString();
                    this.netwttxtbox.Text = getSelectedProductionDetails.NetWt.ToString();
                    this.lastbox.Text = getSelectedProductionDetails.BoxNoFmtd.ToString();
                }
            }

            Log.writeMessage("DTY dataGridView1_CellContentClick - End : " + DateTime.Now);
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

        private void SrProdDate_ValueChanged(object sender, EventArgs e)
        {
            Log.writeMessage("DTY SrProdDate_ValueChanged - Start : " + DateTime.Now);

            DateTime selectedDate = dateTimePicker2.Value.Date;
            selectedSrProductionDate = selectedDate.ToString("dd-MM-yyyy");

            Log.writeMessage("DTY SrProdDate_ValueChanged - End : " + DateTime.Now);
        }

        private void btnDatalistClosePopup_Click(object sender, EventArgs e)
        {
            Log.writeMessage("DTY btnDatalistClosePopup_Click - Start : " + DateTime.Now);

            datalistpopuppanel.Visible = false;

            Log.writeMessage("DTY btnDatalistClosePopup_Click - End : " + DateTime.Now);
        }
    }
}
