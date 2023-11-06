namespace MyCollection.Core.Models
{
    public static class CacheKeyHelper
    {
        public static readonly string CollectionItemKey = $"{Guid.NewGuid()}-collection-item";
    }
}
