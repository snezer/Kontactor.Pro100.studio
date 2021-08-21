using System;

namespace Babel.Db.Base
{
    public class Vector
    {
        public static readonly Vector Zero = new Vector(0, 0);
        
        public double X { get; set; }

        public double Y { get; set; }

        public int IntX
        {
            get => (int) X;
            set => X = value;
        }

        public int IntY
        {
            get => (int)Y;
            set => Y = value;
        }

        public double Length
        {
            get
            {
                return Math.Sqrt(SquaredLength);
            }
        }

        public double SquaredLength
        {
            get
            {
                return X * X + Y * Y;
            }
        }

        public double Angle
        {
            get
            {
                return Math.Atan2(Y, X);
            }
        }

        public Vector()
        {
            X = 1;
            Y = 1;
        }

        public Vector(double xValue, double yValue)
            : this()
        {
            X = xValue;
            Y = yValue;
        }

        public override bool Equals(object obj)
        {
            if (Object.ReferenceEquals(obj, null))
            {
                return false;
            }

            if (Object.ReferenceEquals(this, obj))
            {
                return true;
            }

            if (this.GetType() != obj.GetType())
                return false;

            Vector other = ((Vector)obj);
            return (X == other.X) && (Y == other.Y);
        }

        public override int GetHashCode()
        {
            return X.GetHashCode() ^ Y.GetHashCode();
        }

        public override string ToString()
        {
            return "(" + X + ", " + Y + ")";
        }

        public static bool operator ==(Vector v1, Vector v2)
        {
            if (object.ReferenceEquals(v1, null))
            {
                return object.ReferenceEquals(v2, null);
            }
            return v1.Equals(v2);
        }

        public static bool operator !=(Vector v1, Vector v2)
        {
            return !(v1 == v2);
        }

        public static Vector operator +(Vector a, Vector b)
        {
            return new Vector(a.X + b.X, a.Y + b.Y);
        }

        public static Vector operator -(Vector a, Vector b)
        {
            return new Vector(a.X - b.X, a.Y - b.Y);
        }

        public static Vector operator *(Vector a, Vector b)
        {
            return new Vector(a.X * b.X, a.Y * b.Y);
        }

        public static Vector operator *(Vector a, double b)
        {
            return new Vector(a.X * b, a.Y * b);
        }

        public static Vector operator *(double a, Vector b)
        {
            return new Vector(b.X * a, b.Y * a);
        }

        public static Vector operator /(Vector a, Vector b)
        {
            return new Vector(a.X / b.X, a.Y / b.Y);
        }

        public static Vector operator /(Vector a, double b)
        {
            return new Vector(a.X / b, a.Y / b);
        }

        public static Vector operator -(Vector a)
        {
            return new Vector(-a.X, -a.Y);
        }
    }
}
