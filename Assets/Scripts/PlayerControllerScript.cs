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
        private TetrisModel _model;

        private const float TIME_BETWEEN_FALLS = 0.4f;
        private const float ACCELERATED_TIME_BETWEEN_FALLS = 0.01f;
        private const float HORIZONTAL_SPEED = 0.03f;

        private float _mCurrentTimeBetweenFalls = TIME_BETWEEN_FALLS;

        private float _mVerticalTimeCounter;
        private float _mHorizontalTimeCounter;

        void Start()
        {
            
            if (BackgroundMusic != null)
            {
                BackgroundMusic = Instantiate(BackgroundMusic);
                BackgroundMusic.Play();
            }
              
            _model = new TetrisModel(10, 24);

            var view = new TetrisView(_model);

            _controller = new Controller(_model);

            MuteToggle.onValueChanged.AddListener(isMute => BackgroundMusic.mute = isMute);
        }

        void Update()
        {
            if (Input.GetButtonDown("Drop"))  _controller.DropTrigger(); 

            if (Input.GetButtonDown("Down")) _controller.MoveTrigger(MoveDirection.Down);
            if (Input.GetButton("Down")) _mCurrentTimeBetweenFalls = ACCELERATED_TIME_BETWEEN_FALLS;

            if (_mHorizontalTimeCounter < 0)
            {
                _mHorizontalTimeCounter = HORIZONTAL_SPEED;
                if (Input.GetButton("Right")) _controller.MoveTrigger(MoveDirection.Right);
                if (Input.GetButton("Left")) _controller.MoveTrigger(MoveDirection.Left);
            }
            _mHorizontalTimeCounter -= Time.deltaTime * TIME_BETWEEN_FALLS;

            if (Input.GetButtonDown("RotateCCW")) _controller.RotateTrigger(RotateDirection.CounterClockWise);
            if (Input.GetButtonDown("RotateCW")) _controller.RotateTrigger(RotateDirection.ClockWise);

            if (_mVerticalTimeCounter < 0)
            {
                _mVerticalTimeCounter = _mCurrentTimeBetweenFalls / _model.Level;
                _controller.MoveTrigger(MoveDirection.Down);
            }
            _mVerticalTimeCounter -= Time.deltaTime * TIME_BETWEEN_FALLS;
            _mCurrentTimeBetweenFalls = TIME_BETWEEN_FALLS;

        }

        void OnGUI()
        {
            GUI.Label(new Rect(50, 50, 100, 50), "Score: " + _model.Score);
            GUI.Label(new Rect(200, 50, 100, 50), "Level:  " + _model.Level);
        }
    }

}
