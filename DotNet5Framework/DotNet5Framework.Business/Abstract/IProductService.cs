using Core.Utilities.Results.Abstract;
using DotNet5Framework.Entities.Dtos.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNet5Framework.Business.Abstract
{
    public interface IProductService
    {
        IDataResult<IList<ProductGetDto>> GetAll();
        IResult Add(ProductAddDto dto);
        IResult TransactionTest();
    }
}
