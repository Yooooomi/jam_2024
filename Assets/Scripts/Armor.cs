using UnityEngine;
using UnityEngine.Events;

public class Armor : MonoBehaviour
{
    public float stunDuration;

    private float playerStunLeft = 0;
    [SerializeField]
    private Animator animator;
    private Transform playerTrapped;
    public UnityEvent onTrapPlayer = new UnityEvent();

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
        playerTrapped.GetComponent<PlayerToogleVisibility>().SetPlayerVisibility(true);
        Destroy(gameObject);
    }

    private void TrapPlayer(ThrowablePickable throwable, Collider2D collider) {
        throwable.StopThrow();
        collider.GetComponent<SpeedModifier>().ApplyDot(new SpeedDot(stunDuration, 0));
        collider.GetComponent<JuggleModifier>().ApplyDot(new JuggleDot(stunDuration));
        playerTrapped = collider.transform;
        playerStunLeft = stunDuration;
        GameState.instance.gamePoints.RegisterPlayerInArmor(throwable.oldHolder, collider.transform);
        playerTrapped.GetComponent<PlayerMovement>().SetPos(transform.position);
        playerTrapped.GetComponent<PlayerToogleVisibility>().SetPlayerVisibility(false);
        animator.SetBool("playerTrap", true);
        onTrapPlayer.Invoke();
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
        TrapPlayer(throwable, collider);
    }
}
