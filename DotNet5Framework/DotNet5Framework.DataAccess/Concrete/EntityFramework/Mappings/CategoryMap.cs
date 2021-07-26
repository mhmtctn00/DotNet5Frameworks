using DotNet5Framework.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNet5Framework.DataAccess.Concrete.EntityFramework.Mappings
{
    public class CategoryMap : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Categories");

            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).ValueGeneratedOnAdd();
            builder.Property(c => c.Name).IsRequired();
            builder.Property(c => c.Name).HasMaxLength(100);
            builder.Property(c => c.Status).IsRequired();
            builder.Property(c => c.Status).HasDefaultValue(0);

            builder.HasData(new Category
            {
                Id = 1,
                Name = "Initial Category 1",
                Status = 0
            },
            new Category
            {
                Id = 2,
                Name = "Initial Category 2",
                Status = 0
            }, new Category
            {
                Id = 3,
                Name = "Initial Category 3",
                Status = 0
            }, new Category
            {
                Id = 4,
                Name = "Initial Category 4",
                Status = 0
            });
        }
    }
}
