using UnityEngine;
using UnityEngine.Events;

public class PlayerGameState : MonoBehaviour
{
    public class PlayerPointsEarnedEvent : UnityEvent<int> {}

    public PlayerPointsEarnedEvent onPlayerPointsEarned = new PlayerPointsEarnedEvent();

    private bool ready = false;

    public int points = 0;

    public void AddPoints(int toAdd)
    {
        points += toAdd;
        onPlayerPointsEarned.Invoke(toAdd);
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