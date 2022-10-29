
using MyCollection.Domain.Contracts;
using MyCollection.Domain.Entities;

namespace MyCollection.Domain.Repositories
{
    public interface ICollectionItemRepository : IRepository<CollectionItem>
    {
        Task CreateAsync(CollectionItem item);
        void Delete(CollectionItem item);
        void Update(CollectionItem item);
        Task<CollectionItem?> GetByIdAsync(Guid collectionItemId);
        Task<Contact?> GetContactByIdAsync(Guid contactId);
        Task<CollectionItemPaged<CollectionItem>> GetAllPagedAsync(string globalFilter, string sortOrder, string sortField, int pageNumber = 1, int pageSize = 10);
    }
}
