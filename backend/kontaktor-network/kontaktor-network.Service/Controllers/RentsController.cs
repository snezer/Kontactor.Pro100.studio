using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
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
using KONTAKTOR.Service.Services.ContractTempating;
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
        private ContractGenerationService _generationService;
        private TenancyRepository _tenants;

        // private readonly log4net.ILog _logger;
        public RentsController(RentFactRepository rents, ContractGenerationService generationService, TenancyRepository tenants)
        {
            _rents = rents;
            _generationService = generationService;
            _tenants = tenants;
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

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var rents = await _rents.GetAllAsync();
            var tenants = ( await _tenants.GetAllAsync()).ToDictionary(t=>t.Id);

            var result = rents.Select(RentModel.From).ToArray();
            foreach (var model in result)
            {
                model.CompanyId = tenants[model.TenantId].CompanyId;
                model.UserId = tenants[model.TenantId].UserInformationId;
            }

            return Ok(result);
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

        /// <summary>
        /// Подтверждение сотрудником УК бронирования помещения
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("validate")]
        public async Task<IActionResult> ValidateBooking(BookingValidationModel model)
        {
            var booking = await _rents.GetAsync(model.RentInfoId);
            if (booking == null)
                return NotFound();
            booking.IsValidated = true;
            booking.ValidatedByUser = model.UserId;
            var result = await _rents.UpdateAsync(booking);
            return Ok(result);
        }

        [HttpGet("contract/blob/{rentId}")]
        public async Task<IActionResult> GetContractBlob(string rentId)
        {
            var data = await _generationService.CreateContract(rentId);
            var ms = new MemoryStream();
            await ms.WriteAsync(data, 0, data.Length);
            await ms.FlushAsync();
            ms.Seek(0, SeekOrigin.Begin);

            return File(ms, "application/vnd.openxmlformats-officedocument.wordprocessingml.document", "Контракт.docx");
        }

    }
}
