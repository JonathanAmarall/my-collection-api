using MyCollection.Core.Contracts;
using MyCollection.Domain.Entities;

namespace MyCollection.Domain.Events
{
    public sealed class RentItemDomainEvent : IDomainEvent
    {
        internal RentItemDomainEvent(Guid borrowerId, CollectionItem item, int rentedQuantity)
        {
            BorrowerId = borrowerId;
            Item = item;
            RentedQuantity = rentedQuantity;
        }

        public Guid BorrowerId { get; set; }
        public CollectionItem Item { get; set; }
        public int RentedQuantity { get; set; }
    }
}