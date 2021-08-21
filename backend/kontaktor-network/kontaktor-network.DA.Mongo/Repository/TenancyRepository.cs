using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using kontaktor_network.DA.Models;
using MongoDB.Driver;

namespace KONTAKTOR.DA.Mongo.Repository
{
    public class TenancyRepository : BaseMongoRepository<Tenant>
    {
        public TenancyRepository(MongoConnectionOptions settings) : base(settings)
        {
        }

        public async Task<Tenant> GetTenantForUserAsync(string userid)
        {
            var tenant = (await _collection.FindAsync<Tenant>(x => x.UserInformationId == userid)).FirstOrDefault();
            return tenant;
        }

        public async Task<Tenant> GetTenantForCompanyAsync(string companyid)
        {
            var tenant = (await _collection.FindAsync<Tenant>(x => x.CompanyId == companyid)).FirstOrDefault();
            return tenant;
        }
    }
}
