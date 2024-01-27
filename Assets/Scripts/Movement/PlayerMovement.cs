using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private PlayerControls controls;
    public Rigidbody2D rigidbody2d;
    public SpeedModifier speedModifier;
    public float moveSpeed = 5.0f;

    private void Start()
    {
        controls = GetComponent<PlayerControls>();
    }

    public void SetPos(Vector2 pos) {
        transform.position = new Vector3(pos.x, pos.y, transform.position.z);
    }

    private void FixedUpdate()
    {
        Vector2 movement = new Vector2(controls.direction.x, controls.direction.y) * moveSpeed * speedModifier.getValue() * Time.deltaTime;
        rigidbody2d.MovePosition(movement + new Vector2(transform.position.x, transform.position.y));
    }
}