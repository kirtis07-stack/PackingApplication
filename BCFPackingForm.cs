﻿using PackingApplication.Helper;
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
    public partial class BCFPackingForm : Form
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
        public BCFPackingForm(long productionId)
        {
            InitializeComponent();
            ApplyFonts();
            _productionId = productionId;
            this.Shown += BCFPackingForm_Shown;
            this.AutoScroll = true;

            _cmethod.SetButtonBorderRadius(this.addqty, 8);
            _cmethod.SetButtonBorderRadius(this.submit, 8);
            _cmethod.SetButtonBorderRadius(this.cancelbtn, 8);
            _cmethod.SetButtonBorderRadius(this.saveprint, 8);

            LineNoList.SelectedIndexChanged += LineNoList_SelectedIndexChanged;
            MergeNoList.SelectedIndexChanged += MergeNoList_SelectedIndexChanged;
            PackSizeList.SelectedIndexChanged += PackSizeList_SelectedIndexChanged;
            QualityList.SelectedIndexChanged += QualityList_SelectedIndexChanged;
            WindingTypeList.SelectedIndexChanged += WindingTypeList_SelectedIndexChanged;
            SaleOrderList.SelectedIndexChanged += SaleOrderList_SelectedIndexChanged;
            PrefixList.SelectedIndexChanged += PrefixList_SelectedIndexChanged;
            copyno.TextChanged += CopyNos_TextChanged;
            spoolno.TextChanged += SpoolNo_TextChanged;
            spoolwt.TextChanged += SpoolWeight_TextChanged;
            palletwtno.TextChanged += PalletWeight_TextChanged;
            grosswtno.TextChanged += GrossWeight_TextChanged;
            width = flowLayoutPanel1.ClientSize.Width;
            rowMaterial.AutoGenerateColumns = false;
            windinggrid.AutoGenerateColumns = false;
            qualityqty.AutoGenerateColumns = false;
            //sidebarContainer.Width = sidebarContainer.MinimumSize.Width;
            //panel10.Width = panel10.MinimumSize.Width;
            //panel12.Width = panel12.MinimumSize.Width;
            //leftpanel.Width = leftpanel.MinimumSize.Width;
        }

        private void BCFPackingForm_Load(object sender, EventArgs e)
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
            boxpalletitemwt.Text = "0";
            boxpalletstock.Text = "0";
            copsitemwt.Text = "0";
            copsstock.Text = "0";
            frdenier.Text = "0";
            updenier.Text = "0";
            deniervalue.Text = "0";
            isFormReady = true;
        }

        private void getLotRelatedDetails()
        {
            var getSaleOrder = new List<LotSaleOrderDetailsResponse>();
            getSaleOrder.Insert(0, new LotSaleOrderDetailsResponse { SaleOrderDetailsId = 0, SaleOrderNumber = "Select Sale Order" });
            SaleOrderList.DataSource = getSaleOrder;
            SaleOrderList.DisplayMember = "SaleOrderNumber";
            SaleOrderList.ValueMember = "SaleOrderDetailsId";
            SaleOrderList.SelectedIndex = 0;

            var windingtypeList = new List<LotsProductionDetailsResponse>();
            windingtypeList.Insert(0, new LotsProductionDetailsResponse { WindingTypeId = 0, WindingTypeName = "Select Winding Type" });
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
            this.copsstock.Font = FontManager.GetFont(8F, FontStyle.Regular);
            this.boxtype.Font = FontManager.GetFont(8F, FontStyle.Bold);
            this.boxweight.Font = FontManager.GetFont(8F, FontStyle.Bold);
            this.boxpalletitemwt.Font = FontManager.GetFont(8F, FontStyle.Regular);
            this.boxstock.Font = FontManager.GetFont(8F, FontStyle.Bold);
            this.boxpalletstock.Font = FontManager.GetFont(8F, FontStyle.Regular);
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
            this.BoxItemList.Font = FontManager.GetFont(8F, FontStyle.Regular);
            this.SaleOrderList.Font = FontManager.GetFont(8F, FontStyle.Regular);
            this.prcompany.Font = FontManager.GetFont(8F, FontStyle.Regular);
            this.prowner.Font = FontManager.GetFont(8F, FontStyle.Regular);
            this.prdate.Font = FontManager.GetFont(8F, FontStyle.Regular);
            this.pruser.Font = FontManager.GetFont(8F, FontStyle.Regular);
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
            this.addqty.Font = FontManager.GetFont(8F, FontStyle.Bold);
            this.flowLayoutPanel1.Font = FontManager.GetFont(8F, FontStyle.Regular);
            this.submit.Font = FontManager.GetFont(8F, FontStyle.Bold);
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
            //this.reviewlbl.Font = FontManager.GetFont(9F, FontStyle.Bold);
            //this.reviewsubtitle.Font = FontManager.GetFont(8F, FontStyle.Regular);
            //this.weighlbl.Font = FontManager.GetFont(9F, FontStyle.Bold);
            //this.weighsubtitle.Font = FontManager.GetFont(8F, FontStyle.Regular);
            //this.packaginglbl.Font = FontManager.GetFont(9F, FontStyle.Bold);
            //this.packagingsubtitle.Font = FontManager.GetFont(8F, FontStyle.Regular);
            //this.orderlbl.Font = FontManager.GetFont(9F, FontStyle.Bold);
            //this.orderdetailssubtitle.Font = FontManager.GetFont(8F, FontStyle.Regular);
            this.grdsoqty.Font = FontManager.GetFont(8F, FontStyle.Regular);
            this.prodnbalqty.Font = FontManager.GetFont(8F, FontStyle.Regular);
            //this.rowMaterial.Font = FontManager.GetFont(8F, FontStyle.Regular);
            this.rowMaterialBox.Font = FontManager.GetFont(8F, FontStyle.Bold);
            this.spoolweight.Font = FontManager.GetFont(8F, FontStyle.Bold);
            this.fromdenier.Font = FontManager.GetFont(8F, FontStyle.Bold);
            this.uptodenier.Font = FontManager.GetFont(8F, FontStyle.Bold);
            this.Font = FontManager.GetFont(9F, FontStyle.Bold);
        }

        private async void BCFPackingForm_Shown(object sender, EventArgs e)
        {
            var machineTask = getMachineList();
            var lotTask = getAllLotList();
            var prefixTask = getPrefixList();
            var packsizeTask = getPackSizeList();
            var copsitemTask = getCopeItemList();
            var boxitemTask = getBoxItemList();
            var palletitemTask = getPalletItemList();

            // 2. Wait for all to complete
            await Task.WhenAll(machineTask, lotTask, prefixTask, packsizeTask, copsitemTask, boxitemTask, palletitemTask);

            // 3. Get the results
            var machineList = machineTask.Result;
            var lotList = lotTask.Result;
            var prefixList = prefixTask.Result;
            var packsizeList = packsizeTask.Result;
            var copsitemList = copsitemTask.Result;
            var boxitemList = boxitemTask.Result;
            var palletitemList = palletitemTask.Result;

            //machine
            machineList.Insert(0, new MachineResponse { MachineId = 0, MachineName = "Select Line No." });
            LineNoList.DataSource = machineList;
            LineNoList.DisplayMember = "MachineName";
            LineNoList.ValueMember = "MachineId";
            LineNoList.SelectedIndex = 0;
            LineNoList.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            LineNoList.AutoCompleteSource = AutoCompleteSource.ListItems;
            //LineNoList.DropDownStyle = ComboBoxStyle.DropDown;

            //lot
            lotList.Insert(0, new LotsResponse { LotId = 0, LotNo = "Select MergeNo" });
            MergeNoList.DataSource = lotList;
            MergeNoList.DisplayMember = "LotNo";
            MergeNoList.ValueMember = "LotId";
            MergeNoList.SelectedIndex = 0;
            MergeNoList.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            MergeNoList.AutoCompleteSource = AutoCompleteSource.ListItems;
            //MergeNoList.DropDownStyle = ComboBoxStyle.DropDown;

            //prefix
            prefixList.Insert(0, new PrefixResponse { PrefixCode = 0, Prefix = "Select Prefix" });
            PrefixList.DataSource = prefixList;
            PrefixList.DisplayMember = "Prefix";
            PrefixList.ValueMember = "PrefixCode";
            PrefixList.SelectedIndex = 0;
            PrefixList.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            PrefixList.AutoCompleteSource = AutoCompleteSource.ListItems;
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
            ComPortList.DropDownStyle = ComboBoxStyle.DropDown;

            var weightingList = await Task.Run(() => getWeighingList());
            //weighting
            WeighingList.DataSource = weightingList;
            WeighingList.DisplayMember = "Name";
            WeighingList.ValueMember = "Id";
            WeighingList.SelectedIndex = 0;
            WeighingList.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            WeighingList.AutoCompleteSource = AutoCompleteSource.ListItems;
            WeighingList.DropDownStyle = ComboBoxStyle.DropDown;

            //copsitem
            copsitemList.Insert(0, new ItemResponse { ItemId = 0, Name = "Select Cops Item" });
            CopsItemList.DataSource = copsitemList;
            CopsItemList.DisplayMember = "Name";
            CopsItemList.ValueMember = "ItemId";
            CopsItemList.SelectedIndex = 0;
            CopsItemList.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            CopsItemList.AutoCompleteSource = AutoCompleteSource.ListItems;
            //CopsItemList.DropDownStyle = ComboBoxStyle.DropDown;

            //palletitem
            palletitemList.Insert(0, new ItemResponse { ItemId = 0, Name = "Select Box/Pallet" });
            PalletTypeList.DataSource = palletitemList;
            PalletTypeList.DisplayMember = "Name";
            PalletTypeList.ValueMember = "ItemId";
            PalletTypeList.SelectedIndex = 0;
            PalletTypeList.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            PalletTypeList.AutoCompleteSource = AutoCompleteSource.ListItems;
            //PalletTypeList.DropDownStyle = ComboBoxStyle.DropDown;

            //boxitem
            boxitemList.Insert(0, new ItemResponse { ItemId = 0, Name = "Select Box/Pallet" });
            BoxItemList.DataSource = boxitemList;
            BoxItemList.DisplayMember = "Name";
            BoxItemList.ValueMember = "ItemId";
            BoxItemList.SelectedIndex = 0;
            BoxItemList.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            BoxItemList.AutoCompleteSource = AutoCompleteSource.ListItems;
            //BoxItemList.DropDownStyle = ComboBoxStyle.DropDown;

            RefreshLastBoxDetails();

            if (Convert.ToInt64(_productionId) > 0)
            {
                var productionResponse = Task.Run(() => getProductionById(Convert.ToInt64(_productionId))).Result;

                if (productionResponse != null)
                {
                    LineNoList.SelectedValue = productionResponse.MachineId;
                    departmentname.Text = productionResponse.DepartmentName;
                    PrefixList.SelectedValue = 316;         //added hardcoded for now
                    MergeNoList.SelectedValue = productionResponse.LotId;
                    dateTimePicker1.Text = productionResponse.ProductionDate.ToString();
                    dateTimePicker1.Value = productionResponse.ProductionDate;
                    QualityList.SelectedValue = productionResponse.QualityId;
                    WindingTypeList.SelectedValue = productionResponse.WindingTypeId;
                    SaleOrderList.SelectedValue = productionResponse.SaleOrderId;
                    PackSizeList.SelectedValue = productionResponse.PackSizeId;
                    CopsItemList.SelectedValue = productionResponse.SpoolItemId;
                    BoxItemList.SelectedValue = productionResponse.BoxItemId;
                    prodtype.Text = productionResponse.ProductionType;
                    remarks.Text = productionResponse.Remarks;
                    prcompany.Checked = productionResponse.PrintCompany;
                    prowner.Checked = productionResponse.PrintOwner;
                    prdate.Checked = productionResponse.PrintDate;
                    pruser.Checked = productionResponse.PrintUser;
                    prwtps.Checked = productionResponse.PrintWTPS;
                    prqrcode.Checked = productionResponse.PrintQRCode;
                    spoolno.Text = productionResponse.Spools.ToString();
                    spoolwt.Text = productionResponse.SpoolsWt.ToString();
                    palletwtno.Text = productionResponse.EmptyBoxPalletWt.ToString();
                    grosswtno.Text = productionResponse.GrossWt.ToString();
                    tarewt.Text = productionResponse.TareWt.ToString();
                    netwt.Text = productionResponse.NetWt.ToString();
                    submit.Text = "Update";
                    saveprint.Enabled = false;

                    if (productionResponse.PalletDetailsResponse.Count > 0)
                    {
                        if (productionResponse?.PalletDetailsResponse != null && productionResponse.PalletDetailsResponse.Any())
                        {
                            BindPalletDetails(productionResponse.PalletDetailsResponse);
                        }
                    }
                }
            }

            isFormReady = true;
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
                var palletItemList = Task.Run(() => getPalletItemList()).Result;
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
                System.Windows.Forms.Label lblSrNo = new System.Windows.Forms.Label() { Text = rowCount.ToString(), Width = 30, Location = new Point(2, 10), Font = FontManager.GetFont(8F, FontStyle.Regular) };

                // Item Name
                System.Windows.Forms.Label lblItem = new System.Windows.Forms.Label() { Text = selectedItem.Name, Width = 140, Location = new Point(50, 10), Font = FontManager.GetFont(8F, FontStyle.Regular), Tag = selectedItem.ItemId };

                // Qty
                System.Windows.Forms.Label lblQty = new System.Windows.Forms.Label() { Text = palletDetail.Quantity.ToString(), Width = 50, Location = new Point(200, 10), Font = FontManager.GetFont(8F, FontStyle.Regular) };
                // Edit Button
                System.Windows.Forms.Button btnEdit = new System.Windows.Forms.Button() { Text = "Edit", Size = new Size(35, 23), Location = new Point(250, 5), Font = FontManager.GetFont(7F, FontStyle.Regular), BackColor = Color.FromArgb(230, 240, 255), ForeColor = Color.FromArgb(51, 133, 255), Tag = new Tuple<ItemResponse, System.Windows.Forms.Label>(selectedItem, lblQty), FlatStyle = FlatStyle.Flat };
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
                System.Windows.Forms.Button btnDelete = new System.Windows.Forms.Button() { Text = "Remove", Size = new Size(50, 23), Location = new Point(300, 5), Font = FontManager.GetFont(7F, FontStyle.Regular), BackColor = Color.FromArgb(255, 230, 230), ForeColor = Color.FromArgb(255, 51, 51), Tag = rowPanel, FlatStyle = FlatStyle.Flat };
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
                    var rect = new Rectangle(0, 0, btnDelete.Width - 1, btnDelete.Height - 1);

                    using (GraphicsPath path = _cmethod.GetRoundedRect(rect, 4))
                    using (Pen borderPen = new Pen(btnDelete.FlatAppearance.BorderColor, btnDelete.FlatAppearance.BorderSize))
                    using (SolidBrush brush = new SolidBrush(btnDelete.BackColor))
                    {
                        f.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

                        f.Graphics.FillPath(brush, path);
                        f.Graphics.DrawPath(borderPen, path);

                        TextRenderer.DrawText(
                            f.Graphics,
                            btnDelete.Text,
                            btnDelete.Font,
                            rect,
                            btnDelete.ForeColor,
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
            }

            flowLayoutPanel1.AutoScroll = true;
            flowLayoutPanel1.WrapContents = false;
            flowLayoutPanel1.FlowDirection = FlowDirection.TopDown;
        }

        private ProductionRequest productionRequest = new ProductionRequest();
        private bool isFormReady = false;
        private async void LineNoList_SelectedIndexChanged(object sender, EventArgs e)
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
                    var department = await Task.Run(() => _masterService.getMachineById(selectedMachineId));
                    departmentname.Text = department.DepartmentName;
                    productionRequest.DepartmentId = department.DepartmentId;

                    getLotList(selectedMachineId);
                }
            }
        }

        private async void MergeNoList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isFormReady) return;

            if (MergeNoList.SelectedIndex <= 0)
            {
                itemname.Text = "";
                shadename.Text = "";
                shadecd.Text = "";
                deniervalue.Text = "";
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
                mergenoerror.Text = "";
                mergenoerror.Visible = false;
            }
            if (MergeNoList.SelectedValue != null)
            {
                mergenoerror.Visible = false;

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
                    productionRequest.SaleLot = lotResponse.SaleLot;
                    productionRequest.MachineId = lotResponse.MachineId;
                    productionRequest.ItemId = lotResponse.ItemId;
                    productionRequest.ShadeId = lotResponse.ShadeId;
                    LineNoList.SelectedValue = lotResponse.MachineId;

                    if (lotResponse.ItemId > 0)
                    {
                        var itemResponse = await Task.Run(() => _masterService.getItemById(lotResponse.ItemId));

                        var qualityList = await getQualityListByItemTypeId(itemResponse.ItemTypeId);
                        qualityList.Insert(0, new QualityResponse { QualityId = 0, Name = "Select Quality" });
                        QualityList.DataSource = qualityList;
                        QualityList.DisplayMember = "Name";
                        QualityList.ValueMember = "QualityId";
                        QualityList.SelectedIndex = 0;
                        QualityList.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                        QualityList.AutoCompleteSource = AutoCompleteSource.ListItems;
                        QualityList.DropDownStyle = ComboBoxStyle.DropDown;
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

                    getWindingTypeList(productionRequest.LotId);
                    getSaleOrderList(productionRequest.LotId);
                    lotsDetailsList = new List<LotsDetailsResponse>();
                    if (lotResponse.LotsDetailsResponses != null)
                    {
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
                        rowMaterial.Columns.Add(new DataGridViewTextBoxColumn { Name = "EffectiveFrom", DataPropertyName = "EffectiveFrom", HeaderText = "EffectiveFrom", Width = 150 });
                        rowMaterial.Columns.Add(new DataGridViewTextBoxColumn { Name = "EffectiveUpto", DataPropertyName = "EffectiveUpto", HeaderText = "EffectiveUpto", Width = 150 });
                        rowMaterial.DataSource = lotsDetailsList;
                    }
                }
            }
        }

        private async void PackSizeList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isFormReady) return;

            if (PackSizeList.SelectedIndex <= 0)
            {
                frdenier.Text = "0";
                updenier.Text = "0";
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
                    var packsize = await Task.Run(() => _masterService.getPackSizeById(selectedPacksizeId));
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
                LotsProductionDetailsResponse selectedWindingType = (LotsProductionDetailsResponse)WindingTypeList.SelectedItem;
                int selectedWindingTypeId = selectedWindingType.WindingTypeId;

                if (selectedWindingTypeId > 0)
                {
                    productionRequest.WindingTypeId = selectedWindingTypeId;
                    RefreshWindingGrid();
                }
            }
        }

        private async void SaleOrderList_SelectedIndexChanged(object sender, EventArgs e)
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
                string soNumber = selectedSaleOrder.SaleOrderNumber;
                productionRequest.SaleOrderId = selectedSaleOrderId;

                if (selectedSaleOrderId > 0)
                {
                    selectedSOId = selectedSaleOrderId;
                    selectedSONumber = selectedSaleOrder.SaleOrderNumber;
                    totalSOQty = 0;
                    grdsoqty.Text = "";
                    var saleOrderItemResponse = await Task.Run(() => _saleService.getSaleOrderItemByItemIdAndShadeIdAndSaleOrderId(lotResponse.ItemId, lotResponse.ShadeId, selectedSaleOrderId));
                    if (saleOrderItemResponse != null)
                    {
                        productionRequest.SaleOrderItemId = saleOrderItemResponse.SaleOrderItemsId;
                        productionRequest.ContainerTypeId = saleOrderItemResponse.ContainerTypeId;
                    }

                    var saleResponse = await getSaleOrderById(selectedSaleOrderId);

                    foreach (var soitem in saleResponse.saleOrderItemsResponses)
                    {
                        totalSOQty += soitem.Quantity;
                    }

                    grdsoqty.Text = totalSOQty.ToString("F2");

                    RefreshGradewiseGrid();
                    RefreshLastBoxDetails();
                }
            }
        }

        private async void RefreshWindingGrid()
        {
            if (WindingTypeList.SelectedValue != null)
            {
                int selectedWindingTypeId = Convert.ToInt32(WindingTypeList.SelectedValue.ToString());
                if (selectedWindingTypeId > 0)
                {
                    var getProductionByWindingType = await getProductionLotIdandSaleOrderIdandPackingType(selectLotId, selectedSOId);
                    List<WindingTypeGridResponse> gridList = new List<WindingTypeGridResponse>();
                    foreach (var winding in getProductionByWindingType)
                    {
                        var existing = gridList.FirstOrDefault(x => x.WindingTypeId == winding.WindingTypeId && x.SaleOrderId == winding.SaleOrderId);

                        if (existing == null)
                        {
                            WindingTypeGridResponse grid = new WindingTypeGridResponse();
                            grid.WindingTypeId = winding.WindingTypeId;
                            grid.SaleOrderId = winding.SaleOrderId;
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
                totalProdQty = 0;
                prodnbalqty.Text = "";
                int selectedQualityId = Convert.ToInt32(QualityList.SelectedValue.ToString());
                var getProductionByQuality = await getProductionLotIdandSaleOrderIdandPackingType(selectLotId, selectedSOId);
                List<QualityGridResponse> gridList = new List<QualityGridResponse>();
                foreach (var quality in getProductionByQuality)
                {
                    var existing = gridList.FirstOrDefault(x => x.QualityId == quality.QualityId && x.SaleOrderId == quality.SaleOrderId);

                    if (existing == null)
                    {
                        QualityGridResponse grid = new QualityGridResponse();
                        grid.QualityId = quality.QualityId;
                        grid.SaleOrderId = quality.SaleOrderId;
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

                foreach (var proditem in gridList)
                {
                    totalProdQty += proditem.GrossWt;
                }
                balanceQty = (totalSOQty - totalProdQty);
                if (balanceQty <= 0)
                {
                    submit.Enabled = false;
                    saveprint.Enabled = false;
                    MessageBox.Show("Quantity not remaining for " + selectedSONumber, "Warning", MessageBoxButtons.OK);
                    ResetForm(this);
                }
                else
                {
                    submit.Enabled = true;
                    saveprint.Enabled = true;
                }
                prodnbalqty.Text = balanceQty.ToString("F2");
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
            }
        }

        private async void CopsItemList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isFormReady) return;

            if (CopsItemList.SelectedIndex <= 0)
            {
                copsitemwt.Text = "0";
                return;
            }

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
                        SpoolNo_TextChanged(sender, e);
                        GrossWeight_TextChanged(sender, e);
                    }
                }
            }
        }

        private async void BoxItemList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isFormReady) return;

            if (BoxItemList.SelectedIndex <= 0)
            {
                boxpalletitemwt.Text = "";
                palletwtno.Text = "";
                return;
            }

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
                        GrossWeight_TextChanged(sender, e);
                    }
                }
            }
        }

        private async void PrefixList_SelectedIndexChanged(object sender, EventArgs e)
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

        private Task<List<MachineResponse>> getMachineList()
        {
            return Task.Run(() => _masterService.getMachineList());
        }

        private async Task getLotList(int machineId)
        {
            var getLots = await Task.Run(() => _productionService.getLotList(machineId));
            getLots.Insert(0, new LotsResponse { LotId = 0, LotNo = "Select MergeNo" });
            MergeNoList.DataSource = getLots;
            MergeNoList.DisplayMember = "LotNo";
            MergeNoList.ValueMember = "LotId";
            MergeNoList.SelectedIndex = 0;
            MergeNoList.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            MergeNoList.AutoCompleteSource = AutoCompleteSource.ListItems;
            //MergeNoList.DropDownStyle = ComboBoxStyle.DropDown;
        }

        private Task<List<LotsResponse>> getAllLotList()
        {
            return Task.Run(() => _productionService.getAllLotList());
        }

        private Task<List<QualityResponse>> getQualityListByItemTypeId(int itemTypeId)
        {
            return Task.Run(() => _masterService.getQualityListByItemTypeId(itemTypeId));
        }

        private Task<List<PackSizeResponse>> getPackSizeList()
        {
            return Task.Run(() => _masterService.getPackSizeList());
        }

        private async Task getWindingTypeList(int lotId)
        {
            var getWindingType = await Task.Run(() => _productionService.getWinderTypeList(lotId));
            getWindingType.Insert(0, new LotsProductionDetailsResponse { WindingTypeId = 0, WindingTypeName = "Select Winding Type" });
            WindingTypeList.DataSource = getWindingType;
            WindingTypeList.DisplayMember = "WindingTypeName";
            WindingTypeList.ValueMember = "WindingTypeId";
            WindingTypeList.SelectedIndex = 0;
            WindingTypeList.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            WindingTypeList.AutoCompleteSource = AutoCompleteSource.ListItems;
            //WindingTypeList.DropDownStyle = ComboBoxStyle.DropDown;
        }

        private async Task getSaleOrderList(int lotId)
        {
            var getSaleOrder = await Task.Run(() => _productionService.getSaleOrderList(lotId));
            getSaleOrder.Insert(0, new LotSaleOrderDetailsResponse { SaleOrderDetailsId = 0, SaleOrderNumber = "Select Sale Order" });
            SaleOrderList.DataSource = getSaleOrder;
            SaleOrderList.DisplayMember = "SaleOrderNumber";
            SaleOrderList.ValueMember = "SaleOrderDetailsId";
            SaleOrderList.SelectedIndex = 0;
            SaleOrderList.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            SaleOrderList.AutoCompleteSource = AutoCompleteSource.ListItems;
            //SaleOrderList.DropDownStyle = ComboBoxStyle.DropDown;
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

        private Task<List<ItemResponse>> getCopeItemList()
        {
            return Task.Run(() => _masterService.getCopeItemList());
        }

        private Task<List<ItemResponse>> getBoxItemList()
        {
            return Task.Run(() => _masterService.getBoxItemList());
        }

        private Task<List<ItemResponse>> getPalletItemList()
        {
            return Task.Run(() => _masterService.getBoxItemList());
        }

        private Task<List<PrefixResponse>> getPrefixList()
        {
            return Task.Run(() => _masterService.getPrefixList());
        }

        private Task<SaleOrderResponse> getSaleOrderById(int saleOrderId)
        {
            return Task.Run(() => _saleService.getSaleOrderById(saleOrderId));
        }

        private Task<ProductionResponse> getProductionById(long productionId)
        {
            return Task.Run(() => _packingService.getProductionById(productionId));
        }

        private Task<List<ProductionResponse>> getProductionLotIdandSaleOrderIdandPackingType(int lotId, int saleOrderId)
        {
            return Task.Run(() => _packingService.getAllByLotIdandSaleOrderIdandPackingType(lotId, saleOrderId));
        }

        private Task<ProductionResponse> getLastBoxDetails()
        {
            return Task.Run(() => _packingService.getLastBoxDetails("bcfpacking"));
        }

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

                    addqty.Text = "Add"; // reset button text back to Add
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
                    System.Windows.Forms.Label lblSrNo = new System.Windows.Forms.Label() { Text = rowCount.ToString(), Width = 30, Location = new Point(2, 10), Font = FontManager.GetFont(8F, FontStyle.Regular) };

                    // Item Name
                    System.Windows.Forms.Label lblItem = new System.Windows.Forms.Label() { Text = selectedItem.Name, Width = 140, Location = new Point(50, 10), Font = FontManager.GetFont(8F, FontStyle.Regular), Tag = selectedItem.ItemId };

                    // Qty
                    System.Windows.Forms.Label lblQty = new System.Windows.Forms.Label() { Text = qty.ToString(), Width = 50, Location = new Point(200, 10), Font = FontManager.GetFont(8F, FontStyle.Regular) };

                    // Edit Button
                    System.Windows.Forms.Button btnEdit = new System.Windows.Forms.Button() { Text = "Edit", Size = new Size(35, 23), Location = new Point(250, 5), Font = FontManager.GetFont(7F, FontStyle.Regular), BackColor = Color.FromArgb(230, 240, 255), ForeColor = Color.FromArgb(51, 133, 255), Tag = new Tuple<ItemResponse, System.Windows.Forms.Label>(selectedItem, lblQty), FlatStyle = FlatStyle.Flat };
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
                    System.Windows.Forms.Button btnDelete = new System.Windows.Forms.Button() { Text = "Remove", Size = new Size(50, 23), Location = new Point(300, 5), Font = FontManager.GetFont(7F, FontStyle.Regular), BackColor = Color.FromArgb(255, 230, 230), ForeColor = Color.FromArgb(255, 51, 51), Tag = rowPanel, FlatStyle = FlatStyle.Flat };
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
                    addqty.Text = "Add";

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
                row.Location = new Point(0, y);
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

            headerPanel.Controls.Add(new System.Windows.Forms.Label() { Text = "SrNo", Width = 30, Location = new Point(2, 10), Font = FontManager.GetFont(7F, FontStyle.Bold) });
            headerPanel.Controls.Add(new System.Windows.Forms.Label() { Text = "Item Name", Width = 140, Location = new Point(50, 10), Font = FontManager.GetFont(7F, FontStyle.Bold) });
            headerPanel.Controls.Add(new System.Windows.Forms.Label() { Text = "Qty", Width = 50, Location = new Point(200, 10), Font = FontManager.GetFont(7F, FontStyle.Bold) });
            headerPanel.Controls.Add(new System.Windows.Forms.Label() { Text = "Action", Width = 120, Location = new Point(250, 10), Font = FontManager.GetFont(7F, FontStyle.Bold) });

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
                addqty.Text = "Update";

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
                spoolwterror.Visible = true;
                CalculateTareWeight();
            }
            else
            {
                spoolwterror.Text = "";
                spoolwterror.Visible = false;
                CalculateTareWeight();
            }
        }

        private void PalletWeight_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(palletwtno.Text))
            {
                palletwterror.Visible = true;
                CalculateTareWeight();
            }
            else
            {
                palletwterror.Text = "";
                palletwterror.Visible = false;
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
                        grosswterror.Text = "";
                        grosswterror.Visible = false;
                    }
                    else
                    {
                        grosswterror.Text = "Gross Wt > Tare Wt";
                        grosswterror.Visible = true;
                        netwt.Text = "0";
                        wtpercop.Text = "0";
                    }
                }
            }
        }

        private void GrossWeight_TextChanged(object sender, EventArgs e)
        {
            if (!isFormReady) return;

            if (selectedSOId == 0)
            {
                soerror.Visible = true;
                soerror.Text = "Please select sale order";
                return;
            }
            if (string.IsNullOrWhiteSpace(grosswtno.Text))
            {
                grosswterror.Visible = true;
                //grosswterror.Text = "Please enter gross weight";
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(tarewt.Text))
                {
                    decimal gross, tare;
                    if (decimal.TryParse(grosswtno.Text, out gross) && decimal.TryParse(tarewt.Text, out tare))
                    {
                        decimal newBalanceQty = balanceQty - gross;
                        if (newBalanceQty < 0)
                        {
                            grosswterror.Text = "No Prod Bal Qty remaining";
                            grosswterror.Visible = true;
                            submit.Enabled = false;
                            saveprint.Enabled = false;
                            return;
                        }
                        else
                        {
                            grosswterror.Text = "";
                            grosswterror.Visible = false;
                            submit.Enabled = true;
                            saveprint.Enabled = true;
                        }
                        if (gross >= tare)
                        {
                            CalculateNetWeight();
                            grosswterror.Text = "";
                            grosswterror.Visible = false;
                        }
                        else
                        {
                            grosswterror.Text = "Gross Wt > Tare Wt";
                            grosswterror.Visible = true;
                            netwt.Text = "0";
                            wtpercop.Text = "0";
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
            if (string.IsNullOrWhiteSpace(spoolno.Text))
            {
                spoolnoerror.Text = "Please enter spool no";
                spoolnoerror.Visible = true;
                tarewt.Text = "0";
            }
            else if (string.IsNullOrWhiteSpace(copsitemwt.Text))
            {
                spoolwt.Text = "";
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
                else if (spoolnum == 0)
                {
                    spoolnoerror.Text = "Spool no > 0";
                    spoolnoerror.Visible = true;
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
                productionRequest.PackingType = "BCFPacking";
                productionRequest.Remarks = remarks.Text.Trim();
                productionRequest.Spools = Convert.ToInt32(spoolno.Text.Trim());
                productionRequest.SpoolsWt = Convert.ToDecimal(spoolwt.Text.Trim());
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
                RefreshWindingGrid();
                RefreshGradewiseGrid();
                RefreshLastBoxDetails();
                this.spoolno.Text = "";
                this.spoolnoerror.Text = "";
                this.spoolnoerror.Visible = false;
                this.spoolwt.Text = "";
                this.grosswtno.Text = "";
                this.grosswterror.Text = "";
                this.grosswterror.Visible = false;
                this.tarewt.Text = "";
                this.netwt.Text = "";
                this.wtpercop.Text = "";
                this.boxpalletstock.Text = "";
                this.copsstock.Text = "";
                if (_productionId == 0)
                {
                    MessageBox.Show("BCF Packing added successfully!",
                    "Success",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                    if (isPrint)
                    { }
                }
                else
                {
                    MessageBox.Show("BCF Packing updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    var dashboard = this.ParentForm as AdminAccount;
                    if (dashboard != null)
                    {
                        // Open the List form instead of Add form
                        dashboard.LoadFormInContent(new BCFPackingList());
                    }
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
                qualityerror.Text = "Please select quality";
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

            if (BoxItemList.SelectedIndex <= 0)
            {
                isValid = false;
            }

            if (CopsItemList.SelectedIndex <= 0)
            {
                isValid = false;
            }

            if (string.IsNullOrWhiteSpace(spoolno.Text) || Convert.ToInt32(spoolno.Text) == 0)
            {
                spoolnoerror.Text = "Please enter spool no";
                spoolnoerror.Visible = true;
                isValid = false;
            }

            if (string.IsNullOrWhiteSpace(spoolwt.Text) || Convert.ToDecimal(spoolwt.Text) == 0)
            {
                spoolwterror.Text = "Please enter spool wt";
                spoolwterror.Visible = true;
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

            if (flowLayoutPanel1.Controls.Count == 1)
            {
                MessageBox.Show("Please add atleast one record in Pallet details");
                isValid = false;
            }

            return isValid;
        } 

        private void btnCancel_Click(object sender, EventArgs e)
        {
            var dashboard = this.ParentForm as AdminAccount;
            if (dashboard != null)
            {
                dashboard.LoadFormInContent(new BCFPackingList());
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

        private void ResetForm(Control parent)
        {
            foreach (Control c in parent.Controls)
            {
                if (c is System.Windows.Forms.TextBox)
                    ((System.Windows.Forms.TextBox)c).Clear();

                else if (c is System.Windows.Forms.ComboBox)
                    ((System.Windows.Forms.ComboBox)c).SelectedIndex = 0;

                else if (c is DateTimePicker)
                    ((DateTimePicker)c).Value = DateTime.Now;

                else if (c is System.Windows.Forms.CheckBox)
                    ((System.Windows.Forms.CheckBox)c).Checked = false;

                else if (c is System.Windows.Forms.RadioButton)
                    ((System.Windows.Forms.RadioButton)c).Checked = false;

                // Recursive call if the control has children (like Panels, GroupBoxes, etc.)
                if (c.HasChildren)
                    ResetForm(c);
            }
            copyno.Text = "2";
            spoolno.Text = "0";
            this.spoolnoerror.Text = "";
            this.spoolnoerror.Visible = false;
            spoolwt.Text = "0";
            palletwtno.Text = "0";
            grosswtno.Text = "0";
            tarewt.Text = "0";
            netwt.Text = "0";
            wtpercop.Text = "0";
            boxpalletitemwt.Text = "0";
            boxpalletstock.Text = "0";
            copsitemwt.Text = "0";
            copsstock.Text = "0";
            frdenier.Text = "0";
            updenier.Text = "0";
            deniervalue.Text = "0";
        }
    }
}
