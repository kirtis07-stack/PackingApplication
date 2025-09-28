using PackingApplication.Helper;
using PackingApplication.Models.RequestEntities;
using PackingApplication.Models.ResponseEntities;
using PackingApplication.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PackingApplication
{
    public partial class ChipsPackingForm : Form
    {
        private static Logger Log = Logger.GetLogger();

        MasterService _masterService = new MasterService();
        ProductionService _productionService = new ProductionService();
        PackingService _packingService = new PackingService();
        SaleService _saleService = new SaleService();
        private long _productionId;
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
        public ChipsPackingForm(long productionId)
        {
            InitializeComponent();
            ApplyFonts();
            this.Shown += ChipsPackingForm_Shown;
            this.AutoScroll = true;
            _productionId = productionId;

            _cmethod.SetButtonBorderRadius(this.submit, 8);
            _cmethod.SetButtonBorderRadius(this.cancelbtn, 8);

            LineNoList.SelectedIndexChanged += LineNoList_SelectedIndexChanged;
            MergeNoList.SelectedIndexChanged += MergeNoList_SelectedIndexChanged;
            PackSizeList.SelectedIndexChanged += PackSizeList_SelectedIndexChanged;
            QualityList.SelectedIndexChanged += QualityList_SelectedIndexChanged;
            WindingTypeList.SelectedIndexChanged += WindingTypeList_SelectedIndexChanged;
            SaleOrderList.SelectedIndexChanged += SaleOrderList_SelectedIndexChanged;
            PrefixList.SelectedIndexChanged += PrefixList_SelectedIndexChanged;
            copyno.TextChanged += CopyNos_TextChanged;
            palletwtno.TextChanged += PalletWeight_TextChanged;
            grosswtno.TextChanged += GrossWeight_TextChanged;
            rowMaterial.AutoGenerateColumns = false;
        }

        private void ChipsPackingForm_Load(object sender, EventArgs e)
        {
            //var getItem = new List<LotsResponse>();
            //getItem.Insert(0, new LotsResponse { LotId = 0, LotNo = "Select MergeNo" });
            //MergeNoList.DataSource = getItem;
            //MergeNoList.DisplayMember = "LotNo";
            //MergeNoList.ValueMember = "LotId";
            //MergeNoList.SelectedIndex = 0;

            var getSaleOrder = new List<LotSaleOrderDetailsResponse>();
            getSaleOrder.Insert(0, new LotSaleOrderDetailsResponse { SaleOrderDetailsId = 0, SaleOrderNumber = "Select Sale Order" });
            SaleOrderList.DataSource = getSaleOrder;
            SaleOrderList.DisplayMember = "SaleOrderNumber";
            SaleOrderList.ValueMember = "SaleOrderDetailsId";
            SaleOrderList.SelectedIndex = 0;

            var qualityList = new List<QualityResponse>();
            qualityList.Insert(0, new QualityResponse { QualityId = 0, Name = "Select Quality" });
            QualityList.DataSource = qualityList;
            QualityList.DisplayMember = "Name";
            QualityList.ValueMember = "QualityId";
            QualityList.SelectedIndex = 0;

            copyno.Text = "1";
            //Username.Text = SessionManager.UserName;
            //role.Text = SessionManager.Role;

            isFormReady = true;
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
            this.textBox1.Font = FontManager.GetFont(8F, FontStyle.Regular);
            this.productiontype.Font = FontManager.GetFont(8F, FontStyle.Bold);
            this.remark.Font = FontManager.GetFont(8F, FontStyle.Bold);
            this.remarks.Font = FontManager.GetFont(8F, FontStyle.Regular);
            this.scalemodel.Font = FontManager.GetFont(8F, FontStyle.Bold);
            this.LineNoList.Font = FontManager.GetFont(8F, FontStyle.Regular);
            this.departmentname.Font = FontManager.GetFont(8F, FontStyle.Regular);
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
            this.prodtype.Font = FontManager.GetFont(8F, FontStyle.Regular);
            this.submit.Font = FontManager.GetFont(8F, FontStyle.Bold);
            this.Printinglbl.Font = FontManager.GetFont(9F, FontStyle.Bold);
            this.netwttxtbox.Font = FontManager.GetFont(8F, FontStyle.Bold);
            this.netweight.Font = FontManager.GetFont(8F, FontStyle.Bold);
            this.grosswttxtbox.Font = FontManager.GetFont(8F, FontStyle.Bold);
            this.grossweight.Font = FontManager.GetFont(8F, FontStyle.Bold);
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
            this.reviewlbl.Font = FontManager.GetFont(9F, FontStyle.Bold);
            this.reviewsubtitle.Font = FontManager.GetFont(8F, FontStyle.Regular);
            this.weighlbl.Font = FontManager.GetFont(9F, FontStyle.Bold);
            this.weighsubtitle.Font = FontManager.GetFont(8F, FontStyle.Regular);
            this.packaginglbl.Font = FontManager.GetFont(9F, FontStyle.Bold);
            this.packagingsubtitle.Font = FontManager.GetFont(8F, FontStyle.Regular);
            this.orderlbl.Font = FontManager.GetFont(9F, FontStyle.Bold);
            this.orderdetailssubtitle.Font = FontManager.GetFont(8F, FontStyle.Regular);
            this.rowMaterial.Font = FontManager.GetFont(8F, FontStyle.Regular);
            this.rowMaterialBox.Font = FontManager.GetFont(8F, FontStyle.Bold);
            this.bagstockvalue.Font = FontManager.GetFont(8F, FontStyle.Regular);
            this.bagsstock.Font = FontManager.GetFont(8F, FontStyle.Bold);
            this.bagstype.Font = FontManager.GetFont(8F, FontStyle.Bold);
            this.bagsweight.Font = FontManager.GetFont(8F, FontStyle.Bold);
            this.fromdenier.Font = FontManager.GetFont(8F, FontStyle.Bold);
            this.uptodenier.Font = FontManager.GetFont(8F, FontStyle.Bold);
            this.Font = FontManager.GetFont(9F, FontStyle.Bold);
        }

        private async void ChipsPackingForm_Shown(object sender, EventArgs e)
        {
            var machineList = await Task.Run(() => getMachineList());
            //machine
            machineList.Insert(0, new MachineResponse { MachineId = 0, MachineName = "Select Line No." });
            LineNoList.DataSource = machineList;
            LineNoList.DisplayMember = "MachineName";
            LineNoList.ValueMember = "MachineId";
            LineNoList.SelectedIndex = 0;

            var lotList = await Task.Run(() => getAllLotList());
            //lot
            lotList.Insert(0, new LotsResponse { LotId = 0, LotNo = "Select MergeNo" });
            MergeNoList.DataSource = lotList;
            MergeNoList.DisplayMember = "LotNo";
            MergeNoList.ValueMember = "LotId";
            MergeNoList.SelectedIndex = 0;

            var prefixList = await Task.Run(() => getPrefixList());
            //prefix
            prefixList.Insert(0, new PrefixResponse { PrefixCode = 0, Prefix = "Select Prefix" });
            PrefixList.DataSource = prefixList;
            PrefixList.DisplayMember = "Prefix";
            PrefixList.ValueMember = "PrefixCode";
            PrefixList.SelectedIndex = 0;

            var packsizeList = await Task.Run(() => getPackSizeList());
            //packsize
            packsizeList.Insert(0, new PackSizeResponse { PackSizeId = 0, PackSizeName = "Select Pack Size" });
            PackSizeList.DataSource = packsizeList;
            PackSizeList.DisplayMember = "PackSizeName";
            PackSizeList.ValueMember = "PackSizeId";
            PackSizeList.SelectedIndex = 0;

            var windingtypeList = await Task.Run(() => getWindingTypeList());
            //windingtype
            windingtypeList.Insert(0, new WindingTypeResponse { WindingTypeId = 0, WindingTypeName = "Select Winding Type" });
            WindingTypeList.DataSource = windingtypeList;
            WindingTypeList.DisplayMember = "WindingTypeName";
            WindingTypeList.ValueMember = "WindingTypeId";
            WindingTypeList.SelectedIndex = 0;

            var comportList = await Task.Run(() => getComPortList());
            //comport
            ComPortList.DataSource = comportList;
            ComPortList.SelectedIndex = 0;

            var weightingList = await Task.Run(() => getWeighingList());
            //weighting
            WeighingList.DataSource = weightingList;
            WeighingList.DisplayMember = "Name";
            WeighingList.ValueMember = "Id";
            WeighingList.SelectedIndex = 0;

            var copsitemList = await Task.Run(() => getCopeItemList());
            //copsitem
            copsitemList.Insert(0, new ItemResponse { ItemId = 0, Name = "Select Bags Type" });
            CopsItemList.DataSource = copsitemList;
            CopsItemList.DisplayMember = "Name";
            CopsItemList.ValueMember = "ItemId";
            CopsItemList.SelectedIndex = 0;

            var getLastBox = await Task.Run(() => getLastBoxDetails());
            //lastboxdetails
            if (getLastBox.ProductionId > 0)
            {
                this.copstxtbox.Text = getLastBox.Spools.ToString();
                this.tarewghttxtbox.Text = getLastBox.TareWt.ToString();
                this.grosswttxtbox.Text = getLastBox.GrossWt.ToString();
                this.netwttxtbox.Text = getLastBox.NetWt.ToString();
                this.lastbox.Text = getLastBox.BoxNoFmtd.ToString();
            }

            if (Convert.ToInt64(_productionId) > 0)
            {
                var productionResponse = Task.Run(() => getProductionById(Convert.ToInt64(_productionId))).Result;

                if (productionResponse != null)
                {
                    LineNoList.SelectedValue = productionResponse.MachineId;
                    departmentname.Text = productionResponse.DepartmentName;
                    PrefixList.SelectedValue = 316;         //added hardcoded for now
                    MergeNoList.SelectedValue = productionResponse.LotId;
                    dateTimePicker1.Text = productionResponse.ProductionDate.ToShortDateString();
                    QualityList.SelectedValue = productionResponse.QualityId;
                    SaleOrderList.SelectedValue = productionResponse.SaleOrderId;
                    PackSizeList.SelectedValue = productionResponse.PackSizeId;
                    WindingTypeList.SelectedValue = productionResponse.WindingTypeId;
                    CopsItemList.SelectedValue = productionResponse.SpoolItemId;
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

            isFormReady = true;
        }

        private ProductionRequest productionRequest = new ProductionRequest();
        private bool isFormReady = false;
        private void LineNoList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isFormReady) return; // skip during load

            if (LineNoList.SelectedIndex <= 0)
            {
                departmentname.Text = "";
                return;
            }
            if (LineNoList.SelectedIndex > 0)
            {
                linenoerror.Text = "";
                linenoerror.Visible = false;
            }
            if (LineNoList.SelectedValue != null)
            {
                linenoerror.Visible = false;
                MachineResponse selectedMachine = (MachineResponse)LineNoList.SelectedItem;
                int selectedMachineId = selectedMachine.MachineId;

                if (selectedMachineId > 0)
                {
                    productionRequest.MachineId = selectedMachineId;
                    // Call API to get department info by MachineId
                    var department = _masterService.getMachineById(selectedMachineId);
                    departmentname.Text = department.DepartmentName;
                    productionRequest.DepartmentId = department.DepartmentId;

                    getLotList(selectedMachineId);
                }
            }
        }

        private void MergeNoList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isFormReady) return;

            if (MergeNoList.SelectedIndex <= 0)
            {
                itemname.Text = "";
                shadename.Text = "";
                shadecd.Text = "";
                deniervalue.Text = "";
                twistvalue.Text = "";
                return;
            }
            if (MergeNoList.SelectedIndex > 0)
            {
                mergenoerror.Text = "";
                mergenoerror.Visible = false;
            }
            if (MergeNoList.SelectedValue != null)
            {
                mergenoerror.Visible = false;
                LotsResponse selectedLot = (LotsResponse)MergeNoList.SelectedItem;
                int selectedLotId = selectedLot.LotId;
                if (selectedLotId > 0)
                {
                    selectLotId = selectedLotId;
                    productionRequest.LotId = selectedLot.LotId;

                    lotResponse = _productionService.getLotById(selectedLotId);
                    itemname.Text = lotResponse.ItemName;
                    shadename.Text = lotResponse.ShadeName;
                    shadecd.Text = lotResponse.ShadeCode;
                    deniervalue.Text = lotResponse.Denier.ToString();
                    twistvalue.Text = lotResponse.TwistName;
                    productionRequest.SaleLot = lotResponse.SaleLot;
                    productionRequest.MachineId = lotResponse.MachineId;
                    productionRequest.ItemId = lotResponse.ItemId;
                    productionRequest.ShadeId = lotResponse.ShadeId;

                    var itemResponse = _masterService.getItemById(lotResponse.ItemId);

                    var qualityList = getQualityListByItemTypeId(itemResponse.ItemTypeId);
                    qualityList.Insert(0, new QualityResponse { QualityId = 0, Name = "Select Quality" });
                    QualityList.DataSource = qualityList;
                    QualityList.DisplayMember = "Name";
                    QualityList.ValueMember = "QualityId";
                    QualityList.SelectedIndex = 0;

                    getSaleOrderList(productionRequest.LotId);

                    List<LotsDetailsResponse> lotsDetailsList = new List<LotsDetailsResponse>();
                    foreach (var lot in lotResponse.LotsDetailsResponses)
                    {
                        LotsDetailsResponse lotsDetails = new LotsDetailsResponse();
                        lotsDetails.LotId = lot.LotId;
                        lotsDetails.UpdatedOn = lot.UpdatedOn;
                        lotsDetails.UpdatedBy = lot.UpdatedBy;
                        lotsDetails.CreatedBy = lot.CreatedBy;
                        lotsDetails.CreatedOn = lot.CreatedOn;
                        lotsDetails.EffectiveFrom = lot.EffectiveFrom;
                        lotsDetails.EffectiveUpto = lot.EffectiveUpto;
                        lotsDetails.GainLossPerc = lot.GainLossPerc;
                        lotsDetails.InputPerc = lot.InputPerc;
                        lotsDetails.ProductionPerc = lot.ProductionPerc;
                        lotsDetails.Extruder = lot.Extruder;
                        lotsDetails.LotType = lot.LotType;
                        lotsDetails.PrevLotId = lot.PrevLotId;
                        lotsDetails.PrevLotNo = lot.PrevLotNo;
                        lotsDetails.PrevLotType = lot.PrevLotType;
                        lotsDetails.PrevLotQuality = lot.PrevLotQuality;
                        lotsDetails.PrevLotItemName = lot.PrevLotItemName;
                        lotsDetails.PrevLotShadeName = lot.PrevLotShadeName;
                        lotsDetails.PrevLotShadeCode = lot.PrevLotShadeCode;
                        lotsDetailsList.Add(lot);
                    }
                    rowMaterial.Columns.Clear();
                    rowMaterial.Columns.Add(new DataGridViewTextBoxColumn { Name = "PrevLotType", DataPropertyName = "PrevLotType", HeaderText = "Prev.LotType" });
                    rowMaterial.Columns.Add(new DataGridViewTextBoxColumn { Name = "PrevLotNo", DataPropertyName = "PrevLotNo", HeaderText = "Prev.LotNo" });
                    rowMaterial.Columns.Add(new DataGridViewTextBoxColumn { Name = "PrevLotItemName", DataPropertyName = "PrevLotItemName", HeaderText = "Prev.LotItem" });
                    rowMaterial.Columns.Add(new DataGridViewTextBoxColumn { Name = "PrevLotShadeName", DataPropertyName = "PrevLotShadeName", HeaderText = "Prev.LotShade" });
                    rowMaterial.Columns.Add(new DataGridViewTextBoxColumn { Name = "PrevLotQuality", DataPropertyName = "PrevLotQuality", HeaderText = "Quality" });
                    rowMaterial.Columns.Add(new DataGridViewTextBoxColumn { Name = "ProductionPerc", DataPropertyName = "ProductionPerc", HeaderText = "Production %" });
                    rowMaterial.Columns.Add(new DataGridViewTextBoxColumn { Name = "EffectiveFrom", DataPropertyName = "EffectiveFrom", HeaderText = "EffectiveFrom" });
                    rowMaterial.Columns.Add(new DataGridViewTextBoxColumn { Name = "EffectiveUpto", DataPropertyName = "EffectiveUpto", HeaderText = "EffectiveUpto" });
                    rowMaterial.DataSource = lotsDetailsList;
                }
            }
        }

        private void PackSizeList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isFormReady) return;

            if (PackSizeList.SelectedIndex <= 0)
            {
                frdenier.Text = "";
                updenier.Text = "";
                return;
            }
            if (PackSizeList.SelectedIndex > 0)
            {
                packsizeerror.Text = "";
                packsizeerror.Visible = false;
            }
            if (PackSizeList.SelectedValue != null)
            {
                packsizeerror.Visible = false;
                PackSizeResponse selectedPacksize = (PackSizeResponse)PackSizeList.SelectedItem;
                int selectedPacksizeId = selectedPacksize.PackSizeId;

                productionRequest.PackSizeId = selectedPacksizeId;

                if (selectedPacksizeId > 0)
                {
                    var packsize = _masterService.getPackSizeById(selectedPacksizeId);
                    frdenier.Text = packsize.FromDenier.ToString();
                    updenier.Text = packsize.UpToDenier.ToString();
                }
            }
        }

        private void QualityList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isFormReady) return;

            if (QualityList.SelectedIndex > 0)
            {
                qualityerror.Text = "";
                qualityerror.Visible = false;
            }
            if (QualityList.SelectedValue != null)
            {
                qualityerror.Visible = false;
                QualityResponse selectedQuality = (QualityResponse)QualityList.SelectedItem;
                int selectedQualityId = selectedQuality.QualityId;

                productionRequest.QualityId = selectedQualityId;
            }
        }

        private void WindingTypeList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isFormReady) return;

            if (WindingTypeList.SelectedIndex > 0)
            {
                windingerror.Text = "";
                windingerror.Visible = false;
            }
            if (WindingTypeList.SelectedValue != null)
            {
                windingerror.Visible = false;
                WindingTypeResponse selectedWindingType = (WindingTypeResponse)WindingTypeList.SelectedItem;
                int selectedWindingTypeId = selectedWindingType.WindingTypeId;

                productionRequest.WindingTypeId = selectedWindingTypeId;
            }
        }

        private void SaleOrderList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isFormReady) return;

            if (SaleOrderList.SelectedIndex > 0)
            {
                soerror.Text = "";
                soerror.Visible = false;
            }
            if (SaleOrderList.SelectedValue != null)
            {
                soerror.Visible = false;
                LotSaleOrderDetailsResponse selectedSaleOrder = (LotSaleOrderDetailsResponse)SaleOrderList.SelectedItem;
                int selectedSaleOrderId = selectedSaleOrder.SaleOrderDetailsId;

                if (selectedSaleOrderId > 0)
                {
                    productionRequest.SaleOrderId = selectedSaleOrderId;
                    selectedSOId = selectedSaleOrderId;
                    var saleOrderItemResponse = _saleService.getSaleOrderItemByItemIdAndShadeIdAndSaleOrderId(lotResponse.ItemId, lotResponse.ShadeId, selectedSaleOrderId);
                    if (saleOrderItemResponse != null)
                    {
                        productionRequest.SaleOrderItemId = saleOrderItemResponse.SaleOrderItemsId;
                        productionRequest.ContainerTypeId = saleOrderItemResponse.ContainerTypeId;
                    }
                    RefreshLastBoxDetails();
                }
            }
        }

        private async void RefreshLastBoxDetails()
        {
            var getLastBox = await Task.Run(() => getLastBoxDetails());

            //lastboxdetails
            if (getLastBox.ProductionId > 0)
            {
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

            if (ComPortList.SelectedValue != null)
            {
                var ComPort = ComPortList.SelectedValue.ToString();
            }
        }

        private void WeighingList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isFormReady) return;

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

        private void CopsItemList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isFormReady) return;

            if (CopsItemList.SelectedValue != null)
            {
                ItemResponse selectedCopsItem = (ItemResponse)CopsItemList.SelectedItem;
                int selectedItemId = selectedCopsItem.ItemId;

                if (selectedItemId > 0)
                {
                    productionRequest.SpoolItemId = selectedItemId;

                    var itemResponse = _masterService.getItemById(selectedItemId);
                    if (itemResponse != null)
                    {
                        copsitemwt.Text = itemResponse.Weight.ToString();
                    }
                }
            }
        }

        private void PrefixList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isFormReady) return;

            if (PrefixList.SelectedIndex <= 0)
            {
                prodtype.Text = "";
                return;
            }

            if (PrefixList.SelectedIndex > 0)
            {
                boxnoerror.Text = "";
                boxnoerror.Visible = false;
            }

            if (PrefixList.SelectedValue != null)
            {
                boxnoerror.Visible = false;
                PrefixResponse selectedPrefix = (PrefixResponse)PrefixList.SelectedItem;
                int selectedPrefixId = selectedPrefix.PrefixCode;

                productionRequest.PrefixCode = selectedPrefixId;

                prodtype.Text = selectedPrefix.ProductionType.ToString();
                productionRequest.ProdTypeId = selectedPrefix.ProductionTypeId;
            }
        }

        private List<MachineResponse> getMachineList()
        {
            var getMachine = _masterService.getMachineList();
            return getMachine;
        }

        private void getLotList(int machineId)
        {
            var getLots = _productionService.getLotList(machineId);
            getLots.Insert(0, new LotsResponse { LotId = 0, LotNo = "Select MergeNo" });
            MergeNoList.DataSource = getLots;
            MergeNoList.DisplayMember = "LotNo";
            MergeNoList.ValueMember = "LotId";
            MergeNoList.SelectedIndex = 0;
        }
        private List<LotsResponse> getAllLotList()
        {
            var getLots = _productionService.getAllLotList();
            return getLots;
        }
        private List<QualityResponse> getQualityListByItemTypeId(int itemTypeId)
        {
            var getQuality = _masterService.getQualityListByItemTypeId(itemTypeId);
            return getQuality;
        }

        private List<PackSizeResponse> getPackSizeList()
        {
            var getPackSize = _masterService.getPackSizeList();
            return getPackSize;
        }

        private List<WindingTypeResponse> getWindingTypeList()
        {
            var getWindingType = _masterService.getWindingTypeList();
            return getWindingType;
        }

        private void getSaleOrderList(int lotId)
        {
            var getSaleOrder = _productionService.getSaleOrderList(lotId);
            getSaleOrder.Insert(0, new LotSaleOrderDetailsResponse { SaleOrderDetailsId = 0, SaleOrderNumber = "Select Sale Order" });
            SaleOrderList.DataSource = getSaleOrder;
            SaleOrderList.DisplayMember = "SaleOrderNumber";
            SaleOrderList.ValueMember = "SaleOrderDetailsId";
            SaleOrderList.SelectedIndex = 0;
        }

        private List<string> getComPortList()
        {
            var getComPortType = new List<string>
            {
                "Select Com Port",
                "COM1",
                "COM2",
                "COM3"
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

        private List<ItemResponse> getCopeItemList()
        {
            var getCopeItem = _masterService.getCopeItemList();

            return getCopeItem;
        }

        private List<PrefixResponse> getPrefixList()
        {
            var getPrefix = _masterService.getPrefixList();
            return getPrefix;
        }

        private ProductionResponse getLastBoxDetails()
        {
            var getPacking = _packingService.getLastBoxDetails("chipspacking");
            return getPacking;
        }

        private ProductionResponse getProductionById(long productionId)
        {
            var getProduction = _packingService.getProductionById(productionId);
            return getProduction;
        }

        private void SpoolWeight_TextChanged(object sender, EventArgs e)
        {
            CalculateTareWeight();
        }

        private void PalletWeight_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(palletwtno.Text))
            {
                palletwterror.Visible = true;
            }
            else
            {
                CalculateTareWeight();
                palletwterror.Text = "";
                palletwterror.Visible = false;
            }
        }

        private void CalculateTareWeight()
        {
            int num1 = 0;
            int.TryParse(palletwtno.Text, out num1);

            tarewt.Text = (num1).ToString();
        }

        private void GrossWeight_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(grosswtno.Text))
            {
                grosswterror.Visible = true;
            }
            else
            {
                CalculateNetWeight();
                grosswterror.Text = "";
                grosswterror.Visible = false;
            }
        }

        private void CalculateNetWeight()
        {
            int num1 = 0, num2 = 0;

            int.TryParse(grosswtno.Text, out num1);
            int.TryParse(tarewt.Text, out num2);

            netwt.Text = (num1 - num2).ToString();
        }

        private void NetWeight_TextChanged(object sender, EventArgs e)
        {
            CalculateWeightPerCop();
        }

        private void SpoolNo_TextChanged(object sender, EventArgs e)
        {
            CalculateWeightPerCop();
        }

        private void CalculateWeightPerCop()
        {
            int num1 = 0, num2 = 0;

            int.TryParse(netwt.Text, out num1);
            //int.TryParse(spoolno.Text, out num2);

            //wtpercop.Text = (num1 / num2).ToString();
            wtpercop.Text = (num1).ToString();
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

        private async void submitForm(bool isPrint)
        {
            if (ValidateForm())
            {
                productionRequest.PackingType = "ChipsPacking";
                productionRequest.Remarks = remarks.Text.Trim();
                productionRequest.Spools = 0;
                productionRequest.SpoolsWt = 0;
                productionRequest.EmptyBoxPalletWt = Convert.ToDecimal(palletwtno.Text.Trim());
                productionRequest.GrossWt = Convert.ToDecimal(grosswtno.Text.Trim());
                productionRequest.NoOfCopies = Convert.ToInt32(copyno.Text.Trim());
                productionRequest.TareWt = Convert.ToDecimal(tarewt.Text.Trim());
                productionRequest.NetWt = Convert.ToDecimal(netwt.Text.Trim());
                productionRequest.ProductionDate = dateTimePicker1.Value;

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
            result = _packingService.AddUpdatePOYPacking(_productionId, productionRequest);
            if (result != null && result.ProductionId > 0)
            {
                submit.Enabled = true;
                saveprint.Enabled = true;
                RefreshLastBoxDetails();
                this.grosswtno.Text = "";
                this.tarewt.Text = "";
                this.netwt.Text = "";
                this.wtpercop.Text = "";
                if (_productionId == 0)
                {
                    MessageBox.Show("Chips Packing added successfully!",
                    "Success",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                    if (isPrint)
                    {
                    }
                }
                else
                {
                    MessageBox.Show("Chips Packing updated successfully.");
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
            return result;
        }

        private bool ValidateForm()
        {
            bool isValid = true;

            if (LineNoList.SelectedIndex <= 0)
            {
                linenoerror.Text = "Please select a line no";
                linenoerror.Visible = true;
                isValid = false;
            }

            if (string.IsNullOrWhiteSpace(copyno.Text))
            {
                copynoerror.Text = "Please enter no of copies";
                copynoerror.Visible = true;
                isValid = false;
            }

            if (MergeNoList.SelectedIndex <= 0)
            {
                mergenoerror.Text = "Please select merge no";
                mergenoerror.Visible = true;
                isValid = false;
            }

            if (QualityList.SelectedIndex <= 0)
            {
                qualityerror.Text = "Please select quantity";
                qualityerror.Visible = true;
                isValid = false;
            }

            if (SaleOrderList.SelectedIndex <= 0)
            {
                soerror.Text = "Please select sale order";
                soerror.Visible = true;
                isValid = false;
            }

            if (PackSizeList.SelectedIndex <= 0)
            {
                packsizeerror.Text = "Please select pack size";
                packsizeerror.Visible = true;
                isValid = false;
            }

            if (WindingTypeList.SelectedIndex <= 0)
            {
                windingerror.Text = "Please select winding type";
                windingerror.Visible = true;
                isValid = false;
            }

            if (PrefixList.SelectedIndex <= 0)
            {
                boxnoerror.Text = "Please select prefix";
                boxnoerror.Visible = true;
                isValid = false;
            }

            if (string.IsNullOrWhiteSpace(palletwtno.Text) || Convert.ToDecimal(palletwtno.Text) == 0)
            {
                palletwterror.Text = "Please enter pallet wt";
                palletwterror.Visible = true;
                isValid = false;
            }

            if (string.IsNullOrWhiteSpace(grosswtno.Text) || Convert.ToDecimal(grosswtno.Text) == 0)
            {
                grosswterror.Text = "Please enter gross wt";
                grosswterror.Visible = true;
                isValid = false;
            }

            return isValid;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            var dashboard = this.ParentForm as AdminAccount;
            if (dashboard != null)
            {
                dashboard.LoadFormInContent(new ChipsPackingList());
            }
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

        //private async void sidebarTimer_Tick(object sender, EventArgs e)
        //{
        //    showSidebarBorder = false;

        //    if (sidebarExpand)
        //    {
        //        this.sidebarContainer.Width -= 10;
        //        if (sidebarContainer.Width == sidebarContainer.MinimumSize.Width)
        //        {
        //            panel12.Width = panel12.MinimumSize.Width;
        //            panel10.Width = panel10.MinimumSize.Width;

        //            if (panel10.Width == panel10.MinimumSize.Width)
        //            {
        //                sidebarExpand = false;
        //                leftpanel.Width = leftpanel.MinimumSize.Width;
        //            }
        //            sidebarTimer.Stop();
        //            sidebarContainer.Invalidate();
        //        }
        //    }
        //    else
        //    {
        //        this.sidebarContainer.Width += 10;
        //        if (sidebarContainer.Width == sidebarContainer.MaximumSize.Width)
        //        {
        //            panel12.Width = panel12.MaximumSize.Width;
        //            panel10.Width = panel10.MaximumSize.Width;

        //            if (panel10.Width == panel10.MaximumSize.Width)
        //            {
        //                sidebarExpand = true;
        //                leftpanel.Width = leftpanel.MaximumSize.Width;
        //            }
        //            sidebarTimer.Stop();
        //            sidebarContainer.Invalidate();
        //        }
        //    }

        //    // Show border after all animations
        //    showSidebarBorder = true;
        //    sidebarContainer.Invalidate();
        //}

        //private void menuBtn_Click(object sender, EventArgs e)
        //{
        //    sidebarTimer.Start();
        //}

        //private void sidebarContainer_Paint(object sender, PaintEventArgs e)
        //{
        //    if (showSidebarBorder)   // only draw when allowed
        //    {
        //        _cmethod.DrawRightBorder(sidebarContainer, e, Color.FromArgb(191, 191, 191), 1);
        //    }
        //}

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow control keys (backspace, delete, etc.)
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Reject the input
            }
        }
    }
}
