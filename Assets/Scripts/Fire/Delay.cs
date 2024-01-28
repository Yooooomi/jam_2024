using System.Collections;
using UnityEngine;

public class Delay : MonoBehaviour
{
    public float delay;
    public Animator script;

    private IEnumerator StartDelayed() {
        yield return new WaitForSecondsRealtime(delay);
        script.enabled = true;
    }

    private void Start() {
        StartCoroutine(StartDelayed());
    }
}
