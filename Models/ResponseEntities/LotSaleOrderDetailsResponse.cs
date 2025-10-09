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
        public int LotId { get; set; }
        public string LotType { get; set; }
        public int SaleOrderItemsId { get; set; }
        public string SaleOrderNumber { get; set; }
        public string ItemName { get; set; }
        public decimal Quantity { get; set; }
    }
}
