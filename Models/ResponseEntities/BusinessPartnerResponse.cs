using PackingApplication.Models.CommonEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PackingApplication.Models.ResponseEntities
{
    public class BusinessPartnerResponse : BaseAuditEntity
    {
        public int BusinessPartnerId { get; set; }
        public string LegalName { get; set; }
        public string PrintName { get; set; }
        public string CName { get; set; }
        public string BrokerGroup { get; set; }
        public int BrokerGroupBPId { get; set; }
        public string BPCode { get; set; }
        public string InterCompany { get; set; }
        public bool IsInterCompany { get; set; }
        public string ContactPerson { get; set; }
        public string ContactPersonNo { get; set; }
        public int DesignationId { get; set; }
        public string DesignationName { get; set; }
        public string BPType { get; set; }
        public string BPTypeNames { get; set; }
        public int AssesseeCategoryId { get; set; }
        public string AssesseeCategoryName { get; set; }
        public string PAN { get; set; }
        public string TAN { get; set; }
        public string CIN { get; set; }
        public string WebSite { get; set; }
        public string UIDNO { get; set; }
        public string PaymentType { get; set; }
        public decimal PaymentPerc { get; set; }
        public decimal CreditLimitAmount { get; set; }
        public int CreditLimitDays { get; set; }
        public int BusinessCategoryId { get; set; }
        public string BusinessCategoryName { get; set; }
        public string Address { get; set; }
        public string GSTRegNo { get; set; }
        public int BPDetailsId { get; set; }
        public string BPVertical { get; set; }
        public string BPVerticalNames { get; set; }
        public string CityName { get; set; }
        public int CityId { get; set; }
        public string TaxId { get; set; }
        public string ShortName { get; set; }
    }
}
