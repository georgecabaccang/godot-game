using Godot;

public partial class TestState : BattleState
{
    private bool stopping;

    protected override void OnMove(Vector2I direction)
    {
        SelectTile(pos + direction);
    }

    protected override void OnFire(int type)
    {
        if (type == 1)
            levelMapCursor.GetNode<AnimatedSprite3D>("CursorAnimationSprite").Play();
        else if (levelMapCursor.GetNode<AnimatedSprite3D>("CursorAnimationSprite").IsPlaying())
            Stop();
    }

    private async void Stop()
    {
        if (stopping)
            return;

        stopping = true;

        await ToSignal(levelMapCursor.GetNode<AnimatedSprite3D>("CursorAnimationSprite"), "animation_looped");

        levelMapCursor.GetNode<AnimatedSprite3D>("CursorAnimationSprite").Stop();
        stopping = false;
    }
}
