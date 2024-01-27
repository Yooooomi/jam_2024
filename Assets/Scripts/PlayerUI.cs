using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    private Picker picker;
    private PowerDisplay powerDisplay;
    private PlayerMovement movement;

    private void Start() {
        picker = GetComponent<Picker>();
        powerDisplay = GetComponent<PowerDisplay>();
        movement = GetComponent<PlayerMovement>();
    }

    private void Update() {
        powerDisplay.SetShootDirection(movement.lookingDirection);
        if (!picker.IsThrowing()) {
            powerDisplay.HideDirection();
            return;
        }
        powerDisplay.ShowDirection();
        powerDisplay.SetPower(picker.GetCurrentThrowPower());
    }
}
