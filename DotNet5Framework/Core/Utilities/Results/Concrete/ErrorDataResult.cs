using Core.Utilities.Results.ComplexTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Results.Concrete
{
    public class ErrorDataResult<T> : DataResult<T>
    {
        public ErrorDataResult(T data) : base(data, ResultStatus.Error)
        {
        }
        public ErrorDataResult(T data, int statusCode) : base(data, ResultStatus.Error, statusCode)
        {
        }

        public ErrorDataResult(T data, string message) : base(data, ResultStatus.Error, message)
        {
        }

        public ErrorDataResult(T data, int statusCode, string message) : base(data, ResultStatus.Error, statusCode, message)
        {
        }
        public ErrorDataResult(int statusCode) : base(default, ResultStatus.Error, statusCode)
        {
        }
        public ErrorDataResult(string message) : base(default, ResultStatus.Error, message)
        {
        }
        public ErrorDataResult(int statusCode, string message) : base(default, ResultStatus.Error, statusCode, message)
        {
        }
        public ErrorDataResult() : base(default, ResultStatus.Error)
        {
        }
    }
}
