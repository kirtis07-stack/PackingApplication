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
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PackingApplication
{
    public partial class ViewChipsPackingForm : Form
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
        public ViewChipsPackingForm()
        {
            InitializeComponent();
            ApplyFonts();
            this.Shown += ViewChipsPackingForm_Shown;
            this.AutoScroll = true;
            lblLoading = CommonMethod.InitializeLoadingLabel(this);
        }

        private void ViewChipsPackingForm_Load(object sender, EventArgs e)
        {
            getLotRelatedDetails();

            copyno.Text = "1";
            palletwtno.Text = "0";
            grosswtno.Text = "0";
            tarewt.Text = "0";
            netwt.Text = "0";
            wtpercop.Text = "0";
            boxpalletstock.Text = "0";
            boxpalletitemwt.Text = "0";
            frdenier.Text = "0";
            updenier.Text = "0";
            deniervalue.Text = "0";
            isFormReady = true;
            //this.reportViewer1.RefreshReport();

            this.prcompany.Enabled = true;
        }

        private void getLotRelatedDetails()
        {
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
            this.Weighboxlbl.Font = FontManager.GetFont(9F, FontStyle.Bold);
            this.Packagingboxlbl.Font = FontManager.GetFont(9F, FontStyle.Bold);
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
        }

        private async void ViewChipsPackingForm_Shown(object sender, EventArgs e)
        {
            try
            {
                var machineTask = _masterService.getMachineList("ChipsLot");
                var lotTask = _productionService.getAllLotList();
                //var prefixTask = getPrefixList();
                var packsizeTask = _masterService.getPackSizeList();
                var copsitemTask = _masterService.getItemList(itemCopsCategoryId);
                var boxitemTask = _masterService.getItemList(itemBoxCategoryId);
                var deptTask = _masterService.getDepartmentList();
                var ownerTask = _masterService.getOwnerList();

                // 2. Wait for all to complete
                await Task.WhenAll(machineTask, lotTask, packsizeTask, copsitemTask, boxitemTask, deptTask, ownerTask);

                // 3. Get the results
                var machineList = machineTask.Result;
                var lotList = lotTask.Result;
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
                lotList.Insert(0, new LotsResponse { LotId = 0, LotNoFrmt = "Select MergeNo" });
                MergeNoList.DataSource = lotList;
                MergeNoList.DisplayMember = "LotNoFrmt";
                MergeNoList.ValueMember = "LotId";
                MergeNoList.SelectedIndex = 0;
                MergeNoList.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                MergeNoList.AutoCompleteSource = AutoCompleteSource.ListItems;
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

                RefreshLastBoxDetails();

                //if (Convert.ToInt64(_productionId) > 0)
                //{
                //    await LoadProductionDetailsAsync(Convert.ToInt64(_productionId));
                //}
                isFormReady = true;
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
                //PrefixList.SelectedValue = 316;  //19       //added hardcoded for now
                MergeNoList.SelectedValue = productionResponse.LotId;
                dateTimePicker1.Text = productionResponse.ProductionDate.ToString();
                dateTimePicker1.Value = productionResponse.ProductionDate;
                QualityList.SelectedValue = productionResponse.QualityId;
                WindingTypeList.SelectedValue = productionResponse.WindingTypeId;
                PackSizeList.SelectedValue = productionResponse.PackSizeId;
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
                palletwtno.Text = productionResponse.EmptyBoxPalletWt.ToString();
                grosswtno.Text = productionResponse.GrossWt.ToString();
                tarewt.Text = productionResponse.TareWt.ToString();
                netwt.Text = productionResponse.NetWt.ToString();
            }
        }

        private async void LineNoList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isFormReady) return; // skip during load

            //if (LineNoList.DroppedDown) return;

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
                            DeptList.SelectedIndex = 1;
                            DeptList.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                            DeptList.AutoCompleteSource = AutoCompleteSource.ListItems;
                        }
                        var getLots = await Task.Run(() => _productionService.getLotList(selectedMachineId));
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

            if (MergeNoList.SelectedIndex <= 0)
            {
                itemname.Text = "";
                shadename.Text = "";
                shadecd.Text = "";
                deniervalue.Text = "";
                salelotvalue.Text = "";
                lotResponse = new LotsResponse();
                lotsDetailsList = new List<LotsDetailsResponse>();
                getLotRelatedDetails();
                rowMaterial.Columns.Clear();
                totalProdQty = 0;
                selectedSOId = 0;
                totalSOQty = 0;
                balanceQty = 0;
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

                        lotResponse = await Task.Run(() => _productionService.getLotById(selectedLotId));
                        itemname.Text = lotResponse.ItemName;
                        shadename.Text = lotResponse.ShadeName;
                        shadecd.Text = lotResponse.ShadeCode;
                        deniervalue.Text = lotResponse.Denier.ToString();
                        salelotvalue.Text = (!string.IsNullOrEmpty(lotResponse.SaleLot)) ? lotResponse.SaleLot.ToString() : null;
                        productionRequest.SaleLot = (!string.IsNullOrEmpty(lotResponse.SaleLot)) ? lotResponse.SaleLot : null;
                        productionRequest.MachineId = lotResponse.MachineId;
                        productionRequest.ItemId = lotResponse.ItemId;
                        productionRequest.ShadeId = lotResponse.ShadeId;
                        LineNoList.SelectedValue = lotResponse.MachineId;

                        if (lotResponse.ItemId > 0)
                        {
                            var itemResponse = await Task.Run(() => _masterService.getItemById(lotResponse.ItemId));

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
                        }

                        var getWindingType = new List<WindingTypeResponse>();
                        getWindingType = await Task.Run(() => _productionService.getWinderTypeList(selectedLotId));
                        getWindingType.Insert(0, new WindingTypeResponse { WindingTypeId = 0, WindingTypeName = "Select Winding Type" });
                        if (getWindingType.Count <= 1)
                        {
                            getWindingType = await Task.Run(() => _masterService.getWindingTypeList());
                            getWindingType.Insert(0, new WindingTypeResponse { WindingTypeId = 0, WindingTypeName = "Select Winding Type" });

                        }
                        WindingTypeList.DataSource = getWindingType;
                        WindingTypeList.DisplayMember = "WindingTypeName";
                        WindingTypeList.ValueMember = "WindingTypeId";
                        WindingTypeList.SelectedIndex = 0;
                        WindingTypeList.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                        WindingTypeList.AutoCompleteSource = AutoCompleteSource.ListItems;

                        lotsDetailsList = new List<LotsDetailsResponse>();
                        productionRequest.ProductionDate = dateTimePicker1.Value;
                        lotsDetailsList = await Task.Run(() => _productionService.getLotsDetailsByLotsIdAndProductionDate(selectedLotId, productionRequest.ProductionDate));
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
                        var packsize = await Task.Run(() => _masterService.getPackSizeById(selectedPacksizeId));
                        frdenier.Text = packsize.FromDenier.ToString();
                        updenier.Text = packsize.UpToDenier.ToString();
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

                    if (_productionId > 0 && productionResponse != null)
                    {
                        WindingTypeList.SelectedValue = productionResponse.WindingTypeId;
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
                //    MessageBox.Show("Quantity not remaining for " + selectedSONumber, "Warning", MessageBoxButtons.OK);
                //}
                //else
                //{
                //}
            }
        }

        private async void RefreshLastBoxDetails()
        {
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
                        var itemResponse = await Task.Run(() => _masterService.getItemById(selectedBoxItemId));
                        if (itemResponse != null)
                        {
                            boxpalletitemwt.Text = itemResponse.Weight.ToString();
                            palletwtno.Text = itemResponse.Weight.ToString();
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

        //private void PrefixList_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (!isFormReady) return;

        //    //if (PrefixList.DroppedDown) return;

        //    if (PrefixList.SelectedIndex <= 0)
        //    {
        //        prodtype.Text = "";
        //        return;
        //    }

        //    if (PrefixList.SelectedIndex > 0)
        //    {
        //        //boxnoerror.Text = "";
        //        //boxnoerror.Visible = false;
        //    }

        //    if (PrefixList.SelectedValue != null)
        //    {
        //        //boxnoerror.Visible = false;

        //        PrefixResponse selectedPrefix = (PrefixResponse)PrefixList.SelectedItem;
        //        int selectedPrefixId = selectedPrefix.PrefixCode;

        //        productionRequest.PrefixCode = selectedPrefixId;

        //        if (selectedPrefix.ProductionType.ToString() != null)
        //        {
        //            prodtype.Text = selectedPrefix.ProductionType.ToString();
        //            productionRequest.ProdTypeId = selectedPrefix.ProductionTypeId;
        //        }

        //    }
        //}

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
                        var machineList = await Task.Run(() => _masterService.getMachineByDepartmentId(selectedDepartmentId));

                        //var filteredMachine = machineList.Where(m => m.DepartmentId == selectedDepartment.DepartmentId).ToList();
                        //LineNoList.SelectedValue = selectedDepartment;
                        machineList.Insert(0, new MachineResponse { MachineId = 0, MachineName = "Select Line No." });
                        LineNoList.DataSource = machineList;
                    }

                    productionRequest.DepartmentId = selectedDepartmentId;
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

        //private Task<List<MachineResponse>> getMachineList()
        //{
        //    return Task.Run(() => _masterService.getMachineList("ChipsLot"));
        //}

        //private Task<List<LotsResponse>> getAllLotList()
        //{
        //    return Task.Run(() => _productionService.getAllLotList());
        //}

        //private Task<List<QualityResponse>> getQualityListByItemTypeId(int itemTypeId)
        //{
        //    return Task.Run(() => _masterService.getQualityListByItemTypeId(itemTypeId));
        //}

        //private Task<List<PackSizeResponse>> getPackSizeList()
        //{
        //    return Task.Run(() => _masterService.getPackSizeList());
        //}

        private List<string> getComPortList()
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

        private List<WeighingItem> getWeighingList()
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

        //private Task<List<ItemResponse>> getCopeItemList(int categoryId)
        //{
        //    return Task.Run(() => _masterService.getItemList(categoryId));
        //}

        //private Task<List<ItemResponse>> getBoxItemList(int categoryId)
        //{
        //    return Task.Run(() => _masterService.getItemList(categoryId));
        //}

        //private Task<List<ItemResponse>> getPalletItemList(int categoryId)
        //{
        //    return Task.Run(() => _masterService.getItemList(categoryId));
        //}

        //private Task<List<PrefixResponse>> getPrefixList(TransactionTypePrefixRequest prefix)
        //{
        //    return Task.Run(() => _masterService.getPrefixList(prefixRequest));
        //}

        //private Task<ProductionResponse> getProductionById(long productionId)
        //{
        //    return Task.Run(() => _packingService.getProductionById(productionId));
        //}

        //private Task<List<BusinessPartnerResponse>> getOwnerList()
        //{
        //    return Task.Run(() => _masterService.getOwnerList());
        //}

        //private Task<List<ProductionResponse>> getProductionLotIdandSaleOrderItemIdandPackingType(int lotId, int saleOrderItemId)
        //{
        //    return Task.Run(() => _packingService.getAllByLotIdandSaleOrderItemIdandPackingType(lotId, saleOrderItemId));
        //}

        //private Task<ProductionResponse> getLastBoxDetails()
        //{
        //    return Task.Run(() => _packingService.getLastBoxDetails("chipspacking"));
        //}

        //private Task<List<DepartmentResponse>> getDepartmentList()
        //{
        //    return Task.Run(() => _masterService.getDepartmentList());
        //}

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
                        //return;
                    }
                }
            }

        }

        private void GrossWeight_TextChanged(object sender, EventArgs e)
        {
            if (!isFormReady) return;

            if (string.IsNullOrWhiteSpace(grosswtno.Text))
            {
                //grosswterror.Visible = true;
                //grosswterror.Text = "Please enter gross weight";
                MessageBox.Show("Please enter gross weight", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                //soerror.Visible = false;
                //soerror.Text = "";
                if (!string.IsNullOrWhiteSpace(tarewt.Text))
                {
                    decimal gross, tare;
                    if (decimal.TryParse(grosswtno.Text, out gross) && decimal.TryParse(tarewt.Text, out tare))
                    {
                        if (gross >= tare)
                        {
                            CalculateNetWeight();
                            //grosswterror.Text = "";
                            //grosswterror.Visible = false;
                        }
                        else
                        {
                            //grosswterror.Text = "Gross Wt > Tare Wt";
                            //if(grosswterror.Visible)
                            //{
                            //MessageBox.Show("Gross Wt > Tare Wt", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            //netwt.Text = "0";
                            //wtpercop.Text = "0";
                            //}

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

        private void CalculateWeightPerCop()
        {
            decimal num1 = 0;

            decimal.TryParse(netwt.Text, out num1);
            if (num1 > 0)
            {
                //if (num1 >= num2)
                //{
                wtpercop.Text = (num1).ToString("F3");
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
    }
}
