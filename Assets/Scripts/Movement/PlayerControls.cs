using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements.Experimental;

public class PlayerControls : MonoBehaviour
{
    public Vector2 direction;
    public bool picking;
    public bool secondaryAction = false;
    public bool shooting;

    private void OnMovement(InputValue value)
    {
        direction = value.Get<Vector2>();
    }

    private void OnPick(InputValue value)
    {
        picking = value.Get<float>() > 0;
    }

    private void OnSecondaryAction(InputValue value)
    {
        secondaryAction = value.Get<float>() > 0;
    }

    private void OnShoot(InputValue value) {
        shooting = value.Get<float>() > 0;
    }
}
