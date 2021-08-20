using System;
using System.Collections.Generic;
using System.Text;

namespace Babel.Db.Models
{
    public class Level: BaseModel
    {
        public int Value { get; set; }
        public string Image { get; set; }
        public string Title { get; set; }
    }
}
