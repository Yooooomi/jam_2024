using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ArmorSpawner : ObjectSpawner
{
    protected new void Update() {
        base.Update();
        if (itemIsPresent && spawnedGameObject.IsDestroyed()) {
            OnObjectUsed();
        }

    }
}
