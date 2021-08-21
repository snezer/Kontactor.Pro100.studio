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

namespace netcoreservice.Service.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class UsersController : ControllerBase
    {
        private UserInformationRepository _users;
        private TenancyRepository _tenancy;

        // private readonly log4net.ILog _logger;
        public UsersController(UserInformationRepository users, TenancyRepository tenancy,  IMapper mapper)
        {
            _users = users;
            _tenancy = tenancy;
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
                    Email = u.Email
                })
                );
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserInformation user)
        {
            if (user.IsTenant && string.IsNullOrWhiteSpace(user.TenancyId))
            {
                var tenant = await _tenancy.CreateAsync(new Tenant() { UserInformationId = user.Id });
                user.TenancyId = tenant.Id;
            }
            var result = await _users.CreateAsync(user);
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

    }
}
