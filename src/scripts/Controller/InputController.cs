using Godot;

public partial class InputController : Node
{
    private Timer thresholdTimer = new(){WaitTime = 0.7, OneShot = true};
    private Timer repeatDelay = new(){WaitTime = 0.16};

    private Vector2I direction;
    private Vector2I oldDirection;

    [Signal] public delegate void MoveEventHandler(Vector2I direction);
    [Signal] public delegate void FireEventHandler(int type);

    public override void _EnterTree()
    {
        AddChild(thresholdTimer);
        AddChild(repeatDelay);

        thresholdTimer.OneShot = true;
        thresholdTimer.Timeout += StartMoveRepeat;
        repeatDelay.Timeout += MoveSignal;

        direction = Vector2I.Zero;
    }

    public override void _ExitTree()
    {
        thresholdTimer.Timeout -= StartMoveRepeat;
        repeatDelay.Timeout -= MoveSignal;
    }

    public override void _Notification(int what)
    {
        if (what == MainLoop.NotificationApplicationFocusOut)
        {
            thresholdTimer.Stop();
            repeatDelay.Stop();
        }
    }

    public override void _Process(double delta)
    {
        if(!IsMoving())
            if (!repeatDelay.IsStopped() || !thresholdTimer.IsStopped())
                StopMoving();
    }

    public override void _Input(InputEvent @event)
    {
        // Move conditionals
        if (@event.IsActionPressed("right"))
        {
            direction += Vector2I.Right;
            NewMoveInput(Vector2I.Right);
        }
        if (@event.IsActionPressed("left"))
        {
            direction += Vector2I.Left;
            NewMoveInput(Vector2I.Left);
        }
        if (@event.IsActionPressed("up"))
        {
            direction += Vector2I.Up;
            NewMoveInput(Vector2I.Up);
        }
        if (@event.IsActionPressed("down"))
        {
            direction += Vector2I.Down;
            NewMoveInput(Vector2I.Down);
        }

        if (@event.IsActionReleased("right") && !Input.IsActionPressed("right"))
            direction -= Vector2I.Right;
        if (@event.IsActionReleased("left") && !Input.IsActionPressed("left"))
            direction -= Vector2I.Left;
        if (@event.IsActionReleased("up") && !Input.IsActionPressed("up"))
            direction -= Vector2I.Up;
        if (@event.IsActionReleased("down") && !Input.IsActionPressed("down"))
            direction -= Vector2I.Down;

        // Other input conditionals
        if (@event.IsActionPressed("confirm"))
        {
            EmitSignal(SignalName.Fire, 1);
        }
        if (@event.IsActionPressed("cancel"))
        {
            EmitSignal(SignalName.Fire, 0);
        }
        // if (@event.IsActionPressed("misc"))
        // {

        // }
    }

    //Repeater function methods
    private void NewMoveInput(Vector2I _direction)
    {
        if (direction == Vector2I.Zero && !IsFirstMoveInput())
        {
            repeatDelay.Start();
            MoveSignal();
        }
        else if (IsFirstMoveInput())
        {
            repeatDelay.Stop();
            thresholdTimer.Start();
            MoveSignal(_direction);
        }
        else if (DirectionChanged())
        {
            repeatDelay.Start();
            MoveSignal(_direction);
        }
    }

    private bool DirectionChanged()
    {
        return !oldDirection.Equals(ClampDirection());
    }

    private bool IsFirstMoveInput()
    {
        return repeatDelay.IsStopped();
    }

    public bool IsMoving()
    {
        return Input.IsActionPressed("right") || Input.IsActionPressed("left") || Input.IsActionPressed("up") || Input.IsActionPressed("down");
    }

    public void StopMoving()
    {
        direction = Vector2I.Zero;
        thresholdTimer.Stop();
        repeatDelay.Stop();
    }

    private void StartMoveRepeat()
    {
        repeatDelay.Start();
        MoveSignal();
    }
    
    //Signal emit methods
    private void MoveSignal()
    {
        EmitSignal(SignalName.Move, ClampDirection());
        oldDirection = ClampDirection();
    }

    private void MoveSignal(Vector2I _direction)
    {
        EmitSignal(SignalName.Move, _direction);
    }

    private Vector2I ClampDirection()
    {
        return direction = direction.Clamp(new Vector2I(-1,-1), new Vector2I(1,1));
    }
}
