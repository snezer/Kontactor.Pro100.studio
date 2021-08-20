using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CENTROS.SMSNotifications.Service.Models
{
    public class CompanyDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public OccupationDto[] Occupations { get; set; }
    }
}
