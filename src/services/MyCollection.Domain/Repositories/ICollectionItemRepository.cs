using MyCollection.Domain.Contracts;
using MyCollection.Domain.Dto;
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
        Task<PagedList<CollectionItem>> GetAllPagedAsync(string? globalFilter, string? sortOrder, string? sortField, ECollectionStatus? status, EType? type, int pageNumber = 1, int pageSize = 5);
        Task<PagedList<Contact>> GetAllContactsPagedAsync(string? globalFilter, int pageNumber = 1, int pageSize = 5);
    }
}
