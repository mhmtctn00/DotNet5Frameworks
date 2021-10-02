using Core.Utilities.Results.ComplexTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Results.Abstract
{
    public interface IResult
    {
        ResultStatus Status { get; }
        int ResultCode { get; }
        string Message { get; }
    }
}
