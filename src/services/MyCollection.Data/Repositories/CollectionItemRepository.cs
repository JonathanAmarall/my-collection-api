using MyCollection.Data.Extensions;
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

        public async Task<CollectionItemPaged<CollectionItem>> GetAllPagedAsync(string? globalFilter, string? sortOrder, string? sortField, ECollectionStatus? status, EType? type, int pageNumber = 1, int pageSize = 5)
        {
            var query = _context.CollectionItems.AsQueryable();

            if (!string.IsNullOrWhiteSpace(globalFilter))
            {
                query = query.Where(x =>
                    x.Autor.ToUpper().Contains(globalFilter.ToUpper()) ||
                    x.Title.ToUpper().Contains(globalFilter.ToUpper()) ||
                    x.Edition.ToUpper().Contains(globalFilter.ToUpper())
                );
            }

            if (!string.IsNullOrWhiteSpace(sortOrder) && !string.IsNullOrWhiteSpace(sortField))
            {
                query = query.GenericOrderBy(sortField, sortOrder.ToUpper() == "DESC");
            }

            if (status != null)
            {
                query = query.Where(x => x.Status == status);
            }

            if (type != null)
            {
                query = query.Where(x => x.ItemType == type);
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
