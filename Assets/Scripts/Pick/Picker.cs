using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Picker : MonoBehaviour
{
    private Collider2D myCollider;
    [SerializeField]
    private PlayerPickable ownPickable;

    [SerializeField]
    private SpeedModifier speedModifier;
    private SpeedDot carryPlayerSlowDot;
    [SerializeField]
    private JuggleModifier juggleModifier;
    private JuggleDot juggleDot = new JuggleDot(Dot.INFINITE_DURATION);

    public Pickable currentlyPicked {
        private set;
        get;
    }
    private PlayerControls controls;
    private float startedThrowingAt = -1;
    [SerializeField]
    private float maxTimePickingPlayer;
    private float startedPickingAt = -1;
    public float timeToMaxThrowPower;
    private bool wasHittingPickButton;
    [SerializeField]
    private List<Collider2D> unpickableColliders;
    [SerializeField]
    private float releaseThenPickCooldown;
    private float lastTimeReleased;
    public UnityEvent onThrow = new UnityEvent();

    private void Start()
    {
        controls = GetComponent<PlayerControls>();
        myCollider = GetComponent<Collider2D>();
    }

    private void PickNearest()
    {
        if (lastTimeReleased + releaseThenPickCooldown >= Time.time)
        {
            return;
        }
        List<Collider2D> results = new List<Collider2D>();
        ContactFilter2D filter = new ContactFilter2D().NoFilter();
        filter.useTriggers = true;

        int resultCount = myCollider.OverlapCollider(filter, results);
        if (resultCount == 0)
        {
            return;
        }

        List<Pickable> pickables = results.Select(e =>
        {
            if (unpickableColliders.Contains(e))
            {
                return null;
            }
            Pickable pickable = e.GetComponent<Pickable>();
            if (pickable == null)
            {
                return null;
            }
            if (!pickable.CanBePicked())
            {
                return null;
            }
            if (pickable.root.CompareTag(Tags.PLAYER))
            {
                Picker otherPlayerPicker = pickable.root.GetComponent<Picker>();
                if (otherPlayerPicker.IsHoldingPlayer())
                {
                    return null;
                }
            }
            return pickable;
        }).Where(e => e != null).ToList();

        if (pickables.Count == 0)
        {
            return;
        }

        Pickable nearestPickable = pickables.OrderBy(e => (transform.position - e.transform.position).magnitude).First();
        startedPickingAt = Time.time;
        nearestPickable.Pick(transform);
        currentlyPicked = nearestPickable;

        juggleModifier.ApplyDot(juggleDot);
        carryPlayerSlowDot = new SpeedDot(Dot.INFINITE_DURATION, currentlyPicked.speedDotValue);
        speedModifier.ApplyDot(carryPlayerSlowDot);
    }

    public bool IsHoldingPlayer() {
        return IsHolding() && currentlyPicked.root.CompareTag(Tags.PLAYER);
    }

    private IEnumerator ReenableCollisions(List<Collider2D> colliders)
    {
        yield return new WaitForSecondsRealtime(.5f);
        colliders.ForEach(e =>
        {
            if (e.IsDestroyed())
            {
                return;
            }
            Physics2D.IgnoreCollision(myCollider, e, false);
        });
    }

    private void ReleaseCurrentlyPicked(bool dropped)
    {
        speedModifier.CancelDot(carryPlayerSlowDot);
        juggleModifier.CancelDot(juggleDot);
        lastTimeReleased = Time.time;
        List<Collider2D> effectiveColliders = currentlyPicked.effectiveColliders;
        effectiveColliders.ForEach(e => Physics2D.IgnoreCollision(myCollider, e, true));
        if (dropped) {
            currentlyPicked.Drop();
        } else {
            currentlyPicked.Release(Mathf.Clamp01(Time.time - startedThrowingAt) / timeToMaxThrowPower);
            onThrow.Invoke();
        }
        currentlyPicked = null;
        startedThrowingAt = -1;
        StartCoroutine(ReenableCollisions(effectiveColliders));
    }

    public bool IsHolding()
    {
        return currentlyPicked != null;
    }

    public bool IsThrowing()
    {
        return startedThrowingAt != -1;
    }

    public float GetCurrentThrowPower()
    {
        if (!IsThrowing())
        {
            return -1;
        }
        return Mathf.Clamp01((Time.time - startedThrowingAt) / timeToMaxThrowPower);
    }

    private bool ShouldReleasePlayer() {
        return IsHoldingPlayer() && startedPickingAt + maxTimePickingPlayer <= Time.time;
    }

    private void Update()
    {
        if (ownPickable.IsBeingPicked() || ownPickable.IsBeingThrown())
        {
            return;
        }

        if (!wasHittingPickButton && controls.picking && IsHolding())
        {
            startedThrowingAt = Time.time;
        }

        if (ShouldReleasePlayer()) {
            ReleaseCurrentlyPicked(true);
        }

        if (wasHittingPickButton && !controls.picking)
        {
            if (IsHolding())
            {
                ReleaseCurrentlyPicked(false);
            }
            else
            {
                PickNearest();
            }
        }

        wasHittingPickButton = controls.picking;
    }
}
