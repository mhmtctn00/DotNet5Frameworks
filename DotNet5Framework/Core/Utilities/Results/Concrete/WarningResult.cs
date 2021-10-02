using Core.Utilities.Results.ComplexTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Results.Concrete
{
    public class WarningResult : Result
    {
        public WarningResult() : base(ResultStatus.Warning)
        {
        }

        public WarningResult(int statusCode) : base(ResultStatus.Warning, statusCode)
        {
        }

        public WarningResult(string message) : base(ResultStatus.Warning, message)
        {
        }
        public WarningResult(int statusCode, string message) : base(ResultStatus.Warning, statusCode, message)
        {
        }
    }
}
