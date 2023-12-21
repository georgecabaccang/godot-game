using Godot;

[Tool]
public partial class LevelMap : GridMap {

    [Export]public Map map = new();

    public override void _Ready(){
        // GD.Print(GetClass());
    }
}
