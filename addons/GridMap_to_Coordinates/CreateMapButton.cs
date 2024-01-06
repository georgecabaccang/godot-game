using System.Linq;
using Godot;

[Tool]
public partial class CreateMapButton : Button
{

    public GridMap levelMap;

    public override void _EnterTree()
    {
        Pressed += Clicked;
    }

    public void Clicked()
    {
        CreateMap();
    }
    
    public void CreateMap()
    {
        Godot.Collections.Array<Vector3I> tiles = levelMap.GetUsedCells();

        //Check if gridmap is empty
        if (tiles.Count == 0)
        {
            GD.PushWarning("GridMap is empty. Place some tiles");
            return;
        }

        int minX = tiles.Min(v => v.X);
        int minY = tiles.Min(v => v.Z);
        Vector2I offset = new(minX, minY);
        Vector2I pos;

        Godot.Collections.Dictionary<Vector2I, Vector3I> coordinates = new();

        //Add tiles to coordinates dictionary
        foreach (Vector3I tile in tiles)
        {
            pos.X = tile.X;
            pos.Y = tile.Z;
            pos -= offset;

            if (coordinates.ContainsKey(pos))
            {
                if (coordinates[pos].Y < tile.Y)
                    coordinates[pos] = tile;
            }
            else
                coordinates.Add(pos, tile);
        }

        //Check if coordinates is empty
        if (coordinates == null || coordinates.Count == 0)
        {
            GD.Print("Something went wrong!");
            return;
        }

        //Check if starting position is out of bounds 
        pos = (Vector2I)levelMap.Get("startingPos");

        if (!coordinates.ContainsKey(pos))
        {
            GD.PushError("Starting position is out of bounds\nMap not saved");
            return;
        }

        levelMap.Set("coordinates", coordinates);
        GD.Print("Map coordinates saved");

    }           
}
