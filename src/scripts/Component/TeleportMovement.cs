using Godot;

public partial class TeleportMovement : Movement
{
    public override void _EnterTree()
    {
        unit = GetParent<Unit>();
    }

    public override void _Ready()
    {
        animator = unit.GetChild<AnimationPlayer>(2);
    }

    public override Tween AnimateMovement(Tile tile)
    {
        animator.Play("Fade_Out_In");

        Tween delay = CreateTween().SetTrans(Tween.TransitionType.Expo).SetEase(Tween.EaseType.InOut);
        delay.TweenProperty(unit, "position", tile.center, 0.8f);

        unit.Place(tile);
        return delay;
    }

    public async void Teleport(Tween delay)
    {
        await ToSignal(delay.TweenProperty(unit, "modulate", 1, 0.4f), "finished");
    }
}
