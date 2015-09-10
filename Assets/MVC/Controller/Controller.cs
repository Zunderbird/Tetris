using UnityEngine;
using System.Collections;
using System;

public class Controller
{
    TetrisModel Model { get; set; }

    ITetrisView TetrisView { get; set; }

    public Controller(ITetrisView i_iTetrisView)
    {
        string _shapesFileContent = System.IO.File.ReadAllText("Assets/StandardAssets/DataObjects/shapes.json");
        var _shapes = SimpleJSON.JSON.Parse(_shapesFileContent);

        string _coloursFileContent = System.IO.File.ReadAllText("Assets/StandardAssets/DataObjects/colours.json");
        var _colours = SimpleJSON.JSON.Parse(_coloursFileContent);

        TetrisView = i_iTetrisView;
        Model = new TetrisModel(_shapes, _colours);

        Model.ShapesMoveIsFinished += OnShapesMoveIsFinished;
        Model.LineIsCollected += OnLineIsCollected;
        Model.GameOver += OnGameOver;

        TetrisView.DisplayBoard();
        TetrisView.UpdateScore();
        TetrisView.UpdateLevel();
        TetrisView.DisplayNextShape(Model.NextShape);
        TetrisView.SpawnShape(Model.CurrentShape, Model.CurrentShapeCoord);
    }

    public void RotateTrigger (RotateDirection i_rotateDirection)
    {
        if (Model.RotateShape(i_rotateDirection)) 
            TetrisView.RotateShape(Model.CurrentShape); 
    }

    public void MoveTrigger(MoveDirection i_direction)
    {
        if (Model.MoveShape(i_direction))
            TetrisView.MoveShape(i_direction);
    }

    private void OnShapesMoveIsFinished(object sender, EventArgs e)
    {
        UnityEngine.Debug.Log("Shape Move is finished");
        TetrisView.DisplayNextShape(Model.NextShape);
        TetrisView.SpawnShape(Model.CurrentShape, Model.CurrentShapeCoord);
    }

    private void OnLineIsCollected(object sender, EventArgs e)
    {
        UnityEngine.Debug.Log("Collected!");
        TetrisView.DestroyLine(Model.CollectedLine[Model.CollectedLine.Count - 1]);
    }

    private void OnGameOver(object sender, EventArgs e)
    {
        UnityEngine.Debug.Log("Game Over!");
    }


}
