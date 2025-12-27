using CodeFirst.Models;
using Microsoft.EntityFrameworkCore;

namespace CodeFirst.Context;

public class ApiDbContext : DbContext
{
    public ApiDbContext()
    {
    }

    public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<TypeUser> TypeUsers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(x => x.Id); // Definindo qual é a propriedade da chave primária

            entity.Property(x => x.Name)
                .HasMaxLength(100)
                .IsRequired();

            entity.HasIndex(x => x.Email).IsUnique();
            entity.Property(x => x.Email)
                .HasMaxLength(100);

            entity.HasOne(x => x.TypeUser)
                .WithMany(x => x.Users)
                .HasForeignKey(x => x.TypeUserId);
        });

        modelBuilder.Entity<TypeUser>(entity =>
        {
            entity.HasKey(x => x.Id);

            entity.Property(x => x.Name)
                .IsRequired();

            entity.HasMany(x => x.Users)
                .WithOne(x => x.TypeUser)
                .HasForeignKey(x => x.TypeUserId);
        });

        base.OnModelCreating(modelBuilder);
    }
}
