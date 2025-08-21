using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PackingApplication.Models.ResponseEntities
{
    public class WindingTypeResponse
    {
        public int WindingTypeId { get; set; }
        public string WindingTypeName { get; set; }
        public string ShortCode { get; set; }
        public string CName { get; set; }
    }
}
