using System;
using System.Collections.Generic;
using System.Text;

namespace Babel.Db.Models.Entities
{
    public class EntityType: BaseModel
    {
        public string Type { get; set; }
        public string Description { get; set; }
    }
}
