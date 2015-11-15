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
        }

        private static void NewGame()
        {
            Application.LoadLevel("Stage_01");
        }
    }
}
