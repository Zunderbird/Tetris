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
            if (!_model.IsOnPause) _model.RotateShape(rotateDirection);
        }

        public void MoveTrigger(MoveDirection direction)
        {
            if (!_model.IsOnPause) _model.CheckMovement(direction);
        }

        public void DropTrigger()
        {
            if (!_model.IsOnPause) _model.DropShape();
        }

        public void DroppingBlocksAnimationEnded()
        {
            _model.FinishMovement();
        }

        public void DroppingShapeAnimationEnded()
        {
            if (!_model.CheckCollectedLines()) _model.FinishMovement();
        }

    }
}

