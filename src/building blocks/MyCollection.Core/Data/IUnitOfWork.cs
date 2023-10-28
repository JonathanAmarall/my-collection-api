namespace MyCollection.Core.Data
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();
    }
}
