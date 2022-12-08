using MyCollection.Domain.Entities;
using MongoDB.Driver;
using Microsoft.Extensions.Configuration;

namespace MyCollection.Data
{
    public class MongoDBClient : IMongoDBClient
    {
        private readonly IMongoDatabase _mongoDatabase;

        public MongoDBClient(IConfiguration configuration)
        {
            var client = new MongoClient(configuration.GetConnectionString("MongoConnection"));
            _mongoDatabase = client.GetDatabase("mycollection");
        }

        public void DeleteOne<T>(Guid id) where T : EntityBase
            => GetCollection<T>().DeleteOne(x => x.Id == id);

        public IQueryable<T> Get<T>() where T : EntityBase
            => GetCollection<T>().AsQueryable();

        public void InsertOne<T>(T obj) where T : EntityBase
            => GetCollection<T>().InsertOne(obj);

        public void InsertMany<T>(T obj) where T : EntityBase
            => GetCollection<T>().InsertOne(obj);

        public void ReplaceOne<T>(T obj) where T : EntityBase
            => GetCollection<T>().ReplaceOne(x => x.Id == obj.Id, obj);

        private IMongoCollection<T> GetCollection<T>()
           => _mongoDatabase.GetCollection<T>(nameof(T));
    }
}