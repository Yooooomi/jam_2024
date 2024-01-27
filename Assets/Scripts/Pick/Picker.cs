using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Picker : MonoBehaviour
{
    private Collider2D myCollider;
    private Pickable currentlyPicked;
    private PlayerControls controls;
    private float startedThrowingAt = -1;
    public float timeToMaxThrowPower;
    private bool wasHittingPickButton;

    private void Start()
    {
        controls = GetComponent<PlayerControls>();
        myCollider = GetComponent<Collider2D>();
    }

    private void PickNearest() {
        List<Collider2D> results = new List<Collider2D>();
        ContactFilter2D filter = new ContactFilter2D().NoFilter();
        filter.useTriggers = true;

        int resultCount = myCollider.OverlapCollider(filter, results);
        if (resultCount == 0)
        {
            return;
        }

        List<Pickable> pickables = results.Select(e => {
            Pickable pickable = e.GetComponent<Pickable>();
            if (pickable == null) {
                return null;
            }
            if (!pickable.CanBePicked()) {
                return null;
            }
            return pickable;
        }).Where(e => e != null).ToList();

        if (pickables.Count == 0) {
            return;
        }

        Pickable nearestPickable = pickables.OrderBy(e => (transform.position - e.transform.position).magnitude).First();
        nearestPickable.Pick(transform);
        currentlyPicked = nearestPickable;
    }

    private IEnumerator ReenableCollisions(List<Collider2D> colliders) {
        yield return new WaitForSecondsRealtime(.5f);
        colliders.ForEach(e => {
            if (e.IsDestroyed()) {
                return;
            }
            Physics2D.IgnoreCollision(myCollider, e, false);
        });
    }

    private void ReleaseCurrentlyPicked() {
        List<Collider2D> effectiveColliders = currentlyPicked.effectiveColliders;
        effectiveColliders.ForEach(e => Physics2D.IgnoreCollision(myCollider, e, true));
        currentlyPicked.Release(Mathf.Clamp01(Time.time - startedThrowingAt) / timeToMaxThrowPower);
        currentlyPicked = null;
        startedThrowingAt = -1;
        StartCoroutine(ReenableCollisions(effectiveColliders));
    }

    public bool IsHolding() {
        return currentlyPicked != null;
    }

    public bool IsThrowing() {
        return startedThrowingAt != -1;
    }

    public float GetCurrentThrowPower() {
        if (!IsThrowing()) {
            return -1;
        }
        return Mathf.Clamp01((Time.time - startedThrowingAt) / timeToMaxThrowPower);
    }

    private void Update()
    {
        if (!wasHittingPickButton && controls.picking && IsHolding()) {
            startedThrowingAt = Time.time;
        }

        if (wasHittingPickButton && !controls.picking) {
            if (IsHolding()) {
                ReleaseCurrentlyPicked();
            } else {
                PickNearest();
            }
        }

        wasHittingPickButton = controls.picking;
    }
}
