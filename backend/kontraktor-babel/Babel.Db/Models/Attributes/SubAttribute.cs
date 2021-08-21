using System;
using System.Collections.Generic;
using System.Text;

namespace Babel.Db.Models.Attributes
{
    public class SubAttribute: BaseModel
    {
        public string AttributeId { get; set; }
        public string SubattributeName { get; set; }
        public string SubattributeCode { get; set; }
        public string Description { get; set; }
    }
}
