using MyCollection.Domain.Entities;

namespace MyCollection.Api.Dto
{
    public class QueryCollectionItemResponse
    {
        public string? GlobalFilter { get; set; }
        public string? SortOrder { get; set; }
        public string? SortField { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 5;
        public ECollectionStatus? Status { get; set; }
        public EType? Type { get; set; }
    }
}
