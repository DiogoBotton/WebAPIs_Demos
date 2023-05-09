using API.Frenet.Domains;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Frenet.EntityTypeConfiguration
{
    public class QuoteEntityTypeConfiguration : IEntityTypeConfiguration<Quote>
    {
        public void Configure(EntityTypeBuilder<Quote> builder)
        {
            builder.ToTable("Quotes");

            builder.Property(x => x.Id).IsRequired();
            builder.HasKey(x => x.Id);

            builder.HasOne<Shipping>()
                .WithMany()
                .HasForeignKey("ShippingId")
                .IsRequired();
        }
    }
}
