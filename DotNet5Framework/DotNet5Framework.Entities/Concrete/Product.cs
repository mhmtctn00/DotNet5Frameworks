using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNet5Framework.Entities.Concrete
{
    public class Product : IEntity
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public int Amount { get; set; }
        public decimal Price { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public short Status { get; set; } = 0; //0:Active, 1:Deactive, 2:Deleted, 3:Archived
        public virtual Category Category { get; set; }
    }
    public enum ProductStatus
    {
        Active = 0,
        Deactive = 1,
        Deleted = 2,
        Archived = 3
    }
}
