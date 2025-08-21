using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PackingApplication.Models.ResponseEntities
{
    public class PackSizeResponse
    {
        public int PackSizeId { get; set; }
        public string PackSizeName { get; set; }
        public string ShortCode { get; set; }
        public short MainItemTypeId { get; set; }
        public string MainItemTypeName { get; set; }
        public decimal StartWeight { get; set; }
        public decimal EndWeight { get; set; }
        public string CName { get; set; }
        public int QualityId { get; set; }
        public string Quality { get; set; }
        public short FromDenier { get; set; }
        public short UpToDenier { get; set; }
        public bool ConveyorPackSize { get; set; }
        public string IsConveyorPackSize { get; set; }
        public bool EvenPackSize { get; set; }
        public string IsEvenPackSize { get; set; }
    }
}
