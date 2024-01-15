using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Zenject;
[RequireComponent(typeof(TextMeshProUGUI))]
public class PlayerHealthUI : MonoBehaviour
{
    [Inject] private SceneManager _sceneMgr;
    private TextMeshProUGUI _text;
    private void Start()
    {
        _text = GetComponent<TextMeshProUGUI>();
        _sceneMgr.Player.OnHealthChange += UpdateText;
        _text.text = _sceneMgr.Player.Health.ToString();
    }
    private void UpdateText(float remainingHP)
    {
        _text.text = remainingHP.ToString();
    }
}
