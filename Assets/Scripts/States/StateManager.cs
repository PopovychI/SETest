using UnityEngine;

public class StateManager : MonoBehaviour
{
    [SerializeField] private string _currentStateName;
    protected State _currentState;
    private State _initialState;

    private State _nextState;

    public State CurrentState => _currentState;

    public virtual void InitializeAllStates()
    {
    }

    private void Start()
    {

        InitializeAllStates();
        _initialState = _currentState;
        _currentState?.RunOnStart();
    }

    private void Update() => RunStateMachine();

    public void ResetToMainState()
    {
        SwitchToTheNextState(_initialState);
    }
    private void RunStateMachine()
    {
        _nextState = _currentState?.RunCurrentState();
        if (_nextState != null) SwitchToTheNextState(_nextState);
        _currentStateName = _currentState.ToString();
    }

    private void SwitchToTheNextState(State _nextState)
    {
        if (_currentState != _nextState)
        {
            _currentState.RunOnExit();
            _nextState.RunOnStart();
        }

        _currentState = _nextState;
    }

    public void SwitchState(State _externalState)
    {
        if (_currentState != _externalState)
        {
            _currentState.RunOnExit();
            _externalState.RunOnStart();
        }

        _currentState = _externalState;
    }
}