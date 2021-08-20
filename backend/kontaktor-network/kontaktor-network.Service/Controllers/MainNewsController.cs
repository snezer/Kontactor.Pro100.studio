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
    public class MainNewsController : ControllerBase
    {
        private MainNewsRepository _news;


        // private readonly log4net.ILog _logger;
        public MainNewsController(MainNewsRepository news,  IMapper mapper)
        {
            _news = news;
        }

        
        [HttpGet]
        
        public async Task<IActionResult> Get()
        {
            var result = await _news.GetAllAsync();
            return Ok(result);
        }

    }
}
