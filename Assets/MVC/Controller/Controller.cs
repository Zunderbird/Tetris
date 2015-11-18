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
            _model.MoveShape(direction, true);
        }

        public void DropTrigger()
        {
            _model.DropShape();
        }

        public void DroppingBlocksAnimationStart()
        {
            _model.IsOnPause = true;
        }

        public void DroppingBlocksAnimationEnded()
        {
            _model.IsOnPause = false;
        }

        public void DroppingShapeAnimationStart()
        {
            _model.IsOnPause = true;
        }

        public void DroppingShapeAnimationEnded()
        {
            //UnityEngine.Debug.Log("Animation Ended");
            _model.AttachShape();
            _model.IsOnPause = false;
        }

    }
}

