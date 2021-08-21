using System;
using System.Collections.Generic;
using System.Text;

namespace KONTAKTOR.DA.Models
{
    public class Chat : BaseModel
    {
        public string Subject { get; set; }

        public string ExternalId { get; set; }
    }
}
