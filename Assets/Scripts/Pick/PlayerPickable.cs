using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickable : ThrowablePickable
{
    [SerializeField]
    private Rigidbody2D playerRigidbody;

    protected override void ReactToPick()
    {
        base.ReactToPick();
        playerRigidbody.simulated = false;
    }

    protected override bool ReactToRelease(float power)
    {
        bool isLaunched = base.ReactToRelease(power);
        if (!isLaunched) {
            return false;
        }
        GameState.instance.gamePoints.RegisterPlayerThrow(oldHolder, root);
        playerRigidbody.simulated = true;
        return isLaunched;
    }

}
