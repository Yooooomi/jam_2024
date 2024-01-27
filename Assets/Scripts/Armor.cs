using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Armor : MonoBehaviour
{
    public float stunDuration;

    private bool playerTrap = false;
    private float playerStunLeft = 0;

    void Update() {
        if (!playerTrap) {
            return;
        }
        playerStunLeft -= Time.deltaTime;
        if (playerStunLeft < 0) {
            OnTrapReleased();
        }
    }

    void OnTrapReleased() {
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D collider) {
        if (!playerTrap && !collider.CompareTag(Tags.PLAYER)) {
            return;
        }
        collider.GetComponent<SpeedModifier>().ApplyDot(new SpeedDot(stunDuration, 0));
        playerTrap = true;
        playerStunLeft = stunDuration;
    }
}
