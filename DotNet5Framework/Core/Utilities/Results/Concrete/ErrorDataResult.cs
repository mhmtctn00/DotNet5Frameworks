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
        public ErrorDataResult(T data, int resultCode) : base(data, ResultStatus.Error, resultCode)
        {
        }

        public ErrorDataResult(T data, string message) : base(data, ResultStatus.Error, message)
        {
        }

        public ErrorDataResult(T data, int resultCode, string message) : base(data, ResultStatus.Error, resultCode, message)
        {
        }
        public ErrorDataResult(int resultCode) : base(default, ResultStatus.Error, resultCode)
        {
        }
        public ErrorDataResult(string message) : base(default, ResultStatus.Error, message)
        {
        }
        public ErrorDataResult(int resultCode, string message) : base(default, ResultStatus.Error, resultCode, message)
        {
        }
        public ErrorDataResult() : base(default, ResultStatus.Error)
        {
        }
    }
}
