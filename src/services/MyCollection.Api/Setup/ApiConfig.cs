using Microsoft.EntityFrameworkCore;
using MyCollection.Data;
using System.Text.Json.Serialization;

namespace MyCollection.Api.Setup
{
    public static class ApiConfig
    {
        public static void AddApiConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers().AddJsonOptions(x =>
            {
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            });

            services.AddDbContext<MyCollectionContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddAutoMapper(typeof(MapperConfiguration));

            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

            //services.AddScoped<IMongoDBClient, MongoDBClient>();
        }
    }
}
