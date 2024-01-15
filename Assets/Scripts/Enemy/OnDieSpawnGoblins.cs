using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(Enemy))]
public class OnDieSpawnGoblins : MonoBehaviour
{
    [SerializeField] private EnemyType _enemyToSpawn;
    [Inject] private EnemyFactory _factory;

    private Enemy _enemy;

    private void Awake()
    {
        _enemy = GetComponent<Enemy>();
        _enemy.OnDie += Spawn;
    }
    private void Spawn()
    {
        for (int i = 0; i < _enemyToSpawn.enemyCount; i++)
        {
            var enemy = _factory.GetEnemy(_enemyToSpawn.enemy.Id);
            enemy.transform.position = transform.position;
            enemy.RestartEnemy();
        }
    }
}
