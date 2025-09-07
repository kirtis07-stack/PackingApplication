using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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

namespace PackingApplication
{
    public partial class POYPackingForm: Form
    {          
        private static Logger Log = Logger.GetLogger();

        MasterService _masterService = new MasterService();
        ProductionService _productionService = new ProductionService();
        PackingService _packingService = new PackingService();
        private long _productionId;
        public POYPackingForm(long productionId)
        {
            InitializeComponent();
            _productionId = productionId;
            this.Shown += POYPackingForm_Shown;
            this.AutoScroll = true;

            SetButtonBorderRadius(this.addqty, 8);
            SetButtonBorderRadius(this.submit, 8);

            LineNoList.SelectedIndexChanged += LineNoList_SelectedIndexChanged;
            MergeNoList.SelectedIndexChanged += MergeNoList_SelectedIndexChanged;
            PackSizeList.SelectedIndexChanged += PackSizeList_SelectedIndexChanged;
            QualityList.SelectedIndexChanged += QualityList_SelectedIndexChanged;
            WindingTypeList.SelectedIndexChanged += WindingTypeList_SelectedIndexChanged;
            //SaleOrderList.SelectedIndexChanged += SaleOrderList_SelectedIndexChanged;
            PrefixList.SelectedIndexChanged += PrefixList_SelectedIndexChanged;
            copyno.TextChanged += CopyNos_TextChanged;
            spoolno.TextChanged += SpoolNo_TextChanged;
            spoolwt.TextChanged += SpoolWeight_TextChanged;
            palletwtno.TextChanged += PalletWeight_TextChanged;
            grosswtno.TextChanged += GrossWeight_TextChanged;
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
            getSaleOrder.Insert(0, new LotSaleOrderDetailsResponse { LotSaleOrderDetailsId = 0, SaleOrderNumber = "Select Sale Order" });
            SaleOrderList.DataSource = getSaleOrder;
            SaleOrderList.DisplayMember = "SaleOrderNumber";
            SaleOrderList.ValueMember = "LotSaleOrderDetailsId";
            SaleOrderList.SelectedIndex = 0;

            copyno.Text = "1";
            //Username.Text = SessionManager.UserName;
            //role.Text = SessionManager.Role;

            isFormReady = true;
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

            var qualityList = await Task.Run(() => getQualityList());
            //quality
            qualityList.Insert(0, new QualityResponse { QualityId = 0, Name = "Select Quality" });
            QualityList.DataSource = qualityList;
            QualityList.DisplayMember = "Name";
            QualityList.ValueMember = "QualityId";
            QualityList.SelectedIndex = 0;

            var packsizeList = await Task.Run(() => getPackSizeList());
            //packsize
            PackSizeList.DataSource = packsizeList;
            PackSizeList.DisplayMember = "PackSizeName";
            PackSizeList.ValueMember = "PackSizeId";
            PackSizeList.SelectedIndex = 0;

            var windingtypeList = await Task.Run(() => getWindingTypeList());
            //windingtype
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
                this.copstxtbox.Text = "";
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
                        //flowLayoutPanel1.Controls.Clear();

                        //foreach (var pallet in productionResponse.PalletDetailsResponse)
                        //{
                        //    // find pallet info (from master list)
                        //    var palletItemList = Task.Run(() => getPalletItemList()).Result;
                        //    var palletInfo = palletItemList.FirstOrDefault(x => x.ItemId == pallet.PalletId);

                        //    // create label for quantity
                        //    System.Windows.Forms.Label qtyLabel = new System.Windows.Forms.Label
                        //    {
                        //        Text = pallet.Quantity.ToString(),
                        //        AutoSize = true,
                        //    };

                        //    // create panel to hold info
                        //    Panel pnl = new Panel
                        //    {
                        //        Width = 200,
                        //        Height = 50,
                        //        BorderStyle = BorderStyle.FixedSingle,
                        //        Tag = new Tuple<ItemResponse, System.Windows.Forms.Label>(palletInfo, qtyLabel)
                        //    };

                        //    // add child controls
                        //    System.Windows.Forms.Label nameLabel = new System.Windows.Forms.Label
                        //    {
                        //        Text = palletInfo != null ? palletInfo.Name : $"Pallet {pallet.PalletId}",
                        //        AutoSize = true,
                        //        Location = new Point(5, 5)
                        //    };

                        //    qtyLabel.Location = new Point(5, 25);

                        //    pnl.Controls.Add(nameLabel);
                        //    pnl.Controls.Add(qtyLabel);

                        //    // add panel to flowLayout
                        //    flowLayoutPanel1.Controls.Add(pnl);
                        //}
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
                // find pallet info from master pallet list
                var palletItemList = Task.Run(() => getPalletItemList()).Result;
                var selectedItem = palletItemList.FirstOrDefault(x => x.ItemId == palletDetail.PalletId);

                if (selectedItem == null)
                {
                    // fallback if master data not found
                    selectedItem = new ItemResponse { ItemId = palletDetail.PalletId, Name = $"Pallet {palletDetail.PalletId}" };
                }

                rowCount++;

                Panel rowPanel = new Panel();
                rowPanel.Size = new Size(flowLayoutPanel1.ClientSize.Width - 20, 35);
                rowPanel.BorderStyle = BorderStyle.FixedSingle;

                // SrNo
                System.Windows.Forms.Label lblSrNo = new System.Windows.Forms.Label() { Text = rowCount.ToString(), Width = 40, Location = new Point(10, 10) };

                // Item Name
                System.Windows.Forms.Label lblItem = new System.Windows.Forms.Label() { Text = selectedItem.Name, Width = 120, Location = new Point(60, 10), Tag = selectedItem.ItemId };

                // Qty
                System.Windows.Forms.Label lblQty = new System.Windows.Forms.Label() { Text = palletDetail.Quantity.ToString(), Width = 50, Location = new Point(190, 10) };

                // Edit Button
                System.Windows.Forms.Button btnEdit = new System.Windows.Forms.Button() { Text = "Edit", Size = new Size(50, 23), Location = new Point(250, 5), Tag = new Tuple<ItemResponse, int>(selectedItem, palletDetail.Quantity) };
                btnEdit.Click += editPallet_Click;

                // Delete Button
                System.Windows.Forms.Button btnDelete = new System.Windows.Forms.Button() { Text = "Delete", Size = new Size(60, 23), Location = new Point(310, 5), Tag = rowPanel };
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

                // Store pallet info in Tag (same as addqty_Click)
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

                var item = _productionService.getLotById(selectedLotId);
                itemname.Text = item.ItemName;
                shadename.Text = item.ShadeName;
                shadecd.Text = item.ShadeCode;
                deniervalue.Text = item.Denier.ToString();

                getSaleOrderList(productionRequest.LotId);
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

                //var getSaleOrderResponse = GetCallApi(saleURL + "SaleOrder/GetById?saleOrderId=" + productionRequest.SaleOrderId);
                //var getSaleOrder = JsonConvert.DeserializeObject<SaleOrderResponse>(getSaleOrderResponse);
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
                var WeighingScale = WeighingList.SelectedValue.ToString();
            }
        }

        private void CopsItemList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isFormReady) return; 

            if (CopsItemList.SelectedValue != null)
            {
                ItemResponse selectedCopsItem = (ItemResponse)CopsItemList.SelectedItem;
                int selectedItemId = selectedCopsItem.ItemId;

                productionRequest.SpoolItemId = selectedItemId;
            }
        }

        private void BoxItemList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isFormReady) return; 

            if (BoxItemList.SelectedValue != null)
            {
                ItemResponse selectedBoxItem = (ItemResponse)BoxItemList.SelectedItem;
                int selectedBoxItemId = selectedBoxItem.ItemId;

                productionRequest.BoxItemId = selectedBoxItemId;
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

        private List<QualityResponse> getQualityList()
        {
            var getQuality = _masterService.getQualityList();
            return getQuality;
        }

        private List<PackSizeResponse> getPackSizeList()
        {
            var getPackSize = _masterService.getPackSizeList();
            getPackSize.Insert(0, new PackSizeResponse { PackSizeId = 0, PackSizeName = "Select Pack Size" });
            return getPackSize;
        }

        private List<WindingTypeResponse> getWindingTypeList()
        {
            var getWindingType = _masterService.getWindingTypeList();
            getWindingType.Insert(0, new WindingTypeResponse { WindingTypeId = 0, WindingTypeName = "Select Winding Type" });
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

        private List<string> getWeighingList()
        {
            var getWeighingScale = new List<string>
            {
                "Select Weigh Scale",
                "Old",
                "Unique",
                "JISL (9600)",
                "JISL (2400)"
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

        private ProductionResponse getProductionById(long productionId)
        {
            var getProduction = _packingService.getProductionById(productionId);
            return getProduction;
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
                    rowPanel.Size = new Size(flowLayoutPanel1.ClientSize.Width - 20, 35);
                    rowPanel.BorderStyle = BorderStyle.FixedSingle;

                    // SrNo
                    System.Windows.Forms.Label lblSrNo = new System.Windows.Forms.Label() { Text = rowCount.ToString(), Width = 40, Location = new Point(10, 10) };

                    // Item Name
                    System.Windows.Forms.Label lblItem = new System.Windows.Forms.Label() { Text = selectedItem.Name, Width = 120, Location = new Point(60, 10), Tag = selectedItem.ItemId };

                    // Qty
                    System.Windows.Forms.Label lblQty = new System.Windows.Forms.Label() { Text = qty.ToString(), Width = 50, Location = new Point(190, 10) };

                    // Edit Button
                    System.Windows.Forms.Button btnEdit = new System.Windows.Forms.Button() { Text = "Edit", Size = new Size(50, 23), Location = new Point(250, 5), Tag = new Tuple<ItemResponse, int>(selectedItem, qty) };
                    btnEdit.Click += editPallet_Click;

                    // Delete Button
                    System.Windows.Forms.Button btnDelete = new System.Windows.Forms.Button() { Text = "Delete", Size = new Size(60, 23), Location = new Point(310, 5), Tag = rowPanel };

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
            headerPanel.BackColor = Color.LightGray;

            headerPanel.Controls.Add(new System.Windows.Forms.Label() { Text = "SrNo", Width = 40, Location = new Point(10, 10), Font = new Font("Segoe UI", 9, FontStyle.Bold) });
            headerPanel.Controls.Add(new System.Windows.Forms.Label() { Text = "Item Name", Width = 120, Location = new Point(60, 10), Font = new Font("Segoe UI", 9, FontStyle.Bold) });
            headerPanel.Controls.Add(new System.Windows.Forms.Label() { Text = "Qty", Width = 50, Location = new Point(190, 10), Font = new Font("Segoe UI", 9, FontStyle.Bold) });
            headerPanel.Controls.Add(new System.Windows.Forms.Label() { Text = "Action", Width = 120, Location = new Point(250, 10), Font = new Font("Segoe UI", 9, FontStyle.Bold) });

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
                spoolwterror.Text = "";
                spoolwterror.Visible = false;
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
                palletwterror.Text = "";
                palletwterror.Visible = false;
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

            tarewt.Text = (num1 + num2).ToString();
        }

        private void GrossWeight_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(grosswtno.Text))
            {
                grosswterror.Text = "";
                grosswterror.Visible = true;
            }
            else
            {
                CalculateNetWeight();
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
                spoolnoerror.Text = "";
                spoolnoerror.Visible = false;
            }
            else {
                CalculateWeightPerCop();
            }
        }

        private void CalculateWeightPerCop()
        {
            decimal num1 = 0, num2 = 0;

            decimal.TryParse(netwt.Text, out num1);
            decimal.TryParse(spoolno.Text, out num2);

            wtpercop.Text = (num1 / num2).ToString();
        }

        private async void submit_Click(object sender, EventArgs e)
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

                ProductionResponse result = SubmitPacking(productionRequest);
            }
        }

        public ProductionResponse SubmitPacking(ProductionRequest productionRequest)
        {
            ProductionResponse result = new ProductionResponse();
            result = _packingService.AddUpdatePOYPacking(_productionId, productionRequest);
            if (result != null)
            {
                if (result.ProductionId > 0)
                {
                    MessageBox.Show("Packing added successfully.");
                    //var dashboard = this.FindForm() as Dashboard;
                    //if (dashboard != null)
                    //{
                    //    dashboard.LoadFormInContent(new POYPackingList());
                    //}
                }
                else
                {
                    MessageBox.Show("Something went wrong.");
                }
            }

            return result;
        }

        //private void Logout_Click(object sender, EventArgs e)
        //{
        //    SessionManager.Clear();

        //    var loginForm = new Login();
        //    loginForm.Show();
        //    this.Close();
        //}

        private ProductionResponse getLastBoxDetails()
        {
            var getPacking = _packingService.getLastBoxDetails();
            return getPacking;
        }

        private void gradewiseprodn_Paint(object sender, PaintEventArgs e)
        {
            // Clear background
            e.Graphics.Clear(this.BackColor);

            // Get text size
            SizeF textSize = e.Graphics.MeasureString(gradewiseprodn.Text, gradewiseprodn.Font);

            // Define rectangle for border (skip space for text)
            Rectangle rect = new Rectangle(
                gradewiseprodn.ClientRectangle.X,
                gradewiseprodn.ClientRectangle.Y + (int)(textSize.Height / 2),
                gradewiseprodn.ClientRectangle.Width - 1,
                gradewiseprodn.ClientRectangle.Height - (int)(textSize.Height / 2) - 1
            );

            // Draw border
            using (Pen pen = new Pen(Color.Black, 2))  // custom border color
            {
                e.Graphics.DrawRectangle(pen, rect);
            }

            //// Draw text manually
            //using (Brush brush = new SolidBrush(Color.Maroon)) // custom text color
            //{
            //    e.Graphics.DrawString(gradewiseprodn.Text, gradewiseprodn.Font, brush, 10, 0);
            //}
        }

        private void qualityqty_Paint(object sender, PaintEventArgs e)
        {
            using (Pen pen = new Pen(Color.LightGray, 2))
            {
                Rectangle rect = qualityqty.ClientRectangle;
                rect.Width -= 1;
                rect.Height -= 1;
                e.Graphics.DrawRectangle(pen, rect);
            }
        }

        private void windinggrid_Paint(object sender, PaintEventArgs e)
        {
            using (Pen pen = new Pen(Color.LightGray, 2))
            {
                Rectangle rect = windinggrid.ClientRectangle;
                rect.Width -= 1;
                rect.Height -= 1;
                e.Graphics.DrawRectangle(pen, rect);
            }
        }

        private void backbutton_Click(object sender, EventArgs e)
        {
            AdminAccount parentForm = this.ParentForm as AdminAccount;

            if (parentForm != null)
            {
                parentForm.LoadFormInContent(new Dashboard());  
            }
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

            if (string.IsNullOrWhiteSpace(spoolno.Text) || Convert.ToInt32(spoolno.Text) == 0)
            {
                spoolnoerror.Text = "Please enter valid spool no";
                spoolnoerror.Visible = true;
                isValid = false;
            }

            if (string.IsNullOrWhiteSpace(spoolwt.Text) || Convert.ToDecimal(spoolwt.Text) == 0)
            {
                spoolwterror.Text = "Please enter valid spool weight";
                spoolwterror.Visible = true;
                isValid = false;
            }

            if (string.IsNullOrWhiteSpace(palletwtno.Text) || Convert.ToDecimal(palletwtno.Text) == 0)
            {
                palletwterror.Text = "Please enter valid empty box/pallet weight";
                palletwterror.Visible = true;
                isValid = false;
            }

            if (string.IsNullOrWhiteSpace(grosswtno.Text) || Convert.ToDecimal(grosswtno.Text) == 0)
            {
                grosswterror.Text = "Please enter valid gross weight";
                grosswterror.Visible = true;
                isValid = false;
            }

            if (flowLayoutPanel1.Controls.Count == 1) {
                MessageBox.Show("Please add atleast one record in Pallet details");
                isValid = false;
            }

            return isValid;
        }

        private void SetButtonBorderRadius(System.Windows.Forms.Button button, int radius)
        {
            Log.writeMessage("SetButtonBorderRadius start");
            try
            {
                button.FlatStyle = FlatStyle.Flat;
                button.FlatAppearance.BorderSize = 0;
                button.FlatAppearance.BorderColor = Color.FromArgb(0, 92, 232); // Set to the background color of your form or panel
                button.FlatAppearance.MouseOverBackColor = button.BackColor; // To prevent color change on mouseover
                button.BackColor = Color.FromArgb(0, 92, 232);

                // Set the border radius
                System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
                int diameter = radius * 2;
                path.AddArc(0, 0, diameter, diameter, 180, 95); // Top-left corner
                path.AddArc(button.Width - diameter, 0, diameter, diameter, 270, 95); // Top-right corner
                path.AddArc(button.Width - diameter, button.Height - diameter, diameter, diameter, 0, 95); // Bottom-right corner
                path.AddArc(0, button.Height - diameter, diameter, diameter, 90, 95); // Bottom-left corner
                path.CloseFigure();

                button.Region = new Region(path);
            }
            catch (Exception ex)
            {
                //MessageBox.Show($"An error occurred: {ex.Message}");
                Log.writeMessage($"An error occurred: {ex.Message}");
            }
            Log.writeMessage("SetButtonBorderRadius end");
        }

        private void CopyNos_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(copyno.Text))
            {
                copynoerror.Text = "";
                copynoerror.Visible = false;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            var dashboard = this.FindForm() as Dashboard;
            if (dashboard != null)
            {
                dashboard.LoadFormInContent(new POYPackingList());
            }
        }
    }
}
