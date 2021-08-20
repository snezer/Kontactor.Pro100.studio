using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CENTROS.SMSNotifications.Service.Models;
using KONTAKTOR.DA.Models;
using KONTAKTOR.DA.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using log4net;

namespace netcoreservice.Service.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class OccupationController : ControllerBase
    {
        private IOccupationRepository _repo;
        private IMapper _mapper;

        public OccupationController(IOccupationRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Create(Occupation model)
        {
            var result = await _repo.CreateAsync(model);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update(Occupation model)
        {
            var result = await _repo.UpdateAsync(model);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var all = await _repo.GetOccupationsAsync();
            return Ok(all);
        }

    }
}