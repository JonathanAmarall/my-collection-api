using Microsoft.EntityFrameworkCore;
using MyCollection.Domain.Contracts;
using MyCollection.Domain.Dto;
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

        public async Task<List<LocationDto>?> GetChildrensAsync(Guid id)
        {
            var locations = await _context.Locations
                .Include(x => x.Childrens)
                .AsNoTracking()
                .Where(x => x.Childrens != null && x.Id == id)
                .Select(x => x.Childrens.Select(c => 
                new LocationDto 
                { 
                    Description = c.Description,
                    Id = c.Id,
                    Initials = c.Initials,
                    ParentId = c.ParentId
                }).ToList())
                .FirstAsync();

            return locations;
        }

        public async Task<List<Location>> GetRootsAsync()
        {
            var locations = await _context.Locations
                 .Where(x => x.ParentId == null)
                 .AsNoTracking()
                 .ToListAsync();

            return locations;
        }

        public void Update(Location location)
        {
            _context.Locations.Update(location);
        }
    }
}
