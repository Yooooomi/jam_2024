using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    public static GameState instance;

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

    private void Awake()
    {
        instance = this;
    }

    public void RegisterJuggle(Transform player)
    {

    }

    public void RegisterFoodThrow(Transform throwBy)
    {

    }

    public void RegisterFoodThrowHit(Transform throwBy, Transform hit)
    {

    }

    public void RegisterPlayerThrow(Transform throwBy, Transform throwWho)
    {

    }

    public void RegisterPlayerInArmor(Transform throwBy, Transform playerBullied)
    {

    }

    public void AddPlayer(Transform transform) {
        int id = transform.GetInstanceID();
        players[id] = new PlayerState();
    }

    public void RemovePlayer(Transform transform) {
        int id = transform.GetInstanceID();
        players.Remove(id);
    }

    public void ReadyPlayer(Transform transform) {
        PlayerState state = players[transform.GetInstanceID()];
        state.Ready();
    }

    public void UnreadPlayer(Transform transform) {
        PlayerState state = players[transform.GetInstanceID()];
        state.Unready();
    }
}
