using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class PreloaderScript : MonoBehaviour 
{
	IEnumerator Start () 
    {     
        JsonParser.LoadResources();

        yield return new WaitForSeconds (2);

        Application.LoadLevel("Stage_01");
	}
}
