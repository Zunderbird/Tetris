using System;
using System.Collections.Generic;

namespace Assets.MVC.Model
{
    public static class JsonReader
    {
        public static SimpleJSON.JSONNode Shapes { get; set; }
        public static SimpleJSON.JSONNode Colors { get; set; }
        private static readonly Random Rand = new Random();

        public static bool LoadResources()
        {
            var shapes = UnityEngine.Resources.Load("DataObjects/shapes");
            Shapes = SimpleJSON.JSON.Parse(shapes.ToString());

            var colors = UnityEngine.Resources.Load("DataObjects/colors");
            Colors = SimpleJSON.JSON.Parse(colors.ToString());

            return true;
        }

        public static List<TetrisShape> GetShapes(string type)
        {
            var shapesCount = Shapes["shapes"].Count;
            var shapes = new List<TetrisShape>();

            for (var i = 0; i < shapesCount; i++)
            {
                if (type.Equals(Shapes["shapes"][i]["type"]))
                    shapes.Add(GetShape(i));
            }
            return shapes;
        }

        public static List<int> GetColors()
        {
            var colorCount = Colors["colors"].Count;
            var colors = new List<int>();

            for (var i = 0; i < colorCount; i++)
            {
                colors.Add(GetColor(i));
            }
            return colors;
        }

        public static TetrisShape GetShape(int shapeIndex)
        {
            var shapeParticleCount = Shapes["shapes"][shapeIndex]["coordinates"].Count;

            var shapeParticles = new List<Point>();
            var shapeParticle = new Point();

            for (var i = 0; i < shapeParticleCount; i++)
            {
                shapeParticle.X = Shapes["shapes"][shapeIndex]["coordinates"][i][0].AsInt;
                shapeParticle.Y = Shapes["shapes"][shapeIndex]["coordinates"][i][1].AsInt;

                shapeParticles.Add(shapeParticle);
            }
            return new TetrisShape(shapeParticles);
        }

        public static TetrisShape GetRandomShape()
        {
            var shapeIndex = Rand.Next(Shapes["shapes"].Count);
            return GetShape(shapeIndex);
        }

        public static TetrisShape GetRandomClassicShape()
        {
            var shapeIndex = Rand.Next(GetTypesCount("classic"));
            return GetShape(shapeIndex);
        }

        public static int GetColor(int colorIndex)
        {
            string hexColor = Colors["colors"][colorIndex]["colorCode"];
            return Convert.ToInt32(hexColor, 16);
        }

        public static int GetRandomColor()
        {
            var colorIndex = Rand.Next(Colors["colors"].Count);
            return GetColor(colorIndex);
        }

        public static int GetRandomColorForLevel(int level)
        {
            var colorCount = Colors["colors"].Count;

            if (level <= colorCount)
                colorCount = level;

            var colorIndex = Rand.Next(colorCount);

            return GetColor(colorIndex);
        }

        public static int GetTypesCount(string type)
        {
            var typesCount = 0;
            var shapesCount = Shapes["shapes"].Count;

            for (var i = 0; i < shapesCount; i++)
            {
                if (type.Equals(Shapes["shapes"][i]["type"]))
                    typesCount++;
            }

            return typesCount;
        }
    }
}

