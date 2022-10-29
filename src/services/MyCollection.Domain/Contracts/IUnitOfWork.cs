namespace MyCollection.Domain.Contracts
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();
    }
}
