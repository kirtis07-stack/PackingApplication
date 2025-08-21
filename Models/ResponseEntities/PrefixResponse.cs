using PackingApplication.Models.CommonEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PackingApplication.Models.ResponseEntities
{
    public class PrefixResponse : BaseAuditEntity
    {
        public int PrefixCode { get; set; }
        public string Prefix { get; set; }
        public int TransactionTypeId { get; set; }
        public string TransactionType { get; set; }
        public int FinYearId { get; set; }
        public string Year { get; set; }
        public string Month { get; set; }
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public short BankId { get; set; }
        public string BankName { get; set; }
        public string GenType { get; set; }
        public int DepartmentId { get; set; }
        public string Department { get; set; }
        public int MachineId { get; set; }
        public string MachineName { get; set; }
        public string VoucherNo { get; set; }
        public int VoucherFormat { get; set; }
        public string VoucherFormatType { get; set; }
        //public int TrCompanyId { get; set; }
        //public string TrCompanyName { get; set; }
        //public string Consignment { get; set; }
        //public short UserId { get; set; }
        //public string UserName { get; set; }
        public int ProductionTypeId { get; set; }
        public string ProductionType { get; set; }
        public int CompanyBranchId { get; set; }
        public string CompanyBranch { get; set; }
        public string TxnFlag { get; set; }
    }
}
