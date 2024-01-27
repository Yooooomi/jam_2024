using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Juggle : MonoBehaviour
{
    public SpeedModifier playerSpeedModifier;
    public PlayerControls playerControls;
    public float juggleDuration = 1.0f;

    private float jugglingLeftTime = 0.0f;

    void StartToJuggle()
    {
        jugglingLeftTime = juggleDuration;
        playerSpeedModifier.ApplyDot(new SpeedDot(juggleDuration, 0.0f));
    }

    void StopJuggle()
    {
        jugglingLeftTime = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (jugglingLeftTime > 0.0f)
        {
            jugglingLeftTime -= Time.deltaTime;
            if (jugglingLeftTime <= 0.0f)
            {
                GameState.instance.gamePoints.RegisterJuggle(transform);
                StopJuggle();
            }
        }
        else if (playerControls.secondaryAction)
        {
            StartToJuggle();
        }
    }
}
