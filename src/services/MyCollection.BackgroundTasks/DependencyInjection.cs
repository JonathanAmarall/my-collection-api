using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyCollection.BackgroundTasks.Services;
using MyCollection.BackgroundTasks.Tasks;
using MyCollection.MessageBus;

namespace MyCollection.BackgroundTasks;

public static class DependencyInjection
{
    public static IServiceCollection AddBackgroundTasks(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        services.Configure<MessageBusSettings>(configuration.GetSection(MessageBusSettings.SettingsKey));
        services.AddScoped<IIntegrationEventConsumer, IntegrationEventConsumer>();
        services.AddHostedService<IntegrationEventConsumerBackgroundService>();

        return services;
    }
}
