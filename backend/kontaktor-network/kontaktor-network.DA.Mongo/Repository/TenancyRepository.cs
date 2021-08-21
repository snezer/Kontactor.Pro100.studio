using System;
using System.Collections.Generic;
using System.Text;
using kontaktor_network.DA.Models;

namespace KONTAKTOR.DA.Mongo.Repository
{
    public class TenancyRepository : BaseMongoRepository<Tenant>
    {
        public TenancyRepository(MongoConnectionOptions settings) : base(settings)
        {
        }
    }
}
