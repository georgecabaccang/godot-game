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
        controller.AddChild(levelMap);

        levelMapCursor = ResourceLoader.Load<PackedScene>("res://src/scenes/Cursors/LevelMapCursor.tscn").Instantiate<Node3D>();
        controller.AddChild(levelMapCursor);

        cameraSetup = ResourceLoader.Load<PackedScene>("res://src/scenes/Camera/CameraSetup.tscn").Instantiate<CameraSetup>();
        controller.AddChild(cameraSetup);
        
        SelectTile(levelMap.startingPos);
        GD.Print("Starting position is " + levelMap.startingPos);

        await ToSignal(GetTree(), SceneTree.SignalName.ProcessFrame);
        controller.ChangeState<TestState>();
    }
}
