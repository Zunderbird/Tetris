using UnityEngine;
using UnityEngine.UI;
using InputKeys = System.Collections.Generic.Dictionary<string, System.Func<bool>>;

namespace Assets.MVC.View.Android
{   
    public class GameInitializerAndroidScript : GameInitializerScript
    {
        public Canvas GameCanvas;
        public Text LevelText;
        public Text ScoreText;

        private PlayerController _player;

        private readonly InputKeys _inputKeys = 
        new InputKeys
        {
            { "Left", TouchControls.LeftShift },
            { "Right", TouchControls.RightShift },
            { "Down", TouchControls.DownwardShift },
            { "Drop", TouchControls.DoubleTouch },
            { "RotateCW", TouchControls.Rotate },
            { "RotateCCW", () => false }
        };

        void Start()
        {
            _player = new PlayerController(_inputKeys, "MainMenu_android");
            _player.GameView.NewGame(GameCanvas);
        }

        void Update()
        {
            _player.HandleEvents();
        }

        void OnGUI()
        {
            LevelText.text = "Level: " + _player.GameModel.Level;
            ScoreText.text = "Score: " + _player.GameModel.Score;
        }
    }

}

