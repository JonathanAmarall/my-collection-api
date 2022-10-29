using MyCollection.Domain.Contracts;

namespace MyCollection.Domain.Contracts
{
    public interface IRepository<T> : IDisposable where T : IAggregateRoot
    {
        IUnitOfWork UnitOfWork { get; }
    }
}
