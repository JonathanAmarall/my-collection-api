using MyCollection.Data.Queries;
using MyCollection.Data.Repositories;
using MyCollection.Domain.Handler;
using MyCollection.Domain.Queries;
using MyCollection.Domain.Repositories;

namespace MyCollection.Api.Setup
{
    public static class DependencyInjection
    {
        public static void AddServiceDependencies(this IServiceCollection services)
        {
            services.AddScoped<ICollectionItemRepository, CollectionItemRepository>();
            services.AddScoped<ILocationRepository, LocationRepository>();

            services.AddScoped<LocationHandler>();
            services.AddScoped<CollectionItemHandler>();

            //services.AddScoped<ICollectionItemsQueries, CollectionItemsQueries>();
        }
    }
}
