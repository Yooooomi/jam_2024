using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class King : MonoBehaviour
{

    [SerializeField]
    private List<Sprite> spritePerStep;
    [SerializeField]
    private SpriteRenderer kingStepSpriteRenderer;

    [System.Serializable]
    private struct SpriteWithExpectation
    {
        public KingExpectationType expectation;
        public Sprite sprite;
    }

    [SerializeField]
    private Animator animator;

    [SerializeField]
    private List<SpriteWithExpectation> spriteWithExpectations;

    [SerializeField]
    private SpriteRenderer expectationSpriteRenderer;

    void Start()
    {
        GameState.instance.kingLifecycle.kingExpectationChangeEvent.AddListener(OnKingExpectationChange);
        GameState.instance.kingLifecycle.kingStepChangeEvent.AddListener(OnKingStepChange);

    }

    void OnDestroy()
    {
        GameState.instance.kingLifecycle.kingExpectationChangeEvent.RemoveListener(OnKingExpectationChange);
        GameState.instance.kingLifecycle.kingStepChangeEvent.RemoveListener(OnKingStepChange);

    }

    public void DisplayExpectationIcon()
    {
        expectationSpriteRenderer.enabled = true;
    }

    void OnKingStepChange(KingStep kingStep) {
        if (spritePerStep.Count <= kingStep.stepIndex) {
            Debug.LogError("OnKingStepChange King.cs do not have enough sprite");
            return;
        }
        kingStepSpriteRenderer.sprite = spritePerStep[kingStep.stepIndex];
    }

    void OnKingExpectationChange(KingExpectationChangeEventArguments args)
    {
        bool haveExpectation = args.expectationType != KingExpectationType.Unspecified;
        animator.SetBool("HaveExpectation", haveExpectation);
        animator.SetTrigger("ChangeExpectation");

        if (!haveExpectation)
        {
            return;
        }

        expectationSpriteRenderer.enabled = false;
        SpriteWithExpectation spriteWithExpectation = spriteWithExpectations.Find(e => e.expectation == args.expectationType);
        expectationSpriteRenderer.sprite = spriteWithExpectation.sprite;
    }
}
