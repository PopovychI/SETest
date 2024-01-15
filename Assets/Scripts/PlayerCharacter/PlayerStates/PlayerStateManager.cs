using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PlayerStateManager : StateManager
{
    [Inject] private InputActionsInterpreter _input;
    private PlayerCharacterBehaviour _player;
    private PlayerAttackState _attackState = new();
    private PlayerMainState _mainState = new();
    private void Awake()
    {
        _player = GetComponent<PlayerCharacterBehaviour>();
    }
    public override void InitializeAllStates()
    {
        _mainState.InitializeState(_player, _attackState, _input);
        _attackState.InitializeState(_player, _mainState);
        _currentState = _mainState;
    }
}
