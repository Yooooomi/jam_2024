using UnityEngine;
using UnityEngine.Events;

public abstract class Pickable : MonoBehaviour
{
    [SerializeField]
    protected Transform root;
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
    protected abstract bool ReactToRelease(float power);

    public void Pick(Transform parentTransform)
    {
        currentHolder = parentTransform;
        ReactToPick();
    }

    public void Release(float power)
    {
        ReactToRelease(power);
        currentHolder = null;
    }
}
