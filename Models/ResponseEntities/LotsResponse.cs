using PackingApplication.Models.CommonEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PackingApplication.Models.ResponseEntities
{
    public class LotsResponse
    {
        public int LotId { get; set; }
        public string LotType { get; set; }
        public int BPId { get; set; }
        public string BusinessPartner { get; set; }
        public string BPPrintName { get; set; }
        public string LotNo { get; set; }
        public int PrefixId { get; set; }
        public string Prefix { get; set; }
        public int FinYearId { get; set; }
        public string FinYear { get; set; }
        public int MachineId { get; set; }
        public string MachineName { get; set; }
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public int ShadeId { get; set; }
        public string ShadeName { get; set; }
        public string ShadeCode { get; set; }
        public int WindingTypeId { get; set; }
        public string WindingTypeName { get; set; }
        public int TwistId { get; set; }
        public string TwistName { get; set; }
        public string SaleLot { get; set; }
        public string MISLot { get; set; }
        public bool AllowLabelPrinting { get; set; }
        public string IsAllowLabelPrinting { get; set; }
        public bool ConeLabelPrinting { get; set; }
        public string IsConeLabelPrinting { get; set; }
        public decimal LDRPerc { get; set; }
        public decimal OilPickupPerc { get; set; }
        public int Denier { get; set; }
        public int Filament { get; set; }
        public string ProductionFor { get; set; }
        public string Nips { get; set; }
        public string Remarks { get; set; }
        public int Speed { get; set; }
        public string LabelDescription { get; set; }
        public int ProcessId { get; set; }
        public string ProcessName { get; set; }
        public List<LotsProductionDetailsResponse> LotsProductionDetailsResponses { get; set; }
    }

    public class LotsProductionDetailsResponse : BaseAuditEntity
    {
        public int LotProdDetailsId { get; set; }
        public int LotId { get; set; }
        public string LotType { get; set; }
        public int WindingTypeId { get; set; }
        public string WindingTypeName { get; set; }
        public int Quantity { get; set; }
    }
}
