using Godot;

public partial class WalkMovement : Movement
{
    public override void _EnterTree()
    {
        unit = GetParent<Unit>();
    }

    protected override bool SearchCriteria(Tile from, Tile to)
    {
        if (Mathf.Abs(from.height - to.height) > unit.jumpRange)
            return false;
        
        if (to.content != null)
            return false;

        return base.SearchCriteria(from, to);
    }

    public override void AnimateMovement(Tile tile)
    {
        unit.Place(tile);
        unit.Match();
    }
}
