using UnityEngine;

public class KingSound : MonoBehaviour
{
    [SerializeField]
    private RandomSound step1;
    [SerializeField]
    private RandomSound step3;
    [SerializeField]
    private RandomSound expectations;

    private void Start() {
        GameState.instance.kingLifecycle.kingExpectationChangeEvent.AddListener(PlayExpectation);
        GameState.instance.kingLifecycle.kingStepChangeEvent.AddListener(PlayStep);
    }

    private void PlayExpectation(KingExpectationChangeEventArguments arg) {
        if (arg.expectationType != KingExpectationType.Unspecified) {
            expectations.PlayRandom();
        }
    }

    private void PlayStep(KingStep arg) {
        if (arg.stepIndex == 0) {
            step1.PlayRandom();
        }
        if (arg.stepIndex == 2) {
            step3.PlayRandom();
        }
    }
}
