using System.Collections.Generic;
using UnityEngine;

namespace Assets.MVC.View
{
    static class BoardRenderer
    {
        private static readonly GameObject Block;
        public const float BLOCK_SIZE = 0.5f;

        static BoardRenderer()
        {
            Block = (GameObject)Resources.Load("block");
        }

        public static List<GameObject> CreateBoard(int height, int width, Transform player)
        {
            var board = new GameObject("Board");
            var frame = new GameObject("Frame");
            frame.transform.SetParent(board.transform);

            var middleX = ((float)width + 1) / 2;

            var lines = new List<GameObject>();

            for (var i = 0; i < height; i++)
            {               
                var y = i - (float) height/2 + 1;
                //Left side
                CreateBlock(-middleX, y, frame.transform);
                //Right side
                CreateBlock(middleX, y, frame.transform);

                lines.Add(new GameObject("Line " + i));
                lines[i].transform.SetParent(board.transform, false);
                lines[i].transform.position += lines[i].transform.up * (i - (float)height / 2 + 1) * BLOCK_SIZE;
            }

            var bottomY = -(float)height / 2;

            for (var i = 0; i < width + 2; i++)
            {
                var x = (float) (width + 1)/2 - i;
                //Bottom side
                CreateBlock(x, bottomY, frame.transform);
            }
            board.transform.SetParent(player, false);
            return lines;
        }

        private static void CreateBlock(float x, float y, Transform parent)
        {
            var block = Object.Instantiate(Block);
            block.transform.parent = parent;
            block.transform.position += new Vector3(x, y) * BLOCK_SIZE;
        }
    }
}
