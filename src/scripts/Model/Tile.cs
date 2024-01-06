using Godot;

public partial class Tile : RefCounted
{
    public const float stepHeight = 0.3f;

    private Vector3I pos;
    public int height { get { return pos.Y; }}

    public Vector3 center { get { return new Vector3(pos.X, pos.Y * stepHeight, pos.Z); }}

    public Tile(Vector3I _pos)
    {
        pos = _pos;
    }

    public Tile() {}
}
