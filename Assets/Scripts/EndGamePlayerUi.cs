using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EndGamePlayerUi : MonoBehaviour
{
    [SerializeField]
    private Image playerSpriteRenderer;
    [SerializeField]
    private Sprite[] playerColors;
    [SerializeField]
    private TextMeshProUGUI pointText;

    public void UpdateUi(PlayerGameState playerGameState) {
        pointText.text = playerGameState.points.ToString();
        int index = GameState.instance.GetPlayerIndex(playerGameState.gameObject.GetInstanceID());
        playerSpriteRenderer.sprite = playerColors[index];
    }
}
