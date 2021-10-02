using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using System.Net;

namespace Core.Utilities.Helpers
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

        public static string HttpGetRequest(string url, string token = null)
        {
            //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);

            httpWebRequest.Method = "GET";
            httpWebRequest.ContentType = "application/json";
            if (!String.IsNullOrEmpty(token))
                httpWebRequest.Headers.Add("Authorization", $"Bearer {token}");

            //if (data != null)
            //    using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            //    {
            //        string json = JsonConvert.SerializeObject(data);

            //        streamWriter.Write(json);
            //    }

            try
            {
                using (HttpWebResponse response = (HttpWebResponse)httpWebRequest.GetResponse())
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }

        }
        public static string HttpPostRequest(string url, object data, string token = null)
        {
            try
            {
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);

                httpWebRequest.Method = "POST";
                httpWebRequest.ContentType = "application/json";

                if (!string.IsNullOrEmpty(token))
                    httpWebRequest.Headers.Add("Authorization", $"Bearer {token}");

                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    string json = JsonConvert.SerializeObject(data);

                    streamWriter.Write(json);
                }

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    return streamReader.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }

        }
        public static string HttpPutRequest(string url, object data, string token = null)
        {
            try
            {
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);

                httpWebRequest.Method = "PUT";
                httpWebRequest.ContentType = "application/json";

                if (token != null)
                    httpWebRequest.Headers.Add("Authorization", $"Bearer {token}");

                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    string json = JsonConvert.SerializeObject(data);

                    streamWriter.Write(json);
                }

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    return streamReader.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }
        public static string HttpDeleteRequest(string url, object data, string token = null)
        {
            try
            {
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);

                httpWebRequest.Method = "DELETE";
                httpWebRequest.ContentType = "application/json";

                if (token != null)
                    httpWebRequest.Headers.Add("Authorization", $"Bearer {token}");

                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    string json = JsonConvert.SerializeObject(data);

                    streamWriter.Write(json);
                }

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    return streamReader.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
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
