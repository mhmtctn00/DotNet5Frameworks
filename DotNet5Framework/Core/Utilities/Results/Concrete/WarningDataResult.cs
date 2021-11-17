using Core.Utilities.Results.ComplexTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Results.Concrete
{
    public class WarningDataResult<T> : DataResult<T>
    {
        public WarningDataResult(T data) : base(data, ResultStatus.Warning)
        {
        }
        public WarningDataResult(T data, int resultCode) : base(data, ResultStatus.Warning, resultCode)
        {
        }

        public WarningDataResult(T data, string message) : base(data, ResultStatus.Warning, message)
        {
        }
        public WarningDataResult(T data, int resultCode, string message) : base(data, ResultStatus.Warning, resultCode, message)
        {
        }
        public WarningDataResult(int resultCode) : base(default, ResultStatus.Warning, resultCode)
        {
        }
        public WarningDataResult(string message) : base(default, ResultStatus.Warning, message)
        {
        }
        public WarningDataResult(int resultCode, string message) : base(default, ResultStatus.Warning, resultCode, message)
        {
        }
        public WarningDataResult() : base(default, ResultStatus.Warning)
        {
        }
    }
}
