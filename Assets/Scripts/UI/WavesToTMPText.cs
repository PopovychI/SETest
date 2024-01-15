using TMPro;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(TextMeshProUGUI))]
public class WavesToTMPText : MonoBehaviour
{

    [Inject] private SceneManager _sceneMgr;
    private void Start()
    {
        GetComponent<TextMeshProUGUI>().text = _sceneMgr.WaveCount.ToString();
    }
}
