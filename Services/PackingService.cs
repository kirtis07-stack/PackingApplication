using Newtonsoft.Json;
using PackingApplication.Helper;
using PackingApplication.Models.RequestEntities;
using PackingApplication.Models.ResponseEntities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlTypes;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PackingApplication.Services
{
    public class PackingService
    {
        HTTPMethod method = new HTTPMethod();
        string packingURL = ConfigurationManager.AppSettings["packingURL"];
        private static Logger Log = Logger.GetLogger();
        public async Task<List<ProductionResponse>> getAllPackingListByPackingType(string packingType)
        {
            var getPackingResponse = await method.GetCallApi(packingURL + "Production/GetAllProductionByPackingType?packingType=" + packingType);
            if (string.IsNullOrWhiteSpace(getPackingResponse))
                return new List<ProductionResponse>();
            var getPacking = JsonConvert.DeserializeObject<List<ProductionResponse>>(getPackingResponse) 
                ?? new List<ProductionResponse>();
            return getPacking;
        }

        public async Task<ProductionResponse> getLastBoxDetails(string packingType, long productionId)
        {
            var getPackingResponse = await method.GetCallApi(packingURL + "Production/GetLastBoxDetails?packingType=" + packingType + "&productionId=" + productionId);
            if (string.IsNullOrWhiteSpace(getPackingResponse))
                return new ProductionResponse();
            var getPacking = JsonConvert.DeserializeObject<ProductionResponse>(getPackingResponse)
                ?? new ProductionResponse();
            return getPacking;
        }

        public ProductionResponse AddUpdatePOYPacking(long packingId, ProductionRequest productionRequest)
        {
            Log.writeMessage("API call AddUpdatePOYPacking - Start : " + DateTime.Now);
            string jsonRequest = JsonConvert.SerializeObject(productionRequest, Formatting.Indented);
            Log.writeMessage("AddUpdatePOYPacking : Production/Add" + jsonRequest);
            if (packingId == 0)
            {
                var getPackingResponse = method.PostCallApi(packingURL + "Production/Add", productionRequest).Result;
                if (getPackingResponse.StatusCode != 200)
                {
                    var error = JsonConvert.DeserializeObject<ApiErrorResponse>(getPackingResponse.ResponseBody);
                    MessageBox.Show(error?.Message ?? "Something went wrong", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }
                Log.writeMessage("AddResponse : " + getPackingResponse);
                return JsonConvert.DeserializeObject<ProductionResponse>(getPackingResponse.ResponseBody);
            }
            else
            {
                var getPackingResponse = method.PutCallApi(packingURL + "Production/Update?productionId=" + packingId, productionRequest).Result;
                if (getPackingResponse.StatusCode != 200)
                {
                    var error = JsonConvert.DeserializeObject<ApiErrorResponse>(getPackingResponse.ResponseBody);
                    MessageBox.Show(error?.Message ?? "Something went wrong", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }
                Log.writeMessage("UpdateResponse : " + getPackingResponse);
                return JsonConvert.DeserializeObject<ProductionResponse>(getPackingResponse.ResponseBody);
            }
        }

        public async Task<ProductionResponse> getProductionById(long productionId)
        {
            var getPackingResponse = await method.GetCallApi(packingURL + "Production/GetById?PackingId=" + productionId);
            if (string.IsNullOrWhiteSpace(getPackingResponse))
                return new ProductionResponse();
            var getPacking = JsonConvert.DeserializeObject<ProductionResponse>(getPackingResponse) 
                ?? new ProductionResponse();
            return getPacking;
        }

        public async Task<List<ProductionResponse>> getAllProductionByQualityandSaleOrder(int qualityId, int saleOrderId)
        {
            var getPackingResponse = await method.GetCallApi(packingURL + "Production/GetAllProductionByQualityandSaleOrder?qualityId=" + qualityId + "&saleOrderId=" + saleOrderId);
            if (string.IsNullOrWhiteSpace(getPackingResponse))
                return new List<ProductionResponse>();
            var getPacking = JsonConvert.DeserializeObject<List<ProductionResponse>>(getPackingResponse) 
                ?? new List<ProductionResponse>();
            return getPacking;
        }

        public async Task<List<ProductionResponse>> getAllProductionByWindingTypeandSaleOrder(int windingTypeId, int saleOrderId)
        {
            var getPackingResponse = await method.GetCallApi(packingURL + "Production/GetAllProductionByWindingTypeandSaleOrder?windingTypeId=" + windingTypeId + "&saleOrderId=" + saleOrderId);
            if (string.IsNullOrWhiteSpace(getPackingResponse))
                return new List<ProductionResponse>();
            var getPacking = JsonConvert.DeserializeObject<List<ProductionResponse>>(getPackingResponse)
                ?? new List<ProductionResponse>();
            return getPacking;
        }

        public async Task<List<ProductionResponse>> getAllByLotIdandSaleOrderItemIdandPackingType(int lotId, int saleOrderItemId)
        {
            var getPackingResponse = await method.GetCallApi(packingURL + "Production/GetAllByLotIdandSaleOrderItemId?lotId=" + lotId + "&saleOrderItemId=" + saleOrderItemId);
            if (string.IsNullOrWhiteSpace(getPackingResponse))
                return new List<ProductionResponse>();              // handle empty response
            var getPacking = JsonConvert.DeserializeObject<List<ProductionResponse>>(getPackingResponse)
                     ?? new List<ProductionResponse>();             // handle null JSON
            return getPacking;
        }

        public async Task<List<ProductionResponse>> getAllBoxNoByPackingType(string packingType, string subString)
        {
            var getPackingResponse = await method.GetCallApi(packingURL + "Production/GetAllBoxNoByPackingType?packingType=" + packingType + "&subString=" + subString);
            if (string.IsNullOrWhiteSpace(getPackingResponse))
                return new List<ProductionResponse>();
            var getPacking = JsonConvert.DeserializeObject<List<ProductionResponse>>(getPackingResponse)
                ?? new List<ProductionResponse>();
            return getPacking;
        }

        public async Task<List<ProductionResponse>> getProductionDetailsBySelectedParameter(string packingType, int machineId, int deptId, string boxNo, string productionDate)
        {
            string ProductionDate = null;
            if (!string.IsNullOrEmpty(productionDate))
            {
                if (DateTime.TryParseExact(productionDate, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedDate))
                {
                    ProductionDate = parsedDate.ToString("yyyy-MM-dd");
                }
            }
            var getPackingResponse = await method.GetCallApi(packingURL + "Production/GetProductionDetailsBySelectedParameter?packingType=" + packingType + "&machineId=" + machineId + "&deptId=" + deptId + "&boxNo=" + boxNo + "&productionDate=" + ProductionDate);
            if (string.IsNullOrWhiteSpace(getPackingResponse))
                return new List<ProductionResponse>();
            var getPacking = JsonConvert.DeserializeObject<List<ProductionResponse>>(getPackingResponse)
                ?? new List<ProductionResponse>();
            return getPacking;
        }

        public int AddPrintSlip(ProductionPrintSlipRequest slipRequest)
        {
            Log.writeMessage("API call AddPrintSlip - Start : " + DateTime.Now);
            string jsonRequest = JsonConvert.SerializeObject(slipRequest, Formatting.Indented);
            Log.writeMessage("AddPrintSlip : ProductionPrintSlip/Add" + jsonRequest);

            var getSlipResponse = method.PostCallApi(packingURL + "ProductionPrintSlip/Add", slipRequest).Result;
            if (getSlipResponse.StatusCode != 200)
            {
                var error = JsonConvert.DeserializeObject<ApiErrorResponse>(getSlipResponse.ResponseBody);
                MessageBox.Show(error?.Message ?? "Something went wrong", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 0;
            }
            Log.writeMessage("AddPrintSlipResponse : " + getSlipResponse);
            return JsonConvert.DeserializeObject<int>(getSlipResponse.ResponseBody);
        }
    }
}
