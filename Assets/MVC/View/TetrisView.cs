using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using Assets.MVC.Model;
using DG.Tweening;

namespace Assets.MVC.View
{
    public class TetrisView
    {
        private static GameObject _block;
        private static GameObject _board;

        private const float BLOCK_SIZE = 0.5f;
        private const float X_CORRECTION = 2.25f;
        private const float Y_CORRECTION = 5.25f;
        private readonly static Vector2 Centre = new Vector2(3.75f, 3.75f);

        private GameObject _currentShape;
        private GameObject _nextShape;
        private List<Transform> _animationObjects; 
        
        private readonly List<GameObject> _lines; 
        private readonly TetrisModel _model;
        private readonly Controller.Controller _controller;

        public TetrisView(TetrisModel iModel, Controller.Controller controller)
        {
            _model = iModel;
            _controller = controller;

            _block = (GameObject) Resources.Load("block");
            _board = (GameObject) Resources.Load("Board");

            _lines = new List<GameObject>();

            _model.MovementDone += OnMovementDone;
            _model.RotateDone += OnRotateDone;
            _model.ShapeAdded += OnShapeAdded;
            _model.LineDestroyed += OnLineDestroyed;
            _model.GameOver += OnGameOver;
            _model.ShapeDropping += OnShapeDropping;
            _model.ShapeIsAttachedAddListener(OnShapeIsAttached);
            _model.BlockIsAttachedAddListener(OnAttachedBlock);

            NewGame();
        }

        private void NewGame()
        {
            DisplayBoard();
            DisplayNextShape(_model.NextShape);
            SpawnShape(_model.CurrentShape, _model.CurrentShapeCoord);
        }

        public void DisplayBoard()
        {
            var board = UnityEngine.Object.Instantiate(_board);
            board.transform.position = Vector3.zero;

            for (var i = 0; i < _model.BoardHeight; i++)
            {
                _lines.Add(new GameObject("Line " + i));
                _lines[i].transform.parent = board.transform;
                _lines[i].transform.position += _lines[i].transform.up*(BLOCK_SIZE*i - Y_CORRECTION);
            }
        }

        public void SpawnShape(TetrisShape shape, Point spawnCoord)
        {
            _currentShape = CreateShape(shape);
            _currentShape.transform.position = new Vector2(spawnCoord.X * BLOCK_SIZE - X_CORRECTION, spawnCoord.Y * BLOCK_SIZE - Y_CORRECTION);
        }

        public void PaintShape(Color color)
        {
            foreach (Transform block in _currentShape.transform)
            {
                block.GetComponent<SpriteRenderer>().color = color;
            }
        }

        public static void MoveObject(Point moveVector, GameObject gameObject)
        {
            gameObject.transform.position += new Vector3(moveVector.X, moveVector.Y) * BLOCK_SIZE;
        }

        public void RotateShape(TetrisShape shape)
        {
            Vector2 currentShapePosition = _currentShape.transform.position;

            UnityEngine.Object.Destroy(_currentShape);

            _currentShape = CreateShape(shape);
            _currentShape.transform.position = currentShapePosition;
        }

        private void DestroyLine(int number)
        {
            foreach (Transform block in _lines[number].transform)
            {
                UnityEngine.Object.Destroy(block.gameObject);
            }
        }

        private void DropBlocks(int startBlockIndex)
        {
            _animationObjects = new List<Transform>();
            _controller.DropBlockAnimationStart();

            for (var i = startBlockIndex + 1; i < _model.BoardHeight; i++)
            {
                while (_lines[i].transform.childCount > 0)
                {
                    var block = _lines[i].transform.GetChild(0);
                    block.SetParent(_lines[i - 1].transform, true);

                    _animationObjects.Add(block);

                    block.DOMoveY(block.transform.position.y - BLOCK_SIZE, 0.25f)
                        .OnComplete(() => AnimationCompleted(block));
                }
            }
        }

        private void AnimationCompleted(UnityEngine.Object animatedObject)
        {
            if (_animationObjects.Last() == animatedObject) _controller.DropBlocksAnimationEnded();
        }

        public void DisplayNextShape(TetrisShape shape)
        {
            UnityEngine.Object.Destroy(_nextShape);
            _nextShape = CreateShape(shape);
            _nextShape.transform.position = Centre;
        }

        public void DisplayGameOver()
        {
        }

        public static GameObject CreateShape(TetrisShape modelShape)
        {
            var shape = new GameObject("TetrisShape");

            var hexCode = modelShape.HexColor;
            var color = hexCode.ToColor();

            foreach (var block in modelShape.Blocks)
            {
                float x = block.X;
                float y = block.Y;

                var localVar = UnityEngine.Object.Instantiate(_block);
                localVar.transform.parent = shape.transform;
                localVar.transform.position = new Vector2(x * BLOCK_SIZE, y * BLOCK_SIZE);
                localVar.GetComponent<SpriteRenderer>().color = color;
            }
            return shape;
        }

        private void OnAttachedBlock(int x, int y)
        {
            _currentShape.transform.GetChild(0).transform.parent = _lines[y].transform;
        }

        private void OnMovementDone(object sender, MovementEventArgs e)
        {
            MoveObject(e.MoveVector, _currentShape);
        }

        private void OnRotateDone(object sender, EventArgs e)
        {
            RotateShape(_model.CurrentShape);
        }

        private void OnShapeAdded(object sender, EventArgs e)
        {
            DisplayNextShape(_model.NextShape);
            SpawnShape(_model.CurrentShape, _model.CurrentShapeCoord);
        }

        private void OnShapeIsAttached(object sender, EventArgs e)
        {
            UnityEngine.Object.Destroy(_currentShape);
        }

        private void OnLineDestroyed(object sender, LineIndexEventArgs e)
        {
            DestroyLine(e.LineIndex);
            DropBlocks(e.LineIndex);
        }

        private void OnGameOver(object sender, EventArgs e)
        {
            Debug.Log("Game Over!");
            Application.LoadLevel("MainMenu");
        }

        private void OnShapeDropping(object sender, DroppingEventArgs e)
        {
        }
    }

}
