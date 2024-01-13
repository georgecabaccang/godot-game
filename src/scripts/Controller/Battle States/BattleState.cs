using Godot;

public abstract partial class BattleState : State
{
    protected BattleController stateMachine;
    public LevelMap levelMap { get { return stateMachine.levelMap; } set { stateMachine.levelMap = value; }}
    public Node3D levelMapCursor { get { return stateMachine.levelMapCursor; } set { stateMachine.levelMapCursor = value; }}
    public CameraSetup cameraSetup { get { return stateMachine.cameraSetup; } set { stateMachine.cameraSetup = value; }}
    public Vector2I pos { get { return stateMachine.pos; } set { stateMachine.pos = value; }}

    public override void _EnterTree()
    {
        stateMachine = GetParent().GetParent<BattleController>();
    }

    protected override void AddListeners()
    {
        stateMachine.inputController.Move += OnMove;
        stateMachine.inputController.Fire += OnFire;
    }

    protected override void RemoveListeners()
    {
        stateMachine.inputController.Move -= OnMove;
        stateMachine.inputController.Fire -= OnFire;
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
