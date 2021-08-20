using System;
using System.Collections.Generic;
using System.Text;

namespace kontaktor_network.DA.Models
{
    public class Compartment
    {
        public string Name { get; set; }
        public string ShortNameOrCode { get; set; }
        public bool IsForRent { get; set; }
        public bool IsForLongTermRent { get; set; }
    }
}
