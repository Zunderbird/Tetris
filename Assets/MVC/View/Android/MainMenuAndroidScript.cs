using UnityEngine;

namespace Assets.MVC.View.Android
{
    class MainMenuAndroidScript: MainMenuScript
    {
        protected override void NewGame()
        {
            Application.LoadLevel("Stage_android");
        }
    }
}
