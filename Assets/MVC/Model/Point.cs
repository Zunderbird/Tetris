namespace Assets.MVC.Model
{
    public struct Point
    {
        public static readonly Point Empty = new Point();

        public int X { get; set; }
        public int Y { get; set; }

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public Point(Point previousPoint)
        {
            X = previousPoint.X;
            Y = previousPoint.Y;
        }

        public bool IsEmpty
        {
            get { return X == 0 && Y == 0; }
        }

        public void Offset(int dx, int dy)
        {
            X += dx;
            Y += dy;
        }

        public static Point operator +(Point left, Point right)
        {
            return new Point(left.X + right.X, left.Y + right.Y);
        }

        public static Point operator -(Point left, Point right)
        {
            return new Point(left.X - right.X, left.Y - right.Y);
        }

        public static Point operator *(Point left, Point right)
        {
            return new Point(left.X * right.X, left.Y * right.Y);
        }

        public static Point Swap(Point point)
        {
            return new Point(point.Y, point.X);
        }

        public static bool operator ==(Point left, Point right)
        {
            return left.X == right.X && left.Y == right.Y;
        }

        public static bool operator !=(Point left, Point right)
        {
            return !(left == right);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Point)) return false;
            var comp = (Point)obj;

            return comp.X == X && comp.Y == Y;
        }

        public override int GetHashCode()
        {
            return X ^ Y;
        }
    }
}
