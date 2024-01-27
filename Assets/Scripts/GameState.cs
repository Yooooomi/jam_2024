using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    public static GameState instance;

    public Transform players;

    private void Awake() {
        instance = this;
    }
}
