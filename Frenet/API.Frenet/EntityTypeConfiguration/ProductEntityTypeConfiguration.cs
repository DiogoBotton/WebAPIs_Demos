using API.Frenet.Domains;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Frenet.EntityTypeConfiguration
{
    public class ProductEntityTypeConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");

            builder.Property(x => x.Id).IsRequired();
            builder.HasKey(x => x.Id);

            builder.HasOne<Shipping>()
                .WithMany()
                .HasForeignKey("ShippingId")
                .IsRequired();
        }
    }
}
