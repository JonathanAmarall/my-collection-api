using MyCollection.Domain.Contracts;
using MyCollection.Domain.Entities;
using MyCollection.Domain.Repositories;
using X.PagedList;

namespace MyCollection.Data.Repositories
{
    public class CollectionItemRepository : ICollectionItemRepository
    {
        private readonly MyCollectionContext _context;

        public CollectionItemRepository(MyCollectionContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public async Task CreateAsync(CollectionItem item)
        {
            await _context.CollectionItems.AddAsync(item);
        }

        public void Delete(CollectionItem item)
        {
            _context.CollectionItems.Remove(item);
        }

        public void Dispose()
        {
            _context?.Dispose();
        }

        public async Task<CollectionItemPaged<CollectionItem>> GetAllPagedAsync(string globalFilter, string sortOrder, string sortField, int pageNumber = 1, int pageSize = 10)
        {
            var query = _context.CollectionItems.AsQueryable();

            if (!string.IsNullOrWhiteSpace(globalFilter))
            {

            }

            return new CollectionItemPaged<CollectionItem>
            {
                Data = await query.ToPagedListAsync(pageNumber, pageSize),
                TotalCount = query.Count()
            };
        }

        public async Task<CollectionItem?> GetByIdAsync(Guid collectionItemId)
        {
            return await _context.CollectionItems.FindAsync(collectionItemId);
        }

        public Task<Contact?> GetContactByIdAsync(Guid contactId)
        {
            throw new NotImplementedException();
        }

        public void Update(CollectionItem item)
        {
            _context.CollectionItems.Update(item);
        }
    }
}
