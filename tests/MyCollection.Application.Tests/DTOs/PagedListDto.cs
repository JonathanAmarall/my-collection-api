namespace MyCollection.Application.Tests.DTOs
{
    public class PagedListDto<T>
    {
        public int TotalCount { get; init; }

        public List<T> Data { get; init; }
    }
}
