using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTriggerRestart : MonoBehaviour
{
    [SerializeField]
    private PlayerControls playerControls;

    // Update is called once per frame
    void Update()
    {
        if (!GameState.instance.gameIsEnded)
        {
            return;
        }
        if (playerControls.picking || playerControls.secondaryAction)
        {
            GameState.instance.Restart();
        }
    }
}
