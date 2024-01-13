using Godot;

public partial class SelectUnitState : BattleState
{
    protected override void OnMove(Vector2I direction)
    {
        SelectTile(pos + direction);
    }

    protected override void OnFire(int type)
    {
        Node content = stateMachine.currentTile.content;

        if (content != null)
        {
            stateMachine.currentUnit = (Unit)content;
            stateMachine.ChangeState<MoveTargetState>();
        }
        else
            GD.Print("Tile selected is empty");
    }
}
