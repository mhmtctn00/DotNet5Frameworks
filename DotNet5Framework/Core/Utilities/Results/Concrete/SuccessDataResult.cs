using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Results.Concrete
{
    public class SuccessDataResult<T> : DataResult<T>
    {
        public SuccessDataResult(T data) : base(data, true)
        {
        }
        public SuccessDataResult(T data, int statusCode) : base(data, true, statusCode)
        {
        }

        public SuccessDataResult(T data, string message) : base(data, true, message)
        {
        }

        public SuccessDataResult(T data, int statusCode, string message) : base(data, true, statusCode, message)
        {
        }
        public SuccessDataResult(int statusCode) : base(default, true, statusCode)
        {
        }
        public SuccessDataResult(string message) : base(default, true, message)
        {
        }
        public SuccessDataResult(int statusCode, string message) : base(default, true, statusCode, message)
        {
        }
        public SuccessDataResult() : base(default, true)
        {
        }
    }
}
