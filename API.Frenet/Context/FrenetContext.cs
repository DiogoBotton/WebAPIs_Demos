using API.Frenet.Domains;
using API.Frenet.EntityTypeConfiguration;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Frenet.Context
{
    public class FrenetContext : DbContext
    {
        public DbSet<Product> Products { get; set; }

        public FrenetContext(DbContextOptions<FrenetContext> options) : base(options)
        {
        }

        public FrenetContext()
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProductEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new QuoteEntityTypeConfiguration());
        }
    }
}
