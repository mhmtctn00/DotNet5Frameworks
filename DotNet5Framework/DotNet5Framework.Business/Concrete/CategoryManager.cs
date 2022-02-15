using AutoMapper;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DotNet5Framework.Business.Abstract;
using DotNet5Framework.DataAccess.Abstract;
using DotNet5Framework.Entities.Concrete;
using DotNet5Framework.Entities.Dtos.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNet5Framework.Business.Concrete
{
    public class CategoryManager : ICategoryService
    {
        private readonly ICategoryDal _categoryDal;
        private readonly IMapper _mapper;

        public CategoryManager(ICategoryDal categoryDal, IMapper mapper)
        {
            _categoryDal = categoryDal;
            _mapper = mapper;
        }

        public async Task<IResult> Add(CategoryAddDto dto)
        {
            var addedCategory = _mapper.Map<CategoryAddDto, Category>(dto);
            await _categoryDal.AddAsync(addedCategory);
            await _categoryDal.SaveChangesAsync();
            return new SuccessResult();
        }

        public async Task<IDataResult<IList<CategoryGetDto>>> GetAll()
        {
            var categoryList = await _categoryDal.GetListAsync();
            var categoryGetList = _mapper.Map<List<Category>, List<CategoryGetDto>>(categoryList.ToList());
            return new SuccessDataResult<IList<CategoryGetDto>>(categoryGetList);
        }
    }
}
