using System.Collections.Generic;
using UnityEngine;
using Assets.MVC.Model;

namespace Assets.MVC.View
{
    public class PlayerController
    {
        public Controller.Controller GameController { get; private set; }
        public TetrisModel GameModel { get; private set; }
        public TetrisView GameView { get; private set; }

        private const float TIME_BETWEEN_FALLS = 0.4f;
        private const float ACCELERATED_TIME_BETWEEN_FALLS = 0.01f;
        private const float HORIZONTAL_SPEED = 0.03f;

        private float _currentTimeBetweenFalls;

        private float _verticalTimeCounter;
        private float _horizontalTimeCounter;

        private readonly Dictionary<string, string> _inputKeys; 

        public PlayerController(Dictionary<string, string> inputKeys)
        {
            _inputKeys = inputKeys;

            GameModel = new TetrisModel(Configuration.BoardWidth, Configuration.BoardHeight);
            GameController = new Controller.Controller(GameModel);
            GameView = new TetrisView(GameModel, GameController);
            GameModel.GameOver += (sender, args) => Application.LoadLevel("MainMenu_pc");
        }

        public void HandleEvents()
        {
            if (GameModel.IsOnPause) return;

            if (Input.GetButtonDown(_inputKeys["Drop"])) GameController.DropTrigger();

            if (Input.GetButtonDown(_inputKeys["Down"])) GameController.MoveTrigger(MoveDirection.Down);
            if (Input.GetButton(_inputKeys["Down"])) _currentTimeBetweenFalls = ACCELERATED_TIME_BETWEEN_FALLS;

            if (_horizontalTimeCounter < 0)
            {
                _horizontalTimeCounter = HORIZONTAL_SPEED;
                if (Input.GetButton(_inputKeys["Right"])) GameController.MoveTrigger(MoveDirection.Right);
                if (Input.GetButton(_inputKeys["Left"])) GameController.MoveTrigger(MoveDirection.Left);
            }
            _horizontalTimeCounter -= Time.deltaTime * TIME_BETWEEN_FALLS;

            if (Input.GetButtonDown(_inputKeys["RotateCCW"])) GameController.RotateTrigger(RotateDirection.CounterClockWise);
            if (Input.GetButtonDown(_inputKeys["RotateCW"])) GameController.RotateTrigger(RotateDirection.ClockWise);

            if (_verticalTimeCounter < 0)
            {
                _verticalTimeCounter = _currentTimeBetweenFalls / GameModel.Level;
                GameController.MoveTrigger(MoveDirection.Down);
            }
            _verticalTimeCounter -= Time.deltaTime * TIME_BETWEEN_FALLS;
            _currentTimeBetweenFalls = TIME_BETWEEN_FALLS;
        }
    }

}
