using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

[RequireComponent(typeof(Button))]
public class PowerfulAttackButton : MonoBehaviour
{
    private Button _button;
    private ButtonCooldown _buttonCD;
    private PlayerCharacterBehaviour _player;
    [Inject] private SceneManager _sceneMgr;
    
    private void Awake()
    {
        _button = GetComponent<Button>();
        _buttonCD = GetComponent<ButtonCooldown>();
        _player = _sceneMgr.Player;
        _button.onClick.AddListener(TryPressButton);
    }

    private void TryPressButton()
    {
        if (_player.AttackCast.CheckIfInRange())
        {
            _player.QueuePowerfulAttack();
            _buttonCD.BeginCooldown();
        }
    }

}