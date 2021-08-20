using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using KONTAKTOR.DA.Models;
using KONTAKTOR.DA.Repository;

namespace KONTAKTOR.DA.Mongo.Repository
{
    public class CompanyRepository:  BaseMongoRepository<Company>
    {
        public CompanyRepository(MongoConnectionOptions settings) : base(settings)
        {
        }
    }
}
