using Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNet5Framework.Entities.Dtos.Product
{
    public class ProductUpdateDto : IDto
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public int Amount { get; set; }
        public decimal Price { get; set; }
        public short Status { get; set; } = 0; //0:Active, 1:Deactive, 2:Deleted, 3:Archived
    }
}
