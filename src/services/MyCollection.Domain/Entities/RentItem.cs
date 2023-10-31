using MyCollection.Core.Contracts;
using MyCollection.Core.Exceptions;
using MyCollection.Core.Models;
using MyCollection.Domain.Events;

namespace MyCollection.Domain.Entities
{
    public class RentItem : AggregateRoot, IAuditableEntity
    {
        private const int RentDueInDays = 5;

        public RentItem(int rentedQuantity, CollectionItem collectionItem, Borrower borrower)
        {
            if (rentedQuantity <= 0)
            {
                throw new DomainException("Rent Quantity is invalid.");
            }

            RentDueDate = DateTime.Now.AddDays(RentDueInDays);
            RentedQuantity = rentedQuantity;
            CollectionItemId = collectionItem.Id;
            CollectionItem = collectionItem;
            BorrowerId = borrower.Id;
            Borrower = borrower;
        }

        public int RentedQuantity { get; private set; }
        public int QuantityReturned { get; private set; }

        public DateTime RentDueDate { get; private set; }

        public Guid CollectionItemId { get; private set; }
        public CollectionItem CollectionItem { get; private set; }

        public Guid BorrowerId { get; private set; }
        public Borrower Borrower { get; private set; }

        public DateTime CreatedAt { get; private set; }
        public DateTime? UpdatedAt { get; }

        public bool AllItemsReturned() => RentedQuantity == QuantityReturned;

        public void ReturnItem(int quantityReturned)
        {
            if (quantityReturned > RentedQuantity)
            {
                throw new DomainException("Quantity Returned is invalid.");
            }

            QuantityReturned = quantityReturned;
            AddDomainEvent(new ReturnItemDomainEvent(BorrowerId, CollectionItem, quantityReturned));
        }
    }
}
