using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EndGamePlayerUi : MonoBehaviour
{
    [SerializeField]
    private Image playerSpriteRenderer;
    [SerializeField]
    private TextMeshProUGUI pointText;

    public void UpdateUi(PlayerGameState playerGameState) {
        pointText.text = playerGameState.points.ToString();
        Sprite sprite = GameState.instance.GetPlayerColors(playerGameState.gameObject.GetInstanceID());
        playerSpriteRenderer.sprite = sprite;
    }
}
