using System.Collections.Generic;

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
            get
            {
                return _mCurrentShapeCoord;
            }
            set
            {
                _mCurrentShapeCoord = new Point(value);
            }
        }

        private int this[Point point]
        {
            get
            {
                return _mTetrisLines[point.Y][point.X];
            }
            set
            {
                _mTetrisLines[point.Y][point.X] = value;
            }
        }

        public List<int> AttachShape(TetrisShape shape)
        {
            var collectedLine = new List<int>();

            foreach (var block in shape.Blocks)
            {
                var coord = new Point(_mCurrentShapeCoord + block);

                if (coord.Y < Height)
                {
                    this[coord] = 1;
                    _mLinesCount[coord.Y] += 1;

                    if (_mLinesCount[coord.Y] == Width)
                    {
                        collectedLine.Add(coord.Y);
                    }
                }
            }
            collectedLine.Sort();
            return collectedLine;
        }

        public bool CheckShapeOffset(TetrisShape shape, Point offset)
        {
            foreach (var block in shape.Blocks)
            {
                var coord = _mCurrentShapeCoord + block + offset;

                if (coord.X >= 0 && coord.X <= Width - 1 && coord.Y >= 0)
                    if (coord.Y >= Height || this[coord] == 0) continue;
                return false;
            }
            return true;
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
