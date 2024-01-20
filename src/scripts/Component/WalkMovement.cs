using System.Collections.Generic;
using Godot;

public partial class WalkMovement : Movement
{
    public override void _EnterTree()
    {
        unit = GetParent<Unit>();
    }

    public override void _Ready()
    {
        animator = unit.GetChild<AnimationPlayer>(2);
        animator.SpeedScale = 2;
    }

    protected override bool SearchCriteria(Tile from, Tile to)
    {
        if (Mathf.Abs(from.height - to.height) > unit.jumpRange)
            return false;
        
        if (to.content != null)
            return false;

        return base.SearchCriteria(from, to);
    }

    public override Tween AnimateMovement(Tile tile)
    {
        animator.Stop();

        Tween move = CreateTween();
        Tween jump = CreateTween().SetTrans(Tween.TransitionType.Sine);

        Tile current;
        Tile from = unit.tile;
        Vector2I direction;
        Stack<Tile> path = new();

        while (tile != unit.tile)
        {
            path.Push(tile);
            tile = tile.previous;
        }

        while (path.Count != 0)
        {
            current = path.Pop();
            direction = from.pos - current.pos;
            
            if (direction == Vector2I.Down)
                animator.Queue("Walk_Back_Right");
            else if (direction == Vector2I.Left)
                animator.Queue("Walk_Front_Right");
            else if (direction == Vector2I.Up)
                animator.Queue("Walk_Front_Left");
            else if (direction == Vector2I.Right)
                animator.Queue("Walk_Back_Left");
            
            Jump(from, current, jump);
            MoveToTile(current, move);

            from = current;
        }

        unit.Place(from);
        return move;
    }

    private void MoveToTile(Tile tile, Tween tween)
    {
        tween.Chain().TweenProperty(unit, "position", tile.center, 0.4f);
    }

    private void Jump(Tile from, Tile tile, Tween tween)
    {
        if (from.height < tile.height)
        {
            tween.SetEase(Tween.EaseType.Out);
            tween.Chain().TweenProperty(unit, "position:y", tile.localHeight + 0.3f, 0.25f);
            tween.SetEase(Tween.EaseType.In);
            tween.Chain().TweenProperty(unit, "position:y", tile.localHeight, 0.15f);
        }
        else if (from.height > tile.height)
        {
            tween.SetEase(Tween.EaseType.Out);
            tween.Chain().TweenProperty(unit, "position:y", from.localHeight + 0.2f, 0.2f);
            tween.SetEase(Tween.EaseType.In);
            tween.Chain().TweenProperty(unit, "position:y", tile.localHeight, 0.2f);
        }
        else
            tween.TweenInterval(0.4f);
    }
}
