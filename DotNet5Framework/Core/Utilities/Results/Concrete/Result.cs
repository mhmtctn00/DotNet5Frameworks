using Core.Utilities.Results.Abstract;
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
            ResultCode = statusCode;
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
        public int ResultCode { get; } = 0;
        public string Message { get; }


    }
}
