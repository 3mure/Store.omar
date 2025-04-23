using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configration
{
    public class ProductConfigration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasOne(P => P.ProductBrand)
                   .WithMany()
                   .HasForeignKey(p=>p.BrandId);
            builder.HasOne(p => p.ProductType)
                   .WithMany()
                     .HasForeignKey(p => p.TypeId);
            builder.Property(p => p.Price).HasColumnType("decimal(18,2)");
        }
    }

}
