using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Events;
using System.Collections;

public class PlayerState
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

public class GameState : MonoBehaviour
{
    public GamePoints gamePoints;
    public KingLifecycle kingLifecycle;

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
    public UnityEvent onGameEnd = new UnityEvent();
    private Coroutine gameStartCoroutine;

    private Dictionary<int, PlayerState> players = new Dictionary<int, PlayerState>();

    private void Awake()
    {
        instance = this;
    }

    public void AddPlayer(Transform transform)
    {
        int id = transform.GetInstanceID();
        players[id] = new PlayerState();
        onPlayerCountChange.Invoke();
    }

    public int GetPlayerIdWithMostPoint()
    {
        if (players.Count == 0) {
            Debug.LogError("Calling GetPlayerIdWithMostPoint but no player are available");
            return 0;
        }
        return players.OrderByDescending(p => p.Value.points).First().Key;
    }

    public int GetTotalPoints() {
        return (int)players.Sum(p => p.Value.points);
    }

    public void RemovePlayer(Transform transform)
    {
        int id = transform.GetInstanceID();
        players.Remove(id);
        onPlayerCountChange.Invoke();
    }

    public PlayerState GetPlayerState(Transform transform)
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