using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class King : MonoBehaviour
{

    [System.Serializable]
    private struct SpriteWithExpectation
    {
        public KingExpectationType expectation;
        public Sprite sprite;
        public Vector2 scale;
    }

    [SerializeField]
    private Animator bubbleAnimator;

    [SerializeField]
    private Animator kingAnimator;

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
        kingAnimator.SetInteger("step", kingStep.stepIndex);
    }

    void OnKingExpectationChange(KingExpectationChangeEventArguments args)
    {
        bool haveExpectation = args.expectationType != KingExpectationType.Unspecified;
        bubbleAnimator.SetBool("HaveExpectation", haveExpectation);
        bubbleAnimator.SetTrigger("ChangeExpectation");
        expectationSpriteRenderer.enabled = false;

        if (!haveExpectation)
        {
            return;
        }

        SpriteWithExpectation spriteWithExpectation = spriteWithExpectations.Find(e => e.expectation == args.expectationType);
        expectationSpriteRenderer.sprite = spriteWithExpectation.sprite;
        expectationSpriteRenderer.transform.localScale = spriteWithExpectation.scale;
    }
}
