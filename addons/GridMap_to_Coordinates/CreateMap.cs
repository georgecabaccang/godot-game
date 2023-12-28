using Godot;

[Tool]
public partial class CreateMap : EditorInspectorPlugin {

    private CreateMapButton button;

    public override bool _CanHandle(GodotObject @object) {

        return @object is GridMap;
    }

    public override void _ParseEnd(GodotObject @object) {

        button = new() {
            Text = "Create/Update Map",
            levelMap = @object as GridMap
        };
        AddCustomControl(button);
    }
}

