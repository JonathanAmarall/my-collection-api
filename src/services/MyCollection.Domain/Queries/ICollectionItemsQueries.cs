using MyCollection.Domain.Dto;
using MyCollection.Domain.Entities;

namespace MyCollection.Domain.Queries
{

    public interface ICollectionItemsQueries
    {
        void Insert(CollectionItem item);
        CollectionItem? GetById(Guid collectionItemId);
        PagedList<CollectionItem> GetAllPaged(string? globalFilter, string? sortOrder, string? sortField, ECollectionStatus? status, EType? type, int pageNumber = 1, int pageSize = 5);
        void Update(CollectionItem item);
    }
}
