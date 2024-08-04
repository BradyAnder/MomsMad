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
    public Dictionary<string, Color> playerColors; // Add this line

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
        SceneNameUpdate();
        if (currentLevelName == "MainMenu") // Destroys self if on main menu
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        Debug.Log("InGameScoreboard Debug");
        foreach (string i in playerNames)
        {
            Debug.Log(i);
        }
        if (playerNames == null) { Debug.Log("InGameScoreboard: player names not defined."); return; }
        if (scoreboardRows == null) { Debug.Log("InGameScoreboard: text component not specified."); return; }
        for (int i = playerNames.Length - 1; i < scoreboardRows.Length; i++)
        {
            scoreboardRows[i].text = "";
        }

        // Set the initial text color for each player
        for (int i = 0; i < playerNames.Length; i++)
        {
            if (playerColors != null && playerColors.TryGetValue(playerNames[i], out Color color))
            {
                scoreboardRows[i].color = color;
                Debug.Log($"Set color for {playerNames[i]}: {color}");
            }
        }
    }

    public void updateScore(int playerNumber, int score)
    {
        if (playerNumber < 0 || playerNumber >= scoreboardRows.Length)
        {
            Debug.LogError("Invalid player number.");
            return;
        }

        scoreboardRows[playerNumber].text = "Player " + playerNumber + ": " + score.ToString();

        // Update the text color for the player
        string playerName = playerNames[playerNumber];
        if (playerColors != null && playerColors.TryGetValue(playerName, out Color color))
        {
            scoreboardRows[playerNumber].color = color;
        }
    }

    public void resetScore()
    {
        foreach (TextMeshProUGUI text in scoreboardRows)
        {
            text.text = "";
        }
    }

    void SceneNameUpdate() // Update the scene name
    {
        // Get the current scene name
        Scene currentLevel = SceneManager.GetActiveScene();
        currentLevelName = currentLevel.name;
        // Debug.Log("Loaded Level: " + currentLevel.name);
    }
}
