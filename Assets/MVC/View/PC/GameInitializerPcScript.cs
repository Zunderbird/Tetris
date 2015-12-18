using UnityEngine;

using InputKeys = System.Collections.Generic.Dictionary<string, System.Func<bool>>;

namespace Assets.MVC.View.PC
{   
    public class GameInitializerPcScript : GameInitializerScript
    {
        private PlayerController[] _players;

        private readonly InputKeys[] _inputKeys = {
        new InputKeys
        {
            { "Left", () => Input.GetButton("Left_player1") },
            { "Right", () => Input.GetButton("Right_player1") },
            { "Down", () => Input.GetButton("Down_player1") },
            { "Drop", () => Input.GetButtonDown("Drop_player1") },
            { "RotateCW", () => Input.GetButtonDown("RotateCW_player1") },
            { "RotateCCW", () => Input.GetButtonDown("RotateCCW_player1") },
        },
        new InputKeys
        {
            { "Left", () => Input.GetButton("Left_player2") },
            { "Right", () => Input.GetButton("Right_player2") },
            { "Down", () => Input.GetButton("Down_player2") },
            { "Drop", () => Input.GetButtonDown("Drop_player2") },
            { "RotateCW", () => Input.GetButtonDown("RotateCW_player2") },
            { "RotateCCW", () => Input.GetButtonDown("RotateCCW_player2") }

        }
        };

        void Start()
        {
            _players = new PlayerController[PlayerPrefs.GetInt("PlayersCount")];

            for (var i = 0; i < PlayerPrefs.GetInt("PlayersCount"); i++)
            {
                _players[i] = new PlayerController(_inputKeys[i], "MainMenu_pc");
                _players[i].GameView.NewGame(GetPlayersBoardPosition(i));
            }
        }

        private static Vector3 GetPlayersBoardPosition(int index)
        {
            //TODO: New formula for getting x coordinate
            var x = (index*10 - 5)*(PlayerPrefs.GetInt("PlayersCount") - 1);
            return new Vector3(x, 0);
        }

        void Update()
        {
            for (var i = 0; i < PlayerPrefs.GetInt("PlayersCount"); i++)
            {
                _players[i].HandleEvents();
            }
        }

        void OnGUI()
        {
            //TODO: View Score and Level as child of Players boards
            for (var i = 0; i < PlayerPrefs.GetInt("PlayersCount"); i++)
            {               
                GUI.Label(new Rect(250 + i*400, 50, 100, 50), "Score: " + _players[i].GameModel.Score);
                GUI.Label(new Rect(400 + i*400, 50, 100, 50), "Level:  " + _players[i].GameModel.Level);
            }
        }
    }

}

