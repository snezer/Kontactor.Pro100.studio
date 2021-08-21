using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using KONTAKTOR.DA.Models;
using MongoDB.Driver;

namespace KONTAKTOR.DA.Mongo.Repository
{
    public class CompartmentRepository : BaseMongoRepository<Compartment>
    {
        public CompartmentRepository(MongoConnectionOptions settings) : base(settings)
        {
        }

        public async Task<Compartment> GetByMapIdAsync(string id)
        {
            var result = (await _collection.FindAsync<Compartment>(x => x.MapRoomId == id)).SingleOrDefault();
            return result;
        }
    }
}
