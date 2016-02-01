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
        private GameObject _playersBoard;
        private readonly static Vector2 NextShapePosition = new Vector2(3.75f, 3.75f);

        private GameObject _currentShape;
        private GameObject _nextShape;
        private List<Transform> _animationObjects; 
        
        private List<GameObject> _lines; 
        private readonly TetrisModel _model;
        private readonly Controller.Controller _controller;

        public TetrisView(TetrisModel model, Controller.Controller controller)
        {
            _model = model;
            _controller = controller;

            _model.MovementDone += OnMovementDone;
            _model.RotateDone += OnRotateDone;
            _model.ShapeAdded += OnShapeAdded;
            _model.LinesDestroyed += OnLinesDestroyed;
            _model.GameOver += OnGameOver;
            _model.ShapeDropping += OnShapeDropping;
            _model.ShapeIsAttachedAddListener(OnShapeIsAttached);
            _model.BlockIsAttachedAddListener(OnAttachedBlock);
        }

        public void NewGame(Vector3 boardsOffset)
        {
            _playersBoard = new GameObject("Player");
            _playersBoard.transform.position = boardsOffset;

            _lines = BoardRenderer.CreateBoard(_model.BoardHeight, _model.BoardWidth, _playersBoard.transform);

            DisplayNextShape(_model.NextShape);
            SpawnShape(_model.CurrentShape, _model.CurrentShapeCoord);
        }

        public void NewGame(Canvas gameCanvas)
        {
            NewGame(new Vector3(0f, 0f));
            _playersBoard.transform.SetParent(gameCanvas.transform, false);
        }

        public void SpawnShape(TetrisShape shape, Point spawnCoord)
        {
            _currentShape = ShapeFactory.CreateShape(shape);

            _currentShape.transform.position = new Vector2(
            (spawnCoord.X - (float)(_model.BoardWidth - 1) / 2),
            spawnCoord.Y - (float)_model.BoardHeight/2 + 1)
            *ShapeFactory.BLOCK_SIZE;

            _currentShape.transform.SetParent(_playersBoard.transform, false);
        }

        public void RotateShape(TetrisShape shape)
        {
            var i = 0;
            foreach (Transform block in _currentShape.transform)
            {
                block.localPosition = new Vector3(
                    shape.Blocks[i].X, 
                    shape.Blocks[i++].Y) 
                    * ShapeFactory.BLOCK_SIZE;
            }
        }

        private void DestroyLine(int number)
        {
            foreach (Transform block in _lines[number].transform)
            {
                UnityEngine.Object.Destroy(block.gameObject);
            }
        }

        private void DropBlocks(int lineIndex, Action action)
        {
            _animationObjects = new List<Transform>();

            for (var i = lineIndex + 1; i < _model.BoardHeight; i++)
            {
                while (_lines[i].transform.childCount > 0)
                {
                    var block = _lines[i].transform.GetChild(0);
                    block.SetParent(_lines[i - 1].transform, true);

                    _animationObjects.Add(block);

                    block.DOMoveY(block.transform.position.y - ShapeFactory.BLOCK_SIZE, 0.35f)
                        .OnComplete(() => AnimationCompleted(block, action));
                }
            }

            if (_animationObjects.Count == 0) action();
        }

        private void AnimationCompleted(UnityEngine.Object animatedObject, Action action)
        {
            if (_animationObjects.Last() == animatedObject) action();
        }

        public void DisplayNextShape(TetrisShape shape)
        {
            UnityEngine.Object.Destroy(_nextShape);
            _nextShape = ShapeFactory.CreateShape(shape);
            _nextShape.transform.position = NextShapePosition;
            _nextShape.transform.SetParent(_playersBoard.transform, false);
        }

        public void DisplayGameOver()
        {
        }

        private void OnAttachedBlock(object sender, int x, int y)
        {
            _currentShape.transform.GetChild(0).transform.parent = _lines[y].transform;
        }

        private void OnMovementDone(object sender, MovementEventArgs e)
        {
            _currentShape.transform.position += new Vector3(e.MoveVector.X, e.MoveVector.Y) * ShapeFactory.BLOCK_SIZE;
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

        private void AnimateDestroyingLines(IEnumerator<int> enumerator)
        {
            if (enumerator.MoveNext())
            {
                DestroyLine(enumerator.Current);
                DropBlocks(enumerator.Current, () => AnimateDestroyingLines(enumerator));
            }
            else
            {              
                _controller.DroppingBlocksAnimationEnded();
            }
        }

        private void OnLinesDestroyed(object sender, LineIndexEventArgs e)
        {
            AnimateDestroyingLines(e.LineIndexes.GetEnumerator());
        }

        private void OnGameOver(object sender, EventArgs e)
        {
            Debug.Log("Game Over!");
        }

        private void OnShapeDropping(object sender, DroppingEventArgs e)
        {
            _currentShape.transform.DOMoveY(_currentShape.transform.position.y - ShapeFactory.BLOCK_SIZE*e.Distance, 0.2f)
                .OnComplete(_controller.DroppingShapeAnimationEnded);
        }
    }

}
