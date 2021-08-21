using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KONTAKTOR.DA.Models;
using KONTAKTOR.DA.Mongo.Repository;
using kontaktor_network.DA.Models;

namespace KONTAKTOR.Service.Services
{
    public class UserSeeder
    {
        private UserInformationRepository _repo;

        public UserSeeder(UserInformationRepository repository)
        {
            _repo = repository;
        }

        public async Task Seed()
        {
            await _repo.RemoveAll();
            await _repo.CreateAsync(
                new UserInformation()
                {
                    Login = "admin",
                    Password = "admin",
                    FirstName = "Админ",
                    LastName = "Админов",
                    SystemRoles = new[] {"System.Admin"}
                }

                );

            await _repo.CreateAsync(
                new UserInformation()
                {
                    Login = "ivanov",
                    Password = "ivanov",
                    FirstName = "Иван",
                    LastName = "Иванов",
                    SystemRoles = new[] { "System.User" },
                    IsIP = true,
                    Email = "ivanov@ivan.ru",
                    IsTenant = true,
                    Disabled = false,
                    AccountingInformation = new AccountingInformation()
                    {
                        Account = "04108100001111000111",
                        BankName = "ПАО СБЕРБАНК",
                        BIK = "156132",
                        CorrAccount = "1564810213265454",
                        INN = "123456789011",
                        KPP = "132132154",
                        LegalAddress = "Россия, г. Ульяновск, ул. Садовая, 27 кв.14",
                        MailAdress = "Россия, г. Ульяновск, ул. Садовая, 27 кв.14"
                    }
                }

            );
        }
    }
}
