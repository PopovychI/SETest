using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class EnemyFactory : MonoBehaviour
{
    [SerializeField] private List<EnemyType> _enemiesToCreate;
    private List<Enemy> _allEnemies = new();

    [Inject] private DiContainer _diCont;

    private void Awake()
    {
        PopulateList();
    }
    private void PopulateList()
    {
        for (int i = 0; i < _enemiesToCreate.Count; i++)
        {
            for (int j = 0; j < _enemiesToCreate[i].enemyCount; j++)
            {
                var enemy = _diCont.InstantiatePrefabForComponent<Enemy>(_enemiesToCreate[i].enemy);
                enemy.gameObject.SetActive(false);
                _allEnemies.Add(enemy);
            }
        }
    }
    public Enemy GetEnemy(int enemyId)
    {
        for (int i = 0; i < _allEnemies.Count; i++)
        {
            if (!_allEnemies[i].gameObject.activeSelf && _allEnemies[i].Id == enemyId) return _allEnemies[i]; ;
        }
        return null;
    }
}

[Serializable]
public class EnemyType
{
    public Enemy enemy;
    public int enemyCount;
}
