using System.Collections.Generic;
using UnityEngine;
using Assets.MVC.Controller;
using Assets.MVC.View;
using Assets.MVC.Model;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class PlayerControllerScript : MonoBehaviour
    {
        public AudioSource BackgroundMusic;
        public Toggle MuteToggle;

        private Controller _controller;
        private Controller _controller2;
        private TetrisModel _model;
        private TetrisModel _model2;

        private const float TIME_BETWEEN_FALLS = 0.4f;
        private const float ACCELERATED_TIME_BETWEEN_FALLS = 0.01f;
        private const float HORIZONTAL_SPEED = 0.03f;

        private readonly float[] _mCurrentTimeBetweenFalls = {TIME_BETWEEN_FALLS, TIME_BETWEEN_FALLS};

        private readonly float[] _mVerticalTimeCounter = {0f, 0f};
        private readonly float[] _mHorizontalTimeCounter = {0f, 0f};


        private Dictionary<string, string[]> _inputKeys = new Dictionary<string, string[]>
        {
            { "Left", new []{ "Left_player1", "Left_player2"}},
            { "Right", new []{ "Right_player1", "Right_player2"}},
            { "Down", new []{"Down_player1", "Down_player2"}},
            { "Drop", new []{"Drop_player1", "Drop_player2"}},
            { "RotateCW", new []{"RotateCW_player1", "RotateCW_player2"}},
            { "RotateCCW", new []{"RotateCCW_player1", "RotateCCW_player2"}}
        };

        void Start()
        {
            
            if (BackgroundMusic != null)
            {
                BackgroundMusic = Instantiate(BackgroundMusic);
                BackgroundMusic.Play();
            }
            MuteToggle.onValueChanged.AddListener(isMute => BackgroundMusic.mute = isMute);

            _model = new TetrisModel(10, 24, "first");
            _controller = new Controller(_model);
            var view = new TetrisView(_model, _controller);
            view.NewGame(new Vector3(-5, 0));

            //_model2 = new TetrisModel(10, 24, "second");
            //_controller2 = new Controller(_model2);
            //var view2 = new TetrisView(_model2, _controller2);
            //view2.NewGame(new Vector3(5, 0));

        }

        void Update()
        {            
            HandleEvents(_controller, _model, 0);
            //HandleEvents(_controller2, _model2, 1);
        }

        void HandleEvents(Controller controller, TetrisModel model, int playerNumber)
        {
            if (model.IsOnPause) return;

            if (Input.GetButtonDown(_inputKeys["Drop"][playerNumber])) controller.DropTrigger();

            if (Input.GetButtonDown(_inputKeys["Down"][playerNumber])) controller.MoveTrigger(MoveDirection.Down);
            if (Input.GetButton(_inputKeys["Down"][playerNumber])) _mCurrentTimeBetweenFalls[playerNumber] = ACCELERATED_TIME_BETWEEN_FALLS;

            if (_mHorizontalTimeCounter[playerNumber] < 0)
            {
                _mHorizontalTimeCounter[playerNumber] = HORIZONTAL_SPEED;
                if (Input.GetButton(_inputKeys["Right"][playerNumber])) controller.MoveTrigger(MoveDirection.Right);
                if (Input.GetButton(_inputKeys["Left"][playerNumber])) controller.MoveTrigger(MoveDirection.Left);
            }
            _mHorizontalTimeCounter[playerNumber] -= Time.deltaTime * TIME_BETWEEN_FALLS;

            if (Input.GetButtonDown(_inputKeys["RotateCCW"][playerNumber])) controller.RotateTrigger(RotateDirection.CounterClockWise);
            if (Input.GetButtonDown(_inputKeys["RotateCW"][playerNumber])) controller.RotateTrigger(RotateDirection.ClockWise);

            if (_mVerticalTimeCounter[playerNumber] < 0)
            {
                _mVerticalTimeCounter[playerNumber] = _mCurrentTimeBetweenFalls[playerNumber] / model.Level;
                controller.MoveTrigger(MoveDirection.Down);
            }
            _mVerticalTimeCounter[playerNumber] -= Time.deltaTime * TIME_BETWEEN_FALLS;
            _mCurrentTimeBetweenFalls[playerNumber] = TIME_BETWEEN_FALLS;
        }

        void OnGUI()
        {
            GUI.Label(new Rect(50, 50, 100, 50), "Score: " + _model.Score);
            GUI.Label(new Rect(200, 50, 100, 50), "Level:  " + _model.Level);
        }
    }

}
