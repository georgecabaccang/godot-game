#if TOOLS
using Godot;

[Tool]
public partial class GridMaptoCoordinates : EditorPlugin {
	
	private CreateMap plugin;

	public override void _EnterTree() {
		// Initialization of the plugin goes here.
		plugin = new CreateMap();
		AddInspectorPlugin(plugin);

		Script script = GD.Load<Script>("res://addons/GridMap_to_Coordinates/CreateMapButton.cs");
		Texture2D texture = GD.Load<Texture2D>("res://icon.svg");

		AddCustomType("CreateMapButton", "Button", script, texture);

		script = GD.Load<Script>("res://addons/GridMap_to_Coordinates/LevelMap.cs");

		AddCustomType("LevelMap", "GridMap", script, texture);
	}

	public override void _ExitTree() {
		// Clean-up of the plugin goes here.
		RemoveInspectorPlugin(plugin);
		RemoveCustomType("CreateMapButton");
		RemoveCustomType("LevelMap");
	}
}
#endif
