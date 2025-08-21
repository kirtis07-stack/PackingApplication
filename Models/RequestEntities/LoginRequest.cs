using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PackingApplication.Models.RequestEntities
{
    public class LoginRequest
    {
        public string Email { set; get; }
        public string PasswordHash { set; get; }
        public bool IsRemember { get; set; }
    }
}
