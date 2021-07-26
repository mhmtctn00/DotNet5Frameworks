using AutoMapper;
using DotNet5Framework.Entities.Concrete;
using DotNet5Framework.Entities.Dtos.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNet5Framework.Business.Mappings.AutoMapper
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<ProductAddDto, Product>();
            CreateMap<Product, ProductGetDto>();
            CreateMap<ProductUpdateDto, Product>();

            //CreateMap<UserSupport, BoPanelUserSupportGetDto>()
            //    .ForMember(dto => dto.InterestedBackOfficeUser, opt => opt.MapFrom(x => x.BackOfficeUser));
        }
    }
}
