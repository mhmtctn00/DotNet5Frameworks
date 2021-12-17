using Core.Entities.Concrete;
using Core.Extensions;
using Core.Utilities.Security.Encyption;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Claims;

namespace Core.Utilities.Security.Authorization.JWT
{
    public class JwtHelper : ITokenHelper
    {
        public IConfiguration Configuration { get; }
        private TokenOptions _tokenOptions;
        private DateTime _accessTokenExpiration;
        public JwtHelper(IConfiguration configuration)
        {
            Configuration = configuration;
            _tokenOptions = Configuration.GetSection("TokenOptions").Get<TokenOptions>();
            _accessTokenExpiration = DateTime.Now.AddMinutes(_tokenOptions.AccessTokenExpiration);
        }
        public AccessToken CreateToken(User user, List<Role> roles, bool refreshToken = true)
        {
            _accessTokenExpiration = DateTime.Now.AddMinutes(_tokenOptions.AccessTokenExpiration);
            var securityKey = SecurityKeyHelper.CreateSecurityKey(_tokenOptions.SecurityKey);
            var signingCredentials = SigningCredentialsHelper.CreateSigningCredentials(securityKey);
            var jwt = CreateJwtSecurityToken(_tokenOptions, user, signingCredentials, roles);
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var token = jwtSecurityTokenHandler.WriteToken(jwt);

            if (refreshToken)
                user.Session = GenerateRefreshToken();
            user.SessionExpireDate = _accessTokenExpiration.AddMinutes(10);

            return new AccessToken
            {
                Token = token,
                Expiration = _accessTokenExpiration
            };
        }
        public JwtSecurityToken CreateJwtSecurityToken(TokenOptions tokenOptions, User user,
            SigningCredentials signingCredentials, List<Role> roles)
        {
            var jwt = new JwtSecurityToken(
                issuer: tokenOptions.Issuer,
                audience: tokenOptions.Audience,
                expires: _accessTokenExpiration,
                notBefore: DateTime.Now, //tokenın expires bilgisi şuandan önce mi ?
                claims: SetClaims(user, roles),
                signingCredentials: signingCredentials
            );
            return jwt;
        }

        private IEnumerable<Claim> SetClaims(User user, List<Role> roles)
        {
            var claims = new List<Claim>();
            claims.AddNameIdentifier(user.Id.ToString());
            claims.AddEmail(user.Email);
            claims.AddFullName($"{user.FullName}");
            claims.AddRoles(roles.Select(c => c.RoleName).ToList());

            return claims;
        }


        public static Jwt GetJwt(string token)
        {
            token = token.Replace("Bearer", "").Trim();
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            var configuration = new ConfigurationBuilder()
                 .SetBasePath(Directory.GetCurrentDirectory())
                 .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                 .Build();
            var tokenOptions = configuration.GetSection("TokenOptions").Get<TokenOptions>();
            var TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidIssuer = tokenOptions.Issuer,
                ValidAudience = tokenOptions.Audience,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = SecurityKeyHelper.CreateSecurityKey(tokenOptions.SecurityKey)
            };
            var jsonToken = handler.ReadToken(token);
            var user = jsonToken as JwtSecurityToken;

            var jwt = new Jwt();

            jwt.UserId = Convert.ToInt32(user.Claims.FirstOrDefault(c => c.Type.Equals("name_identifier"))?.Value);
            jwt.Email = user.Claims.FirstOrDefault(c => c.Type.Equals("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress"))?.Value.ToString();
            jwt.FirstName = user.Claims.FirstOrDefault(c => c.Type.Equals("firstname"))?.Value.ToString();
            jwt.LastName = user.Claims.FirstOrDefault(c => c.Type.Equals("lastname"))?.Value.ToString();
            jwt.Roles = user.Claims.Where(c => c.Type.Contains("role")).Select(c => c.Value.ToString().Split("/")[^1]).ToList();
            jwt.RememberMe = Convert.ToBoolean(user.Claims.FirstOrDefault(c => c.Type.Equals("remember_me"))?.Value);
            jwt.ExpireDate = DateTimeOffset.FromUnixTimeSeconds(Convert.ToInt64(user.Claims.FirstOrDefault(c => c.Type.Equals("exp"))?.Value.ToString())).DateTime;


            return jwt;
        }
        public static bool VerifyJWT(string token)
        {
            token = token.Replace("Bearer", "").Trim();
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            var configuration = new ConfigurationBuilder()
                 .SetBasePath(Directory.GetCurrentDirectory())
                 .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                 .Build();
            var tokenOptions = configuration.GetSection("TokenOptions").Get<TokenOptions>();
            var TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidIssuer = tokenOptions.Issuer,
                ValidAudience = tokenOptions.Audience,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = SecurityKeyHelper.CreateSecurityKey(tokenOptions.SecurityKey)
            };
            try
            {
                var user = handler.ValidateToken(token, TokenValidationParameters, out SecurityToken validatedToken);
                if (string.IsNullOrEmpty(user.Claims.FirstOrDefault(c => c.Type == "name_identifier")?.Value))
                    return false;
                return true;
            }
            catch (System.Exception)
            {
                return false;
            }
        }
        private static List<AccessToken> closedTokens = new List<AccessToken>();
        public static bool IsClosed(string tokenStr) // true: closed, false: active
        {
            closedTokens.RemoveAll(x => x.Expiration < DateTime.UtcNow);
            return closedTokens.Any(x => x.Token == tokenStr);
        }
        public static void CloseToken(string token)
        {
            var tokenObj = GetJwt(token);
            closedTokens.Add(new AccessToken
            {
                Token = token,
                Expiration = tokenObj.ExpireDate
            });
        }

        private string GenerateRefreshToken()
        {
            return Guid.NewGuid().ToString("N") + Guid.NewGuid().ToString("N");
        }
    }
}
