using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Babel.Api.Base;

namespace Babel.Api.Dto.Entity
{
    [AutoMap(typeof(Db.Models.Entities.Entity), ReverseMap = true)]
    public class EntityDto: BaseDto
    {
        public PositionDto Position { get; set; }
        public string Description { get; set; }
        public string BaseId { get; set; }
        public string Type { get; set; }
        public List<string> BoundEntitiesIds { get; set; }
        public string Photo { get; set; }
    }
}
