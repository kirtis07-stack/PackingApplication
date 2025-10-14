using PackingApplication.Models.CommonEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PackingApplication.Models.ResponseEntities
{
    public class DepartmentResponse: BaseAuditEntity
    {
        public int DepartmentId { get; set; }
        public int PlantId { get; set; }
        public string PlantName { get; set; }
        public int DepartmentTypeId { get; set; }
        public string DepartmentTypeName { get; set; }
        public string DepartmentName { get; set; }
        public string CName { get; set; }
        public string DepartmentCode { get; set; }
    }
}
