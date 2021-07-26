using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.HTTP
{
    public static class HttpHelper
    {
        private static IHttpContextAccessor _httpContextAccessor;
        private static readonly object _lock = new object();

        static void Init()
        {
            if (_httpContextAccessor == null)
            {
                lock (_lock)
                {
                    _httpContextAccessor = new HttpContextAccessor();
                }
            }
        }
        public static string GetIpAddress()
        {//Need to test without localhost.
            Init();
            var ip = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress;

            return ip.ToString();
        }
        public static int GetCurrentUserId()
        {//Need to test without localhost.
            Init();
            var id = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "name_identifier").Value;

            return Convert.ToInt32(id);
        }
    }
}
