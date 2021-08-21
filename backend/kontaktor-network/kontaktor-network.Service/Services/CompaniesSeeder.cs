using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KONTAKTOR.DA.Models;
using KONTAKTOR.DA.Mongo.Repository;
using kontaktor_network.DA.Models;

namespace KONTAKTOR.Service.Services
{
    public class CompaniesSeeder
    {
        private CompanyRepository _comps;
        private EmployeeRepository _employees;
        private UserInformationRepository _users;

        public CompaniesSeeder(CompanyRepository comps, EmployeeRepository employees, UserInformationRepository users)
        {
            _comps = comps;
            _employees= employees;
            _users = users;
        }

        public async Task Seed()
        {
            await _comps.RemoveAll();
            var uk = await _comps.CreateAsync(new Company()
            {
                Name = "ООО УК КОНТАКТОР",
                Description= "Управляющая компания креативного кластера",
                IsMain = true,
                FullName = "ОБЩЕСТВО С ОГРАНИЧЕННОЙ ОТВЕСТВЕННОСТЬЮ УПРАВЛЯЮЩАЯ КОМПАНИЯ КОНТАКТОР",
                AccountingInformation = new AccountingInformation()
                {
                    Account = "30101810400000000111 ",
                    BankName = "ПАО ВТБ",
                    BIK = "044525225",
                    CorrAccount = "30101810400000000111",
                    INN = "7722734777",
                    OGRN = "5107744668877",
                    KPP = "",
                    LegalAddress = "Россия, Ульяновск, ул. Торговый Ряд, 22",
                    MailAdress = "Россия, Ульяновск, ул. Торговый Ряд, 22"
                }
            });

            var admin = await _users.FindByLogin("admin");
            await _employees.CreateAsync(new Employee()
            {
                IsActive = true,
                Roles = new[] {"Company.Director", "Company.User"},
                UserId = admin.Id,
                CompanyId = uk.Id
            });
        }
    }
}
