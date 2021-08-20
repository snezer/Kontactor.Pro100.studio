using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using KONTAKTOR.DA.Models;
using KONTAKTOR.DA.Repository;

namespace KONTAKTOR.DA.Mongo.Repository
{
    public class OccupationRepository : BaseMongoRepository<Occupation>
    {
        public OccupationRepository(MongoConnectionOptions settings) : base(settings)
        {
        }
    }
}
