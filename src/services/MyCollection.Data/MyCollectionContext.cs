using Microsoft.EntityFrameworkCore;
using MyCollection.Domain.Contracts;
using MyCollection.Domain.Entities;

namespace MyCollection.Data
{
    public class MyCollectionContext : DbContext, IUnitOfWork
    {
        public DbSet<CollectionItem>? CollectionItems { get; set; }
        public DbSet<Location>? Locations { get; set; }
        public DbSet<Contact>? Contacts { get; set; }

        public MyCollectionContext() { }

        public MyCollectionContext(DbContextOptions<MyCollectionContext> options) : base(options) { }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            foreach (var property in builder.Model.GetEntityTypes().SelectMany(
               e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
                property.SetColumnType("varchar(100)");

            builder.ApplyConfigurationsFromAssembly(typeof(MyCollectionContext).Assembly);

            base.OnModelCreating(builder);
        }

        public async Task<bool> Commit()
        {
            return await SaveChangesAsync() > 0;
        }
    }


}
