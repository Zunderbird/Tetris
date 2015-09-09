using System;
using System.Collections.Generic;

public interface ITetrisView  
{
    void DisplayBoard();

    void SpawnShape(TetrisShape i_shape, Point i_spawnCoord); 
    
    void MoveShape(MoveDirection i_moveDirection);

    void RotateShape(TetrisShape i_shape);

    void DestroyLine(int i_numbers);

    void DisplayNextShape(TetrisShape i_shape);

    void UpdateScore();

    void UpdateLevel();

    void DisplayGameOver();

}
