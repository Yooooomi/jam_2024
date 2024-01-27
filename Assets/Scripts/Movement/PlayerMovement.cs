using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private PlayerControls controls;
    public Rigidbody2D rigidbody2d;
    public SpeedModifier speedModifier;
    public float moveSpeed = 5.0f;
    public Quaternion lookingDirection;
    public ThrowablePickable throwable;

    private void Start()
    {
        controls = GetComponent<PlayerControls>();
    }

    public void SetPos(Vector2 pos) {
        transform.position = new Vector3(pos.x, pos.y, transform.position.z);
    }

    private void FixedUpdate()
    {
        if (throwable.IsBeingPicked()) {
            return;
        }
    
        Vector2 movement;
        if (throwable.IsBeingThrow()) {
            movement = throwable.GetFrameThrow();
        } else {
            movement = new Vector2(controls.direction.x, controls.direction.y) * moveSpeed * speedModifier.getValue();
        }
    
        rigidbody2d.MovePosition(Time.deltaTime * movement + new Vector2(transform.position.x, transform.position.y));

        if (Mathf.Abs(movement.x) > 0 || Mathf.Abs(movement.y) > 0) {
            lookingDirection = Quaternion.Euler(new Vector3(0, 0, Mathf.Atan2(movement.y, movement.x) * Mathf.Rad2Deg));
        }
    }
}
