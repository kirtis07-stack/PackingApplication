using Newtonsoft.Json;
using PackingApplication.Helper;
using PackingApplication.Models.ResponseEntities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PackingApplication.Services
{
    public class ProductionService
    {
        HTTPMethod method = new HTTPMethod();
        string productionURL = ConfigurationManager.AppSettings["productionURL"];

        public async Task<List<LotsResponse>> getLotList(int machineId, string subString)
        {
            var getLotsResponse = await method.GetCallApi(productionURL + "Lots/GetAllByMachineId?machineId=" + machineId + "&subString=" + subString);
            if (string.IsNullOrWhiteSpace(getLotsResponse))
                return new List<LotsResponse>();
            var getItem = JsonConvert.DeserializeObject<List<LotsResponse>>(getLotsResponse)
                ?? new List<LotsResponse>();
            foreach (var item in getItem)
            {
                item.LotNoFrmt = item.LotNoFrmt + "--" + item.ItemTradeName;
            }
            return getItem;
        }

        public async Task<List<LotsResponse>> getAllLotList()
        {
            var getLotsResponse = await method.GetCallApi(productionURL + "Lots/GetAll?IsDropDown=" + false);
            if (string.IsNullOrWhiteSpace(getLotsResponse))
                return new List<LotsResponse>();
            var getItem = JsonConvert.DeserializeObject<List<LotsResponse>>(getLotsResponse)
                ?? new List<LotsResponse>();
            return getItem;
        }

        public async Task<List<LotSaleOrderDetailsResponse>> getSaleOrderList(int lotId, string subString)
        {
            var getSaleOrderResponse = await method.GetCallApi(productionURL + "LotSaleOrderDetails/GetAllByLotsId?lotsId=" + lotId + "&subString=" + subString);
            if (string.IsNullOrWhiteSpace(getSaleOrderResponse))
                return new List<LotSaleOrderDetailsResponse>();
            var getSaleOrder = JsonConvert.DeserializeObject<List<LotSaleOrderDetailsResponse>>(getSaleOrderResponse)
                ?? new List<LotSaleOrderDetailsResponse>();
            foreach (var item in getSaleOrder)
            {
                item.ItemName = item.SaleOrderNumber + "--" + item.ItemName + "--" + item.ShadeName + "--" + item.Quantity;
            }
            return getSaleOrder;
        }

        public async Task<LotsResponse> getLotById(int lotId)
        {
            var getLotsResponse = await method.GetCallApi(productionURL + "Lots/GetById?LotsId=" + lotId);
            if (string.IsNullOrWhiteSpace(getLotsResponse))
                return new LotsResponse();
            var getLot = JsonConvert.DeserializeObject<LotsResponse>(getLotsResponse)
                ?? new LotsResponse();
            getLot.ItemName = getLot.ItemTradeName;
            return getLot;
        }

        public async Task<List<LotsDetailsResponse>> getLotsDetailsByLotsIdAndProductionDate(int lotsId, DateTime ProductionDate)
        {
            String date = ProductionDate.ToString("yyyy-MM-dd");
            var getLotsDetailsResponse = await method.GetCallApi(productionURL + "LotsDetails/GetAllByLotsIdAndProductionDate?lotsId=" + lotsId + "&ProductionDate=" + date);
            if (string.IsNullOrWhiteSpace(getLotsDetailsResponse))
                return new List<LotsDetailsResponse>();
            var getLotDetails = JsonConvert.DeserializeObject<List<LotsDetailsResponse>>(getLotsDetailsResponse) 
                ?? new List<LotsDetailsResponse>();
            return getLotDetails;
        }

        public async Task<List<WindingTypeResponse>> getWinderTypeList(int lotId, string subString)
        {
            List<WindingTypeResponse> getWindingList = new List<WindingTypeResponse>();
            var getWinderTypeResponse = await method.GetCallApi(productionURL + "LotsProductionDetails/GetAllByLotsId?lotsId=" + lotId);
            if (string.IsNullOrWhiteSpace(getWinderTypeResponse))
                return new List<WindingTypeResponse>();
            var getWinderType = JsonConvert.DeserializeObject<List<LotsProductionDetailsResponse>>(getWinderTypeResponse) 
                ?? new List<LotsProductionDetailsResponse>();
            foreach (var item in getWinderType)
            {
                WindingTypeResponse type = new WindingTypeResponse();
                type.WindingTypeId = item.WindingTypeId;
                type.WindingTypeName = item.WindingTypeName;
                type.Quantity = item.Quantity;
                getWindingList.Add(type);
            }
            return getWindingList;
        }

        public async Task<List<LotsResponse>> getLotsByLotType(string lotType, string subString, DateTime? startDate = null, DateTime? endDate = null)
        {
            var getLotsResponse = await method.GetCallApi(productionURL + "Lots/GetByLotType?lotType=" + lotType + "&subString=" + subString);
            if (string.IsNullOrWhiteSpace(getLotsResponse))
                return new List<LotsResponse>();
            var getItem = JsonConvert.DeserializeObject<List<LotsResponse>>(getLotsResponse)
                ?? new List<LotsResponse>();
            foreach (var item in getItem)
            {
                item.LotNoFrmt = item.LotNoFrmt + "--" + item.ItemTradeName;
            }
            return getItem;
        }
    }
}