using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ButtonInactiveVisual : MonoBehaviour
{
    private Image _image;
    private ButtonCooldown _buttonCD;

    private void Awake()
    {
        _image = GetComponent<Image>();
        _buttonCD = GetComponentInParent<ButtonCooldown>();
        _buttonCD.OnCooldown += Show;
    }
    private void Show(float percent)
    {
        _image.fillAmount = 1f - percent;
    }
}
