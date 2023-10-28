using MyCollection.Core.Data;
using MyCollection.Domain.Entities;

namespace MyCollection.Domain.Repositories
{
    public interface ILocationRepository : IRepository<Location>
    {
        Task CreateAsync(Location location);
        void Delete(Location location);
        void Update(Location location);
        Task<List<Location>> GetRootsAsync();
        Task<List<Location>?> GetLocationsChildrenAsync(Guid id);
        Task<Location?> GetByIdAsync(Guid locationId);
        Task<string> GetFullLocationTag(Guid id);
    }
}
