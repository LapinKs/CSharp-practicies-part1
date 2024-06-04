namespace Geometry
{
    public class Vector
    {
        public double X;
		public double Y;
		public double GetLength() => Geometry.GetLength(this);
        public Vector Add(Vector v) => Geometry.Add(this, v);
        public bool Belongs(Segment s) => Geometry.IsVectorInSegment(this,s);

    }
    public class Geometry
    {
        public static double GetLength(Segment s) {
            var result = new Vector();
            result.X = s.End.X - s.Begin.X;
            result.Y = s.End.Y-s.Begin.Y;
            return GetLength(result);
        }
        public static double GetLength(Vector vector) {
            return Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y);
        }
        public static Vector Add(Vector first, Vector second) {
            var result = new Vector();
            result.X = first.X + second.X;
            result.Y = first.Y + second.Y;
            return result;
        }
        public static bool IsVectorInSegment(Vector v,Segment s)
        {
			var semi = new Vector() { X = s.End.X-s.Begin.X, Y = s.End.Y - s.Begin.Y };
            return InSegment(v, s)&& (Math.Abs((s.Begin.X-v.X) * semi.Y
                - ( s.Begin.Y - v.Y) * semi.X) < 1e-10);
        }
        public static bool InSegment(Vector v,Segment s)
        {
            return (Math.Min(s.Begin.X, s.End.X) <= v.X && Math.Max(s.Begin.X, s.End.X) >= v.X) &&
                Math.Min(s.Begin.Y, s.End.Y) <= v.Y && Math.Max(s.Begin.Y, s.End.Y) >= v.Y;
        }
    }
    public class Segment
    {
        public Vector Begin;
        public Vector End;
		public bool Contains(Vector v)=> Geometry.IsVectorInSegment(v,this);
		public double GetLength()=> Geometry.GetLength(this);
    }
}