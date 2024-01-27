using UnityEngine;

public class ThrowablePickable : OverheadPickable
{
    private Quaternion direction;
    private float thrownAt = -100;
    public AnimationCurve speedCurve;
    public float travelTime;
    public float baseThrowSpeed;
    public float throwPowerSpeedMultiplier;
    private float thrownPower;

    protected override bool ReactToRelease(float power)
    {
        Transform oldHolder = currentHolder;
        bool isLaunched = base.ReactToRelease(power);
        if (!isLaunched)
        {
            return false;
        }
        direction = oldHolder.GetComponent<PlayerMovement>().lookingDirection;
        thrownAt = Time.time;
        thrownPower = baseThrowSpeed + baseThrowSpeed * throwPowerSpeedMultiplier * power;
        return true;
    }

    public bool IsBeingThrow()
    {
        return HasBeenThrown() && thrownAt + travelTime > Time.time;
    }

    public bool HasBeenThrown()
    {
        return thrownAt > -100;
    }

    public float StopThrow()
    {
        return thrownAt = -99;
    }

    public Vector2 GetFrameThrow()
    {
        if (!IsBeingThrow())
        {
            return Vector2.zero;
        }
        float thrownSince = Time.time - thrownAt;
        float frameThrowSpeed = speedCurve.Evaluate(thrownSince / travelTime) * thrownPower;
        return direction * Vector2.right * frameThrowSpeed;
    }
}
