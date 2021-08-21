using System.Collections.Generic;
using AutoMapper;
using Babel.Db.Models.Rooms;

namespace Babel.Api.Dto.Room
{
    [AutoMap(typeof(BaseRoom), ReverseMap = true)]
    [AutoMap(typeof(BaseRoom))]
    public class RoomDto: BaseDto
    {
        public string Level { get; set; }
        public PositionDto PositionStart { get; set; }
        public SizeDto Size { get; set; }
        public string Description { get; set; }
        public string Photo { get; set; }
        public string Name { get; set; }
        public List<string> Attributes { get; set; }
        public string Type { get; set; }
    }
}
