using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Results.Concrete
{
    public class ErrorDataResult<T> : DataResult<T>
    {
        public ErrorDataResult(T data) : base(data, false)
        {
        }
        public ErrorDataResult(T data, int statusCode) : base(data, false, statusCode)
        {
        }

        public ErrorDataResult(T data, string message) : base(data, false, message)
        {
        }

        public ErrorDataResult(T data, int statusCode, string message) : base(data, false, statusCode, message)
        {
        }
        public ErrorDataResult(int statusCode) : base(default, false, statusCode)
        {
        }
        public ErrorDataResult(string message) : base(default, false, message)
        {
        }
        public ErrorDataResult(int statusCode, string message) : base(default, false, statusCode, message)
        {
        }
        public ErrorDataResult() : base(default, false)
        {
        }
    }
}
