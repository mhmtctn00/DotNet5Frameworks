using Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNet5Framework.Entities.Dtos.Category
{
    public class CategoryAddDto : IDto
    {
        public string Name { get; set; }
    }
}
