using MyCollection.Application.Borrower.Events;
using MyCollection.Core.Contracts;

namespace MyCollection.BackgroundTasks.IntegrationEvents.Borrowers
{
    internal class CreatedBorrowerIntegrationEventHandler : IIntegrationEventHandler<CreatedBorrowerIntegrationEvent>
    {
        public Task Handle(CreatedBorrowerIntegrationEvent notification, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
