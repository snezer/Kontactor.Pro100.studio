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
using KONTAKTOR.Service.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using log4net;

namespace netcoreservice.Service.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class SystemController : ControllerBase
    {
        private RolesSeeder _roles;
        private UserSeeder _users;
        private CompaniesSeeder _comps;

        // private readonly log4net.ILog _logger;
        public SystemController(RolesSeeder roles, UserSeeder users,  CompaniesSeeder comps)
        {
            _roles = roles;
            _users = users;
            _comps = comps;
        }

        /// <summary>
        /// Сидирует роли и дефолтного пользователя
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("seed")]
        public async Task<IActionResult> Seed()
        {
            await _roles.Seed();
            await _users.Seed();
            await _comps.Seed();
            return Ok();
        }

    }
}
