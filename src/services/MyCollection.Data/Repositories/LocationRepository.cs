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
            await _context.Locations!.AddAsync(location);
        }

        public void Delete(Location location)
        {
            _context.Locations!.Remove(location);
        }

        public void Dispose()
        {
            _context?.Dispose();
        }

        public async Task<Location?> GetByIdAsync(Guid id)
        {
            return await _context.Locations!.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<LocationDto>?> GetChildrensAsync(Guid id)
        {
            var locations = await _context.Locations!
                .Include(x => x.Childrens)
                .AsNoTracking()
                .Where(x => x.Childrens != null && x.Id == id)
                .Select(x => x.Childrens!.Select(c
                    => new LocationDto(c.Id, c.Initials, c.Description, c.ParentId))
                .ToList())
                .FirstAsync();

            return locations;
        }

        public async Task<string> GetFullLocationTag(Guid id)
        {
            string tag = "";

            Location? currentLocation = await _context.Locations!.AsNoTracking()
                .Where(x => x.Id == id)
                .Include(x => x.Parent)
                .FirstOrDefaultAsync();

            if (currentLocation == null)
                return tag;

            tag += $"{currentLocation.Description}";

            while (currentLocation?.ParentId != null)
            {
                currentLocation = await _context.Locations!
                .AsNoTracking()
                .Include(x => x.Parent)
                .ThenInclude(x => x!.Parent)
                .Where(x => x.Id == currentLocation.ParentId)
                .FirstOrDefaultAsync();

                tag = $"{currentLocation?.Description} > {tag}";
            }

            return tag;
        }

        public async Task<List<Location>> GetRootsAsync()
        {
            var locations = await _context.Locations!
                 .Where(x => x.ParentId == null)
                 .AsNoTracking()
                 .ToListAsync();

            return locations;
        }

        public void Update(Location location)
        {
            _context.Locations!.Update(location);
        }
    }
}
