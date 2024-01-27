using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Events;
using System.Collections;

public enum KingExpectationType
{
    Unspecified,
    FocusPlayer,
    Juggle,
    ThrowInArmor,
    CakeFlyingAround,
}

public class GameState : MonoBehaviour
{
    public class GameReadyCountdownChangeEvent : UnityEvent<int> { }

    public static GameState instance;
    [HideInInspector]
    public UnityEvent onPlayerCountChange = new UnityEvent();
    [HideInInspector]
    public UnityEvent onPlayerReady = new UnityEvent();
    [HideInInspector]
    public UnityEvent onPlayerUnready = new UnityEvent();
    [HideInInspector]
    public GameReadyCountdownChangeEvent onGameReadyCountdownChanged = new GameReadyCountdownChangeEvent();
    [HideInInspector]
    public UnityEvent onGameStarted = new UnityEvent();
    private Coroutine gameStartCoroutine;

    [SerializeField]
    private PointsSystem pointsSystem;

    [System.Serializable]
    private struct PointsSystem
    {
        public float juggle;
        public float throwPlayer;
        public float throwPlayerInArmor;
        public float foodThrow;
        public float foodThrowHit;
        public float meetKingExpectationMultiplicator;

    }

    private class PlayerState
    {
        private bool ready;

        public float points = 0;

        public void AddPoints(float toAdd)
        {
            points += toAdd;
        }

        public PlayerState()
        {
            ready = false;
        }

        public void Ready()
        {
            ready = true;
        }

        public void Unready()
        {
            ready = false;
        }

        public bool IsReady()
        {
            return ready;
        }
    }

    private Dictionary<int, PlayerState> players = new Dictionary<int, PlayerState>();
    private KingExpectationType currentKingExpectation = KingExpectationType.Unspecified;
    int focusedPlayerId = 0;

    private void Awake()
    {
        instance = this;
    }

    private void AddPlayerPoint(PlayerState player, float points, KingExpectationType kingExpectationType, int playerBullied = 0)
    {
        if (kingExpectationType == currentKingExpectation &&
            (kingExpectationType != KingExpectationType.FocusPlayer || playerBullied == focusedPlayerId))
        {
            points *= pointsSystem.meetKingExpectationMultiplicator;
        }
        player.AddPoints(points);
    }

    public void RegisterJuggle(Transform player)
    {
        AddPlayerPoint(GetPlayerState(player), pointsSystem.juggle, KingExpectationType.Juggle);
    }

    public void RegisterFoodThrow(Transform throwBy)
    {
        AddPlayerPoint(GetPlayerState(throwBy), pointsSystem.foodThrow, KingExpectationType.CakeFlyingAround);
    }

    public void RegisterFoodThrowHit(Transform throwBy, Transform hit)
    {
        AddPlayerPoint(GetPlayerState(throwBy), pointsSystem.foodThrowHit, KingExpectationType.CakeFlyingAround);
    }

    public void RegisterPlayerThrow(Transform throwBy, Transform throwWho)
    {
        AddPlayerPoint(GetPlayerState(throwBy), pointsSystem.throwPlayer, KingExpectationType.FocusPlayer, throwWho.GetInstanceID());
    }

    public void RegisterPlayerInArmor(Transform throwBy, Transform playerBullied)
    {
        AddPlayerPoint(GetPlayerState(throwBy), pointsSystem.throwPlayerInArmor, KingExpectationType.FocusPlayer, playerBullied.GetInstanceID());
    }

    public void AddPlayer(Transform transform)
    {
        int id = transform.GetInstanceID();
        players[id] = new PlayerState();
        onPlayerCountChange.Invoke();
    }

    public void RemovePlayer(Transform transform)
    {
        int id = transform.GetInstanceID();
        players.Remove(id);
        onPlayerCountChange.Invoke();
    }

    private PlayerState GetPlayerState(Transform transform)
    {
        if (!players.ContainsKey(transform.GetInstanceID()))
        {
            Debug.LogError("Invalid player transform, not registered");
            return new PlayerState();
        }
        return players[transform.GetInstanceID()];
    }

    public void ReadyPlayer(Transform transform)
    {
        PlayerState state = players[transform.GetInstanceID()];
        state.Ready();
        onPlayerReady.Invoke();
        CheckAllPlayersReady();
    }

    public void UnreadyPlayer(Transform transform)
    {
        PlayerState state = players[transform.GetInstanceID()];
        state.Unready();
        onPlayerUnready.Invoke();
        CheckAllPlayersReady();
    }

    public int CountPlayers()
    {
        return players.Values.Count;
    }

    public int CountReadyPlayers()
    {
        int nbReady = players.Values.Count(e => e.IsReady());

        return nbReady;
    }

    private IEnumerator startGameCountdown()
    {
        onGameReadyCountdownChanged.Invoke(3);
        yield return new WaitForSecondsRealtime(1f);
        onGameReadyCountdownChanged.Invoke(2);
        yield return new WaitForSecondsRealtime(1f);
        onGameReadyCountdownChanged.Invoke(1);
        yield return new WaitForSecondsRealtime(1f);
        onGameStarted.Invoke();
    }

    private void ReadyGame()
    {
        gameStartCoroutine = StartCoroutine(startGameCountdown());
    }

    private void UnreadyGame()
    {
        if (gameStartCoroutine != null)
        {
            StopCoroutine(gameStartCoroutine);
            gameStartCoroutine = null;
        }
    }

    private void CheckAllPlayersReady()
    {
        bool allReady = CountReadyPlayers() == players.Count;

        if (!allReady)
        {
            UnreadyGame();
        }
        else
        {
            ReadyGame();
        }
    }
}