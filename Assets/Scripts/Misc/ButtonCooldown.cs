using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ButtonCooldown : MonoBehaviour
{
    public Action<float> OnCooldown;

    [SerializeField] private float _cooldown;

    private Button _button;
    private float _currentCD;
    private WaitForEndOfFrame _wait = new();

    public void Awake()
    {
        _button = GetComponent<Button>();
    }
    public void BeginCooldown()
    {
        _button.interactable = false;
        _currentCD = 0;
        StartCoroutine(Cooldown());
    }
    private IEnumerator Cooldown()
    {
        while (_currentCD <= _cooldown)
        {
            _currentCD += Time.deltaTime;
            OnCooldown?.Invoke(_currentCD / _cooldown);
            yield return _wait;
        }
        _button.interactable = true;
        yield break;
    }

}

