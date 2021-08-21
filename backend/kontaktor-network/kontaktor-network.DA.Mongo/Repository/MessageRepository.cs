using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using KONTAKTOR.DA.Models;
using MongoDB.Driver;

namespace KONTAKTOR.DA.Mongo.Repository
{
    public class MessageRepository:BaseMongoRepository<Message>
    {
        public MessageRepository(MongoConnectionOptions settings) : base(settings)
        {
        }

        public async Task<IEnumerable<Message>> GetForChatAsync(string chatId)
        {
            var result = (await _collection.FindAsync<Message>(m => m.ChatId == chatId)).ToEnumerable();
            return result;
        }
    }
}
