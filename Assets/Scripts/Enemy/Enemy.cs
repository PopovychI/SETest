using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

public class Enemy : Entity, IDamageable
{
    public int Id;
    public System.Action OnDie;

    [SerializeField] private float _hp;
    [SerializeField] private float _damage;
    [SerializeField] private float _rangeCheck = 2f;
    [SerializeField] private float _attackCd = 2f;

    private float _initHp;
    private EnemyAnimationController _anim;
    private NavMeshAgent _agent;
    private Movement _move;
    private MeleeAttackCast _attackCast;
    private StateManager _stateMgr;
    private BoxCollider _collider;
    public float Health => _hp;
    public float MaxHealth => _initHp;
    public float Damage => _damage;
    public Movement Move => _move;
    public EnemyAnimationController AnimationController => _anim;
    public MeleeAttackCast AttackCast => _attackCast;
    public float RangeCheck => _rangeCheck;
    public float AttackCooldown => _attackCd;
    public NavMeshAgent Agent => _agent;

    [Inject] private SceneManager _sceneMgr;

    private void Awake()
    {
        _initHp = _hp;
        _anim = GetComponentInChildren<EnemyAnimationController>();
        _attackCast = GetComponentInChildren<MeleeAttackCast>();
        _move = GetComponentInChildren<Movement>();
        _agent = GetComponent<NavMeshAgent>();
        _stateMgr = GetComponentInChildren<StateManager>();
        _collider = GetComponentInChildren<BoxCollider>();
        _attackCast.Damage = _damage;

    }

    protected override void Die()
    {
        OnDie?.Invoke();
        _sceneMgr.RemoveEnemy(this);
        _anim.Die();
        _anim.OnDie += Disable;
        _stateMgr.enabled = false;
        _collider.enabled = false;
    }
    private void Disable()
    {
        gameObject.SetActive(false);
    }
    public void RestartEnemy()
    {
        _sceneMgr.AddEnemy(this);
        _collider.enabled = true;
        _hp = _initHp;
        _stateMgr.ResetToMainState();
        _stateMgr.enabled = true;
        gameObject.SetActive(true);
    }
    public void ReceiveDamage(float dmg)
    {      
        _hp -= dmg;
        if (_hp <= 0) Die();
        else _anim.GetHurt();
    }
    private void OnDestroy()
    {
        OnDie = null;
    }
}
