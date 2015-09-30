using UnityEngine;
using System.Collections;
using Assets.MVC.Controller;
using Assets.MVC.View;
using Assets.MVC.Model;

namespace Assets.Scripts
{
    public class PlayerControllerScript : MonoBehaviour
    {
        public AudioSource BackgroundMusic;
        public string HexColor;

        Controller _mController;
        TetrisModel _mModel;

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
              
            _mModel = new TetrisModel(10, 24);

            var view = new TetrisView(_mModel);

            _mController = new Controller(_mModel);

        }

        void Update()
        {
            if (Input.GetButtonDown("Drop")) { StartCoroutine(Wait(10)); _mController.DropTrigger(); }

            if (Input.GetButtonDown("Down")) _mController.MoveTrigger(MoveDirection.Down);
            if (Input.GetButton("Down")) _mCurrentTimeBetweenFalls = ACCELERATED_TIME_BETWEEN_FALLS;

            if (_mHorizontalTimeCounter < 0)
            {
                _mHorizontalTimeCounter = HORIZONTAL_SPEED;
                if (Input.GetButton("Right")) _mController.MoveTrigger(MoveDirection.Right);
                if (Input.GetButton("Left")) _mController.MoveTrigger(MoveDirection.Left);
            }
            _mHorizontalTimeCounter -= Time.deltaTime * TIME_BETWEEN_FALLS;

            if (Input.GetButtonDown("RotateCCW")) _mController.RotateTrigger(RotateDirection.CounterClockWise);
            if (Input.GetButtonDown("RotateCW")) _mController.RotateTrigger(RotateDirection.ClockWise);

            if (_mVerticalTimeCounter < 0)
            {
                _mVerticalTimeCounter = _mCurrentTimeBetweenFalls / _mModel.Level;
                _mController.MoveTrigger(MoveDirection.Down);
            }
            _mVerticalTimeCounter -= Time.deltaTime * TIME_BETWEEN_FALLS;
            _mCurrentTimeBetweenFalls = TIME_BETWEEN_FALLS;

        }

        void OnGUI()
        {
            GUI.Label(new Rect(50, 50, 100, 50), "Score: " + _mModel.Score);
            GUI.Label(new Rect(200, 50, 100, 50), "Level:  " + _mModel.Level);
        }

        IEnumerator Wait(int time)
        {
            yield return new WaitForSeconds(time);
        }
    }

}
