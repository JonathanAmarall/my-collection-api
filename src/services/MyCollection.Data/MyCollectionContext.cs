using Microsoft.EntityFrameworkCore;
using MyCollection.Core.Contracts;
using MyCollection.Domain.Entities;

namespace MyCollection.Data
{
    public class MyCollectionContext : DbContext, IUnitOfWork
    {
        public DbSet<CollectionItem>? CollectionItems { get; set; }
        public DbSet<Location>? Locations { get; set; }
        public DbSet<Borrower>? Contacts { get; set; }

        public MyCollectionContext() { }

        public MyCollectionContext(DbContextOptions<MyCollectionContext> options) :
            base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(
               e => e.GetProperties()
                   .Where(p => p.ClrType == typeof(string))))
                property.SetColumnType("varchar(100)");

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(MyCollectionContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }

        public async Task<bool> Commit()
        {
            return await SaveChangesAsync() > 0;
        }
    }
}
