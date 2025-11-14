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
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PackingApplication
{
    public partial class ViewBCFPackingForm : Form
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
        public ViewBCFPackingForm()
        {
            InitializeComponent();
            ApplyFonts();
            this.Shown += ViewBCFPackingForm_Shown;
            this.AutoScroll = true;
            lblLoading = CommonMethod.InitializeLoadingLabel(this);

            width = flowLayoutPanel1.ClientSize.Width;
            rowMaterial.AutoGenerateColumns = false;
            windinggrid.AutoGenerateColumns = false;
            qualityqty.AutoGenerateColumns = false;
        }

        private void ViewBCFPackingForm_Load(object sender, EventArgs e)
        {
            AddHeader();

            getLotRelatedDetails();

            copyno.Text = "2";
            spoolno.Text = "0";
            spoolwt.Text = "0";
            palletwtno.Text = "0";
            grosswtno.Text = "0";
            tarewt.Text = "0";
            netwt.Text = "0";
            wtpercop.Text = "0";
            copsstock.Text = "0";
            boxpalletstock.Text = "0";
            copsitemwt.Text = "0";
            boxpalletitemwt.Text = "0";
            frdenier.Text = "0";
            updenier.Text = "0";
            deniervalue.Text = "0";
            partyn.Text = "";
            partyshade.Text = "";
            isFormReady = true;
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
            this.palletdetails.Font = FontManager.GetFont(9F, FontStyle.Bold);
            this.label6.Font = FontManager.GetFont(8F, FontStyle.Bold);
            this.PalletTypeList.Font = FontManager.GetFont(8F, FontStyle.Regular);
            this.pquantity.Font = FontManager.GetFont(8F, FontStyle.Bold);
            this.qnty.Font = FontManager.GetFont(8F, FontStyle.Regular);
            this.flowLayoutPanel1.Font = FontManager.GetFont(8F, FontStyle.Regular);
            this.Printinglbl.Font = FontManager.GetFont(9F, FontStyle.Bold);
            this.wgroupbox.Font = FontManager.GetFont(8F, FontStyle.Bold);
            this.netwttxtbox.Font = FontManager.GetFont(8F, FontStyle.Bold);
            this.netweight.Font = FontManager.GetFont(8F, FontStyle.Bold);
            this.grosswttxtbox.Font = FontManager.GetFont(8F, FontStyle.Bold);
            this.grossweight.Font = FontManager.GetFont(8F, FontStyle.Bold);
            this.copstxtbox.Font = FontManager.GetFont(8F, FontStyle.Bold);
            this.tarewghttxtbox.Font = FontManager.GetFont(8F, FontStyle.Bold);
            this.tareweight.Font = FontManager.GetFont(8F, FontStyle.Bold);
            this.cops.Font = FontManager.GetFont(8F, FontStyle.Bold);
            this.gradewiseprodn.Font = FontManager.GetFont(8F, FontStyle.Bold);
            this.totalprodbalqty.Font = FontManager.GetFont(8F, FontStyle.Regular);
            this.saleordrqty.Font = FontManager.GetFont(8F, FontStyle.Regular);
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
            this.windingerror.Font = FontManager.GetFont(7F, FontStyle.Regular);
            this.packsizeerror.Font = FontManager.GetFont(7F, FontStyle.Regular);
            this.soerror.Font = FontManager.GetFont(7F, FontStyle.Regular);
            this.qualityerror.Font = FontManager.GetFont(7F, FontStyle.Regular);
            this.mergenoerror.Font = FontManager.GetFont(7F, FontStyle.Regular);
            this.copynoerror.Font = FontManager.GetFont(7F, FontStyle.Regular);
            this.linenoerror.Font = FontManager.GetFont(7F, FontStyle.Regular);
            this.grdsoqty.Font = FontManager.GetFont(8F, FontStyle.Regular);
            this.prodnbalqty.Font = FontManager.GetFont(8F, FontStyle.Regular);
            this.rowMaterialBox.Font = FontManager.GetFont(8F, FontStyle.Bold);
            this.fromdenier.Font = FontManager.GetFont(8F, FontStyle.Bold);
            this.uptodenier.Font = FontManager.GetFont(8F, FontStyle.Bold);
            this.Font = FontManager.GetFont(9F, FontStyle.Bold);
            this.bppartyname.Font = FontManager.GetFont(8F, FontStyle.Bold);
            this.partyshade.Font = FontManager.GetFont(8F, FontStyle.Regular);
            this.partyshd.Font = FontManager.GetFont(8F, FontStyle.Bold);
            this.partyn.Font = FontManager.GetFont(8F, FontStyle.Regular);
            this.salelotvalue.Font = FontManager.GetFont(8F, FontStyle.Regular);
            this.salelot.Font = FontManager.GetFont(8F, FontStyle.Bold);
            this.owner.Font = FontManager.GetFont(8F, FontStyle.Bold);
            this.OwnerList.Font = FontManager.GetFont(8F, FontStyle.Regular);
        }

        private async void ViewBCFPackingForm_Shown(object sender, EventArgs e)
        {
            try
            {

                var machineTask = _masterService.getMachineList("BCFLot");
                //var lotTask = _productionService.getAllLotList();
                //var prefixTask = getPrefixList();
                var packsizeTask = _masterService.getPackSizeList();
                var copsitemTask = _masterService.getItemList(itemCopsCategoryId);
                var boxitemTask = _masterService.getItemList(itemBoxCategoryId);
                var palletitemTask = _masterService.getItemList(itemPalletCategoryId);
                var deptTask = _masterService.getDepartmentList();
                var ownerTask = _masterService.getOwnerList();

                // 2. Wait for all to complete
                await Task.WhenAll(machineTask, packsizeTask, copsitemTask, boxitemTask, palletitemTask, deptTask, ownerTask);

                // 3. Get the results
                var machineList = machineTask.Result;
                //var lotList = lotTask.Result;
                //var prefixList = prefixTask.Result;
                var packsizeList = packsizeTask.Result;
                var copsitemList = copsitemTask.Result;
                var boxitemList = boxitemTask.Result;
                var palletitemList = palletitemTask.Result;
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


                //palletitem
                palletitemList.Insert(0, new ItemResponse { ItemId = 0, Name = "Select Box/Pallet" });
                PalletTypeList.DataSource = palletitemList;
                PalletTypeList.DisplayMember = "Name";
                PalletTypeList.ValueMember = "ItemId";
                PalletTypeList.SelectedIndex = 0;
                PalletTypeList.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                PalletTypeList.AutoCompleteSource = AutoCompleteSource.ListItems;
                //PalletTypeList.DropDownStyle = ComboBoxStyle.DropDown;

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
            //productionResponse = Task.Run(() => getProductionById(Convert.ToInt64(productionId))).Result;

            if (prodResponse != null)
            {
                productionResponse = prodResponse;

                LineNoList.SelectedValue = productionResponse.MachineId;
                DeptList.SelectedValue = productionResponse.DepartmentId;
                MergeNoList.SelectedValue = productionResponse.LotId;
                dateTimePicker1.Text = productionResponse.ProductionDate.ToString();
                dateTimePicker1.Value = productionResponse.ProductionDate;
                SaleOrderList.SelectedValue = productionResponse.SaleOrderItemsId;
                QualityList.SelectedValue = productionResponse.QualityId;
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
                spoolno.Text = productionResponse.Spools.ToString();
                spoolwt.Text = productionResponse.SpoolsWt.ToString();
                palletwtno.Text = productionResponse.EmptyBoxPalletWt.ToString();
                grosswtno.Text = productionResponse.GrossWt.ToString();
                tarewt.Text = productionResponse.TareWt.ToString();
                netwt.Text = productionResponse.NetWt.ToString();
                OwnerList.SelectedValue = productionResponse.OwnerId;
                LineNoList_SelectedIndexChanged(LineNoList, EventArgs.Empty);
                //MergeNoList_SelectedIndexChanged(MergeNoList, EventArgs.Empty);
                //PackSizeList_SelectedIndexChanged(PackSizeList, EventArgs.Empty);
                //CopsItemList_SelectedIndexChanged(CopsItemList, EventArgs.Empty);
                //BoxItemList_SelectedIndexChanged(BoxItemList, EventArgs.Empty);
                if (productionResponse.PalletDetailsResponse.Count > 0)
                {
                    if (productionResponse?.PalletDetailsResponse != null && productionResponse.PalletDetailsResponse.Any())
                    {
                        this.BeginInvoke((Action)(() =>
                            BindPalletDetails(productionResponse.PalletDetailsResponse)
                        ));
                    }
                }
            }
        }

        private void BindPalletDetails(List<ProductionPalletDetailsResponse> palletDetailsResponse)
        {
            flowLayoutPanel1.Controls.Clear();
            rowCount = 0;
            headerAdded = false;

            // add header first
            AddHeader();

            foreach (var palletDetail in palletDetailsResponse)
            {
                var palletItemList = _masterService.getItemList(itemPalletCategoryId).Result;
                var selectedItem = palletItemList.FirstOrDefault(x => x.ItemId == palletDetail.PalletId);

                if (selectedItem == null)
                {
                    selectedItem = new ItemResponse { ItemId = palletDetail.PalletId, Name = $"Pallet {palletDetail.PalletId}" };
                }

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
                System.Windows.Forms.Label lblItem = new System.Windows.Forms.Label() { Text = selectedItem.Name, Width = 140, Location = new System.Drawing.Point(50, 10), Font = FontManager.GetFont(8F, FontStyle.Regular), Tag = selectedItem.ItemId };

                // Qty
                System.Windows.Forms.Label lblQty = new System.Windows.Forms.Label() { Text = palletDetail.Quantity.ToString(), Width = 50, Location = new System.Drawing.Point(200, 10), Font = FontManager.GetFont(8F, FontStyle.Regular) };

                rowPanel.Controls.Add(lblSrNo);
                rowPanel.Controls.Add(lblItem);
                rowPanel.Controls.Add(lblQty);
                rowPanel.Tag = new Tuple<ItemResponse, System.Windows.Forms.Label>(selectedItem, lblQty);

                flowLayoutPanel1.Controls.Add(rowPanel);
            }

            flowLayoutPanel1.AutoScroll = true;
            flowLayoutPanel1.WrapContents = false;
            flowLayoutPanel1.FlowDirection = FlowDirection.TopDown;
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
                            MergeNoList_SelectedIndexChanged(MergeNoList, EventArgs.Empty);
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
                partyn.Text = "";
                partyshade.Text = "";
                salelotvalue.Text = "";
                lotResponse = new LotsResponse();
                lotsDetailsList = new List<LotsDetailsResponse>();
                getLotRelatedDetails();
                rowMaterial.Columns.Clear();
                windinggrid.Columns.Clear();
                qualityqty.Columns.Clear();
                totalProdQty = 0;
                prodnbalqty.Text = "";
                selectedSOId = 0;
                totalSOQty = 0;
                grdsoqty.Text = "";
                balanceQty = 0;
                flowLayoutPanel1.Controls.Clear();
                rowCount = 0;
                AddHeader();
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

                        var getSaleOrder = await Task.Run(() => _productionService.getSaleOrderList(selectedLotId));
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
                        RefreshWindingGrid();
                    }

                    if (_productionId > 0 && productionResponse != null)
                    {
                        WindingTypeList.SelectedValue = productionResponse.WindingTypeId;
                        WindingTypeList_SelectedIndexChanged(WindingTypeList, EventArgs.Empty);
                    }
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
                    totalSOQty = 0;
                    grdsoqty.Text = "";
                    LotSaleOrderDetailsResponse selectedSaleOrder = (LotSaleOrderDetailsResponse)SaleOrderList.SelectedItem;
                    int selectedSaleOrderId = selectedSaleOrder.SaleOrderItemsId;
                    string soNumber = selectedSaleOrder.SaleOrderNumber;
                    productionRequest.SaleOrderItemsId = selectedSaleOrderId;
                    if (selectedSaleOrderId > 0)
                    {
                        selectedSOId = selectedSaleOrderId;
                        selectedSONumber = selectedSaleOrder.SaleOrderNumber;
                        totalSOQty = selectedSaleOrder.Quantity;
                        grdsoqty.Text = totalSOQty.ToString("F2");
                        var saleOrderItemResponse = await Task.Run(() => _saleService.getSaleOrderItemById(selectedSaleOrderId));
                        if (saleOrderItemResponse != null)
                        {
                            productionRequest.ContainerTypeId = saleOrderItemResponse.ContainerTypeId;
                            partyn.Text = saleOrderItemResponse.ItemDescription;
                            partyshade.Text = saleOrderItemResponse.ShadeNameDescription + "-" + saleOrderItemResponse.ShadeCodeDescription;
                        }

                        //var saleItemResponse = await getSaleOrderItemById(selectedSaleOrderId);

                        //foreach (var soitem in saleResponse.saleOrderItemsResponses)
                        //{
                        //}

                        RefreshGradewiseGrid();
                        if (_productionId > 0 && productionResponse != null)
                        {
                            WindingTypeList.SelectedValue = productionResponse.WindingTypeId;
                            WindingTypeList_SelectedIndexChanged(WindingTypeList, EventArgs.Empty);
                        }
                        //RefreshLastBoxDetails();
                    }

                }
            }
            finally
            {
                lblLoading.Visible = false;
            }
        }
        private async void RefreshWindingGrid()
        {
            if (WindingTypeList.SelectedValue != null)
            {
                int selectedWindingTypeId = Convert.ToInt32(WindingTypeList.SelectedValue.ToString());
                if (selectedWindingTypeId > 0)
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

        }

        private async void RefreshGradewiseGrid()
        {
            if (QualityList.SelectedValue != null)
            {
                prodnbalqty.Text = "";
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
                    //MessageBox.Show("Quantity not remaining for " + selectedSONumber, "Warning", MessageBoxButtons.OK);
                    prodnbalqty.Text = "0";
                }
                else
                {
                    prodnbalqty.Text = balanceQty.ToString("F2");
                }
                
            }
        }

        private async void RefreshLastBoxDetails()
        {
            var getLastBox = _packingService.getLastBoxDetails("bcfpacking").Result;

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

                        var itemResponse = await Task.Run(() => _masterService.getItemById(selectedItemId));
                        if (itemResponse != null)
                        {
                            copsitemwt.Text = itemResponse.Weight.ToString();
                            //spoolwt.Text = itemResponse.Weight.ToString();
                            SpoolNo_TextChanged(sender, e);
                            GrossWeight_TextChanged(sender, e);
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

                    //prefixRequest.DepartmentId = selectedDepartmentId;
                    //prefixRequest.TxnFlag = "POY";
                    //prefixRequest.TransactionTypeId = 5;
                    //prefixRequest.ProductionTypeId = 1;
                    //prefixRequest.Prefix = "";
                    //prefixRequest.FinYearId = SessionManager.FinYearId;

                    //List<PrefixResponse> prefixList = await Task.Run(() => _masterService.getPrefixList(prefixRequest));
                    //prefixList.Insert(0, new PrefixResponse { PrefixCode = 0, Prefix = "Select Prefix" });
                    //PrefixList.DataSource = prefixList;
                    //PrefixList.DisplayMember = "Prefix";
                    //PrefixList.ValueMember = "PrefixCode";
                    //PrefixList.SelectedIndex = 0;
                    //PrefixList.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    //PrefixList.AutoCompleteSource = AutoCompleteSource.ListItems;

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
        //    return Task.Run(() => _masterService.getMachineList("BCFLot"));
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
        //    return Task.Run(() => _packingService.getLastBoxDetails("bcfpacking"));
        //}

        //private Task<List<DepartmentResponse>> getDepartmentList()
        //{
        //    return Task.Run(() => _masterService.getDepartmentList());
        //}

        private int rowCount = 0; // Keeps track of SrNo
        private bool headerAdded = false; // To ensure header is added only once
        private int currentY = 35; // Start below header height
        private void addqty_Click(object sender, EventArgs e)
        {
            var selectedItem = (ItemResponse)PalletTypeList.SelectedItem;
            if (selectedItem != null)
            {
                if (selectedItem.ItemId == 0)
                {
                    MessageBox.Show("Please select an item.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                    return;
                }
            }
            if (string.IsNullOrEmpty(qnty.Text))
            {
                MessageBox.Show("Please enter quantity.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error); return;
            }
            int qty = Convert.ToInt32(qnty.Text);

            if (selectedItem.ItemId > 0)
            {
                //if (!headerAdded)
                //{
                //    AddHeader();
                //}
                // Check duplicate value
                bool alreadyExists = flowLayoutPanel1.Controls
                    .OfType<Panel>().Skip(1) // Skip header
                    .Any(ctrl => {
                        if (ctrl.Tag is Tuple<ItemResponse, int> tagData)
                        {
                            return tagData.Item1.ItemId == selectedItem.ItemId && tagData.Item2 == qty;
                        }
                        return false;
                    });

                var existingPanel = flowLayoutPanel1.Controls
                    .OfType<Panel>()
                    .Skip(1) // Skip header
                    .FirstOrDefault(ctrl =>
                        ctrl.Tag is Tuple<ItemResponse, System.Windows.Forms.Label> tag &&
                        tag.Item1.ItemId == selectedItem.ItemId);

                if (existingPanel != null)
                {
                    var tag = (Tuple<ItemResponse, System.Windows.Forms.Label>)existingPanel.Tag;
                    tag.Item2.Text = qty.ToString();
                    //MessageBox.Show("Item quantity updated.");
                    foreach (var control in existingPanel.Controls.OfType<System.Windows.Forms.Button>())
                    {
                        if (control.Text == "Remove")
                        {
                            control.Enabled = true;
                        }
                    }

                    qnty.Text = "";
                    PalletTypeList.SelectedIndex = 0;
                    return;
                }

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
                    System.Windows.Forms.Label lblItem = new System.Windows.Forms.Label() { Text = selectedItem.Name, Width = 140, Location = new System.Drawing.Point(50, 10), Font = FontManager.GetFont(8F, FontStyle.Regular), Tag = selectedItem.ItemId };

                    // Qty
                    System.Windows.Forms.Label lblQty = new System.Windows.Forms.Label() { Text = qty.ToString(), Width = 50, Location = new System.Drawing.Point(200, 10), Font = FontManager.GetFont(8F, FontStyle.Regular) };

                    // Edit Button
                    System.Windows.Forms.Button btnEdit = new System.Windows.Forms.Button() { Text = "Edit", Size = new Size(35, 23), Location = new System.Drawing.Point(250, 5), Font = FontManager.GetFont(7F, FontStyle.Regular), BackColor = Color.FromArgb(230, 240, 255), ForeColor = Color.FromArgb(51, 133, 255), Tag = new Tuple<ItemResponse, System.Windows.Forms.Label>(selectedItem, lblQty), FlatStyle = FlatStyle.Flat };
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
                    System.Windows.Forms.Button btnDelete = new System.Windows.Forms.Button() { Text = "Remove", Size = new Size(50, 23), Location = new System.Drawing.Point(300, 5), Font = FontManager.GetFont(7F, FontStyle.Regular), BackColor = Color.FromArgb(255, 230, 230), ForeColor = Color.FromArgb(255, 51, 51), Tag = rowPanel, FlatStyle = FlatStyle.Flat };
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

                    flowLayoutPanel1.Controls.Add(rowPanel);
                    flowLayoutPanel1.AutoScroll = true;
                    flowLayoutPanel1.WrapContents = false;
                    flowLayoutPanel1.FlowDirection = FlowDirection.TopDown;

                    qnty.Text = "";
                    PalletTypeList.SelectedIndex = 0;
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
        }

        private void ReorderSrNo()
        {
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
        }

        private void AddHeader()
        {
            Panel headerPanel = new Panel();
            headerPanel.Size = new Size(flowLayoutPanel1.ClientSize.Width - 20, 35);
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
            headerPanel.Controls.Add(new System.Windows.Forms.Label() { Text = "Item Name", Width = 140, Location = new System.Drawing.Point(50, 10), Font = FontManager.GetFont(7F, FontStyle.Bold) });
            headerPanel.Controls.Add(new System.Windows.Forms.Label() { Text = "Qty", Width = 50, Location = new System.Drawing.Point(200, 10), Font = FontManager.GetFont(7F, FontStyle.Bold) });
            //headerPanel.Controls.Add(new System.Windows.Forms.Label() { Text = "Action", Width = 120, Location = new System.Drawing.Point(250, 10), Font = FontManager.GetFont(7F, FontStyle.Bold) });

            flowLayoutPanel1.Controls.Add(headerPanel);
            headerAdded = true;
        }

        private void editPallet_Click(object sender, EventArgs e)
        {
            var btn = sender as System.Windows.Forms.Button;
            var data = btn.Tag as Tuple<ItemResponse, System.Windows.Forms.Label>;

            if (data != null)
            {
                ItemResponse item = data.Item1;
                int quantity = Convert.ToInt32(data.Item2.Text);

                foreach (ItemResponse entry in PalletTypeList.Items)
                {
                    if (entry.ItemId == item.ItemId)
                    {
                        PalletTypeList.SelectedItem = entry;
                        break;
                    }
                }

                qnty.Text = quantity.ToString();

                //disable remove button when edit row
                var rowPanel = btn.Parent as Panel;
                if (rowPanel != null)
                {
                    foreach (var control in rowPanel.Controls.OfType<System.Windows.Forms.Button>())
                    {
                        if (control.Text == "Remove")
                        {
                            control.Enabled = false;
                        }
                    }
                }
                PalletTypeList.Focus();
            }
        }

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

        private void GrossWeight_TextChanged(object sender, EventArgs e)
        {
            if (!isFormReady) return;

            if (string.IsNullOrWhiteSpace(grosswtno.Text))
            {
                //if(grosswterror.Visible)
                //{
                //grosswterror.Text = "Please enter gross weight";
                MessageBox.Show("Please enter gross weight", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //}
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

        private void SpoolNo_TextChanged(object sender, EventArgs e)
        {
            if (!isFormReady) return;
            if (string.IsNullOrWhiteSpace(spoolno.Text))
            {
                //spoolnoerror.Text = "Please enter spool no";
                //if(spoolnoerror.Visible)
                //{
                MessageBox.Show("Please enter spool no", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tarewt.Text = "0";
                spoolwt.Text = "0";
                return;
                //}

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
                //else if (spoolnum == 0)
                //{
                //    //spoolnoerror.Text = "Spool no > 0";
                //    //if(spoolnoerror.Visible)
                //    //{
                //    MessageBox.Show("Spool no > 0", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //    tarewt.Text = "0";
                //    spoolwt.Text = "0";
                //    return;
                //    //}

                //}
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

        private void palletdetailsheader_Resize(object sender, EventArgs e)
        {
            _cmethod.SetTopRoundedRegion(palletdetailsheader, 8);
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
