using Core.Utilities.Results.ComplexTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Results.Concrete
{
    public class ErrorResult : Result
    {
        public ErrorResult() : base(ResultStatus.Error)
        {
        }

        public ErrorResult(int resultCode) : base(ResultStatus.Error, resultCode)
        {
        }

        public ErrorResult(string message) : base(ResultStatus.Error, message)
        {
        }

        public ErrorResult(int resultCode, string message) : base(ResultStatus.Error, resultCode, message)
        {
        }
    }
}
