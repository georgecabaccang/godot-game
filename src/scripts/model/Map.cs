using Godot;
using System.Collections.Generic;

[GlobalClass]
public partial class Map : Resource {

    public Dictionary<Point, Vector3I> coordinates;
    [Export]public string filePath;
}

