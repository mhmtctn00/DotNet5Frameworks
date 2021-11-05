using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Security.Authorization.JWT
{
    public class Jwt
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool RememberMe { get; set; }
        public List<string> Roles { get; set; }
        public DateTime ExpireDate { get; set; }
    }
}
