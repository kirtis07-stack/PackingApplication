using PackingApplication.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PackingApplication.Models.CommonEntities
{
    public class BaseAuditEntity
    {
        public DateTime? EffectiveFrom { get; set; }
        public DateTime? EffectiveUpto { get; set; }
        public int CreatedBy { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public bool IsDisabled { get; set; }

        public BaseAuditEntity()
        {
            CreatedOn = DateTimeHelper.GetDateTime();
            UpdatedOn = DateTimeHelper.GetDateTime();
            IsDisabled = false;
        }
    }
}
