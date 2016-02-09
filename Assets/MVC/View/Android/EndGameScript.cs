using UnityEngine;
using UnityEngine.UI;

public class EndGameScript : MonoBehaviour
{
    public Button RestartButton;
    public Button MainMenuButton;

    public Text ScoreText;
    public Text BestScoreText;

    public InputField PlayerName;

	void Start ()
    {
        RestartButton.onClick.AddListener((() => Application.LoadLevel("Stage_android")));
        MainMenuButton.onClick.AddListener((() => Application.LoadLevel("MainMenu_android")));

	    PlayerName.text = PlayerPrefs.GetString("PlayerName");
        PlayerName.onEndEdit.AddListener((arg0 => PlayerPrefs.SetString("PlayerName", arg0)));
    }

    public void SetResults(int collectedLinesCount, int score, int level)
    {    
        ScoreText.text = score.ToString();
        BestScoreText.text = PlayerPrefs.GetInt("BestScore").ToString();
    }
}
