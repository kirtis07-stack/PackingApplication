using PackingApplication.Models.CommonEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PackingApplication.Models.RequestEntities
{
    public class ProductionRequest : BaseAuditEntity
    {
        public string PackingType { get; set; }
        public int DepartmentId { get; set; }
        public int MachineId { get; set; }
        public int LotId { get; set; }
        public int PrefixCode { get; set; }
        public DateTime ProductionDate { get; set; }
        public int QualityId { get; set; }
        public int SaleOrderId { get; set; }
        public int PackSizeId { get; set; }
        public int WindingTypeId { get; set; }
        public int TwistId { get; set; }
        public int ProdTypeId { get; set; }
        public int SpoolItemId { get; set; }
        public int BoxItemId { get; set; }
        public string Remarks { get; set; }
        public bool PrintCompany { get; set; }
        public bool PrintOwner { get; set; }
        public bool PrintDate { get; set; }
        public bool PrintUser { get; set; }
        public bool PrintHindiWords { get; set; }
        public bool PrintQRCode { get; set; }
        public bool PrintTwist { get; set; }
        public bool PrintWTPS { get; set; }
        public int Spools { get; set; }
        public decimal SpoolsWt { get; set; }
        public decimal EmptyBoxPalletWt { get; set; }
        public decimal GrossWt { get; set; }
        public decimal TareWt { get; set; }
        public decimal NetWt { get; set; }
        public int NoOfCopies { get; set; }
        public int ChallanId { get; set; }
        public List<ProductionPalletDetailsRequest> PalletDetailsRequest { get; set; }
        public List<ProductionConsumptionDetailsRequest> ConsumptionDetailsRequest { get; set; }
        public int DispatchChallanId { get; set; }
        public DateTime? DispatchDate { get; set; }
        public int SaleOrderItemsId { get; set; }
        public int PalletId { get; set; }
        public string SaleLot { get; set; }
        public int DispatchReserveId { get; set; }
        public int ItemId { get; set; }             //added for to insert in ProductionSummary and FinishedGoodsStock table
        public int ShadeId { get; set; }            //added for to insert in ProductionSummary and FinishedGoodsStock table
        public int ContainerTypeId { get; set; }    //added for to insert in ProductionSummary and FinishedGoodsStock table
        public int OwnerId { get; set; }
    }

    public class ProductionPalletDetailsRequest : BaseAuditEntity
    {
        public int PalletId { get; set; }
        public int Quantity { get; set; }
    }

    public class ProductionConsumptionDetailsRequest : BaseAuditEntity
    {
        public int ProductionLotId { get; set; }
        public int InputLotId { get; set; }
        public int InputItemId { get; set; }
        public int InputQualityId { get; set; }
        public decimal PropWeight { get; set; }
        public decimal PropProdGain { get; set; }
        public decimal PropProdLoss { get; set; }
        public decimal InputPerc { get; set; }
        public decimal ProductionPerc { get; set; }
        public decimal GainLossPerc { get; set; }
        public byte Extruder { get; set; }
        public long StockTrfDetailsId { get; set; }
    }

    public class ProductionPrintSlipRequest
    {
        public int SlipId { get; set; }
        public long ProductionId { get; set; }
        public int UserId { get; set; }
        public DateTime PrintDate { get; set; }
    }
}
