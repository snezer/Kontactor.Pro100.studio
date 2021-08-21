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

        public async Task<UserInformation> FindByLogin(string modelLogin)
        {
            var users = await _collection.FindAsync(u => u.Login == modelLogin);
            return users.SingleOrDefault();
        }
    }
}
