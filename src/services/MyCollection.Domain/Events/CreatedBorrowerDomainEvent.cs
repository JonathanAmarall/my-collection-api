using MyCollection.Core.Contracts;
using MyCollection.Domain.Entities;

namespace MyCollection.Domain.Events
{
    public record CreatedBorrowerDomainEvent(Borrower Borrower) : IDomainEvent
    {

    }
}
