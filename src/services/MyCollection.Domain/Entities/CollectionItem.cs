using MyCollection.Domain.Contracts;

namespace MyCollection.Domain.Entities
{
    public class CollectionItem : EntityBase, IAggregateRoot
    {
        private IList<Contact>? _contacts;
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
        public ICollection<Contact>? Contacts { get => _contacts; }

        public Guid? LocationId { get; private set; }
        public Location Location { get; private set; }

        public void LendOneItem(Contact contact)
        {
            if (_contacts == null)
                _contacts = new List<Contact>();

            _contacts.Add(contact);

            Quantity--;
            if (Quantity == 0)
                Status = ECollectionStatus.UNAVAILABLE;

            UpdateAt = DateTime.Now;
        }


        public void RecoveredItem(Contact contact)
        {
            _contacts?.Remove(contact);

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

        public bool ICanLend()
        {
            return Quantity > 0 && Status == ECollectionStatus.AVAILABLE;
        }

        public bool HasLocation()
        {
            //if (LocationId == null)
            //    return false;

            return true;
        }

        public string GetAbbreviatedLocation()
        {
            //if (Location == null)
            //    return "";

            //return Location.Initials;
            return "";
        }

    }
}
