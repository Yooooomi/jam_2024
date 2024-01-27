using UnityEngine;

public class RegisterPlayerToGameState : MonoBehaviour
{
    private void Start() {
        GameState.instance.AddPlayer(transform);
    }
}
