using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using KONTAKTOR.DA.Models;
using KONTAKTOR.DA.Repository;

namespace KONTAKTOR.DA.Mongo.Repository
{
    public class OccupationRepository : BaseMongoRepository<Occupation>, IOccupationRepository
    {
        public OccupationRepository(MongoConnectionOptions settings) : base(settings)
        {
        }

        public async Task<IEnumerable<Occupation>> GetOccupationsAsync()
        {
            return await this.Get();
        }

        public async Task<Occupation> CreateAsync(Occupation model)
        {
            return await this.Create(model);
        }

        public async Task<bool> UpdateAsync(Occupation model)
        {
            return await this.Update(model.Id, model);
        }
    }
}
