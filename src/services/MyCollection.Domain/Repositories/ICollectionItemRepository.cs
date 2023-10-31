using MyCollection.Core.Data;
using MyCollection.Core.DTOs;
using MyCollection.Domain.Entities;


namespace MyCollection.Domain.Repositories
{
    public interface ICollectionItemRepository : IRepository<CollectionItem>
    {
        Task<PagedList<CollectionItem>> GetAllPagedAsync(string? globalFilter, string? sortOrder, string? sortField, ECollectionStatus? status, EType? type, int pageNumber = 1, int pageSize = 5);
        Task CreateAsync(CollectionItem item);
        void Delete(CollectionItem item);
        void Update(CollectionItem item);
        Task<CollectionItem?> GetByIdAsync(Guid collectionItemId);
    }
}
