using UnityEngine;

public class PlayerPickable : ThrowablePickable
{
    [SerializeField]
    private Rigidbody2D playerRigidbody;
    [SerializeField]
    private JuggleModifier juggleModifier;

    private JuggleDot juggleDot = new JuggleDot(Dot.INFINITE_DURATION);
    private bool juggleDotSet = false;

    protected override void ReactToPick()
    {
        base.ReactToPick();
        if (!juggleDotSet)
        {
            juggleDotSet = true;
            juggleModifier.ApplyDot(juggleDot);
        }

        playerRigidbody.simulated = false;
    }

    void Update()
    {
        if (juggleDotSet && !IsBeingPicked() && !IsBeingThrown())
        {
            juggleModifier.CancelDot(juggleDot);
            juggleDotSet = false;
        }
    }

    protected override bool ReactToRelease(float power)
    {
        bool isLaunched = base.ReactToRelease(power);
        if (!isLaunched)
        {
            return false;
        }
        GameState.instance.gamePoints.RegisterPlayerThrow(oldHolder, root);
        playerRigidbody.simulated = true;
        return isLaunched;
    }

}
