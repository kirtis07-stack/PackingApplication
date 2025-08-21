using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PackingApplication.Models.CommonEntities
{
    public static class SessionManager
    {
        public static string AuthToken { get; set; }
        public static string UserName { get; set; }
        public static string Role { get; set; }

        public static void Clear()
        {
            AuthToken = null;
            UserName = null;
            Role = null;
        }
    }
}
