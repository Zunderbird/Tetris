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
        public Button BackButton;

        new void Awake()
        {
            base.Awake();
            NewGameButton.onClick.AddListener(NewGame);
            SettingsButton.onClick.AddListener(SettingsMode);
            BackButton.onClick.AddListener(MainMenuMode);        

            ExitButton.onClick.AddListener(Application.Quit);
            //TODO: Leaderboards Button event
        }

        protected abstract void NewGame();

        private void SettingsMode()
        {
            SettingsButton.transform.parent.gameObject.SetActive(false);
            BackButton.transform.parent.gameObject.SetActive(true);
        }

        private void MainMenuMode()
        {
            BackButton.transform.parent.gameObject.SetActive(false);
            SettingsButton.transform.parent.gameObject.SetActive(true);  
        }

        
    }
}
