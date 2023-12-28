using System.Linq;
using Godot;

[Tool]
public partial class CreateMapButton : Button {

    public GridMap levelMap;
    private int minX;
    private int minY;
    private Vector2 offset;
    private Vector2 pos;

    public override void _EnterTree() {

        Pressed += Clicked;
    }

    public void Clicked() {

        CreateMap();
    }
    
    public void CreateMap() {

        Godot.Collections.Array<Vector3I> tiles = levelMap.GetUsedCells();

        if (tiles.Count == 0) {
            GD.Print("GridMap is empty. Place some tiles");
            return;
        }

        Godot.Collections.Dictionary<Vector2, Vector3I> coordinates = new();
        minX = tiles.Min(v => v.X);
        minY = tiles.Min(v => v.Z);
        offset = new(minX, minY);
        
        foreach (Vector3I tile in tiles) {
            pos.X = tile.X;
            pos.Y = tile.Z;
            pos -= offset;
            if (coordinates.ContainsKey(pos)) {
                if (coordinates[pos].Y < tile.Y)
                    coordinates[pos] = tile;
            } else {
                coordinates.Add(pos, tile);
            }
        }

        levelMap.Set("coordinates", coordinates);
        GD.Print("Saved");

    }           
}
