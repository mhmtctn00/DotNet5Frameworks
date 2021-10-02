using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.ComplexTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Results.Concrete
{
    public class DataResult<T> : Result, IDataResult<T>
    {
        public DataResult(T data, ResultStatus status) : base(status)
        {
            Data = data;
        }
        public DataResult(T data, ResultStatus status, int statusCode) : base(status, statusCode)
        {
            Data = data;
        }

        public DataResult(T data, ResultStatus status, string message) : base(status, message)
        {
            Data = data;
        }

        public DataResult(T data, ResultStatus status, int statusCode, string message) : base(status, statusCode, message)
        {
            Data = data;
        }

        public T Data { get; }

    }
}
