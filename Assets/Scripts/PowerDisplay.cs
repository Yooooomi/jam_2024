using UnityEngine;
using UnityEngine.UI;

public class PowerDisplay : MonoBehaviour
{
    [SerializeField]
    private Transform shootArrow;
    [SerializeField]
    private Image shootArrowImage;
    [SerializeField]
    private Color startColor;
    [SerializeField]
    private Color middleColor;
    [SerializeField]
    private Color endColor;
    

    public void ShowDirection() {
        shootArrow.gameObject.SetActive(true);
    }

    public void HideDirection() {
        shootArrow.gameObject.SetActive(false);
    }

    public void SetPower(float power) {
        Color powerColor;
        if (power < .5f) {
            powerColor = Color.Lerp(startColor, middleColor, power * 2);
        } else {
            powerColor = Color.Lerp(middleColor, endColor, (power - .5f) * 2);
        }
        shootArrowImage.color = powerColor;
    }

    public void SetShootDirection(Quaternion angle) {
        shootArrow.localRotation = angle;
    }
}
