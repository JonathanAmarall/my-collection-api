using MyCollection.Domain.Contracts;
using MyCollection.Domain.Entities;

namespace MyCollection.Domain.Repositories
{
    public interface ILocationRepository : IRepository<Location>
    {
        Task CreateAsync(Location location);    
        void Delete(Location location);
        void Update(Location location); 
    }
}
