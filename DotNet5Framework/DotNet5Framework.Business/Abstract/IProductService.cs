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
        Task<IDataResult<IList<ProductGetDto>>> GetAllAsync();
        Task<IResult> AddAsync(ProductAddDto dto);
    }
}
