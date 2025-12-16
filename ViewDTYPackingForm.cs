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
    public partial class ViewDTYPackingForm : Form
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
        public ViewDTYPackingForm()
        {
            Log.writeMessage("DTY ViewDTYPackingForm constructor - Start : " + DateTime.Now);

            InitializeComponent();
            ApplyFonts();
            //this.Shown += ViewDTYPackingForm_Shown;
            this.AutoScroll = true;
            lblLoading = CommonMethod.InitializeLoadingLabel(this);
            _cmethod.SetButtonBorderRadius(this.cancelbtn, 8);
            rowMaterial.AutoGenerateColumns = false;

            Log.writeMessage("DTY ViewDTYPackingForm constructor - End : " + DateTime.Now);
        }

        private void ViewDTYPackingForm_Load(object sender, EventArgs e)
        {
            Log.writeMessage("DTY ViewDTYPackingForm_Load - Start : " + DateTime.Now);

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
            Log.writeMessage("DTY ViewDTYPackingForm_Load - End : " + DateTime.Now);
        }

        private void LoadDropdowns()
        {
            Log.writeMessage("DTY LoadDropdowns - Start : " + DateTime.Now);

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

            isFormReady = true;

            Log.writeMessage("DTY LoadDropdowns - End : " + DateTime.Now);
        }

        private void ApplyFonts()
        {
            Log.writeMessage("DTY ApplyFonts - Start : " + DateTime.Now);

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

            Log.writeMessage("DTY ApplyFonts - End : " + DateTime.Now);
        }

        //private async void ViewDTYPackingForm_Shown(object sender, EventArgs e)
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
            Log.writeMessage("DTY LoadProductionDetailsAsync - Start : " + DateTime.Now);

            if (prodResponse != null)
            {
                productionResponse = prodResponse;

                //LineNoList.SelectedValue = productionResponse.MachineId;
                //DeptList.SelectedValue = productionResponse.DepartmentId;
                //MergeNoList.SelectedValue = productionResponse.LotId;
                //boxnofrmt.Text = productionResponse.BoxNoFmtd;
                //dateTimePicker1.Text = productionResponse.ProductionDate.ToString();
                //dateTimePicker1.Value = productionResponse.ProductionDate;
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
                //prtwist.Checked = productionResponse.PrintTwist;
                //spoolno.Text = productionResponse.Spools.ToString();
                //spoolwt.Text = productionResponse.SpoolsWt.ToString();
                //palletwtno.Text = productionResponse.EmptyBoxPalletWt.ToString();
                //grosswtno.Text = productionResponse.GrossWt.ToString();
                //tarewt.Text = productionResponse.TareWt.ToString();
                //netwt.Text = productionResponse.NetWt.ToString();
                //OwnerList.SelectedValue = productionResponse.OwnerId;

                //LineNoList_SelectedIndexChanged(LineNoList, EventArgs.Empty);

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
                itemname.Text = productionResponse.ItemName;
                shadename.Text = productionResponse.ShadeName;
                shadecd.Text = productionResponse.ShadeCode;
                deniervalue.Text = productionResponse.Denier.ToString();
                twistvalue.Text = productionResponse.TwistName.ToString();
                salelotvalue.Text = productionResponse.SaleLot.ToString();
                frdenier.Text = productionResponse.FromDenier.ToString();
                updenier.Text = productionResponse.UpToDenier.ToString();
                frwt.Text = productionResponse.StartWeight.ToString();
                upwt.Text = productionResponse.EndWeight.ToString();
                copsitemwt.Text = productionResponse.CopsItemWeight.ToString();
                boxpalletitemwt.Text = productionResponse.BoxItemWeight.ToString();
                palletwtno.Text = productionResponse.BoxItemWeight.ToString();
                AdjustNameByCharCount();
                boxnofrmt.Text = productionResponse.BoxNoFmtd;
                dateTimePicker1.Text = productionResponse.ProductionDate.ToString();
                dateTimePicker1.Value = productionResponse.ProductionDate;
                spoolno.Text = productionResponse.Spools.ToString();
                spoolwt.Text = productionResponse.SpoolsWt.ToString();
                palletwtno.Text = productionResponse.EmptyBoxPalletWt.ToString();
                grosswtno.Text = productionResponse.GrossWt.ToString();
                tarewt.Text = productionResponse.TareWt.ToString();
                netwt.Text = productionResponse.NetWt.ToString();
            }

            Log.writeMessage("DTY LoadProductionDetailsAsync - End : " + DateTime.Now);
        }

        private async void RefreshLastBoxDetails()
        {
            Log.writeMessage("DTY RefreshLastBoxDetails - Start : " + DateTime.Now);

            var getLastBox = _packingService.getLastBoxDetails("dtypacking").Result;

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

            Log.writeMessage("DTY RefreshLastBoxDetails - End : " + DateTime.Now);
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
        //                    //DeptList.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
        //                    //DeptList.AutoCompleteSource = AutoCompleteSource.ListItems;
        //                    //DeptList_SelectedIndexChanged(DeptList, EventArgs.Empty);
        //                }
        //                //var getLots = _productionService.getLotList(selectedMachineId, "").Result;
        //                //getLots.Insert(0, new LotsResponse { LotId = 0, LotNoFrmt = "Select MergeNo" });
        //                //MergeNoList.DataSource = getLots;
        //                //MergeNoList.DisplayMember = "LotNoFrmt";
        //                //MergeNoList.ValueMember = "LotId";
        //                //MergeNoList.SelectedIndex = 0;
        //                //MergeNoList.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
        //                //MergeNoList.AutoCompleteSource = AutoCompleteSource.ListItems;

        //                //if (_productionId > 0 && productionResponse != null)
        //                //{
        //                //    MergeNoList.SelectedValue = productionResponse.LotId;
        //                //    DeptList.SelectedValue = productionResponse.DepartmentId;
        //                //    MergeNoList_SelectedIndexChanged(MergeNoList, EventArgs.Empty);
        //                //}
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
        //                //lotsDetailsList = _productionService.getLotsDetailsByLotsIdAndProductionDate(selectedLotId, productionRequest.ProductionDate).Result;
        //                //if (lotsDetailsList.Count > 0)
        //                //{
        //                //    rowMaterial.Columns.Clear();
        //                //    rowMaterial.Columns.Add(new DataGridViewTextBoxColumn { Name = "PrevLotType", DataPropertyName = "PrevLotType", HeaderText = "Prev.LotType" });
        //                //    rowMaterial.Columns.Add(new DataGridViewTextBoxColumn { Name = "PrevLotNo", DataPropertyName = "PrevLotNo", HeaderText = "Prev.LotNo" });
        //                //    rowMaterial.Columns.Add(new DataGridViewTextBoxColumn { Name = "PrevLotItemName", DataPropertyName = "PrevLotItemName", HeaderText = "Prev.LotItem" });
        //                //    rowMaterial.Columns.Add(new DataGridViewTextBoxColumn { Name = "PrevLotShadeName", DataPropertyName = "PrevLotShadeName", HeaderText = "Prev.LotShade" });
        //                //    rowMaterial.Columns.Add(new DataGridViewTextBoxColumn { Name = "PrevLotQuality", DataPropertyName = "PrevLotQuality", HeaderText = "Quality" });
        //                //    rowMaterial.Columns.Add(new DataGridViewTextBoxColumn { Name = "ProductionPerc", DataPropertyName = "ProductionPerc", HeaderText = "Production %" });
        //                //    rowMaterial.Columns.Add(new DataGridViewTextBoxColumn { Name = "EffectiveFrom", DataPropertyName = "EffectiveFrom", HeaderText = "EffectiveFrom", Width = 150, DefaultCellStyle = { Format = "dd-MM-yyyy hh:mm tt" } });
        //                //    rowMaterial.Columns.Add(new DataGridViewTextBoxColumn { Name = "EffectiveUpto", DataPropertyName = "EffectiveUpto", HeaderText = "EffectiveUpto", Width = 150, DefaultCellStyle = { Format = "dd-MM-yyyy hh:mm tt" } });
        //                //    rowMaterial.DataSource = lotsDetailsList;
        //                //}

        //                //if (_productionId > 0 && productionResponse != null)
        //                //{
        //                //    SaleOrderList.SelectedValue = productionResponse.SaleOrderItemsId;
        //                //    SaleOrderList_SelectedIndexChanged(SaleOrderList, EventArgs.Empty);
        //                //}
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

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Log.writeMessage("DTY btnCancel_Click - Start : " + DateTime.Now);

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

            Log.writeMessage("DTY btnCancel_Click - End : " + DateTime.Now);
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
            if (itemCharCount > 20)
            {
                itemname.Location = new System.Drawing.Point(38, -3);
            }
            else
            {
                itemname.Location = new System.Drawing.Point(38, 5);
            }

            Log.writeMessage("DTY AdjustNameByCharCount - End : " + DateTime.Now);
        }
    }
}
