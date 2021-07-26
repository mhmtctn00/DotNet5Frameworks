using AutoMapper;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Performance;
using Core.Aspects.Autofac.Transaction;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Logging.Serilog.Concrete.Loggers;
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
    [ExceptionLogAspect(typeof(DatabaseLogger))]
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
        [LogAspect(typeof(DatabaseLogger))]
        [CacheRemoveAspect("ProductManager.GetAll")]
        public IResult Add(ProductAddDto dto)
        {
            var addedProduct = _mapper.Map<ProductAddDto, Product>(dto);
            _productDal.Add(addedProduct);
            return new SuccessResult();
        }

        [PerformanceAspect(5)]
        [CacheAspect()]
        public IDataResult<IList<ProductGetDto>> GetAll()
        {
            var productList = _productDal.GetList();
            //var x = 0;
            //var y = 8 / x;
            var productGetList = _mapper.Map<List<Product>, List<ProductGetDto>>(productList.ToList());
            return new SuccessDataResult<IList<ProductGetDto>>(productGetList);
        }

        [TransactionScopeAspect()]
        public IResult TransactionTest()
        {
            _productDal.Add(new Product
            {
                Name = "Transaction Test Item 1",
                CategoryId = 1,
                Amount = 100,
                Price = (decimal)10.0
            });
            _productDal.Add(new Product
            {
                Name = "Transaction Test Item 2",
                CategoryId = 2,
                Amount = 100,
                Price = (decimal)10.0
            });
            _productDal.Add(new Product
            {
                Name = "Transaction Test Item 3",
                CategoryId = 3,
                Amount = 100,
                Price = (decimal)10.0
            });

            var x = 0;
            var y = 8 / x;

            _productDal.Add(new Product
            {
                Name = "Transaction Test Item 4",
                CategoryId = 4,
                Amount = 100,
                Price = (decimal)10.0
            });

            return new SuccessResult();
        }
    }
}
