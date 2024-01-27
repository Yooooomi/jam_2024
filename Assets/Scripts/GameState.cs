using System.Collections.Generic;
using UnityEngine;

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
    public static GameState instance;

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
    }

    public void RemovePlayer(Transform transform)
    {
        int id = transform.GetInstanceID();
        players.Remove(id);
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
    }

    public void UnreadPlayer(Transform transform)
    {
        PlayerState state = players[transform.GetInstanceID()];
        state.Unready();
    }
}
