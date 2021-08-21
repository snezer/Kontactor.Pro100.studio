using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using KONTAKTOR.DA.Models;
using KONTAKTOR.DA.Repository;
using MongoDB.Driver;

namespace KONTAKTOR.DA.Mongo.Repository
{
    public class CompanyRepository:  BaseMongoRepository<Company>
    {
        public CompanyRepository(MongoConnectionOptions settings) : base(settings)
        {
        }

        public async Task RemoveAll()
        {
            await _collection.DeleteManyAsync(FilterDefinition<Company>.Empty);
        }
    }
}
