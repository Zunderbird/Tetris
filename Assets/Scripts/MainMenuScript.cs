using Assets.MVC.Model;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class MainMenuScript : MonoBehaviour
    {
        public AudioSource BackgroundMusic;
        public Button NewGameButton;
        public Button SettingsButton;
        public Button RecordsButton;
        public Button PlayerCountButton;
        public Button BackButton;
        public Toggle MuteToggle;

        void Start()
        {
            if (BackgroundMusic != null)
            {
                BackgroundMusic = Instantiate(BackgroundMusic);
                BackgroundMusic.Play();
            }

            MuteToggle.onValueChanged.AddListener(isMute => BackgroundMusic.mute = isMute);
            NewGameButton.onClick.AddListener(NewGame);

            SettingsButton.onClick.AddListener(SettingsMode);
            BackButton.onClick.AddListener(MainMenuMode);

            PlayerCountButton.onClick.AddListener(ChangePlayersCount);
            //TODO: Records Button event
        }

        private static void NewGame()
        {
            Application.LoadLevel("Stage_01");
        }

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

        private void ChangePlayersCount()
        {
            if (Configuration.PlayersCount >= Configuration.MAX_PLAYERS_COUNT)
            {
                Configuration.PlayersCount = 1;
                PlayerCountButton.transform.GetChild(0).GetComponent<Text>().text = Configuration.PlayersCount + " Player";
            }
            else
            {
                Configuration.PlayersCount++;
                PlayerCountButton.transform.GetChild(0).GetComponent<Text>().text = Configuration.PlayersCount + " Players";
            }
        }
    }
}
