using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyCollection.BackgroundTasks.Services;
using MyCollection.BackgroundTasks.Tasks;
using MyCollection.Core.Contracts;
using MyCollection.Core.Email;
using MyCollection.MessageBus;
using System.Reflection;

namespace MyCollection.BackgroundTasks;

public static class DependencyInjection
{
    public static IServiceCollection AddBackgroundTasks(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

        services.Configure<MessageBusSettings>(configuration.GetSection(MessageBusSettings.SettingsKey));
        services.Configure<MailSettings>(configuration.GetSection(MailSettings.SettingsKey));

        services.AddScoped<IIntegrationEventConsumer, IntegrationEventConsumer>();
        services.AddTransient<IEmailService, EmailService>();
        services.AddHostedService<IntegrationEventConsumerBackgroundService>();

        return services;
    }
}
