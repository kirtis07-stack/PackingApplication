using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PackingApplication.Models.ResponseEntities
{
    public class UserResponse
    {
        public int UserId { get; set; }
        public string GUID { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public byte RoleId { get; set; }
        public string Role { get; set; }
        public DateTime LastLogin { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public string Username { get; set; }
    }
}
