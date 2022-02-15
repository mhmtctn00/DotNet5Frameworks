using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Results.Concrete
{
    public class SuccessResult : Result
    {
        public SuccessResult() : base(true)
        {
        }

        public SuccessResult(int statusCode) : base(true, statusCode)
        {
        }

        public SuccessResult(string message) : base(true, message)
        {
        }

        public SuccessResult(int statusCode, string message) : base(true, statusCode, message)
        {
        }
    }
}
