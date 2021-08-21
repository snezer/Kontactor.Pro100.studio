using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KONTAKTOR.DA.Models;
using MongoDB.Driver;

namespace KONTAKTOR.DA.Mongo.Repository
{
    public class ChatRepository: BaseMongoRepository<Chat>
    {
        public ChatRepository(MongoConnectionOptions settings) : base(settings)
        {
        }

        public async Task<Chat> GetByExtIdAsync(string initId)
        {
            return (await _collection.FindAsync<Chat>(x => x.ExternalId == initId)).FirstOrDefault();
        }

        public async Task<IEnumerable<Chat>> GetForUser(string userid)
        {
            return (await _collection.FindAsync<Chat>(x => x.Users.Contains(userid))).ToEnumerable();
        }
    }
}
