using Core.Utilities.Results.ComplexTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Results.Concrete
{
    public class SuccessResult : Result
    {
        public SuccessResult() : base(ResultStatus.Success)
        {
        }

        public SuccessResult(int resultCode) : base(ResultStatus.Success, resultCode)
        {
        }

        public SuccessResult(string message) : base(ResultStatus.Success, message)
        {
        }

        public SuccessResult(int resultCode, string message) : base(ResultStatus.Success, resultCode, message)
        {
        }
    }
}
