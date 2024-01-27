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

public class KingLifecycle : MonoBehaviour
{
    public KingExpectationType currentKingExpectation = KingExpectationType.Unspecified;
    public int focusedPlayerId = 0;

    public class KingExpectationChangeEvent : UnityEvent<KingExpectationChangeEventArguments> { }
    [HideInInspector]
    public KingExpectationChangeEvent kingExpectationChangeEvent = new KingExpectationChangeEvent();
}
