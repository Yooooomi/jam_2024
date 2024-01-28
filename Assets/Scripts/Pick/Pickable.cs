using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Pickable : MonoBehaviour
{
    static protected float DroppedPower = -1f;

    [SerializeField]
    public float speedDotValue = 1.0f;

    [SerializeField]
    public Transform root;
    public UnityEvent onPick = new UnityEvent();
    public Transform currentHolder {
        get;
        protected set;
    }
    [SerializeField]
    public List<Collider2D> effectiveColliders = new List<Collider2D>();

    public bool CanBePicked()
    {
        return currentHolder == null;
    }

    public bool IsBeingPicked()
    {
        return currentHolder != null;
    }

    protected abstract void ReactToPick();
    protected abstract bool ReactToRelease(float power);

    public void Pick(Transform parentTransform)
    {
        currentHolder = parentTransform;
        onPick.Invoke();
        ReactToPick();
    }

    public void Release(float power)
    {
        ReactToRelease(power);
        currentHolder = null;
    }

    public void Drop() {
        ReactToRelease(DroppedPower);
        currentHolder = null;
    }
}
