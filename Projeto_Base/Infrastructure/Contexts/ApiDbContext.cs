using Domains.Models.Addresses;
using Domains.Models.ResetPasswordCodes;
using Domains.Models.Users;
using Domains.SeedWork;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Infrastructure.Contexts;

public class ApiDbContext : DbContext, IUnitOfWork
{
    public DbSet<User> Users { get; set; }
    public DbSet<ResetPasswordCode> ResetPasswordCodes { get; set; }
    public DbSet<Address> Addresses { get; set; }

    public ApiDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder mb)
    {
        mb.Entity<User>(d =>
        {
            d.HasKey(d => d.Id);
            d.HasIndex(d => d.Email).IsUnique();
            d.Property(d => d.Status).HasDefaultValue(true);
            d.HasMany(d => d.ResetPasswordCodes).WithOne(d => d.User).HasForeignKey(d => d.UserId);
            d.HasOne(d => d.Address).WithOne(d => d.User).HasForeignKey<Address>(d => d.UserId).OnDelete(DeleteBehavior.Cascade);
        });

        mb.Entity<ResetPasswordCode>(d =>
        {
            d.HasKey(d => d.Id);
            d.HasOne(d => d.User).WithMany(d => d.ResetPasswordCodes).HasForeignKey(d => d.UserId);
        });

        mb.Entity<Address>(d =>
        {
            d.HasKey(d => d.Id);
            d.HasOne(d => d.User).WithOne(d => d.Address).HasForeignKey<Address>(d => d.UserId);
        });

        base.OnModelCreating(mb);
    }

    public async Task SaveDbChanges(CancellationToken cancellationToken = default)
    {
        await base.SaveChangesAsync();
    }

    #region Set CreatedAt and ModifiedAt Property

    /// <summary>
    /// Iterate on each IEntity and set the created and modified at behavior
    /// </summary>
    /// <param name="modelBuilder"></param>
    public void SetCreatedAtAndModifiedAtProperty(ModelBuilder modelBuilder)
    {
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            var entityClrType = entityType.ClrType;

            if (typeof(IEntity).IsAssignableFrom(entityClrType))
            {
                var method = SetCreatedAtAndModifiedAtPropertyOnAddMethodInfo.MakeGenericMethod(entityClrType);
                method.Invoke(this, new object[] { modelBuilder });
            }
        }
    }

    /// <summary>
    /// Adds to the entity the behavior of filling date information on create or update
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="builder"></param>
    public void SetCreatedAtAndModifiedAtPropertyOnAdd<T>(ModelBuilder builder) where T : class, IEntity
    {
        builder.Entity<T>().Property(d => d.CreatedAt).HasDefaultValueSql("NOW(6)").ValueGeneratedOnAdd();
        builder.Entity<T>().Property(d => d.ModifiedAt).HasDefaultValueSql("NOW(6)").ValueGeneratedOnAdd();
    }

    private static readonly MethodInfo SetCreatedAtAndModifiedAtPropertyOnAddMethodInfo = typeof(ApiDbContext).GetMethods(BindingFlags.Public | BindingFlags.Instance)
        .Single(t => t.IsGenericMethod && t.Name == nameof(SetCreatedAtAndModifiedAtPropertyOnAdd));

    public override int SaveChanges()
    {
        UpdateModifiedProperty();
        return base.SaveChanges();
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        UpdateModifiedProperty();
        return base.SaveChangesAsync(cancellationToken);
    }

    private void UpdateModifiedProperty()
    {
        var entries = ChangeTracker
            .Entries()
            .Where(e => e.Entity is AbstractDomain && e.State == EntityState.Modified);

        foreach (var entityEntry in entries)
        {
            ((AbstractDomain)entityEntry.Entity).ModifiedAt = DateTime.UtcNow;
            entityEntry.Property(nameof(AbstractDomain.ModifiedAt)).IsModified = true;
        }
    }

    #endregion
}
