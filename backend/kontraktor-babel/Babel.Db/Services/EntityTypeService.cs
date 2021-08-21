using System;
using System.Collections.Generic;
using System.Text;
using Babel.Db.Models.Entities;
using Babel.Db.Settings;

namespace Babel.Db.Services
{
    public class EntityTypeService: BaseMongoService<EntityType>
    {
        public EntityTypeService(IBaseDatabaseSettings settings) : base(settings)
        {
        }
    }
}
