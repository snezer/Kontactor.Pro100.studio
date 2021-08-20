using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using KONTRAKTOR.DA.Models;
using MongoDB.Driver;

namespace KONTAKTOR.DA.Mongo.Repository
{
    public class RentFactRepository: BaseMongoRepository<RentFact>
    {
        public RentFactRepository(MongoConnectionOptions settings, UserInformationRepository users) : base(settings)
        {
        }

        public async Task<IEnumerable<RentFact>> GetRentsForTenantId(string id)
        {

            var result = (await _collection.FindAsync<RentFact>(x => x.TenantId == id)).ToEnumerable();
            return result;
        }

        public async Task<IEnumerable<RentFact>> GetRentsForCompartment(string id)
        {

            var result = (await _collection.FindAsync<RentFact>(x => x.CompartmentId == id)).ToEnumerable();
            return result;
        }

        public async Task<IEnumerable<RentFact>> GetRentsForCompartment(string id, DateTime from, DateTime to)
        {

            var result = (await _collection.FindAsync<RentFact>(x => x.CompartmentId == id && x.ContractStart>=from && x.ContractEnd<=to)).ToEnumerable();
            return result;
        }
    }
}
