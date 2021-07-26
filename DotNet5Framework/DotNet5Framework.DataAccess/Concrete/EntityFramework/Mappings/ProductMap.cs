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
    public class ProductMap : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");

            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).ValueGeneratedOnAdd();
            builder.Property(p => p.Name).IsRequired();
            builder.Property(p => p.Name).HasMaxLength(100);
            builder.Property(p => p.Amount).IsRequired();
            builder.Property(p => p.Price).IsRequired();
            builder.Property(p => p.CategoryId).IsRequired();
            builder.Property(p => p.CreatedDate).IsRequired();
            builder.Property(p => p.CreatedDate).HasDefaultValue(DateTime.Now);
            builder.Property(p => p.Status).IsRequired();
            builder.Property(p => p.Status).HasDefaultValue(0);

            builder.HasOne<Category>(p => p.Category).WithMany(c => c.Products).HasForeignKey(p => p.CategoryId);

            builder.HasData(new Product
            {
                Id = 1,
                Name = "Initial Product 1",
                Amount = 100,
                Price = (decimal)10.0,
                CategoryId = 1,
                Status = 0,
                CreatedDate = DateTime.Now
            }, new Product
            {
                Id = 2,
                Name = "Initial Product 2",
                Amount = 100,
                Price = (decimal)10.0,
                CategoryId = 2,
                Status = 0,
                CreatedDate = DateTime.Now
            }, new Product
            {
                Id = 3,
                Name = "Initial Product 3",
                Amount = 100,
                Price = (decimal)10.0,
                CategoryId = 3,
                Status = 0,
                CreatedDate = DateTime.Now
            }, new Product
            {
                Id = 4,
                Name = "Initial Product 4",
                Amount = 100,
                Price = (decimal)10.0,
                CategoryId = 4,
                Status = 0,
                CreatedDate = DateTime.Now
            });
        }
    }
}
