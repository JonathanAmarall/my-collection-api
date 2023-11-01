using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MyCollection.MessageBus
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddMessageBus(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<MessageBusSettings>(configuration.GetSection(MessageBusSettings.SettingsKey));

            services.AddSingleton<IMessageBus, MessageBus>();

            return services;
        }
    }
}
