using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowablePickable : OverheadPickable
{
    private Quaternion direction;
    private float thrownAt = -1;
    public AnimationCurve speedCurve;
    public float travelTime;
    public float throwSpeed;

    protected override bool ReactToRelease()
    {
        Transform oldHolder = currentHolder;
        bool isLaunched = base.ReactToRelease();
        if (!isLaunched)
        {
            return false;
        }
        direction = oldHolder.rotation;
        thrownAt = Time.time;
        return true;
    }

    public bool IsBeingThrow() {
        return HasBeenThrown() && thrownAt + travelTime > Time.time;
    }

    public bool HasBeenThrown() {
        return thrownAt > 0;
    }

    public Vector2 GetFrameThrow() {
        if (!IsBeingThrow()) {
            return Vector2.zero;
        }
        float thrownSince = Time.time - thrownAt;
        float frameThrowSpeed = speedCurve.Evaluate(thrownSince / travelTime) * throwSpeed;
        return direction * Vector2.right * frameThrowSpeed;
    }
}
