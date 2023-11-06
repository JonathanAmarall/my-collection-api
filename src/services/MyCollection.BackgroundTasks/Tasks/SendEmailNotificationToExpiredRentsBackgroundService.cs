using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MyCollection.BackgroundTasks.Services;
using MyCollection.BackgroundTasks.Settings;

namespace MyCollection.BackgroundTasks.Tasks
{
    internal sealed class SendEmailNotificationToExpiredRentsBackgroundService : BackgroundService
    {
        private readonly ILogger<SendEmailNotificationToExpiredRentsBackgroundService> _logger;
        private readonly BackgroundTaskSettings _backgroundTaskSettings;
        private readonly IServiceProvider _serviceProvider;

        public SendEmailNotificationToExpiredRentsBackgroundService(
            ILogger<SendEmailNotificationToExpiredRentsBackgroundService> logger,
            IOptions<BackgroundTaskSettings> backgroundTaskSettingsOptions,
            IServiceProvider serviceProvider)
        {
            _logger = logger;
            _backgroundTaskSettings = backgroundTaskSettingsOptions.Value;
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogDebug("SendEmailNotificationToExpiredRentsBackgroundService is starting.");
            stoppingToken.Register(() => _logger.LogDebug("SendEmailNotificationToExpiredRentsBackgroundService background task is stopping."));

            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogDebug("SendEmailNotificationToExpiredRentsBackgroundService background task is doing background work.");

                await ConsumeEventNotificationsAsync(stoppingToken);

                await Task.Delay(_backgroundTaskSettings.SleepTimeInMilliseconds, stoppingToken);
            }

            _logger.LogDebug("SendEmailNotificationToExpiredRentsBackgroundService background task is stopping.");

            await Task.CompletedTask;
        }

        private async Task ConsumeEventNotificationsAsync(CancellationToken stoppingToken)
        {
            try
            {
                using IServiceScope scope = _serviceProvider.CreateScope();
                var emailNotificationsConsumer = scope.ServiceProvider.GetRequiredService<IEmailNotificationsConsumer>();

                await emailNotificationsConsumer.ConsumeAsync(
                  _backgroundTaskSettings.NotificationsBatchSize,
                  _backgroundTaskSettings.AllowedNotificationTimeDiscrepancyInMinutes,
                  stoppingToken);
            }
            catch (Exception e)
            {
                _logger.LogCritical($"ERROR: Failed to process the batch of notifications: {e.Message}", e.Message);
            }
        }
    }
}
