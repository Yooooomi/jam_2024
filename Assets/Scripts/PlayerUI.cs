using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    private Picker picker;
    private PowerDisplay powerDisplay;

    private void Start() {
        picker = GetComponent<Picker>();
        powerDisplay = GetComponent<PowerDisplay>();
    }

    private void Update() {
        if (!picker.IsThrowing()) {
            powerDisplay.Hide();
            return;
        }
        powerDisplay.Show();
        powerDisplay.SetPower(picker.GetCurrentThrowPower());
    }
}
