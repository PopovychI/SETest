using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationController : EntityAnimationController
{
    private const string _getDamaged = "GetDamaged";
    public void GetHurt()
    {
        _anim.SetTrigger(_getDamaged);
    }
}
