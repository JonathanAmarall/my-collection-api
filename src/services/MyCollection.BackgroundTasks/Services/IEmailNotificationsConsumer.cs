using MyCollection.Domain.Repositories;

namespace MyCollection.BackgroundTasks.Services
{
    internal interface IEmailNotificationsConsumer
    {
        /// <summary>
        /// Consumes the event notifications for the specified batch size.
        /// </summary>
        /// <param name="batchSize">The batch size.</param>
        /// <param name="allowedNotificationTimeDiscrepancyInMinutes">
        /// The allowed discrepancy in minutes between the current time and the notification time.
        /// </param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The completed task.</returns>
        Task ConsumeAsync(int batchSize, int allowedNotificationTimeDiscrepancyInMinutes, CancellationToken cancellationToken = default);
    }

    internal sealed class EmailNotificationsConsumer : IEmailNotificationsConsumer
    {
        private readonly IRentItemRepository _rentItemRepository;

        public EmailNotificationsConsumer(IRentItemRepository rentItemRepository)
        {
            _rentItemRepository = rentItemRepository;
        }

        public async Task ConsumeAsync(int batchSize, int allowedNotificationTimeDiscrepancyInMinutes, CancellationToken cancellationToken = default)
        {
            var rentItensExpireds = await _rentItemRepository.GetExpiredRents(batchSize);

            foreach (var expiredItem in rentItensExpireds)
            {
                //

            }
        }
    }
}
