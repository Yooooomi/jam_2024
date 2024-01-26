using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private PlayerControls controls;
    public Rigidbody2D rigidbody2d;
    public float moveSpeed;

    private void Start() {
        controls = GetComponent<PlayerControls>();
    }

    private void FixedUpdate() {
        Vector2 movement = new Vector2(controls.direction.x, controls.direction.y) * moveSpeed * Time.deltaTime;
        rigidbody2d.MovePosition(movement + new Vector2(transform.position.x, transform.position.y));
    }
}
