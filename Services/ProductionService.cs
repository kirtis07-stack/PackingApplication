using Newtonsoft.Json;
using PackingApplication.Helper;
using PackingApplication.Models.ResponseEntities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PackingApplication.Services
{
    public class ProductionService
    {
        HTTPMethod method = new HTTPMethod();
        string productionURL = ConfigurationManager.AppSettings["productionURL"];

        public List<LotsResponse> getLotList(int machineId)
        {
            var getLotsResponse = method.GetCallApi(productionURL + "Lots/GetAllByMachineId?machineId=" + machineId);
            var getItem = JsonConvert.DeserializeObject<List<LotsResponse>>(getLotsResponse);
            return getItem;
        }

        public List<LotsResponse> getAllLotList()
        {
            var getLotsResponse = method.GetCallApi(productionURL + "Lots/GetAll?IsDropDown=" + true);
            var getItem = JsonConvert.DeserializeObject<List<LotsResponse>>(getLotsResponse);
            return getItem;
        }

        public List<LotSaleOrderDetailsResponse> getSaleOrderList(int lotId)
        {
            var getSaleOrderResponse = method.GetCallApi(productionURL + "LotSaleOrderDetails/GetAllByLotsId?lotsId=" + lotId);
            var getSaleOrder = JsonConvert.DeserializeObject<List<LotSaleOrderDetailsResponse>>(getSaleOrderResponse);
            return getSaleOrder;
        }

        public LotsResponse getLotById(int lotId)
        {
            var getLotsResponse = method.GetCallApi(productionURL + "Lots/GetById?LotsId=" + lotId);
            var getLot = JsonConvert.DeserializeObject<LotsResponse>(getLotsResponse);
            return getLot;
        }
    }
}
