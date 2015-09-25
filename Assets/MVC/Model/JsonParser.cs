using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ExtensionMethods;

public static class JsonParser
{
    public static SimpleJSON.JSONNode Shapes { get; set; }
    public static SimpleJSON.JSONNode Colors { get; set; }
    static Random rand = new Random();

    static JsonParser()
    { }

    public static bool LoadResources()
    {
        string _shapesFileContent = System.IO.File.ReadAllText("Assets/DataObjects/shapes.json");
        Shapes = SimpleJSON.JSON.Parse(_shapesFileContent);

        string _coloursFileContent = System.IO.File.ReadAllText("Assets/DataObjects/colors.json");
        Colors = SimpleJSON.JSON.Parse(_coloursFileContent);

        return true;
    }

    public static TetrisShape GetRandomShape()
    {
        int _shapeIndex = rand.Next(Shapes["shapes"].Count);
        int _shapeParticleCount = Shapes["shapes"][_shapeIndex]["coordinates"].Count;

        var _shapeParticles = new List<Point>();
        var _shapeParticle = new Point();

        for (int i = 0; i < _shapeParticleCount; i++)
        {
            _shapeParticle.X = Shapes["shapes"][_shapeIndex]["coordinates"][i][0].AsInt;
            _shapeParticle.Y = Shapes["shapes"][_shapeIndex]["coordinates"][i][1].AsInt;

            _shapeParticles.Add(_shapeParticle);
        }
        return new TetrisShape(_shapeParticles);
    }

    public static int GetRandomColor()
    {
        int _colorIndex = rand.Next(Colors["colors"].Count);
        string _hexColor = Colors["colors"][_colorIndex]["colorCode"];
        //int _hexCode = _hexColor.ToHexInt();
        //UnityEngine.Debug.Log(_colorIndex + " " + _hexColor + " " + _hexCode);
        return 0x00ff00;
    }
}

