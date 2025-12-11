using PackingApplication.Helper;
using PackingApplication.Models.CommonEntities;
using PackingApplication.Models.RequestEntities;
using PackingApplication.Models.ResponseEntities;
using PackingApplication.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
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
        int selectedItemTypeid = 0;
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

            RefreshLastBoxDetails();

            prcompany.FlatStyle = FlatStyle.System;
            this.tableLayoutPanel4.SetColumnSpan(this.panel11, 2);
            this.tableLayoutPanel4.SetColumnSpan(this.panel12, 2);
            this.tableLayoutPanel4.SetColumnSpan(this.panel17, 3);
            this.tableLayoutPanel4.SetColumnSpan(this.panel30, 2);
            this.tableLayoutPanel6.SetColumnSpan(this.panel29, 2);

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

            Log.writeMessage("LoadDropdowns - End : " + DateTime.Now);
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
                submit.Text = "Update";
                saveprint.Text = "Update && Print";
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
                salesOrderNumber = productionResponse.SalesOrderNumber + "--" + productionResponse.ItemName + "--" + productionResponse.ShadeName + "--" + productionResponse.SOQuantity;
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
                itemname.Text = productionResponse.ItemName;
                shadename.Text = productionResponse.ShadeName;
                shadecd.Text = productionResponse.ShadeCode;
                deniervalue.Text = productionResponse.Denier.ToString();
                twistvalue.Text = productionResponse.TwistName.ToString();
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
                RefreshGradewiseGrid();
                AdjustNameByCharCount();
                productionRequest.ItemId = productionResponse.ItemId;
                productionRequest.ShadeId = productionResponse.ShadeId;
                productionRequest.TwistId = productionResponse.TwistId;
                productionRequest.ContainerTypeId = productionResponse.ContainerTypeId;
                boxnofrmt.Text = productionResponse.BoxNoFmtd;
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

            if (typedText.Length >= 2)
            {
                LineNoList.BeginUpdate();
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

                LineNoList.TextUpdate -= LinoNoList_TextUpdate;

                LineNoList.DisplayMember = "MachineName";
                LineNoList.ValueMember = "MachineId";
                LineNoList.DataSource = machineList;
                //LineNoList.Text = typedText;

                LineNoList.EndUpdate();

                LineNoList.DroppedDown = true;
                //LineNoList.SelectionStart = typedText.Length;
                LineNoList.SelectionLength = 0;

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

            if (typedText.Length >= 2)
            {
                suppressEvents = true;

                MergeNoList.BeginUpdate();
                //MergeNoList.Items.Clear();

                var mergenoList = _productionService.getLotList(selectedMachineid, typedText).Result.OrderBy(x => x.LotNoFrmt).ToList();

                mergenoList.Insert(0, new LotsResponse { LotId = 0, LotNoFrmt = "Select MergeNo" });

                MergeNoList.TextUpdate -= MergeNoList_TextUpdate;
                MergeNoList.DisplayMember = "LotNoFrmt";
                MergeNoList.ValueMember = "LotId";
                MergeNoList.DataSource = mergenoList;
                //MergeNoList.Text = typedText;

                MergeNoList.EndUpdate();

                MergeNoList.DroppedDown = true;
                //MergeNoList.SelectionStart = typedText.Length;
                MergeNoList.SelectionLength = 0;
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

            if (typedText.Length >= 2)
            {
                PackSizeList.BeginUpdate();
                //PackSizeList.Items.Clear();

                var packsizeList = _masterService.GetPackSizeList(typedText).Result.OrderBy(x => x.PackSizeName).ToList();

                packsizeList.Insert(0, new PackSizeResponse { PackSizeId = 0, PackSizeName = "Select Pack Size" });

                PackSizeList.TextUpdate -= PackSizeList_TextUpdate;

                PackSizeList.DisplayMember = "PackSizeName";
                PackSizeList.ValueMember = "PackSizeId";
                PackSizeList.DataSource = packsizeList;
                //PackSizeList.Text = typedText;

                PackSizeList.EndUpdate();

                PackSizeList.DroppedDown = true;
                //PackSizeList.SelectionStart = typedText.Length;
                PackSizeList.SelectionLength = 0;

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

            if (typedText.Length >= 2)
            {
                suppressEvents = true;

                QualityList.BeginUpdate();
                //QualityList.Items.Clear();

                var qualityList = _masterService.GetQualityListByItemTypeId(selectedItemTypeid).Result.OrderBy(x => x.Name).ToList();
                qualityList.Insert(0, new QualityResponse { QualityId = 0, Name = "Select Quality" });
                QualityList.TextUpdate -= QualityList_TextUpdate;

                QualityList.DisplayMember = "Name";
                QualityList.ValueMember = "QualityId";
                QualityList.DataSource = qualityList;
                //QualityList.Text = typedText;

                QualityList.EndUpdate();

                QualityList.DroppedDown = true;
                //QualityList.SelectionStart = typedText.Length;
                QualityList.SelectionLength = 0;

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

            if (typedText.Length >= 2)
            {
                suppressEvents = true;

                WindingTypeList.BeginUpdate();
                //WindingTypeList.Items.Clear();

                var getWindingType = new List<WindingTypeResponse>();
                getWindingType = _productionService.getWinderTypeList(selectLotId, typedText).Result.OrderBy(x => x.WindingTypeName).ToList();
                getWindingType.Insert(0, new WindingTypeResponse { WindingTypeId = 0, WindingTypeName = "Select Winding Type" });
                if (getWindingType.Count <= 1)
                {
                    getWindingType = _masterService.GetWindingTypeList(typedText).Result.OrderBy(x => x.WindingTypeName).ToList();
                    getWindingType.Insert(0, new WindingTypeResponse { WindingTypeId = 0, WindingTypeName = "Select Winding Type" });

                }
                WindingTypeList.TextUpdate -= WindingTypeList_TextUpdate;

                WindingTypeList.DisplayMember = "WindingTypeName";
                WindingTypeList.ValueMember = "WindingTypeId";
                WindingTypeList.DataSource = getWindingType;
                //WindingTypeList.Text = typedText;

                WindingTypeList.EndUpdate();

                WindingTypeList.DroppedDown = true;
                //WindingTypeList.SelectionStart = typedText.Length;
                WindingTypeList.SelectionLength = 0;

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

            if (typedText.Length >= 2)
            {
                suppressEvents = true;

                SaleOrderList.BeginUpdate();
                //SaleOrderList.Items.Clear();

                var getSaleOrder = _productionService.getSaleOrderList(selectLotId, typedText).Result.OrderBy(x => x.ItemName).ToList();
                getSaleOrder.Insert(0, new LotSaleOrderDetailsResponse { SaleOrderItemsId = 0, ItemName = "Select Sale Order Item" });
                SaleOrderList.TextUpdate -= SaleOrderList_TextUpdate;

                SaleOrderList.DisplayMember = "ItemName";
                SaleOrderList.ValueMember = "SaleOrderItemsId";
                SaleOrderList.DataSource = getSaleOrder;
                //SaleOrderList.Text = typedText;

                SaleOrderList.EndUpdate();

                SaleOrderList.DroppedDown = true;
                //SaleOrderList.SelectionStart = typedText.Length;
                SaleOrderList.SelectionLength = 0;

                SaleOrderList.TextUpdate += SaleOrderList_TextUpdate;

                suppressEvents = false;
            }
            Log.writeMessage("DTY SaleOrderList_TextUpdate - End : " + DateTime.Now);
        }

        private async void RefreshGradewiseGrid()
        {
            Log.writeMessage("DTY RefreshGradewiseGrid - Start : " + DateTime.Now);

            if (QualityList.SelectedValue != null)
            {
                balanceQty = 0;
                int selectedQualityId = Convert.ToInt32(QualityList.SelectedValue.ToString());
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

            if (typedText.Length >= 2)
            {
                CopsItemList.BeginUpdate();
                //CopsItemList.Items.Clear();

                var copsitemList = _masterService.GetItemList(itemCopsCategoryId, typedText).Result.OrderBy(x => x.Name).ToList();

                copsitemList.Insert(0, new ItemResponse { ItemId = 0, Name = "Select Cops Item" });

                CopsItemList.TextUpdate -= CopsItemList_TextUpdate;

                CopsItemList.DisplayMember = "Name";
                CopsItemList.ValueMember = "ItemId";
                CopsItemList.DataSource = copsitemList;
                //CopsItemList.Text = typedText;

                CopsItemList.EndUpdate();

                CopsItemList.DroppedDown = true;
                //CopsItemList.SelectionStart = typedText.Length;
                CopsItemList.SelectionLength = 0;

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

            if (typedText.Length >= 2)
            {
                BoxItemList.BeginUpdate();
                //BoxItemList.Items.Clear();

                var boxitemList = _masterService.GetItemList(itemBoxCategoryId, typedText).Result.OrderBy(x => x.Name).ToList();

                boxitemList.Insert(0, new ItemResponse { ItemId = 0, Name = "Select Box/Pallet" });

                BoxItemList.TextUpdate -= BoxItemList_TextUpdate;

                BoxItemList.DisplayMember = "Name";
                BoxItemList.ValueMember = "ItemId";
                BoxItemList.DataSource = boxitemList;
                //BoxItemList.Text = typedText;

                BoxItemList.EndUpdate();

                BoxItemList.DroppedDown = true;
                //BoxItemList.SelectionStart = typedText.Length;
                BoxItemList.SelectionLength = 0;

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

            if (typedText.Length >= 2)
            {
                DeptList.BeginUpdate();
                //DeptList.Items.Clear();

                var deptList = _masterService.GetDepartmentList(typedText).Result.OrderBy(x => x.DepartmentName).ToList();

                deptList.Insert(0, new DepartmentResponse { DepartmentId = 0, DepartmentName = "Select Dept" });

                DeptList.TextUpdate -= DeptList_TextUpdate;

                DeptList.DisplayMember = "DepartmentName";
                DeptList.ValueMember = "DepartmentId";
                DeptList.DataSource = deptList;
                //DeptList.Text = typedText;

                DeptList.EndUpdate();

                DeptList.DroppedDown = true;
                //DeptList.SelectionStart = typedText.Length;
                DeptList.SelectionLength = 0;

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

            if (typedText.Length >= 2)
            {
                OwnerList.BeginUpdate();
                //OwnerList.Items.Clear();

                var ownerList = _masterService.GetOwnerList(typedText).Result.OrderBy(x => x.LegalName).ToList();

                ownerList.Insert(0, new BusinessPartnerResponse { BusinessPartnerId = 0, LegalName = "Select Owner" });

                OwnerList.TextUpdate -= OwnerList_TextUpdate;

                OwnerList.DisplayMember = "LegalName";
                OwnerList.ValueMember = "BusinessPartnerId";
                OwnerList.DataSource = ownerList;
                //OwnerList.Text = typedText;

                OwnerList.EndUpdate();

                OwnerList.DroppedDown = true;
                //OwnerList.SelectionStart = typedText.Length;
                OwnerList.SelectionLength = 0;

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
                    consumptionDetailsRequest.InputItemId = lotResponse.ItemId;
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

            submit.Enabled = false;
            saveprint.Enabled = false;
            ProductionResponse result = new ProductionResponse();
            result = _packingService.AddUpdatePOYPacking(_productionId, productionRequest);
            if (result != null && result.ProductionId > 0)
            {
                submit.Enabled = true;
                saveprint.Enabled = true;
                RefreshGradewiseGrid();
                RefreshLastBoxDetails();
                if (_productionId == 0)
                {
                    MessageBox.Show("DTY Packing added successfully!",
                    "Success",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                    isFormReady = false;
                    this.spoolno.Text = "0";
                    this.spoolwt.Text = "0";
                    this.grosswtno.Text = "0.000";
                    this.tarewt.Text = "0.000";
                    this.netwt.Text = "0.000";
                    this.wtpercop.Text = "0.000";
                    palletwtno.Text = boxpalletitemwt.Text;
                    isFormReady = true;
                    //if (isPrint)
                    //{
                    //    //call ssrs report to print
                    //    string reportServer = "http://desktop-ocu1bqt/ReportServer";
                    //    string reportPath = "/PackingSSRSReport/TextureAndPOY";
                    //    string format = "PDF";

                    //    //set params
                    //    string productionId = result.ProductionId.ToString();
                    //    string startDate = "";
                    //    string endDate = "";
                    //    string url = $"{reportServer}?{reportPath}&rs:Format={format}" + $"&ProductionId={productionId}&StartDate={startDate}&EndDate={endDate}";

                    //    WebClient client = new WebClient();
                    //    client.Credentials = CredentialCache.DefaultNetworkCredentials;

                    //    byte[] bytes = client.DownloadData(url);

                    //    // Save to file
                    //    string tempFile = Path.Combine(Path.GetTempPath(), "Report.pdf");
                    //    File.WriteAllBytes(tempFile, bytes);

                    //    //// Open with default PDF reader
                    //    //System.Diagnostics.Process.Start("Report.pdf");

                    //    using (var pdfDoc = PdfDocument.Load(tempFile))
                    //    {
                    //        using (var printDoc = pdfDoc.CreatePrintDocument())
                    //        {
                    //            var printerSettings = new PrinterSettings()
                    //            {
                    //                // PrinterName = "YourPrinterName", // optional, default printer if omitted
                    //                Copies = 1
                    //            };
                    //            // Set custom 4x4 label size
                    //            printDoc.DefaultPageSettings.PaperSize = new PaperSize("Label4x4", 400, 400);
                    //            printDoc.DefaultPageSettings.Margins = new Margins(0, 0, 0, 0); // no margins

                    //            printDoc.PrinterSettings = printerSettings;
                    //            printDoc.Print(); // sends PDF to printer
                    //        }
                    //    }

                    //    // 5️⃣ Clean up temp file
                    //    File.Delete(tempFile);
                    //}
                }
                else
                {
                    ShowCustomMessage(result.BoxNoFmtd);
                    //if (isPrint)
                    //{
                    //    string reportServer = "http://desktop-ocu1bqt/ReportServer";
                    //    string reportPath = "/PackingSSRSReport/TextureAndPOY";
                    //    string format = "PDF";

                    //    //set params
                    //    string productionId = result.ProductionId.ToString();
                    //    string startDate = "2025-09-01";
                    //    string endDate = "2025-09-30";
                    //    string url = $"{reportServer}?{reportPath}&rs:Format={format}" + $"&ProductionId={productionId}&StartDate={startDate}&EndDate={endDate}";

                    //    WebClient client = new WebClient();
                    //    client.Credentials = CredentialCache.DefaultNetworkCredentials;

                    //    byte[] bytes = client.DownloadData(url);

                    //    // Save to file
                    //    string tempFile = Path.Combine(Path.GetTempPath(), "Report.pdf");
                    //    File.WriteAllBytes(tempFile, bytes);

                    //    //// Open with default PDF reader
                    //    //System.Diagnostics.Process.Start("Report.pdf");

                    //    using (var pdfDoc = PdfDocument.Load(tempFile))
                    //    {
                    //        using (var printDoc = pdfDoc.CreatePrintDocument())
                    //        {
                    //            printDoc.PrinterSettings = new PrinterSettings()
                    //            {
                    //                // PrinterName = "YourPrinterName", // optional, default printer if omitted
                    //                Copies = 1
                    //            };
                    //            printDoc.Print(); // sends PDF to printer
                    //        }
                    //    }

                    //    // 5️⃣ Clean up temp file
                    //    File.Delete(tempFile);

                    //}
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
                spoolno.Text = "";
                productionRequest = new ProductionRequest();
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
                msgForm.ShowDialog();
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
    }
}
