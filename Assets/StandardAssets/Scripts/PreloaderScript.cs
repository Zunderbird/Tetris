using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class PreloaderScript : MonoBehaviour 
{
    
	IEnumerator Start () 
    {
        Preloader.LoadResources();

        yield return new WaitForSeconds (2);

        Application.LoadLevel("Stage_01");
        
        Debug.Log("Level is loaded but i'm still alive!");
	}

}
