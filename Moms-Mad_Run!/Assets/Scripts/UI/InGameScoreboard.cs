using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InGameScoreboard : MonoBehaviour
{
    public string[] playerNames;
    public TextMeshProUGUI[] scoreboardRows;

    private void OnEnable()
    {
        if (playerNames == null) { Debug.Log("InGameScoreboard: player names not defined."); return; }
        if (scoreboardRows == null) { Debug.Log("InGameScoreboard: text component not specified."); return; }
        for (int i = playerNames.Length - 1; i < scoreboardRows.Length; i++) {
            scoreboardRows[i].text = "";
        }
    }

    public void updateScore(string playerName, int score) {
        int index = playerName.IndexOf(playerName);
        scoreboardRows[index].text = playerName + ": " + score.ToString();
    }
}
