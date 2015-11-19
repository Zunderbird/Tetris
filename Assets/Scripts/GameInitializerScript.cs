using System.Collections.Generic;
using UnityEngine;
using Assets.MVC.Model;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class GameInitializerScript : MonoBehaviour
    {
        public AudioSource BackgroundMusic;
        public Toggle MuteToggle;

        private PlayerController[] _players;

        private readonly Dictionary<string, string>[] _inputKeys = {
        new Dictionary<string, string>
        {
            { "Left", "Left_player1" },
            { "Right", "Right_player1" },
            { "Down", "Down_player1" },
            { "Drop", "Drop_player1" },
            { "RotateCW", "RotateCW_player1" },
            { "RotateCCW", "RotateCCW_player1" }

        },
        new Dictionary<string, string>
        {
            { "Left", "Left_player2" },
            { "Right", "Right_player2" },
            { "Down", "Down_player2" },
            { "Drop", "Drop_player2" },
            { "RotateCW", "RotateCW_player2" },
            { "RotateCCW", "RotateCCW_player2" }
        }
        };

        void Start()
        {
            if (BackgroundMusic != null)
            {
                BackgroundMusic = Instantiate(BackgroundMusic);
                BackgroundMusic.Play();
            }
            MuteToggle.onValueChanged.AddListener(isMute => BackgroundMusic.mute = isMute);

            _players = new PlayerController[Configuration.PlayersCount];

            for (var i = 0; i < Configuration.PlayersCount; i++)
            {
                _players[i] = new PlayerController(_inputKeys[i]);
                _players[i].GameView.NewGame(GetPlayersBoardPosition(i));
            }
        }

        private static Vector3 GetPlayersBoardPosition(int index)
        {
            //TODO: New formula for getting x coordinate
            var x = (index*10 - 5)*(Configuration.PlayersCount-1);
            return new Vector3(x, 0);
        }

        void Update()
        {
            for (var i = 0; i < Configuration.PlayersCount; i++)
            {
                _players[i].HandleEvents();
            }
        }

        void OnGUI()
        {
            //TODO: View Score and Level as child of Players boards
            for (var i = 0; i < Configuration.PlayersCount; i++)
            {               
                GUI.Label(new Rect(250 + i*400, 50, 100, 50), "Score: " + _players[i].GameModel.Score);
                GUI.Label(new Rect(400 + i*400, 50, 100, 50), "Level:  " + _players[i].GameModel.Level);
            }
        }
    }

}

