using MyCollection.Data.Repositories;
using MyCollection.Domain.Handler;
using MyCollection.Domain.Repositories;

namespace MyCollection.Api.Setup
{
    public static class DependencyInjection
    {
        public static void AddDependencies(this IServiceCollection services)
        {
            services.AddScoped<ICollectionItemRepository, CollectionItemRepository>(); 
            services.AddScoped<ILocationRepository, LocationRepository>();

            services.AddScoped<LocationHandler>(); 
            services.AddScoped<CollectionItemHandler>(); 
        }
    }
}
