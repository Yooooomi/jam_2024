using UnityEngine;

public class Armor : MonoBehaviour
{
    public float stunDuration;

    private bool playerTrap = false;
    private float playerStunLeft = 0;

    void Update()
    {
        if (!playerTrap)
        {
            return;
        }
        playerStunLeft -= Time.deltaTime;
        if (playerStunLeft < 0)
        {
            OnTrapReleased();
        }
    }

    void OnTrapReleased()
    {
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (!playerTrap && !collider.CompareTag(Tags.PLAYER))
        {
            return;
        }
        ThrowablePickable throwable = collider.GetComponentInChildren<ThrowablePickable>();
        if (!throwable.IsBeingThrown())
        {
            return;
        }
        throwable.StopThrow();
        collider.GetComponent<SpeedModifier>().ApplyDot(new SpeedDot(stunDuration, 0));
        collider.GetComponent<JuggleModifier>().ApplyDot(new JuggleDot(stunDuration));
        playerTrap = true;
        playerStunLeft = stunDuration;
        GameState.instance.gamePoints.RegisterPlayerInArmor(throwable.oldHolder, collider.transform);
    }
}
