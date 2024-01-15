using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class SceneManager : MonoBehaviour
{
    public System.Action<int> OnWaveStart;

    [SerializeField] private PlayerCharacterBehaviour _player;
    [SerializeField] private List<Enemy> _enemies;
    [SerializeField] private GameObject _lose;
    [SerializeField] private GameObject _win;

    private int _currWave = 0;
    [SerializeField] private LevelConfig _config;
    [Inject] private EnemyFactory _factory;

    public PlayerCharacterBehaviour Player => _player;
    public List<Enemy> Enemies => _enemies;
    public int WaveCount => _config.Waves.Length;
    public int CurrentWave => _currWave;
    public bool GameOver { get; set; }

    private void Start()
    {
        SpawnWave();
    }

    public void AddEnemy(Enemy enemy)
    {
        _enemies.Add(enemy);
    }
    public bool CheckIfEnemyNear(Transform transform, float range)
    {
        for (int i = 0; i < _enemies.Count; i++)
        {
            if (Vector3.Distance(transform.position, _enemies[i].transform.position) < range) return true;
        }
        return false;
    }
    public void RemoveEnemy(Enemy enemy)
    {
        _enemies.Remove(enemy);
        if (_enemies.Count == 0)
        {
            SpawnWave();
        }
        _player.Heal(enemy.MaxHealth / 10f);
    }

    public void SetGameOver()
    {
        GameOver = true;
        _lose.SetActive(true);
    }

    private void SpawnWave()
    {

        if (_currWave >= _config.Waves.Length)
        {
            _win.SetActive(true);
            return;
        }

        OnWaveStart?.Invoke(_currWave+1);
        var wave = _config.Waves[_currWave];
        foreach (EnemyType enemyType in wave._enemies)
        {
            for (int i = 0; i < enemyType.enemyCount; i++)
            {
                Vector3 pos = new Vector3(Random.Range(-10, 10), 0, Random.Range(-10, 10));
                var tmpChar = _factory.GetEnemy(enemyType.enemy.Id);
                tmpChar.transform.position = pos;
                tmpChar.RestartEnemy();
            }
        }
        _currWave++;


    }
    private void OnDestroy()
    {
        OnWaveStart = null;
    }

    public void Reset()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }


}
