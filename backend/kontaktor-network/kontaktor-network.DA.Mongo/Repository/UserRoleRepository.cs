using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using KONTAKTOR.DA.Models;
using MongoDB.Driver;

namespace KONTAKTOR.DA.Mongo.Repository
{
    public class UserRoleRepository : BaseMongoRepository<UserRole>
    {
        public UserRoleRepository(MongoConnectionOptions settings) : base(settings)
        {
        }

        public async Task RemoveAll()
        {
            await _collection.DeleteManyAsync(FilterDefinition<UserRole>.Empty);
        }
    }
}
