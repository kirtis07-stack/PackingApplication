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

    }
}
