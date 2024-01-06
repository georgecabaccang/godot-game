using Godot;

public partial class StateMachine : Node
{
    public virtual State CurrentState
    {
        get { return _currentState; }
        set { Transition(value); }
    }
    protected State _currentState;
    protected bool _inTransition;

    public Node States;

    public override void _EnterTree()
    {
        States = new(){Name = "States"};
        AddChild(States);
    }

    public virtual T GetState<T>() where T : State , new()
    {
        //Create new State node if node does not exist
        if (!States.HasNode(typeof(T).Name))
        {
            T newState = new(){Name = typeof(T).Name};
            States.AddChild(newState);
            return newState;
        }
        T target = States.GetNode<T>(typeof(T).Name);
        AddChild(target);
        return target;
    }

    public virtual void ChangeState<T>() where T : State , new()
    {
        CurrentState = GetState<T>();
    }

    protected virtual void Transition(State value)
    {
        if (_currentState == value || _inTransition)
            return;
        
        _inTransition = true;

        if (_currentState != null)
            _currentState.Exit();

        _currentState = value;

        if (_currentState != null)
            _currentState.Enter();

        _inTransition = false;
    }
}
