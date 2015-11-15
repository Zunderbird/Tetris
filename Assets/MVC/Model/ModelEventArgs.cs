using System;

namespace Assets.MVC.Model
{
    public class MovementEventArgs : EventArgs
    {
        public MoveDirection MoveDirect { get; set; }

        public MovementEventArgs(MoveDirection moveDirection)
        {
            MoveDirect = moveDirection;
        }
    }

    public class DroppingEventArgs : EventArgs
    {
        public int Distance { get; set; }

        public DroppingEventArgs(int distance)
        {
            Distance = distance;
        }
    }
}


