using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    public static GameState instance;

    public List<Transform> players;

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

}
