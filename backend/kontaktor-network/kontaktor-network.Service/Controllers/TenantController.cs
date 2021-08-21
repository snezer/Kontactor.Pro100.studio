using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KONTAKTOR.DA.Mongo.Repository;
using kontaktor_network.DA.Models;
using Microsoft.AspNetCore.Mvc;

namespace KONTAKTOR.Service.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class TenantController : ControllerBase
    {
        private TenancyRepository _tenants;

        public TenantController(TenancyRepository tenants)
        {
            _tenants = tenants;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _tenants.GetAllAsync();
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> Get(string id)
        {
            var result = await _tenants.GetAsync(id);
            return Ok(result);
        }


        [HttpPut]
        public async Task<IActionResult> Update(Tenant model)
        {
            var result = await _tenants.UpdateAsync(model);
            return Ok(result);
        }

        
    }
}
