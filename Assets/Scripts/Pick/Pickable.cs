using UnityEngine;
using UnityEngine.Events;

public abstract class Pickable : MonoBehaviour
{
    private UnityEvent onPick;
    protected Transform currentHolder;

    public void OnPick()
    {
        onPick.Invoke();
    }

    public bool CanBePicked()
    {
        return currentHolder == null;
    }

    protected abstract void ReactToPick();
    protected abstract bool ReactToRelease();

    public void Pick(Transform parentTransform)
    {
        currentHolder = parentTransform;
        ReactToPick();
    }

    public void Release()
    {
        ReactToRelease();
        currentHolder = null;
    }
}
