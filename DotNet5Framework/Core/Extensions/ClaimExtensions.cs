using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Core.Extensions
{
    public static class ClaimExtensions
    {
        public static void AddEmail(this ICollection<Claim> claims, string email)
        {
            claims.Add(new Claim("email", email));
        }

        public static void AddFullName(this ICollection<Claim> claims, string fullname)
        {
            claims.Add(new Claim("fullname", fullname));
        }

        public static void AddFirstName(this ICollection<Claim> claims, string firstname)
        {
            claims.Add(new Claim("firstname", firstname));
        }

        public static void AddLastName(this ICollection<Claim> claims, string lastname)
        {
            claims.Add(new Claim("lastname", lastname));
        }
        public static void AddRememberMe(this ICollection<Claim> claims, string rememberMe)
        {
            claims.Add(new Claim("remember_me", rememberMe));
        }

        public static void AddNameIdentifier(this ICollection<Claim> claims, string nameIdentifier)
        {
            claims.Add(new Claim("name_identifier", nameIdentifier));
        }

        public static void AddRoles(this ICollection<Claim> claims, List<string> roles)
        {
            roles.ForEach(role => claims.Add(new Claim("roles", role)));
        }
    }
}
