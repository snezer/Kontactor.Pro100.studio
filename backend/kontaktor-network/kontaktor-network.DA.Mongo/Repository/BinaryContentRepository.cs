using System;
using System.Collections.Generic;
using System.Text;
using KONTAKTOR.DA.Models;

namespace KONTAKTOR.DA.Mongo.Repository
{
    public class BinaryContentRepository : BaseMongoRepository<BinaryContent>
    {
        public BinaryContentRepository(MongoConnectionOptions settings) : base(settings)
        {
        }
    }
}
