using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities.Concrete
{
    public class Role : IEntity
    {
        public int Id { get; set; }
        public string RoleName { get; set; }
        public string DescriptionKey { get; set; }
    }
}
