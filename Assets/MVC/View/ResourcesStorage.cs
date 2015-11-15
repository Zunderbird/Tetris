using UnityEngine;

namespace Assets.MVC.View
{
    public static class ResourcesStorage
    {
        public static GameObject Block { get; private set; }
        public static GameObject Board { get; private set; }

        public static void LoadResources()
        {
            Block = (GameObject)Resources.Load("block");
            Board = (GameObject) Resources.Load("Board");
        }
        
    }
}
