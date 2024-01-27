using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnBulleEnd : MonoBehaviour
{
    [SerializeField]
    private King king; 
    public void OnBulleEndEvent() {
        king.DisplayExpectationIcon();
    }
}
