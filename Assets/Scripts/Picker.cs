using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Picker : MonoBehaviour
{
    private List<Pickable> pickables = new List<Pickable>();
    private Pickable currentlyPicked;
    private PlayerControls controls;

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
        Debug.Log("ReleaseCurrentlyPicked");
        currentlyPicked.Release();
        currentlyPicked = null;
    }

    private void Update()
    {
        if (!controls.pick)
        {
            return;
        }
        if (currentlyPicked != null) {
            ReleaseCurrentlyPicked();
        } else {
            PickNearest();
        }
    }
}
