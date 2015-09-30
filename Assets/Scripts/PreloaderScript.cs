using UnityEngine;
using System.Collections;
using Assets.MVC.Model;

namespace Assets.Scripts
{
    public class PreloaderScript : MonoBehaviour
    {

        IEnumerator Start()
        {
            JsonReader.LoadResources();

            yield return new WaitForSeconds(2);

            Application.LoadLevel("Stage_01");
        }
    }

}