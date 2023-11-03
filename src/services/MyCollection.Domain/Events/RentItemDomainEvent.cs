using MyCollection.Core.Contracts;
using MyCollection.Domain.Entities;

namespace MyCollection.Domain.Events
{
    public sealed class RentItemDomainEvent : IDomainEvent
    {
        internal RentItemDomainEvent(Borrower borrower, CollectionItem item, int rentedQuantity)
        {
            Borrower = borrower;
            Item = item;
            RentedQuantity = rentedQuantity;
        }

        public Borrower Borrower { get; set; }
        public CollectionItem Item { get; set; }
        public int RentedQuantity { get; set; }
    }
}