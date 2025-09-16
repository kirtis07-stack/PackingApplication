using PackingApplication.Models.CommonEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PackingApplication.Models.ResponseEntities
{
    public class SaleOrderResponse : BaseAuditEntity
    {
        public int SalesOrderId { get; set; }
        public int BranchId { get; set; }
        public string BranchName { get; set; }
        public string SalesOrderNumber { get; set; }
        public DateTime SalesOrderDate { get; set; }
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }
        public int ConsigneeId { get; set; }
        public string ConsigneeName { get; set; }
        public string ConsigneeAddress { get; set; }
        public int AgentId { get; set; }
        public string AgentName { get; set; }
        public string AgentAddress { get; set; }
        public int DespatchTo { get; set; }
        public string CityName { get; set; }
        public int SaleOrderTypeId { get; set; }
        public string SaleOrderType { get; set; }
        public string Narration { get; set; }
        public string Remarks { get; set; }
        public string POReferenceNumber { get; set; }
        public DateTime PODate { get; set; }
        public int CustomerDetailsId { get; set; }
        public string CustomerDetails { get; set; }
        public int AgentDetailsId { get; set; }
        public string AgentDetails { get; set; }
        public int ConsigneeDetailsId { get; set; }
        public string ConsigneeDetails { get; set; }
        public string PaymentType { get; set; }
        public decimal AdvAmount { get; set; }
        public DateTime? AdvDate { get; set; }
        public int LCId { get; set; }
        public string LCName { get; set; }
        public int SaleCommitmentId { get; set; }
        public List<SaleOrderItemsResponse> saleOrderItemsResponses { get; set; }

    }

    public class SaleOrderItemsResponse : BaseAuditEntity
    {
        public int SaleOrderItemsId { get; set; }
        public int SaleOrderId { get; set; }
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public short ItemTypeId { get; set; }
        public string ItemTypeName { get; set; }
        public int QualityId { get; set; }
        public string QualityName { get; set; }
        public int ShadeId { get; set; }
        public string ShadeName { get; set; }
        public int SalesCommitmentNumber { get; set; }
        public string TradeName { get; set; }
        public string LotNo { get; set; }
        public bool PrintLotNo { get; set; }
        public string IsPrintLotNo { get; set; }
        public decimal Quantity { get; set; }
        public string POReferenceNumber { get; set; }
        public DateTime PODate { get; set; }
        //public int StatusId { get; set; }
        public string Status { get; set; }
        public decimal SCRate { get; set; }
        public decimal SORate { get; set; }
        public decimal InitialCommissionPercentage { get; set; }
        public decimal NetRate { get; set; }
        public decimal BalanceCommissionPercentage { get; set; }
        public decimal DharaRate { get; set; }
        public decimal PackageWeightPerCop { get; set; }
        public decimal BillRate { get; set; }
        public int DeliveryDays { get; set; }
        public DateTime DeliveryStartDate { get; set; }
        public DateTime DeliveryEndDate { get; set; }
        public int BusinessVerticalId { get; set; }
        public string BPVerticalName { get; set; }
        public int WindingTypeId { get; set; }
        public string WindingTypeName { get; set; }
        public int PackSizeId { get; set; }
        public string PackSizeName { get; set; }
        public int PackingTypeId { get; set; }
        public string PackingSpecifications { get; set; }
        public int ContainerTypeId { get; set; }
        public string ContainerTypeName { get; set; }
        public int NumberOfCreels { get; set; }
        public int BobbinsPerCreel { get; set; }
        public bool HeatSet { get; set; }
        public string IsHeatSet { get; set; }
        public string EndUse { get; set; }
        public string ShadeMatchingType { get; set; }
        public decimal ProdTolerancePercentage { get; set; }
        public decimal TolerancePercentage { get; set; }
        public string ItemRemark { get; set; }
        public string ItemDescription { get; set; }
        public string ShadeCodeDescription { get; set; }
        public string ShadeNameDescription { get; set; }
        public decimal ItemAmount { get; set; }
        public int NoOfBobbins { get; set; }
        public decimal GSTAmount { get; set; }
        public string CommitmentNumberFrmt { get; set; }        //added for convert sc to so
        public int SCItemId { get; set; }
        public DateTime CommitmentDate { get; set; }
        public string AgentName { get; set; }
        public string CustomerName { get; set; }
        public string ShadeCode { get; set; }
        public decimal RemainingQuantity { get; set; }
        public string QualityCode { get; set; }
        public int AgentId { get; set; }            // added for show pending sc in so
        public int AgentDetailsId { get; set; }     // added for show pending sc in so
    }
}
