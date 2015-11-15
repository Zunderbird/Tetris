using UnityEngine;
using Assets.MVC.Model;

namespace Assets.Scripts
{
    public class PreloaderScript : MonoBehaviour
    {
        void Start()
        {
            JsonReader.LoadResources();          
        }
    }
}