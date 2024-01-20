using System.Collections.Generic;
using Godot;

public abstract partial class Movement : Node
{
    public Unit unit;
    public AnimationPlayer animator;

    public virtual List<Tile> GetTilesInRange(LevelMap levelMap)
    {
        List<Tile> tiles = levelMap.Search(unit.tile, SearchCriteria);
        Filter(tiles);
        return tiles;
    }

    protected virtual bool SearchCriteria(Tile from, Tile to)
    {
        return (from.distance + 1) <= unit.moveRange;
    }

    protected virtual void Filter(List<Tile> tiles)
    {
        for (int i = tiles.Count - 1; i >= 0; --i)
            if (tiles[i].content != null)
                tiles.RemoveAt(i);
    }

    public abstract Tween AnimateMovement(Tile tile);
}
