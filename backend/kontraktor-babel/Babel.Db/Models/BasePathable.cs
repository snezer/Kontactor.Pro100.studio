using System;
using System.Collections.Generic;
using System.Text;
using Babel.Db.Base;

namespace Babel.Db.Models
{
    public class BasePathable: BaseModel
    {
        public Size Size { get; set; }
        public Vector Position { get; set; }

        public string PointForLine => Math.Floor(Position.X + (Size == null ? 0 : Size.Width / 2)) + ","
            + Math.Floor(Position.Y + (Size == null ? 0 : Size.Height / 2));
    }
}
