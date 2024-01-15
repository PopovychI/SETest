using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : State
{
    private EnemyMoveState _moveState;
    private EnemyAttackState _attackState;
    private Enemy _enemy;
    private SceneManager _sceneMgr;
    private Timer _timer = new();
    private Transform _playerTransform;
    public void InitializeState(EnemyMoveState moveState,
                                EnemyAttackState attackState,
                                Enemy enemy,
                                SceneManager sceneMgr)
    {
        _moveState = moveState;
        _attackState = attackState;
        _enemy = enemy;
        _sceneMgr = sceneMgr;
        _playerTransform = _sceneMgr.Player.transform.parent;        
        _timer.Start();
        _timer.Evaluate(_enemy.AttackCooldown);
    }
    public override State RunCurrentState()
    {
        if (_sceneMgr.GameOver) return this;
        if (!_enemy.Move.IsPositionNearby(_playerTransform.position, _enemy.RangeCheck)) return _moveState;
        else
        if (_timer.ElapsedTime > _enemy.AttackCooldown)
        {
            _timer.Stop();
            _timer.Start();
            return _attackState;
        }
        else
        {
            var dir = _playerTransform.position -_enemy.transform.position;
            dir.Normalize();
            _enemy.Move.RotateInDirection(dir);
        }
        _timer.Evaluate(Time.deltaTime);
        return this;
    }
}
