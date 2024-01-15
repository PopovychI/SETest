using TMPro;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(TextMeshProUGUI))]
public class CurrentWaveCountUI : MonoBehaviour
{
    [Inject] private SceneManager _sceneMgr;
    private TextMeshProUGUI _text;
    private void Start()
    {
        _text = GetComponent<TextMeshProUGUI>();
        _sceneMgr.OnWaveStart += UpdateText;
    }
    private void UpdateText(int wave)
    {
        _text.text = wave.ToString();
    }
}
