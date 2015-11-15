using System.Collections.Generic;
using System.Linq;

namespace Assets.MVC.Model
{
    public class Board
    {
        private readonly List<int[]> _mTetrisLines;

        private readonly List<int> _mLinesCount;

        private Point _mCurrentShapeCoord;

        public int Width { get; private set; }
        public int Height { get; private set; }

        public Board(int width, int height)
        {
            Width = width;
            Height = height;

            _mLinesCount = new List<int>();
            _mTetrisLines = new List<int[]>();

            while (_mTetrisLines.Count < Height)
            {
                _mTetrisLines.Add(new int[Width]);
                _mLinesCount.Add(0);
            }
        }

        public Point CurrentShapeCoord
        {
            get { return _mCurrentShapeCoord; }
            set { _mCurrentShapeCoord = new Point(value); }
        }

        private int this[Point point]
        {
            get { return _mTetrisLines[point.Y][point.X]; }
            set { _mTetrisLines[point.Y][point.X] = value; }
        }

        public List<int> AttachShape(TetrisShape shape)
        {
            var collectedLine = shape.Blocks
                .Select(block => _mCurrentShapeCoord + block)
                .Where(coord => coord.Y < Height)
                .Where(coord =>
                {
                    this[coord] = 1;
                    return (++_mLinesCount[coord.Y] == Width);
                })
                .Select(coord => coord.Y)
                .ToList();

            collectedLine.Sort();
            return collectedLine;
        }

        public bool CheckShapeOffset(TetrisShape shape, Point offset)
        {
            return !shape.Blocks
                .Select(block => _mCurrentShapeCoord + block + offset)
                .Any(coord => coord.X < 0 || coord.X > Width - 1 ||
                              coord.Y < 0 || coord.Y < Height &&
                              this[coord] != 0);
        }

        public void DestroyLine(int lineIndex)
        {
            _mTetrisLines.RemoveAt(lineIndex);
            _mTetrisLines.Add(new int[Width]);

            _mLinesCount.RemoveAt(lineIndex);
            _mLinesCount.Add(0);
        }
    }
}
