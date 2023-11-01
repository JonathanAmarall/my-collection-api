using MyCollection.Core.Contracts;

namespace MyCollection.Application.Borrower.Events
{
    public sealed class CreatedBorrowerIntegrationEvent : IIntegrationEvent
    {
        public CreatedBorrowerIntegrationEvent(Guid borrowerId) => BorrowerId = borrowerId;

        public Guid BorrowerId { get; set; }
    }
}
