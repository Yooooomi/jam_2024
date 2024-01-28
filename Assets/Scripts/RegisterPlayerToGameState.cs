using UnityEngine;

public class RegisterPlayerToGameState : MonoBehaviour
{
    private void Awake() {
        GameState.instance.AddPlayer(GetComponent<PlayerGameState>());
    }
}
