using System;
using System.Collections.Generic;
using System.Text;
using kontaktor_network.DA.Models;

namespace KONTAKTOR.DA.Mongo.Repository
{
    public class MainNewsRepository : BaseMongoRepository<MainNews>
    {
        public MainNewsRepository(MongoConnectionOptions settings) : base(settings)
        {
        }
    }
}
