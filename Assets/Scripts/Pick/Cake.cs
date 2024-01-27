using UnityEngine;

public class Cake : MonoBehaviour
{
    [SerializeField]
    private Transform root;
    public ThrowablePickable throwablePicker;
    public float slowEfficiency;
    public float slowTime;

    private void Update()
    {
        if (throwablePicker.IsBeingThrown())
        {
            Vector2 throwDirection = throwablePicker.GetFrameThrow();
            root.position += new Vector3(throwDirection.x, throwDirection.y, 0) * Time.deltaTime;
        } else if (throwablePicker.HasBeenThrown()) {
            Destroy(root.gameObject);
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

        GameState.instance.gamePoints.RegisterFoodThrowHit(throwablePicker.oldHolder, collider.transform);
        speedModifier.ApplyDot(new SpeedDot(slowTime, 1 - slowEfficiency));
        Destroy(root.gameObject);
    }
}
