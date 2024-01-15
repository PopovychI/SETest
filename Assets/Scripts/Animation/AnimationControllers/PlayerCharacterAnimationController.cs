using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacterAnimationController : EntityAnimationController
{
    private const string _powerfulAttack = "PowerfulAttack";
        private const string _getDamaged = "GetDamaged";
    public void SetPowerfulAttack()
    {
        _attackPlaying = true;
        _anim.SetTrigger(_powerfulAttack);
    }
    
    public void GetHurt()
    {
        _anim.SetTrigger(_getDamaged);
    }
}
