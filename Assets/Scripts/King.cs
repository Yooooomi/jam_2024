using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class King : MonoBehaviour
{
    void Start()
    {
        GameState.instance.kingLifecycle.kingExpectationChangeEvent.AddListener(OnKingExpectationChange);
    }

    void OnDestroy() {
        GameState.instance.kingLifecycle.kingExpectationChangeEvent.RemoveListener(OnKingExpectationChange);
    }

    void OnKingExpectationChange(KingExpectationChangeEventArguments args) {
        Debug.Log("King expectation" + args.expectationType);
    }
}
