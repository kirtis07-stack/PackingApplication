using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PackingApplication.Helper;
using System.Net;
using System.Security.Policy;
using Newtonsoft.Json;
using static System.Net.WebRequestMethods;
using Newtonsoft.Json.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using PackingApplication.Models.ResponseEntities;
using PackingApplication.Models.RequestEntities;
using PackingApplication.Models.CommonEntities;
using System.Configuration;
using System.Security.Cryptography;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace PackingApplication
{
    public partial class POYPackingForm: Form
    {          
        string masterURL = ConfigurationManager.AppSettings["masterURL"];
        string saleURL = ConfigurationManager.AppSettings["saleURL"];
        string productionURL = ConfigurationManager.AppSettings["productionURL"];
        string packingURL = ConfigurationManager.AppSettings["packingURL"];
        //string packingURL = "https://localhost:7027/api/";

        private static Logger Log = Logger.GetLogger();
        public POYPackingForm()
        {
            InitializeComponent();
            this.AutoScroll = true;
        }

        public string GetCallApi(string WebApiurl)
        {
            var request = (HttpWebRequest)WebRequest.Create(WebApiurl);

            request.Method = "GET";
            request.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            request.Headers.Add("Authorization", "Bearer " + SessionManager.AuthToken);
            var content = string.Empty;

            using (var response = (HttpWebResponse)request.GetResponse())
            {
                using (var stream = response.GetResponseStream())
                {
                    using (var sr = new StreamReader(stream))
                    {
                        content = sr.ReadToEnd();
                    }
                }
            }

            return content;
        }
        public async Task<string> PostCallApi(string path, object data)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(path);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", SessionManager.AuthToken);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            using (var content = new StringContent(JsonConvert.SerializeObject(data), System.Text.Encoding.UTF8, "application/json"))
            {
                var response = client.PostAsync(path, content).Result;
                //use await it has moved in some context on .core 6.0
                if (response.IsSuccessStatusCode == true)
                {
                    return response.Content.ReadAsStringAsync().Result;
                }
                else
                {
                    return response.Content.ReadAsStringAsync().Result;
                }
            }
            
        }
        private void POYPackingForm_Load(object sender, EventArgs e)
        {
            AddHeader();
            getMachineList();
            getQualityList();
            getPackSizeList();
            getWindingTypeList();
            getComPortList();
            getWeighingList();
            getCopeItemList();
            getPalletItemList();
            getBoxItemList();
            getAllPOYPackingList();
            getPrefixList();

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

            Username.Text = SessionManager.UserName;
            role.Text = SessionManager.Role;

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
            if (LineNoList.SelectedValue != null)
            {
                MachineResponse selectedMachine = (MachineResponse)LineNoList.SelectedItem;
                int selectedMachineId = selectedMachine.MachineId;

                productionRequest.MachineId = selectedMachineId;
                // Call API to get department info by MachineId
                string deptJson = GetCallApi(masterURL + "Machine/GetById?machineId=" + selectedMachineId);

                var department = JsonConvert.DeserializeObject<MachineResponse>(deptJson);
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
            if (MergeNoList.SelectedValue != null)
            {
                LotsResponse selectedLot = (LotsResponse)MergeNoList.SelectedItem;
                int selectedLotId = selectedLot.LotId;

                productionRequest.LotId = selectedLot.LotId;

                string lotResponse = GetCallApi(productionURL + "Lots/GetById?LotsId=" + selectedLotId);

                var item = JsonConvert.DeserializeObject<LotsResponse>(lotResponse);
                itemname.Text = item.ItemName;
                shadename.Text = item.ShadeName;
                shadecd.Text = item.ShadeCode;

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
            if (PackSizeList.SelectedValue != null)
            {
                PackSizeResponse selectedPacksize = (PackSizeResponse)PackSizeList.SelectedItem;
                int selectedPacksizeId = selectedPacksize.PackSizeId;

                productionRequest.PackSizeId = selectedPacksizeId;

                string packsizeResponse = GetCallApi(masterURL + "PackSize/GetById?PackSizeId=" + selectedPacksizeId);

                var packsize = JsonConvert.DeserializeObject<PackSizeResponse>(packsizeResponse);
                frdenier.Text = packsize.FromDenier.ToString();
                updenier.Text = packsize.UpToDenier.ToString();
            }
        }

        private void QualityList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isFormReady) return;

            if (QualityList.SelectedValue != null)
            {
                QualityResponse selectedQuality = (QualityResponse)QualityList.SelectedItem;
                int selectedQualityId = selectedQuality.QualityId;

                productionRequest.QualityId = selectedQualityId;
            }
        }

        private void WindingTypeList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isFormReady) return; 

            if (WindingTypeList.SelectedValue != null)
            {
                WindingTypeResponse selectedWindingType = (WindingTypeResponse)WindingTypeList.SelectedItem;
                int selectedWindingTypeId = selectedWindingType.WindingTypeId;

                productionRequest.WindingTypeId = selectedWindingTypeId;
            }
        }

        private void SaleOrderList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isFormReady) return; 

            if (SaleOrderList.SelectedValue != null)
            {
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

            if (PrefixList.SelectedValue != null)
            {
                PrefixResponse selectedPrefix = (PrefixResponse)PrefixList.SelectedItem;
                int selectedPrefixId = selectedPrefix.PrefixCode;

                productionRequest.PrefixCode = selectedPrefixId;

                prodtype.Text = selectedPrefix.ProductionType.ToString();
                productionRequest.ProdTypeId = selectedPrefix.ProductionTypeId;
            }
        }

        private void getMachineList()
        {
            var getMachineResponse = GetCallApi(masterURL + "Machine/GetAll?IsDropDown=" + false);
            var getMachine = JsonConvert.DeserializeObject<List<MachineResponse>>(getMachineResponse);
            getMachine.Insert(0, new MachineResponse { MachineId = 0, MachineName = "Select Line No." });
            LineNoList.DataSource = getMachine;
            LineNoList.DisplayMember = "MachineName";
            LineNoList.ValueMember = "MachineId";
            LineNoList.SelectedIndex = 0;
        }

        private void getLotList(int machineId) 
        {
            var getLotsResponse = GetCallApi(productionURL + "Lots/GetAllByMachineId?machineId=" + machineId);
            var getItem = JsonConvert.DeserializeObject<List<LotsResponse>>(getLotsResponse);
            getItem.Insert(0, new LotsResponse { LotId = 0, LotNo = "Select MergeNo" });
            MergeNoList.DataSource = getItem;
            MergeNoList.DisplayMember = "LotNo";
            MergeNoList.ValueMember = "LotId";
            MergeNoList.SelectedIndex = 0;
        }

        private void getQualityList()
        {
            var getQualityResponse = GetCallApi(masterURL + "Quality/GetAll?IsDropDown=" + false);
            var getQuality = JsonConvert.DeserializeObject<List<QualityResponse>>(getQualityResponse);
            getQuality.Insert(0, new QualityResponse { QualityId = 0, Name = "Select Quality" });
            QualityList.DataSource = getQuality;
            QualityList.DisplayMember = "Name";
            QualityList.ValueMember = "QualityId";
            QualityList.SelectedIndex = 0;
        }

        private void getPackSizeList()
        {
            var getPackSizeResponse = GetCallApi(masterURL + "PackSize/GetAll?IsDropDown=" + false);
            var getPackSize = JsonConvert.DeserializeObject<List<PackSizeResponse>>(getPackSizeResponse);
            getPackSize.Insert(0, new PackSizeResponse { PackSizeId = 0, PackSizeName = "Select Pack Size" });
            PackSizeList.DataSource = getPackSize;
            PackSizeList.DisplayMember = "PackSizeName";
            PackSizeList.ValueMember = "PackSizeId";
            PackSizeList.SelectedIndex = 0;
        }

        private void getWindingTypeList()
        {
            var getWindingTypeResponse = GetCallApi(masterURL + "WindingType/GetAll?IsDropDown=" + false);
            var getWindingType = JsonConvert.DeserializeObject<List<WindingTypeResponse>>(getWindingTypeResponse);
            getWindingType.Insert(0, new WindingTypeResponse { WindingTypeId = 0, WindingTypeName = "Select Winding Type" });
            WindingTypeList.DataSource = getWindingType;
            WindingTypeList.DisplayMember = "WindingTypeName";
            WindingTypeList.ValueMember = "WindingTypeId";
            WindingTypeList.SelectedIndex = 0;
        }

        private void getSaleOrderList(int lotId)
        {
            var getSaleOrderResponse = GetCallApi(productionURL + "LotSaleOrderDetails/GetAllByLotsId?lotsId=" + lotId);
            var getSaleOrder = JsonConvert.DeserializeObject<List<LotSaleOrderDetailsResponse>>(getSaleOrderResponse);
            getSaleOrder.Insert(0, new LotSaleOrderDetailsResponse { LotSaleOrderDetailsId = 0, SaleOrderNumber = "Select Sale Order" });
            SaleOrderList.DataSource = getSaleOrder;
            SaleOrderList.DisplayMember = "SaleOrderNumber";
            SaleOrderList.ValueMember = "LotSaleOrderDetailsId";
            SaleOrderList.SelectedIndex = 0;
        }

        private void getComPortList()
        {
            var getComPortType = new List<string>
            {
                "Select Com Port",
                "COM 1",
                "COM 2",
                "COM 3"
            };

            ComPortList.DataSource = getComPortType;
            ComPortList.SelectedIndex = 0;
        }

        private void getWeighingList()
        {
            var getWeighingScale = new List<string>
            {
                "Select Weigh Scale",
                "Old",
                "Unique",
                "JISL (9600)",
                "JISL (2400)"
            };

            WeighingList.DataSource = getWeighingScale;
            WeighingList.SelectedIndex = 0;
        }

        private void getCopeItemList()
        {
            var getCopeItemResponse = GetCallApi(masterURL + "Items/GetAll?IsDropDown=" + false);
            var getCopeItem = JsonConvert.DeserializeObject<List<ItemResponse>>(getCopeItemResponse);
            getCopeItem.Insert(0, new ItemResponse { ItemId = 0, Name = "Select Cops Item" });
            CopsItemList.DataSource = getCopeItem;
            CopsItemList.DisplayMember = "Name";
            CopsItemList.ValueMember = "ItemId";
            CopsItemList.SelectedIndex = 0;
        }

        private void getBoxItemList()
        {
            var getBoxItemResponse = GetCallApi(masterURL + "Items/GetAll?IsDropDown=" + false);
            var getBox = JsonConvert.DeserializeObject<List<ItemResponse>>(getBoxItemResponse);
            getBox.Insert(0, new ItemResponse { ItemId = 0, Name = "Select Box/Pallet" });
            BoxItemList.DataSource = getBox;
            BoxItemList.DisplayMember = "Name";
            BoxItemList.ValueMember = "ItemId";
            BoxItemList.SelectedIndex = 0;
        }

        private void getPalletItemList()
        {
            var getBoxItemResponse = GetCallApi(masterURL + "Items/GetAll?IsDropDown=" + false);
            var getBox = JsonConvert.DeserializeObject<List<ItemResponse>>(getBoxItemResponse);
            getBox.Insert(0, new ItemResponse { ItemId = 0, Name = "Select Box/Pallet" });
            PalletTypeList.DataSource = getBox;
            PalletTypeList.DisplayMember = "Name";
            PalletTypeList.ValueMember = "ItemId";
            PalletTypeList.SelectedIndex = 0;
        }

        private void getPrefixList()
        {
            var getPrefixResponse = GetCallApi(masterURL + "Prefix/GetByTransactionTypeId?transactionTypeId=" + 7);
            var getPrefix = JsonConvert.DeserializeObject<List<PrefixResponse>>(getPrefixResponse);
            getPrefix.Insert(0, new PrefixResponse { PrefixCode = 0, Prefix = "Select Prefix" });
            PrefixList.DataSource = getPrefix;
            PrefixList.DisplayMember = "Prefix";
            PrefixList.ValueMember = "PrefixCode";
            PrefixList.SelectedIndex = 0;
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
                // Add header only once
                //if (!headerAdded)
                //{
                //    AddHeader();
                //}
                // Check for duplicates
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

                // If found, update its quantity instead of adding new
                if (existingPanel != null)
                {
                    var tag = (Tuple<ItemResponse, System.Windows.Forms.Label>)existingPanel.Tag;
                    tag.Item2.Text = qty.ToString(); // update Qty label
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
            CalculateTareWeight();
        }

        private void PalletWeight_TextChanged(object sender, EventArgs e)
        {
            CalculateTareWeight();
        }

        private void CalculateTareWeight()
        {
            int num1 = 0, num2 = 0;

            // Safely parse numbers
            int.TryParse(spoolwt.Text, out num1);
            int.TryParse(palletwtno.Text, out num2);

            // Set sum in tarewt
            tarewt.Text = (num1 + num2).ToString();
        }

        private void GrossWeight_TextChanged(object sender, EventArgs e)
        {
            CalculateNetWeight();
        }

        private void CalculateNetWeight()
        {
            int num1 = 0, num2 = 0;

            // Safely parse numbers
            int.TryParse(grosswtno.Text, out num1);
            int.TryParse(tarewt.Text, out num2);

            // Set sum in tarewt
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

            // Safely parse numbers
            int.TryParse(netwt.Text, out num1);
            int.TryParse(spoolno.Text, out num2);

            // Set sum in wtpercop
            wtpercop.Text = (num1 / num2).ToString();
        }

        private async void submit_Click(object sender, EventArgs e)
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

        public ProductionResponse SubmitPacking(ProductionRequest productionRequest)
        {
            // Submit logic
            var getResponse = PostCallApi(packingURL + "Production/Add", productionRequest).Result;
            var getBox = JsonConvert.DeserializeObject<ProductionResponse>(getResponse);

            return getBox;
        }

        private void Logout_Click(object sender, EventArgs e)
        {
            SessionManager.Clear();

            var loginForm = new Login();
            loginForm.Show();
            this.Close();
        }

        private void getAllPOYPackingList()
        {
            var getPackingResponse = GetCallApi(packingURL + "Production/GetAll?IsDropDown=" + false);
            var getPacking = JsonConvert.DeserializeObject<List<ProductionResponse>>(getPackingResponse);
            getPacking.Insert(0, new ProductionResponse { ProductionId = 0, PackingType = "Select Packing Type" });
            //PalletTypeList.DataSource = getPacking;
            //PalletTypeList.DisplayMember = "PackingType";
            //PalletTypeList.ValueMember = "PackingId";
            //PalletTypeList.SelectedIndex = 0;
            var getLastBox = getPacking.OrderByDescending(x => x.ProductionId).FirstOrDefault();
            this.copstxtbox.Text = "";
            this.tarewghttxtbox.Text = getLastBox.TareWt.ToString();
            this.grosswttxtbox.Text = getLastBox.GrossWt.ToString();
            this.netwttxtbox.Text = getLastBox.NetWt.ToString();
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
            // Custom border color
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
            // Custom border color
            using (Pen pen = new Pen(Color.LightGray, 2))
            {
                Rectangle rect = windinggrid.ClientRectangle;
                rect.Width -= 1;
                rect.Height -= 1;
                e.Graphics.DrawRectangle(pen, rect);
            }
        }
    }
}
