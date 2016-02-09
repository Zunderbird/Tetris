using UnityEngine;
using UnityEngine.UI;

namespace Assets.MVC.View
{
    public abstract class MainMenuScript : GameInitializerScript
    {
        public Button NewGameButton;
        public Button SettingsButton;
        public Button LeaderboardsButton;
        public Button ExitButton;       

        public Canvas SettingsCanvas;
        public Canvas LeaderboardsCanvas;

        protected abstract void NewGame();

        new void Awake()
        {
            base.Awake();

            NewGameButton.onClick.AddListener(NewGame);
            SettingsButton.onClick.AddListener(SettingsMode);   
            LeaderboardsButton.onClick.AddListener(LeaderboardsMode);  
            ExitButton.onClick.AddListener(Application.Quit);
        }

        private void SettingsMode()
        {
            transform.gameObject.SetActive(false);
            SettingsCanvas.gameObject.SetActive(true);
        }

        private void LeaderboardsMode()
        {
            transform.gameObject.SetActive(false);
            LeaderboardsCanvas.gameObject.SetActive(true);
        }
    }
}
