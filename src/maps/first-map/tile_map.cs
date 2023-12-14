using System;
using System.Collections.Generic;
using System.Linq;
using Godot;

public partial class tile_map : TileMap
{
    enum ENTITY_TYPES
    {
        PLAYER = 1,
        OBSTACLE = 2,
        ITEM = 3
    }

    PackedScene obstacle = (PackedScene)
        GD.Load("res://src/for_testing_purposes/obstacles_like_characters_in_a_cell.tscn");

    Vector2 gridSize = new Vector2(16, 16);
    List<int[]> gridArray = new List<int[]>();

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        Random randomInt = new Random();

        int[] dummyArray = { };
        Vector2I tileSize = TileSet.TileSize;
        Vector2I halfTileSize = tileSize / 2;

        for (int i = 0; i < gridSize.X; i++)
        {
            gridArray.Add(dummyArray);

            for (int j = 0; j < gridSize.Y; j++)
            {
                gridArray[i] = null;
            }
        }

        Vector2[] obstaclePositions = new Vector2[5];

        for (int i = 0; i < 5; i++)
        {
            Vector2 obstacleCoord = new Vector2(
                randomInt.Next(0, (int)gridSize.X),
                randomInt.Next(0, (int)gridSize.Y)
            );
            if (!obstaclePositions.Contains(obstacleCoord))
            {
                obstaclePositions[i] = obstacleCoord;
            }
        }

        foreach (var item in gridArray)
        {
            GD.Print(item);
        }

        for (int i = 0; i < obstaclePositions.Length; i++)
        {
            CharacterBody2D newObstacle = (CharacterBody2D)obstacle.Instantiate();
            newObstacle.Position = obstaclePositions[i] + halfTileSize;
            GD.Print(newObstacle.Position);

            // GD.Print(gridArray[(int)obstaclePositions[i].X][(int)obstaclePositions[i].Y]);
            // gridArray[(int)obstaclePositions[i].X][(int)obstaclePositions[i].Y] = (int)
            // 	ENTITY_TYPES.OBSTACLE;
            AddChild(newObstacle);
        }
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta) { }
}
