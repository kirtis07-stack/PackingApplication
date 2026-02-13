using Microsoft.Reporting.Map.WebForms.BingMaps;
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
    public class MasterService
    {
        private static Logger Log = Logger.GetLogger();
        HTTPMethod method = new HTTPMethod();
        string masterURL = ConfigurationManager.AppSettings["masterURL"];

        public async Task<List<MachineResponse>> GetMachineList(string lotType, string subString)
        {
            Log.writeMessage("API call GetMachineList - Start : " + DateTime.Now);
            Log.writeMessage("GetMachineList : Machine/GetAllByLotType?lotType=" + lotType + "&subString=" + subString);
            var getMachineResponse = await method.GetCallApi(masterURL + "Machine/GetAllByLotType?lotType=" + lotType + "&subString=" + subString);

            Log.writeMessage("Response : " + getMachineResponse);

            if (string.IsNullOrWhiteSpace(getMachineResponse))
            {                
                Log.writeMessage("API call GetMachineList - End : " + DateTime.Now);
                return new List<MachineResponse>();
            }

            var getMachine = JsonConvert.DeserializeObject<List<MachineResponse>>(getMachineResponse) ?? new List<MachineResponse>();
            Log.writeMessage("API call GetMachineList - End : " + DateTime.Now);

            return getMachine;            
        }

        public async Task<List<QualityResponse>> GetQualityList()
        {
            Log.writeMessage("API call GetQualityList - Start : " + DateTime.Now);
            Log.writeMessage("GetQualityList : Quality/GetAll?IsDropDown=false");
            var getQualityResponse = await method.GetCallApi(masterURL + "Quality/GetAll?IsDropDown=" + false);

            Log.writeMessage("Response : " + getQualityResponse);

            if (string.IsNullOrWhiteSpace(getQualityResponse))
            {                
                Log.writeMessage("API call GetQualityList - End : " + DateTime.Now);
                return new List<QualityResponse>();
            }

            var getQuality = JsonConvert.DeserializeObject<List<QualityResponse>>(getQualityResponse) ?? new List<QualityResponse>();
            Log.writeMessage("API call GetQualityList - End : " + DateTime.Now);

            return getQuality;
        }

        public async Task<List<PackSizeResponse>> GetPackSizeList(short mainItemTypeId, string subString)
        {
            Log.writeMessage("API call GetPackSizeList - Start : " + DateTime.Now);
            Log.writeMessage("GetPackSizeList : PackSize/GetPackSizeByMainItemTypeId?mainItemTypeId=mainItemTypeId&subString=" + subString);

            var getPackSizeResponse = await method.GetCallApi(masterURL + "PackSize/GetPackSizeByMainItemTypeId?mainItemTypeId=" + mainItemTypeId + "&subString=" + subString);

            Log.writeMessage("Response : " + getPackSizeResponse);

            if (string.IsNullOrWhiteSpace(getPackSizeResponse))
            {                
                Log.writeMessage("API call GetPackSizeList - End : " + DateTime.Now);
                return new List<PackSizeResponse>();
            }

            var getPackSize = JsonConvert.DeserializeObject<List<PackSizeResponse>>(getPackSizeResponse) ?? new List<PackSizeResponse>();
            Log.writeMessage("API call GetPackSizeList - End : " + DateTime.Now);

            return getPackSize;
        }

        //public async Task<List<WindingTypeResponse>> GetWindingTypeList(string subString)
        //{
        //    Log.writeMessage("API call GetWindingTypeList - Start : " + DateTime.Now);
        //    Log.writeMessage("GetWindingTypeList : WindingType/GetAll?IsDropDown=false" + "&subString=" + subString);

        //    var getWindingTypeResponse = await method.GetCallApi(masterURL + "WindingType/GetAll?IsDropDown=" + false + "&subString=" + subString);

        //    Log.writeMessage("GetWindingTypeList Response : " + getWindingTypeResponse);

        //    if (string.IsNullOrWhiteSpace(getWindingTypeResponse))
        //    {                
        //        Log.writeMessage("API call GetWindingTypeList - End : " + DateTime.Now);
        //        return new List<WindingTypeResponse>();
        //    }

        //    List<WindingTypeResponse> getWindingType = JsonConvert.DeserializeObject<List<WindingTypeResponse>>(getWindingTypeResponse);
        //    Log.writeMessage("API call GetWindingTypeList - End : " + DateTime.Now);

        //    return getWindingType;
        //}

        public async Task<List<ItemResponse>> GetItemList(int categoryId, string subString)
        {
            Log.writeMessage("API call GetItemList - Start : " + DateTime.Now);
            Log.writeMessage("GetItemList : Items/GetAllItemsByItemCategoryId?itemCategoryId=" + categoryId + "&subString=" + subString);

            var getItemResponse = await method.GetCallApi(masterURL + "Items/GetAllItemsByItemCategoryId?itemCategoryId=" + categoryId + "&subString=" + subString);

            Log.writeMessage("GetItemList Response : " + getItemResponse);

            if (string.IsNullOrWhiteSpace(getItemResponse))
            {                
                Log.writeMessage("API call GetItemList - End : " + DateTime.Now);
                return new List<ItemResponse>();              // handle empty response
            }

            var getItem = JsonConvert.DeserializeObject<List<ItemResponse>>(getItemResponse) ?? new List<ItemResponse>();
            Log.writeMessage("API call GetItemList - End : " + DateTime.Now);

            return getItem;
        }

        public async Task<List<PrefixResponse>> GetPrefixList(TransactionTypePrefixRequest prefixRequest)
        {
            Log.writeMessage("API call GetPrefixList - Start : " + DateTime.Now);
            Log.writeMessage("GetPrefixList : Prefix/GetPrefixByTransactionTypeFlags - Request > " + JsonConvert.SerializeObject(prefixRequest));

            List<PrefixResponse> getPrefixResponse = await method.PostAsync<PrefixResponse>(masterURL + "Prefix/GetPrefixByTransactionTypeFlags", prefixRequest);

            Log.writeMessage("GetItemList Response : " + JsonConvert.SerializeObject(getPrefixResponse));
            Log.writeMessage("API call GetPrefixList - End : " + DateTime.Now);

            return getPrefixResponse;
        }

        public async Task<MachineResponse> GetMachineById(int machineId)
        {
            Log.writeMessage("API call GetMachineById - Start : " + DateTime.Now);
            Log.writeMessage("GetMachineById : Machine/GetById?machineId= " + +machineId);

            var getMachineResponse = await method.GetCallApi(masterURL + "Machine/GetById?machineId=" + machineId);
            
            Log.writeMessage("GetMachineById Response : " + getMachineResponse);

            if (string.IsNullOrWhiteSpace(getMachineResponse))
            {                
                Log.writeMessage("API call GetMachineById - End : " + DateTime.Now);
                return new MachineResponse();
            }

            var getMachine = JsonConvert.DeserializeObject<MachineResponse>(getMachineResponse) ?? new MachineResponse();           
            Log.writeMessage("API call GetMachineById - End : " + DateTime.Now);

            return getMachine;
        }

        public async Task<PackSizeResponse> GetPackSizeById(int packSizeId)
        {
            Log.writeMessage("API call GetPackSizeById - Start : " + DateTime.Now);
            Log.writeMessage("GetPackSizeById : PackSize/GetById?PackSizeId=" + packSizeId);

            var getPacksizeResponse = await method.GetCallApi(masterURL + "PackSize/GetById?PackSizeId=" + packSizeId);

            Log.writeMessage("GetPackSizeById Response : " + getPacksizeResponse);

            if (string.IsNullOrWhiteSpace(getPacksizeResponse))
            {                
                Log.writeMessage("API call GetPackSizeById - End : " + DateTime.Now);
                return new PackSizeResponse();
            }

            var getPacksize = JsonConvert.DeserializeObject<PackSizeResponse>(getPacksizeResponse) ?? new PackSizeResponse();            
            Log.writeMessage("API call GetPackSizeById - End : " + DateTime.Now);

            return getPacksize;
        }

        public async Task<List<QualityResponse>> GetQualityListByItemTypeId(int itemTypeId)
        {
            Log.writeMessage("API call GetQualityListByItemTypeId - Start : " + DateTime.Now);
            Log.writeMessage("GetQualityListByItemTypeId : Quality/GetByItemTypeId?itemTypeId=" + itemTypeId);

            var getQualityResponse = await method.GetCallApi(masterURL + "Quality/GetByItemTypeId?itemTypeId=" + itemTypeId);

            Log.writeMessage("GetQualityListByItemTypeId Response : " + getQualityResponse);

            if (string.IsNullOrWhiteSpace(getQualityResponse))
            {                
                Log.writeMessage("API call GetQualityListByItemTypeId - End : " + DateTime.Now);
                return new List<QualityResponse>();
            }

            var getQuality = JsonConvert.DeserializeObject<List<QualityResponse>>(getQualityResponse) ?? new List<QualityResponse>();
            Log.writeMessage("API call GetQualityListByItemTypeId - End : " + DateTime.Now);

            return getQuality;
        }

        public async Task<ItemResponse> GetItemById(int itemId)
        {
            Log.writeMessage("API call GetItemById - Start : " + DateTime.Now);
            Log.writeMessage("GetItemById : Items/GetById?itemsId=" + itemId);

            var getItemResponse = await method.GetCallApi(masterURL + "Items/GetById?itemsId=" + itemId);

            Log.writeMessage("GetItemById Response : " + getItemResponse);

            if (string.IsNullOrWhiteSpace(getItemResponse))
            {
                Log.writeMessage("API call GetItemById - End : " + DateTime.Now);
                return new ItemResponse();
            }

            var getItem = JsonConvert.DeserializeObject<ItemResponse>(getItemResponse) ?? new ItemResponse();
            Log.writeMessage("API call GetItemById - End : " + DateTime.Now);

            return getItem;
        }

        public async Task<List<DepartmentResponse>> GetDepartmentList(string packingType, string subString)
        {
            Log.writeMessage("API call GetDepartmentList - Start : " + DateTime.Now);
            Log.writeMessage("GetDepartmentsByPackingType : Departments/packingType?packingType=packingType&subString=subString");

            var getDepartmentResponse = await method.GetCallApi(masterURL + "Departments/GetAllByPackingType?packingType=" + packingType + "&subString=" + subString);

            Log.writeMessage("GetDepartmentList Response : " + getDepartmentResponse);

            if (string.IsNullOrWhiteSpace(getDepartmentResponse))
            {
                Log.writeMessage("API call GetDepartmentList - End : " + DateTime.Now);
                return new List<DepartmentResponse>();
            }

            var getDepartment = JsonConvert.DeserializeObject<List<DepartmentResponse>>(getDepartmentResponse) ?? new List<DepartmentResponse>();
            Log.writeMessage("API call GetDepartmentList - End : " + DateTime.Now);

            return getDepartment;
        }

        public async Task<List<MachineResponse>> GetMachineByDepartmentIdAndLotType(int departmentId, string lotType)
        {
            Log.writeMessage("API call GetMachineByDepartmentIdAndLotType - Start : " + DateTime.Now);
            Log.writeMessage("GetMachineByDepartmentIdAndLotType : Machine/GetAllByDepartmentIdAndLotType?departmentId=" + departmentId + "&lotType=" + lotType);

            var getMachineResponse = await method.GetCallApi(masterURL + "Machine/GetAllByDepartmentIdAndLotType?departmentId=" + departmentId + "&lotType=" + lotType);

            Log.writeMessage("GetMachineByDepartmentIdAndLotType Response : " + getMachineResponse);

            if (string.IsNullOrWhiteSpace(getMachineResponse))
            {
                Log.writeMessage("API call GetMachineByDepartmentIdAndLotType - End : " + DateTime.Now);
                return new List<MachineResponse>();
            }

            var getMachine = JsonConvert.DeserializeObject<List<MachineResponse>>(getMachineResponse) ?? new List<MachineResponse>();
            Log.writeMessage("API call GetMachineByDepartmentIdAndLotType - End : " + DateTime.Now);

            return getMachine;
        }

        public async Task<List<BusinessPartnerResponse>> GetOwnerList(string subString)
        {
            Log.writeMessage("API call GetOwnerList - Start : " + DateTime.Now);
            Log.writeMessage("GetOwnerList : BusinessPartner/GetAllSearchable");

            var getBusinessPartnerResponse = await method.GetCallApi(masterURL + "BusinessPartner/GetAllSearchable?subString=" + subString);

            Log.writeMessage("GetOwnerList Response : " + getBusinessPartnerResponse);

            if (string.IsNullOrWhiteSpace(getBusinessPartnerResponse))
            {
                Log.writeMessage("API call GetOwnerList - End : " + DateTime.Now);
                return new List<BusinessPartnerResponse>();              // handle empty response
            }

            var getBusinessPartner = JsonConvert.DeserializeObject<List<BusinessPartnerResponse>>(getBusinessPartnerResponse) ?? new List<BusinessPartnerResponse>();             // handle null JSON
            Log.writeMessage("API call GetOwnerList - End : " + DateTime.Now);

            return getBusinessPartner;
        }
    }
}