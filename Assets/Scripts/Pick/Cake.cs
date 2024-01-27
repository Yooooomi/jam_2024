using UnityEngine;

public class Cake : MonoBehaviour
{
    public AnimationCurve speedCurve;
    public ThrowablePickable throwablePicker;
    public float slowEfficiency;
    public float slowTime;

    private void Update()
    {
        if (throwablePicker.IsBeingThrow())
        {
            Vector2 throwDirection = throwablePicker.GetFrameThrow();
            transform.position += new Vector3(throwDirection.x, throwDirection.y, 0) * Time.deltaTime;
        } else if (throwablePicker.HasBeenThrown()) {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (!throwablePicker.HasBeenThrown()) {
            return;
        }
        SpeedModifier speedModifier = collider.GetComponent<SpeedModifier>();

        if (speedModifier == null)
        {
            return;
        }

        Destroy(gameObject);
        speedModifier.ApplyDot(new SpeedDot(slowTime, 1 - slowEfficiency));
    }
}
