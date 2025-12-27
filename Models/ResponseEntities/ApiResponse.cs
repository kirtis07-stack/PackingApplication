using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PackingApplication.Models.ResponseEntities
{
    public class ApiResponse
    {
        public int StatusCode { get; set; }
        public string ResponseBody { get; set; }
    }

    public class ApiErrorResponse
    {
        public string Message { get; set; }
    }
}
