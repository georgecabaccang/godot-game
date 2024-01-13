using Godot;

public partial class Unit : CharacterBody3D
{
    public Tile tile { get; protected set; }

    public Vector2I front;
    private Movement _movement;
    public Movement movement { get { return _movement; } set { SetMovement(value); } }

    public int moveRange;
    public int jumpRange;

    public void Place(Tile target)
    {
        if (tile != null && tile.content == this)
            tile.content = null;

        tile = target;

        if (target != null)
            target.content = this;
    }

    public void Match()
    {
        Position = tile.center;
    }

    protected void SetMovement(Movement newMovement)
    {
        _movement = newMovement;
        AddChild(_movement);
    }
}
