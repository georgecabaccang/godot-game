using Godot;

public partial class Tile : RefCounted
{
    public const float stepHeight = 0.3f;

    private Vector3I localPos;
    public Vector2I pos;
    public int height { get { return localPos.Y; }}

    public Vector3 center { get { return new Vector3(localPos.X, localPos.Y * stepHeight, localPos.Z); }}
    
    public Node3D content;

    //Search properties
    public Tile previous;
    public int distance;

    public Tile(Vector3I _localPos)
    {
        localPos = _localPos;
    }

    public Tile() {}
}
