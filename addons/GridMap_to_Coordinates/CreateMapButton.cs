using System.Collections.Generic;
using System.Linq;
using Godot;

[Tool]
public partial class CreateMapButton : Button {

    public LevelMap mapObject;
    private int minX;
    private int minY;
    private Point offset;
    private Point pos;

    public override void _EnterTree() {

        Pressed += Clicked;
    }

    public void Clicked() {

        Godot.Collections.Array<Vector3I> tiles = mapObject.GetUsedCells();

        if (tiles.Count() == 0) {
            GD.Print("Map is empty");
        } else {
            Create(tiles);
        }
            
    }
    
    public void Create(Godot.Collections.Array<Vector3I> tiles) {

        Dictionary<Point, Vector3I> coordinates = new();
        minX = tiles.Min(v => v.X);
        minY = tiles.Min(v => v.Z);
        offset = new(minX, minY);
        
        foreach (Vector3I tile in tiles) {
            pos.x = tile.X;
            pos.y = tile.Z;
            pos -= offset;
            if (coordinates.ContainsKey(pos)) {
                if (coordinates[pos].Y < tile.Y) {
                    coordinates[pos] = tile;
                }
            }
            else {
                coordinates.Add(pos, tile);
            }
        }

        mapObject.map.coordinates = coordinates;

        foreach (KeyValuePair<Point, Vector3I> item in mapObject.map.coordinates) {
            GD.Print(item.Key + " " + item.Value);
        }
    }           
}
