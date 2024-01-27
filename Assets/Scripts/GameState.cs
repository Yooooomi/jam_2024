using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Events;
using System.Collections;

public class GameState : MonoBehaviour
{
    public class GameReadyCountdownChangeEvent : UnityEvent<int> {}

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

    private class PlayerState {
        private bool ready;
        
        public PlayerState() {
            ready = false;
        }

        public void Ready() {
            ready = true;
        }

        public void Unready() {
            ready = false;
        }

        public bool IsReady() {
            return ready;
        }
    }

    private Dictionary<int, PlayerState> players = new Dictionary<int, PlayerState>();

    private void Awake() {
        instance = this;
    }

    public void AddPlayer(Transform transform) {
        int id = transform.GetInstanceID();
        players[id] = new PlayerState();
        onPlayerCountChange.Invoke();
    }

    public void RemovePlayer(Transform transform) {
        int id = transform.GetInstanceID();
        players.Remove(id);
        onPlayerCountChange.Invoke();
    }

    public void ReadyPlayer(Transform transform) {
        PlayerState state = players[transform.GetInstanceID()];
        state.Ready();
        onPlayerReady.Invoke();
        CheckAllPlayersReady();
    }

    public void UnreadyPlayer(Transform transform) {
        PlayerState state = players[transform.GetInstanceID()];
        onPlayerUnready.Invoke();
        state.Unready();
    }

    public int CountPlayers() {
        return players.Values.Count;
    }

    public int CountReadyPlayers() {
        int nbReady = players.Values.Count(e => e.IsReady());

        return nbReady;
    }

    private IEnumerator startGameCountdown() {
        onGameReadyCountdownChanged.Invoke(3);
        yield return new WaitForSecondsRealtime(1f);
        onGameReadyCountdownChanged.Invoke(2);
        yield return new WaitForSecondsRealtime(1f);
        onGameReadyCountdownChanged.Invoke(1);
        yield return new WaitForSecondsRealtime(1f);
        onGameStarted.Invoke();
    }

    private void ReadyGame() {
        gameStartCoroutine = StartCoroutine(startGameCountdown());
    }

    private void UnreadyGame() {
        if (gameStartCoroutine != null) {
            StopCoroutine(gameStartCoroutine);
            gameStartCoroutine = null;
        }
    }

    private void CheckAllPlayersReady() {
        bool allReady = CountReadyPlayers() == players.Count;

        if (!allReady) {
            UnreadyGame();
        } else {
            ReadyGame();
        }
    }
}
