using UnityEngine;
using System.Collections;
using System;

public class Controller
{
    TetrisModel Model { get; set; }

    public Controller(TetrisModel i_model)
    {
        Model = i_model;
    }

    public void RotateTrigger (RotateDirection i_rotateDirection)
    {
        Model.RotateShape(i_rotateDirection);
    }

    public void MoveTrigger(MoveDirection i_direction)
    {
        Model.MoveShape(i_direction);
    }

}
