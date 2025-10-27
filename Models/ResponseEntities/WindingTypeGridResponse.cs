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
        public int SaleOrderItemId { get; set; }
        public decimal SaleOrderQty { get; set; } 
        public decimal GrossWt { get; set; }

        public decimal BalanceQty
        {
            get { return SaleOrderQty - GrossWt; }
        }
    }

    public class QualityGridResponse
    {
        public int QualityId { get; set; }
        public string QualityName { get; set; }
        public int SaleOrderItemId { get; set; }
        public decimal SaleOrderQty { get; set; }
        public decimal GrossWt { get; set; }
    }
}
