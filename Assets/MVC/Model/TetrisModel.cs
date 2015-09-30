using System;
using System.Collections.Generic;

namespace Assets.MVC.Model
{
    public class TetrisModel
    {
        private readonly Board _mBoard;

        private TetrisShape _mCurrentShape;
        private TetrisShape _mNextShape;

        private List<int> _mCollectedLine;

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
        public event EventHandler ShapeIsDropping;

        public TetrisModel(int width, int height)
        {
            Score = 0;
            CollectedCount = 0;
            Level = 1;

            _mBoard = new Board(width, height);

            _mNextShape = GenerateNextShape();
            AddShape();

            _mCollectedLine = new List<int>();
        }

        public TetrisShape NextShape
        {
            get { return new TetrisShape(_mNextShape); }
        }

        public TetrisShape CurrentShape
        {
            get { return new TetrisShape(_mCurrentShape); }
        }

        public Point CurrentShapeCoord
        {
            get { return new Point(_mBoard.CurrentShapeCoord); }
        }

        public List<int> CollectedLine
        {
            get { return new List<int>(_mCollectedLine); }
        }

        private TetrisShape GenerateNextShape()
        {
            var newShape = JsonReader.GetRandomClassicShape();
            newShape.HexColor = JsonReader.GetRandomColorForLevel(Level);
            return newShape;
        }

        private bool AddShape()
        {
            _mCurrentShape = _mNextShape;
            _mNextShape = GenerateNextShape();
            _mBoard.CurrentShapeCoord = new Point((_mBoard.Width - _mCurrentShape.Width) / 2, _mBoard.Height - _mCurrentShape.Height - 2);
            return _mBoard.CheckShapeOffset(_mCurrentShape, new Point(0, 0));
        }

        public bool MoveShape(MoveDirection moveDirection)
        {
            Point offset;

            switch (moveDirection)
            {
                case MoveDirection.Left:
                    {
                        offset = new Point(-1, 0);
                    }
                    break;
                case MoveDirection.Right:
                    {
                        offset = new Point(1, 0);
                    }
                    break;
                case MoveDirection.Down:
                    {
                        offset = new Point(0, -1);
                    }
                    break;
                default: return false;
            }

            var isMovementDone = IsMovementDone(offset, moveDirection);
            if (isMovementDone)
            {
                var eventArg = new MovementEventArgs();
                eventArg.MoveDirect = moveDirection;
                MovementDone(eventArg);
            }
            return isMovementDone;
        }

        public void DropShape()
        {
            while (MoveShape(MoveDirection.Down) == true)
            {
                ShapeIsDropping(this, EventArgs.Empty);
            }
        }

        public void RotateShape(RotateDirection rotateDirection)
        {
            var shape = _mCurrentShape.Rotate(rotateDirection);

            if (_mBoard.CheckShapeOffset(shape, new Point(0, 0)) == true)
            {
                _mCurrentShape = shape;
                RotateDone(this, EventArgs.Empty);
            }
        }

        private bool IsMovementDone(Point offset, MoveDirection moveDirection)
        {
            if (_mBoard.CheckShapeOffset(_mCurrentShape, offset) == true)
            {
                _mBoard.CurrentShapeCoord += offset;
                return true;
            }
            else if (moveDirection == MoveDirection.Down)
            {
                FinishMovement();
            }
            return false;
        }

        private void FinishMovement()
        {
            _mCollectedLine = _mBoard.AttachShape(_mCurrentShape);
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
            while (_mCollectedLine.Count > 0)
            {
                LineIsCollected(this, EventArgs.Empty);
                GaineScorePoints();
                _mBoard.DestroyLine(_mCollectedLine[_mCollectedLine.Count - 1]);
                _mCollectedLine.RemoveAt(_mCollectedLine.Count - 1);
            }
        }

        private void GaineScorePoints()
        {
            const int collectedToNextLevel = 4;

            Score += Level;
            CollectedCount += 1;

            if (CollectedCount % collectedToNextLevel == 0) LevelUp();
        }

        private void LevelUp()
        {
            Level += 1;
        }
    }


}