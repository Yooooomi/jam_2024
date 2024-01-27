using UnityEngine;

public class Juggle : MonoBehaviour
{
    public SpeedModifier playerSpeedModifier;
    public PlayerControls playerControls;
    public float juggleDuration = 1.0f;

    private float jugglingTime = 0f;
    public bool isJuggling {
        get;
        private set;
    }
    private SpeedDot infiniteDot;

    private void StartToJuggle()
    {
        isJuggling = true;
        playerSpeedModifier.ApplyDot(new SpeedDot(juggleDuration, .0f));
    }

    private void ContinueJuggling() {
        if (infiniteDot != null) {
            return;
        }
        infiniteDot = new SpeedDot(Dot.INFINITE_DURATION, .0f);
        playerSpeedModifier.ApplyDot(infiniteDot);
    }

    private void StopJuggle()
    {
        isJuggling = false;
        jugglingTime = 0f;
        if (infiniteDot != null) {
            playerSpeedModifier.CancelDot(infiniteDot);
            infiniteDot = null;
        }
    }


    private void Update()
    {
        if (isJuggling) {
            jugglingTime += Time.deltaTime;
        }
        if (jugglingTime > juggleDuration) {
            ContinueJuggling();
            jugglingTime -= juggleDuration;
            GameState.instance.gamePoints.RegisterJuggle(transform);
        }
        if (!isJuggling && playerControls.secondaryAction) {
            StartToJuggle();
        }
        if (isJuggling && !playerControls.secondaryAction) {
            StopJuggle();
        }
    }
}
