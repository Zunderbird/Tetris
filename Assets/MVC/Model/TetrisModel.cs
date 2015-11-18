using System;
using System.Collections.Generic;

namespace Assets.MVC.Model
{
    public class TetrisModel
    {
        private readonly Board _board;

        private TetrisShape _currentShape;
        private TetrisShape _nextShape;

        private List<int> _collectedLine;
        private bool _isOnPause;

        public string PlayerName { get; private set; }
        public int Level { get; set; } 
        public int Score { get; private set; }
        public int CollectedCount { get; private set; }

        public const int COLLECT_TO_NEXT_LEVEL = 4;

        public delegate void MovementEvent(object sender, MovementEventArgs e);
        public event MovementEvent MovementDone;

        public delegate void DroppingEvent(object sender, DroppingEventArgs e);
        public event DroppingEvent ShapeDropping;

        public delegate void DestroyingEvent(object sender, LineIndexEventArgs e);
        public event DestroyingEvent LineDestroyed;

        public event EventHandler RotateDone;
        public event EventHandler ShapeAdded;
        public event EventHandler GameOver;

        private readonly Dictionary<MoveDirection, Point> _offsets = new Dictionary<MoveDirection, Point>
            {
                { MoveDirection.Right, new Point(1, 0) },
                { MoveDirection.Left, new Point(-1, 0) },
                { MoveDirection.Up, new Point(0, 1) },
                { MoveDirection.Down, new Point(0, -1) },
            };

        public bool IsOnPause
        {
            get { return _isOnPause; }
            set
            {
                //UnityEngine.Debug.Log("Is on pause changed, was " + _isOnPause + ", now it's " + value);
                _isOnPause = value;
                if (_isOnPause == false) FinishMovement();
            }            
        }

        public TetrisModel(int width, int height, string playerIndex)
        {
            PlayerName = playerIndex;

            Score = 0;
            CollectedCount = 0;
            Level = 1;

            _board = new Board(width, height);

            _nextShape = GenerateNextShape();
            TryToAddShape();

            _collectedLine = new List<int>();
        }

        public int BoardWidth
        {
            get { return _board.Width; }
        }

        public int BoardHeight
        {
            get { return _board.Height; }
        }

        public TetrisShape NextShape
        {
            get { return new TetrisShape(_nextShape); }
        }

        public TetrisShape CurrentShape
        {
            get { return new TetrisShape(_currentShape); }
        }

        public Point CurrentShapeCoord
        {
            get { return new Point(_board.CurrentShapeCoord); }
        }

        public void BlockIsAttachedAddListener(Action<object, int, int> action)
        {
            _board.BlockIsAttached += (sender, coord) => action(sender, coord.X, coord.Y);
        }

        public void ShapeIsAttachedAddListener(Action<object, EventArgs> action)
        {
            _board.ShapeIsAttached += (sender, arg) => action(sender, arg);
        }

        private TetrisShape GenerateNextShape()
        {
            var newShape = JsonReader.GetRandomClassicShape();
            newShape.HexColor = JsonReader.GetRandomColorForLevel(Level);
            return newShape;
        }

        private bool TryToAddShape()
        {
            _currentShape = _nextShape;
            _nextShape = GenerateNextShape();
            _board.CurrentShapeCoord = new Point((_board.Width - _currentShape.Width) / 2, _board.Height - _currentShape.Height - 2);
            return _board.CheckShapeOffset(_currentShape, new Point(0, 0));
        }

        public bool MoveShape(MoveDirection moveDirection, bool broadcastEvent)
        {
            if (_isOnPause) return false;
            var offset = _offsets[moveDirection];

            if (_board.CheckShapeOffset(_currentShape, offset))
            {
                _board.CurrentShapeCoord += offset;

                if (broadcastEvent && MovementDone != null)
                    MovementDone(this, new MovementEventArgs(offset));

                return true;
            }
            if (moveDirection == MoveDirection.Down)
            {
                if (broadcastEvent)
                {
                    //UnityEngine.Debug.Log("simple moving down");
                    AttachShape();
                    FinishMovement();
                }    
            }
            return false;
        }

        public void DropShape()
        {
            var counter = 0; 
            while (MoveShape(MoveDirection.Down, false))
            {
                counter++;
            }          
            if (ShapeDropping != null) ShapeDropping(this, new DroppingEventArgs(counter));
        }

        public void AttachShape()
        {
            _collectedLine = _board.AttachShape(_currentShape);
            //UnityEngine.Debug.Log("Attached, Collected lines: " + _collectedLine.Count);
        }

        public void RotateShape(RotateDirection rotateDirection)
        {
            var shape = _currentShape.Rotate(rotateDirection);

            if (_board.CheckShapeOffset(shape, new Point(0, 0)))
            {
                _currentShape = shape;
                if (RotateDone != null) RotateDone(this, EventArgs.Empty);
            }
        }

        public void FinishMovement()
        {         
            if (_collectedLine.Count == 0)
            {
                if (TryToAddShape())
                {
                    if (ShapeAdded != null) ShapeAdded(this, EventArgs.Empty);
                }
                else if (GameOver != null) GameOver(this, EventArgs.Empty);
            }
            else
            {
                DestroyFirstCollectedLine();
            }
        }

        public bool DestroyFirstCollectedLine()
        {
            if (_collectedLine.Count == 0) return true;

            var lineIndex = _collectedLine[_collectedLine.Count - 1];                     
            if (LineDestroyed != null) LineDestroyed(this, new LineIndexEventArgs(lineIndex));

            GaineScorePoints();

            _board.DestroyLine(lineIndex);
            _collectedLine.RemoveAt(_collectedLine.Count - 1);

            return (_collectedLine.Count == 0);
        }

        private void GaineScorePoints()
        {
            Score += Level;
            CollectedCount += 1;

            if (CollectedCount % COLLECT_TO_NEXT_LEVEL == 0) LevelUp();
        }

        private void LevelUp()
        {
            Level += 1;
        }
    }


}