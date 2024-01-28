using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameStateEndUI : MonoBehaviour
{
    [SerializeField]
    private GameObject endGameUI;
    [SerializeField]
    private GameObject playerPointPrefab;
    [SerializeField]
    private float spawnY;

    void Start()
    {
        GameState.instance.onGameEnd.AddListener(OnGameEnd);
    }

    void OnDestroy()
    {
        GameState.instance.onGameEnd.RemoveListener(OnGameEnd);
    }

    void OnGameEnd()
    {
        endGameUI.SetActive(true);
        float spaceForEachUI = 1980 / GameState.instance.players.Count;
        List<PlayerGameState> players = GameState.instance.players.Values.ToList();
        for (int i = 0; i < players.Count; i++)
        {
            float x = spaceForEachUI * i + spaceForEachUI / 2;
            x -= 1980 / 2;
            GameObject UIGameObject = Instantiate(playerPointPrefab, endGameUI.transform);
            UIGameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(x, spawnY);
            EndGamePlayerUi endGamePlayerUI = UIGameObject.GetComponent<EndGamePlayerUi>();
            endGamePlayerUI.UpdateUi(players[i]);
        }
    }

}
