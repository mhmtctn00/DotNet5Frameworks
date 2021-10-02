using Core.Utilities.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Membership.Google
{
    public class GoogleHelper
    {
        const string verifyIdTokenUrl = "https://oauth2.googleapis.com/tokeninfo?id_token=";
        public static GoogleTokenInfo GetTokenInfo(string token)
        {
            try
            {
                var tokenInfoStr = HttpHelper.HttpGetRequest($"{verifyIdTokenUrl}{token}");
                var tokenInfoObj = JsonConvert.DeserializeObject<GoogleTokenInfo>(tokenInfoStr);
                return tokenInfoObj;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public static bool CheckEmail(string token, string email)
        {
            try
            {
                var tokenInfoStr = HttpHelper.HttpGetRequest($"{verifyIdTokenUrl}{token}");
                var tokenInfoObj = JsonConvert.DeserializeObject<GoogleTokenInfo>(tokenInfoStr);
                if (!tokenInfoObj.Email.Equals(email))
                    return false;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
