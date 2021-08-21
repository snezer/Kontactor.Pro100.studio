using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper.Contrib.Extensions;
using kontaktor_network.DA.Models;

namespace KONTAKTOR.Service.Models
{
    public class UserInformationResult : UserInformation
    {
        public string TenantId { get; set; }

        public string CompanyId { get; set; }

        public string CompanyName { get; set; }

        public bool IsUK { get; set; }
        public bool IsEmployee { get; set; }
    }
}
