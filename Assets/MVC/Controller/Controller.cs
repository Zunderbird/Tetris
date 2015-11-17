using Assets.MVC.Model;

namespace Assets.MVC.Controller
{
    public class Controller
    {
        private readonly TetrisModel _model;

        public Controller(TetrisModel model)
        {
            _model = model;
        }

        public void RotateTrigger (RotateDirection rotateDirection)
        {
            _model.RotateShape(rotateDirection);
        }

        public void MoveTrigger(MoveDirection direction)
        {
            _model.MoveShape(direction);
        }

        public void DropTrigger()
        {
            _model.DropShape();
        }

        public void DropBlockAnimationStart()
        {
            _model.IsOnPause = true;
        }

        public void DropBlocksAnimationEnded()
        {
            _model.IsOnPause = false;
        }

    }
}

