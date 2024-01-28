using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public enum KingExpectationType
{
    Unspecified,
    FocusPlayer,
    Juggle,
    ThrowInArmor,
    CakeFlyingAround,
}

public struct KingExpectationChangeEventArguments
{
    public KingExpectationType expectationType;
    public int focusedPlayerId;
}

[System.Serializable]
public class KingStep
{
    public int pointsToReach;
    public List<KingExpectationType> expectations;
    public float minNewExpectationSeconds;
    public float maxNewExpectationSeconds;
    public int stepIndex;
}

public class KingLifecycle : MonoBehaviour
{
    public GamePoints gamePoints;

    public KingExpectationType currentKingExpectation
    {
        get;
        private set;
    } = KingExpectationType.Unspecified;
    public int focusedPlayerId
    {
        get;
        private set;
    } = 0;

    [SerializeField]
    private List<KingStep> kingSteps;
    private int currentKingStep = -1;

    public class KingExpectationChangeEvent : UnityEvent<KingExpectationChangeEventArguments> { }
    [HideInInspector]
    public KingExpectationChangeEvent kingExpectationChangeEvent = new KingExpectationChangeEvent();

    public class KingStepChangeEvent : UnityEvent<KingStep> { }
    [HideInInspector]
    public KingStepChangeEvent kingStepChangeEvent = new KingStepChangeEvent();

    private float timeForNewExpectation = 0.0f;

    void Start()
    {
        GameState.instance.onGameStarted.AddListener(NextKingStep);
    }

    void OnDestroy()
    {
        GameState.instance.onGameStarted.RemoveListener(NextKingStep);
    }

    void NextKingStep()
    {
        if (currentKingStep + 1 >= kingSteps.Count)
        {
            GameState.instance.onGameEnd.Invoke();
        }
        currentKingStep += 1;
        if (!HaveActiceKingStep())
        {
            return;
        }
        kingStepChangeEvent.Invoke(GetCurrentKingStep());
        NextExpectation();
    }

    void EmitKingExpectationEvent() {
            kingExpectationChangeEvent.Invoke(new KingExpectationChangeEventArguments() { expectationType = currentKingExpectation, focusedPlayerId = focusedPlayerId });
    }

    void ClearExpectation()
    {
        currentKingExpectation = KingExpectationType.Unspecified;
        focusedPlayerId = 0;

        KingStep activeKingStep = GetCurrentKingStep();
        timeForNewExpectation = Random.Range(activeKingStep.minNewExpectationSeconds, activeKingStep.maxNewExpectationSeconds);
    }

    void NextExpectation()
    {
        KingExpectationType oldKingExpectation = currentKingExpectation;
        ClearExpectation();
        KingStep activeKingStep = GetCurrentKingStep();
        List<KingExpectationType> eligibleExpectation = activeKingStep.expectations.Where(e => e != oldKingExpectation).ToList();
        currentKingExpectation = eligibleExpectation[Random.Range(0, eligibleExpectation.Count)];
        if (currentKingExpectation == KingExpectationType.FocusPlayer)
        {
            focusedPlayerId = GameState.instance.GetPlayerIdWithMostPoint();
        }
        kingExpectationChangeEvent.Invoke(new KingExpectationChangeEventArguments() { expectationType = currentKingExpectation, focusedPlayerId = focusedPlayerId });
    }

    void Update()
    {
        if (!HaveActiceKingStep())
        {
            return;
        }
        if (GameState.instance.GetTotalPoints() >= GetCurrentKingStep().pointsToReach)
        {
            NextKingStep();
            return;
        }
        timeForNewExpectation -= Time.deltaTime;
        if (timeForNewExpectation <= 0)
        {
            if (currentKingExpectation == KingExpectationType.Unspecified)
            {
                NextExpectation();
            }
            else
            {
                ClearExpectation();
            }
            EmitKingExpectationEvent();
        }
    }

    public bool HaveActiceKingStep()
    {
        return currentKingStep >= 0 && currentKingStep < kingSteps.Count;
    }

    public KingStep GetCurrentKingStep()
    {
        if (!HaveActiceKingStep())
        {
            Debug.LogError("Calling GetCurrentKingStep despite non are active");
            return new KingStep();
        }
        return kingSteps[currentKingStep];
    }

}
