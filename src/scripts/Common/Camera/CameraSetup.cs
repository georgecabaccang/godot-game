using Godot;

public partial class CameraSetup : Node3D
{
    public float speed = 3f;
    public Node3D follow;

    public override void _EnterTree()
    {
        follow = GetParent<BattleController>().levelMapCursor;
    }

    public override void _PhysicsProcess(double delta)
    {
        if (!Position.IsEqualApprox(follow.Position))
            Position = Position.Lerp(follow.Position, speed * (float)delta);
    }
}
