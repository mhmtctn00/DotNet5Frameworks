using Core.Utilities.HTTP;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Membership.Facebook
{
    public class FacebookHelper
    {
        const string verifyIdTokenUrl = "https://graph.facebook.com/me?fields=name,email&access_token=";
        public static FbTokenInfo GetTokenInfo(string token)
        {
            try
            {
                var tokenInfoStr = HttpHelper.Get($"{verifyIdTokenUrl}{token}");
                var tokenInfoObj = JsonConvert.DeserializeObject<FbTokenInfo>(tokenInfoStr);
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
                var tokenInfoStr = HttpHelper.Get($"{verifyIdTokenUrl}{token}");
                var tokenInfoObj = JsonConvert.DeserializeObject<FbTokenInfo>(tokenInfoStr);
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
