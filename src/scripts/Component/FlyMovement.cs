using System.Collections.Generic;
using Godot;

public partial class FlyMovement : Movement
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

    public override Tween AnimateMovement(Tile tile)
    {
        animator.Stop();

        Tween move = CreateTween();
        Tween jump = CreateTween().SetTrans(Tween.TransitionType.Quad).SetEase(Tween.EaseType.Out);

        Vector2I direction;
        Tile current;
        Tile from = unit.tile;
        Tile highestTile = unit.tile;
        Stack<Tile> path = new();

        while (tile != unit.tile)
        {
            path.Push(tile);

            if (tile.height > tile.previous.height)
                highestTile = tile;

            tile = tile.previous;
        }

        if (path.Count == 1 && from.height != path.Peek().height)
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

            jump.TweenProperty(unit, "position:y", highestTile.localHeight + 0.5f, 0.2f);
            jump.SetEase(Tween.EaseType.In).SetTrans(Tween.TransitionType.Sine);
            jump.Chain().TweenProperty(unit, "position:y", current.localHeight, 0.2f);
            MoveToTile(current, move);

            unit.Place(current);
            return move;
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
            
            if (path.Count != 0)
            {
                Jump(highestTile, jump);
                MoveToTile(current, move);
            }
            else
            {
                jump.SetEase(Tween.EaseType.In);
                jump.Chain().TweenProperty(unit, "position:y", current.localHeight, 0.4f);
                MoveToTile(current, move);
            }

            from = current;
        }

        unit.Place(from);
        return move;
    }

    private void MoveToTile(Tile tile, Tween tween)
    {
        tween.Chain().TweenProperty(unit, "position", tile.center, 0.4f);
    }

    private void Jump(Tile tile, Tween tween)
    {
        tween.Chain().TweenProperty(unit, "position:y", tile.localHeight + 0.8f, 0.4f);
    }
}
