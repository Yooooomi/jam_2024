using UnityEngine;

public class PlayerPointFeedback : MonoBehaviour
{
    private PlayerGameState playerGameState;
    [SerializeField]
    private float starsPerPointRatio;
    
    [SerializeField]
    private ParticleSystem system;

    private void Start() {
        playerGameState = GetComponent<PlayerGameState>();
        playerGameState.onPlayerPointsEarned.AddListener(Feedback);
    }

    public void Feedback(int points) {
        int particleCount = (int) (points / starsPerPointRatio);
        system.Emit(particleCount);
    }
}
