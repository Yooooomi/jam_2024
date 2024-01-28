using UnityEngine;

public class PlayerPointFeedback : MonoBehaviour
{
    private PlayerGameState playerGameState;
    [SerializeField]
    private float baseScale;
    [SerializeField]
    private float scalePerPoint;
    
    [SerializeField]
    private Transform star;
    [SerializeField]
    private Animator starHolderAnimator;

    private void Start() {
        playerGameState = GetComponent<PlayerGameState>();
        playerGameState.onPlayerPointsEarned.AddListener(Feedback);
    }

    public void Feedback(int points) {
        float finalScale = baseScale + scalePerPoint * points;
        star.localScale = new Vector3(finalScale, finalScale, finalScale);
        starHolderAnimator.SetTrigger("Feedback");
    }
}
