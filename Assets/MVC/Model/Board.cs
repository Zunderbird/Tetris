using System;
using System.Collections.Generic;
using System.Linq;

namespace Assets.MVC.Model
{
    public class Board
    {
        private readonly List<int[]> _tetrisLines;

        private readonly List<int> _linesCount;

        private Point _currentShapeCoord;

        public delegate void AttachBlockEvent(object sender, CoordEventArgs coord);

        public event AttachBlockEvent BlockIsAttached;
        public event EventHandler ShapeIsAttached;

        public int Width { get; private set; }
        public int Height { get; private set; }

        public Board(int width, int height)
        {
            Width = width;
            Height = height;

            _linesCount = new List<int>();
            _tetrisLines = new List<int[]>();

            while (_tetrisLines.Count < Height)
            {
                _tetrisLines.Add(new int[Width]);
                _linesCount.Add(0);
            }
        }

        public Point CurrentShapeCoord
        {
            get { return _currentShapeCoord; }
            set { _currentShapeCoord = new Point(value); }
        }

        private int this[Point point]
        {
            get { return _tetrisLines[point.Y][point.X]; }
            set { _tetrisLines[point.Y][point.X] = value; }
        }

        public List<int> AttachShape(TetrisShape shape)
        {
            var collectedLine = shape.Blocks
                .Select(block => _currentShapeCoord + block)
                .Where(coord => coord.Y < Height)
                .Where(coord =>
                {
                    this[coord] = 1;
                    if (BlockIsAttached != null) BlockIsAttached(this, new CoordEventArgs(coord.X, coord.Y));
                    return (++_linesCount[coord.Y] == Width);
                })
                .Select(coord => coord.Y)
                .ToList();

            collectedLine.Sort();

            if (ShapeIsAttached != null) ShapeIsAttached(this, EventArgs.Empty);
            return collectedLine;
        }

        public bool CheckShapeOffset(TetrisShape shape, Point offset)
        {
            return !shape.Blocks
                .Select(block => _currentShapeCoord + block + offset)
                .Any(coord => coord.X < 0 || coord.X > Width - 1 ||
                              coord.Y < 0 || coord.Y < Height &&
                              this[coord] != 0);
        }

        public void DestroyLines(List<int> lineIndexes)
        {
            foreach (var currentLine in lineIndexes)
            {
                _tetrisLines.RemoveAt(currentLine);
                _tetrisLines.Add(new int[Width]);

                _linesCount.RemoveAt(currentLine);
                _linesCount.Add(0);
            }
        }
    }
}
