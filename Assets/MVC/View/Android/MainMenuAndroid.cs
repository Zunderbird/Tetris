using Assets.MVC.View.Scripts;
using UnityEngine;

namespace Assets.MVC.View.Android
{
    class MainMenuAndroid: MainMenuScript
    {
        protected override void NewGame()
        {
            Application.LoadLevel("Stage_android");
        }
    }
}
