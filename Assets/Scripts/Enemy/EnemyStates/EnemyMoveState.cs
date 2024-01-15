using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class EnemyMoveState : State
{

    private SceneManager _sceneMgr;
    private EnemyIdleState _idleState;
    private Enemy _enemy;
    private Transform _playerTransform;
    public void InitializeState(SceneManager sceneMgr,
                                Enemy enemy,
                                EnemyIdleState idleState)
    {
        _idleState = idleState;
        _sceneMgr = sceneMgr;
        _enemy = enemy;
    }
    public override void RunOnStart()
    {
        _playerTransform = _sceneMgr.Player.transform.parent;
        _enemy.Agent.isStopped = false;
        _enemy.AnimationController.Move();

    }
    public override State RunCurrentState()
    {
        _enemy.Agent.SetDestination(_playerTransform.position);
        if (_enemy.Move.IsPositionNearby(_playerTransform.position, _enemy.RangeCheck)) return _idleState;
        return this;
    }
    public override void RunOnExit()
    {
       if (_enemy.Agent.isOnNavMesh) _enemy.Agent.isStopped = true;
        _enemy.AnimationController.Stop();
    }

}
