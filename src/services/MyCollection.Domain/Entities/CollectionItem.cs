using MyCollection.Core.Contracts;
using MyCollection.Core.Models;
using MyCollection.Domain.Events;

namespace MyCollection.Domain.Entities
{
    public class CollectionItem : AggregateRoot, IAuditableEntity
    {
        private readonly List<Borrower> _borrowers = new();

        public CollectionItem(string title, string autor, int quantity, string? edition, EType itemType)
        {
            Title = title;
            Autor = autor;
            Quantity = quantity;
            Edition = edition;
            ItemType = itemType;

            Status = ECollectionStatus.AVAILABLE;

            AddDomainEvent(new CreatedCollectionItemEvent(this));
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
        public ICollection<Borrower> Borrowers
        {
            get => _borrowers;
        }

        public Guid? LocationId { get; private set; }
        public Location? Location { get; private set; }

        public void LendOneItem(Borrower borrower)
        {
            _borrowers.Add(borrower);

            Quantity--;
            if (Quantity == 0)
            {
                Status = ECollectionStatus.UNAVAILABLE;
            }

            AddDomainEvent(new RentItemDomainEvent(borrower.Id, this));
        }

        public void RecoveredItem(Borrower borrower)
        {
            _borrowers?.Remove(borrower);

            Quantity++;
            Status = ECollectionStatus.AVAILABLE;
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