using Godot;

public partial class MoveSequenceState : BattleState
{
    public override async void Enter()
    {
        base.Enter();
        await ToSignal(stateMachine.currentUnit.movement.AnimateMovement(stateMachine.currentTile), "finished");
        stateMachine.currentUnit.movement.animator.Stop();
        stateMachine.ChangeState<SelectUnitState>();
    }
}
