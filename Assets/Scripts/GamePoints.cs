using UnityEngine;

public class GamePoints : MonoBehaviour
{
    public KingLifecycle kingLifecycle;

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

        private void AddPlayerPoint(PlayerState player, float points, KingExpectationType kingExpectationType, int playerBullied = 0)
    {
        if (kingExpectationType == kingLifecycle.currentKingExpectation &&
            (kingExpectationType != KingExpectationType.FocusPlayer || playerBullied == kingLifecycle.focusedPlayerId))
        {
            points *= pointsSystem.meetKingExpectationMultiplicator;
        }
        player.AddPoints(points);
    }

    public void RegisterJuggle(Transform player)
    {
        AddPlayerPoint(GameState.instance.GetPlayerState(player), pointsSystem.juggle, KingExpectationType.Juggle);
    }

    public void RegisterFoodThrow(Transform throwBy)
    {
        AddPlayerPoint(GameState.instance.GetPlayerState(throwBy), pointsSystem.foodThrow, KingExpectationType.CakeFlyingAround);
    }

    public void RegisterFoodThrowHit(Transform throwBy, Transform hit)
    {
        AddPlayerPoint(GameState.instance.GetPlayerState(throwBy), pointsSystem.foodThrowHit, KingExpectationType.CakeFlyingAround);
    }

    public void RegisterPlayerThrow(Transform throwBy, Transform throwWho)
    {
        AddPlayerPoint(GameState.instance.GetPlayerState(throwBy), pointsSystem.throwPlayer, KingExpectationType.FocusPlayer, throwWho.GetInstanceID());
    }

    public void RegisterPlayerInArmor(Transform throwBy, Transform playerBullied)
    {
        AddPlayerPoint(GameState.instance.GetPlayerState(throwBy), pointsSystem.throwPlayerInArmor, KingExpectationType.FocusPlayer, playerBullied.GetInstanceID());
    }
}
