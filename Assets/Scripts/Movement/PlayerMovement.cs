using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private PlayerControls controls;
    public Rigidbody2D rigidbody2d;
    public SpeedModifier speedModifier;
    public float moveSpeed = 5.0f;
    public Quaternion lookingDirection;
    public ThrowablePickable throwable;
    public float currentSpeed {
        get;
        private set;
    }

    private void Start()
    {
        controls = GetComponent<PlayerControls>();
    }

    public void SetPos(Vector2 pos) {
        transform.position = new Vector3(pos.x, pos.y, transform.position.z);
    }

    public void MovePos(Vector2 pos) {
        rigidbody2d.MovePosition(pos);
    }

    private void FixedUpdate()
    {
        if (throwable.IsBeingPicked()) {
            return;
        }
    
        Vector2 movement;
        if (throwable.IsBeingThrown()) {
            movement = throwable.GetFrameThrow();
        } else {
            movement = new Vector2(controls.direction.x, controls.direction.y) * moveSpeed * speedModifier.getValue();
        }
    
        currentSpeed = movement.magnitude * moveSpeed;
        rigidbody2d.MovePosition(Time.deltaTime * movement + new Vector2(transform.position.x, transform.position.y));

        if (Mathf.Abs(movement.x) > 0 || Mathf.Abs(movement.y) > 0) {
            lookingDirection = Quaternion.Euler(new Vector3(0, 0, Mathf.Atan2(movement.y, movement.x) * Mathf.Rad2Deg));
        }
    }
}
