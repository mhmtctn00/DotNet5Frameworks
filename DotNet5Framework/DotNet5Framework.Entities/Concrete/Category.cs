using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNet5Framework.Entities.Concrete
{
    public class Category : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public short Status { get; set; } = 0; //0:Active, 1:Deactive, 2:Deleted, 3:Archived
        public virtual ICollection<Product> Products { get; set; }
    }
    public enum CategoryStatus
    {
        Active = 0,
        Deactive = 1,
        Deleted = 2,
        Archived = 3
    }
}
