using UnityEngine;
using UnityEngine.Events;

public abstract class Pickable : MonoBehaviour
{
    [SerializeField]
    protected Transform root;
    public UnityEvent onPick = new UnityEvent();
    protected Transform currentHolder;

    public bool CanBePicked()
    {
        return currentHolder == null;
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
}
