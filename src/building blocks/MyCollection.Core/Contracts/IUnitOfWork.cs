namespace MyCollection.Core.Contracts
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();
    }
}
