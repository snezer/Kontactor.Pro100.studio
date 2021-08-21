using AutoMapper;
using Babel.Api.Base;
using Babel.Db.Base;

namespace Babel.Api.Dto
{
    [AutoMap(typeof(Size), ReverseMap = true)]
    [AutoMap(typeof(Size))]
    public class SizeDto
    {
        public double Width { get; set; }
        public double Height { get; set; }
    }
}
