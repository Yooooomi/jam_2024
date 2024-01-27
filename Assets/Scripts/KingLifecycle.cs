using System.Collections.Generic;
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
        ClearExpectation();
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
        ClearExpectation();
        KingStep activeKingStep = GetCurrentKingStep();
        currentKingExpectation = activeKingStep.expectations[Random.Range(0, activeKingStep.expectations.Count)];
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
            NextExpectation();
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
