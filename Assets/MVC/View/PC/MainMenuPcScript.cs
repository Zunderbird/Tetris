using UnityEngine;
using UnityEngine.UI;

namespace Assets.MVC.View.PC
{
    class MainMenuPcScript: MainMenuScript
    {
        public Button PlayerCountButton;

        void Start()
        {
            if (!PlayerPrefs.HasKey("MaxPlayersCount")) PlayerPrefs.SetInt("MaxPlayersCount", 2);
            if (!PlayerPrefs.HasKey("PlayersCount")) PlayerPrefs.SetInt("PlayersCount", 1);

            PlayerCountButton.onClick.AddListener(ChangePlayersCount);
            PlayerCountButton.transform.GetChild(0).GetComponent<Text>().text
                    = PlayerPrefs.GetInt("PlayersCount") 
                    + ((PlayerPrefs.GetInt("PlayersCount") == 1)? " Player": " Players");
        }

        protected override void NewGame()
        {
            Application.LoadLevel("Stage_pc");
        }

        private void ChangePlayersCount()
        {
            if (PlayerPrefs.GetInt("PlayersCount") >= PlayerPrefs.GetInt("MaxPlayersCount"))
            {
                PlayerPrefs.SetInt("PlayersCount", 1);
                PlayerCountButton.transform.GetChild(0).GetComponent<Text>().text = "1 Player";
            }
            else
            {
                PlayerPrefs.SetInt("PlayersCount", PlayerPrefs.GetInt("PlayersCount") +1);
                PlayerCountButton.transform.GetChild(0).GetComponent<Text>().text 
                    = PlayerPrefs.GetInt("PlayersCount") + " Players";
            }
        }
    }
}
