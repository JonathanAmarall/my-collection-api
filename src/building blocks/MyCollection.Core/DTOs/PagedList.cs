using X.PagedList;

namespace MyCollection.Core.DTOs
{
    public record PagedList<T> where T : class
    {
        public PagedList(int totalCount, IPagedList<T> data)
        {
            TotalCount = totalCount;
            Data = data;
        }

        public int TotalCount { get; init; }

        public IPagedList<T> Data { get; init; }
    }
}