using Godot;

public abstract partial class BattleState : State
{
    protected BattleController controller;
    public LevelMap levelMap { get { return controller.levelMap; } set { controller.levelMap = value; }}
    public Node3D levelMapCursor { get { return controller.levelMapCursor; } set { controller.levelMapCursor = value; }}
    public CameraSetup cameraSetup { get { return controller.cameraSetup; } set { controller.cameraSetup = value; }}
    public Vector2I pos { get { return controller.pos; } set { controller.pos = value; }}

    public override void _EnterTree()
    {
        controller = GetParent().GetParent<BattleController>();
    }

    protected override void AddListeners()
    {
        controller.inputController.Move += OnMove;
        controller.inputController.Fire += OnFire;
    }

    protected override void RemoveListeners()
    {
        controller.inputController.Move -= OnMove;
        controller.inputController.Fire -= OnFire;
    }

    protected virtual void OnMove(Vector2I direction)
    {

    }

    protected virtual void OnFire(int type)
    {
        
    }

    protected virtual void SelectTile(Vector2I p)
    {
        if (pos == p || !levelMap.map.ContainsKey(p))
            return;

        pos = p;
        levelMapCursor.Position = levelMap.map[p].center;
    }
}
