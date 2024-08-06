using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class InGameScoreboard : MonoBehaviour
{
    public string[] playerNames;
    public TextMeshProUGUI[] scoreboardRows;
    private InGameScoreboard instance;
    private int currRoundNumber;

    private static string currentLevelName = "Null"; // Hold the current level scene

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {

    }

    private void OnEnable()
    {
        if (playerNames == null) { Debug.Log("InGameScoreboard: player names not defined."); return; }
        if (scoreboardRows == null) { Debug.Log("InGameScoreboard: text component not specified."); return; }
    }

    public void updateScore(int playerNumber, int score)
    {
        scoreboardRows[playerNumber - 1].text = "Player " + playerNumber + ": " + score.ToString();
    }

    public void resetScore()
    {
        foreach (TextMeshProUGUI text in scoreboardRows) {
            text.text = "";
        }
    }

}
