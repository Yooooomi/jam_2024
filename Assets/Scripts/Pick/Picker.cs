using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Picker : MonoBehaviour
{
    private List<Pickable> pickables = new List<Pickable>();
    private Pickable currentlyPicked;
    private PlayerControls controls;
    private float startedThrowingAt = -1;
    public float timeToMaxThrowPower;
    private bool wasHittingPickButton;

    private void Start()
    {
        controls = GetComponent<PlayerControls>();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        Pickable pickable = collider.GetComponent<Pickable>();

        if (pickable == null)
        {
            return;
        }

        pickables.Add(pickable);
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        Pickable pickable = collider.GetComponent<Pickable>();

        if (pickable == null)
        {
            return;
        }

        pickables.Remove(pickable);
    }

    private void PickNearest() {
        if (pickables.Count == 0)
        {
            return;
        }
        List<Pickable> allowedPickables = pickables.Where(e => e.CanBePicked()).ToList();

        if (allowedPickables.Count == 0) {
            return;
        }

        Pickable nearestPickable = allowedPickables.OrderBy(e => (transform.position - e.transform.position).magnitude).First();
        nearestPickable.Pick(transform);
        currentlyPicked = nearestPickable;
    }

    private void ReleaseCurrentlyPicked() {
        currentlyPicked.Release(Mathf.Clamp01(Time.time - startedThrowingAt) / timeToMaxThrowPower);
        currentlyPicked = null;
        startedThrowingAt = -1;
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
