using System;
using System.Collections.Generic;

public class TetrisModel 
{
    private int m_score;
    private int m_level;
    private int m_speed;

    private int m_boardWidth;
    private int m_boardHeight;
    private Board m_board;

    private TetrisShape m_currentShape;
    private TetrisShape m_nextShape;

    List<int> m_collectedLine;

    private static SimpleJSON.JSONNode m_shapes;
    private static SimpleJSON.JSONNode m_colours;

    static Random rand = new Random();

    public TetrisModel(SimpleJSON.JSONNode i_shapes, SimpleJSON.JSONNode i_colours)
    {
        m_score = 0;
        m_level = 1;
        m_speed = 1;

        m_boardWidth = 10;
        m_boardHeight = 24;
        m_board = new Board(m_boardWidth, m_boardHeight);

        m_shapes = i_shapes;
        m_colours = i_colours;

        m_nextShape = GenerateNextShape();
        AddShape();

        m_collectedLine = new List<int>();

    }

    public event EventHandler ScorePointsGained;
    public event EventHandler LevelUp;
    public event EventHandler ShapesMoveIsFinished;
    public event EventHandler LineIsCollected;
    public event EventHandler GameOver;

    public TetrisShape NextShape
    {
        get
        {
            return m_nextShape;
        }
    }

    public TetrisShape CurrentShape
    {
        get
        {
            return m_currentShape;
        }
    }

    public Point CurrentShapeCoord
    {
        get
        {
            return m_board.CurrentShapeCoord;
        }
    }

    public int Level
    {
        get
        {
            return m_level;
        }
        set
        {
            m_level = value;
            m_speed = m_level * 1;
        }
    }

    public List<int> CollectedLine
    {
        get
        {
            return m_collectedLine;
        }
    }

    public static TetrisShape GenerateNextShape()
    {
        int _nextShapeIndex = rand.Next(m_shapes["shapes"].Count);
        int _nextShapeParticleCount = m_shapes["shapes"][_nextShapeIndex]["coordinates"].Count;

        List<Point> _nextShapeParticles = new List<Point>();
        Point _nextShapeParticle = new Point();

        for (int i = 0; i < _nextShapeParticleCount; i++)
        {
            _nextShapeParticle.X = m_shapes["shapes"][_nextShapeIndex]["coordinates"][i][0].AsInt;
            _nextShapeParticle.Y = m_shapes["shapes"][_nextShapeIndex]["coordinates"][i][1].AsInt;

            _nextShapeParticles.Add(_nextShapeParticle);
        }
        return new TetrisShape(_nextShapeParticles);
    }

    public bool AddShape()
    {
        m_currentShape = m_nextShape;
        m_nextShape = GenerateNextShape();

        m_board.CurrentShapeCoord = new Point((m_boardWidth - m_currentShape.Width) / 2, m_boardHeight - 1);

        return m_board.CheckShapeOffset(m_currentShape, new Point (0, 0));
    }

    public bool MoveShape(MoveDirection i_moveDirection)
    {
        Point _offset;

        switch (i_moveDirection)
        {
            case MoveDirection.Left:
                {
                    _offset = new Point(-1, 0);
                }
                break;
            case MoveDirection.Right:
                {
                    _offset = new Point(1, 0);
                }
                break;
            case MoveDirection.Down:
                {
                    _offset = new Point(0, -1);
                }
                break;
            default: return false;
        }

        return IsMovementDone(_offset, i_moveDirection);
    }

    private bool IsMovementDone(Point i_offset, MoveDirection i_moveDirection)
    {
        if (m_board.CheckShapeOffset(m_currentShape, i_offset) == true)
        {
            m_board.CurrentShapeCoord += i_offset;
            return true;
        }
        else if (i_moveDirection == MoveDirection.Down)
        {
            MovementFinish();
        }
        return false;
    }

    private void MovementFinish()
    {
        m_collectedLine = m_board.AttachShape(m_currentShape);

        if (AddShape() == true)
        {
            ShapesMoveIsFinished(this, EventArgs.Empty);
            DestroyCollectedLines();
        }
        else GameOver(this, EventArgs.Empty);
    }

    private void DestroyCollectedLines()
    {
        while (m_collectedLine.Count > 0)
        {
            UnityEngine.Debug.Log(m_collectedLine.Count - 1);
            LineIsCollected(this, EventArgs.Empty);
            m_board.DestroyLine(m_collectedLine[m_collectedLine.Count - 1]);
            m_collectedLine.RemoveAt(m_collectedLine.Count - 1);
        }
    }

    public bool RotateShape(RotateDirection i_rotateDirection)
    {
        TetrisShape _shape =  m_currentShape.Rotate(i_rotateDirection);

        if (m_board.CheckShapeOffset(_shape, new Point(0, 0)) == true)
        {
            m_currentShape = _shape;
            return true;
        }
        return false;
    }

}

