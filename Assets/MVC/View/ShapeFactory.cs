using UnityEngine;
using Assets.MVC.Model;

namespace Assets.MVC.View
{
    public static class ShapeFactory
    {
        private static readonly GameObject Block;

        public const float BLOCK_SIZE = 0.5f;

        static ShapeFactory()
        {
            Block = (GameObject)Resources.Load("block");
        }

        public static GameObject CreateShape(TetrisShape modelShape)
        {
            var shape = new GameObject("TetrisShape");

            var hexCode = modelShape.HexColor;
            var color = hexCode.ToColor();

            foreach (var block in modelShape.Blocks)
            {
                float x = block.X;
                float y = block.Y;

                var localVar = Object.Instantiate(Block);
                localVar.transform.parent = shape.transform;
                localVar.transform.position = new Vector2(x * BLOCK_SIZE, y * BLOCK_SIZE);
                localVar.GetComponent<SpriteRenderer>().color = color;
            }
            return shape;
        }
    }
}
