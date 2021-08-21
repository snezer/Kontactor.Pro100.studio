namespace Babel.Db.Base
{
    public class Size: Vector
    {
        public double Width
        {
            get => X;
            set => X = value;
        }

        public double Height
        {
            get => Y;
            set => Y = value;
        }
    }
}
