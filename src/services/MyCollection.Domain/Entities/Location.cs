using MyCollection.Domain.Contracts;

namespace MyCollection.Domain.Entities
{
    public class Location : EntityBase, IAggregateRoot
    {
        private ICollection<CollectionItem>? _colletionItems;
        private ICollection<Location>? _childrens;

        public Location(string initials, string description, Guid? parentId)
        {
            Initials = initials.ToUpper();
            Description = description;
            ParentId = parentId;
        }

        public string Initials { get; private set; }
        public string Description { get; private set; }

        // EF Rel.
        public ICollection<Location>? Childrens { get => _childrens; }
        public ICollection<CollectionItem>? CollectionItems { get => _colletionItems; }

        public Guid? ParentId { get; private set; }
        public Location? Parent { get; private set; }

        public bool HasChildren()
        {
            return _childrens?.Count > 0;
        }

        public bool HasParent()
        {
            return ParentId != Guid.Empty;
        }

        public void AddChildrens(ICollection<Location> productLocations)
        {
            foreach (var productLocation in productLocations)
            {
                _childrens?.Add(productLocation);
            }

            UpdateAt = DateTime.Now;
        }

        public void LinkACollectionItem(CollectionItem item)
        {
            if (_colletionItems == null)
                _colletionItems = new List<CollectionItem>();

            _colletionItems?.Add(item);
        }
    }


}
