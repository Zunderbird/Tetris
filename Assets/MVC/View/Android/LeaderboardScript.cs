using UnityEngine;
using UnityEngine.UI;

public class LeaderboardScript : MonoBehaviour
{
    public Button BackButton;

    public Canvas MainMenuCanvas;

	void Start ()
    {
        BackButton.onClick.AddListener(MainMenuMode);
	}

    private void MainMenuMode()
    {
        transform.gameObject.SetActive(false);
        MainMenuCanvas.gameObject.SetActive(true);
    }

}
