using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField]
    private Transform head;
    private Transform currentHead;

    public void SetHead(Transform newHead) {
        newHead.SetParent(head);
        newHead.localPosition = Vector3.forward;
        currentHead = newHead;
    }

    public bool RemoveHeadIfThis(Transform headToRemove) {
        if (currentHead != headToRemove) {
            return false;
        }
        headToRemove.SetParent(null);
        currentHead = null;
        headToRemove.transform.position = new Vector3(headToRemove.transform.position.x, headToRemove.transform.position.y, 0);
        return true;
    }
}
