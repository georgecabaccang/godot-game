using Godot;

public partial class LevelMap : GridMap
{
    [Export]public Godot.Collections.Dictionary<Vector2I, Vector3I> coordinates;
    [Export]public Vector2I startingPos;

    public Godot.Collections.Dictionary<Vector2I, Tile> map;

    public override void _EnterTree()
    {   
        if (coordinates != null)
            CoordinatesToMap();
        else
            GD.PushWarning("Map is empty! Don't forget to save after editing.");
        
    }

    private void CoordinatesToMap()
    {
        map = new Godot.Collections.Dictionary<Vector2I, Tile>();

        foreach (var tile in coordinates)
            map.Add(tile.Key, new Tile(tile.Value));

        GD.Print("Map coordinates ready");
    }

}
