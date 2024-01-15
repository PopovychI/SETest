using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using Zenject;

public class InputActionsInterpreterInstaller : MonoInstaller
{
    [SerializeField] private InputActionsInterpreter _input;
    public override void InstallBindings()
    {
        Container.BindInstance(_input).AsSingle();
    }
}
