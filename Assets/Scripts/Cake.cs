using UnityEngine;

public class Cake : OverheadPickable
{
    public AnimationCurve speedCurve;
    public float speed;
    public float lifetime;
    private float thrownAt = -1;
    private Quaternion direction;

    protected override bool ReactToRelease() {
        Transform oldHolder = currentHolder;
        bool isLaunched = base.ReactToRelease();
        if (!isLaunched) {
            return false;
        }
        direction = oldHolder.rotation;
        thrownAt = Time.time;
        return true;
    }

    private void Update() {
        if (thrownAt == -1) {
            return;
        }
        float thrownSince = Time.time - thrownAt;
        float frameThrowSpeed = speedCurve.Evaluate(thrownSince / lifetime) * speed;
        if (frameThrowSpeed == 0) {
            Destroy(gameObject);
        }
        transform.position += direction * Vector3.right * frameThrowSpeed * Time.deltaTime;
    }
}
