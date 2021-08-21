using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KONTAKTOR.DA.Models;
using kontaktor_network.DA.Models;
using KONTRAKTOR.DA.Models;
using KONTRAKTOR.Services.Contract.Letter;

namespace KONTAKTOR.Service.Services.ContractTempating
{
    public class ContractDataBuilder
    {
        private Company _company;
        private RentFact _rent;
        private Tenant _tenant;
        private UserInformation _user;
        private Compartment _compartment;

        enum ClientTypeEnum
        {
            FL,
            UL,
            IP
        };

        ClientTypeEnum ClientType => _user != null ? (_user.IsIP ? ClientTypeEnum.IP : ClientTypeEnum.FL) : ClientTypeEnum.UL;

        public bool LongTermRent => _compartment.IsForLongTermRent && (!_rent.ContractEnd.HasValue ||
                                                                       _rent.ContractEnd.Value - _rent.ContractStart >
                                                                       TimeSpan.FromDays(1));

        public ContractDataBuilder(RentFact rent, Tenant tenant, UserInformation user, Company company, Compartment compartment)
        {
            _rent = rent;
            _tenant = tenant;
            _user = user;
            _company = company;
            _compartment = compartment;
        }


        public ContractLetterModel Build()
        {
            ContractLetterModel result = new ContractLetterModel();
            result.CommonTemplateData = new CommonContractTemplateData()
            {
                Area = $"{_compartment.Area:F}",
                CommonAddress = "Контактор (основной адрес) ",
                MaxFloors = "5",
                RoomFloor = $"{_compartment.Floor}",
                OwnerStart = CreateOwnerStart(),
                TenantStart = CreateTenantStart(ClientType, _company, _user),
                RentFrom = _rent.ContractStart.ToString("")
            };
            return result;
        }

        private string CreateTenantStart(ClientTypeEnum clientType, Company company, UserInformation user)
        {
            switch (clientType)
            {
                case ClientTypeEnum.FL:
                    return
                        $"{user.LastName} {user.FirstName} {user.MiddleName}, действующий/ая как физическое лицо (далее - 'Арендатор'), с другой стороны,";
                case ClientTypeEnum.UL:
                    return
                        $"{company.Name}, именуемое в дальнейшем 'Арендатор', от имени которого действует генеральный директор Иванов Иван Иванович на основании Устава, с другой стороны,";
                case ClientTypeEnum.IP:
                    return $"Индивидуальный предприниматель {user.LastName} {user.FirstName} {user.MiddleName}, зарегистрированный/ая в реестре индивидуальных предпринимателей под № {user.AccountingInformation?.OGRN} (далее - 'Арендатор'), с другой стороны,";
                default:
                    throw new ArgumentOutOfRangeException(nameof(clientType), clientType, null);
            }
        }

        private string CreateOwnerStart()
        {
            return $"ООО УК КОНТАКТОР , именуемое далее 'Арендодатель', от имени которого действует генеральный директор Иванов Иван Иванович на освновании Устава, с одной стороны, и  ";
        }
    }
}
