using MyCollection.Core.Contracts;
using MyCollection.Domain.Events;

namespace MyCollection.Application.CollectionItem.Events
{
    public sealed class RentItemIntegrationEvent : IIntegrationEvent
    {
        public Domain.Entities.Borrower Borrower { get; set; }
        public int RentedQuantity { get; set; }
        public DateTime DueDate { get; set; }
        public string ItemTitle { get; set; }

        internal RentItemIntegrationEvent(RentItemDomainEvent rentItemDomainEvent, DateTime dueDate, string itemTitle)
        {
            Borrower = rentItemDomainEvent.Borrower;
            DueDate = dueDate;
            RentedQuantity = rentItemDomainEvent.RentedQuantity;
            ItemTitle = itemTitle;
        }

        public RentItemIntegrationEvent(Domain.Entities.Borrower borrower, int rentedQuantity, DateTime dueDate, string itemTitle)
        {
            Borrower = borrower;
            RentedQuantity = rentedQuantity;
            DueDate = dueDate;
            ItemTitle = itemTitle;
        }
    }
}
