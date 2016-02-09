using UnityEngine;
using UnityEngine.UI;

namespace Assets.MVC.View
{
    public abstract class GameInitializerScript: MonoBehaviour
    {
        public AudioSource BackgroundMusic;
        public Toggle MuteToggle;

        protected void Awake()
        {
            CheckPlayerPrefs();
            SetMusic();
        }

        private static void CheckPlayerPrefs()
        {
            if (!PlayerPrefs.HasKey("PlayerName")) PlayerPrefs.SetString("PlayerName", "New player");
            if (!PlayerPrefs.HasKey("IsMute")) PlayerPrefs.SetString("IsMute", "false");
            if (!PlayerPrefs.HasKey("BestScore")) PlayerPrefs.SetInt("BestScore", 0);
            //if (!PlayerPrefs.HasKey("BoardWidth")) PlayerPrefs.SetInt("BoardWidth", 10);
            //if (!PlayerPrefs.HasKey("BoardHeight")) PlayerPrefs.SetInt("BoardHeight", 24);
            PlayerPrefs.SetInt("BoardWidth", 10);
            PlayerPrefs.SetInt("BoardHeight", 24);
        }

        private void SetMusic()
        {
            if (BackgroundMusic != null)
            {
                BackgroundMusic = Instantiate(BackgroundMusic);
                BackgroundMusic.Play();
                BackgroundMusic.mute = (PlayerPrefs.GetString("IsMute") == "true");
                MuteToggle.isOn = BackgroundMusic.mute;
            }
            MuteToggle.onValueChanged.AddListener(isMute =>
            {
                BackgroundMusic.mute = isMute;
                PlayerPrefs.SetString("IsMute", isMute ? "true" : "false"); 
            });
        }
    }
}
