using Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNet5Framework.Entities.Dtos.Category
{
    public class CategoryUpdateDto : IDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public short Status { get; set; } = 0; //0:Active, 1:Deactive, 2:Deleted, 3:Archived
    }
}
