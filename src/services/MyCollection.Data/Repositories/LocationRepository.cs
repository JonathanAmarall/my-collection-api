using MyCollection.Domain.Contracts;
using MyCollection.Domain.Entities;
using MyCollection.Domain.Repositories;

namespace MyCollection.Data.Repositories
{
    public class LocationRepository : ILocationRepository
    {
        private readonly MyCollectionContext _context;

        public LocationRepository(MyCollectionContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public async Task CreateAsync(Location location)
        {
            await _context.Locations.AddAsync(location);
        }

        public void Delete(Location location)
        {
            _context.Locations.Remove(location);
        }

        public void Dispose()
        {
            _context?.Dispose();
        }

        public void Update(Location location)
        {
            _context.Locations.Update(location);
        }
    }
}
