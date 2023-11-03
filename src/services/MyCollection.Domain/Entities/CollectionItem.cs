using MyCollection.Core.Contracts;
using MyCollection.Core.Exceptions;
using MyCollection.Core.Models;
using MyCollection.Domain.Events;

namespace MyCollection.Domain.Entities
{
    public class CollectionItem : AggregateRoot, IAuditableEntity
    {
        public CollectionItem(string title, string autor, int quantity, string? edition, EType itemType)
        {
            Title = title;
            Autor = autor;
            Quantity = quantity;
            Edition = edition;
            ItemType = itemType;

            Status = ECollectionStatus.AVAILABLE;

            AddDomainEvent(new CreatedCollectionItemDomainEvent(this));
        }

        public string Title { get; private set; }
        public string Autor { get; private set; }
        public int Quantity { get; private set; }
        public string? Edition { get; private set; }
        public EType ItemType { get; private set; }
        public ECollectionStatus Status { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? UpdatedAt { get; }

        // EF Rel.
        public Guid? LocationId { get; private set; }
        public Location? Location { get; private set; }
        public IReadOnlyCollection<RentItem>? Rentals { get; private set; } = new List<RentItem>();

        public void RentItem(Borrower borrower, int rentQuantity)
        {
            if (rentQuantity > Quantity)
            {
                throw new DomainException("Quantity for rent unavailable.");
            }

            Quantity--;

            if (Quantity == 0)
            {
                Status = ECollectionStatus.UNAVAILABLE;
            }

            AddDomainEvent(new RentItemDomainEvent(borrower.Id, this, rentQuantity));
        }

        public void RecoveredItem(Borrower borrower)
        {
            Quantity++;
            Status = ECollectionStatus.AVAILABLE;
            // TODO: Disparar evento
        }

        public void AddLocation(Location location)
        {
            LocationId = location.Id;
            Location = location;
        }

        public bool CanLend()
        {
            return Quantity > 0 && Status == ECollectionStatus.AVAILABLE;
        }

        public bool HasLocation()
        {
            return LocationId != null;
        }

        public string GetAbbreviatedLocation()
        {
            return Location is null ? string.Empty : Location.Initials;
        }
    }
}