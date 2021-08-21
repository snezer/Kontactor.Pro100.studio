using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CENTROS.SMSNotifications.Service.Models;
using KONTAKTOR.DA.Models;
using KONTAKTOR.DA.Mongo.Repository;
using KONTAKTOR.DA.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using log4net;

namespace netcoreservice.Service.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class CompartmentController : ControllerBase
    {
        private CompartmentRepository _repo;
        private IMapper _mapper;

        public CompartmentController(CompartmentRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Create(Compartment model)
        {
            var result = await _repo.CreateAsync(model);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update(Compartment model)
        {
            var result = await _repo.UpdateAsync(model);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var all = await _repo.GetAllAsync();
            return Ok(all);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var all = await _repo.GetAsync(id);
            return Ok(all);
        }

        [HttpGet("mapid/{id}")]
        public async Task<IActionResult> GetForMap(string id)
        {
            var all = await _repo.GetByMapIdAsync(id);
            return Ok(all);
        }

    }
}