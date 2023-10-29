using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using MyCollection.Core.Contracts;
using MyCollection.Core.Data;
using MyCollection.Domain.Entities;
using System.Reflection;

namespace MyCollection.Data
{
    public class MyCollectionContext : DbContext, IUnitOfWork
    {
        public DbSet<CollectionItem>? CollectionItems { get; set; }
        public DbSet<Location>? Locations { get; set; }
        public DbSet<Borrower>? Borrowers { get; set; }

        public MyCollectionContext() { }

        public MyCollectionContext(DbContextOptions<MyCollectionContext> options) :
            base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(MyCollectionContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }

        public async Task<bool> Commit()
        {
            DateTime utcNow = new();

            UpdateAuditableEntities(utcNow);

            return await SaveChangesAsync() > 0;
        }

        private void UpdateAuditableEntities(DateTime utcNow)
        {
            foreach (EntityEntry<IAuditableEntity> entityEntry in ChangeTracker.Entries<IAuditableEntity>())
            {
                if (entityEntry.State == EntityState.Added)
                {
                    entityEntry.Property(nameof(IAuditableEntity.CreatedAt)).CurrentValue = utcNow;
                }

                if (entityEntry.State == EntityState.Modified)
                {
                    entityEntry.Property(nameof(IAuditableEntity.UpdateAt)).CurrentValue = utcNow;
                }
            }
        }

    }
}
