using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PackingApplication.Models.ResponseEntities
{
    public class ItemResponse
    {
        public int ItemId { get; set; }
        public int ItemSPCategoryId { get; set; }
        public string ItemSPCategoryName { get; set; }
        public int ItemGSTCategoryId { get; set; }
        public string ItemGSTCategoryName { get; set; }
        public string ItemStructureHash { get; set; }
        public int ItemTypeId { get; set; }
        public int DopeTypeId { get; set; }
        public string GSTItemTypeName { get; set; }
        public string Name { get; set; }
        public string TradeName { get; set; }
        //public string CMarketName { get; set; }
        public string StoresitemCode { get; set; }
        public string AdditionalDescription { get; set; }
        //public string Description { get; set; }
        public int UnitId { get; set; }
        public string UnitOfMeasurementName { get; set; }
        public int HSNId { get; set; }
        public string HSNCode { get; set; }
        public string GSTCode { get; set; }
        public bool WasteItem { get; set; }
        public string IsWasteItem { get; set; }
        //public bool Recycleditem { get; set; }
        //public int AccountId { get; set; }
        //public int ProcessId { get; set; }
        //public string ProcessName { get; set; }
        //public int ItemTypeStructureId { get; set; }
        public int BusinessPartnerId { get; set; }
        public string BusinessPartnerName { get; set; }
        public bool PrintWindingType { get; set; }
        public string IsPrintWindingType { get; set; }
        public int ItemGroupId { get; set; }
        public string ItemGroupName { get; set; }
        public bool IsNotEditable { get; set; }
        //public bool IsExistCrossSectionInStructure { get; set; }
        //public bool IsExistLustreInStructure { get; set; }
        public int CrossSectionId { get; set; }
        public string CrossSection { get; set; }
        public int LustreId { get; set; }
        public string Lustre { get; set; }
        public int AccountId { get; set; }
        public int SubAccountId { get; set; }
        public string Twist { get; set; }
        public string TPM { get; set; }
        public int ProcessId { get; set; }              //get for Lots master
        public string ProcessName { get; set; }         //get for Lots master
        public string CName { get; set; }
    }
}
