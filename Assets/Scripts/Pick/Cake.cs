using UnityEngine;

public class Cake : OverheadPickable
{
    public AnimationCurve speedCurve;
    public float speed;
    public float throwPowerSpeedMultiplier;
    public float lifetime;
    public float slowEfficiency;
    public float slowTime;

    private float thrownAt = -1;
    private float thrownPower;
    private Quaternion direction;

    protected override bool ReactToRelease(float power) {
        Transform oldHolder = currentHolder;
        bool isLaunched = base.ReactToRelease(power);
        if (!isLaunched) {
            return false;
        }
        thrownPower = power * throwPowerSpeedMultiplier;
        direction = oldHolder.rotation;
        thrownAt = Time.time;
        return true;
    }

    private bool IsThrown() {
        return thrownAt != -1;
    }

    private void Update() {
        if (!IsThrown()) {
            return;
        }
        float thrownSince = Time.time - thrownAt;
        float frameThrowSpeed = speedCurve.Evaluate(thrownSince / lifetime) * speed * thrownPower;
        if (frameThrowSpeed == 0) {
            Destroy(gameObject);
        }
        transform.position += direction * Vector3.right * frameThrowSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collider) {
        if (!IsThrown()) {
            return;
        }

        SpeedModifier speedModifier = collider.GetComponent<SpeedModifier>();

        if (speedModifier == null) {
            return;
        }

        Destroy(gameObject);
        speedModifier.ApplyDot(new SpeedDot(slowTime, 1 - slowEfficiency));
    }
}
