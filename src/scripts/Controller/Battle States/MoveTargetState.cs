using System.Collections.Generic;
using Godot;

public partial class MoveTargetState : BattleState
{
    List<Tile> tiles;

    public override void Enter()
    {
        base.Enter();
        Movement movement = stateMachine.currentUnit.movement;
        tiles = movement.GetTilesInRange(levelMap);
        levelMap.HighlightTiles(tiles);
    }

    public override void Exit()
    {
        base.Exit();
        levelMap.DeHighlightTiles();
        tiles = null;
    }

    protected override void OnMove(Vector2I direction)
    {
        SelectTile(pos + direction);
    }

    protected override void OnFire(int type)
    {
        if (tiles.Contains(stateMachine.currentTile))
            stateMachine.ChangeState<MoveSequenceState>();
    }
}
