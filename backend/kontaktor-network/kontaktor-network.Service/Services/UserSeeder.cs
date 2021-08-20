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
                    SecondName = "Админов",
                    SystemRoles = new[] {"System.Admin"}
                }

                );
        }
    }
}
