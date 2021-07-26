using AutoMapper;
using DotNet5Framework.Entities.Concrete;
using DotNet5Framework.Entities.Dtos.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNet5Framework.Business.Mappings.AutoMapper
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<CategoryAddDto, Category>();
            CreateMap<Category, CategoryGetDto>();
            CreateMap<CategoryUpdateDto, Category>();
        }
    }
}
