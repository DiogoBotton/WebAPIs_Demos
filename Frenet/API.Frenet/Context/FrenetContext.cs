using API.Frenet.Domains;
using API.Frenet.EntityTypeConfiguration;
using API.Frenet.SeedWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace API.Frenet.Context
{
    public class FrenetContext : DbContext, IUnitOfWork
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Quote> Quotes { get; set; }
        public DbSet<Shipping> Shippings { get; set; }

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
            modelBuilder.ApplyConfiguration(new ShippingEntityTypeConfiguration());
        }

        public async Task SaveDbChanges(CancellationToken cancellationToken = default)
        {
            await base.SaveChangesAsync();
        }
    }
}
