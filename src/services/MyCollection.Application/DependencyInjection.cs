using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using MyCollection.Domain.Handler;

namespace MyCollection.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

        services
            .AddScoped<CreateCollectionItemCommandHandler>()
            .AddScoped<CreateLocationCommandHandler>()
            .AddScoped<LendCollectionItemCommandHandler>()
            .AddScoped<AddLocationInCollectionCommandHandler>();
            
        return services;
    }
}
