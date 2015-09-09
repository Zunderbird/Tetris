using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class PreloaderScript : MonoBehaviour 
{
    private SimpleJSON.JSONNode m_shapes;
    private SimpleJSON.JSONNode m_colours;
    
	IEnumerator Start () 
    {
        LoadFiles();

        yield return new WaitForSeconds (2);

        Application.LoadLevel("Stage_01");
        
        Debug.Log("Level is loaded but i'm still alive!");
	}

    private bool LoadFiles()
    {
        string _shapesFileContent = System.IO.File.ReadAllText("Assets/StandardAssets/DataObjects/shapes.json");
        m_shapes = SimpleJSON.JSON.Parse(_shapesFileContent);

        string _coloursFileContent = System.IO.File.ReadAllText("Assets/StandardAssets/DataObjects/colours.json");
        m_colours = SimpleJSON.JSON.Parse(_coloursFileContent);

        return true;
    }

    public SimpleJSON.JSONNode Shapes
    {
        get
        {
            return m_shapes;
        }
    }

    public SimpleJSON.JSONNode Colours
    {
        get
        {
            return m_colours;
        }
    }


}
