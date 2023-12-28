using Godot;

public partial class LevelMap : GridMap {

    [Export]public Godot.Collections.Dictionary<Vector2, Vector3I> coordinates;

}
