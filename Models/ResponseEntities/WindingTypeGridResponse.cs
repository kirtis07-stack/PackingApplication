using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PackingApplication.Models.ResponseEntities
{
    public class WindingTypeGridResponse
    {
        public int WindingTypeId { get; set; }
        public string WindingTypeName { get; set; }
        public int SaleOrderItemsId { get; set; }
        public decimal WindingQty { get; set; } 
        public decimal NetWt { get; set; }

        public decimal BalanceQty
        {
            get { return WindingQty - NetWt; }
        }
    }

    public class QualityGridResponse
    {
        public int QualityId { get; set; }
        public string QualityName { get; set; }
        public int SaleOrderItemsId { get; set; }
        public decimal SaleOrderQty { get; set; }
        public decimal NetWt { get; set; }
    }
}
