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
        public DTYPackingForm()
        {
            InitializeComponent();
            this.Shown += DTYPackingForm_Shown;
            this.AutoScroll = true;

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
            getSaleOrder.Insert(0, new LotSaleOrderDetailsResponse { LotSaleOrderDetailsId = 0, SaleOrderNumber = "Select Sale Order" });
            SaleOrderList.DataSource = getSaleOrder;
            SaleOrderList.DisplayMember = "SaleOrderNumber";
            SaleOrderList.ValueMember = "LotSaleOrderDetailsId";
            SaleOrderList.SelectedIndex = 0;

            //Username.Text = SessionManager.UserName;
            //role.Text = SessionManager.Role;

            isFormReady = true;
        }

        private async void DTYPackingForm_Shown(object sender, EventArgs e)
        {
            var machineList = await Task.Run(() => getMachineList());
            var qualityList = await Task.Run(() => getQualityList());
            var packsizeList = await Task.Run(() => getPackSizeList());
            var windingtypeList = await Task.Run(() => getWindingTypeList());
            var comportList = await Task.Run(() => getComPortList());
            var weightingList = await Task.Run(() => getWeighingList());
            var copsitemList = await Task.Run(() => getCopeItemList());
            var boxitemList = await Task.Run(() => getBoxItemList());
            var prefixList = await Task.Run(() => getPrefixList());
            var dtypackingList = await Task.Run(() => getAllDTYPackingList());

            //machine
            LineNoList.DataSource = machineList;
            LineNoList.DisplayMember = "MachineName";
            LineNoList.ValueMember = "MachineId";
            LineNoList.SelectedIndex = 0;

            //quality
            QualityList.DataSource = qualityList;
            QualityList.DisplayMember = "Name";
            QualityList.ValueMember = "QualityId";
            QualityList.SelectedIndex = 0;

            //packsize
            PackSizeList.DataSource = packsizeList;
            PackSizeList.DisplayMember = "PackSizeName";
            PackSizeList.ValueMember = "PackSizeId";
            PackSizeList.SelectedIndex = 0;

            //windingtype
            WindingTypeList.DataSource = windingtypeList;
            WindingTypeList.DisplayMember = "WindingTypeName";
            WindingTypeList.ValueMember = "WindingTypeId";
            WindingTypeList.SelectedIndex = 0;

            //comport
            ComPortList.DataSource = comportList;
            ComPortList.SelectedIndex = 0;

            //weighting
            WeighingList.DataSource = weightingList;
            WeighingList.SelectedIndex = 0;

            //copsitem
            CopsItemList.DataSource = copsitemList;
            CopsItemList.DisplayMember = "Name";
            CopsItemList.ValueMember = "ItemId";
            CopsItemList.SelectedIndex = 0;

            //boxitem
            BoxItemList.DataSource = boxitemList;
            BoxItemList.DisplayMember = "Name";
            BoxItemList.ValueMember = "ItemId";
            BoxItemList.SelectedIndex = 0;

            //prefix
            PrefixList.DataSource = prefixList;
            PrefixList.DisplayMember = "Prefix";
            PrefixList.ValueMember = "PrefixCode";
            PrefixList.SelectedIndex = 0;

            //poypacking
            var getLastBox = dtypackingList.OrderByDescending(x => x.ProductionId).FirstOrDefault();
            this.copstxtbox.Text = "";
            this.tarewghttxtbox.Text = getLastBox.TareWt.ToString();
            this.grosswttxtbox.Text = getLastBox.GrossWt.ToString();
            this.netwttxtbox.Text = getLastBox.NetWt.ToString();

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
            getMachine.Insert(0, new MachineResponse { MachineId = 0, MachineName = "Select Line No." });
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
            getQuality.Insert(0, new QualityResponse { QualityId = 0, Name = "Select Quality" });
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
            getSaleOrder.Insert(0, new LotSaleOrderDetailsResponse { LotSaleOrderDetailsId = 0, SaleOrderNumber = "Select Sale Order" });
            SaleOrderList.DataSource = getSaleOrder;
            SaleOrderList.DisplayMember = "SaleOrderNumber";
            SaleOrderList.ValueMember = "LotSaleOrderDetailsId";
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
            getCopeItem.Insert(0, new ItemResponse { ItemId = 0, Name = "Select Cops Item" });
            return getCopeItem;
        }

        private List<ItemResponse> getBoxItemList()
        {
            var getBox = _masterService.getBoxItemList();
            getBox.Insert(0, new ItemResponse { ItemId = 0, Name = "Select Box/Pallet" });
            return getBox;
        }

        private List<PrefixResponse> getPrefixList()
        {
            var getPrefix = _masterService.getPrefixList();
            getPrefix.Insert(0, new PrefixResponse { PrefixCode = 0, Prefix = "Select Prefix" });
            return getPrefix;
        }

        private void SpoolWeight_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(spoolwt.Text))
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
            if (!string.IsNullOrWhiteSpace(palletwtno.Text))
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
            if (!string.IsNullOrWhiteSpace(spoolno.Text))
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
            result = _packingService.AddUpdatePOYPacking(0, productionRequest);
            if (result != null)
            {
                if (result.ProductionId > 0)
                {
                    MessageBox.Show("Packing added successfully.");
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

        private List<ProductionResponse> getAllDTYPackingList()
        {
            var getPacking = _packingService.getAllPOYPackingList();
            getPacking.Insert(0, new ProductionResponse { ProductionId = 0, PackingType = "Select Packing Type" });
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

            if (string.IsNullOrWhiteSpace(spoolno.Text) || Convert.ToInt32(spoolno.Text) > 0)
            {
                spoolnoerror.Text = "Please enter valid spool no";
                spoolnoerror.Visible = true;
                isValid = false;
            }

            if (string.IsNullOrWhiteSpace(spoolwt.Text) || Convert.ToInt32(spoolwt.Text) > 0)
            {
                spoolwterror.Text = "Please enter valid spool weight";
                spoolwterror.Visible = true;
                isValid = false;
            }

            if (string.IsNullOrWhiteSpace(palletwtno.Text) || Convert.ToInt32(palletwtno.Text) > 0)
            {
                palletwterror.Text = "Please enter valid empty box/pallet weight";
                palletwterror.Visible = true;
                isValid = false;
            }

            if (string.IsNullOrWhiteSpace(grosswtno.Text) || Convert.ToInt32(grosswtno.Text) >= 0)
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
            if (!string.IsNullOrWhiteSpace(copyno.Text))
            {
                copynoerror.Text = "";
                copynoerror.Visible = false;
            }
        }
    }
}
