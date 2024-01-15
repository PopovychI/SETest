using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;
using Zenject;

public class PlayerCharacterBehaviour : Entity, IDamageable
{
    public System.Action<float> OnHealthChange;

    private Movement _movement;
    [SerializeField] private float _hp;
    [SerializeField] private float _damage;
    [SerializeField] private float _powerfulAttackMultiplier = 3f;
    [SerializeField] private float _attackRange;


    private PlayerCharacterAnimationController _anim;
    private MeleeAttackCast _attackCast;
    public Movement CharMovement => _movement;
    public PlayerCharacterAnimationController AnimationController => _anim;
    public MeleeAttackCast AttackCast => _attackCast;

    public bool PowerfulAttack { get; set; }
    public float Health => _hp;
    public float Damage => _damage;

    [Inject] private SceneManager _sceneMgr;
    [Inject] private InputActionsInterpreter _input;

    private void Awake()
    {
        _movement = GetComponentInParent<Movement>();
        _anim = transform.parent.GetComponentInChildren<PlayerCharacterAnimationController>();
        _attackCast = transform.parent.GetComponentInChildren<MeleeAttackCast>();
        _movement.OnStartMove += _anim.Move;
        _movement.OnStop += _anim.Stop;
        _attackCast.Damage = _damage;
    }

    protected override void Die()
    {
        _anim.Die();
        _sceneMgr.SetGameOver();
    }
    public void QueuePowerfulAttack()
    {
        _attackCast.SetCustomDamageForCast(_damage * _powerfulAttackMultiplier);
        PowerfulAttack = true;
    }
    public void StartRecharge()
    {
        PowerfulAttack = false;
    }
    public void ReceiveDamage(float dmg)
    {
        _hp -= dmg;
       
        if (_hp <= 0)
        {
            _hp = 0;
            Die();
        }else _anim.GetHurt();
        OnHealthChange?.Invoke(_hp);
    }
    public void Heal(float value)
    {
        _hp += value;
        OnHealthChange?.Invoke(_hp);
    }
}
