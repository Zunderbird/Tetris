using System.Collections.Generic;
using System;

public class Board 
{
    private int m_width;
    private int m_height;

    private List<int> m_linesCount;

    private Point m_currentShapeCoord;

    private List<int[]> m_tetrisLines;

    public Board(int i_width, int i_height)
    {
        m_width = i_width;
        m_height = i_height;

        m_linesCount = new List<int>();
        m_tetrisLines = new List<int[]>();

        while (m_tetrisLines.Count < m_height)
        {
            m_tetrisLines.Add(new int[m_width]);
            m_linesCount.Add(0);
        }

    }

    public Point CurrentShapeCoord
    {
        get
        {
            return m_currentShapeCoord;
        }
        set
        {
            m_currentShapeCoord = new Point(value);
        }
    }

    private int this[Point i_point]
    {
        get
        {
            return m_tetrisLines[i_point.Y][i_point.X];
        }
        set
        {
            m_tetrisLines[i_point.Y][i_point.X] = value;
        }
    }

    public List<int> AttachShape(TetrisShape i_shape)
    {
        List<int> _collectedLine = new List<int>();

        foreach (Point _block in i_shape.Blocks)
        {
            Point _coord = new Point(m_currentShapeCoord + _block);

            if (_coord.Y < m_height)
            {
                this[_coord] = 1;
                m_linesCount[_coord.Y] += 1;

                if (m_linesCount[_coord.Y] == m_width)
                {
                    _collectedLine.Add(_coord.Y);
                }
            }
        }
        return _collectedLine;
    }

    public bool CheckShapeOffset(TetrisShape i_shape, Point i_offset)
    {
        foreach (Point _block in i_shape.Blocks)
        {
            Point _coord = m_currentShapeCoord + _block + i_offset;

            if (_coord.Y >= m_height) continue;
            else if (_coord.X < 0 || _coord.X > m_width - 1 || _coord.Y < 0 || this[_coord] == 1)
                return false;
        }
        return true;
    }

    public void DestroyLine(int i_lineIndex)
    {
        m_tetrisLines.RemoveAt(i_lineIndex);
        m_tetrisLines.Add(new int[m_width]);

        m_linesCount.RemoveAt(i_lineIndex);
        m_linesCount.Add(0);
    }
}
