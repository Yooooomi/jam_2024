using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Events;
using System.Collections;
using UnityEngine.SceneManagement;

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
    public bool gameIsEnded
    {
        get;
        private set;
    } = false;
    [SerializeField]
    private float cooldownTimeBeforeRestart = 5;
    private float endTime = 0;
    [SerializeField]
    private GameObject restartText;

    private Coroutine gameStartCoroutine;

    public Dictionary<int, PlayerGameState> players
    {
        get;
        private set;
    } = new Dictionary<int, PlayerGameState>();

    public bool gameStarted
    {
        get;
        private set;
    } = false;

    private void Awake()
    {
        instance = this;
        onGameEnd.AddListener(OnGameEnd);
    }

    void OnGameEnd()
    {
        gameIsEnded = true;
        endTime = Time.time;
    }

    public void AddPlayer(PlayerGameState playerGameState)
    {
        int id = playerGameState.gameObject.GetInstanceID();
        players[id] = playerGameState;
        onPlayerCountChange.Invoke();
        CheckAllPlayersReady();
    }

    public int GetPlayerIndex(int id) {
        int index = 0;
        foreach (var item in players)
        {
            if (item.Key == id) {
                return index;
            }
            index += 1;
        }
        Debug.LogWarning("Returning default player index");
        return 0;
    }

    public void RemovePlayer(PlayerGameState playerGameState)
    {
        int id = playerGameState.gameObject.GetInstanceID();
        players.Remove(id);
        onPlayerCountChange.Invoke();
        CheckAllPlayersReady();
    }

    public int GetPlayerIdWithMostPoint()
    {
        if (players.Count == 0)
        {
            Debug.LogError("Calling GetPlayerIdWithMostPoint but no player are available");
            return 0;
        }
        return players.OrderByDescending(p => p.Value.points).First().Key;
    }

    public int GetTotalPoints()
    {
        return players.Sum(p => p.Value.points);
    }

    public PlayerGameState GetPlayerState(int id)
    {
        return players[id];
    }

    public void ReadyPlayer(Transform playerTransform)
    {
        PlayerGameState state = players[playerTransform.gameObject.GetInstanceID()];
        state.Ready();
        onPlayerReady.Invoke();
        CheckAllPlayersReady();
    }

    public void UnreadyPlayer(Transform playerTransform)
    {
        PlayerGameState state = players[playerTransform.gameObject.GetInstanceID()];
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
        gameStarted = true;
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

    private bool ReadyToRestart() {
        return gameIsEnded && endTime + cooldownTimeBeforeRestart < Time.time;
    }

    void Update() {
        if (ReadyToRestart() && !restartText.activeSelf) {
            restartText.SetActive(true);
        }
    }

    public void Restart()
    {
        if (ReadyToRestart()) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}