using Assets.MVC.Model;
using Assets.MVC.View.Scripts;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.MVC.View.PC
{
    class MainMenuPc: MainMenuScript
    {
        public Button PlayerCountButton;

        void Start()
        {      
            PlayerCountButton.onClick.AddListener(ChangePlayersCount);
        }

        protected override void NewGame()
        {
            Application.LoadLevel("Stage_pc");
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
