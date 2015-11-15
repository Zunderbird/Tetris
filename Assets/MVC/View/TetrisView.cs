using UnityEngine;
using System;
using Assets.MVC.Model;

namespace Assets.MVC.View
{
    public class TetrisView : ITetrisView
    {
        private GameObject _mCurrentShape;
        private GameObject _mNextShape;
        private readonly GameObject _mShapeHeap;
        private readonly TetrisModel _mModel;

        public TetrisView(TetrisModel iModel)
        {
            _mModel = iModel;

            _mShapeHeap = new GameObject("ShapeHeap");

            _mModel.MovementDone += OnMovementDone;
            _mModel.RotateDone += OnRotateDone;
            _mModel.ShapeIsAdded += OnShapeIsAdded;
            _mModel.ShapeIsAttached += OnShapeIsAttached;
            _mModel.LineIsCollected += OnLineIsCollected;
            _mModel.GameOver += OnGameOver;
            _mModel.ShapeIsDropping += OnShapeIsDropping;

            NewGame();
        }

        private void NewGame()
        {
            DisplayBoard();
            DisplayNextShape(_mModel.NextShape);
            SpawnShape(_mModel.CurrentShape, _mModel.CurrentShapeCoord);
        }

        public void DisplayBoard()
        {
            var board = UnityEngine.Object.Instantiate((GameObject)Resources.Load("Board"));
            board.transform.position = new Vector3(0f, 0f);
        }

        public void SpawnShape(TetrisShape shape, Point spawnCoord)
        {
            _mCurrentShape = CreateShape(shape);
            _mCurrentShape.transform.position = new Vector2(spawnCoord.X * 0.5f - 2.25f, spawnCoord.Y * 0.5f - 5.25f);
        }

        public void PaintShape(Color color)
        {
            foreach (Transform block in _mCurrentShape.transform)
            {
                block.GetComponent<SpriteRenderer>().color = color;
            }
        }

        public void MoveShape(MoveDirection moveDirection)
        {
            switch (moveDirection)
            {
                case MoveDirection.Right:
                    {
                        _mCurrentShape.transform.position += _mCurrentShape.transform.right * (0.5f);
                    }
                    break;
                case MoveDirection.Left:
                    {
                        _mCurrentShape.transform.position += _mCurrentShape.transform.right * (-0.5f);
                    }
                    break;
                case MoveDirection.Up:
                    {
                        _mCurrentShape.transform.position += _mCurrentShape.transform.up * (0.5f);
                    }
                    break;
                case MoveDirection.Down:
                    {
                        _mCurrentShape.transform.position += _mCurrentShape.transform.up * (-0.5f);
                    }
                    break;
            }
        }

        public void RotateShape(TetrisShape shape)
        {
            Vector2 currentShapePosition = _mCurrentShape.transform.position;

            UnityEngine.Object.Destroy(_mCurrentShape);

            _mCurrentShape = CreateShape(shape);
            _mCurrentShape.transform.position = currentShapePosition;

        }

        public void DestroyLine(int number)
        {
            foreach (Transform block in _mShapeHeap.transform)
            {
                if (block.transform.position.y == 0.5f * number - 5.25f)
                {
                    UnityEngine.Object.Destroy(block.gameObject);
                }
                else if (block.transform.position.y > 0.5f * number - 5.25f)
                {
                    block.transform.position += block.transform.up * (-0.5f);
                }
            }
        }

        public void DisplayNextShape(TetrisShape shape)
        {
            UnityEngine.Object.Destroy(_mNextShape);
            _mNextShape = CreateShape(shape);
            _mNextShape.transform.position = new Vector2(3.75f, 3.75f);
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

                var localVar = UnityEngine.Object.Instantiate((GameObject)Resources.Load("block"));
                localVar.transform.parent = shape.transform;
                localVar.transform.position = new Vector2(x * 0.5f, y * 0.5f);
                localVar.GetComponent<SpriteRenderer>().color = color;
            }
            return shape;
        }

        private void AttachShape()
        {
            while (_mCurrentShape.transform.childCount > 0)
            {
                _mCurrentShape.transform.GetChild(0).transform.parent = _mShapeHeap.transform;
            }
            UnityEngine.Object.Destroy(_mCurrentShape);
        }

        private void OnMovementDone(MovementEventArgs e)
        {
            MoveShape(e.MoveDirect);
        }

        private void OnRotateDone(object sender, EventArgs e)
        {
            RotateShape(_mModel.CurrentShape);
        }

        private void OnShapeIsAdded(object sender, EventArgs e)
        {
            DisplayNextShape(_mModel.NextShape);
            SpawnShape(_mModel.CurrentShape, _mModel.CurrentShapeCoord);
        }

        private void OnShapeIsAttached(object sender, EventArgs e)
        {
            AttachShape();
        }

        private void OnLineIsCollected(object sender, EventArgs e)
        {
            DestroyLine(_mModel.CollectedLine[_mModel.CollectedLine.Count - 1]);
        }

        private void OnGameOver(object sender, EventArgs e)
        {
            Debug.Log("Game Over!");
            Application.LoadLevel("MainMenu");
        }

        private void OnShapeIsDropping(DroppingEventArgs e)
        {
        }
    }

}
