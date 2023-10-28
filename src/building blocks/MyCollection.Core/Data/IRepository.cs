using MyCollection.Core.Models;

namespace MyCollection.Core.Data
{
    public interface IRepository<T> : IDisposable where T : AggregateRoot
    {
        IUnitOfWork UnitOfWork { get; }
    }
}
