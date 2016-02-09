using System;
using UnityEngine;


public static class TouchControls
{
    public static bool IsInRotationPhase;

    public static bool DoubleTouch()
    {
        return Input.touchCount == 2;
    }

    public static bool LeftShift()
    {
        if (Input.touchCount != 1) return false;

        var touch = Input.GetTouch(0);
        return touch.deltaPosition.x < 0 
            && Math.Abs(touch.deltaPosition.x) > Math.Abs(touch.deltaPosition.y);
    }

    public static bool RightShift()
    {
        if (Input.touchCount != 1) return false;

        var touch = Input.GetTouch(0);
        return touch.deltaPosition.x > 0
            && Math.Abs(touch.deltaPosition.x) > Math.Abs(touch.deltaPosition.y);
    }

    public static bool Rotate()
    {
        if (Input.touchCount != 1) return false;

        var touch = Input.GetTouch(0);

        if (touch.phase == TouchPhase.Began) IsInRotationPhase = true;
        if (touch.phase == TouchPhase.Ended) IsInRotationPhase = false;

        if (touch.deltaPosition.y > 0
            && Math.Abs(touch.deltaPosition.x) < Math.Abs(touch.deltaPosition.y)
            && IsInRotationPhase)
        {
            IsInRotationPhase = false;
            return true;
        }
        return false;
    }

    public static bool DownwardShift()
    {
        if (Input.touchCount != 1) return false;

        var touch = Input.GetTouch(0);
        return Input.touchCount == 1 
            && touch.deltaPosition.y < 0
            && Math.Abs(touch.deltaPosition.x) < Math.Abs(touch.deltaPosition.y);
    }
}
