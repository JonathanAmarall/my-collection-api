using MyCollection.Domain.Entities;
using X.PagedList;

namespace MyCollection.Domain.Dto
{
    public class PagedList<T> where T : EntityBase
    {
        public PagedList(int totalCount, IPagedList<T> data)
        {
            TotalCount = totalCount;
            Data = data;
        }

        public int TotalCount { get; set; }

        public IPagedList<T> Data { get; set; }
    }
}
