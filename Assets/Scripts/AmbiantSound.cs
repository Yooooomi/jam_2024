using UnityEngine;

public class AmbiantSound : MonoBehaviour
{
    [SerializeField]
    private AudioSource source;

    private void Start() {
        GameState.instance.onGameStarted.AddListener(Play);
    }

    private void Play() {
        source.Play();
    }
}
