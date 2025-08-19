using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using MyFirstWebAPIProject.Database.Models.Entities;

namespace MyFirstWebAPIProject.Database.DatabaseContext;

public interface IApplicationDbContext
{
    DbSet<User> Users { get; set; }
    DbSet<UserAddress> UsersAddresses { get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    int SaveChanges();
    EntityEntry<TEntity> Update<TEntity>(TEntity entity) where TEntity : class;
}

internal class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<UserAddress> UsersAddresses { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
        .HasOne(x => x.UserAddress)
        .WithOne(x => x.User)
        .HasForeignKey<UserAddress>(x => x.UserId);
    }
}