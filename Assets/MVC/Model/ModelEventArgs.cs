using System;

namespace Assets.MVC.Model
{
    public class MovementEventArgs : EventArgs
    {
        public MoveDirection MoveDirect { get; set; }
    }
}


