using System;
using System.Collections.Generic;
using System.Text;
using KONTAKTOR.DA.Models;

namespace KONTAKTOR.DA.Mongo.Repository
{
    public class MessageRepository:BaseMongoRepository<Message>
    {
        public MessageRepository(MongoConnectionOptions settings) : base(settings)
        {
        }
    }
}
