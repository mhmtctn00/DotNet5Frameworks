using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Membership.Facebook
{
    public class FbTokenInfo
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public FbErrorDetails Error { get; set; } = new FbErrorDetails();
    }
    public class FbErrorDetails
    {
        public string Message { get; set; }
        public string Type { get; set; }
        public int Code { get; set; }
        [JsonProperty("fbtrace_id")]
        public string FbTraceId { get; set; }
    }
}
