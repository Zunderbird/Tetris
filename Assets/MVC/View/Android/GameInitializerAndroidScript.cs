using UnityEngine;
using UnityEngine.UI;
using InputKeys = System.Collections.Generic.Dictionary<string, System.Func<bool>>;

namespace Assets.MVC.View.Android
{   
    public class GameInitializerAndroidScript : GameInitializerScript
    {      
        public Text LevelText;
        public Text ScoreText;

        public Button PauseButton;

        public Canvas GameCanvas;
        public Canvas PauseCanvas;
        public Canvas GaveOverCanvas;
        public Canvas CongratulationsCanvas;

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
            PauseButton.onClick.AddListener(() => PauseCanvas.gameObject.SetActive(true));
            CongratulationsCanvas.GetComponent<CongratulationsCanvasScript>().RecordOkButton.onClick.AddListener(ShowGameOver);

            _player = new PlayerController(_inputKeys, GameOverMode);
            _player.GameView.NewGame(GameCanvas);  
        }

        void Update()
        {
            if (Time.timeScale > 0) _player.HandleEvents();
        }

        void OnGUI()
        {
            LevelText.text = "Level: " + _player.GameModel.Level;
            ScoreText.text = "Score: " + _player.GameModel.Score;
        }

        private void GameOverMode(int collectedLinesCount, int score, int level)
        {          
            if (!PlayerPrefs.HasKey("BestScore") || PlayerPrefs.GetInt("BestScore") < score)
            {
                CongratulationsCanvas.gameObject.SetActive(true);
                CongratulationsCanvas.GetComponent<CongratulationsCanvasScript>().RecordText.text = score.ToString();
                PlayerPrefs.SetInt("BestScore", score);
            }
            else
            {
                ShowGameOver();
            }
        }

        private void ShowGameOver()
        {
            GaveOverCanvas.GetComponent<EndGameScript>().SetResults(
                _player.GameModel.CollectedLinesCount, 
                _player.GameModel.Score, 
                _player.GameModel.Level);

            GaveOverCanvas.gameObject.SetActive(true);
            Destroy(transform.gameObject);
        }
    }

}

