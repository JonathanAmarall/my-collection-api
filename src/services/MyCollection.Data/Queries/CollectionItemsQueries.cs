using Microsoft.EntityFrameworkCore;
using MyCollection.Data.Extensions;
using MyCollection.Domain.Dto;
using MyCollection.Domain.Entities;
using MyCollection.Domain.Queries;
using X.PagedList;

namespace MyCollection.Data.Queries
{
    public class CollectionItemsQueries : ICollectionItemsQueries
    {
        private readonly IMongoDBClient _mongoDb;

        public CollectionItemsQueries(IMongoDBClient mongoDb)
        {
            _mongoDb = mongoDb;
        }

        public void Insert(CollectionItem item)
        {
            _mongoDb.InsertOne(item);
        }

        public Domain.Dto.PagedList<CollectionItem> GetAllPaged(string? globalFilter, string? sortOrder, string? sortField, ECollectionStatus? status, EType? type, int pageNumber = 1, int pageSize = 5)
        {
            var query = _mongoDb.Get<CollectionItem>().Include(x => x.Contacts).AsQueryable();

            if (!string.IsNullOrWhiteSpace(globalFilter))
            {
                query = query.Where(x =>
                    x.Autor.ToUpper().Contains(globalFilter.ToUpper()) ||
                    x.Title.ToUpper().Contains(globalFilter.ToUpper()) ||
                    x.Edition!.ToUpper().Contains(globalFilter.ToUpper())
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

            return new Domain.Dto.PagedList<CollectionItem>(
                query.Count(),
                query.ToPagedList(pageNumber, pageSize)
            );
        }

        public CollectionItem? GetById(Guid collectionItemId)
        {
            return _mongoDb.Get<CollectionItem>().FirstOrDefault(x => x.Id == collectionItemId);
        }

        public void Update(CollectionItem item)
        {
            _mongoDb.ReplaceOne(item);
        }
    }
}