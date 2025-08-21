using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PackingApplication.Models.ResponseEntities
{
    public class MachineResponse
    {
        public int MachineId { get; set; }
        public string MachineName { get; set; }
        public string MachineCode { get; set; }
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public int ProcessId { get; set; }
        public string ProcessName { get; set; }
        public int Spindled { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string CName { get; set; }
    }
}
