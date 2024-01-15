

public class EnemyAttackState : State
{
    private Enemy _enemy;
    private EnemyAnimationController _anim;
    private MeleeAttackCast _attackCast;
    private EnemyIdleState _idleState;

    public void InitializeState(Enemy enemy,
                                EnemyIdleState idleState)
    {
        _enemy = enemy;
        _anim = _enemy.AnimationController;
        _attackCast = _enemy.AttackCast;
        _idleState = idleState;
    }
    public override void RunOnStart()
    {
        _anim.OnAttackSwing += _attackCast.AttackWithBoxCast;
        _anim.StartAttack();
    }
    public override State RunCurrentState()
    {
        if (_anim.IsAttackPlaying) return this;
        else return _idleState;
    }
    public override void RunOnExit()
    {
        _anim.OnAttackSwing -= _attackCast.AttackWithBoxCast;
    }
}
