using System;
using System.Collections.Generic;
using System.Text;
using KONTAKTOR.DA.Models;

namespace KONTAKTOR.DA.Mongo.Repository
{
    public class ChatRepository: BaseMongoRepository<Chat>
    {
        public ChatRepository(MongoConnectionOptions settings) : base(settings)
        {
        }
    }
}
