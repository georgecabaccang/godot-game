using Godot;

[Tool]
public partial class CreateMap : EditorInspectorPlugin {

    private CreateMapButton button;

    public override bool _CanHandle(GodotObject @object) {

        return @object is LevelMap;
    }

    public override void _ParseEnd(GodotObject @object) {

        button = GD.Load<PackedScene>("res://addons/GridMap_to_Coordinates/CreateMapButton.tscn").Instantiate<CreateMapButton>();
        button.mapObject = @object as LevelMap;
        AddCustomControl(button);
    }
}

