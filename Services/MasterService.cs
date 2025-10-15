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

        public List<ItemResponse> getItemList(int categoryId)
        {
            var getItemResponse = method.GetCallApi(masterURL + "Items/GetAllItemsByItemCategoryId?itemCategoryId=" + categoryId);
            var getItem = JsonConvert.DeserializeObject<List<ItemResponse>>(getItemResponse);
            return getItem;
        }

        public List<PrefixResponse> getPrefixList()         //passed hardcoded transactionTypeId for now
        {
            var getPrefixResponse = method.GetCallApi(masterURL + "Prefix/GetByTransactionTypeId?transactionTypeId=" + 5);
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

        public List<QualityResponse> getQualityListByItemTypeId(int itemTypeId)
        {
            var getQualityResponse = method.GetCallApi(masterURL + "Quality/GetByItemTypeId?itemTypeId=" + itemTypeId);
            var getQuality = JsonConvert.DeserializeObject<List<QualityResponse>>(getQualityResponse);
            return getQuality;
        }

        public ItemResponse getItemById(int itemId)
        {
            var getItemResponse = method.GetCallApi(masterURL + "Items/GetById?itemsId=" + itemId);
            var getItem = JsonConvert.DeserializeObject<ItemResponse>(getItemResponse);
            return getItem;
        }

        public List<DepartmentResponse> getDepartmentList()
        {
            var getDepartmentResponse = method.GetCallApi(masterURL + "Departments/GetAll?IsDropDown=" + false);
            var getDepartment = JsonConvert.DeserializeObject<List<DepartmentResponse>>(getDepartmentResponse);
            return getDepartment;
        }

        public List<MachineResponse> getMachineByDepartmentId(int departmentId)
        {
            var getMachineResponse = method.GetCallApi(masterURL + "Machine/GetAllByDepartmentId?departmentId=" + departmentId);
            var getMachine = JsonConvert.DeserializeObject<List<MachineResponse>>(getMachineResponse);
            return getMachine;
        }

    }
}
