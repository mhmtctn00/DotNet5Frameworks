using Core.DataAccess.Concrete.EntityFramework;
using DotNet5Framework.DataAccess.Abstract;
using DotNet5Framework.DataAccess.Concrete.EntityFramework.Contexts;
using DotNet5Framework.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNet5Framework.DataAccess.Concrete.EntityFramework.Repositories
{
    public class EfProductDal : EfEntityRepositoryBase<Product, DotNet5FrameworkContext>, IProductDal
    {
    }
}
