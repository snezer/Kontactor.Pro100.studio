using AutoMapper;
using Babel.Api.Base;
using Babel.Db.Base;

namespace Babel.Api.Dto
{
    [AutoMap(typeof(Vector), ReverseMap = true)]
    [AutoMap(typeof(Vector))]
    public class PositionDto
    {
        public double X { get; set; }
        public double Y { get; set; }
    }
}
