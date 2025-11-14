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
        HTTPMethod method = new HTTPMethod();
        string masterURL = ConfigurationManager.AppSettings["masterURL"];

        public async Task<List<MachineResponse>> getMachineList(string lotType)
        {
            var getMachineResponse = method.GetCallApi(masterURL + "Machine/GetAllByLotType?lotType=" + lotType);
            if (string.IsNullOrWhiteSpace(getMachineResponse))
                return new List<MachineResponse>();
            var getMachine = JsonConvert.DeserializeObject<List<MachineResponse>>(getMachineResponse)
                ?? new List<MachineResponse>();
            return getMachine;
        }

        public async Task<List<QualityResponse>> getQualityList()
        {
            var getQualityResponse = method.GetCallApi(masterURL + "Quality/GetAll?IsDropDown=" + true);
            if (string.IsNullOrWhiteSpace(getQualityResponse))
                return new List<QualityResponse>();
            var getQuality = JsonConvert.DeserializeObject<List<QualityResponse>>(getQualityResponse)
                ?? new List<QualityResponse>();
            return getQuality;
        }

        public async Task<List<PackSizeResponse>> getPackSizeList()
        {
            var getPackSizeResponse = method.GetCallApi(masterURL + "PackSize/GetAll?IsDropDown=" + true);
            if (string.IsNullOrWhiteSpace(getPackSizeResponse))
                return new List<PackSizeResponse>();
            var getPackSize = JsonConvert.DeserializeObject<List<PackSizeResponse>>(getPackSizeResponse)
                ?? new List<PackSizeResponse>();
            return getPackSize;
        }

        public async Task<List<WindingTypeResponse>> getWindingTypeList()
        {
            var getWindingTypeResponse = method.GetCallApi(masterURL + "WindingType/GetAll?IsDropDown=" + true);
            if (string.IsNullOrWhiteSpace(getWindingTypeResponse))
                return new List<WindingTypeResponse>();
            var getWindingType = JsonConvert.DeserializeObject<List<WindingTypeResponse>>(getWindingTypeResponse)
                ?? new List<WindingTypeResponse>();
            return getWindingType;
        }

        public async Task<List<ItemResponse>> getItemList(int categoryId)
        {
            var getItemResponse = method.GetCallApi(masterURL + "Items/GetAllItemsByItemCategoryId?itemCategoryId=" + categoryId);
            //var getItem = JsonConvert.DeserializeObject<List<ItemResponse>>(getItemResponse);
            if (string.IsNullOrWhiteSpace(getItemResponse))
                return new List<ItemResponse>();              // handle empty response
            var getItem = JsonConvert.DeserializeObject<List<ItemResponse>>(getItemResponse)
                     ?? new List<ItemResponse>();
            return getItem;
        }

        public async Task<List<PrefixResponse>> getPrefixList(TransactionTypePrefixRequest prefixRequest)
        {
            List<PrefixResponse> getPrefixResponse = await method.PostAsync<PrefixResponse>(masterURL + "Prefix/GetPrefixByTransactionTypeFlags", prefixRequest);
            return getPrefixResponse;
        }


        public async Task<MachineResponse> getMachineById(int machineId)
        {
            var getMachineResponse = method.GetCallApi(masterURL + "Machine/GetById?machineId=" + machineId);
            if (string.IsNullOrWhiteSpace(getMachineResponse))
                return new MachineResponse();
            var getMachine = JsonConvert.DeserializeObject<MachineResponse>(getMachineResponse)
                ?? new MachineResponse();
            return getMachine;
        }

        public async Task<PackSizeResponse> getPackSizeById(int packSizeId)
        {
            var getPacksizeResponse = method.GetCallApi(masterURL + "PackSize/GetById?PackSizeId=" + packSizeId);
            if (string.IsNullOrWhiteSpace(getPacksizeResponse))
                return new PackSizeResponse();
            var getPacksize = JsonConvert.DeserializeObject<PackSizeResponse>(getPacksizeResponse)
                ?? new PackSizeResponse();
            return getPacksize;
        }

        public async Task<List<QualityResponse>> getQualityListByItemTypeId(int itemTypeId)
        {
            var getQualityResponse = method.GetCallApi(masterURL + "Quality/GetByItemTypeId?itemTypeId=" + itemTypeId);
            if (string.IsNullOrWhiteSpace(getQualityResponse))
                return new List<QualityResponse>();
            var getQuality = JsonConvert.DeserializeObject<List<QualityResponse>>(getQualityResponse)
                ?? new List<QualityResponse>();
            return getQuality;
        }

        public async Task<ItemResponse> getItemById(int itemId)
        {
            var getItemResponse = method.GetCallApi(masterURL + "Items/GetById?itemsId=" + itemId);
            if (string.IsNullOrWhiteSpace(getItemResponse))
                return new ItemResponse();
            var getItem = JsonConvert.DeserializeObject<ItemResponse>(getItemResponse)
                ?? new ItemResponse();
            return getItem;
        }

        public async Task<List<DepartmentResponse>> getDepartmentList()
        {
            var getDepartmentResponse = method.GetCallApi(masterURL + "Departments/GetAll?IsDropDown=" + false);
            if (string.IsNullOrWhiteSpace(getDepartmentResponse))
                return new List<DepartmentResponse>();
            var getDepartment = JsonConvert.DeserializeObject<List<DepartmentResponse>>(getDepartmentResponse)
                ?? new List<DepartmentResponse>();
            return getDepartment;
        }

        public async Task<List<MachineResponse>> getMachineByDepartmentId(int departmentId)
        {
            var getMachineResponse = method.GetCallApi(masterURL + "Machine/GetAllByDepartmentId?departmentId=" + departmentId);
            if (string.IsNullOrWhiteSpace(getMachineResponse))
                return new List<MachineResponse>();
            var getMachine = JsonConvert.DeserializeObject<List<MachineResponse>>(getMachineResponse)
                ?? new List<MachineResponse>();
            return getMachine;
        }

        public async Task<List<BusinessPartnerResponse>> getOwnerList()
        {
            var getBusinessPartnerResponse = method.GetCallApi(masterURL + "BusinessPartner/GetAll?IsDropDown=" + true);
            if (string.IsNullOrWhiteSpace(getBusinessPartnerResponse))
                return new List<BusinessPartnerResponse>();              // handle empty response
            var getBusinessPartner = JsonConvert.DeserializeObject<List<BusinessPartnerResponse>>(getBusinessPartnerResponse)
                     ?? new List<BusinessPartnerResponse>();             // handle null JSON
            return getBusinessPartner;
        }
    }
}