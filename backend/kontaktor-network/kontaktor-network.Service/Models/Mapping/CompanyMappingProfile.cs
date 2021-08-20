using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using KONTAKTOR.DA.Models;

namespace CENTROS.SMSNotifications.Service.Models.Mapping
{
    public class CompanyMappingProfile : Profile
    {
        public CompanyMappingProfile()
        {
            CreateMap<Company, CompanyDto>();
            CreateMap<CompanyDto, Company>()
                .ForSourceMember(c => c.Occupations, o => o.DoNotValidate());

            CreateMap<Occupation, OccupationDto>();
            CreateMap<OccupationDto, Occupation>();
        }
    }
}
