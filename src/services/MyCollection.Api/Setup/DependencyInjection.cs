using MyCollection.Data.Repositories;
using MyCollection.Domain.Repositories;
using MyCollection.Application;

namespace MyCollection.Api.Setup;
public static class DependencyInjection
{
    public static void AddDependencies(this IServiceCollection services)
    {
        services.AddScoped<ICollectionItemRepository, CollectionItemRepository>();
        services.AddScoped<ILocationRepository, LocationRepository>();

        services.AddApplication();
    }
}