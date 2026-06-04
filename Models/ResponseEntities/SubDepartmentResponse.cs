using PackingApplication.Models.CommonEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PackingApplication.Models.ResponseEntities
{
    public class SubDepartmentResponse: BaseAuditEntity
    {
        public int SubDepartmentId { get; set; }
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public string SubDepartmentName { get; set; }
        public string CName { get; set; }
        public string SubDepartmentCode { get; set; }
    }
}
