using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyCollection.Data.Repositories;
using MyCollection.Domain.Repositories;

namespace MyCollection.Data
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddData(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<MyCollectionContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
            });

            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

            services.AddMemoryCache();

            services
                .AddScoped<ICollectionItemRepository, CollectionItemRepository>()
                .AddScoped<ILocationRepository, LocationRepository>()
                .AddScoped<IBorrowerRepository, BorrowerRepository>()
                .AddScoped<IRentItemRepository, RentItemRepository>();

            services.Decorate<ICollectionItemRepository, CacheItemRepository>();

            return services;
        }
    }
}
