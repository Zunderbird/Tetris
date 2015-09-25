using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour 
{
    public AudioSource backgroundMusic;

    Controller controller;
    TetrisModel model;

    private float m_timeBetweenFalls = 0.3f;

    private float m_verticalTimeCounter = 0f;
    private float m_horizontalTimeCounter = 0f;

	void Start () 
    {
        backgroundMusic = Instantiate(backgroundMusic);
        if (backgroundMusic != null)
            backgroundMusic.Play();

        model = new TetrisModel(10, 24);

        var view = new TetrisView(model);

        controller = new Controller(model);
	}

	void Update () 
    {
        if (m_verticalTimeCounter < 0)
        {
            m_verticalTimeCounter = m_timeBetweenFalls / model.Level;
            controller.MoveTrigger(MoveDirection.Down);
        }
        m_verticalTimeCounter -= Time.deltaTime * 0.5f;
        m_timeBetweenFalls = 0.3f;

        if (m_horizontalTimeCounter < 0)
        {
            m_horizontalTimeCounter = m_timeBetweenFalls * 0.1f;
            if (Input.GetButton("Right")) controller.MoveTrigger(MoveDirection.Right);
            if (Input.GetButton("Left")) controller.MoveTrigger(MoveDirection.Left);
        }
        m_horizontalTimeCounter -= Time.deltaTime * 0.5f;

        if (Input.GetButtonDown("Fire1")) controller.RotateTrigger(RotateDirection.CounterClockWise);
        if (Input.GetButtonDown("Fire2")) controller.RotateTrigger(RotateDirection.ClockWise);
        if (Input.GetButtonDown("Fire3")) controller.RotateTrigger(RotateDirection.ClockWise);

        if (Input.GetButtonDown("Vertical")) controller.MoveTrigger(MoveDirection.Down);
        
        if (Input.GetButton("Vertical")) m_timeBetweenFalls *= 0.05f;
       // if (Input.GetButton("Jump")) m_timeBetweenFalls *= 0.05f;
	}

    void OnGUI()
    {
        GUI.Label(new Rect(50, 50, 100, 50), "Score: " + model.Score);
        GUI.Label(new Rect(200, 50, 100, 50), "Level:  " + model.Level);
    }
}
