using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Results.Concrete
{
    public class ErrorResult : Result
    {
        public ErrorResult() : base(false)
        {
        }

        public ErrorResult(int statusCode) : base(false, statusCode)
        {
        }

        public ErrorResult(string message) : base(false, message)
        {
        }

        public ErrorResult(int statusCode, string message) : base(false, statusCode, message)
        {
        }
    }
}
