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
using KONTRAKTOR.DA.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using log4net;

namespace netcoreservice.Service.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class RentsController : ControllerBase
    {
        private RentFactRepository _rents;

        // private readonly log4net.ILog _logger;
        public RentsController(RentFactRepository rents, IMapper mapper)
        {
            _rents = rents;
        }

        [HttpPost]
        [Route("book")]
        public async Task<IActionResult> BookCompartment(BookModel model)
        {
            var booked = await _rents.GetRentsForCompartment(model.CompartmentId);
            var conflicting = booked.Where(b => 
                model.From<=b.ContractEnd && model.To>=b.ContractStart 
                ||
                model.To>=b.ContractStart && model.From<=b.ContractEnd
            );
            if (conflicting.Any())
            {
                return Conflict(conflicting);
            }

            var rent = await _rents.CreateAsync(new RentFact()
            {
                CompartmentId = model.CompartmentId,
                TenantId = model.TenantId,
                ContractEnd = model.To,
                ContractStart = model.From
            });
            return Ok(rent);
        }

        [HttpPut]
        public async Task<IActionResult> Update(RentFact model)
        {
            var result = await _rents.UpdateAsync(model);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var result = await _rents.GetAsync(id);

            return result != null
                ? (IActionResult)Ok(result)
                : NotFound();
        }

        [HttpGet("{tenantid}")]
        public async Task<IActionResult> GetForTenant(string tenantid)
        {
            var result = await _rents.GetRentsForTenantId(tenantid);

            return result != null
                ? (IActionResult)Ok(result)
                : NotFound();
        }

        [HttpGet("{compartmentid}")]
        public async Task<IActionResult> GetForCompartment(string compartmentid)
        {
            var result = await _rents.GetRentsForCompartment(compartmentid);

            return result != null
                ? (IActionResult)Ok(result)
                : NotFound();
        }

        [HttpGet("{compartmentid}/{from}/{to}")]
        public async Task<IActionResult> GetForCompartment(string compartmentid, DateTime from, DateTime to)
        {
            var result = await _rents.GetRentsForCompartment(compartmentid, from, to);

            return result != null
                ? (IActionResult)Ok(result)
                : NotFound();
        }

    }
}
