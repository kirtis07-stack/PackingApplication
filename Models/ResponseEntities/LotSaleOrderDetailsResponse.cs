using PackingApplication.Models.CommonEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PackingApplication.Models.ResponseEntities
{
    public class LotSaleOrderDetailsResponse : BaseAuditEntity
    {
        public int LotSaleOrderDetailsId { get; set; }
        public long LotId { get; set; }
        public string LotType { get; set; }
        public int SaleOrderItemsId { get; set; }
        public string ItemName { get; set; }
        public string SaleOrderNumber { get; set; }
        public int QualityId { get; set; }
        public string QualityName { get; set; }
        public string QualityCode { get; set; }
        public int ShadeId { get; set; }
        public string ShadeName { get; set; }
        public string ShadeCode { get; set; }
        public decimal Quantity { get; set; }
    }
}
