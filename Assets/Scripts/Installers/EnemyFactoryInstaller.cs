using UnityEngine;
using Zenject;

public class EnemyFactoryInstaller : MonoInstaller
{
    [SerializeField] private EnemyFactory _enemyFactory;
    public override void InstallBindings()
    {
        Container.BindInstance(_enemyFactory).AsSingle();
    }
}
