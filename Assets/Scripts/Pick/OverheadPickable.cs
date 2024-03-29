using UnityEngine;

public class OverheadPickable : Pickable
{
    protected override void ReactToPick()
    {
        PlayerInventory inventory = currentHolder.GetComponent<PlayerInventory>();
        if (inventory == null) {
            Debug.LogWarning("ReactToPick: No player inventory found");
        }
        inventory.SetHead(root);
    }

    protected override bool ReactToRelease(float power)
    {
        PlayerInventory inventory = currentHolder.GetComponent<PlayerInventory>();
        if (inventory == null) {
            Debug.LogWarning("ReactToRelease: No player inventory found");
        }
        bool deleted = inventory.RemoveHeadIfThis(root);
        if (!deleted) {
            Debug.Log("RemoveHeadIfThis false");
            return false;
        }
        return true;
    }
}
