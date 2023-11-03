using MyCollection.Core.Contracts;
using MyCollection.Domain.Events;

namespace MyCollection.Application.Borrower.Events
{
    public sealed class CreatedBorrowerIntegrationEvent : IIntegrationEvent
    {
        public CreatedBorrowerIntegrationEvent(Guid borrowerId) => BorrowerId = borrowerId;

        internal CreatedBorrowerIntegrationEvent(CreatedBorrowerDomainEvent domainEvent) => BorrowerId = domainEvent.Borrower.Id;

        public Guid BorrowerId { get; set; }
    }
}
