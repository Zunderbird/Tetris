using System.Collections.Generic;
using System;
using System.Linq;

namespace Assets.MVC.Model
{
    public class TetrisShape
    {
        private readonly List<Point> _mShapeBlocks;

        public List<Point> Blocks
        {
            get { return new List<Point>(_mShapeBlocks);}
        }

        public int HexColor { get; set; }

        public TetrisShape()
        {
            _mShapeBlocks = new List<Point>();
        }

        public TetrisShape(IEnumerable<Point> shapeBlocks)
        {
            _mShapeBlocks = new List<Point>(shapeBlocks);
        }

        public TetrisShape(TetrisShape shape)
        {
            _mShapeBlocks = new List<Point>(shape.Blocks);
            HexColor = shape.HexColor;
        }

        public int Width
        {
            get { return _mShapeBlocks.Max(n => n.X); }
        }

        public int Height
        {
            get { return _mShapeBlocks.Max(n => n.Y);}
        }

        private void AddBlock(Point shapeParticle)
        {
            _mShapeBlocks.Add(new Point(shapeParticle.X, shapeParticle.Y));
        }

        public TetrisShape Rotate(RotateDirection rotateDirection)
        {
            var shape = new TetrisShape();
            Point offset;
            Point unitVector;
            var facet = Math.Max(Width, Height);

            if (rotateDirection == RotateDirection.ClockWise)
            {
                offset = new Point(0, facet);
                unitVector = new Point(1, -1);
            }
            else
            {
                offset = new Point(facet, 0);
                unitVector = new Point(-1, 1);
            }

            foreach (var block in _mShapeBlocks)
            {
                shape.AddBlock(Point.Swap(block) * unitVector + offset);
            }
            shape.HexColor = HexColor;

            return shape;
        }
    }
}
