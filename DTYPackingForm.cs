using PackingApplication.Helper;
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
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PackingApplication
{
    public partial class DTYPackingForm: Form
    {
        private static Logger Log = Logger.GetLogger();

        MasterService _masterService = new MasterService();
        ProductionService _productionService = new ProductionService();
        PackingService _packingService = new PackingService();
        private long _productionId;
        public DTYPackingForm(long productionId)
        {
            InitializeComponent();
            this.Shown += DTYPackingForm_Shown;
            this.AutoScroll = true;
            _productionId = productionId;

            SetButtonBorderRadius(this.submit, 8);

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
        }

        private void DTYPackingForm_Load(object sender, EventArgs e)
        {
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

            copyno.Text = "1";
            //Username.Text = SessionManager.UserName;
            //role.Text = SessionManager.Role;

            isFormReady = true;
        }

        private async void DTYPackingForm_Shown(object sender, EventArgs e)
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

            var getLastBox = await Task.Run(() => getLastBoxDetails());
            //lastboxdetails
            if (getLastBox.ProductionId > 0)
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
                    prtwist.Checked = productionResponse.PrintTwist;
                    spoolno.Text = productionResponse.Spools.ToString();
                    spoolwt.Text = productionResponse.SpoolsWt.ToString();
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
            int num1 = 0, num2 = 0;

            int.TryParse(spoolwt.Text, out num1);
            int.TryParse(palletwtno.Text, out num2);

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
            if (string.IsNullOrWhiteSpace(spoolno.Text))
            {
                spoolnoerror.Text = "";
                spoolnoerror.Visible = false;
            }
            else
            {
                CalculateWeightPerCop();
            }
        }

        private void CalculateWeightPerCop()
        {
            int num1 = 0, num2 = 0;

            int.TryParse(netwt.Text, out num1);
            int.TryParse(spoolno.Text, out num2);

            wtpercop.Text = (num1 / num2).ToString();
        }

        private async void submit_Click(object sender, EventArgs e)
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

                productionRequest.PrintCompany = prcompany.Checked;
                productionRequest.PrintOwner = prowner.Checked;
                productionRequest.PrintDate = prdate.Checked;
                productionRequest.PrintUser = pruser.Checked;
                productionRequest.PrintHindiWords = prhindi.Checked;
                productionRequest.PrintQRCode = prqrcode.Checked;
                productionRequest.PrintWTPS = prwtps.Checked;
                productionRequest.PrintTwist = prtwist.Checked;

                productionRequest.PalletDetailsRequest = new List<ProductionPalletDetailsRequest>();

                ProductionResponse result = SubmitPacking(productionRequest);
            }
        }

        public ProductionResponse SubmitPacking(ProductionRequest productionRequest)
        {
            ProductionResponse result = new ProductionResponse();
            result = _packingService.AddUpdatePOYPacking(_productionId, productionRequest);
            if (result != null)
            {
                if (_productionId == 0)
                {
                    MessageBox.Show("DTY Packing added successfully.");
                }
                else
                {
                    MessageBox.Show("DTY Packing updated successfully.");
                }
            }
            else
            {
                MessageBox.Show("Something went wrong.");
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
            var dashboard = this.ParentForm as AdminAccount;
            if (dashboard != null)
            {
                dashboard.LoadFormInContent(new DTYPackingList());
            }
        }
    }
}
