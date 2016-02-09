using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PauseCanvasScript : MonoBehaviour
{
    public Button ResumeButton;
    public Button RestartButton;
    public Button ExitButton;

    public Text PauseCounter;

    public int PauseDelayInSeconds = 3;

	void Start ()
    {
        ResumeButton.onClick.AddListener(() => StartCoroutine(ResumeGame()));

        RestartButton.onClick.AddListener(() => Application.LoadLevel("Stage_android"));

        ExitButton.onClick.AddListener((() => Application.LoadLevel("MainMenu_android")));
    }

    void OnEnable()
    {
        Time.timeScale = 0;
    }

    void OnDisable()
    {
        Time.timeScale = 1;
    }

    IEnumerator ResumeGame()
    {
        transform.FindChild("ForegroundPanel").gameObject.SetActive(false);

        var pauseEndTime = Time.realtimeSinceStartup + PauseDelayInSeconds;

        var i = PauseDelayInSeconds;

        while (Time.realtimeSinceStartup < pauseEndTime)
        {
            if (Time.realtimeSinceStartup >= pauseEndTime - i)
                PauseCounter.text = i--.ToString();

            yield return 0;
        }

        transform.gameObject.SetActive(false);
        transform.FindChild("ForegroundPanel").gameObject.SetActive(true);
    }
}
