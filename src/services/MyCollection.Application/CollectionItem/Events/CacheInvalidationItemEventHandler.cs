using Microsoft.Extensions.Caching.Memory;
using MyCollection.Core.Contracts;
using MyCollection.Core.Models;
using MyCollection.Domain.Events;
using System.Collections;
using System.Reflection;


namespace MyCollection.Application.CollectionItem.Events
{
    internal class CacheInvalidationItemEventHandler : IDomainEventHandler<CreatedCollectionItemDomainEvent>, IDomainEventHandler<RentItemDomainEvent>
    {
        private readonly IMemoryCache _memoryCache;

        public CacheInvalidationItemEventHandler(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public async Task Handle(RentItemDomainEvent notification, CancellationToken cancellationToken)
        {
            await InternalHandler();
        }

        public async Task Handle(CreatedCollectionItemDomainEvent notification, CancellationToken cancellationToken)
        {
            await InternalHandler();
        }

        public async Task InternalHandler()
        {
            var keys = GetPersistedKeys();

            keys.Where(key => key.Contains(CacheKeyHelper.CollectionItemKey))
                .ToList()
                .ForEach(_memoryCache.Remove);

            await Task.CompletedTask;
        }

        public IEnumerable<string> GetPersistedKeys()
        {
            var persistedKeys = new List<string>();

            var fieldInfo = typeof(MemoryCache).GetField("_coherentState", BindingFlags.Instance | BindingFlags.NonPublic);
            var propertyInfo = fieldInfo!.FieldType.GetProperty("EntriesCollection", BindingFlags.Instance | BindingFlags.NonPublic);
            var value = fieldInfo.GetValue(_memoryCache);
            var dict = propertyInfo!.GetValue(value);
            var cacheEntries = dict as dynamic;

            if (cacheEntries != null)
            {
                foreach (var item in cacheEntries)
                {
                    ICacheEntry cacheItemValue = item.GetType().GetProperty("Value").GetValue(item, null);
                    if (cacheItemValue is not null)
                    {
                        persistedKeys.Add(cacheItemValue.Key.ToString()!);
                    }
                }
            }

            return persistedKeys;
        }
    }
}
