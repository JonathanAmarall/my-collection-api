using MyCollection.Core.Contracts;
using MyCollection.Domain.Entities;

namespace MyCollection.Domain.Events
{
    public class CreatedCollectionItemDomainEvent : IDomainEvent
    {
        public CreatedCollectionItemDomainEvent(CollectionItem item)
        {
            Item = item;
        }

        public CollectionItem Item { get; private set; }
    }
}