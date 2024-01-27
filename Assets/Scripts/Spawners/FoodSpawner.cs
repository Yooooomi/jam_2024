
using UnityEngine;

public class FoodSpawner : ObjectSpawner
{
    protected override void SpawnObject()
    {
        base.SpawnObject();
        Pickable pickable = spawnedGameObject.GetComponentInChildren<Pickable>();
        if (pickable == null) {
            Debug.LogWarning("No pickable on spawned food");
            return;
        }
        pickable.onPick.AddListener(OnPickupCallback);
    }

    public void OnPickupCallback()
    {
        OnObjectUsed();
    }
}
