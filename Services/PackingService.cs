using Newtonsoft.Json;
using PackingApplication.Helper;
using PackingApplication.Models.RequestEntities;
using PackingApplication.Models.ResponseEntities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PackingApplication.Services
{
    public class PackingService
    {
        HTTPMethod method = new HTTPMethod();
        string packingURL = ConfigurationManager.AppSettings["packingURL"];

        public async Task<List<ProductionResponse>> getAllPackingListByPackingType(string packingType)
        {
            var getPackingResponse = await method.GetCallApi(packingURL + "Production/GetAllProductionByPackingType?packingType=" + packingType);
            if (string.IsNullOrWhiteSpace(getPackingResponse))
                return new List<ProductionResponse>();
            var getPacking = JsonConvert.DeserializeObject<List<ProductionResponse>>(getPackingResponse) 
                ?? new List<ProductionResponse>();
            return getPacking;
        }

        public async Task<ProductionResponse> getLastBoxDetails(string packingType)
        {
            var getPackingResponse = await method.GetCallApi(packingURL + "Production/GetLastBoxDetails?packingType=" + packingType);
            if (string.IsNullOrWhiteSpace(getPackingResponse))
                return new ProductionResponse();
            var getPacking = JsonConvert.DeserializeObject<ProductionResponse>(getPackingResponse)
                ?? new ProductionResponse();
            return getPacking;
        }

        public ProductionResponse AddUpdatePOYPacking(long packingId, ProductionRequest productionRequest)
        {
            if(packingId == 0)
            {
                var getPackingResponse = method.PostCallApi(packingURL + "Production/Add", productionRequest).Result;
                return JsonConvert.DeserializeObject<ProductionResponse>(getPackingResponse);
            }
            else
            {
                var getPackingResponse = method.PutCallApi(packingURL + "Production/Update?productionId=" + packingId, productionRequest).Result;
                return JsonConvert.DeserializeObject<ProductionResponse>(getPackingResponse);
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
    }
}
