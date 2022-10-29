using MyCollection.Data.Repositories;
using MyCollection.Domain.Repositories;

namespace MyCollection.Api.Setup
{
    public static class DependencyInjection
    {
        public static void AddDependencies(this IServiceCollection services)
        {
            services.AddScoped<ICollectionItemRepository, CollectionItemRepository>();

        }
    }
}
