using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.ComplexTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Results.Concrete
{
    public class Result : IResult
    {
        public Result(ResultStatus status)
        {
            Status = status;
        }
        public Result(ResultStatus status, int resultCode)
        {
            Status = status;
            ResultCode = resultCode;
        }
        public Result(ResultStatus status, string message) : this(status)
        {
            Message = message;
        }
        public Result(ResultStatus status, int resultCode, string message) : this(status, resultCode)
        {
            Message = message;
        }

        public ResultStatus Status { get; }
        public int ResultCode { get; } = 0;
        public string Message { get; }


    }
}
