using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodPickable : ThrowablePickable
{
    protected override bool ReactToRelease(float power)
    {
        bool isLaunched = base.ReactToRelease(power);
        if (!isLaunched) {
            return false;
        }
        GameState.instance.RegisterFoodThrow(root);
        return isLaunched;
    }
}
