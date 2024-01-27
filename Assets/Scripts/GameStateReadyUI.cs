using TMPro;
using UnityEngine;

public class GameStateReadyUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI textMesh;

    private void Start()
    {
        GameState.instance.onGameReadyCountdownChanged.AddListener(OnGameReadyCountdownChanged);
        GameState.instance.onGameStarted.AddListener(OnGameStarted);
        GameState.instance.onPlayerReady.AddListener(OnPlayerReady);
        GameState.instance.onPlayerUnready.AddListener(OnPlayerUnready);
        GameState.instance.onPlayerCountChange.AddListener(OnPlayerCountChange);
        UpdateReadyText();
    }

    private void UpdateReadyText() {
        textMesh.text = $"{GameState.instance.CountReadyPlayers()} / {GameState.instance.CountPlayers()} players ready!";
    }

    private void OnGameReadyCountdownChanged(int count)
    {
        UpdateReadyText();
    }

    private void OnGameStarted()
    {
        textMesh.enabled = false;
    }

    private void OnPlayerReady()
    {
        UpdateReadyText();
    }

    private void OnPlayerUnready()
    {
        UpdateReadyText();
    }

    private void OnPlayerCountChange() {
        UpdateReadyText();
    }
}
