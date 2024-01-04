using Godot;

public partial class InputController : Node {

    private Timer thresholdTimer = new(){WaitTime = 0.7};
    private Timer repeatDelay = new(){WaitTime = 0.16};

    private Vector2 direction;
    private Vector2 oldDirection;

    [Signal] public delegate void MoveEventHandler(Vector2 direction);
    [Signal] public delegate void FireEventHandler(int type);

    public override void _EnterTree() {
        
        AddChild(thresholdTimer);
        AddChild(repeatDelay);

        thresholdTimer.OneShot = true;
        thresholdTimer.Timeout += StartMoveRepeat;
        repeatDelay.Timeout += MoveIt;

        direction = Vector2.Zero;
    }

    public override void _ExitTree() {

        thresholdTimer.Timeout -= StartMoveRepeat;
        repeatDelay.Timeout -= MoveIt;
    }

    public override void _Notification(int what) {
        if (what == MainLoop.NotificationApplicationFocusOut) {
            thresholdTimer.Stop();
            repeatDelay.Stop();
        }
    }

    public override void _Process(double delta) {
        if(!IsMoving())
            if (!repeatDelay.IsStopped() || !thresholdTimer.IsStopped())
                StopMoving();
    }

    public override void _Input(InputEvent @event) {
        
        // Move conditionals
        if (@event.IsActionPressed("right")) {
            direction += Vector2.Right;
            NewMoveInput(Vector2.Right);
        }
        if (@event.IsActionPressed("left")) {
            direction += Vector2.Left;
            NewMoveInput(Vector2.Left);
        }
        if (@event.IsActionPressed("up")) {
            direction += Vector2.Down;
            NewMoveInput(Vector2.Down);
        }
        if (@event.IsActionPressed("down")) {
            direction += Vector2.Up;
            NewMoveInput(Vector2.Up);
        }

        if (@event.IsActionReleased("right") && !Input.IsActionPressed("right"))
            direction -= Vector2.Right;
        if (@event.IsActionReleased("left") && !Input.IsActionPressed("left"))
            direction -= Vector2.Left;
        if (@event.IsActionReleased("up") && !Input.IsActionPressed("up"))
            direction -= Vector2.Down;
        if (@event.IsActionReleased("down") && !Input.IsActionPressed("down"))
            direction -= Vector2.Up;

        // Other input conditionals
        if (@event.IsActionPressed("confirm")) {
            EmitSignal(SignalName.Fire, 1);
        }
        if (@event.IsActionPressed("cancel")) {
            EmitSignal(SignalName.Fire, 0);
        }
        // if (@event.IsActionPressed("misc")) {

        // }
    }

    private void NewMoveInput(Vector2 _direction) {
        if (direction.IsZeroApprox() && !IsFirstMoveInput()) {
            repeatDelay.Start();
            MoveIt();
        }
        else if (IsFirstMoveInput()) {
            repeatDelay.Stop();
            thresholdTimer.Start();
            MoveIt(_direction);
        }
        else if (DirectionChanged()) {
            repeatDelay.Start();
            MoveIt(_direction);
        }
    }

    private bool DirectionChanged() {
        return !oldDirection.IsEqualApprox(ClampDirection());
    }

    private bool IsFirstMoveInput() {
        return repeatDelay.IsStopped();
    }

    public bool IsMoving() {
        return Input.IsActionPressed("right") || Input.IsActionPressed("left") || Input.IsActionPressed("up") || Input.IsActionPressed("down");
    }

    public void StopMoving() {
        direction = Vector2.Zero;
        thresholdTimer.Stop();
        repeatDelay.Stop();
    }

    private void StartMoveRepeat() {
        repeatDelay.Start();
        MoveIt();
    }

    private void MoveIt() {
        EmitSignal(SignalName.Move, ClampDirection());
        oldDirection = ClampDirection();
    }

    private void MoveIt(Vector2 _direction) {
        EmitSignal(SignalName.Move, _direction);
    }

    private Vector2 ClampDirection() {
        return direction = direction.Clamp(new Vector2(-1,-1), new Vector2(1,1));
    }
}
