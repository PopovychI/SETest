using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class EnemyStateManager : StateManager
{
    private Enemy _enemy;
    private EnemyIdleState _idleState = new();
    private EnemyMoveState _moveState = new();
    private EnemyAttackState _attackState = new();

    [Inject] private SceneManager _sceneMgr;

    private void Awake()
    {
        _enemy = GetComponentInParent<Enemy>();
    }

    public override void InitializeAllStates()
    {
        _idleState.InitializeState(_moveState, _attackState, _enemy, _sceneMgr);
        _moveState.InitializeState(_sceneMgr,_enemy, _idleState);
        _attackState.InitializeState(_enemy, _idleState);
        _currentState = _idleState;
    }
}
