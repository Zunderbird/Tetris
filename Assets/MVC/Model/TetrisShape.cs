using System.Collections.Generic;

public class TetrisShape 
{
    private List<Point> m_shapeBlocks;
    

    public TetrisShape()
    {
        m_shapeBlocks = new List<Point>();
    }

    public TetrisShape(List<Point> i_shapeBlocks)
    {
        m_shapeBlocks = new List<Point>(i_shapeBlocks);
    }

    public List<Point> Blocks
    {
        get
        {
            return new List<Point> (m_shapeBlocks);
        }
    }

    public int Width
    {
        get
        {
            int _width = 0;

            foreach (Point _point in m_shapeBlocks)
            {
                if (_point.X > _width)
                    _width = _point.X;
            }
            return _width;
        }
    }

    public int Height
    {
        get
        {
            int _height = 0;

            foreach (Point _point in m_shapeBlocks)
            {
                if (_point.Y > _height)
                    _height = _point.Y;
            }
            return _height;
        }
    }

    private void AddBlock(Point i_shapeParticle)
    {
        m_shapeBlocks.Add(new Point(i_shapeParticle.X, i_shapeParticle.Y));
    }

    public TetrisShape Rotate(RotateDirection i_rotateDirection)
    {
        TetrisShape _shape = new TetrisShape();
        Point _offset;
        Point _unitVector;

        if (i_rotateDirection == RotateDirection.ClockWise)
        {
            _offset = new Point(0, Width);
            _unitVector = new Point(1, -1);
        }
        else
        {
            _offset = new Point(Height, 0);
            _unitVector = new Point(-1, 1);
        }

        foreach (Point _block in m_shapeBlocks)
        {
            _shape.AddBlock(Point.Swap(_block) * _unitVector + _offset);
        }
        return _shape;
    }
}
