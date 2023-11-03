using Microsoft.Extensions.Logging;
using MyCollection.Application.CollectionItem.Events;
using MyCollection.Core.Contracts;
using MyCollection.Core.Email;
using MyCollection.Core.Models;

namespace MyCollection.BackgroundTasks.IntegrationEvents.Item
{
    internal class RentItemIntegrationEventHandler : IIntegrationEventHandler<RentItemIntegrationEvent>
    {
        private readonly IEmailService _emailService;
        private readonly ILogger<RentItemIntegrationEventHandler> _logger;

        public RentItemIntegrationEventHandler(IEmailService emailService, ILogger<RentItemIntegrationEventHandler> logger)
        {
            _emailService = emailService;
            _logger = logger;
        }

        public async Task Handle(RentItemIntegrationEvent notification, CancellationToken cancellationToken)
        {
            var (subject, body) = MailTemplates.CreateRentedMessageBorrowerEmail(
                notification.ItemTitle,
                notification.DueDate,
                notification.Borrower.FullName,
                notification.Borrower.Email.Value);

            var mailRequest = new MailRequest(notification.Borrower.Email.Value, subject, body);

            await _emailService.SendEmailAsync(mailRequest);

            _logger.LogInformation("Enviando email para {email} do Borrower {fullName}", notification.Borrower.Email.Value, notification.Borrower.FullName);
        }
    }
}
