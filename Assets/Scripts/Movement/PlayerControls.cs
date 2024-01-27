using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{
    public Vector2 direction;
    public bool pick;
    public bool secondaryAction = false;

    private void LateUpdate()
    {
        pick = false;
    }

    private void OnMovement(InputValue value)
    {
        direction = value.Get<Vector2>();
    }

    private void OnPick(InputValue value)
    {
        pick = value.Get<float>() > 0;
    }

    private void OnSecondaryAction(InputValue value)
    {
        secondaryAction = value.Get<float>() > 0;
    }
}
