using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
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

        /// <summary>
        /// Make a post request with JSon data.
        /// </summary>
        /// <param name="url">Request url</param>
        /// <param name="data">Json string</param>
        /// <returns></returns>
        public static string Post(string url, string data)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = data;

                streamWriter.Write(json);
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                return result;
            }
        }
        /// <summary>
        /// Make a get request with JSon data.
        /// </summary>
        /// <param name="url">Request url</param>
        /// <returns></returns>
        public static string Get(string url)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "GET";

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                return result;
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
