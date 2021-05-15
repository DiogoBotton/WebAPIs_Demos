using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.DotNetCore3.Domains;

namespace WebApi.DotNetCore3.EntityTypeConfigurations
{
    public class ProdutoEntityTypeConfiguration : IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            builder.ToTable("Produtos");

            builder.HasKey(x => x.Id);
        }
    }
}
