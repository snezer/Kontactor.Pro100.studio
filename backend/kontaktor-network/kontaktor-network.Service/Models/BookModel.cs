using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KONTAKTOR.Service.Models
{
    public class BookModel
    {
        public string TenantId { get; set; }
        public string CompartmentId { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
    }
}
