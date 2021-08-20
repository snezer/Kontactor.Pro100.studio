using System;
using System.Collections.Generic;
using System.Text;
using KONTAKTOR.DA.Models;

namespace KONTAKTOR.DA.Models
{
    public class Compartment : BaseModel
    {
        public string Name { get; set; }
        public string ShortNameOrCode { get; set; }
        public bool IsForRent { get; set; }
        public bool IsForLongTermRent { get; set; }
    }
}
