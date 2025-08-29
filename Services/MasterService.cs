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
    public class MasterService
    {
        HTTPMethod method = new HTTPMethod();
        string masterURL = ConfigurationManager.AppSettings["masterURL"];

        public List<MachineResponse> getMachineList()
        {
            var getMachineResponse = method.GetCallApi(masterURL + "Machine/GetAll?IsDropDown=" + false);
            var getMachine = JsonConvert.DeserializeObject<List<MachineResponse>>(getMachineResponse);
            return getMachine;
        }

        public List<QualityResponse> getQualityList()
        {
            var getQualityResponse = method.GetCallApi(masterURL + "Quality/GetAll?IsDropDown=" + false);
            var getQuality = JsonConvert.DeserializeObject<List<QualityResponse>>(getQualityResponse);
            return getQuality;
        }

        public List<PackSizeResponse> getPackSizeList()
        {
            var getPackSizeResponse = method.GetCallApi(masterURL + "PackSize/GetAll?IsDropDown=" + false);
            var getPackSize = JsonConvert.DeserializeObject<List<PackSizeResponse>>(getPackSizeResponse);
            return getPackSize;
        }

        public List<WindingTypeResponse> getWindingTypeList()
        {
            var getWindingTypeResponse = method.GetCallApi(masterURL + "WindingType/GetAll?IsDropDown=" + false);
            var getWindingType = JsonConvert.DeserializeObject<List<WindingTypeResponse>>(getWindingTypeResponse);
            return getWindingType;
        }

        public List<ItemResponse> getCopeItemList()
        {
            var getCopeItemResponse = method.GetCallApi(masterURL + "Items/GetAll?IsDropDown=" + false);
            var getCopeItem = JsonConvert.DeserializeObject<List<ItemResponse>>(getCopeItemResponse);
            return getCopeItem;
        }

        public List<ItemResponse> getBoxItemList()
        {
            var getBoxItemResponse = method.GetCallApi(masterURL + "Items/GetAll?IsDropDown=" + false);
            var getBox = JsonConvert.DeserializeObject<List<ItemResponse>>(getBoxItemResponse);
            return getBox;
        }

        public List<PrefixResponse> getPrefixList()         //passed hardcoded transactionTypeId for now
        {
            var getPrefixResponse = method.GetCallApi(masterURL + "Prefix/GetByTransactionTypeId?transactionTypeId=" + 7);
            var getPrefix = JsonConvert.DeserializeObject<List<PrefixResponse>>(getPrefixResponse);
            return getPrefix;
        }

        public MachineResponse getMachineById(int machineId)
        {
            var getMachineResponse = method.GetCallApi(masterURL + "Machine/GetById?machineId=" + machineId);
            var getMachine = JsonConvert.DeserializeObject<MachineResponse>(getMachineResponse);
            return getMachine;
        }

        public PackSizeResponse getPackSizeById(int packSizeId)
        {
            var getPacksizeResponse = method.GetCallApi(masterURL + "PackSize/GetById?PackSizeId=" + packSizeId);
            var getPacksize = JsonConvert.DeserializeObject<PackSizeResponse>(getPacksizeResponse);
            return getPacksize;
        }
    }
}
