using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Babel.Db.Models.Entities;

namespace Babel.Api.Dto.Entity
{
    [AutoMap(typeof(EntityType))]
    [AutoMap(typeof(EntityType), ReverseMap = true)]
    public class PossibleEntityTypeDto: BaseDto
    {
        public string Type { get; set; }
        public string Description { get; set; }
    }
}
