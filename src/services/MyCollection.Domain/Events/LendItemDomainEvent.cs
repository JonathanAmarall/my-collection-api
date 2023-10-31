using MyCollection.Core.Contracts;
using MyCollection.Domain.Entities;

namespace MyCollection.Domain.Events
{
    public sealed class LendItemDomainEvent : IDomainEvent
    {
        internal LendItemDomainEvent(Guid borrowerId, CollectionItem item)
        {
            BorrowerId = borrowerId;
            Item = item;
        }

        public Guid BorrowerId { get; set; }
        public CollectionItem Item { get; set; }
    }
}