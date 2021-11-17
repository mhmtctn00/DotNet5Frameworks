using Core.Utilities.Results.ComplexTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Results.Concrete
{
    public class InfoDataResult<T> : DataResult<T>
    {
        public InfoDataResult(T data) : base(data, ResultStatus.Info)
        {
        }
        public InfoDataResult(T data, int resultCode) : base(data, ResultStatus.Info, resultCode)
        {
        }

        public InfoDataResult(T data, string message) : base(data, ResultStatus.Info, message)
        {
        }
        public InfoDataResult(T data, int resultCode, string message) : base(data, ResultStatus.Info, resultCode, message)
        {
        }
        public InfoDataResult(int resultCode) : base(default, ResultStatus.Info, resultCode)
        {
        }
        public InfoDataResult(string message) : base(default, ResultStatus.Info, message)
        {
        }
        public InfoDataResult(int resultCode, string message) : base(default, ResultStatus.Info, resultCode, message)
        {
        }
        public InfoDataResult() : base(default, ResultStatus.Info)
        {
        }
    }
}
