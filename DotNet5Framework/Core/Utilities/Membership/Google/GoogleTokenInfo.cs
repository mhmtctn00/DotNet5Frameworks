using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Membership.Google
{
    public class GoogleTokenInfo
    {
        public string Iss { get; set; }
        public string Azp { get; set; }
        public string Aud { get; set; }
        public string Sub { get; set; }
        public string Email { get; set; }
        [JsonProperty("Email_verified")]
        public string EmailVerified { get; set; }
        [JsonProperty("at_hash")]
        public string AtHash { get; set; }
        public string Name { get; set; }
        public string Picture { get; set; }
        [JsonProperty("given_name")]
        public string GivenName { get; set; }
        [JsonProperty("family_name")]
        public string FamilyName { get; set; }
        public string Locale { get; set; }
        public string Iat { get; set; }
        public string Exp { get; set; }
        public string Jti { get; set; }
        public string Alg { get; set; }
        public string Kid { get; set; }
        [JsonProperty("typ")]
        public string Type { get; set; }
        public string Error { get; set; }
    }
}
