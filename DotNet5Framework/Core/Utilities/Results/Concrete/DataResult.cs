using Core.Utilities.Results.Abstract;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Results.Concrete
{
    public class DataResult<T> : Result, IDataResult<T>
    {
        public DataResult(T data, bool status) : base(status)
        {
            Data = data;
        }
        public DataResult(T data, bool status, int statusCode) : base(status, statusCode)
        {
            Data = data;
        }

        public DataResult(T data, bool status, string message) : base(status, message)
        {
            Data = data;
        }

        public DataResult(T data, bool status, int statusCode, string message) : base(status, statusCode, message)
        {
            Data = data;
        }

        public T Data { get; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
