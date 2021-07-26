using Core.Entities;
using DotNet5Framework.Entities.Dtos.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNet5Framework.Entities.Dtos.Product
{
    public class ProductGetDto : IDto
    {
        public int Id { get; set; }
        public CategoryGetDto Category { get; set; }
        public string Name { get; set; }
        public int Amount { get; set; }
        public decimal Price { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
