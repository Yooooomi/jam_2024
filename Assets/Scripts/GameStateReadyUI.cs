using System.Collections;
using TMPro;
using UnityEngine;

public class GameStateReadyUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI textMesh;
    private Animator animator;

    private void Start()
    {
        animator = textMesh.GetComponent<Animator>();

        GameState.instance.onGameReadyCountdownChanged.AddListener(OnGameReadyCountdownChanged);
        GameState.instance.onGameStarted.AddListener(OnGameStarted);
        GameState.instance.onPlayerReady.AddListener(OnPlayerReady);
        GameState.instance.onPlayerUnready.AddListener(OnPlayerUnready);
        GameState.instance.onPlayerCountChange.AddListener(OnPlayerCountChange);
        UpdateReadyText();
    }

    private void UpdateReadyText() {
        animator.SetTrigger("Bump");
        textMesh.text = $"{GameState.instance.CountReadyPlayers()} / {GameState.instance.CountPlayers()} players ready!";
    }

    private void OnGameReadyCountdownChanged(int count)
    {
        animator.SetTrigger("Bump");
        textMesh.text = $"{count}...";
    }

    private IEnumerator DisplayGameStarted() {
        animator.SetTrigger("Bump");
        textMesh.text = $"Make me laugh!";
        yield return new WaitForSecondsRealtime(1f);
        textMesh.enabled = false;
    }

    private void OnGameStarted()
    {
        StartCoroutine(DisplayGameStarted());
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
