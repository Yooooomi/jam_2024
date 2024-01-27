using UnityEngine;

public class GameReadyArea : MonoBehaviour
{
    private void Start() {
        GameState.instance.onGameStarted.AddListener(DestroyArea);
    }

    private void OnTriggerEnter2D(Collider2D collider) {
        if (!collider.CompareTag(Tags.PLAYER)) {
            return;
        }

        GameState.instance.ReadyPlayer(collider.transform);
    }

    private void OnTriggerExit2D(Collider2D collider) {
        if (!collider.CompareTag(Tags.PLAYER)) {
            return;
        }

        GameState.instance.UnreadyPlayer(collider.transform);
    }

    private void DestroyArea() {
        Destroy(gameObject);
    }
}
