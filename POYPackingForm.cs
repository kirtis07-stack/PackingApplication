using Microsoft.Reporting.WinForms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PackingApplication.Helper;
using PackingApplication.Models.CommonEntities;
using PackingApplication.Models.RequestEntities;
using PackingApplication.Models.ResponseEntities;
using PackingApplication.Services;
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
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Net.WebRequestMethods;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolBar;
using File = System.IO.File;
using PdfiumViewer;
using System.Drawing.Printing;

namespace PackingApplication
{
    public partial class POYPackingForm: Form
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
        public POYPackingForm(long productionId)
        {
            InitializeComponent();
            ApplyFonts();
            _productionId = productionId;
            this.Shown += POYPackingForm_Shown;
            this.AutoScroll = true;
            this.sidebarContainer.BringToFront();

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
            sidebarContainer.Width = sidebarContainer.MinimumSize.Width;
            panel10.Width = panel10.MinimumSize.Width;
            panel12.Width = panel12.MinimumSize.Width;
            leftpanel.Width = leftpanel.MinimumSize.Width;
        }

        private void POYPackingForm_Load(object sender, EventArgs e)
        {
            AddHeader();
            
            var getItem = new List<LotsResponse>();
            getItem.Insert(0, new LotsResponse { LotId = 0, LotNo = "Select MergeNo" });
            MergeNoList.DataSource = getItem;
            MergeNoList.DisplayMember = "LotNo";
            MergeNoList.ValueMember = "LotId";
            MergeNoList.SelectedIndex = 0;

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
            //this.reportViewer1.RefreshReport();
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
            this.textBox2.Font = FontManager.GetFont(8F, FontStyle.Regular);
            this.boxtype.Font = FontManager.GetFont(8F, FontStyle.Bold);
            this.boxweight.Font = FontManager.GetFont(8F, FontStyle.Bold);
            this.boxpalletitemwt.Font = FontManager.GetFont(8F, FontStyle.Regular);         
            this.boxstock.Font = FontManager.GetFont(8F, FontStyle.Bold);
            this.textBox4.Font = FontManager.GetFont(8F, FontStyle.Regular);           
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
            this.reviewlbl.Font = FontManager.GetFont(9F, FontStyle.Bold);
            this.reviewsubtitle.Font = FontManager.GetFont(8F, FontStyle.Regular);
            this.weighlbl.Font = FontManager.GetFont(9F, FontStyle.Bold);
            this.weighsubtitle.Font = FontManager.GetFont(8F, FontStyle.Regular);
            this.packaginglbl.Font = FontManager.GetFont(9F, FontStyle.Bold);
            this.packagingsubtitle.Font = FontManager.GetFont(8F, FontStyle.Regular);
            this.orderlbl.Font = FontManager.GetFont(9F, FontStyle.Bold);
            this.orderdetailssubtitle.Font = FontManager.GetFont(8F, FontStyle.Regular);
            this.grdsoqty.Font = FontManager.GetFont(8F, FontStyle.Regular);
            this.prodnbalqty.Font = FontManager.GetFont(8F, FontStyle.Regular);
            this.rowMaterial.Font = FontManager.GetFont(8F, FontStyle.Regular);
            this.rowMaterialBox.Font = FontManager.GetFont(8F, FontStyle.Bold);
            this.Font = FontManager.GetFont(9F, FontStyle.Bold);
        }

        private async void POYPackingForm_Shown(object sender, EventArgs e)
        {
            var machineList = await Task.Run(() => getMachineList());
            //machine
            machineList.Insert(0, new MachineResponse { MachineId = 0, MachineName = "Select Line No." });
            LineNoList.DataSource = machineList;
            LineNoList.DisplayMember = "MachineName";
            LineNoList.ValueMember = "MachineId";
            LineNoList.SelectedIndex = 0;

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
            copsitemList.Insert(0, new ItemResponse { ItemId = 0, Name = "Select Cops Item" });
            CopsItemList.DataSource = copsitemList;
            CopsItemList.DisplayMember = "Name";
            CopsItemList.ValueMember = "ItemId";
            CopsItemList.SelectedIndex = 0;

            var boxitemList = await Task.Run(() => getBoxItemList());
            //boxitem
            boxitemList.Insert(0, new ItemResponse { ItemId = 0, Name = "Select Box/Pallet" });
            BoxItemList.DataSource = boxitemList;
            BoxItemList.DisplayMember = "Name";
            BoxItemList.ValueMember = "ItemId";
            BoxItemList.SelectedIndex = 0;

            var palletitemList = await Task.Run(() => getPalletItemList());
            //palletitem
            palletitemList.Insert(0, new ItemResponse { ItemId = 0, Name = "Select Box/Pallet" });
            PalletTypeList.DataSource = palletitemList;
            PalletTypeList.DisplayMember = "Name";
            PalletTypeList.ValueMember = "ItemId";
            PalletTypeList.SelectedIndex = 0;

            var getLastBox = await Task.Run(() => getLastBoxDetails());

            //lastboxdetails
            if(getLastBox.ProductionId > 0)
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
                    prhindi.Checked = productionResponse.PrintHindiWords;
                    prwtps.Checked = productionResponse.PrintWTPS;
                    prqrcode.Checked = productionResponse.PrintQRCode;
                    spoolno.Text = productionResponse.Spools.ToString();
                    spoolwt.Text = productionResponse.SpoolsWt.ToString();
                    palletwtno.Text = productionResponse.EmptyBoxPalletWt.ToString();
                    grosswtno.Text = productionResponse.GrossWt.ToString();
                    tarewt.Text = productionResponse.TareWt.ToString();
                    netwt.Text = productionResponse.NetWt.ToString();

                    if(productionResponse.PalletDetailsResponse.Count > 0 )
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
                System.Windows.Forms.Button btnEdit = new System.Windows.Forms.Button() { Text = "Edit", Size = new Size(35, 23), Location = new Point(250, 5), Font = FontManager.GetFont(7F, FontStyle.Regular), BackColor = Color.FromArgb(230, 240, 255), ForeColor = Color.FromArgb(51, 133, 255), Tag = new Tuple<ItemResponse, int>(selectedItem, palletDetail.Quantity), FlatStyle = FlatStyle.Flat };
                btnEdit.FlatAppearance.BorderColor = Color.FromArgb(51, 133, 255);
                btnEdit.FlatAppearance.BorderSize = 1;
                btnEdit.FlatAppearance.MouseOverBackColor = Color.FromArgb(210, 230, 255);
                btnEdit.FlatAppearance.MouseDownBackColor = Color.FromArgb(180, 210, 255);
                btnEdit.FlatAppearance.BorderSize = 0;
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

                productionRequest.MachineId = selectedMachineId;
                // Call API to get department info by MachineId
                var department = _masterService.getMachineById(selectedMachineId);
                departmentname.Text = department.DepartmentName;
                productionRequest.DepartmentId = department.DepartmentId;

                getLotList(selectedMachineId);
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

                lotResponse = _productionService.getLotById(selectedLotId);
                itemname.Text = lotResponse.ItemName;
                shadename.Text = lotResponse.ShadeName;
                shadecd.Text = lotResponse.ShadeCode;
                deniervalue.Text = lotResponse.Denier.ToString();
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

                var packsize = _masterService.getPackSizeById(selectedPacksizeId);
                frdenier.Text = packsize.FromDenier.ToString();
                updenier.Text = packsize.UpToDenier.ToString();
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

                productionRequest.SaleOrderId = selectedSaleOrderId;
                if(selectedSaleOrderId > 0)
                {
                    var saleOrderItemResponse = _saleService.getSaleOrderItemByItemIdAndShadeIdAndSaleOrderId(lotResponse.ItemId, lotResponse.ShadeId, selectedSaleOrderId);
                    if (saleOrderItemResponse != null)
                    {
                        productionRequest.SaleOrderItemId = saleOrderItemResponse.SaleOrderItemsId;
                        productionRequest.ContainerTypeId = saleOrderItemResponse.ContainerTypeId;
                    }

                    int selectedQualityId = Convert.ToInt32(QualityList.SelectedValue.ToString());
                    var getProductionByQuality = getProductionByQualityIdAndSaleOrderId(selectedQualityId, selectedSaleOrderId);
                    qualityqty.Columns.Clear();
                    qualityqty.Columns.Add(new DataGridViewTextBoxColumn { Name = "Quality", DataPropertyName = "QualityName", HeaderText = "Quality" });
                    qualityqty.Columns.Add(new DataGridViewTextBoxColumn { Name = "ProductionQty", DataPropertyName = "GrossWt", HeaderText = "Production Qty" });
                    qualityqty.DataSource = getProductionByQuality;

                    decimal totalSOQty = 0;
                    decimal totalProdQty = 0;
                    var saleResponse = getSaleOrderById(selectedSaleOrderId);
                    
                    foreach (var soitem in saleResponse.saleOrderItemsResponses)
                    {
                        totalSOQty += soitem.Quantity;
                    }
                    grdsoqty.Text = totalSOQty.ToString();

                    foreach (var proditem in getProductionByQuality)
                    {
                        totalProdQty += proditem.GrossWt;
                    }
                    prodnbalqty.Text = (totalSOQty - totalProdQty).ToString();

                    int selectedWindingTypeId = Convert.ToInt32(WindingTypeList.SelectedValue.ToString());
                    var getProductionByWindingType = getProductionByWindingTypeAndSaleOrderId(selectedWindingTypeId, selectedSaleOrderId);
                    List<WindingTypeGridResponse> gridList = new List<WindingTypeGridResponse>();
                    foreach (var winding in getProductionByWindingType)
                    {
                        WindingTypeGridResponse grid = new WindingTypeGridResponse();
                        grid.WindingTypeName = winding.WindingTypeName;
                        grid.SaleOrderQty = totalSOQty;
                        grid.GrossWt = winding.GrossWt;
                        gridList.Add(grid);
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

        private void ComPortList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isFormReady) return; 

            if (ComPortList.SelectedValue != null)
            {
                var ComPort = ComPortList.SelectedValue.ToString();
                comPort = ComPortList.SelectedValue.ToString();
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

                if (selectedItemId > 0) {
                    productionRequest.SpoolItemId = selectedItemId;

                    var itemResponse = _masterService.getItemById(selectedItemId);
                    if (itemResponse != null)
                    {
                        copsitemwt.Text = itemResponse.Weight.ToString();
                    }
                }
            }
        }

        private void BoxItemList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isFormReady) return; 

            if (BoxItemList.SelectedValue != null)
            {
                ItemResponse selectedBoxItem = (ItemResponse)BoxItemList.SelectedItem;
                int selectedBoxItemId = selectedBoxItem.ItemId;

                if (selectedBoxItemId > 0) {
                    productionRequest.BoxItemId = selectedBoxItemId;
                    var itemResponse = _masterService.getItemById(selectedBoxItemId);
                    if (itemResponse != null)
                    {
                        boxpalletitemwt.Text = itemResponse.Weight.ToString();
                        palletwtno.Text = itemResponse.Weight.ToString();
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
                "COM 1",
                "COM 2",
                "COM 3"
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

        private List<ItemResponse> getBoxItemList()
        {
            var getBox = _masterService.getBoxItemList();
            return getBox;
        }

        private List<ItemResponse> getPalletItemList()
        {
            var getBox = _masterService.getBoxItemList();
            return getBox;
        }

        private List<PrefixResponse> getPrefixList()
        {
            var getPrefix = _masterService.getPrefixList();
            return getPrefix;
        }

        private SaleOrderResponse getSaleOrderById(int saleOrderId)
        {
            var getSaleOrder = _saleService.getSaleOrderById(saleOrderId);
            return getSaleOrder;
        }

        private ProductionResponse getProductionById(long productionId)
        {
            var getProduction = _packingService.getProductionById(productionId);
            return getProduction;
        }

        private List<ProductionResponse> getProductionByQualityIdAndSaleOrderId(int qualityId, int saleOrderId)
        {
            var getProduction = _packingService.getAllProductionByQualityandSaleOrder(qualityId, saleOrderId);
            return getProduction;
        }

        private List<ProductionResponse> getProductionByWindingTypeAndSaleOrderId(int windingTypeId, int saleOrderId)
        {
            var getProduction = _packingService.getAllProductionByWindingTypeandSaleOrder(windingTypeId, saleOrderId);
            return getProduction;
        }

        private ProductionResponse getLastBoxDetails()
        {
            var getPacking = _packingService.getLastBoxDetails("poypacking");
            return getPacking;
        }

        private int rowCount = 0; // Keeps track of SrNo
        private bool headerAdded = false; // To ensure header is added only once
        private int currentY = 35; // Start below header height
        private void addqty_Click(object sender, EventArgs e)
        {
            var selectedItem = (ItemResponse)PalletTypeList.SelectedItem;
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
                    System.Windows.Forms.Button btnEdit = new System.Windows.Forms.Button() { Text = "Edit", Size = new Size(35, 23), Location = new Point(250, 5), Font = FontManager.GetFont(7F, FontStyle.Regular), BackColor = Color.FromArgb(230, 240, 255), ForeColor = Color.FromArgb(51, 133, 255), Tag = new Tuple<ItemResponse, int>(selectedItem, qty), FlatStyle = FlatStyle.Flat };
                    btnEdit.FlatAppearance.BorderColor = Color.FromArgb(51, 133, 255);
                    btnEdit.FlatAppearance.BorderSize = 1;  
                    btnEdit.FlatAppearance.MouseOverBackColor = Color.FromArgb(210, 230, 255); 
                    btnEdit.FlatAppearance.MouseDownBackColor = Color.FromArgb(180, 210, 255);
                    btnEdit.FlatAppearance.BorderSize = 0;
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
                    flowLayoutPanel1.AutoScroll = true;
                    flowLayoutPanel1.WrapContents = false;
                    flowLayoutPanel1.FlowDirection = FlowDirection.TopDown;
                    addqty.Text = "Add";

                    getPalletItemList();
                    qnty.Text = "";

                }
                else
                {
                    MessageBox.Show("Item already added.");
                }
            }
            else
            {
                MessageBox.Show("Please select an item.");
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
            var data = btn.Tag as Tuple<ItemResponse, int>;

            if (data != null)
            {
                ItemResponse item = data.Item1;
                int quantity = data.Item2;

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
            }
        }

        private void SpoolWeight_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(spoolwt.Text))
            {
                spoolwterror.Visible = true;
            }
            else
            {
                CalculateTareWeight();
                spoolwterror.Text = "";
                spoolwterror.Visible = false;
            }
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
            decimal num1 = 0, num2 = 0;

            decimal.TryParse(spoolwt.Text, out num1);
            decimal.TryParse(palletwtno.Text, out num2);

            tarewt.Text = (num1 + num2).ToString();
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
            decimal num1 = 0, num2 = 0;

            decimal.TryParse(grosswtno.Text, out num1);
            decimal.TryParse(tarewt.Text, out num2);

            netwt.Text = (num1 - num2).ToString();
        }

        private void NetWeight_TextChanged(object sender, EventArgs e)
        {
            CalculateWeightPerCop();
        }

        private void SpoolNo_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(spoolno.Text))
            {
                spoolnoerror.Visible = true;
            }
            else {
                spoolwt.Text = (Convert.ToInt32(spoolno.Text.ToString()) * Convert.ToDecimal(copsitemwt.Text.ToString())).ToString();
                CalculateWeightPerCop();
                spoolnoerror.Text = "";
                spoolnoerror.Visible = false;
            }
        }

        private void CalculateWeightPerCop()
        {
            decimal num1 = 0, num2 = 0;

            decimal.TryParse(netwt.Text, out num1);
            decimal.TryParse(spoolno.Text, out num2);

            wtpercop.Text = (num1 / num2).ToString("F3");
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
                    //consumptionDetailsRequest.StockTrfDetailsId = 0;
                    productionRequest.ConsumptionDetailsRequest.Add(consumptionDetailsRequest);
                }

                ProductionResponse result = SubmitPacking(productionRequest, isPrint);
            }
        }

        public ProductionResponse SubmitPacking(ProductionRequest productionRequest, bool isPrint)
        {
            ProductionResponse result = new ProductionResponse();
            result = _packingService.AddUpdatePOYPacking(_productionId, productionRequest);
            if (result != null)
            {
                if (_productionId == 0)
                {
                    MessageBox.Show("POY Packing added successfully.");
                    if (isPrint)
                    {
                        //call ssrs report to print
                        string reportServer = "http://desktop-ocu1bqt/ReportServer";
                        string reportPath = "/PackingSSRSReport/TextureAndPOY";
                        string format = "PDF";

                        //set params
                        string productionId = result.ProductionId.ToString();
                        string startDate = "2025-09-01";
                        string endDate = "2025-09-30";
                        string url = $"{reportServer}?{reportPath}&rs:Format={format}" + $"&ProductionId={productionId}&StartDate={startDate}&EndDate={endDate}";

                        WebClient client = new WebClient();
                        client.Credentials = CredentialCache.DefaultNetworkCredentials;

                        byte[] bytes = client.DownloadData(url);

                        // Save to file
                        string tempFile = Path.Combine(Path.GetTempPath(), "Report.pdf");
                        File.WriteAllBytes(tempFile, bytes);

                        //// Open with default PDF reader
                        //System.Diagnostics.Process.Start("Report.pdf");

                        using (var pdfDoc = PdfDocument.Load(tempFile))
                        {
                            using (var printDoc = pdfDoc.CreatePrintDocument())
                            {
                                printDoc.PrinterSettings = new PrinterSettings()
                                {
                                    // PrinterName = "YourPrinterName", // optional, default printer if omitted
                                    Copies = 1
                                };
                                printDoc.Print(); // sends PDF to printer
                            }
                        }

                        // 5️⃣ Clean up temp file
                        File.Delete(tempFile);
                    }
                }
                else
                {
                    MessageBox.Show("POY Packing updated successfully.");
                    if (isPrint)
                    {
                        string reportServer = "http://desktop-ocu1bqt/ReportServer";
                        string reportPath = "/PackingSSRSReport/TextureAndPOY";
                        string format = "PDF";

                        //set params
                        string productionId = result.ProductionId.ToString();
                        string startDate = "2025-09-01";
                        string endDate = "2025-09-30";
                        string url = $"{reportServer}?{reportPath}&rs:Format={format}" + $"&ProductionId={productionId}&StartDate={startDate}&EndDate={endDate}";

                        WebClient client = new WebClient();
                        client.Credentials = CredentialCache.DefaultNetworkCredentials;

                        byte[] bytes = client.DownloadData(url);

                        // Save to file
                        string tempFile = Path.Combine(Path.GetTempPath(), "Report.pdf");
                        File.WriteAllBytes(tempFile, bytes);

                        //// Open with default PDF reader
                        //System.Diagnostics.Process.Start("Report.pdf");

                        using (var pdfDoc = PdfDocument.Load(tempFile))
                        {
                            using (var printDoc = pdfDoc.CreatePrintDocument())
                            {
                                printDoc.PrinterSettings = new PrinterSettings()
                                {
                                    // PrinterName = "YourPrinterName", // optional, default printer if omitted
                                    Copies = 1
                                };
                                printDoc.Print(); // sends PDF to printer
                            }
                        }

                        // 5️⃣ Clean up temp file
                        File.Delete(tempFile);

                    }
                }
            }
            else
            {
                MessageBox.Show("Something went wrong.");
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

            if (flowLayoutPanel1.Controls.Count == 1) {
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
                dashboard.LoadFormInContent(new POYPackingList());
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

        private async void sidebarTimer_Tick(object sender, EventArgs e)
        {
            showSidebarBorder = false;

            if (sidebarExpand)
            {
                this.sidebarContainer.Width -= 10;
                if (sidebarContainer.Width == sidebarContainer.MinimumSize.Width)
                {
                    panel12.Width = panel12.MinimumSize.Width;
                    panel10.Width = panel10.MinimumSize.Width;

                    if(panel10.Width == panel10.MinimumSize.Width)
                    {
                        sidebarExpand = false;
                        leftpanel.Width = leftpanel.MinimumSize.Width;
                    }
                    sidebarTimer.Stop();
                    sidebarContainer.Invalidate();
                }
            }
            else
            {
                this.sidebarContainer.Width += 10;
                if (sidebarContainer.Width == sidebarContainer.MaximumSize.Width)
                {
                    panel12.Width = panel12.MaximumSize.Width;
                    panel10.Width = panel10.MaximumSize.Width;

                    if (panel10.Width == panel10.MaximumSize.Width)
                    {
                        sidebarExpand = true;
                        leftpanel.Width = leftpanel.MaximumSize.Width;
                    }
                    sidebarTimer.Stop();
                    sidebarContainer.Invalidate();
                }
            }

            // Show border after all animations
            showSidebarBorder = true;
            sidebarContainer.Invalidate();
        }

        private void menuBtn_Click(object sender, EventArgs e)
        {
            sidebarTimer.Start();
        }

        private void sidebarContainer_Paint(object sender, PaintEventArgs e)
        {
            if (showSidebarBorder)   // only draw when allowed
            {
                _cmethod.DrawRightBorder(sidebarContainer, e, Color.FromArgb(191, 191, 191), 1);
            }
        }
    }
}
