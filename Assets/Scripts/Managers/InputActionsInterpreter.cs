using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class InputActionsInterpreter : MonoBehaviour
{
    [SerializeField] private InputActionAsset _actions;
    private InputActionMap _map;
    private const string _forward = "Forward";
    private const string _backward = "Backward";
    private const string _left = "Left";
    private const string _right = "Right";
    private const string _attack = "Attack";
    private const string _menu = "Menu";
    private const string _custom = "CustomAction";

    private void Awake()
    {
        _map = _actions.actionMaps[0];
    }

    public InputAction ForwardAction => _map.FindAction(_forward);
    public InputAction BackwardAction => _map.FindAction(_backward);
    public InputAction CustomAction => _map.FindAction(_custom);
    public InputAction LeftAction => _map.FindAction(_left);
    public InputAction RightAction => _map.FindAction(_right);
    public InputAction AttackAction => _map.FindAction(_attack);
    
    public InputAction MenuAction => _map.FindAction(_menu);
}
