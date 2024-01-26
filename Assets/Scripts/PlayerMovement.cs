using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private PlayerControls controls;
    public float moveSpeed;

    private void Start() {
        controls = GetComponent<PlayerControls>();
    }

    private void Update() {
        transform.position += new Vector3(controls.direction.x, controls.direction.y, 0) * moveSpeed * Time.deltaTime;
    }
}
