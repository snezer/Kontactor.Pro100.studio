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
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using log4net;

namespace netcoreservice.Service.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class BinaryContentController : ControllerBase
    {
        private BinaryContentRepository _repo;

        // private readonly log4net.ILog _logger;
        public BinaryContentController(BinaryContentRepository repo,  IMapper mapper)
        {
            _repo = repo;
        }

        [HttpPost]
        public async Task<IActionResult> Create(BinaryContent model)
        {
            var company = await _repo.CreateAsync(model);
            return Ok(company);
        }

        [HttpPut]
        public async Task<IActionResult> Update(BinaryContent model)
        {
            var result = await _repo.UpdateAsync(model);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var result = await _repo.GetAsync(id);

            return result != null
                ? (IActionResult)Ok(result)
                : NotFound();
        }

        
    }
}
