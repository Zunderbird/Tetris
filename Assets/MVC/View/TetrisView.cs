﻿using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using ExtensionMethods;

public partial class TetrisView : ITetrisView
{
    private GameObject m_currentShape;
    private GameObject m_nextShape;
    private GameObject m_shapeHeap;

    public TetrisView()
    {
        m_shapeHeap = new GameObject("ShapeHeap");
    }

    public void DisplayBoard()
    {
        GameObject _board = GameObject.Instantiate ((GameObject)Resources.Load("Board"));
    }

    public void SpawnShape(TetrisShape i_shape, Point i_spawnCoord)
    {
        if (m_currentShape != null) AttachShape(); 
        m_currentShape = CreateShape(i_shape);
        m_currentShape.transform.position = new Vector2(i_spawnCoord.X * 0.5f - 2.25f, i_spawnCoord.Y * 0.5f - 5.25f);
      
        Color _color = new Color(UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f));
        PaintShape(_color);
        
    }

    private void PaintShape(Color32 i_color)
    {
        foreach (Transform _block in m_currentShape.transform)
        {
            _block.GetComponent<SpriteRenderer>().color = i_color;
        }
    }

    public void MoveShape(MoveDirection i_moveDirection)
    {
        switch (i_moveDirection)
        {
            case MoveDirection.Right:
                {
                    m_currentShape.transform.position += m_currentShape.transform.right * (0.5f);
                }
                break;
            case MoveDirection.Left:
                {
                    m_currentShape.transform.position += m_currentShape.transform.right * (-0.5f);
                }
                break;
            case MoveDirection.Up:
                {
                    m_currentShape.transform.position += m_currentShape.transform.up * (0.5f);
                }
                break;
            case MoveDirection.Down:
                {
                    m_currentShape.transform.position += m_currentShape.transform.up * (-0.5f);
                }
                break;
        }
    }

    public void RotateShape(TetrisShape i_shape)
    {
        Vector2 _currentShapePosition = m_currentShape.transform.position;
        Color _color = m_currentShape.GetComponentInChildren<SpriteRenderer>().color;

        UnityEngine.Object.Destroy(m_currentShape);

        m_currentShape = CreateShape(i_shape);
        m_currentShape.transform.position = _currentShapePosition;
        PaintShape(_color);
    }

    public void DestroyLine(int i_number)
    {
        foreach (Transform _block in m_shapeHeap.transform)
        {
            if (_block.transform.position.y == 0.5f * i_number - 5.25f)
            {
                UnityEngine.Object.Destroy(_block.gameObject);
            }
            else if (_block.transform.position.y > 0.5f * i_number - 5.25f)
            {
                _block.transform.position += _block.transform.up * (-0.5f);
            }
        }
    }

    public void DisplayNextShape(TetrisShape i_shape)
    {
        UnityEngine.Object.Destroy(m_nextShape);
        m_nextShape = CreateShape(i_shape);
        m_nextShape.transform.position = new Vector2(3.75f, 3.75f);
    }

    public void UpdateScore()
    {
        
    }

    public void UpdateLevel()
    {
    }

    public void DisplayGameOver()
    {
    }

    public static GameObject CreateShape(TetrisShape i_shape)
    {
        GameObject _shape = new GameObject("TetrisShape");

        foreach (Point _block in i_shape.Blocks)
        {
            float _x = _block.X;
            float _y = _block.Y;

            GameObject _localVar = GameObject.Instantiate((GameObject)Resources.Load("block"));
            _localVar.transform.parent = _shape.transform;
            _localVar.transform.position = new Vector2(_x * 0.5f, _y * 0.5f);
        }
        return _shape;
    }

    private void AttachShape()
    {
        while (m_currentShape.transform.childCount > 0)
        {
            m_currentShape.transform.GetChild(0).transform.parent = m_shapeHeap.transform;
        }
        UnityEngine.Object.Destroy(m_currentShape);
    }
}
