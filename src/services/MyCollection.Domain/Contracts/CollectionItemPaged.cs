using MyCollection.Domain.Entities;
using X.PagedList;

namespace MyCollection.Domain.Contracts
{
    public class CollectionItemPaged<T>  where T : EntityBase
    {
        public CollectionItemPaged(int totalCount, IPagedList<T> data)
        {
            TotalCount = totalCount;
            Data = data;
        }

        public int TotalCount { get; set; }

        public IPagedList<T> Data { get; set; }
    }
}
