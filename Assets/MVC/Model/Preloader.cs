using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public static class Preloader
{
    public static SimpleJSON.JSONNode Shapes { get; set; }
    public static SimpleJSON.JSONNode Colours { get; set; }

    static Preloader()
    { }

    public static bool LoadResources()
    {
        string _shapesFileContent = System.IO.File.ReadAllText("Assets/DataObjects/shapes.json");
        Shapes = SimpleJSON.JSON.Parse(_shapesFileContent);

        string _coloursFileContent = System.IO.File.ReadAllText("Assets/DataObjects/colours.json");
        Colours = SimpleJSON.JSON.Parse(_coloursFileContent);

        return true;
    }
}

