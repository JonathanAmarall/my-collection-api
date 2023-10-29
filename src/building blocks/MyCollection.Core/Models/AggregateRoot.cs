using MyCollection.Core.Contracts;

namespace MyCollection.Core.Models
{
    public abstract class AggregateRoot : EntityBase
    {
        protected AggregateRoot() { }

        protected AggregateRoot(Guid id) : base(id) { }

        private readonly List<IDomainEvent> _domainEvents = new List<IDomainEvent>();

        public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

        public void ClearDomainEvents() => _domainEvents.Clear();

        protected void AddDomainEvent(IDomainEvent domainEvent) => _domainEvents.Add(domainEvent);
    }
}
