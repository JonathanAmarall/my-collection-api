using MyCollection.Domain.Entities;
using X.PagedList;

namespace MyCollection.Domain.Contracts
{
    public class CollectionItemPaged<T>  where T : EntityBase
    {
        public int TotalCount { get; set; }

        public IPagedList<T> Data { get; set; }
    }
}
