using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    private EntityAnimationController _animController;
    protected virtual void Die()
    {
        _animController.Die();
    }
}
