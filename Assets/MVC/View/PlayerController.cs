using UnityEngine;
using Assets.MVC.Model;
using InputKeys = System.Collections.Generic.Dictionary<string, System.Func<bool>>;

namespace Assets.MVC.View
{
    public class PlayerController
    {
        public Controller.Controller GameController { get; private set; }
        public TetrisModel GameModel { get; private set; }
        public TetrisView GameView { get; private set; }

        private const float TIME_BETWEEN_FALLS = 0.4f;
        private const float HORIZONTAL_SPEED = 0.03f;

        private float _verticalTimeCounter = TIME_BETWEEN_FALLS;
        private float _horizontalTimeCounter;

        private readonly InputKeys _inputKeys; 

        public PlayerController(InputKeys inputKeys, string mainMenu)
        {
            _inputKeys = inputKeys;

            GameModel = new TetrisModel(PlayerPrefs.GetInt("BoardWidth"), PlayerPrefs.GetInt("BoardHeight"));
            GameController = new Controller.Controller(GameModel);
            GameView = new TetrisView(GameModel, GameController);
            GameModel.GameOver += (sender, args) => Application.LoadLevel(mainMenu);
        }

        public void HandleEvents()
        {
            if (GameModel.IsOnPause) return;

            if (_inputKeys["Drop"]()) GameController.DropTrigger();

            if (_inputKeys["Down"]()) GameController.MoveTrigger(MoveDirection.Down);

            if (_horizontalTimeCounter < 0)
            {
                _horizontalTimeCounter = HORIZONTAL_SPEED;
                if (_inputKeys["Right"]()) GameController.MoveTrigger(MoveDirection.Right);
                if (_inputKeys["Left"]()) GameController.MoveTrigger(MoveDirection.Left);
            }
            _horizontalTimeCounter -= Time.deltaTime * TIME_BETWEEN_FALLS;

            if (_inputKeys["RotateCCW"]()) GameController.RotateTrigger(RotateDirection.CounterClockWise);
            if (_inputKeys["RotateCW"]()) GameController.RotateTrigger(RotateDirection.ClockWise);

            if (_verticalTimeCounter < 0)
            {
                _verticalTimeCounter = TIME_BETWEEN_FALLS / GameModel.Level;
                GameController.MoveTrigger(MoveDirection.Down);
            }
            _verticalTimeCounter -= Time.deltaTime * TIME_BETWEEN_FALLS;
        }
    }

}
