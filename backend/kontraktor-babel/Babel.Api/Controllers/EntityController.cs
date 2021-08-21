using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper;
using Babel.Api.Base;
using Babel.Api.Dto.Entity;
using Babel.Api.Dto.Room;
using Babel.Db.Models.Entities;
using Babel.Db.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Babel.Api.Controllers
{
    [ApiController]
    [Route("entity")]
    public class EntityController: ControllerBase
    {
        private readonly RoomService _roomService;
        private readonly EntityService _entityService;
        private readonly IMapper _mapper;

        public EntityController(RoomService roomService,
            EntityService entityService,
            IMapper mapper)
        {
            _roomService = roomService;
            _entityService = entityService;
            _mapper = mapper;
        }

        /// <summary>
        /// Получить список проходимых сущностей для этажа
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("{level}")]
        public async Task<IActionResult> GetEntities(string level)
        {
            var entities = await _entityService.GetEntitiesByLevel(level);
            var converted = _mapper.Map<List<EntityDto>>(entities);

            return JsonResponse.New(converted);
        }

        /// <summary>
        /// Добавление сущности на этаж
        /// </summary>
        /// <param name="level"></param>
        /// <param name="entityDto"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("{level}")]
        public async Task<IActionResult> AddEntity(string level, EntityDto entityDto)
        {
            var entity = _mapper.Map<Entity>(entityDto);
            entity.Id = Guid.NewGuid().ToString();
            entity.LevelId = level;
            var result = await _entityService.Create(entity);

            return JsonResponse.New(_mapper.Map<EntityDto> (result));
        }

        /// <summary>
        /// Удалить проходимую сущность
        /// </summary>
        /// <param name="entityId"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{entityId}")]
        public async Task<IActionResult> DeleteEntity(string entityId)
        {
            var entity = await _entityService.Get(entityId);
            if (entity == null)
                return NotFound("Сущность не найдена");
            await _entityService.Remove(entity);
            return JsonResponse.New("ok");
        }

        /// <summary>
        /// Связать проходимую сущность с другой сущностью
        /// </summary>
        [HttpPost]
        [Route("bind/{sourceId}")]
        public async Task<IActionResult> BindEntity(string sourceId, string targetId)
        {
            var sourceEntity = await _entityService.Get(sourceId);
            var targetEntity = await _entityService.Get(targetId);

            if (sourceEntity == null || targetEntity == null)
                return NotFound("Сущность не найдена");

            if (sourceEntity.Type != targetEntity.Type)
                return BadRequest("Типы связываемых сущностей не совпадают");

            if (sourceEntity.Type != "stairs" && sourceEntity.Type != "elevator" &&
                targetEntity.Type != "stairs" && targetEntity.Type != "elevator")
                return BadRequest("Попытка связать несвязываемые сущности");

            if (!sourceEntity.BoundEntitiesIds.Contains(targetId))
                sourceEntity.BoundEntitiesIds.Add(targetId);

            if (!targetEntity.BoundEntitiesIds.Contains(sourceId))
                targetEntity.BoundEntitiesIds.Add(sourceId);

            await _entityService.Update(sourceId, sourceEntity);
            await _entityService.Update(targetId, targetEntity);

            return JsonResponse.New("ok");
        }

        /// <summary>
        /// Указать фотографию для комнаты
        /// </summary>
        [HttpPut, HttpPost]
        [Route("photo/{id}")]
        [Consumes("application/octet-stream", "multipart/form-data")]
        public async Task<IActionResult> SetPhoto(string id, [FromForm] List<IFormFile> files)
        {
            var entity = await _entityService.Get(id);
            var image = files.FirstOrDefault();
            if (image != null && image.Length > 0)
            {
                using (var ms = new MemoryStream())
                {
                    image.CopyTo(ms);
                    var fileBytes = ms.ToArray();
                    string s = Convert.ToBase64String(fileBytes);
                    entity.Photo = s;
                }
            }
            await _entityService.Update(id, entity);
            return JsonResponse.New(_mapper.Map<EntityDto>(entity));
        }
    }
}
