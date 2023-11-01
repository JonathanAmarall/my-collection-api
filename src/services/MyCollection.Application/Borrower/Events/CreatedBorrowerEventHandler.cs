using MyCollection.Core.Contracts;
using MyCollection.Domain.Events;
using MyCollection.MessageBus;

namespace MyCollection.Application.Borrower.Events
{
    internal class CreatedBorrowerEventHandler : IDomainEventHandler<CreatedBorrowerEvent>
    {
        private readonly IMessageBus _messageBus;

        public CreatedBorrowerEventHandler(IMessageBus messageBus)
        {
            _messageBus = messageBus;
        }

        public async Task Handle(CreatedBorrowerEvent notification, CancellationToken cancellationToken)
        {
            _messageBus.Publish(new CreatedBorrowerIntegrationEvent(notification.Borrower.Id));
            await Task.CompletedTask;
        }
    }
}
