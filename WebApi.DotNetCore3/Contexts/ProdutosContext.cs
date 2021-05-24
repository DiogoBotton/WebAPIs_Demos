using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.DotNetCore3.Domains;
using WebApi.DotNetCore3.EntityTypeConfigurations;

namespace WebApi.DotNetCore3.Contexts
{
    public class ProdutosContext : DbContext
    {
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

        public ProdutosContext(DbContextOptions<ProdutosContext> options) : base(options)
        {
        }

        public ProdutosContext()
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProdutoEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new UsuarioEntityTypeConfiguration());
        }
    }
}
