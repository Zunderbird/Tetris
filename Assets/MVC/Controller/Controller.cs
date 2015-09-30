using Assets.MVC.Model;

namespace Assets.MVC.Controller
{
    public class Controller
    {
        private readonly TetrisModel _mModel;

        public Controller(TetrisModel model)
        {
            _mModel = model;
        }

        public void RotateTrigger (RotateDirection rotateDirection)
        {
            _mModel.RotateShape(rotateDirection);
        }

        public void MoveTrigger(MoveDirection direction)
        {
            _mModel.MoveShape(direction);
        }

        public void DropTrigger()
        {
            _mModel.DropShape();
        }

    }
}

