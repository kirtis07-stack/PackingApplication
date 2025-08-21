using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PackingApplication.Models.ResponseEntities
{
    public class QualityResponse
    {
        public int QualityId { get; set; }
        public short ItemTypeId { get; set; }
        public string ItemTypeName { get; set; }
        public string Name { get; set; }
        public string QualityCode { get; set; }
        public string ShortName { get; set; }
        public short QualityGroupId { get; set; }
        public string QualityGroupName { get; set; }
        public string CName { get; set; }
        public bool AutoDoffQuality { get; set; }
        public string IsAutoDoffQuality { get; set; }
    }
}
