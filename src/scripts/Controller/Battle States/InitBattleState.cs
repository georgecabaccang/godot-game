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
        sample.moveRange = 4;
        sample.jumpRange = 1;
        sample.Place(levelMap.GetTile(levelMap.startingPos));
        sample.Match();

        Unit sample2 = ResourceLoader.Load<PackedScene>("res://src/scenes/Units/Unit.tscn").Instantiate<Unit>();
        stateMachine.AddChild(sample2);
        sample2.movement = new FlyMovement();
        sample2.moveRange = 6;
        sample2.Place(levelMap.GetTile(levelMap.startingPos + Vector2I.Up));
        sample2.Match();
        
        Unit sample3 = ResourceLoader.Load<PackedScene>("res://src/scenes/Units/Unit.tscn").Instantiate<Unit>();
        stateMachine.AddChild(sample3);
        sample3.movement = new TeleportMovement();
        sample3.moveRange = 5;
        sample3.Place(levelMap.GetTile(levelMap.startingPos + Vector2I.Down));
        sample3.Match();

        SelectTile(levelMap.startingPos);
        GD.Print("Starting position is " + levelMap.startingPos);

        await ToSignal(GetTree(), SceneTree.SignalName.ProcessFrame);

        stateMachine.ChangeState<SelectUnitState>();
    }
}
