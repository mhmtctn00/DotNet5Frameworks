using Core.Utilities.Results.Abstract;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Results.Concrete
{
    public class Result : IResult
    {
        public Result(bool status)
        {
            Status = status;
        }
        public Result(bool status, int statusCode)
        {
            Status = status;
            StatusCode = statusCode;
        }
        public Result(bool status, string message) : this(status)
        {
            Message = message;
        }
        public Result(bool status, int statusCode, string message) : this(status, statusCode)
        {
            Message = message;
        }

        public bool Status { get; }
        public int StatusCode { get; }
        public string Message { get; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
