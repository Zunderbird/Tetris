using Assets.MVC.Model;

namespace Assets.MVC.View
{
    public interface ITetrisView
    {
        void DisplayBoard();

        void SpawnShape(TetrisShape shape, Point spawnCoord);

        void MoveShape(MoveDirection moveDirection);

        void RotateShape(TetrisShape shape);

        void DestroyLine(int numbers);

        void DisplayNextShape(TetrisShape shape);

        void DisplayGameOver();

    }
}

