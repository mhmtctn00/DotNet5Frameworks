using Core.Utilities.Results.Abstract;
using DotNet5Framework.Entities.Dtos.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNet5Framework.Business.Abstract
{
    public interface ICategoryService
    {
        Task<IDataResult<IList<CategoryGetDto>>> GetAllAsync();
        Task<IResult> AddAsync(CategoryAddDto dto);
    }
}
