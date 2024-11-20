using UnityEngine;
using UnityEngine.UI;

public class Crosshair : MonoBehaviour
{
    private Image crosshairImage;

    private void Awake()
    {
        crosshairImage = GetComponent<Image>();
    }

    public void ShowCrosshair()
    {
        crosshairImage.enabled = true;
    }

    public void HideCrosshair()
    {
        crosshairImage.enabled = false;
    }
}