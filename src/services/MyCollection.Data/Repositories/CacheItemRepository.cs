using Microsoft.Extensions.Caching.Memory;
using MyCollection.Core.Data;
using MyCollection.Core.DTOs;
using MyCollection.Core.Models;
using MyCollection.Domain.Entities;
using MyCollection.Domain.Repositories;

namespace MyCollection.Data.Repositories
{
    public class CacheItemRepository : ICollectionItemRepository
    {
        private readonly CollectionItemRepository _decorated;
        private readonly IMemoryCache _memoryCache;

        public CacheItemRepository(CollectionItemRepository collectionItemRepository, IMemoryCache memoryCache)
        {
            _decorated = collectionItemRepository;
            _memoryCache = memoryCache;
        }

        public IUnitOfWork UnitOfWork => _decorated.UnitOfWork;

        public async Task CreateAsync(CollectionItem item)
        {
            await _decorated.CreateAsync(item);
            await Task.FromResult(item);
        }

        public void Delete(CollectionItem item)
        {
            _decorated.Delete(item);
        }

        public void Dispose()
        {
            _decorated.Dispose();
        }

        public async Task<PagedList<CollectionItem>?> GetAllPagedAsync(string? globalFilter, string? sortOrder, string? sortField, ECollectionStatus? status, EType? type, int pageNumber = 1, int pageSize = 5)
        {
            string key = $"{CacheKeyHelper.CollectionItemKey}-globalFilter-{globalFilter}-sortOrder{sortOrder}-sortField{sortField}status{status}type{type}pageNumber{pageNumber}pageSize{pageSize}";

            return await _memoryCache.GetOrCreateAsync(key, async factory =>
            {
                factory.SetAbsoluteExpiration(TimeSpan.FromMinutes(5));
                return await _decorated.GetAllPagedAsync(globalFilter, sortOrder, sortField, status, type, pageNumber, pageSize);
            });

        }

        public async Task<CollectionItem?> GetByIdAsync(Guid collectionItemId)
        {
            string key = $"{CacheKeyHelper.CollectionItemKey}-{collectionItemId}";
            return await _memoryCache.GetOrCreateAsync(key, async factory =>
            {
                factory.SetAbsoluteExpiration(TimeSpan.FromMinutes(5));

                return await _decorated.GetByIdAsync(collectionItemId);
            });
        }

        public void Update(CollectionItem item)
        {
            _decorated.Update(item);
        }
    }
}
