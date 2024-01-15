using UnityEngine;
using Zenject;

public class SceneManagerInstaller : MonoInstaller
{
    [SerializeField] private SceneManager _sceneMgr;
    public override void InstallBindings()
    {
        Container.BindInstance(_sceneMgr).AsSingle();
    }
}