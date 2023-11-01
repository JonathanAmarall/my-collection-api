using Microsoft.EntityFrameworkCore;
using MyCollection.Core.Data;
using MyCollection.Domain.Entities;
using MyCollection.Domain.Repositories;

namespace MyCollection.Data.Repositories
{
    internal class RentItemRepository : IRentItemRepository
    {
        private readonly MyCollectionContext _context;

        public RentItemRepository(MyCollectionContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public async Task<RentItem> CreateAsync(RentItem item)
        {
            await _context.Set<RentItem>().AddAsync(item);
            return await Task.FromResult(item);
        }

        public async Task<List<RentItem>> GetAllAsync()
        {
            return await _context.Set<RentItem>().ToListAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context?.Dispose();
            }
        }
    }
}
