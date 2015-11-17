using System;

namespace Assets.MVC.Model
{
    public class MovementEventArgs : EventArgs
    {
        public Point MoveVector { get; set; }

        public MovementEventArgs(Point moveVector)
        {
            MoveVector = moveVector;
        }
    }

    public class DroppingEventArgs : EventArgs
    {
        public int Distance { get; set; }

        public DroppingEventArgs(int distance)
        {
            Distance = distance;
        }
    }

    public class CoordEventArgs : EventArgs
    {
        public int X { get; set; }
        public int Y { get; set; }

        public CoordEventArgs(int x, int y)
        {
            X = x;
            Y = y;
        }
    }

    public class LineIndexEventArgs : EventArgs
    {
        public int LineIndex;

        public LineIndexEventArgs(int lineIndex)
        {
            LineIndex = lineIndex;
        }
    }
}


