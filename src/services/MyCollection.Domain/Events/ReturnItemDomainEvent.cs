using MyCollection.Core.Contracts;
using MyCollection.Domain.Entities;

namespace MyCollection.Domain.Events
{
    public sealed class ReturnItemDomainEvent : IDomainEvent
    {
        internal ReturnItemDomainEvent(Guid borrowerId, CollectionItem item, int quantityReturned)
        {
            BorrowerId = borrowerId;
            Item = item;
            QuantityReturned = quantityReturned;
        }

        public int QuantityReturned { get; set; }
        public Guid BorrowerId { get; set; }
        public CollectionItem Item { get; set; }
    }
}