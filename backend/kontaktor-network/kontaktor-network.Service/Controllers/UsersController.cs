using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CENTROS.SMSNotifications.Service.Models;
using KONTAKTOR.DA.Models;
using KONTAKTOR.DA.Mongo.Repository;
using KONTAKTOR.DA.Repository;
using KONTAKTOR.Notifications.DA.Interfaces;
using KONTAKTOR.Service.Models;
using KONTAKTOR.Service.Services;
using kontaktor_network.DA.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using log4net;
using Newtonsoft.Json;

namespace netcoreservice.Service.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class UsersController : ControllerBase
    {
        private UserInformationRepository _users;
        private TenancyRepository _tenancy;
        private EmployeeRepository _employees;
        private CompanyRepository _companies;

        // private readonly log4net.ILog _logger;
        public UsersController(UserInformationRepository users, TenancyRepository tenancy, EmployeeRepository employees, CompanyRepository companies,  IMapper mapper)
        {
            _users = users;
            _tenancy = tenancy;
            _employees = employees;
            _companies = companies;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users =  await _users.GetAllAsync();
            return Ok(
                users.Select(u=>new
                {
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    MiddleName = u.MiddleName,
                    Login = u.Login,
                    Phone = u.Phone,
                    Email = u.Email,
                    TenancyId = u.TenancyId
                })
                );
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserInformation user)
        {
            var result = await _users.CreateAsync(user);
            if (user.IsTenant && string.IsNullOrWhiteSpace(user.TenancyId))
            {
                var tenant = await _tenancy.CreateAsync(new Tenant() { UserInformationId = user.Id });
                result.TenancyId = tenant.Id;
                await _users.UpdateAsync(result);
            }
            
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update(UserInformation user)
        {
            if (user.IsTenant &&  string.IsNullOrWhiteSpace(user.TenancyId))
            {
                var tenant = await _tenancy.CreateAsync(new Tenant() {UserInformationId = user.Id});
                user.TenancyId = tenant.Id;
            }
            var result = await _users.UpdateAsync(user);
            return Ok(result);
        }

        [HttpPost]
        [Route("check")]
        public async Task<IActionResult> CheckAccess(CredentialsPair model)
        {
            var user = await _users.FindByLogin(model.Login);
            if (user == null)
                return NotFound(model.Login);
            return model.Password == user.Password
                ? (IActionResult)Ok(model.Login)
                : NotFound(model.Login);
        }

        [HttpGet("{login}")]
        public async Task<IActionResult> Get(string login)
        {
            var user = await _users.FindByLogin(login);
            if (user == null)
                return NotFound();
            var result = await EnrichUserData(user);

            return Ok(result);
        }

        [HttpGet("id/{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var user = await _users.GetAsync(id);
            if (user == null)
                return NotFound();
            var result = await EnrichUserData(user);


            return Ok(result);
        }

        private async Task<UserInformationResult> EnrichUserData(UserInformation user)
        {
            UserInformationResult result =
                JsonConvert.DeserializeObject<UserInformationResult>(JsonConvert.SerializeObject(user));
            var emplyee = (await _employees.GetAllAsync()).FirstOrDefault(e => e.UserId == user.Id);
            var tenant = await _tenancy.GetTenantForUserAsync(user.Id);
            if (emplyee != null)
            {
                var company = await _companies.GetAsync(emplyee.CompanyId);
                if (company != null)
                {
                    result.CompanyId = company.Id;
                    result.CompanyName = company.Name;
                    result.IsUK = company.IsMain;
                    result.IsEmployee = true;
                    tenant = await _tenancy.GetTenantForCompanyAsync(company.Id);
                }
            }

            result.TenantId = tenant?.Id;
            return result;
        }
    }
}
