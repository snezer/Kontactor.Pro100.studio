using System;
using System.Collections.Generic;
using System.Text;

namespace Babel.Db.Models.Book
{
    public class Attribute: BaseModel
    {
        public string AttributeGroup { get; set; }
        public string AttributeName { get; set; }
    }
}
