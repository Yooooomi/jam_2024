using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField]
    private Transform head;
    private Transform currentHead;

    public void SetHead(Transform newHead) {
        newHead.SetParent(head);
        newHead.localPosition = Vector3.zero;
        currentHead = newHead;
    }

    public bool RemoveHeadIfThis(Transform headToRemove) {
        if (currentHead != headToRemove) {
            return false;
        }
        headToRemove.SetParent(null);
        currentHead = null;
        return true;
    }
}
