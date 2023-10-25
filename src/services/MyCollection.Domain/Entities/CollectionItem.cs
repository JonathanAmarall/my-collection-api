using MyCollection.Core.Contracts;
using MyCollection.Core.Models;

namespace MyCollection.Domain.Entities
{
    public class CollectionItem : EntityBase, IAggregateRoot
    {
        private List<Borrower> _contacts = new();

        public CollectionItem(string title, string autor, int quantity, string? edition, EType itemType)
        {
            Title = title;
            Autor = autor;
            Quantity = quantity;
            Edition = edition;
            ItemType = itemType;

            Status = ECollectionStatus.AVAILABLE;
        }

        public string Title { get; private set; }
        public string Autor { get; private set; }
        public int Quantity { get; private set; }
        public string? Edition { get; private set; }

        public EType ItemType { get; private set; }
        public ECollectionStatus Status { get; private set; }

        // EF Rel.
        public ICollection<Borrower> Contacts
        {
            get => _contacts;
        }

        public Guid? LocationId { get; private set; }
        public Location? Location { get; private set; }

        public void LendOneItem(Borrower borrower)
        {
            _contacts.Add(borrower);

            Quantity--;
            if (Quantity == 0)
                Status = ECollectionStatus.UNAVAILABLE;

            UpdateAt = DateTime.Now;
        }

        public void RecoveredItem(Borrower borrower)
        {
            _contacts?.Remove(borrower);

            Quantity++;
            Status = ECollectionStatus.AVAILABLE;
            UpdateAt = DateTime.Now;
        }

        public void AddLocation(Location location)
        {
            LocationId = location.Id;
            Location = location;

            UpdateAt = DateTime.Now;
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