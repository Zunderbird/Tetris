using System;
using System.Collections.Generic;

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
        public IEnumerable<int> LineIndexes;

        public LineIndexEventArgs(IEnumerable<int> lineIndex)
        {
            LineIndexes = new List<int>(lineIndex);
        }
    }

    public class GameOverEventArgs : EventArgs
    {
        public int CollectedLinesCount { get; set; }
        public int Score { get; set; }
        public int Level { get; set; }

        public GameOverEventArgs(int collectedLinesCount, int score, int level)
        {
            CollectedLinesCount = collectedLinesCount;
            Score = score;
            Level = level;
        }
    }
}


