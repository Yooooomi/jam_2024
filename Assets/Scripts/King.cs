using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class King : MonoBehaviour
{
    void Start()
    {
        GameState.instance.kingExpectationChangeEvent.AddListener(OnKingExpectationChange);
    }

    void OnDestroy() {
        GameState.instance.kingExpectationChangeEvent.RemoveListener(OnKingExpectationChange);
    }

    void OnKingExpectationChange(KingExpectationChangeEventArguments args) {
        Debug.Log("King expectation" + args.expectationType);
    }
}
