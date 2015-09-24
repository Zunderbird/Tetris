
public struct Point 
{
    public static readonly Point Empty = new Point();

    private int m_x;
    private int m_y;

    public Point(int i_x, int i_y)
    {
        this.m_x = i_x;
        this.m_y = i_y;
    }

    public Point(Point i_previousPoint)
    {
        m_x = i_previousPoint.X;
        m_y = i_previousPoint.Y;
    }

    public int X
    {
        get
        {
            return m_x;
        }
        set
        {
            m_x = value;
        }
    }

    public int Y
    {
        get
        {
            return m_y;
        }
        set
        {
            m_y = value;
        }
    }

    public bool IsEmpty
    {
        get
        {
            return m_x == 0 && m_y == 0;
        }
    }

    public void Offset(int i_dx, int i_dy)
    {
        X += i_dx;
        Y += i_dy;
    }

    public static Point operator + (Point i_left, Point i_right)
    {
        return new Point(i_left.X + i_right.X, i_left.Y + i_right.Y);
    }

    public static Point operator - (Point i_left, Point i_right)
    {
        return new Point(i_left.X - i_right.X, i_left.Y - i_right.Y);
    }

    public static Point operator * (Point i_left, Point i_right)
    {
        return new Point(i_left.X * i_right.X, i_left.Y * i_right.Y);
    }

    public static Point Swap (Point i_point)
    {
        return new Point(i_point.Y, i_point.X);
    }

    public static bool operator == (Point i_left, Point i_right)
    {
        return i_left.X == i_right.X && i_left.Y == i_right.Y;
    }

    public static bool operator != (Point i_left, Point i_right)
    {
        return !(i_left == i_right);
    }

    public override bool Equals(object i_obj)
    {
        if (!(i_obj is Point)) return false;
        Point _comp = (Point)i_obj;

        return _comp.X == this.X && _comp.Y == this.Y;
    }

    public override int GetHashCode()
    {
        return m_x ^ m_y;
    }
}
