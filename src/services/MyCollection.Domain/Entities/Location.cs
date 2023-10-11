using MyCollection.Core.Models;
using MyCollection.Domain.Contracts;

namespace MyCollection.Domain.Entities
{
    public class Location : EntityBase, IAggregateRoot
    {
        private IList<Location>? _childrens;
        private IList<CollectionItem>? _colletionItems;

        public Location(string initials, string description, Guid? parentId, int level)
        {
            Initials = initials.ToUpper();
            Description = description;
            ParentId = parentId;

            _colletionItems = new List<CollectionItem>();
            Level = level;
        }

        public string Initials { get; private set; }
        public string Description { get; private set; }
        public int Level { get; private set; }

        // EF Rel.
        public ICollection<Location>? Childrens
        {
            get => _childrens;
        }

        public ICollection<CollectionItem>? CollectionItems
        {
            get => _colletionItems;
        }

        public Guid? ParentId { get; private set; }
        public Location? Parent { get; private set; }

        public bool HasChildren()
        {
            _childrens ??= new List<Location>();
            return _childrens.Count > 0;
        }

        public bool HasParent()
        {
            return ParentId != Guid.Empty;
        }

        public void AddChildrens(ICollection<Location> itemLocations)
        {
            _childrens ??= new List<Location>();

            foreach (var item in itemLocations)
            {
                _childrens.Add(item);
            }

            UpdateAt = DateTime.Now;
        }

        public void LinkACollectionItem(CollectionItem item)
        {
            _colletionItems!.Add(item);
        }

        public bool HasCollectionItem()
        {
            _colletionItems ??= new List<CollectionItem>();
            return _colletionItems?.Count > 0;
        }
    }
}