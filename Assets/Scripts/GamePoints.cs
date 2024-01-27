using UnityEngine;

public class GamePoints : MonoBehaviour
{
    public KingLifecycle kingLifecycle;

    [SerializeField]
    private PointsSystem pointsSystem;

    [System.Serializable]
    private struct PointsSystem
    {
        public int juggle;
        public int throwPlayer;
        public int throwPlayerInArmor;
        public int foodThrow;
        public int foodThrowHit;
        public int meetKingExpectationMultiplicator;
    }

    private void AddPlayerPoint(PlayerGameState player, int points, KingExpectationType kingExpectationType, int playerBullied = 0)
    {
        if (!GameState.instance.gameStarted) {
            return;
        }
        if (kingExpectationType == kingLifecycle.currentKingExpectation &&
            (kingExpectationType != KingExpectationType.FocusPlayer || playerBullied == kingLifecycle.focusedPlayerId))
        {
            points *= pointsSystem.meetKingExpectationMultiplicator;
        }
        player.AddPoints(points);
    }

    public void RegisterJuggle(Transform player)
    {
        AddPlayerPoint(GameState.instance.GetPlayerState(player.gameObject.GetInstanceID()), pointsSystem.juggle, KingExpectationType.Juggle);
    }

    public void RegisterFoodThrow(Transform throwBy)
    {
        AddPlayerPoint(GameState.instance.GetPlayerState(throwBy.gameObject.GetInstanceID()), pointsSystem.foodThrow, KingExpectationType.CakeFlyingAround);
    }

    public void RegisterFoodThrowHit(Transform throwBy, Transform hit)
    {
        AddPlayerPoint(GameState.instance.GetPlayerState(throwBy.gameObject.GetInstanceID()), pointsSystem.foodThrowHit, KingExpectationType.CakeFlyingAround);
    }

    public void RegisterPlayerThrow(Transform throwBy, Transform throwWho)
    {
        AddPlayerPoint(GameState.instance.GetPlayerState(throwBy.gameObject.GetInstanceID()), pointsSystem.throwPlayer, KingExpectationType.FocusPlayer, throwWho.GetInstanceID());
    }

    public void RegisterPlayerInArmor(Transform throwBy, Transform playerBullied)
    {
        AddPlayerPoint(GameState.instance.GetPlayerState(throwBy.gameObject.GetInstanceID()), pointsSystem.throwPlayerInArmor, KingExpectationType.FocusPlayer, playerBullied.GetInstanceID());
    }
}
