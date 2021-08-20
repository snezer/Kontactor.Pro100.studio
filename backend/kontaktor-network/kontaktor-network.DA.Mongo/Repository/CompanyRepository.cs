using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using KONTAKTOR.DA.Models;
using KONTAKTOR.DA.Repository;

namespace KONTAKTOR.DA.Mongo.Repository
{
    public class CompanyRepository:  BaseMongoRepository<Company>, ICompanyRepository
    {
        public CompanyRepository(MongoConnectionOptions settings) : base(settings)
        {
        }

        public async Task<Company> GetByIdAsync(string id, IDbTransaction transaction = null)
        {
            return await Get(id);
        }


        public async Task<Company> CreateAsync(Company company)
        {
            return await this.Create(company);
        }

        public async Task<bool> UpdateAsync(Company company)
        {
            return await this.Update(company.Id, company);
        }
    }
}
