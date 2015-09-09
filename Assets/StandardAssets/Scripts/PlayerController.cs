using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour 
{
    Controller controller;

    private float m_timeBetweenFalls = 0.3f;

    private float m_verticalTimeCounter = 0f;
    private float m_horizontalTimeCounter = 0f;

	void Start () 
    {
        TetrisView _view = new TetrisView();

        controller = new Controller(_view);
	}

	void Update () 
    {
        if (m_horizontalTimeCounter < 0)
        {
            m_horizontalTimeCounter = m_timeBetweenFalls * 0.1f;
            if (Input.GetButton("Right")) controller.MoveTrigger(MoveDirection.Right);
            if (Input.GetButton("Left")) controller.MoveTrigger(MoveDirection.Left);
        }
        m_horizontalTimeCounter -= Time.deltaTime * 0.5f;

        if (Input.GetButtonDown("Fire1")) controller.RotateTrigger(RotateDirection.CounterClockWise);
        if (Input.GetButtonDown("Fire2")) controller.RotateTrigger(RotateDirection.ClockWise);

        if (Input.GetButtonDown("Vertical")) controller.MoveTrigger(MoveDirection.Down);
        
        if (Input.GetButton("Vertical")) m_timeBetweenFalls *= 0.05f;

        if (m_verticalTimeCounter < 0)
        {
            m_verticalTimeCounter = m_timeBetweenFalls;
            controller.MoveTrigger(MoveDirection.Down);
        }
        m_verticalTimeCounter -= Time.deltaTime * 0.5f;
        m_timeBetweenFalls = 0.3f;
	
	}
}
