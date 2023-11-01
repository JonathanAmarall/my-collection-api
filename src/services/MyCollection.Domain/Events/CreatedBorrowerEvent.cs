using MyCollection.Core.Contracts;
using MyCollection.Domain.Entities;

namespace MyCollection.Domain.Events
{
    public record CreatedBorrowerEvent(Borrower Borrower) : IDomainEvent
    {

    }
}
