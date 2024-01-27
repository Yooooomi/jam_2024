using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSpawner : ObjectSpawner
{
    public void OnPickupCallback()
    {
        OnObjectUsed();
    }
}
