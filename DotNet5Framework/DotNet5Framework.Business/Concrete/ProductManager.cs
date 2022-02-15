using AutoMapper;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Performance;
using Core.Aspects.Autofac.Transaction;
using Core.Aspects.Autofac.Validation;
using Core.Aspects.Autofac.Validation.FluentValidation;
using Core.CrossCuttingConcerns.Logging.Serilog.Concrete.Loggers;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DotNet5Framework.Business.Abstract;
using DotNet5Framework.Business.ValidationRules.FluentValidation;
using DotNet5Framework.DataAccess.Abstract;
using DotNet5Framework.Entities.Concrete;
using DotNet5Framework.Entities.Dtos.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DotNet5Framework.Business.Concrete
{
    [ExceptionLogAspect(typeof(MsSqlLogger))]
    public class ProductManager : IProductService
    {
        private readonly IProductDal _productDal;
        private readonly IMapper _mapper;

        public ProductManager(IProductDal productDal, IMapper mapper)
        {
            _productDal = productDal;
            _mapper = mapper;
        }

        [ValidationAspect(typeof(ProductAddDtoValidator))]
        [SuccessLogAspect(typeof(MsSqlLogger))]
        [CacheRemoveAspect("ProductManager.GetAll")]
        public async Task<IResult> Add(ProductAddDto dto)
        {
            var addedProduct = _mapper.Map<ProductAddDto, Product>(dto);
            await _productDal.AddAsync(addedProduct);
            await _productDal.SaveChangesAsync();
            return new SuccessResult();
        }

        [PerformanceAspect(5)]
        [CacheAspect()]
        public async Task<IDataResult<IList<ProductGetDto>>> GetAll()
        {
            var productList = await _productDal.GetListAsync();
            //var x = 0;
            //var y = 8 / x;
            var productGetList = _mapper.Map<List<Product>, List<ProductGetDto>>(productList.ToList());
            return new SuccessDataResult<IList<ProductGetDto>>(productGetList);
        }
    }
}
