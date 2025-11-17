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
    public partial class AddDTYPackingForm : Form
    {
        private static Logger Log = Logger.GetLogger();

        MasterService _masterService = new MasterService();
        ProductionService _productionService = new ProductionService();
        PackingService _packingService = new PackingService();
        SaleService _saleService = new SaleService();
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
        private long _productionId;
        public AddDTYPackingForm()
        {
            InitializeComponent();
            ApplyFonts();
            this.Shown += AddDTYPackingForm_Shown;
            this.AutoScroll = true;
            lblLoading = CommonMethod.InitializeLoadingLabel(this);

            _cmethod.SetButtonBorderRadius(this.submit, 8);
            _cmethod.SetButtonBorderRadius(this.cancelbtn, 8);
            _cmethod.SetButtonBorderRadius(this.saveprint, 8);

            rowMaterial.AutoGenerateColumns = false;
        }

        private void AddDTYPackingForm_Load(object sender, EventArgs e)
        {
            getLotRelatedDetails();

            copyno.Text = "1";
            spoolno.Text = "0";
            spoolwt.Text = "0";
            palletwtno.Text = "0.000";
            grosswtno.Text = "0.000";
            tarewt.Text = "0.000";
            netwt.Text = "0.000";
            wtpercop.Text = "0.000";
            copsstock.Text = "0";
            boxpalletstock.Text = "0";
            copsitemwt.Text = "0";
            boxpalletitemwt.Text = "0";
            frdenier.Text = "0";
            updenier.Text = "0";
            deniervalue.Text = "0";
            twistvalue.Text = "0";
            partyn.Text = "";
            partyshade.Text = "";
            isFormReady = true;
            //this.reportViewer1.RefreshReport();

            prcompany.FlatStyle = FlatStyle.System;
            this.tableLayoutPanel4.SetColumnSpan(this.panel11, 2);
            this.tableLayoutPanel4.SetColumnSpan(this.panel12, 2);
            this.tableLayoutPanel4.SetColumnSpan(this.panel17, 3);
            this.tableLayoutPanel4.SetColumnSpan(this.panel30, 2);
            this.tableLayoutPanel6.SetColumnSpan(this.panel29, 2);
        }

        private void getLotRelatedDetails()
        {
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
            this.boxno.Font = FontManager.GetFont(8F, FontStyle.Bold);
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
            this.PrefixList.Font = FontManager.GetFont(8F, FontStyle.Regular);
            this.machineboxheader.Font = FontManager.GetFont(8F, FontStyle.Regular);
            this.Machinelbl.Font = FontManager.GetFont(9F, FontStyle.Bold);
            this.grosswterror.Font = FontManager.GetFont(7F, FontStyle.Regular);
            this.palletwterror.Font = FontManager.GetFont(7F, FontStyle.Regular);
            this.spoolwterror.Font = FontManager.GetFont(7F, FontStyle.Regular);
            this.spoolnoerror.Font = FontManager.GetFont(7F, FontStyle.Regular);
            this.Weighboxlbl.Font = FontManager.GetFont(9F, FontStyle.Bold);
            this.Packagingboxlbl.Font = FontManager.GetFont(9F, FontStyle.Bold);
            this.cancelbtn.Font = FontManager.GetFont(8F, FontStyle.Bold);
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
        }

        private async void AddDTYPackingForm_Shown(object sender, EventArgs e)
        {
            try
            {
                var machineTask = _masterService.getMachineList("TexturisingLot");
                //var lotTask = _productionService.getAllLotList();
                //var prefixTask = getPrefixList();
                var packsizeTask = _masterService.getPackSizeList();
                var copsitemTask = _masterService.getItemList(itemCopsCategoryId);
                var boxitemTask = _masterService.getItemList(itemBoxCategoryId);
                var deptTask = _masterService.getDepartmentList();
                var ownerTask = _masterService.getOwnerList();

                // 2. Wait for all to complete
                await Task.WhenAll(machineTask, packsizeTask, copsitemTask, boxitemTask, deptTask, ownerTask);

                // 3. Get the results
                var machineList = machineTask.Result;
                //var lotList = lotTask.Result;
                //var prefixList = prefixTask.Result;
                var packsizeList = packsizeTask.Result;
                var copsitemList = copsitemTask.Result;
                var boxitemList = boxitemTask.Result;
                var deptList = deptTask.Result;
                var ownerList = ownerTask.Result;

                //machine
                o_machinesResponse = machineList;
                machineList.Insert(0, new MachineResponse { MachineId = 0, MachineName = "Select Line No." });
                LineNoList.DataSource = machineList;
                LineNoList.DisplayMember = "MachineName";
                LineNoList.ValueMember = "MachineId";
                LineNoList.SelectedIndex = 0;
                LineNoList.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                LineNoList.AutoCompleteSource = AutoCompleteSource.ListItems;

                //lot
                //lotList.Insert(0, new LotsResponse { LotId = 0, LotNoFrmt = "Select MergeNo" });
                //MergeNoList.DataSource = lotList;
                //MergeNoList.DisplayMember = "LotNoFrmt";
                //MergeNoList.ValueMember = "LotId";
                //MergeNoList.SelectedIndex = 0;
                //MergeNoList.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                //MergeNoList.AutoCompleteSource = AutoCompleteSource.ListItems;
                //MergeNoList.DropDownStyle = ComboBoxStyle.DropDown;


                //prefix
                //prefixList.Insert(0, new PrefixResponse { PrefixCode = 0, Prefix = "Select Prefix" });
                //PrefixList.DataSource = prefixList;
                //PrefixList.DisplayMember = "Prefix";
                //PrefixList.ValueMember = "PrefixCode";
                //PrefixList.SelectedIndex = 0;
                //PrefixList.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                //PrefixList.AutoCompleteSource = AutoCompleteSource.ListItems;
                //PrefixList.DropDownStyle = ComboBoxStyle.DropDown;


                //packsize
                packsizeList.Insert(0, new PackSizeResponse { PackSizeId = 0, PackSizeName = "Select Pack Size" });
                PackSizeList.DataSource = packsizeList;
                PackSizeList.DisplayMember = "PackSizeName";
                PackSizeList.ValueMember = "PackSizeId";
                PackSizeList.SelectedIndex = 0;
                PackSizeList.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                PackSizeList.AutoCompleteSource = AutoCompleteSource.ListItems;
                //PackSizeList.DropDownStyle = ComboBoxStyle.DropDown;

                var comportList = await Task.Run(() => getComPortList());
                //comport
                ComPortList.DataSource = comportList;
                ComPortList.SelectedIndex = 0;
                ComPortList.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                ComPortList.AutoCompleteSource = AutoCompleteSource.ListItems;
                //ComPortList.DropDownStyle = ComboBoxStyle.DropDown;

                var weightingList = await Task.Run(() => getWeighingList());
                //weighting
                WeighingList.DataSource = weightingList;
                WeighingList.DisplayMember = "Name";
                WeighingList.ValueMember = "Id";
                WeighingList.SelectedIndex = 0;
                WeighingList.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                WeighingList.AutoCompleteSource = AutoCompleteSource.ListItems;
                //WeighingList.DropDownStyle = ComboBoxStyle.DropDown;


                //copsitem
                copsitemList.Insert(0, new ItemResponse { ItemId = 0, Name = "Select Cops Item" });
                CopsItemList.DataSource = copsitemList;
                CopsItemList.DisplayMember = "Name";
                CopsItemList.ValueMember = "ItemId";
                CopsItemList.SelectedIndex = 0;
                CopsItemList.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                CopsItemList.AutoCompleteSource = AutoCompleteSource.ListItems;
                //CopsItemList.DropDownStyle = ComboBoxStyle.DropDown;


                //boxitem
                boxitemList.Insert(0, new ItemResponse { ItemId = 0, Name = "Select Box/Pallet" });
                BoxItemList.DataSource = boxitemList;
                BoxItemList.DisplayMember = "Name";
                BoxItemList.ValueMember = "ItemId";
                BoxItemList.SelectedIndex = 0;
                BoxItemList.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                BoxItemList.AutoCompleteSource = AutoCompleteSource.ListItems;
                //BoxItemList.DropDownStyle = ComboBoxStyle.DropDown;

                o_departmentResponses = deptList;
                deptList.Insert(0, new DepartmentResponse { DepartmentId = 0, DepartmentName = "Select Dept" });
                DeptList.DataSource = deptList;
                DeptList.DisplayMember = "DepartmentName";
                DeptList.ValueMember = "DepartmentId";
                DeptList.SelectedIndex = 0;
                DeptList.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                DeptList.AutoCompleteSource = AutoCompleteSource.ListItems;

                ownerList.Insert(0, new BusinessPartnerResponse { BusinessPartnerId = 0, LegalName = "Select Owner" });
                OwnerList.DataSource = ownerList;
                OwnerList.DisplayMember = "LegalName";
                OwnerList.ValueMember = "BusinessPartnerId";
                OwnerList.SelectedIndex = 0;
                OwnerList.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                OwnerList.AutoCompleteSource = AutoCompleteSource.ListItems;

                isFormReady = true;

                RefreshLastBoxDetails();
            }
            finally
            {
                lblLoading.Visible = false;
            }
        }

        private async Task LoadProductionDetailsAsync(ProductionResponse prodResponse)
        {
            //productionResponse = Task.Run(() => getProductionById(Convert.ToInt64(_productionId))).Result;
            if (prodResponse != null)
            {
                productionResponse = prodResponse;

                LineNoList.SelectedValue = productionResponse.MachineId;
                DeptList.SelectedValue = productionResponse.DepartmentId;
                MergeNoList.SelectedValue = productionResponse.LotId;
                PrefixList.SelectedValue = productionResponse.PrefixCode;
                //dateTimePicker1.Text = productionResponse.ProductionDate.ToString();
                //dateTimePicker1.Value = productionResponse.ProductionDate;
                SaleOrderList.SelectedValue = productionResponse.SaleOrderItemsId;
                QualityList.SelectedValue = productionResponse.QualityId;
                WindingTypeList.SelectedValue = productionResponse.WindingTypeId;
                PackSizeList.SelectedValue = productionResponse.PackSizeId;
                CopsItemList.SelectedValue = productionResponse.SpoolItemId;
                BoxItemList.SelectedValue = productionResponse.BoxItemId;
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
                //spoolno.Text = productionResponse.Spools.ToString();
                //spoolwt.Text = productionResponse.SpoolsWt.ToString();
                //palletwtno.Text = productionResponse.EmptyBoxPalletWt.ToString();
                //grosswtno.Text = productionResponse.GrossWt.ToString();
                //tarewt.Text = productionResponse.TareWt.ToString();
                //netwt.Text = productionResponse.NetWt.ToString();
                OwnerList.SelectedValue = productionResponse.OwnerId;
                LineNoList_SelectedIndexChanged(LineNoList, EventArgs.Empty);
                //MergeNoList_SelectedIndexChanged(MergeNoList, EventArgs.Empty);
                //PackSizeList_SelectedIndexChanged(PackSizeList, EventArgs.Empty);
                //CopsItemList_SelectedIndexChanged(CopsItemList, EventArgs.Empty);
                //BoxItemList_SelectedIndexChanged(BoxItemList, EventArgs.Empty);
            }
        }

        private async void LineNoList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isFormReady) return; // skip during load

            //if (LineNoList.DroppedDown) return;
            if (LineNoList.Items.Count == 0) return;

            if (LineNoList.SelectedIndex <= 0)
            {
                return;
            }
            if (LineNoList.SelectedIndex > 0)
            {
                //linenoerror.Text = "";
                //linenoerror.Visible = false;
            }
            lblLoading.Visible = true;
            try
            {
                if (LineNoList.SelectedValue != null)
                {
                    //linenoerror.Visible = false;
                    MachineResponse selectedMachine = (MachineResponse)LineNoList.SelectedItem;
                    int selectedMachineId = selectedMachine.MachineId;
                    if (selectedMachineId > 0)
                    {
                        productionRequest.MachineId = selectedMachineId;
                        // Call API to get department info by MachineId
                        //var department = await Task.Run(() => _masterService.getMachineById(selectedMachineId));

                        if (selectedMachine != null)
                        {
                            DeptList.SelectedValue = selectedMachine.DepartmentId;
                            var filteredDepts = o_departmentResponses.Where(m => m.DepartmentId == selectedMachine.DepartmentId).ToList();
                            filteredDepts.Insert(0, new DepartmentResponse { DepartmentId = 0, DepartmentName = "Select Dept" });
                            DeptList.DataSource = filteredDepts;
                            DeptList.DisplayMember = "DepartmentName";
                            DeptList.ValueMember = "DepartmentId";
                            if (DeptList.Items.Count > 1)
                            {
                                DeptList.SelectedIndex = 1;
                            }
                            DeptList.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                            DeptList.AutoCompleteSource = AutoCompleteSource.ListItems;
                            DeptList_SelectedIndexChanged(DeptList, EventArgs.Empty);
                        }
                        var getLots = _productionService.getLotList(selectedMachineId).Result;
                        getLots.Insert(0, new LotsResponse { LotId = 0, LotNoFrmt = "Select MergeNo" });
                        MergeNoList.DataSource = getLots;
                        MergeNoList.DisplayMember = "LotNoFrmt";
                        MergeNoList.ValueMember = "LotId";
                        MergeNoList.SelectedIndex = 0;
                        MergeNoList.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                        MergeNoList.AutoCompleteSource = AutoCompleteSource.ListItems;

                        if (_productionId > 0 && productionResponse != null)
                        {
                            MergeNoList.SelectedValue = productionResponse.LotId;
                            DeptList.SelectedValue = productionResponse.DepartmentId;
                        }
                    }

                }
            }
            finally
            {
                lblLoading.Visible = false;
            }
        }

        private async void MergeNoList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isFormReady) return;

            //if (MergeNoList.DroppedDown) return;
            if (MergeNoList.Items.Count == 0) return;

            if (MergeNoList.SelectedIndex <= 0)
            {
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
                getLotRelatedDetails();
                rowMaterial.Columns.Clear();
                totalProdQty = 0;
                selectedSOId = 0;
                totalSOQty = 0;
                balanceQty = 0;
                MergeNoList.SelectedIndex = 0;
                return;
            }
            if (MergeNoList.SelectedIndex > 0)
            {
                //mergenoerror.Text = "";
                //mergenoerror.Visible = false;
            }
            lblLoading.Visible = true;
            try
            {
                if (MergeNoList.SelectedValue != null)
                {
                    //mergenoerror.Visible = false;
                    LotsResponse selectedLot = (LotsResponse)MergeNoList.SelectedItem;
                    int selectedLotId = selectedLot.LotId;

                    productionRequest.LotId = selectedLot.LotId;
                    if (selectedLotId > 0)
                    {
                        selectLotId = selectedLotId;

                        lotResponse = _productionService.getLotById(selectedLotId).Result;
                        if (lotResponse != null)
                        {
                            itemname.Text = (!string.IsNullOrEmpty(lotResponse.ItemName)) ? lotResponse.ItemName : "";
                            shadename.Text = (!string.IsNullOrEmpty(lotResponse.ShadeName)) ? lotResponse.ShadeName : "";
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
                                var itemResponse = _masterService.getItemById(lotResponse.ItemId).Result;
                                if (itemResponse != null)
                                {
                                    var qualityList = _masterService.getQualityListByItemTypeId(itemResponse.ItemTypeId).Result;
                                    qualityList.Insert(0, new QualityResponse { QualityId = 0, Name = "Select Quality" });
                                    QualityList.DataSource = qualityList;
                                    QualityList.DisplayMember = "Name";
                                    QualityList.ValueMember = "QualityId";
                                    QualityList.SelectedIndex = 0;
                                    QualityList.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                                    QualityList.AutoCompleteSource = AutoCompleteSource.ListItems;
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
                                }

                            }
                        }

                        var getWindingType = new List<WindingTypeResponse>();
                        getWindingType = _productionService.getWinderTypeList(selectedLotId).Result;
                        getWindingType.Insert(0, new WindingTypeResponse { WindingTypeId = 0, WindingTypeName = "Select Winding Type" });
                        if (getWindingType.Count <= 1)
                        {
                            getWindingType = _masterService.getWindingTypeList().Result;
                            getWindingType.Insert(0, new WindingTypeResponse { WindingTypeId = 0, WindingTypeName = "Select Winding Type" });

                        }
                        WindingTypeList.DataSource = getWindingType;
                        WindingTypeList.DisplayMember = "WindingTypeName";
                        WindingTypeList.ValueMember = "WindingTypeId";
                        WindingTypeList.SelectedIndex = 0;
                        WindingTypeList.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                        WindingTypeList.AutoCompleteSource = AutoCompleteSource.ListItems;

                        var getSaleOrder = _productionService.getSaleOrderList(selectedLotId).Result;
                        getSaleOrder.Insert(0, new LotSaleOrderDetailsResponse { SaleOrderItemsId = 0, ItemName = "Select Sale Order Item" });
                        SaleOrderList.DataSource = getSaleOrder;
                        SaleOrderList.DisplayMember = "ItemName";
                        SaleOrderList.ValueMember = "SaleOrderItemsId";
                        SaleOrderList.SelectedIndex = 0;
                        SaleOrderList.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                        SaleOrderList.AutoCompleteSource = AutoCompleteSource.ListItems;
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

                        lotsDetailsList = new List<LotsDetailsResponse>();
                        productionRequest.ProductionDate = dateTimePicker1.Value;
                        lotsDetailsList = _productionService.getLotsDetailsByLotsIdAndProductionDate(selectedLotId, productionRequest.ProductionDate).Result;
                        if (lotsDetailsList.Count > 0)
                        {
                            //foreach (var lot in lotResponse.LotsDetailsResponses)
                            //{
                            //    LotsDetailsResponse lotsDetails = new LotsDetailsResponse();
                            //    lotsDetails.LotId = lot.LotId;
                            //    lotsDetails.UpdatedOn = lot.UpdatedOn;
                            //    lotsDetails.UpdatedBy = lot.UpdatedBy;
                            //    lotsDetails.CreatedBy = lot.CreatedBy;
                            //    lotsDetails.CreatedOn = lot.CreatedOn;
                            //    lotsDetails.EffectiveFrom = lot.EffectiveFrom;
                            //    lotsDetails.EffectiveUpto = lot.EffectiveUpto;
                            //    lotsDetails.GainLossPerc = lot.GainLossPerc;
                            //    lotsDetails.InputPerc = lot.InputPerc;
                            //    lotsDetails.ProductionPerc = lot.ProductionPerc;
                            //    lotsDetails.Extruder = lot.Extruder;
                            //    lotsDetails.LotType = lot.LotType;
                            //    lotsDetails.PrevLotId = lot.PrevLotId;
                            //    lotsDetails.PrevLotNo = lot.PrevLotNo;
                            //    lotsDetails.PrevLotType = lot.PrevLotType;
                            //    lotsDetails.PrevLotQuality = lot.PrevLotQuality;
                            //    lotsDetails.PrevLotItemName = lot.PrevLotItemName;
                            //    lotsDetails.PrevLotShadeName = lot.PrevLotShadeName;
                            //    lotsDetails.PrevLotShadeCode = lot.PrevLotShadeCode;
                            //    lotsDetailsList.Add(lot);
                            //}
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

                        if (_productionId > 0 && productionResponse != null)
                        {
                            SaleOrderList.SelectedValue = productionResponse.SaleOrderItemsId;
                            SaleOrderList_SelectedIndexChanged(SaleOrderList, EventArgs.Empty);
                        }
                    }

                }
            }
            finally
            {
                lblLoading.Visible = false;
            }
        }

        private async void PackSizeList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isFormReady) return;

            //if (PackSizeList.DroppedDown) return;

            if (PackSizeList.SelectedIndex <= 0)
            {
                frdenier.Text = "0";
                updenier.Text = "0";
                return;
            }
            if (PackSizeList.SelectedIndex > 0)
            {
                //packsizeerror.Text = "";
                //packsizeerror.Visible = false;
            }
            lblLoading.Visible = true;
            try
            {
                if (PackSizeList.SelectedValue != null)
                {
                    //packsizeerror.Visible = false;

                    PackSizeResponse selectedPacksize = (PackSizeResponse)PackSizeList.SelectedItem;
                    int selectedPacksizeId = selectedPacksize.PackSizeId;

                    productionRequest.PackSizeId = selectedPacksizeId;
                    if (selectedPacksizeId > 0)
                    {
                        var packsize = _masterService.getPackSizeById(selectedPacksizeId).Result;
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
        }

        private void QualityList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isFormReady) return;

            //if (QualityList.DroppedDown) return;

            if (QualityList.SelectedIndex > 0)
            {
                //qualityerror.Text = "";
                //qualityerror.Visible = false;
            }
            if (QualityList.SelectedValue != null)
            {
                //qualityerror.Visible = false;

                QualityResponse selectedQuality = (QualityResponse)QualityList.SelectedItem;
                int selectedQualityId = selectedQuality.QualityId;

                productionRequest.QualityId = selectedQualityId;
            }
        }

        private void WindingTypeList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isFormReady) return;

            //if (WindingTypeList.DroppedDown) return;

            if (WindingTypeList.SelectedIndex > 0)
            {
                //windingerror.Text = "";
                //windingerror.Visible = false;
            }
            lblLoading.Visible = true;
            try
            {
                if (WindingTypeList.SelectedValue != null)
                {
                    //windingerror.Visible = false;

                    WindingTypeResponse selectedWindingType = (WindingTypeResponse)WindingTypeList.SelectedItem;
                    int selectedWindingTypeId = selectedWindingType.WindingTypeId;

                    if (selectedWindingTypeId > 0)
                    {
                        productionRequest.WindingTypeId = selectedWindingTypeId;
                    }

                    //if (_productionId > 0 && productionResponse != null)
                    //{
                    //    WindingTypeList.SelectedValue = productionResponse.WindingTypeId;
                    //}
                }
            }
            finally
            {
                lblLoading.Visible = false;
            }
        }

        private async void SaleOrderList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isFormReady) return;

            //if (SaleOrderList.DroppedDown) return;

            if (SaleOrderList.SelectedIndex > 0)
            {
                //soerror.Text = "";
                //soerror.Visible = false;
            }
            lblLoading.Visible = true;
            try
            {
                if (SaleOrderList.SelectedValue != null)
                {
                    //soerror.Visible = false;

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

                        //var saleItemResponse = await getSaleOrderItemById(selectedSaleOrderId);

                        //foreach (var soitem in saleResponse.saleOrderItemsResponses)
                        //{
                        totalSOQty = selectedSaleOrder.Quantity;
                        //}

                        RefreshGradewiseGrid();
                        //RefreshLastBoxDetails();

                        if (_productionId > 0 && productionResponse != null)
                        {
                            WindingTypeList.SelectedValue = productionResponse.WindingTypeId;
                            WindingTypeList_SelectedIndexChanged(WindingTypeList, EventArgs.Empty);
                        }
                    }

                }
            }
            finally
            {
                lblLoading.Visible = false;
            }
        }

        private async void RefreshGradewiseGrid()
        {
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

                //totalProdQty = 0;
                //foreach (var proditem in gridList)
                //{
                //    totalProdQty += proditem.GrossWt;
                //}
                //balanceQty = (totalSOQty - totalProdQty);
                //if (balanceQty <= 0)
                //{
                //    submit.Enabled = false;
                //    saveprint.Enabled = false;
                //    MessageBox.Show("Quantity not remaining for " + selectedSONumber, "Warning", MessageBoxButtons.OK);
                //    ResetForm(this);
                //}
                //else
                //{
                //    submit.Enabled = true;
                //    saveprint.Enabled = true;
                //}
            }
        }

        private async void RefreshLastBoxDetails()
        {
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
        }

        private void ComPortList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isFormReady) return;

            //if (ComPortList.DroppedDown) return;

            if (ComPortList.SelectedValue != null)
            {
                var ComPort = ComPortList.SelectedValue.ToString();
                comPort = ComPortList.SelectedValue.ToString();
            }
        }

        private void WeighingList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isFormReady) return;

            //if (WeighingList.DroppedDown) return;

            if (WeighingList.SelectedValue != null)
            {
                WeighingItem selectedWeighingScale = (WeighingItem)WeighingList.SelectedItem;
                int selectedScaleId = selectedWeighingScale.Id;

                //if (selectedScaleId >= 0)
                //{
                //    var readWeight = wtReader.ReadWeight(comPort, selectedScaleId);
                //    grosswtno.Text = readWeight.ToString();
                //    grosswtno.ReadOnly = true;
                //}

            }
        }

        private async void CopsItemList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isFormReady) return;

            //if (CopsItemList.DroppedDown) return;

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

                        var itemResponse = _masterService.getItemById(selectedItemId).Result;
                        if (itemResponse != null)
                        {
                            copsitemwt.Text = itemResponse.Weight.ToString();
                            //spoolwt.Text = itemResponse.Weight.ToString();
                            SpoolNo_TextChanged(sender, e);
                            //GrossWeight_TextChanged(sender, e);
                        }
                    }
                }
            }
            finally
            {
                lblLoading.Visible = false;
            }
        }

        private async void BoxItemList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isFormReady) return;

            //if (BoxItemList.DroppedDown) return;

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
                        var itemResponse = _masterService.getItemById(selectedBoxItemId).Result;
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
        }

        private void PrefixList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isFormReady) return;

            //if (PrefixList.DroppedDown) return;

            if (PrefixList.SelectedIndex <= 0)
            {
                prodtype.Text = "";
                return;
            }

            if (PrefixList.SelectedIndex > 0)
            {
                //boxnoerror.Text = "";
                //boxnoerror.Visible = false;
            }

            if (PrefixList.SelectedValue != null)
            {
                //boxnoerror.Visible = false;

                PrefixResponse selectedPrefix = (PrefixResponse)PrefixList.SelectedItem;
                int selectedPrefixId = selectedPrefix.PrefixCode;

                productionRequest.PrefixCode = selectedPrefixId;

                if (selectedPrefix.ProductionType.ToString() != null)
                {
                    prodtype.Text = selectedPrefix.ProductionType.ToString();
                    productionRequest.ProdTypeId = selectedPrefix.ProductionTypeId;
                }

            }
        }

        private async void DeptList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isFormReady) return;

            //if (DeptList.DroppedDown) return;

            if (DeptList.SelectedIndex <= 0)
            {
                return;
            }
            if (DeptList.SelectedIndex > 0)
            {
                //packsizeerror.Text = "";
                //packsizeerror.Visible = false;
            }
            lblLoading.Visible = true;
            try
            {
                if (DeptList.SelectedValue != null)
                {
                    //packsizeerror.Visible = false;

                    DepartmentResponse selectedDepartment = (DepartmentResponse)DeptList.SelectedItem;
                    int selectedDepartmentId = selectedDepartment.DepartmentId;

                    if (selectedDepartment != null && productionRequest.MachineId == 0)
                    {
                        var machineList = _masterService.getMachineByDepartmentIdAndLotType(selectedDepartmentId,"TexturisingLot").Result;

                        //var filteredMachine = machineList.Where(m => m.DepartmentId == selectedDepartment.DepartmentId).ToList();
                        //LineNoList.SelectedValue = selectedDepartment;
                        machineList.Insert(0, new MachineResponse { MachineId = 0, MachineName = "Select Line No." });
                        LineNoList.DataSource = machineList;
                        //LineNoList.SelectedValue = productionResponse.MachineId;
                    }

                    productionRequest.DepartmentId = selectedDepartmentId;

                    prefixRequest.DepartmentId = selectedDepartmentId;
                    prefixRequest.TxnFlag = "DTY";
                    prefixRequest.TransactionTypeId = 5;
                    prefixRequest.ProductionTypeId = 1;
                    prefixRequest.Prefix = "";
                    prefixRequest.FinYearId = SessionManager.FinYearId;

                    List<PrefixResponse> prefixList = await _masterService.getPrefixList(prefixRequest);
                    prefixList.Insert(0, new PrefixResponse { PrefixCode = 0, Prefix = "Select Prefix" });
                    PrefixList.DataSource = prefixList;
                    PrefixList.DisplayMember = "Prefix";
                    PrefixList.ValueMember = "PrefixCode";
                    PrefixList.SelectedIndex = 0;
                    PrefixList.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    PrefixList.AutoCompleteSource = AutoCompleteSource.ListItems;
                    if (PrefixList.Items.Count == 2)
                    {
                        PrefixList.SelectedIndex = 1;   // Select the single record
                        PrefixList_SelectedIndexChanged(PrefixList, EventArgs.Empty);
                    }
                    else
                    {
                        PrefixList.SelectedIndex = 0;  // Optional: no default selection
                    }

                    //if (_productionId > 0 && productionResponse != null)
                    //{
                    //    PrefixList.SelectedValue = productionResponse.PrefixCode;
                    //    PrefixList_SelectedIndexChanged(PrefixList, EventArgs.Empty);
                    //}
                }
            }
            finally
            {
                lblLoading.Visible = false;
            }
        }

        private async void OwnerList_SelectedIndexChanged(object sender, EventArgs e)
        {
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
        }

        //private async Task<List<MachineResponse>> getMachineList()
        //{
        //    return _masterService.getMachineList("TexturisingLot");
        //}

        //private async Task<List<LotsResponse>> getAllLotList()
        //{
        //    return _productionService.getAllLotList();
        //}

        //private async Task<List<QualityResponse>> getQualityListByItemTypeId(int itemTypeId)
        //{
        //    return _masterService.getQualityListByItemTypeId(itemTypeId);
        //}

        //private async Task<List<PackSizeResponse>> getPackSizeList()
        //{
        //    return _masterService.getPackSizeList();
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

        private async Task<List<WeighingItem>> getWeighingList()
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

        //private async Task<List<ItemResponse>> getCopeItemList(int categoryId)
        //{
        //    return _masterService.getItemList(categoryId);
        //}

        //private async Task<List<ItemResponse>> getBoxItemList(int categoryId)
        //{
        //    return _masterService.getItemList(categoryId);
        //}

        //private Task<ProductionResponse> getProductionById(long productionId)
        //{
        //    return Task.Run(() => _packingService.getProductionById(productionId));
        //}

        //private async Task<List<BusinessPartnerResponse>> getOwnerList()
        //{
        //    return _masterService.getOwnerList();
        //}

        //private async Task<List<ProductionResponse>> getProductionLotIdandSaleOrderItemIdandPackingType(int lotId, int saleOrderItemId)
        //{
        //    return _packingService.getAllByLotIdandSaleOrderItemIdandPackingType(lotId, saleOrderItemId);
        //}

        //private async Task<ProductionResponse> getLastBoxDetails()
        //{
        //    return _packingService.getLastBoxDetails("dtypacking");
        //}

        //private async Task<List<DepartmentResponse>> getDepartmentList()
        //{
        //    return _masterService.getDepartmentList();
        //}

        private void SpoolWeight_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(spoolwt.Text))
            {
                //spoolwterror.Visible = true;
                CalculateTareWeight();
            }
            else
            {
                CalculateTareWeight();
                //spoolwterror.Text = "";
                //spoolwterror.Visible = false;
            }
        }

        private void PalletWeight_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(palletwtno.Text))
            {
                //palletwterror.Visible = true;
                CalculateTareWeight();
            }
            else
            {
                //palletwterror.Text = "";
                //palletwterror.Visible = false;
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
                        //grosswterror.Text = "";
                        grosswterror.Visible = false;
                    }
                    else
                    {
                        //grosswterror.Text = "Gross Wt > Tare Wt";
                        //grosswterror.Visible = true;
                        //if (grosswterror.Visible)
                        //{
                        //    MessageBox.Show("Gross Wt > Tare Wt", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        //    netwt.Text = "0";
                        //    wtpercop.Text = "0";
                        //}
                    }
                }
            }

        }

        private void GrossWeight_Validating(object sender, CancelEventArgs e)
        {
            if (!isFormReady) return;

            //if (selectedSOId == 0)
            //{
            //    //if (soerror.Visible)
            //    //{
            //        //soerror.Text = "Please select sale order";
            //        MessageBox.Show("Please select sale order", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    //}
            //    e.Cancel = true;
            //    return;
            //}
            if (string.IsNullOrWhiteSpace(grosswtno.Text))
            {
                //grosswterror.Visible = true;
                //grosswterror.Text = "Please enter gross weight";
                MessageBox.Show("Please enter gross weight", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true;
                return;
            }
            else
            {
                soerror.Visible = false;
                //soerror.Text = "";
                if (!string.IsNullOrWhiteSpace(tarewt.Text))
                {
                    decimal gross, tare;
                    if (decimal.TryParse(grosswtno.Text, out gross) && decimal.TryParse(tarewt.Text, out tare))
                    {
                        //decimal newBalanceQty = balanceQty - gross;
                        //if (newBalanceQty < 0)
                        //{
                        //    //grosswterror.Text = "No Prod Bal Qty remaining";
                        //    //grosswterror.Visible = true;
                        //    MessageBox.Show("No Prod Bal Qty remaining", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        //    submit.Enabled = false;
                        //    saveprint.Enabled = false;
                        //    e.Cancel = true;
                        //    return; 
                        //}
                        //else
                        //{
                        //    //grosswterror.Text = "";
                        //    //grosswterror.Visible = false;
                        //    submit.Enabled = true;
                        //    saveprint.Enabled = true;
                        //}
                        if (gross >= tare)
                        {
                            CalculateNetWeight();
                            //grosswterror.Text = "";
                            //grosswterror.Visible = false;
                        }
                        else
                        {
                            //grosswterror.Text = "Gross Wt > Tare Wt";
                            //grosswterror.Visible = true;
                            //MessageBox.Show("Gross Wt > Tare Wt", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            //netwt.Text = "0";
                            //wtpercop.Text = "0";
                            //e.Cancel = true;
                            //return;
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
            //if (string.IsNullOrWhiteSpace(spoolno.Text))
            //{
            //    //spoolnoerror.Text = "Please enter spool no";
            //    //spoolnoerror.Visible = true;
            //    MessageBox.Show("Please enter spool no", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    tarewt.Text = "0";
            //    spoolwt.Text = "0";
            //    return;
            //}
            //else
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
                    //GrossWeight_Validating(sender, new CancelEventArgs());
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
                //if (num1 >= num2)
                //{
                wtpercop.Text = (num1 / num2).ToString("F3");
                //}
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

        private async void submit_Click(object sender, EventArgs e)
        {
            submitForm(false);
        }

        private async void saveprint_Click(object sender, EventArgs e)
        {
            submitForm(true);
        }

        public async void submitForm(bool isPrint)
        {
            if (ValidateForm())
            {
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
                    //consumptionDetailsRequest.StockTrfDetailsId = 0;
                    productionRequest.ConsumptionDetailsRequest.Add(consumptionDetailsRequest);
                }

                ProductionResponse result = SubmitPacking(productionRequest, isPrint);
            }
        }

        public ProductionResponse SubmitPacking(ProductionRequest productionRequest, bool isPrint)
        {
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

                //MessageBox.Show("DTY Packing added successfully for BoxNo " + result.BoxNo + ".",
                //"Success",
                //MessageBoxButtons.OK,
                //MessageBoxIcon.Information);
                ShowCustomMessage(result.BoxNoFmtd);
                isFormReady = false;
                this.spoolno.Text = "0";
                this.spoolwt.Text = "0";
                this.grosswtno.Text = "";
                this.tarewt.Text = "";
                this.netwt.Text = "";
                this.wtpercop.Text = "";
                isFormReady = true;
                this.spoolno.Focus();
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
                submit.Enabled = true;
                saveprint.Enabled = true;
                MessageBox.Show("Something went wrong.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            return result;
        }

        private bool ValidateForm()
        {
            bool isValid = true;

            if (LineNoList.SelectedIndex <= 0)
            {
                //linenoerror.Text = "Please select a line no";
                //linenoerror.Visible = true;
                MessageBox.Show("Please select a line no", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                isValid = false;
            }
            if (string.IsNullOrWhiteSpace(copyno.Text))
            {
                //copynoerror.Text = "Please enter no of copies";
                //copynoerror.Visible = true;
                MessageBox.Show("Please enter no of copies", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                isValid = false;
            }

            if (MergeNoList.SelectedIndex <= 0)
            {
                //mergenoerror.Text = "Please select merge no";
                //mergenoerror.Visible = true;
                MessageBox.Show("Please select merge no", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                isValid = false;
            }

            if (QualityList.SelectedIndex <= 0)
            {
                //qualityerror.Text = "Please select quality";
                //qualityerror.Visible = true;
                MessageBox.Show("Please select quality", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                isValid = false;
            }

            if (SaleOrderList.SelectedIndex <= 0)
            {
                //soerror.Text = "Please select sale order";
                //soerror.Visible = true;
                MessageBox.Show("Please select sale order", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                isValid = false;
            }

            if (PackSizeList.SelectedIndex <= 0)
            {
                //packsizeerror.Text = "Please select pack size";
                //packsizeerror.Visible = true;
                MessageBox.Show("Please select pack size", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                isValid = false;
            }

            if (WindingTypeList.SelectedIndex <= 0)
            {
                //windingerror.Text = "Please select winding type";
                //windingerror.Visible = true;
                MessageBox.Show("Please select winding type", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                isValid = false;
            }

            if (PrefixList.SelectedIndex <= 0)
            {
                //boxnoerror.Text = "Please select prefix";
                //boxnoerror.Visible = true;
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
                //spoolnoerror.Text = "Please enter spool no";
                //spoolnoerror.Visible = true;
                MessageBox.Show("Please enter spool no", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                isValid = false;
            }

            if (string.IsNullOrWhiteSpace(spoolwt.Text) || Convert.ToDecimal(spoolwt.Text) == 0)
            {
                //spoolwterror.Text = "Please enter spool wt";
                //spoolwterror.Visible = true;
                MessageBox.Show("Please enter spool wt", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                isValid = false;
            }

            if (string.IsNullOrWhiteSpace(palletwtno.Text) || Convert.ToDecimal(palletwtno.Text) == 0)
            {
                //palletwterror.Text = "Please enter pallet wt";
                //palletwterror.Visible = true;
                MessageBox.Show("Please enter pallet wt", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                isValid = false;
            }

            if (string.IsNullOrWhiteSpace(grosswtno.Text) || Convert.ToDecimal(grosswtno.Text) == 0)
            {
                //grosswterror.Text = "Please enter gross wt";
                //grosswterror.Visible = true;
                MessageBox.Show("Please enter gross wt", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                isValid = false;
            }
            decimal spoolnum = 0;
            decimal.TryParse(spoolno.Text, out spoolnum);
            if (spoolnum == 0)
            {
                MessageBox.Show("Spool no > 0", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //tarewt.Text = "0";
                //spoolwt.Text = "0";
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
            //totalProdQty = 0;
            //foreach (var proditem in gridList)
            //{
            //    totalProdQty += proditem.GrossWt;
            //}
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

            return isValid;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            //var dashboard = this.ParentForm as AdminAccount;
            //if (dashboard != null)
            //{
            //    dashboard.LoadFormInContent(new POYPackingList());
            //}
            ResetForm(this);
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

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow control keys (backspace, delete, etc.)
            //if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            //{
            //    e.Handled = true; // Reject the input
            //}
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
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.V) // Ctrl+V paste
            {
                ((System.Windows.Forms.TextBox)sender).Clear(); // clear existing value before paste
            }
        }

        private void checkBox1_KeyDown(object sender, KeyEventArgs e)
        {
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
        }

        private void LineNoList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.ShiftKey) // Detect Shift key
            {
                LineNoList.DroppedDown = true; // Open the dropdown list
                e.SuppressKeyPress = true;    // Prevent any side effect
            }
            if (e.KeyCode == Keys.Escape)
            {
                LineNoList.DroppedDown = false;
            }
        }

        private void MergeNoList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.ShiftKey) // Detect Shift key
            {
                MergeNoList.DroppedDown = true; // Open the dropdown list
                e.SuppressKeyPress = true;    // Prevent any side effect
            }
            if (e.KeyCode == Keys.Escape)
            {
                MergeNoList.DroppedDown = false;
            }
        }

        private void PackSizeList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.ShiftKey) // Detect Shift key
            {
                PackSizeList.DroppedDown = true; // Open the dropdown list
                e.SuppressKeyPress = true;    // Prevent any side effect
            }
            if (e.KeyCode == Keys.Escape)
            {
                PackSizeList.DroppedDown = false;
            }
        }

        private void QualityList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.ShiftKey) // Detect Shift key
            {
                QualityList.DroppedDown = true; // Open the dropdown list
                e.SuppressKeyPress = true;    // Prevent any side effect
            }
            if (e.KeyCode == Keys.Escape)
            {
                QualityList.DroppedDown = false;
            }
        }

        private void SaleOrderList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.ShiftKey) // Detect Shift key
            {
                SaleOrderList.DroppedDown = true; // Open the dropdown list
                e.SuppressKeyPress = true;    // Prevent any side effect
            }
            if (e.KeyCode == Keys.Escape)
            {
                SaleOrderList.DroppedDown = false;
            }
        }

        private void PrefixList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.ShiftKey) // Detect Shift key
            {
                PrefixList.DroppedDown = true; // Open the dropdown list
                e.SuppressKeyPress = true;    // Prevent any side effect
            }
            if (e.KeyCode == Keys.Escape)
            {
                PrefixList.DroppedDown = false;
            }
        }

        private void WindingTypeList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.ShiftKey) // Detect Shift key
            {
                WindingTypeList.DroppedDown = true; // Open the dropdown list
                e.SuppressKeyPress = true;    // Prevent any side effect
            }
            if (e.KeyCode == Keys.Escape)
            {
                WindingTypeList.DroppedDown = false;
            }
        }

        private void ComPortList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.ShiftKey) // Detect Shift key
            {
                ComPortList.DroppedDown = true; // Open the dropdown list
                e.SuppressKeyPress = true;    // Prevent any side effect
            }
            if (e.KeyCode == Keys.Escape)
            {
                ComPortList.DroppedDown = false;
            }
        }

        private void WeighingList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.ShiftKey) // Detect Shift key
            {
                WeighingList.DroppedDown = true; // Open the dropdown list
                e.SuppressKeyPress = true;    // Prevent any side effect
            }
            if (e.KeyCode == Keys.Escape)
            {
                WeighingList.DroppedDown = false;
            }
        }

        private void CopsItemList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.ShiftKey) // Detect Shift key
            {
                CopsItemList.DroppedDown = true; // Open the dropdown list
                e.SuppressKeyPress = true;    // Prevent any side effect
            }
            if (e.KeyCode == Keys.Escape)
            {
                CopsItemList.DroppedDown = false;
            }
        }

        private void BoxItemList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.ShiftKey) // Detect Shift key
            {
                BoxItemList.DroppedDown = true; // Open the dropdown list
                e.SuppressKeyPress = true;    // Prevent any side effect
            }
            if (e.KeyCode == Keys.Escape)
            {
                BoxItemList.DroppedDown = false;
            }
        }

        private void DeptList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.ShiftKey) // Detect Shift key
            {
                DeptList.DroppedDown = true; // Open the dropdown list
                e.SuppressKeyPress = true;    // Prevent any side effect
            }
            if (e.KeyCode == Keys.Escape)
            {
                DeptList.DroppedDown = false;
            }
        }

        private void OwnerList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.ShiftKey) // Detect Shift key
            {
                OwnerList.DroppedDown = true; // Open the dropdown list
                e.SuppressKeyPress = true;    // Prevent any side effect
            }
            if (e.KeyCode == Keys.Escape)
            {
                OwnerList.DroppedDown = false;
            }
        }

        private void ResetForm(Control parent)
        {
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

                    //else if (c is System.Windows.Forms.CheckBox)
                    //    ((System.Windows.Forms.CheckBox)c).Checked = false;

                    //else if (c is System.Windows.Forms.RadioButton)
                    //    ((System.Windows.Forms.RadioButton)c).Checked = false;

                    // Recursive call if the control has children (like Panels, GroupBoxes, etc.)
                    //if (c.HasChildren)
                    //    ResetForm(c);
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
                lotResponse = new LotsResponse();
                lotsDetailsList = new List<LotsDetailsResponse>();
                getLotRelatedDetails();
                rowMaterial.Columns.Clear();
                totalProdQty = 0;
                selectedSOId = 0;
                totalSOQty = 0;
                balanceQty = 0;
                prcompany.Checked = false;
                prowner.Checked = false;

                DeptList.DataSource = null;
                DeptList.Items.Clear();
                DeptList.Items.Add("Select Dept");
                DeptList.SelectedIndex = 0;

                MergeNoList.DataSource = null;
                MergeNoList.Items.Clear();
                MergeNoList.Items.Add("Select MergeNo");
                MergeNoList.SelectedIndex = 0;

                LineNoList.SelectedIndex = 0;

                PackSizeList.SelectedIndex = 0;

                CopsItemList.SelectedIndex = 0;

                BoxItemList.SelectedIndex = 0;

                ComPortList.SelectedIndex = 0;

                WeighingList.SelectedIndex = 0;

                OwnerList.SelectedIndex = 0;

                PrefixList.SelectedIndex = 0;

                isFormReady = false;
                spoolno.Text = "";
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
        }

        private void prcompany_CheckedChanged(object sender, EventArgs e)
        {
            if (!isFormReady) return;

            if (prcompany.Checked)
            {
                prowner.Checked = false;
                //prowner.Enabled = false; // disable the other
                prcompany.Focus();       // keep focus on the current one
            }
            //else
            //{
            //    prowner.Enabled = true;  // re-enable when unchecked
            //}
        }

        private void prowner_CheckedChanged(object sender, EventArgs e)
        {
            if (!isFormReady) return;

            if (prowner.Checked)
            {
                prcompany.Checked = false;
                //prcompany.Enabled = false; // disable the other
                prowner.Focus();           // keep focus
            }
            //else
            //{
            //    prcompany.Enabled = true;  // re-enable when unchecked
            //}
        }

        private void txtNumeric_KeyPress(object sender, KeyPressEventArgs e)
        {
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
        }

        private void Control_EnterKeyMoveNext(object sender, KeyEventArgs e)
        {
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
        }

        private void spoolNo_Enter(object sender, EventArgs e)
        {
            // When control gets focus
            if (spoolno.Text == "0")
            {
                spoolno.Clear(); // remove the default value
            }
        }

        private void spoolNo_Leave(object sender, EventArgs e)
        {
            // When control loses focus
            if (string.IsNullOrWhiteSpace(spoolno.Text))
            {
                spoolno.Text = "0"; // restore default
            }
        }

        private void ShowCustomMessage(string boxNo)
        {
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
                    Text = $"DTY Packing added successfully for BoxNo {boxNo}.",
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
        }

        private void ComboBox_Leave(object sender, EventArgs e)
        {
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
                // cmb.Text = ""; // clear invalid entry
            }
        }

        private void txtNumeric_Leave(object sender, EventArgs e)
        {
            FormatToThreeDecimalPlaces(sender as TextBox);
        }
        private void FormatToThreeDecimalPlaces(TextBox textBox)
        {
            if (decimal.TryParse(textBox.Text, out decimal value))
                textBox.Text = value.ToString("0.000");
            else
                textBox.Text = "0.000"; // optional fallback
        }
    }
}