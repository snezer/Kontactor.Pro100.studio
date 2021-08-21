using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KONTAKTOR.DA.Models;
using KONTAKTOR.DA.Mongo.Repository;

namespace KONTAKTOR.Service.Services
{
    public class RolesSeeder
    {
        private UserRoleRepository _repo;

        public RolesSeeder(UserRoleRepository repository)
        {
            _repo = repository;
        }

        public async Task Seed()
        {
            await _repo.RemoveAll();
            await _repo.CreateAsync(new UserRole()
            {
                Name = "System.Admin",
                Description = "Системный администратор"
            });
            await _repo.CreateAsync(new UserRole()
            {
                Name = "System.User",
                Description = "Пользователь системы"
            });
            await _repo.CreateAsync(new UserRole()
            {
                Name = "Company.Employee",
                Description = "Работник компании"
            });

            await _repo.CreateAsync(new UserRole()
            {
                Name = "Company.Director",
                Description = "Директор компании"
            });
        }
    }
}
