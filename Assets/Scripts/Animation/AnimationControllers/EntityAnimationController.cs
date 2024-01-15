using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Animator))]
public class EntityAnimationController : MonoBehaviour
{
    public Action OnAttackSwing, OnAttackEnd, OnDie;
    private const string _die = "Die";
    private const string _move = "Move";
    private const string _attack = "Attack";
    protected Animator _anim;
    protected bool _attackPlaying;

    public bool IsAttackPlaying => _attackPlaying;
    private void Awake()
    {
        _anim = GetComponent<Animator>();
    }
    public virtual void Die()
    {
        _anim.SetTrigger(_die);
    }
    public virtual void Move()
    {
        _anim.SetBool(_move, true);
    }
    public virtual void Stop()
    {
        _anim.SetBool(_move, false);
    }
    public virtual void StartAttack()
    {
        _attackPlaying = true;
        _anim.SetTrigger(_attack);
    }
    public void AttackSwing()
    {
        OnAttackSwing?.Invoke();
    }
    public virtual void AttackEnded()
    {
        _attackPlaying = false;
        OnAttackEnd?.Invoke();
    }
    public void DieEnded()
    {
        OnDie?.Invoke();
    }

    private void OnDestroy()
    {
        OnAttackSwing = null;
        OnAttackEnd = null;
    }
}
