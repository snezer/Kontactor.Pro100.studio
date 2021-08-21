using System;
using System.Collections.Generic;
using System.Text;
using Babel.Db.Models;
using Babel.Db.Settings;

namespace Babel.Db.Services
{
    public class LevelService: BaseMongoService<Level>
    {
        public LevelService(IBaseDatabaseSettings settings) : base(settings)
        {
        }
    }
}
