using Godot;

public partial class BattleController : StateMachine
{
    public InputController inputController;

    public LevelMap levelMap;
    public Node3D levelMapCursor;

    public CameraSetup cameraSetup;

    public Vector2I pos;
    public Unit currentUnit;
    public Tile currentTile { get {return levelMap.GetTile(pos); }}

    public override void _EnterTree()
    {
        base._EnterTree();
        AddChild(inputController = new(){Name = "InputController"});
    }

    public override void _Ready()
    {
        ChangeState<InitBattleState>();
    }
}
