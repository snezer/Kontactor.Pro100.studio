using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Babel.Db.Models;

namespace Babel.Api.Dto
{
    [AutoMap(typeof(Level))]
    [AutoMap(typeof(Level), ReverseMap = true)]
    public class LevelDto: BaseDto
    {
        public int Value { get; set; }
        public string Image { get; set; }
        public string Title { get; set; }
    }
}
