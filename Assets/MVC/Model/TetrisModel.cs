using System;
using System.Collections.Generic;

public class TetrisModel 
{
    private Board m_board;

    private TetrisShape m_currentShape;
    private TetrisShape m_nextShape;

    List<int> m_collectedLine;

    public int Level { get; set; }
    public int Score { get; private set; }
    public int CollectedCount { get; private set; }

    public delegate void MovementEvent(MovementEventArgs e);
    public event MovementEvent MovementDone;
    public event EventHandler RotateDone;
    public event EventHandler ShapeIsAdded;
    public event EventHandler ShapeIsAttached;
    public event EventHandler LineIsCollected;
    public event EventHandler GameOver;

    public TetrisModel(int i_width, int i_height)
    {
        Score = 0;
        CollectedCount = 0;
        Level = 1;

        m_board = new Board(i_width, i_height);

        m_nextShape = GenerateNextShape();
        AddShape();

        m_collectedLine = new List<int>();
    }

    public TetrisShape NextShape
    {
        get
        {
            return new TetrisShape(m_nextShape);
        }
    }

    public TetrisShape CurrentShape
    {
        get
        {
            return new TetrisShape(m_currentShape);
        }
    }

    public Point CurrentShapeCoord
    {
        get
        {
            return new Point(m_board.CurrentShapeCoord);
        }
    }

    public List<int> CollectedLine
    {
        get
        {
            return new List<int>(m_collectedLine);
        }
    }

    private static TetrisShape GenerateNextShape()
    {
        var newShape = JsonParser.GetRandomShape();
        newShape.HexColor = JsonParser.GetRandomColor();
        return newShape;
    }

    private bool AddShape()
    {
        m_currentShape = m_nextShape;
        m_nextShape = GenerateNextShape();
        m_board.CurrentShapeCoord = new Point((m_board.Width - m_currentShape.Width) / 2, m_board.Height - m_currentShape.Height - 2);
        return m_board.CheckShapeOffset(m_currentShape, new Point(0, 0));
    }

    public void MoveShape(MoveDirection i_moveDirection)
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
            default: return;
        }

        if (IsMovementDone(_offset, i_moveDirection))
        {
            var eventArg = new MovementEventArgs();
            eventArg.MoveDirect = i_moveDirection;
            MovementDone(eventArg);
        }
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
            FinishMovement();
        }
        return false;
    }

    private void FinishMovement()
    {
        m_collectedLine = m_board.AttachShape(m_currentShape);
        ShapeIsAttached(this, EventArgs.Empty);

        DestroyCollectedLines();

        if (AddShape() == true)
        {
            ShapeIsAdded(this, EventArgs.Empty);
        }
        else GameOver(this, EventArgs.Empty);
    }

    private void DestroyCollectedLines()
    {
        while (m_collectedLine.Count > 0)
        {
            LineIsCollected(this, EventArgs.Empty);
            GaineScorePoints();
            m_board.DestroyLine(m_collectedLine[m_collectedLine.Count - 1]);
            m_collectedLine.RemoveAt(m_collectedLine.Count - 1);
        }
    }

    private void GaineScorePoints()
    {
        int collectedToNextLevel = 10;

        Score += Level;
        CollectedCount += 1;

        if (CollectedCount % collectedToNextLevel == 0) LevelUp();
    }

    private void LevelUp()
    {
        Level += 1;
    }

    public void RotateShape(RotateDirection i_rotateDirection)
    {
        TetrisShape _shape =  m_currentShape.Rotate(i_rotateDirection);

        if (m_board.CheckShapeOffset(_shape, new Point(0, 0)) == true)
        {
            m_currentShape = _shape;
            RotateDone(this, EventArgs.Empty);
        }
    }

}

