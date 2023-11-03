using MyCollection.Core.Contracts;
using MyCollection.Domain.Events;

namespace MyCollection.Application.CollectionItem.Events
{
    public sealed class RentItemIntegrationEvent : IIntegrationEvent
    {
        public Guid BorrowerId { get; set; }
        public int RentedQuantity { get; set; }
        public DateTime DueDate { get; set; }
        public string ItemTitle { get; set; }

        internal RentItemIntegrationEvent(RentItemDomainEvent rentItemDomainEvent, DateTime dueDate, string itemTitle)
        {
            BorrowerId = rentItemDomainEvent.Borrower.Id;
            DueDate = dueDate;
            RentedQuantity = rentItemDomainEvent.RentedQuantity;
            ItemTitle = itemTitle;
        }

        public RentItemIntegrationEvent(Guid borrowerId, int rentedQuantity, DateTime dueDate, string itemTitle)
        {
            BorrowerId = borrowerId;
            RentedQuantity = rentedQuantity;
            DueDate = dueDate;
            ItemTitle = itemTitle;
        }
    }
}
