using UnityEngine;
using UnityEngine.UI;

public class CongratulationsCanvasScript : MonoBehaviour
{
    public Text RecordText;
    public Button RecordOkButton;

    void OnEnable()
    {
        Time.timeScale = 0;
    }

    void OnDisable()
    {
        Time.timeScale = 1;
    }

    void Start ()
    {
        RecordOkButton.onClick.AddListener((() => transform.gameObject.SetActive(false)));
    }

}
