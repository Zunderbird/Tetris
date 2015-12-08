using System;
using UnityEngine;

namespace Assets.MVC.View
{
    public static class TouchControls
    {
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

        public static bool UpwardShipt()
        {
            if (Input.touchCount != 1) return false;

            var touch = Input.GetTouch(0);
            return Input.touchCount == 1
                && touch.deltaPosition.y > 0
                && Math.Abs(touch.deltaPosition.x) < Math.Abs(touch.deltaPosition.y);
        }

        public static bool DownwardShift()
        {
            return Input.touchCount == 1 
                && Input.GetTouch(0).deltaPosition.y < 0
                && Math.Abs(Input.GetTouch(0).deltaPosition.x) < Math.Abs(Input.GetTouch(0).deltaPosition.y);
        }
    }
}
