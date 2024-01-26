using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{
    private Controls input;
    public Vector2 direction;
    public bool pick;
    public bool secondaryAction = false;

    private void Awake()
    {
        input = new Controls();
    }

    private void LateUpdate()
    {
        pick = false;
    }

    private void OnEnable()
    {
        input.Enable();

        input.Player.Movement.performed += OnMovementPerformed;
        input.Player.Movement.canceled += OnMovementCancelled;

        input.Player.Pick.performed += OnTakePerformed;
        input.Player.Pick.canceled += OnTakeCancelled;

        input.Player.SecondaryAction.performed += OnSecondaryActionPerformed;
        input.Player.SecondaryAction.canceled += OnSecondaryActionCancelled;
    }

    private void OnDisable()
    {
        input.Disable();

        input.Player.Movement.performed -= OnMovementPerformed;
        input.Player.Movement.canceled -= OnMovementCancelled;

        input.Player.Pick.performed -= OnTakePerformed;
        input.Player.Pick.canceled -= OnTakeCancelled;

        input.Player.SecondaryAction.performed -= OnSecondaryActionPerformed;
        input.Player.SecondaryAction.canceled -= OnSecondaryActionCancelled;
    }

    private void OnMovementPerformed(InputAction.CallbackContext value)
    {
        direction = value.ReadValue<Vector2>();
    }

    private void OnMovementCancelled(InputAction.CallbackContext value)
    {
        direction = Vector2.zero;
    }

    private void OnTakePerformed(InputAction.CallbackContext value)
    {
        pick = value.ReadValue<float>() > 0;
    }

    private void OnTakeCancelled(InputAction.CallbackContext value)
    {
        pick = false;
    }

    private void OnSecondaryActionPerformed(InputAction.CallbackContext value)
    {
        secondaryAction = value.ReadValue<float>() > 0;
    }

    private void OnSecondaryActionCancelled(InputAction.CallbackContext value)
    {
        secondaryAction = false;
    }
}
