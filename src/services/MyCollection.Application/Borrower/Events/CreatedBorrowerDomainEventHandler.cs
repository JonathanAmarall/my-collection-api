using Microsoft.Extensions.Logging;
using MyCollection.Core.Contracts;
using MyCollection.Domain.Events;
using MyCollection.MessageBus;

namespace MyCollection.Application.Borrower.Events
{
    internal class CreatedBorrowerDomainEventHandler : IDomainEventHandler<CreatedBorrowerDomainEvent>
    {
        private readonly IMessageBus _messageBus;
        private readonly ILogger<CreatedBorrowerDomainEventHandler> _logger;


        public CreatedBorrowerDomainEventHandler(IMessageBus messageBus, ILogger<CreatedBorrowerDomainEventHandler> logger)
        {
            _messageBus = messageBus;
            _logger = logger;
        }

        public async Task Handle(CreatedBorrowerDomainEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Received DomainEvent");

            _messageBus.Publish(new CreatedBorrowerIntegrationEvent(notification));
            await Task.CompletedTask;
        }
    }
}
