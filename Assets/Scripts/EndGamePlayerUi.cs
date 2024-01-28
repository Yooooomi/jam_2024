using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EndGamePlayerUi : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI pointText;

    public void UpdateUi(PlayerGameState playerGameState) {
        pointText.text = playerGameState.points.ToString();
    }
}
