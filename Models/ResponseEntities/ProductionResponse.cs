using PackingApplication.Models.CommonEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PackingApplication.Models.ResponseEntities
{
    //public class ProductionResponse : BaseAuditEntity
    //{
    //    public long ProductionId { get; set; }
    //    public string PackingType { get; set; }
    //    public int DepartmentId { get; set; }
    //    public string DepartmentName { get; set; }
    //    public int MachineId { get; set; }
    //    public string MachineName { get; set; }
    //    public int PrefixCode { get; set; }
    //    public int LotId { get; set; }
    //    public string LotNo { get; set; }
    //    public string BoxPrefix { get; set; }
    //    public string BoxNo { get; set; }
    //    public int BoxYearId { get; set; }
    //    public string FinYear { get; set; }
    //    public string BoxMonth { get; set; }
    //    public string BoxNoFmtd { get; set; }
    //    public DateTime ProductionDate { get; set; }
    //    public int QualityId { get; set; }
    //    public string QualityName { get; set; }
    //    public int SaleOrderItemsId { get; set; }
    //    public string SalesOrderNumber { get; set; }
    //    public int PackSizeId { get; set; }
    //    public string PackSizeName { get; set; }
    //    public int WindingTypeId { get; set; }
    //    public string WindingTypeName { get; set; }
    //    public int TwistId { get; set; }
    //    public int ProdTypeId { get; set; }
    //    public string ProductionType { get; set; }
    //    public int SpoolItemId { get; set; }
    //    public string SpoolItemName { get; set; }
    //    public int BoxItemId { get; set; }
    //    public string BoxItemName { get; set; }
    //    public string Remarks { get; set; }
    //    public bool PrintCompany { get; set; }
    //    public bool PrintOwner { get; set; }
    //    public bool PrintDate { get; set; }
    //    public bool PrintUser { get; set; }
    //    public bool PrintHindiWords { get; set; }
    //    public bool PrintQRCode { get; set; }
    //    public bool PrintTwist { get; set; }
    //    public bool PrintWTPS { get; set; }
    //    public int Spools { get; set; }
    //    public decimal SpoolsWt { get; set; }
    //    public decimal EmptyBoxPalletWt { get; set; }
    //    public decimal GrossWt { get; set; }
    //    public decimal TareWt { get; set; }
    //    public decimal NetWt { get; set; }
    //    public int NoOfCopies { get; set; }
    //    public int ChallanId { get; set; }
    //    public List<ProductionPalletDetailsResponse> PalletDetailsResponse { get; set; }
    //    public List<ProductionConsumptionDetailsResponse> ConsumptionDetailsResponse { get; set; }
    //    public int SrNo { get; set; }
    //    public int OwnerId { get; set; }
    //    public string OwnerName { get; set; }
    //    public int ContainerTypeId { get; set; }
    //    public string ContainerType { get; set; }
    //}

    public class ProductionPalletDetailsResponse : BaseAuditEntity
    {
        public int ProductionPalletId { get; set; }
        public long ProductionId { get; set; }
        public string PackingType { get; set; }
        public int PalletId { get; set; }
        public string ItemName { get; set; }
        public int Quantity { get; set; }
    }

    public class ProductionConsumptionDetailsResponse : BaseAuditEntity
    {
        public int ProductionConsumptionId { get; set; }
        public long ProductionId { get; set; }
        public string PackingType { get; set; }
        public int ProductionLotId { get; set; }
        public string ProductionLot { get; set; }
        public int InputLotId { get; set; }
        public string InputLot { get; set; }
        public int InputItemId { get; set; }
        public string InputItemName { get; set; }
        public int InputQualityId { get; set; }
        public string InputQualityName { get; set; }
        public decimal PropWeight { get; set; }
        public decimal PropProdGain { get; set; }
        public decimal PropProdLoss { get; set; }
        public decimal InputPerc { get; set; }
        public decimal ProductionPerc { get; set; }
        public decimal GainLossPerc { get; set; }
        public byte Extruder { get; set; }
        public long StockTrfDetailsId { get; set; }
        public string UpdOrder { get; set; }
    }

    public class ProductionResponse : BaseAuditEntity
    {
        public long ProductionId { get; set; }
        public string PackingType { get; set; }
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public int MachineId { get; set; }
        public string MachineName { get; set; }
        public int PrefixCode { get; set; }
        public int LotId { get; set; }
        public string LotNo { get; set; }
        public string BoxPrefix { get; set; }
        public string BoxNo { get; set; }
        public int BoxYearId { get; set; }
        public string FinYear { get; set; }
        public string BoxMonth { get; set; }
        public string BoxNoFmtd { get; set; }
        public DateTime ProductionDate { get; set; }
        public int QualityId { get; set; }
        public string QualityName { get; set; }
        //public int SaleOrderId { get; set; }
        public string SalesOrderNumber { get; set; }
        public int PackSizeId { get; set; }
        public string PackSizeName { get; set; }
        public int WindingTypeId { get; set; }
        public string WindingTypeName { get; set; }
        public int TwistId { get; set; }
        public int ProdTypeId { get; set; }
        public string ProductionType { get; set; }
        public int SpoolItemId { get; set; }
        public string SpoolItemName { get; set; }
        public int BoxItemId { get; set; }
        public string BoxItemName { get; set; }
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
        //public int NoOfCopies { get; set; }
        //public int ChallanId { get; set; }
        public List<ProductionPalletDetailsResponse> PalletDetailsResponse { get; set; }
        public List<LotsDetailsResponse> LotsDetailsResponse { get; set; }
        public int DispatchChallanId { get; set; }
        public DateTime? DispatchDate { get; set; }
        public int SaleOrderItemsId { get; set; }
        public int PalletId { get; set; }
        public string SaleLot { get; set; }
        public int DispatchReserveId { get; set; }
        public int OwnerId { get; set; }
        public string OwnerName { get; set; }
        public int ContainerTypeId { get; set; }
        public string ContainerType { get; set; }
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public int ShadeId { get; set; }
        public string ShadeName { get; set; }
        public string ShadeCode { get; set; }
        public int Denier { get; set; }
        public string TwistName { get; set; }
        public short FromDenier { get; set; }
        public short UpToDenier { get; set; }
        public decimal StartWeight { get; set; }
        public decimal EndWeight { get; set; }
        public string SOItemDescription { get; set; }
        public string SOShadeNameDescription { get; set; }
        public string SOShadeCodeDescription { get; set; }
        public decimal SOQuantity { get; set; }
        public decimal CopsItemWeight { get; set; }
        public decimal BoxItemWeight { get; set; }
        public int SerialNo { get; set; }
        public string SOItemName { get; set; }
        public bool CanModifyDelete { get; set; } = false;
        public short ItemTypeId { get; set; }
        public short MainItemTypeId { get; set; }
        public string LastBox { get; set; }
        public int LastBoxSpools { get; set; }
        public decimal LastBoxGrossWt { get; set; }
        public decimal LastBoxTareWt { get; set; }
        public decimal LastBoxNetWt { get; set; }
        public int BPDetailsId { get; set; }
        public string BPAddress { get; set; }
    }
}
