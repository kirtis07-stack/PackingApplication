using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PackingApplication.Models.RequestEntities
{
    public class TransactionTypePrefixRequest
    {
        public string Prefix { get; set; }
        public int CompanyId { get; set; }
        public int CompanyBranchId { get; set; }
        public int BankId { get; set; }
        public int DepartmentId { get; set; }
        public int MachineId { get; set; }
        public int ProductionTypeId { get; set; }
        public int TransactionTypeId { get; set; }
        public int FinYearId { get; set; }
        public DateTime? Date { get; set; } = null;
        public string TxnFlag { get; set; }
        public string SubString { get; set; } = null;
        [DefaultValue(false)]
        public bool? GetAllFlag { get; set; } = false;
    }
}
