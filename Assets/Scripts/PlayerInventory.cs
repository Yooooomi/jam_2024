using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public Transform head;

    public void SetHead(Transform newHead) {
        newHead.SetParent(head);
        newHead.localPosition = Vector3.zero;
        head = newHead;
    }

    public bool RemoveHeadIfThis(Transform currentHead) {
        if (head != currentHead) {
            return false;
        }
        currentHead.SetParent(null);
        head = null;
        return true;
    }
}
