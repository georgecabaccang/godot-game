using Godot;

public partial class InitBattleState : BattleState
{
    public override void _EnterTree()
    {
        base._EnterTree();
        InitBattle();
    }

    private async void InitBattle()
    {
        levelMap = ResourceLoader.Load<PackedScene>("res://src/scenes/Levels/TestLevel.tscn").Instantiate<LevelMap>();
        stateMachine.AddChild(levelMap);

        levelMapCursor = ResourceLoader.Load<PackedScene>("res://src/scenes/Cursors/LevelMapCursor.tscn").Instantiate<Node3D>();
        stateMachine.AddChild(levelMapCursor);

        cameraSetup = ResourceLoader.Load<PackedScene>("res://src/scenes/Camera/CameraSetup.tscn").Instantiate<CameraSetup>();
        stateMachine.AddChild(cameraSetup);

        Unit sample = ResourceLoader.Load<PackedScene>("res://src/scenes/Units/Unit.tscn").Instantiate<Unit>();
        stateMachine.AddChild(sample);
        sample.movement = new WalkMovement();
        sample.moveRange = 3;
        sample.jumpRange = 1;
        sample.Place(levelMap.GetTile(levelMap.startingPos));
        sample.Match();
        
        SelectTile(levelMap.startingPos);
        GD.Print("Starting position is " + levelMap.startingPos);

        await ToSignal(GetTree(), SceneTree.SignalName.ProcessFrame);

        stateMachine.ChangeState<SelectUnitState>();
    }
}
