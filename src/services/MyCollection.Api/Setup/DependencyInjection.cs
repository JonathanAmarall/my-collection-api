using MyCollection.Application;
using MyCollection.Data;
using MyCollection.MessageBus;

namespace MyCollection.Api.Setup;
public static class DependencyInjection
{
    public static void AddDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddData(configuration)
            .AddApplication()
            .AddMessageBus(configuration);
    }
}