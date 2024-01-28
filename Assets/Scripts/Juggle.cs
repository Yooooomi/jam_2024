using UnityEngine;

public class Juggle : MonoBehaviour
{
    [SerializeField]
    private JuggleModifier juggleModifier;
    public SpeedModifier playerSpeedModifier;
    public PlayerControls playerControls;
    public float juggleDuration = 1.0f;

    private float jugglingTime = 0f;
    public bool isJuggling
    {
        get;
        private set;
    }
    private SpeedDot infiniteDot;
    private SpeedDot initialJuggleDot;

    private void StartToJuggle()
    {
        isJuggling = true;
        initialJuggleDot = new SpeedDot(juggleDuration, .0f);
        playerSpeedModifier.ApplyDot(initialJuggleDot);
    }

    private void ContinueJuggling()
    {
        if (infiniteDot != null)
        {
            return;
        }
        infiniteDot = new SpeedDot(Dot.INFINITE_DURATION, .0f);
        playerSpeedModifier.ApplyDot(infiniteDot);
    }

    public void StopJuggle()
    {
        isJuggling = false;
        jugglingTime = 0f;
        playerSpeedModifier.CancelDot(initialJuggleDot);
        if (infiniteDot != null)
        {
            playerSpeedModifier.CancelDot(infiniteDot);
            infiniteDot = null;
        }
    }

    private bool HasJuggleAtLeastMinTime() {
        return infiniteDot != null;
    }


    private void Update()
    {
        if (!juggleModifier.CanJuggle())
        {
            if (isJuggling)
            {
                StopJuggle();
            }
            return;
        }
        if (isJuggling)
        {
            jugglingTime += Time.deltaTime;
        }

        if (jugglingTime > juggleDuration)
        {
            ContinueJuggling();
            jugglingTime -= juggleDuration;
            GameState.instance.gamePoints.RegisterJuggle(transform);
        }
        if (!isJuggling && playerControls.secondaryAction)
        {
            StartToJuggle();
        }
        if (isJuggling && !playerControls.secondaryAction && HasJuggleAtLeastMinTime())
        {
            StopJuggle();
        }
    }
}
