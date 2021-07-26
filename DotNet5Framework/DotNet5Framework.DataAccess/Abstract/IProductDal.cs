using Core.DataAccess.Abstract;
using DotNet5Framework.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNet5Framework.DataAccess.Abstract
{
    public interface IProductDal : IEntityRepository<Product>
    {
    }
}
