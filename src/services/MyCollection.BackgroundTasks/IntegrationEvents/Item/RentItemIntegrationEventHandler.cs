using Microsoft.Extensions.Logging;
using MyCollection.Application.CollectionItem.Events;
using MyCollection.Core.Contracts;
using MyCollection.Core.Email;
using MyCollection.Core.Models;
using MyCollection.Domain.Repositories;

namespace MyCollection.BackgroundTasks.IntegrationEvents.Item
{
    internal class RentItemIntegrationEventHandler : IIntegrationEventHandler<RentItemIntegrationEvent>
    {
        private readonly IEmailService _emailService;
        private readonly IBorrowerRepository _borrowerRepository;
        private readonly ILogger<RentItemIntegrationEventHandler> _logger;

        public RentItemIntegrationEventHandler(IEmailService emailService, ILogger<RentItemIntegrationEventHandler> logger, IBorrowerRepository borrowerRepository)
        {
            _emailService = emailService;
            _logger = logger;
            _borrowerRepository = borrowerRepository;
        }

        public async Task Handle(RentItemIntegrationEvent notification, CancellationToken cancellationToken)
        {
            var borrower = await _borrowerRepository.GetByIdAsync(notification.BorrowerId);

            if(borrower is null)
            {
                _logger.LogError("BorrowerId is invalid: {id}", notification.BorrowerId);
                throw new ArgumentNullException(nameof(borrower));
            }

            var (subject, body) = MailTemplates.CreateRentedMessageBorrowerEmail(
                notification.ItemTitle,
                notification.DueDate,
                borrower.FullName,
                borrower.Email.Value);

            var mailRequest = new MailRequest(borrower.Email.Value, subject, body);

            await _emailService.SendEmailAsync(mailRequest);

            _logger.LogInformation("Enviando email para {email} do Borrower {fullName}", borrower.Email.Value, borrower.FullName);
        }
    }
}
