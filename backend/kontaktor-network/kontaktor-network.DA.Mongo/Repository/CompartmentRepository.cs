using System;
using System.Collections.Generic;
using System.Text;
using KONTAKTOR.DA.Models;

namespace KONTAKTOR.DA.Mongo.Repository
{
    public class CompartmentRepository : BaseMongoRepository<Compartment>
    {
        public CompartmentRepository(MongoConnectionOptions settings) : base(settings)
        {
        }
    }
}
