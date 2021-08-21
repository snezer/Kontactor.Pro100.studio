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
    public class MainNewsController : ControllerBase
    {
        private MainNewsRepository _news;
        private BinaryContentRepository _binaryContent;


        // private readonly log4net.ILog _logger;
        public MainNewsController(MainNewsRepository news, BinaryContentRepository binaryContent,  IMapper mapper)
        {
            _news = news;
            _binaryContent = binaryContent;
        }

        
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _news.GetAllAsync();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(MainNewsModel model)
        {
            var blob = await _binaryContent.CreateAsync(model.Image);
            model.ContentId = blob.Id;
            var result = await _news.CreateAsync(model);
            return Ok(result);
        }


        /// <summary>
        /// Если при обновлении досылается .Image, то мы предполагаем на бэке что картинка изменилась
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> Update(MainNewsModel model)
        {
            if (model.Image != null)
            {
                if (!string.IsNullOrWhiteSpace(model.Image.Id))
                {
                    await _binaryContent.Remove(model.Image.Id);
                    model.Image.Id = null;
                }

                var blob = await _binaryContent.CreateAsync(model.Image);
                model.ContentId = blob.Id;
            }

            var result = await _news.UpdateAsync(model);
            return Ok(result);
        }

        [HttpDelete]
        public async Task<IActionResult> Remove(string id)
        {
            await _binaryContent.Remove(id);
            return Ok();
        }

    }
}
