using Microsoft.EntityFrameworkCore;
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
            return await SaveChangesAsync() > 0;
        }
    }
}
