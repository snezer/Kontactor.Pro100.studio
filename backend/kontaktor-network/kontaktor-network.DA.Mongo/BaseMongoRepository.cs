using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using KONTAKTOR.DA.Models;
using MongoDB.Driver;

namespace KONTAKTOR.DA.Mongo
{
    public class BaseMongoRepository<T> where T : BaseModel
    {
        protected readonly IMongoCollection<T> _collection;

        public BaseMongoRepository(MongoConnectionOptions settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.Database);

            _collection = database.GetCollection<T>(typeof(T).Name);
        }

        public async Task<List<T>> Get()
        {
            return (await _collection.FindAsync(_ => true)).ToList();
        }

        public async Task<T> Get(string id)
        {
            return (await _collection.FindAsync<T>(x => x.Id == id)).FirstOrDefault();
        }

        public async Task<T> Create(T t)
        {
            t.Id = null;
            await _collection.InsertOneAsync(t);
            return t;
        }

        public virtual async Task<bool> Update(string id, T t)
        {
            var result = await _collection.ReplaceOneAsync<T>(x => x.Id == id, t);
            return result.IsAcknowledged;
        }

        public async Task Remove(T t)
        {
            await _collection.DeleteOneAsync<T>(x => x.Id == t.Id);
        }

        public virtual async Task Remove(string id)
        {
            await _collection.DeleteOneAsync<T>(x => x.Id == id);
        }
    }
}
