using MyCollection.Domain.Entities;

namespace MyCollection.Data
{
    public interface IMongoDBClient
    {
        IQueryable<T> Get<T>() where T : EntityBase;

        void InsertOne<T>(T obj) where T : EntityBase;
        void InsertMany<T>(T obj) where T : EntityBase;
        void ReplaceOne<T>(T obj) where T : EntityBase;

        void DeleteOne<T>(Guid id) where T : EntityBase;
    }
}