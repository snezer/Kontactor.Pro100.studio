using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using kontaktor_network.DA.Models;
using MongoDB.Driver;

namespace KONTAKTOR.DA.Mongo.Repository
{
    public class UserInformationRepository : BaseMongoRepository<UserInformation>
    {
        public UserInformationRepository(MongoConnectionOptions settings) : base(settings)
        {
        }

        public async Task RemoveAll()
        {
            await _collection.DeleteManyAsync(FilterDefinition<UserInformation>.Empty);
        }
    }
}
