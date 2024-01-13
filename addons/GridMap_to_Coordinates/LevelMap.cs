using System;
using System.Collections.Generic;
using Godot;

public partial class LevelMap : GridMap
{
    [Export]public Godot.Collections.Dictionary<Vector2I, Vector3I> coordinates;
    [Export]public Vector2I startingPos;

    public Godot.Collections.Dictionary<Vector2I, Tile> map;

    private const string tileSelectionPath = "res://src/scenes/Cursors/TileSelection.tscn";

    private Vector2I[] directions = new Vector2I[4]
    {
        Vector2I.Up,
        Vector2I.Right,
        Vector2I.Down,
        Vector2I.Left
    };

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
        {
            map.Add(tile.Key, new Tile(tile.Value));
            map[tile.Key].pos = tile.Key;
        }

        GD.Print("Map coordinates ready");
    }

    public List<Tile> Search(Tile startTile, Func<Tile, Tile, bool> criteria)
    {
        List<Tile> tilesInRange = new() {startTile};

        ResetSearch();
        startTile.distance = 0;

        Queue<Tile> checking = new();
        Queue<Tile> toBeChecked = new();
        checking.Enqueue(startTile);

        while (checking.Count > 0)
        {
            Tile current = checking.Dequeue();

            for (int i = 0; i < 4; ++i)
            {
                Tile adjacent = GetTile(current.pos + directions[i]);

                if (adjacent == null || adjacent.distance <= current.distance + 1)
                    continue;

                if (criteria(current, adjacent))
                {
                    adjacent.distance = current.distance + 1;
                    adjacent.previous = current;
                    toBeChecked.Enqueue(adjacent);
                    tilesInRange.Add(adjacent);
                }
            }

            if (checking.Count == 0)
                SwapReference(ref checking, ref toBeChecked);
        }

        return tilesInRange;
    }

    void ResetSearch()
    {
        foreach (Tile tile in map.Values)
        {
            tile.previous = null;
            tile.distance = int.MaxValue;
        }
    }

    void SwapReference(ref Queue<Tile> a, ref Queue<Tile> b)
    {
        Queue<Tile> temp = a;
        a = b;
        b = temp;
    }

    public Tile GetTile(Vector2I pos)
    {
        return map.ContainsKey(pos) ? map[pos] : null;
    }

    public void HighlightTiles(List<Tile> tiles)
    {
        Node3D selections = new() {Name = "Selections", Position = new Vector3( 0, 0.31f, 0 )};
        GetParent().AddChild(selections);

        AnimatedSprite3D selection;

        for (int i = tiles.Count - 1; i >= 0; --i)
        {
            selection = ResourceLoader.Load<PackedScene>(tileSelectionPath).Instantiate<AnimatedSprite3D>();
            selections.AddChild(selection);
            selection.Position = tiles[i].center;
        }
    }

    public void DeHighlightTiles()
    {
        GetParent().GetNode("Selections").QueueFree();
    }
}
