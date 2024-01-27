using UnityEngine;
using UnityEngine.UI;

public class PowerDisplay : MonoBehaviour
{
    [SerializeField]
    private Slider slider;
    [SerializeField]
    private Image sliderFillImage;
    [SerializeField]
    private Color startColor;
    [SerializeField]
    private Color middleColor;
    [SerializeField]
    private Color endColor;
    

    public void Show() {
        slider.gameObject.SetActive(true);
    }

    public void Hide() {
        slider.gameObject.SetActive(false);
    }

    public void SetPower(float power) {
        slider.value = power;
        Color powerColor;
        if (power < .5f) {
            powerColor = Color.Lerp(startColor, middleColor, power * 2);
        } else {
            powerColor = Color.Lerp(middleColor, endColor, (power - .5f) * 2);
        }
        sliderFillImage.color = powerColor;
    }
}
