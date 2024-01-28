using UnityEngine;

public class Armor : MonoBehaviour
{
    public float stunDuration;

    private float playerStunLeft = 0;
    [SerializeField]
    private Animator animator;
    private Transform playerTrapped;

    void Update()
    {
        if (playerTrapped == null)
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
        playerTrapped.GetComponentInChildren<SpriteRenderer>().enabled = true;
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (playerTrapped != null || !collider.CompareTag(Tags.PLAYER))
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
        playerTrapped = collider.transform;
        playerStunLeft = stunDuration;
        GameState.instance.gamePoints.RegisterPlayerInArmor(throwable.oldHolder, collider.transform);
        playerTrapped.GetComponent<PlayerMovement>().SetPos(transform.position);
        playerTrapped.GetComponentInChildren<SpriteRenderer>().enabled = false;
        animator.SetBool("playerTrap", true);
    }
}
