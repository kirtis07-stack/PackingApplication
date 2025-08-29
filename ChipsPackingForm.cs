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
    public partial class ChipsPackingForm : Form
    {
        MasterService _masterService = new MasterService();
        ProductionService _productionService = new ProductionService();
        PackingService _packingService = new PackingService();
        public ChipsPackingForm()
        {
            InitializeComponent();
            this.AutoScroll = true;
        }

        private void ChipsPackingForm_Load(object sender, EventArgs e)
        {
            getMachineList();
            getQualityList();
            getPackSizeList();
            getWindingTypeList();
            getComPortList();
            getWeighingList();
            getCopeItemList();
            getAllChipsPackingList();
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

            //Username.Text = SessionManager.UserName;
            //role.Text = SessionManager.Role;

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
            if (MergeNoList.SelectedValue != null)
            {
                LotsResponse selectedLot = (LotsResponse)MergeNoList.SelectedItem;
                int selectedLotId = selectedLot.LotId;

                productionRequest.LotId = selectedLot.LotId;

                var item = _productionService.getLotById(selectedLotId);
                itemname.Text = item.ItemName;
                shadename.Text = item.ShadeName;
                shadecd.Text = item.ShadeCode;
                deniervalue.Text = item.Denier.ToString();
                twistvalue.Text = item.TwistName;

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

                var packsize = _masterService.getPackSizeById(selectedPacksizeId);
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
            var getMachine = _masterService.getMachineList();
            getMachine.Insert(0, new MachineResponse { MachineId = 0, MachineName = "Select Line No." });
            LineNoList.DataSource = getMachine;
            LineNoList.DisplayMember = "MachineName";
            LineNoList.ValueMember = "MachineId";
            LineNoList.SelectedIndex = 0;
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

        private void getQualityList()
        {
            var getQuality = _masterService.getQualityList();
            getQuality.Insert(0, new QualityResponse { QualityId = 0, Name = "Select Quality" });
            QualityList.DataSource = getQuality;
            QualityList.DisplayMember = "Name";
            QualityList.ValueMember = "QualityId";
            QualityList.SelectedIndex = 0;
        }

        private void getPackSizeList()
        {
            var getPackSize = _masterService.getPackSizeList();
            getPackSize.Insert(0, new PackSizeResponse { PackSizeId = 0, PackSizeName = "Select Pack Size" });
            PackSizeList.DataSource = getPackSize;
            PackSizeList.DisplayMember = "PackSizeName";
            PackSizeList.ValueMember = "PackSizeId";
            PackSizeList.SelectedIndex = 0;
        }

        private void getWindingTypeList()
        {
            var getWindingType = _masterService.getWindingTypeList();
            getWindingType.Insert(0, new WindingTypeResponse { WindingTypeId = 0, WindingTypeName = "Select Winding Type" });
            WindingTypeList.DataSource = getWindingType;
            WindingTypeList.DisplayMember = "WindingTypeName";
            WindingTypeList.ValueMember = "WindingTypeId";
            WindingTypeList.SelectedIndex = 0;
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
            var getCopeItem = _masterService.getCopeItemList();
            getCopeItem.Insert(0, new ItemResponse { ItemId = 0, Name = "Select Bags Type" });
            CopsItemList.DataSource = getCopeItem;
            CopsItemList.DisplayMember = "Name";
            CopsItemList.ValueMember = "ItemId";
            CopsItemList.SelectedIndex = 0;
        }

        private void getPrefixList()
        {
            var getPrefix = _masterService.getPrefixList();
            getPrefix.Insert(0, new PrefixResponse { PrefixCode = 0, Prefix = "Select Prefix" });
            PrefixList.DataSource = getPrefix;
            PrefixList.DisplayMember = "Prefix";
            PrefixList.ValueMember = "PrefixCode";
            PrefixList.SelectedIndex = 0;
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
            int num1 = 0;
            int.TryParse(palletwtno.Text, out num1);

            tarewt.Text = (num1).ToString();
        }

        private void GrossWeight_TextChanged(object sender, EventArgs e)
        {
            CalculateNetWeight();
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

        private async void submit_Click(object sender, EventArgs e)
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

            ProductionResponse result = SubmitPacking(productionRequest);
        }

        public ProductionResponse SubmitPacking(ProductionRequest productionRequest)
        {
            var result = _packingService.AddUpdatePOYPacking(0, productionRequest);
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

        private void getAllChipsPackingList()
        {
            var getPacking = _packingService.getAllPOYPackingList();
            getPacking.Insert(0, new ProductionResponse { ProductionId = 0, PackingType = "Select Packing Type" });

            var getLastBox = getPacking.OrderByDescending(x => x.ProductionId).FirstOrDefault();
            this.copstxtbox.Text = "";
            this.tarewghttxtbox.Text = getLastBox.TareWt.ToString();
            this.grosswttxtbox.Text = getLastBox.GrossWt.ToString();
            this.netwttxtbox.Text = getLastBox.NetWt.ToString();
        }

        private void backbutton_Click(object sender, EventArgs e)
        {
            AdminAccount parentForm = this.ParentForm as AdminAccount;

            if (parentForm != null)
            {
                parentForm.LoadFormInContent(new Dashboard());
            }
        }
    }
}
