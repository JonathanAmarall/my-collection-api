using MyCollection.Core.Contracts;
using MyCollection.Domain.Events;

namespace MyCollection.Application.Borrower.Events
{
    public sealed class CreatedBorrowerIntegrationEvent : IIntegrationEvent
    {
        public CreatedBorrowerIntegrationEvent(CreatedBorrowerDomainEvent @event) => BorrowerId = @event.Borrower.Id;


        public Guid BorrowerId { get; set; }
    }
}
