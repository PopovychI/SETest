using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

[RequireComponent(typeof(BoxCollider))]
public class MeleeAttackCast : MonoBehaviour
{
    public Action<IDamageable> onDamageEntity;
    public Action onDealDamage;
    [SerializeField] private float _damage;
    [SerializeField] private LayerMask _targetLayers;
    private BoxCollider attackBox;
    private float _usedDamage;

    private List<IDamageable> attackTargetList = new List<IDamageable>();

    public float Damage { get => _damage; set => _damage = _usedDamage = value; }


    public Action OnBoxEnabled, OnBoxDisabled;
    private void Awake()
    {
        attackBox = GetComponent<BoxCollider>();
        attackBox.enabled = false;
        _usedDamage = _damage;
    }
    public void SetCustomDamageForCast(float damage)
    {
        _usedDamage = damage;
    }
    public void AttackWithBoxCast()
    {
        Debug.Log("Attacking");
        if (!this.isActiveAndEnabled) return;

        var CollidersHitList = Physics.OverlapBox(
            transform.position +
            new Vector3(attackBox.center.x * transform.lossyScale.x, attackBox.center.y),
            attackBox.size,
            transform.rotation,
            _targetLayers);

        foreach (var t in CollidersHitList)
        {
            IDamageable _d;
            t.TryGetComponent(out _d);
            if (_d != null)
            {
                attackTargetList.Add(_d);
            }
        }

        if (attackTargetList.Count == 0) return;

        IDamageable _target;

        {
            _target = attackTargetList.First();
            onDamageEntity?.Invoke(_target);
        }
        _target.ReceiveDamage(_usedDamage);
        onDealDamage?.Invoke();
        Debug.Log("dealing damage to " + _target.ToString());
        attackTargetList.Clear();
        _usedDamage = _damage;

        return;
    }

    public bool CheckIfInRange()
    {
        var CollidersHitList = Physics.OverlapBox(
    transform.position +
    new Vector3(attackBox.center.x * transform.lossyScale.x, attackBox.center.y),
    attackBox.size,
    transform.rotation,
    _targetLayers);

        foreach (var t in CollidersHitList)
        {
            IDamageable _d;
            t.TryGetComponent(out _d);
            if (_d != null)
            {
                attackTargetList.Add(_d);
            }
        }

        if (attackTargetList.Count > 0) return true;
        else return false;

    }
}
