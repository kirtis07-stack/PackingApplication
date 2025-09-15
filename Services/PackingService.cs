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

        public List<ProductionResponse> getAllPackingListByPackingType(string packingType)
        {
            var getPackingResponse = method.GetCallApi(packingURL + "Production/GetAllProductionByPackingType?packingType=" + packingType);
            var getPacking = JsonConvert.DeserializeObject<List<ProductionResponse>>(getPackingResponse);
            return getPacking;
        }

        public ProductionResponse getLastBoxDetails()
        {
            var getPackingResponse = method.GetCallApi(packingURL + "Production/GetLastBoxDetails");
            var getPacking = JsonConvert.DeserializeObject<ProductionResponse>(getPackingResponse);
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

        public ProductionResponse getProductionById(long productionId)
        {
            var getPackingResponse = method.GetCallApi(packingURL + "Production/GetById?PackingId=" + productionId);
            var getPacking = JsonConvert.DeserializeObject<ProductionResponse>(getPackingResponse);
            return getPacking;
        }
    }
}
