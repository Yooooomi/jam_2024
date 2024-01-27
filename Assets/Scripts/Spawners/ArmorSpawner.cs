using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ArmorSpawner : ObjectSpawner
{
    protected override void Update() {
        base.Update();
        if (itemIsPresent && spawnedGameObject.IsDestroyed()) {
            OnObjectUsed();
        }
    }
}
