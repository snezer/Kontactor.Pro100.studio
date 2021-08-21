using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KONTRAKTOR.DA.Models;
using Newtonsoft.Json;

namespace KONTAKTOR.Service.Models
{
    public class RentModel : RentFact
    {

        public string UserId { get; set; }

        public string CompanyId { get; set; }

        public static RentModel From(RentFact rent)
        {
            return JsonConvert.DeserializeObject<RentModel>(JsonConvert.SerializeObject(rent));
        }
    }

    
}
