using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : State
{
    private PlayerCharacterBehaviour _player;
    private PlayerCharacterAnimationController _anim;
    private MeleeAttackCast _attackCast;
    private PlayerMainState _mainState;
    public void InitializeState(PlayerCharacterBehaviour player, PlayerMainState mainState)
    {
        _player = player;
        _anim = _player.AnimationController;
        _mainState = mainState;
        _attackCast = _player.AttackCast;
    }
    public override void RunOnStart()
    {
        if (_player.PowerfulAttack) _anim.SetPowerfulAttack();
        else _anim.StartAttack();
        _anim.OnAttackSwing += _attackCast.AttackWithBoxCast;
        
    }
    public override State RunCurrentState()
    {
        if (_anim.IsAttackPlaying) return this;
        else return _mainState;
    }
    public override void RunOnExit()
    {
        if (_player.PowerfulAttack) _player.PowerfulAttack = false;
        _anim.OnAttackSwing -= _attackCast.AttackWithBoxCast;
    }
}
