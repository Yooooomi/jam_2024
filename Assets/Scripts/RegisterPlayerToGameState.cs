using UnityEngine;

public class RegisterPlayerToGameState : MonoBehaviour
{
    private void Start() {
        GameState.instance.AddPlayer(GetComponent<PlayerGameState>());
        transform.position = new Vector3(transform.position.x, transform.position.y, -10);
    }
}
