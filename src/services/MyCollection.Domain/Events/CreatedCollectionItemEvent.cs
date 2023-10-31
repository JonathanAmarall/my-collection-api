using MyCollection.Core.Contracts;
using MyCollection.Domain.Entities;

namespace MyCollection.Domain.Events
{
    public class CreatedCollectionItemEvent : IDomainEvent
    {
        public CreatedCollectionItemEvent(CollectionItem item)
        {
            Item = item;
        }

        public CollectionItem Item { get; private set; }
    }
}