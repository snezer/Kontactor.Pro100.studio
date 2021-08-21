using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Babel.Db.Models.Entities;
using Babel.Db.Settings;
using MongoDB.Driver;

namespace Babel.Db.Services
{
    public class EntityService: BaseMongoService<Entity>
    {
        public EntityService(IBaseDatabaseSettings settings) : base(settings)
        {
        }

        public async Task<List<Entity>> GetEntitiesByLevel(string level)
        {
            var entities = (await _collection.FindAsync(x => x.LevelId == level)).ToList();
            return entities;
        }

        public async Task<List<Entity>> GetEntitiesByType(string type)
        {
            var entities = (await _collection.FindAsync(x => x.Type.ToLower() == type.ToLower())).ToList();
            return entities;
        }

        public async Task<List<Entity>> GetEntitiesByTypeAndLevel(string type, string level)
        {
            var entities = (await _collection.FindAsync(x => x.Type.ToLower() == type.ToLower())).ToList();
            return entities;
        }
    }
}
