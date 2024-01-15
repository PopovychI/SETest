using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PlayerMoveState : State
{

    [Inject] private InputActionsInterpreter _input;
    public override State RunCurrentState()
    {
        return this;
    }
}
